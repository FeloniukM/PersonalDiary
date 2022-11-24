using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonalDiary.BLL.Exceptions;
using PersonalDiary.BLL.Interfaces;
using PersonalDiary.Common.DTO.Auth;
using PersonalDiary.Common.DTO.User;
using PersonalDiary.Common.Security;
using PersonalDiary.DAL.Entities;
using PersonalDiary.DAL.Interfaces;

namespace PersonalDiary.BLL.Service
{
    public class AuthService : IAuthService
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;

        public AuthService(IJwtFactory jwtFactory,
            IMapper mapper,
            IRepository<User> userRepository, 
            IRepository<RefreshToken> refreshTokenRepository)
        {
            _jwtFactory = jwtFactory;
            _mapper = mapper;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthUserDTO> Authorize(UserLoginDTO userDto)
        {
            var userEntity = await _userRepository.Query()
                .Where(x => x.Email == userDto.Email)
                .FirstOrDefaultAsync();

            if (userEntity == null)
            {
                throw new HttpException(System.Net.HttpStatusCode.NotFound, "User was not found");
            }

            if (!SecurityHelper.ValidatePassword(userDto.Password, userEntity.Password, userEntity.Salt))
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, "Password not valid");
            }

            var token = await GenerateAccessToken(userEntity.Id, userEntity.Nickname, userEntity.Email);
            var user = _mapper.Map<UserDTO>(userEntity);

            return new AuthUserDTO
            {
                User = user,
                Token = token
            };
        }

        public async Task<AccessTokenDTO> GenerateAccessToken(Guid userId, string nickname, string email)
        {
            var refreshToken = _jwtFactory.GenerateRefreshToken();

            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                Token = refreshToken,
                UserId = userId
            });

            await _refreshTokenRepository.SaveChangesAsync();

            var accessToken = await _jwtFactory.GenerateAccessToken(userId, nickname, email);

            return new AccessTokenDTO(accessToken, refreshToken);
        }

        public async Task<AccessTokenDTO> RefreshToken(RefreshTokenDTO dto)
        {
            var userId = _jwtFactory.GetUserIdFromToken(dto.AccessToken, dto.SigningKey);
            var userEntity = await _userRepository.GetByKeyAsync(userId);

            if (userEntity == null)
            {
                throw new HttpException(System.Net.HttpStatusCode.NotFound, "User was not found");
            }

            var rToken = await _refreshTokenRepository
                .Query()
                .FirstOrDefaultAsync(t => t.Token == dto.RefreshToken && t.UserId == userId);

            if (rToken == null)
            {
                throw new HttpException(System.Net.HttpStatusCode.NotFound, "Refresh token not exist");
            }

            if (!rToken.IsActive)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, "Token has expired");
            }

            var jwtToken = await _jwtFactory.GenerateAccessToken(userEntity.Id, userEntity.Nickname, userEntity.Email);
            var refreshToken = _jwtFactory.GenerateRefreshToken();

            await _refreshTokenRepository.DeleteAsync(rToken);
            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                Token = refreshToken,
                UserId = userEntity.Id
            });

            await _refreshTokenRepository.SaveChangesAsync();

            return new AccessTokenDTO(jwtToken, refreshToken);
        }

        public async Task RevokeRefreshToken(string refreshToken, Guid userId)
        {
            var rToken = _refreshTokenRepository
                .Query()
                .FirstOrDefault(t => t.Token == refreshToken && t.UserId == userId);

            if (rToken == null)
            {
                throw new HttpException(System.Net.HttpStatusCode.NotFound, "Refresh token not exist");
            }

            await _refreshTokenRepository.DeleteAsync(rToken);
            await _refreshTokenRepository.SaveChangesAsync();
        }
    }
}

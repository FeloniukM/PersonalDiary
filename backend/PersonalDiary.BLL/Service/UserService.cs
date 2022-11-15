using AutoMapper;
using PersonalDiary.BLL.Interfaces;
using PersonalDiary.Common.DTO.User;
using PersonalDiary.Common.Security;
using PersonalDiary.DAL.Entities;
using PersonalDiary.DAL.Interfaces;

namespace PersonalDiary.BLL.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDTO> CreateUser(UserRegisterDTO userDto)
        {
            var userEntity = _mapper.Map<User>(userDto);
            var salt = SecurityHelper.GetRandomBytes();

            userEntity.Id = Guid.NewGuid();
            userEntity.Salt = Convert.ToBase64String(salt);
            userEntity.Password = SecurityHelper.HashPassword(userDto.Password, salt);

            await _userRepository.AddAsync(userEntity);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserDTO>(userEntity);
        }
    }
}

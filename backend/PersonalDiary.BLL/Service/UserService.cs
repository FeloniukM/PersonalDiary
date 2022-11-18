using AutoMapper;
using PersonalDiary.BLL.Interfaces;
using PersonalDiary.Common.DTO.User;
using PersonalDiary.Common.Email;
using PersonalDiary.Common.Security;
using PersonalDiary.DAL.Entities;
using PersonalDiary.DAL.Interfaces;

namespace PersonalDiary.BLL.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;
        private readonly IEmailService _emailService;

        public UserService(IRepository<User> userRepository, IMapper mapper, IEmailService emailService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<UserDTO> CreateUser(UserRegisterDTO userDto)
        {
            var userEntity = _mapper.Map<User>(userDto);
            var salt = SecurityHelper.GetRandomBytes();

            userEntity.Id = Guid.NewGuid();
            userEntity.Salt = Convert.ToBase64String(salt);
            userEntity.Password = SecurityHelper.HashPassword(userDto.Password, salt);
            userEntity.CreatedAt = userEntity.UpdatedAt = DateTime.UtcNow;

            await _userRepository.AddAsync(userEntity);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserDTO>(userEntity);
        }

        public async Task InviteUser(UserInviteDTO userInviteDTO)
        {
            var request = new MailRequest()
            {
                ToEmail = userInviteDTO.Email,
                Subject = "Welcom to our service - \"PersonalDiary\"",
                Body = "http://localhost:4200/auth/register"
            };

            await _emailService.SendEmailAsync(request, null);
        }
    }
}

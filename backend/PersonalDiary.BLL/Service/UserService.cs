﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task InviteUser(UserEmailDTO userInviteDTO, Guid adminId)
        {
            var user = await _userRepository.GetByKeyAsync(adminId);

            if (user != null && user.IsAdmin == true)
            {
                throw new Exception();
            }

            var request = new MailRequest()
            {
                ToEmail = userInviteDTO.Email,
                Subject = "Welcom to our service - \"PersonalDiary\"",
                Body = "http://localhost:4200/auth/register"
            };

            await _emailService.SendEmailAsync(request, null);
        }

        public async Task<UserInfoDTO> GetUserInfo(Guid userId)
        {
            var user = await _userRepository.GetByKeyAsync(userId);

            if(user == null)
            {
                throw new Exception();
            }

            return _mapper.Map<UserInfoDTO>(user);
        }

        public async Task ChangeUserRole(UserEmailDTO userInviteDTO, Guid adminId)
        {
            var admin = await _userRepository.GetByKeyAsync(adminId);

            if (admin == null || admin.IsAdmin == false) 
            { 
                throw new Exception();
            }

            var user = await _userRepository
                .Query()
                .Where(x => x.Email == userInviteDTO.Email)
                .FirstOrDefaultAsync();

            if(user == null)
            {
                throw new Exception();
            }

            user.IsAdmin = true;

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
        }
    }
}

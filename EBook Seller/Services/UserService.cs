using EBook_Seller.Data;
using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EBook_Seller.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IRoleRepo _roleRepo;
        public UserService(IUserRepo userRepo,IRoleRepo roleRepo)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
        }


        public async Task RegisterUser(RegisterDTO dto)
        {
            if (await _userRepo.EmailExistAsync(dto.Email))
            {
                throw new InvalidOperationException("User with this email already exist!! Please try other email");
            }

            var passwordHasher = new PasswordHasher<User>();
            var newUser = new User
            {
                UserName = dto.Name,
                Email = dto.Email,
            };
            newUser.Password = passwordHasher.HashPassword(newUser, dto.Password);

            await _userRepo.RegisterUser(newUser);

            var userRole = new UserRole { UserId = newUser.Id, RoleId = 3 };
            await _roleRepo.AssignRoleAsync(userRole);
        }
        
        public async Task<List<GetUsersDTO>> GetUsers()
        {
            var users = await _userRepo.GetUsers();
            return users;
        }
        public Task DeleteUser()
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

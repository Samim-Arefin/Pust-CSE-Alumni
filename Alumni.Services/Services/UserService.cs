using Alumni.Domain.DtoModels;
using Alumni.Domain.Entities;
using Alumni.Domain.Enums;
using Alumni.Domain.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Alumni.Services.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(string id);
        Task<List<UserDto>> AllUser();
        Task<UnitResponse> ChangeUserStatus(string id, StatusEnum status);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<UserService> _logger;
        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<List<UserDto>> AllUser()
        {
            try
            {
                var users = await _userManager.Users.Where(x => x.UserRoles.All(x => x.Role.Name == UserRoleType.User.ToString())).ToListAsync();
                return users.Select(x => new UserDto
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    Batch = (BatchEnum)x.Batch,
                    Session = x.Session,
                    Status = (StatusEnum)x.Status,
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->AllUser Error->{ex.Message}");
                return null;
            }
        }

        public async Task<UserDto> GetUserById(string id)
        {
            try
            {
                var hasUser = await _userManager.FindByIdAsync(id);

                if(hasUser is null)
                {
                    return null;
                }

                var hasRoles = await _userManager.GetRolesAsync(hasUser);

                return new()
                {
                    Id = hasUser.Id,
                    UserName = hasUser.UserName ?? string.Empty,
                    Email = hasUser.Email,
                    UserRole = hasRoles.Any() ? hasRoles.First() : string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->GetUserById Error->{ex.Message}");
                return null;
            }
        }

        public async Task<UnitResponse> ChangeUserStatus(string id, StatusEnum status)
        {
            try
            {
                var hasUser = await _userManager.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
                if(hasUser is null)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "User doesn't exists!"
                    };
                }

                hasUser.Status = status;
                await _userManager.UpdateAsync(hasUser);

                return new()
                {
                    IsSuccess = true,
                    Message = "Status changed successfully!"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->ChangeUserStatus Error->{ex.Message}");
                return new();
            }
        }
    }
}

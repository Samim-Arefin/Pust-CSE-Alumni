using Alumni.Domain.DtoModels;
using Alumni.Domain.Entities;
using Alumni.Domain.Enums;
using Alumni.Domain.Response;
using Alumni.Services.Helper;
using Alumni.Services.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Alumni.Services.Services
{
    public interface IAuthenticationService
    {
        Task<UnitResponse> Register(RegisterRequest registerRequest);
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<UnitResponse> ResendConfirmationEmail(ConfimationMailRequest confimationMail);
        Task<UnitResponse> ConfirmEmail(string id, string token);
        Task<UnitResponse> ForgotPassword(ForgotPasswordRequest forgotPassword);
        Task<UnitResponse> ResetPassword(ResetPasswordRequest resetPassword, string Id, string token);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IDataProtectionService _dataProtection;
        private readonly ILogger<AuthenticationService> _logger;
        public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, IEmailService emailService,
            IDataProtectionService dataProtection, ILogger<AuthenticationService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _dataProtection = dataProtection;
            _logger = logger;
        }

        public async Task<UnitResponse> Register(RegisterRequest registerRequest)
        {
            try
            {
                if (string.Equals(registerRequest.Email, AppSettings.Settings.AdminEmail, StringComparison.OrdinalIgnoreCase))
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "System error!"
                    };
                }

                var hasUser = await _userManager.FindByEmailAsync(registerRequest.Email);

                if (hasUser is not null)
                {
                    var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(hasUser);
                    return isEmailConfirmed ? new() { IsSuccess = false, Message = "User already exits! Please Log in" }
                                            : new() { IsSuccess = false, Message = "Existing user! please verify your email!" };           
                }

                var firstTwoDigitOfRoll = registerRequest.Roll.Substring(0, 2);
                var user = new User
                {
                    Email = registerRequest.Email,
                    Roll = registerRequest.Roll,
                    Batch = FindBatch.BatchNumber(firstTwoDigitOfRoll),
                    Session = FindSession.Session(firstTwoDigitOfRoll),
                    UserName = registerRequest.Email.Split('@')[0],
                    NormalizedEmail = registerRequest.Email,
                    NormalizedUserName = registerRequest.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Status = StatusEnum.Pending,
                    EmailConfirmed = false,
                };

                var newUser = await _userManager.CreateAsync(user, registerRequest.Password);

                if (!newUser.Succeeded)
                {
                    var errorList = newUser.Errors.ToList();
                    return new()
                    {
                        IsSuccess = false,
                        Message = string.Join("/n", errorList.Select(e => e.Description))
                    };
                }

                var hasRole = await _roleManager.RoleExistsAsync(UserRoleType.User.ToString());
                if(hasRole)
                {
                    await _userManager.AddToRoleAsync(user, UserRoleType.User.ToString());
                }

                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var encryptEmailToken = await _dataProtection.Encrypt(emailToken);

                await _emailService.SendEmailConfirmationAsync(user.Id, user.Email, encryptEmailToken, EmailSubjectEnum.EmailConfirmation);

                return new()
                {
                    IsSuccess = true,
                    Message = "An email sent to the user with a link for registration verification"
                };

            }
            catch(Exception ex)
            {
                _logger.LogError($"Method->Register Error->{ex.Message}");
                return new()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            try
            {
                var hasUser = await _userManager.FindByEmailAsync(loginRequest.Email);
                if(hasUser is null)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "User doesn't exists!"
                    };
                }

                if (!hasUser.EmailConfirmed)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Please verify your email!"
                    };
                }

                var login = await _signInManager.CheckPasswordSignInAsync(hasUser, loginRequest.Password, true);

                if (!login.Succeeded)
                {
                    if (!login.IsLockedOut)
                    {
                        return new()
                        {
                            IsSuccess = false,
                            Message = "Password is incorrect!"
                        };
                    }

                    return new()
                    {
                        IsSuccess = false,
                        Message = "Your account is locked out. Kindly wait for 10 minutes and try again!"
                    };
                }

                if (hasUser.Status is StatusEnum.Pending)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Your account is not activated yet, please contact the admin!"
                    };
                }

                return new()
                {
                    IsSuccess = true,
                    UserId = hasUser.Id,
                    Message = "Login successful!"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->Login Error->{ex.Message}");
                return new()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<UnitResponse> ResendConfirmationEmail(ConfimationMailRequest confimationMail)
        {
            try
            {
                if (string.Equals(confimationMail.Email, AppSettings.Settings.AdminEmail, StringComparison.OrdinalIgnoreCase))
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "System error!"
                    };
                }

                var hasUser = await _userManager.FindByEmailAsync(confimationMail.Email);

                if (hasUser is null)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "User doesn't exists!"
                    };
                }

                var IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(hasUser);
                if(IsEmailConfirmed)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Email already verified!"
                    };
                }

                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(hasUser);

                var encryptEmailToken = await _dataProtection.Encrypt(emailToken);
                await _emailService.SendEmailConfirmationAsync(hasUser.Id, hasUser.Email, encryptEmailToken, EmailSubjectEnum.EmailConfirmation);

                return new()
                {
                    IsSuccess = true,
                    Message = "An email sent to the user with a link for registration verification"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->ResendConfirmationEmail Error->{ex.Message}");
                return new()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<UnitResponse> ConfirmEmail(string id, string token)
        {
            try
            {
                var hasUser = await _userManager.FindByIdAsync(id);
                if(hasUser is null)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "User doesn't exists!"
                    };
                }

                if (string.IsNullOrEmpty(token))
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Token is not valid!"
                    };
                }

                var decryptEmailToken = await _dataProtection.Decrypt(token);
                var response = await _userManager.ConfirmEmailAsync(hasUser, decryptEmailToken);

                if (!response.Succeeded)
                {
                    var errorList = response.Errors.ToList();
                    return new()
                    {
                        IsSuccess = false,
                        Message = string.Join("/n", errorList.Select(e => e.Description))
                    };
                }

                return new()
                {
                    IsSuccess = true,
                    Message = "Email verified successfully!"
                };
            }
            catch(Exception ex)
            {
                _logger.LogError($"Method->ConfirmEmail Error->{ex.Message}");
                return new()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<UnitResponse> ForgotPassword(ForgotPasswordRequest forgotPassword)
        {
            try
            {
                if (string.Equals(forgotPassword.Email, AppSettings.Settings.AdminEmail, StringComparison.OrdinalIgnoreCase))
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "System error!"
                    };
                }

                var hasUser = await _userManager.FindByEmailAsync(forgotPassword.Email);
                if (hasUser is null)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "User doesn't exists!"
                    };
                }

                var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);
                var encryptedResetPasswordToken = await _dataProtection.Encrypt(resetPasswordToken);

                await _emailService.SendEmailConfirmationAsync(hasUser.Id, hasUser.Email, encryptedResetPasswordToken, EmailSubjectEnum.ResetPasswordEmail);
                return new()
                {
                    IsSuccess = true,
                    Message = "An email sent to the user with a link for password reset"
                };
            }
            catch(Exception ex)
            {
                _logger.LogError($"Method->ForgotPassword Error->{ex.Message}");
                return new()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<UnitResponse> ResetPassword(ResetPasswordRequest resetPassword, string id, string token)
        {
            try
            {
                var hasUser = await _userManager.FindByIdAsync(id);
                if(hasUser is null)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "User doesn't exists!"
                    };
                }

                if (string.IsNullOrEmpty(token))
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Token is not valid!"
                    };
                }

                var decryptEmailToken = await _dataProtection.Decrypt(token);
                var response = await _userManager.ResetPasswordAsync(hasUser, decryptEmailToken, resetPassword.Password);

                if (!response.Succeeded)
                {
                    var errorList = response.Errors.ToList();
                    return new()
                    {
                        IsSuccess = false,
                        Message = string.Join("/n", errorList.Select(e => e.Description))
                    };
                }

                if(await _userManager.IsLockedOutAsync(hasUser))
                {
                    await _userManager.SetLockoutEndDateAsync(hasUser, DateTimeOffset.UtcNow);
                }

                return new()
                {
                    IsSuccess = true,
                    Message = "Password changed successfully!"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->ResetPassword Error->{ex.Message}");
                return new()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}

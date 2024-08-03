using System;
using System.Text.Json;
using _11_Lesson_Auth.Datas;
using _11_Lesson_Auth.Encryptors;
using _11_Lesson_Auth.Models;
using _11_Lesson_Auth.Models.Entities;
using _11_Lesson_Auth.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace _11_Lesson_Auth.Services;

public class UserManager : IUserManager
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _accessor;

    public UserManager(AppDbContext context,IHttpContextAccessor accessor)
    {
        _context = context;
        _accessor = accessor;
    }

    public async Task<bool> LoginAsync(string Email, string Password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == Email);
        if (user is not null)
        {
            var passwordHash = Sha256Encryptor.Encrypt(Password);

            if (user.PasswordHash == passwordHash)
            {
                var userCreditional = new UserCreditional
                {
                    Email = user.Email,
                    IsAdmin = user.IsAdmin
                };
                var userCreditionalJson = JsonSerializer.Serialize(userCreditional);


                var hash = AesEncryptor.EncryptString("b14ca5898a4e4133bbce2ea2315a1916", userCreditionalJson);
                _accessor?.HttpContext?.Response.Cookies.Append("auth", hash);

                return true;
            }
            
        }
        return false;
    }

    public async Task<bool> RegisterAsync(string Email, string Password, bool IsAdmin)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == Email);

        if (user is null)
        {
            var newUser = new User
            {
                Email = Email,
                PasswordHash = Sha256Encryptor.Encrypt(Password),
                IsAdmin = IsAdmin
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return true;
        }
        return false;
    }
}


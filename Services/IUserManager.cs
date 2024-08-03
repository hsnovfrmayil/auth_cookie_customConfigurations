using System;
namespace _11_Lesson_Auth.Services;

public interface IUserManager
{
	Task<bool> LoginAsync(string Email, string Password);

    Task<bool> RegisterAsync(string Email, string Password,bool IsAdmin);

}


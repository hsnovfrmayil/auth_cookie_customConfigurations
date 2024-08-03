using System;
using System.Text.Json;
using _11_Lesson_Auth.Encryptors;
using _11_Lesson_Auth.Models;
using _11_Lesson_Auth.Services;

namespace _11_Lesson_Auth.Middlewares;

public class AuthMiddleware
{
	public readonly RequestDelegate _next;
	private IUserManager _userManager;

	public AuthMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		if (context.Request.Path.Value == "/Auth/Login" || context.Request.Path.Value == "/Auth/Register" || context.Request.Path.Value == "/Auth/Register")
		{
			await _next(context);
		}
		else
		{
            _userManager = context?.RequestServices?.GetService<IUserManager>()!;
            if (context.Request.Cookies["auth"] is not null)
            {
                var hash = context.Request.Cookies["auth"];
                var userCreditialJson = AesEncryptor.DecryptString("b14ca5898a4e4133bbce2ea2315a1916", hash);
                var userCredential = JsonSerializer.Deserialize<UserCreditional>(userCreditialJson);

                if (userCredential is not null)
                    await _next(context);

                else
                    context.Response.WriteAsync("A BALAM LOGIN OL DANA");
            }
			else
			{
                context.Response.Redirect("/Auth/Login");
            }
        }
	}
}


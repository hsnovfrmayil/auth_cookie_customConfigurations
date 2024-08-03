using System;
using System.Text.Json;
using _11_Lesson_Auth.Datas;
using _11_Lesson_Auth.Encryptors;
using _11_Lesson_Auth.Models;
using _11_Lesson_Auth.Models.Entities;
using _11_Lesson_Auth.Models.ViewModels;
using _11_Lesson_Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace _11_Lesson_Auth.Controllers;

public class AuthController :Controller
{
    public readonly AppDbContext _context;

    public readonly IUserManager _userManager;


    public AuthController(AppDbContext context,IUserManager userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
	public IActionResult Register()
	{
		return View();
	}

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM registerVM)
    {
        if (ModelState.IsValid)
        {
            if(await _userManager.RegisterAsync(registerVM.Email,registerVM.Password,registerVM.IsAdmin))
                return RedirectToAction("Login");

            ModelState.AddModelError("All","Email is already taken");
        }
        return View(registerVM);
    }


    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        
        if (ModelState.IsValid)
        {
            if (await _userManager.LoginAsync(loginVM.Email,loginVM.Password))
            {
                return RedirectToAction(actionName:"Index",controllerName:"Home");
            }

            else
                ModelState.AddModelError("All", "Invalid email or password");
        }


        return View(loginVM);
    }
}


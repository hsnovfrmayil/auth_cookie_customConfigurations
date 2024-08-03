﻿using System;
namespace _11_Lesson_Auth.Models.Entities;

public class User
{
	public int Id { get; set; }

	public string? Email { get; set; }

	public string? PasswordHash { get; set; }

	public bool IsAdmin { get; set; }
}


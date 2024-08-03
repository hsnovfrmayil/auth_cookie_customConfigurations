using System;
using System.ComponentModel.DataAnnotations;

namespace _11_Lesson_Auth.Models.ViewModels;

public class RegisterVM
{
    [Required]
    [MinLength(5)]
    public string Email { get; set; }

    [Required]
    [MinLength(5)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [MinLength(5)]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirimPassword { get; set; }

    public bool IsAdmin { get; set; }
}


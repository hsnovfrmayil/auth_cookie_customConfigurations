using System;
using _11_Lesson_Auth.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace _11_Lesson_Auth.Datas;

public class AppDbContext: DbContext
{
	public DbSet<User> Users { get; set; }
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{

	}

}


﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace _11_Lesson_Auth.Encryptors;

public static class Sha256Encryptor
{
	public static string Encrypt(string value)
	{
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

            // Convert byte array to a string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}


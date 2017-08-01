using System;
using System.Collections.Generic;
using System.Text;
using core.Domain;

namespace core.Models
{
    public class AuthModels
    {
        // registration feedback
        public class RegisterFeedback
        {
            public string Username { get; set; }
            public string Errors { get; set; }
            public bool Success { get; set; }
        }

        // login feedback
        public class LoginFeedback
        {
            public bool Success { get; set; }
            public string Errors { get; set; }
            public Guid AuthKey { get; set; }
            public long UserId { get; set; }
        }

        // login user
        public class Login
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        // register user
        public class Register
        {
            public string Username { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }
    }
}

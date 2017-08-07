using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using core.Models;
using core.Domain;

namespace core.Services
{
    public class UserAuthenticationService: IUserAuthenticationService
    {
        #region Authentication

        // function to register new user
        public async Task<AuthModels.RegisterFeedback> Register(User newuser)
        {
            // check if valid user
            if (string.IsNullOrEmpty(newuser.Username) || string.IsNullOrEmpty(newuser.Password))
            {
                return new AuthModels.RegisterFeedback
                {
                    Success = false,
                    Username = newuser.Username,
                    Errors = "Username and Password cannot be empty."
                };
            }

            using (var db = new AppDbContext())
            {
                // check if username already exists
                var prevUser = await db.Users.FirstOrDefaultAsync(x => string.Equals(x.Username, newuser.Username, StringComparison.OrdinalIgnoreCase));
                if (prevUser != null)
                {
                    return new AuthModels.RegisterFeedback
                    {
                        Success = false,
                        Username = newuser.Username,
                        Errors = "Username already exists. Please use different Username."
                    };
                }

                // if no errors add user
                newuser.Id = 0;
                newuser.CreateDateTime = DateTime.Now;
                await db.Users.AddAsync(newuser);
                await db.SaveChangesAsync();

                // return success
                return new AuthModels.RegisterFeedback
                {
                    Success = true,
                    Username = newuser.Username
                };
            }
        }

        // function to login user
        public async Task<AuthModels.LoginFeedback> Login(string username, string password)
        {
            // check if valid user details
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return new AuthModels.LoginFeedback
                {
                    Success = false,
                    Errors = "Username and Password cannot be empty."
                };
            }

            using (var db = new AppDbContext())
            {
                // check if user exists
                var user = await db.Users.FirstOrDefaultAsync(x => string.Equals(x.Username, username, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    // return error
                    return new AuthModels.LoginFeedback
                    {
                        Success = false,
                        Errors = $"Cannot find user {username}. Please register before logging in."
                    };
                }

                // check password
                if (user.Password != password)
                {
                    // return error
                    return new AuthModels.LoginFeedback
                    {
                        Success = false,
                        Errors = "Incorrect password."
                    };
                }

                // if here valid password. set as authenticated
                var auth = new Authentication
                {
                    UserId = user.Id,
                    AuthKey = Guid.NewGuid(),
                    CreateDateTime = DateTime.Now
                };

                await db.Auths.AddAsync(auth);
                await db.SaveChangesAsync();

                // return success
                return new AuthModels.LoginFeedback()
                {
                    Success = true,
                    AuthKey = auth.AuthKey,
                    UserId = user.Id
                };
            }
        }

        // logout current session using authkey
        public async Task Logout(Guid authkey)
        {
            using (var db = new AppDbContext())
            {
                var auth = await db.Auths.FirstOrDefaultAsync(x => x.AuthKey == authkey);
                // check if exists
                if (auth != null)
                {
                    // remove authentication
                    db.Remove(auth);
                    await db.SaveChangesAsync();
                }
            }
        }

        // logout all user sessions by id
        public void Logout(long userId)
        {
            using (var db = new AppDbContext())
            {
                var auths = db.Auths.Where(x => x.UserId == userId).ToList();
                foreach (var auth in auths)
                {
                    // remove authentication
                    db.Remove(auth);
                    db.SaveChanges();
                }
            }
        }

        // function to check if a user is authenticated
        public async Task<bool> IsAuthenticated(Guid authKey)
        {
            using (var db = new AppDbContext())
            {
                // return true if auth valid
                var auth = await db.Auths.FirstOrDefaultAsync(x => x.AuthKey == authKey);
                return auth != null;
            }
        }
        #endregion

        #region User

        // get single user
        public async Task<User> GetUser(long id)
        {
            using (var db = new AppDbContext())
            {
                return await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            }
        }
        
        #endregion
    }
}

using System;
using System.Threading.Tasks;
using core.Models;
using core.Domain;

namespace core
{
    public interface IUserAuthenticationService
    {
        // function to register new user
        Task<AuthModels.RegisterFeedback> Register(User newuser);

        // function to login user
        Task<AuthModels.LoginFeedback> Login(string username, string password);

        // logout current session using authkey
        Task Logout(Guid authkey);

        // logout all user sessions by user name
        Task Logout(string username);

        // logout all user sessions by id
        Task Logout(long userId);

        // function to check if a user is authenticated
        Task<bool> IsAuthenticated(Guid authKey);

        // get single user
        Task<User> GetUser(long id);
    }
}

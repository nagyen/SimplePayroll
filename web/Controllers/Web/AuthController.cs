using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core;
using core.Models;
using Microsoft.AspNetCore.Mvc;

namespace web.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(IUserAuthenticationService auth) : base(auth)
        {
        }

		[HttpPost]
        public async Task<IActionResult> Login([FromBody]AuthModels.Login login)
        {
            // login 
            var res = await Authservice.Login(login.Username, login.Password);
            if (res.Success)
            {
                // set auth key for session
                SetAuth(res.UserId, res.AuthKey);

                // return to listing screen on login
                return SuccessResponse("/listing");
            }

            // return error
            return ErrorResponse(res.Errors);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]AuthModels.Register register)
        {
            // check passwords
            if (register.Password != register.ConfirmPassword)
            {
                return ErrorResponse("Passwords not matched.");
            }

            // define user
            var newuser = new core.Domain.User
            {
                Username = register.Username,
                Password = register.Password,
                FirstName = register.FirstName,
                LastName = register.LastName
            };

            // register user 
            var res = await Authservice.Register(newuser);
            if (res.Success)
            {
                return SuccessResponse("/");
            }
            // return error
            return ErrorResponse(res.Errors);
        }

        // logout
        public async Task<IActionResult> Logout()
        {
            if (await CheckAccess())
            {
                await Authservice.Logout(GetCurrentUser());
            }
            return Redirect("/");
        }
    }
}

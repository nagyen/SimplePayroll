using System;
using core;
using core.Services;

namespace tests
{
	public class AuthenticationTests
	{
        private UserAuthenticationService AuthenticationService { get; set; }
		public AuthenticationTests()
		{
            AuthenticationService = new UserAuthenticationService();
		}

		// run all tests
		public static void Run()
		{
			var test = new AuthenticationTests();

			// register
			test.RegisterWithValidUser();
			test.RegisterWithDuplicateUser();
			test.RegisterWithEmptyUser();
			test.RegisterWithEmptyPassword();

			// login
			test.SuccessLogin();
			test.WrongUsernameLogin();
			test.WrongPasswordLogin();

			//logout
			test.Logout();
		}

		// register with valid user
		public void RegisterWithValidUser()
		{
			Console.WriteLine("Register: Should log new user id.");
			var newUser = new core.Domain.User
			{
				Username = "testUser",
				Password = "testPass"
			};
            var res = AsyncHelpers.RunSync(() => AuthenticationService.Register(newUser));
			Console.WriteLine($" Success: {res.Success} \n Errors: {res.Errors} \n New user id: {newUser.Id}");
			Console.WriteLine();
		}

		// register with in-valid user
		public void RegisterWithDuplicateUser()
		{
			Console.WriteLine("Register: Should show user exits");
			var newUser = new core.Domain.User
			{
				Username = "testUser",
				Password = "testPass"
			};
            var res = AsyncHelpers.RunSync(() => AuthenticationService.Register(newUser));
			Console.WriteLine($" Success: {res.Success} \n Errors: {res.Errors} \n New user id: {newUser.Id}");
			Console.WriteLine();
		}

		// register with empty user name
		public void RegisterWithEmptyUser()
		{
			Console.WriteLine("Register: Empty user. Should show error");
			var newUser = new core.Domain.User
			{
				Username = "",
				Password = "testPass"
			};
            var res = AsyncHelpers.RunSync(() => AuthenticationService.Register(newUser));
			Console.WriteLine($" Success: {res.Success} \n Errors: {res.Errors} \n New user id: {newUser.Id}");
			Console.WriteLine();
		}

		// register with empty password
		public void RegisterWithEmptyPassword()
		{
			Console.WriteLine("Register: Empty password. Should show error");
			var newUser = new core.Domain.User
			{
				Username = "user2",
				Password = ""
			};
            var res = AsyncHelpers.RunSync(() => AuthenticationService.Register(newUser));
			Console.WriteLine($" Success: {res.Success} \n Errors: {res.Errors} \n New user id: {newUser.Id}");
			Console.WriteLine();
		}

		// success login
		public void SuccessLogin()
		{
			Console.WriteLine("Login: should show Auth key.");

            var res = AsyncHelpers.RunSync(() => AuthenticationService.Login("testUser", "testPass"));
			Console.WriteLine($" Success: {res.Success} \n Errors: {res.Errors} \n Auth: {res.AuthKey}");
			Console.WriteLine();
		}

		// wrong password login
		public void WrongPasswordLogin()
		{
			Console.WriteLine("Login: Incorrect password. should show error.");

            var res = AsyncHelpers.RunSync(() => AuthenticationService.Login("testUser", "testPass2"));
			Console.WriteLine($" Success: {res.Success} \n Errors: {res.Errors} \n Auth: {res.AuthKey}");
			Console.WriteLine();
		}

		// wrong username login
		public void WrongUsernameLogin()
		{
			Console.WriteLine("Login: Incorrect username. should show error.");

            var res = AsyncHelpers.RunSync(() => AuthenticationService.Login("testUser2", "testPass"));
			Console.WriteLine($" Success: {res.Success} \n Errors: {res.Errors} \n Auth: {res.AuthKey}");
			Console.WriteLine();
		}

		// log out
		public void Logout()
		{
			Console.WriteLine("Logout:");
            AsyncHelpers.RunSync(() => AuthenticationService.Logout("testUser"));
			Console.WriteLine($" Logged out.");
			Console.WriteLine();
		}
	}
}

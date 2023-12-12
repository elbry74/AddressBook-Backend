using System;
using AddressBook.Application.Login;
using AddressBook.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Address_Book_Project.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IManageRegister _registerManager;
        private readonly IManageLogin _loginManager;

        public AccountController(IManageRegister registerManager, IManageLogin loginManager)
        {
            _registerManager = registerManager ?? throw new ArgumentNullException(nameof(registerManager));
            _loginManager = loginManager ?? throw new ArgumentNullException(nameof(loginManager));
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = _registerManager.Register(model);

            if (success)
            {
                return Ok("Registration successful");
            }

            return BadRequest("Registration failed. User with the same email already exists.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = _loginManager.Login(model);

            if (success)
            {
                return Ok("Login successful");
            }

            return BadRequest("Login failed. Invalid email or password.");
        }
    }
}
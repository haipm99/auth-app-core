using AuthApp.Form;
using AuthApp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : ControllerBase
    {

        private IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult login(LoginForm form)
        {
            string token = _service.authenticate(form);

            return Ok(token);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult get()
        {
            var users = _service.get();
            return Ok(users);
        }
    }
}

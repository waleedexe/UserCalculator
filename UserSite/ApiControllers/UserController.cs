using Interfaces;
using RemoteEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UserSite.ApiControllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IHttpActionResult GetUser(int id)
        {
            var registeredUser = _userService.GetUser(id);
            return Ok(registeredUser);
        }

        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }

        [HttpPost]
        public IHttpActionResult PostUser(User user)
        {
            var registeredUser = _userService.Register(user);
            return Ok(registeredUser);
        }
    }
}

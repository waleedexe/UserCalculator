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
    public class UserOperationController : ApiController
    {
        private readonly IUserService _userService;

        public UserOperationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IHttpActionResult PostOperation(UserOperation operation)
        {
            var userOpetation = _userService.AddOperation(operation);
            return Ok(userOpetation);
        }

        [HttpGet]
        public IHttpActionResult GetOperations(int id)
        {
            var operations = _userService.GetOperations(id);
            return Ok(operations);
        }
    }
}

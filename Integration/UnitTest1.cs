using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities;
using System.Linq;

namespace Integration
{
    [TestClass]
    public class UnitTest1
    {
        Services.UserService _service;

        [TestInitialize]
        public void Init()
        {
            _service = new Services.UserService(
                new SqlRepository.EntityRepository<User>(), new SqlRepository.EntityRepository<UserOperation>(),
                new Services.DataValidationService(), new Services.CalculatorService());
        }

        [TestMethod]
        public void AddUser()
        {
            var x = _service.Register(new RemoteEntities.User { LoginId="u1", UserName = "waleed" });

            Assert.AreNotEqual("", x.Id);
        }

        [TestMethod]
        public void AddOperation()
        {
            var user = _service.GetUser(1);
            var operation = new RemoteEntities.UserOperation
            {
                Operation = "1",
                FirstNumber = "1",
                SecondNumber = "2",
                UserId = user.Id,
            };

            var x = _service.AddOperation(operation);

            Assert.AreNotEqual("", x.Id);
        }

        [TestMethod]
        public void GetOperation()
        {
            var op = _service.GetOperations(2).ToList();

            Assert.IsNotNull(op);
        }
    }
}

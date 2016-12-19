using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using Moq;
using System.Linq.Expressions;
using System.Collections.Generic;
using FluentAssertions;

namespace Services.Tests
{
    [TestClass]
    public class UserTests
    {
        Services.UserService _service;
        Mock<IEntityRepository<Entities.User>> _userRepoMock;
        Mock<IEntityRepository<Entities.UserOperation>> _userOpRepoMock;
        Mock<ICalculator> _calcMock;
        Mock<IDataValidator> _validationMock;

        [TestInitialize]
        public void Init()
        {
            _userRepoMock = new Mock<IEntityRepository<Entities.User>>();
            _userOpRepoMock = new Mock<IEntityRepository<Entities.UserOperation>>();
            _calcMock = new Mock<ICalculator>();
            _validationMock = new Mock<IDataValidator>();

            _service = new UserService(_userRepoMock.Object, _userOpRepoMock.Object,_validationMock.Object, _calcMock.Object);
        }

        [TestMethod]
        public void RegisterNewUser_Should_SaveToDB()
        {
            //Arrange
            var sampleUser = new Entities.User();
            _userRepoMock.Setup(m => m.GetBy(It.IsAny<Expression<Func<Entities.User, bool>>>()))
                .Returns(new List<Entities.User>()).Verifiable();
            _userRepoMock.Setup(m => m.Add(It.IsAny<Entities.User>()))
                .Returns(sampleUser).Verifiable();

            //Act
            var newUserResult = _service.Register(new RemoteEntities.User());

            //Assert
            newUserResult.Should().NotBeNull();
            newUserResult.Should().BeOfType<RemoteEntities.User>();
            _userRepoMock.VerifyAll();
        }

        [TestMethod]
        public void RegisterExistingUser_Should_NotSave()
        {
            //Arrange
            var sampleUser = new Entities.User();
            _userRepoMock.Setup(m => m.GetBy(It.IsAny<Expression<Func<Entities.User, bool>>>()))
                .Returns(new[] { sampleUser }).Verifiable();

            //Act
            var newUserResult = _service.Register(new RemoteEntities.User());


            //Assert
            newUserResult.Should().NotBeNull();
            newUserResult.Messages.Should().HaveCount(1);

            _userRepoMock.Verify(m => m.Add(It.IsAny<Entities.User>()), Times.Never());
            _userRepoMock.VerifyAll();
        }

        [TestMethod]
        public void GetMissingUser_Should_NotFail()
        {
            _userRepoMock.Setup(m => m.GetById(It.IsAny<int>()))
                .Returns((Entities.User)null).Verifiable();

            //Act
            var newUserResult = _service.GetUser(0);

            //Assert
            newUserResult.Should().BeNull();
            _userRepoMock.VerifyAll();
        }

        [TestMethod]
        public void AddInvalidOperationInput_Should_NotSave()
        {
            _validationMock.Setup(m => m.Validate(It.IsAny<RemoteEntities.UserOperation>()))
                .Returns(false);

            //Act
            var newOp = _service.AddOperation(new RemoteEntities.UserOperation());

            //Assert
            newOp.Should().NotBeNull();
            newOp.Messages.Should().HaveCount(1);
            _userOpRepoMock.Verify(m => m.Add(It.IsAny<Entities.UserOperation>()), Times.Never);
        }

        [TestMethod]
        public void AddOperation_Should_Save()
        {
            var sampleRemoteOp = new RemoteEntities.UserOperation()
            {
                FirstNumber = "1", SecondNumber = "2", Operation = "1",
            };
            var sampleOp = new Entities.UserOperation()
            {
                FirstNumber = "1", SecondNumber = "2", Operation = "1",
            };
            _validationMock.Setup(m => m.Validate(It.IsAny<RemoteEntities.UserOperation>()))
                .Returns(true);
            _userOpRepoMock.Setup(m => m.Add(It.IsAny<Entities.UserOperation>()))
                .Returns(sampleOp).Verifiable();

            //Act
            var newOp = _service.AddOperation(sampleRemoteOp);

            //Assert
            newOp.Should().NotBeNull();
            newOp.Should().BeOfType<RemoteEntities.UserOperation>();
            _calcMock.Verify(m => m.Calculate<long>(It.IsAny<RemoteEntities.UserOperation>()), Times.Once);
            _userOpRepoMock.VerifyAll();
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace Services.Tests
{
    [TestClass]
    public class ValidatorTests
    {
        DataValidationService _service;
        RemoteEntities.UserOperation _inputSample;

        [TestInitialize]
        public void Init()
        {
            _service = new DataValidationService();
            _inputSample = new RemoteEntities.UserOperation()
            {
                FirstNumber = "1",
                SecondNumber = "1",
                Operation = "1",
            };
        }

        [TestMethod]
        public void InvalidInput_Should_Fail()
        {
            _inputSample.FirstNumber = "";

            //Act
            var result = _service.Validate(_inputSample);

            //Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void InvalidOperation_Should_Fail()
        {
            _inputSample.Operation = "5";

            //Act
            var result = _service.Validate(_inputSample);

            //Assert
            result.Should().BeFalse();
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace Services.Tests
{
    [TestClass]
    public class CalculatorServiceTests
    {
        CalculatorService _service;
        RemoteEntities.UserOperation _inputSample;

        [TestInitialize]
        public void Init()
        {
            _service = new CalculatorService();
            _inputSample = new RemoteEntities.UserOperation()
            {
                FirstNumber = "1",
                SecondNumber = "1",
                Operation = "1",
            };
        }

        [TestMethod]
        public void AddInt()
        {
            //Act
            _service.Calculate<int>(_inputSample);

            Assert.AreEqual("2", _inputSample.Result);
        }

        [TestMethod]
        public void AddIntNegative()
        {
            _inputSample.SecondNumber = "-1000";

            //Act
            _service.Calculate<int>(_inputSample);

            Assert.AreEqual("-999", _inputSample.Result);
        }

        [TestMethod]
        public void AddIntTwoNegatives()
        {
            _inputSample.FirstNumber = "-1";
            _inputSample.SecondNumber = "-1000";

            //Act
            _service.Calculate<int>(_inputSample);

            Assert.AreEqual("-1001", _inputSample.Result);
        }

        [TestMethod]
        public void DivideByZero()
        {
            _inputSample.SecondNumber = "0";
            _inputSample.Operation = "3";

            //Act
            _service.Calculate<int>(_inputSample);

            Assert.AreEqual("NaN", _inputSample.Result);
        }

        [TestMethod]
        public void AddDecimal()
        {
            _inputSample.SecondNumber = "1.1";

            //Act
            _service.Calculate<decimal>(_inputSample);

            Assert.AreEqual("2.1", _inputSample.Result);
        }
    }
}

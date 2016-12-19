
using DataServices.Extensions;
using Interfaces;
using RemoteEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IEntityRepository<Entities.User> _userRepo;
        private readonly IEntityRepository<Entities.UserOperation> _operationRepo;
        private readonly IDataValidator _dataValidator;
        private ICalculator _calculator;

        public UserService(IEntityRepository<Entities.User> userRepo, IEntityRepository<Entities.UserOperation> operationRepo, 
            IDataValidator dataValidator, ICalculator calculator)
        {
            _userRepo = userRepo;
            _operationRepo = operationRepo;
            _dataValidator = dataValidator;
            _calculator = calculator;
        }

        public User Register(User user)
        {
            // Check user exists.
            var existingUser = _userRepo.GetBy(u => u.LoginId == user.LoginId);
            if (existingUser.Any())
            {
                user.Messages.Add(new Message { Details = "This is an existing, try another login id.." });
                return user;
            }

            var newUser = _userRepo.Add(user.ToEntity());
            return newUser.ToRemote();
        }

        public User GetUser(int id)
        {
            return _userRepo.GetById(id).ToRemote();
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepo.GetAll()
                .Select(o => o.ToRemote());
        }

        public UserOperation AddOperation(UserOperation operation)
        {
            if (!_dataValidator.Validate(operation))
            {
                operation.Messages.Add(new Message { Details = "The operation has invalid data" });
                return operation;
            }

            if (new[] { operation.FirstNumber, operation.SecondNumber }.Any(n => n.IndexOf(".") >= 0))
            {
                _calculator.Calculate<decimal>(operation);
            }
            else
            {
                _calculator.Calculate<long>(operation);
            }

            var newOperation = _operationRepo.Add(operation.ToEntity());
            return newOperation.ToRemote();
        }

        public IEnumerable<UserOperation> GetOperations(int userId)
        {
            return _operationRepo.GetBy(x => x.UserId == userId)
                .Select(o => o.ToRemote());
        }
    }
}

using RemoteEntities;
using System.Collections.Generic;


namespace Interfaces
{
    public interface IUserService
    {
        UserOperation AddOperation(UserOperation operation);
        IEnumerable<UserOperation> GetOperations(int userId);
        IEnumerable<User> GetUsers();
        User GetUser(int id);
        User Register(User user);
    }
}
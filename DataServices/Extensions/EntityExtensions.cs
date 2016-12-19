using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Extensions
{
    public static class EntityExtensions
    {
        public static Entities.User ToEntity(this RemoteEntities.User source)
        {
            if (source == null)
                return null;

            return new Entities.User
            {
                Id= source.Id,
                LoginId= source.LoginId,
                UserName= source.UserName,
            };
            
        }
        public static Entities.UserOperation ToEntity(this RemoteEntities.UserOperation source)
        {
            if (source == null)
                return null;

            return new Entities.UserOperation
            {
                Id = source.Id,
                FirstNumber = source.FirstNumber,
                Operation = source.Operation,
                Result = source.Result,
                SecondNumber = source.SecondNumber,
                UserId = source.UserId,
            };
        }

        public static RemoteEntities.User ToRemote(this Entities.User source)
        {
            if (source == null)
                return null;

            return new RemoteEntities.User
            {
                Id = source.Id,
                LoginId = source.LoginId,
                UserName = source.UserName,
            };
        }
        public static RemoteEntities.UserOperation ToRemote(this Entities.UserOperation source)
        {
            if (source == null)
                return null;

            return new RemoteEntities.UserOperation
            {
                Id = source.Id,
                FirstNumber = source.FirstNumber,
                Operation = source.Operation,
                Result = source.Result,
                SecondNumber = source.SecondNumber,
                UserId = source.UserId,
            };
        }
    }
}

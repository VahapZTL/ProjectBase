using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class UserTypeBusiness : IUserTypeBusiness
    {
        private IUserTypeRepository userType;

        public UserTypeBusiness(IUserTypeRepository userType)
        {
            this.userType = userType;
        }
    }
}

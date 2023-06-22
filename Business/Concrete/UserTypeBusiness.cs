using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class UserTypeBusiness : IUserTypeBusiness
    {
        private IUserTypeDal userType;

        public UserTypeBusiness(IUserTypeDal userType)
        {
            this.userType = userType;
        }

        public IResult Add(UserType entity)
        {
            return userType.Add(entity);
        }

        public IResult Delete(UserType entity)
        {
            return userType.Delete(entity);
        }

        public IDataResult<UserType> GetById(long id)
        {
            return userType.Get(x => x.Id == id);
        }

        public IDataResult<List<UserType>> GetList()
        {
            return userType.GetList();
        }

        public IDataResult<List<UserType>> GetList(Expression<Func<UserType, bool>> filter)
        {
            return userType.GetList(filter);
        }

        public IResult Update(UserType entity)
        {
            return userType.Update(entity);
        }
    }
}

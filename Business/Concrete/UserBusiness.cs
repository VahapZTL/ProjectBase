using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Entities.Dtos.Models;
using Core.Entities.Dtos.Request;
using Core.Entities.Dtos.Response;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class UserBusiness : IUserBusiness
    {
        IUserDal userDal;

        public UserBusiness(IUserDal userDal)
        {
            this.userDal = userDal;
        }

        public IDataResult<List<ClaimModel>> GetClaims(long UserId)
        {
            return new SuccessDataResult<List<ClaimModel>>(userDal.GetClaims(UserId), Messages.ClaimsReturned);
        }

        public IResult Add(User user)
        {
            var data = userDal.Add(user);
            if (data.Success)
                return new SuccessResult(Messages.UserRegistered);
            else
                return new ErrorResult(data.Message);
        }

        public IDataResult<User> GetByMail(string email)
        {
            var data = userDal.Get(u => u.Email == email);

            if (data.Success)
                return new DataResult<User>(data.Data, data.Success, Messages.UserReturned);
            else
                return new ErrorDataResult<User>(data.Message);
        }

        public IDataResult<List<ResponseGetUserRole>> GetUserRoleList(RequestGetUserRole request)
        {
            List<ResponseGetUserRole> roles = new List<ResponseGetUserRole>();

            //kullanıcının yetkileri alınır listeye atılır

            return new SuccessDataResult<List<ResponseGetUserRole>>(roles, Messages.RoleListReturned);
        }

        public IDataResult<User> GetById(long id)
        {
            return userDal.Get(x => x.Id == id);
        }

        public IDataResult<List<User>> GetList()
        {
            return userDal.GetList();
        }

        public IDataResult<List<User>> GetList(Expression<Func<User, bool>> filter)
        {
            return userDal.GetList(filter);
        }

        public IResult Delete(User entity)
        {
            return userDal.Delete(entity);
        }

        public IResult Update(User entity)
        {
            return userDal.Update(entity);
        }
    }
}

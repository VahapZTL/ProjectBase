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
        IUserRepository userRepository;

        public UserBusiness(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IDataResult<List<ClaimModel>> GetClaims(long UserId)
        {
            return new SuccessDataResult<List<ClaimModel>>(userRepository.GetClaims(UserId), Messages.ClaimsReturned);
        }

        public IDataResult<User> GetByMail(string email)
        {
            var data = userRepository.Get(u => u.Email == email);

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
    }
}

using Core.Entities;
using Core.Entities.Concrete;
using Core.Entities.Dtos.Models;
using Core.Entities.Dtos.Request;
using Core.Entities.Dtos.Response;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IUserBusiness
    {
        IDataResult<List<ClaimModel>> GetClaims(long UserId);
        IDataResult<User> GetByMail(string email);
        IDataResult<List<ResponseGetUserRole>> GetUserRoleList(RequestGetUserRole request);
    }
}

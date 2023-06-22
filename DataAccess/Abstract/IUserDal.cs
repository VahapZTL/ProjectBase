using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Dtos.Models;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<ClaimModel> GetClaims(long UserId);
    }
}

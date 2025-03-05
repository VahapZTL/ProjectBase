using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Dtos.Models;

namespace DataAccess.Abstract
{
    public interface IUserRepository : IEntityRepository<User>
    {
        List<ClaimModel> GetClaims(long UserId);
    }
}

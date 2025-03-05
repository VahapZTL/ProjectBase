using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Dtos.Models;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class UserRepository : EntityRepositoryBase<User, ProjectBaseContext>, IUserRepository
    {
        public List<ClaimModel> GetClaims(long UserId)
        {
            using (var context = new ProjectBaseContext())
            {
                var result = from user in context.Users
                              join userType in context.UserType
                              on user.UserTypeId equals userType.Id
                              where user.StatusId == 1 && userType.StatusId == 1
                              select new ClaimModel { Id = userType.Id, Name = userType.Name };
                //var result = from operationClaim in context.OperationClaims
                //    join userOperationClaim in context.UserOperationClaims
                //        on operationClaim.Id equals userOperationClaim.OperationClaimId
                //    where userOperationClaim.UserId == UserId
                //    select new OperationClaim {Id = operationClaim.Id, Name = operationClaim.Name};
                return result.ToList();

            }
        }
    }
}

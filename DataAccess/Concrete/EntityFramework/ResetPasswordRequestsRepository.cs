using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class ResetPasswordRequestsRepository : EntityRepositoryBase<ResetPasswordRequests, ProjectBaseContext>, IResetPasswordRequestsRepository
    {
    }
}

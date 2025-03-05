using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class ResetPasswordRequestBusiness : IResetPasswordRequestBusiness
    {
        private IResetPasswordRequestsRepository resetPasswordRequestsRepository;

        public ResetPasswordRequestBusiness(IResetPasswordRequestsRepository resetPasswordRequestsRepository)
        {
            this.resetPasswordRequestsRepository = resetPasswordRequestsRepository;
        }
    }
}

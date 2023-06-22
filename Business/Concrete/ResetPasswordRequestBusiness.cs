using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class ResetPasswordRequestBusiness : IResetPasswordRequestBusiness
    {
        private IResetPasswordRequestsDal resetPasswordRequestsDal;

        public ResetPasswordRequestBusiness(IResetPasswordRequestsDal resetPasswordRequestsDal)
        {
            this.resetPasswordRequestsDal = resetPasswordRequestsDal;
        }

        public IResult Add(ResetPasswordRequests entity)
        {
            return resetPasswordRequestsDal.Add(entity);   
        }

        public IResult Delete(ResetPasswordRequests entity)
        {
            return resetPasswordRequestsDal.Delete(entity);
        }

        public IDataResult<ResetPasswordRequests> GetById(long id)
        {
            return resetPasswordRequestsDal.Get(x => x.Id == id);
        }

        public IDataResult<List<ResetPasswordRequests>> GetList()
        {
            return resetPasswordRequestsDal.GetList();
        }

        public IDataResult<List<ResetPasswordRequests>> GetList(Expression<Func<ResetPasswordRequests, bool>> filter)
        {
            return resetPasswordRequestsDal.GetList(filter);
        }

        public IResult Update(ResetPasswordRequests entity)
        {
            return resetPasswordRequestsDal.Update(entity);
        }
    }
}

using Core.Entities.Dtos.Models;
using Core.Entities.Dtos.Request;
using Core.Entities.Dtos.Response;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;

namespace Business.Abstract
{
    public interface IAuthBusiness
    {
        IDataResult<ResponseRegisterUser> Register(RequestRegisterUser userForRegisterDto);
        IDataResult<ResponseLoginUser> Login(RequestLoginUser userForLoginDto);
        IResult UserExists(string email);
        AccessToken CreateAccessToken(long UserId, string Email, string FirstName, string LastName);
        List<AuthorizationModel> CreateAuthorizationModel(long UserTypeId, long LangId);
        IResult ValidateToken(string token);
    }
}

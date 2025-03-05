using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Entities.Dtos.Models;
using Core.Entities.Dtos.Request;
using Core.Entities.Dtos.Response;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace Business.Concrete
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IUserBusiness userService;
        private readonly ITokenHelper tokenHelper;
        private readonly IRolePermissionsMatchBusiness rolePermissionsMatchBusiness;

        public AuthBusiness(
            IUserBusiness userService,
            ITokenHelper tokenHelper,
            IRolePermissionsMatchBusiness rolePermissionsMatchBusiness)
        {
            this.userService = userService;
            this.tokenHelper = tokenHelper;
            this.rolePermissionsMatchBusiness = rolePermissionsMatchBusiness;
        }

        public IDataResult<ResponseRegisterUser> Register(RequestRegisterUser userForRegisterDto)
        {
            ResponseRegisterUser response = new ResponseRegisterUser();
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                UserTypeId = userForRegisterDto.UserTypeId,
                Address = userForRegisterDto.Address,
                PhoneNumber = userForRegisterDto.PhoneNumber,
                ImagePath = "",
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                StatusId = 1,
                CreatedDate = DateTime.Now,
                CreatedUserId = -1,
                ModifiedDate = DateTime.Now,
                ModifiedUserId = -1,
            };

            //var addUserResult = userService.Add(user);

            if (true)
                return new SuccessDataResult<ResponseRegisterUser>(response, Messages.UserRegistered);
            else
                return new ErrorDataResult<ResponseRegisterUser>("");
        }

        public IDataResult<ResponseLoginUser> Login(RequestLoginUser userForLoginDto)
        {
            var userToCheck = userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<ResponseLoginUser>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<ResponseLoginUser>(Messages.PasswordError);
            }

            ResponseLoginUser response = new ResponseLoginUser()
            {
                Id = userToCheck.Data.Id,
                Email = userToCheck.Data.Email,
                FirstName = userToCheck.Data.FirstName,
                LastName = userToCheck.Data.LastName,
                UserTypeId = userToCheck.Data.UserTypeId,
                AccessToken = CreateAccessToken(userToCheck.Data.Id, userToCheck.Data.Email, userToCheck.Data.FirstName, userToCheck.Data.LastName)
            };

            return new SuccessDataResult<ResponseLoginUser>(response, Messages.SuccessfulLogin);
        }

        public List<AuthorizationModel> CreateAuthorizationModel(long UserTypeId, long LangId = 1)
        {
            return rolePermissionsMatchBusiness.GetMenuList(UserTypeId, LangId).Data;
        }

        public IResult UserExists(string email)
        {
            var user = userService.GetByMail(email);
            if (user.Data != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public AccessToken CreateAccessToken(long UserId, string Email, string FirstName, string LastName)
        {
            var claims = userService.GetClaims(UserId);
            var accessToken = tokenHelper.CreateToken(UserId, Email, FirstName, LastName, claims.Data);
            return accessToken;
        }

        public IResult ValidateToken(string token)
        {
            if (tokenHelper.ValidateToken(token))
                return new SuccessResult();
            else
                return new ErrorResult();
        }

        public RefreshToken CreateRefreshToken(long userId, string ipAddress)
        {
            RefreshToken refreshToken = tokenHelper.CreateRefreshToken(userId, ipAddress);
            return refreshToken;
        }
    }
}

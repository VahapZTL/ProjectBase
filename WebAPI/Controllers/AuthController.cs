using Business.Abstract;
using Core.Entities.Dtos.Request;
using Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthBusiness authBusiness;
        private readonly IRolePermissionsBusiness rolePermissionsBusiness;

        public AuthController(IAuthBusiness authBusiness, IRolePermissionsBusiness rolePermissionsBusiness)
        {
            this.authBusiness = authBusiness;
            this.rolePermissionsBusiness = rolePermissionsBusiness;
        }

        [HttpPost("Login")]
        public ActionResult Login(RequestLoginUser userForLoginDto)
        {
            var userToLogin = authBusiness.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return Unauthorized(userToLogin.Message);
            }

            var loginData = userToLogin.Data;

            var authModel = authBusiness.CreateAuthorizationModel(loginData.UserTypeId, (long)EnumLanguage.Turkish);

            var result = authBusiness.CreateAccessToken(loginData.Id, loginData.Email, loginData.FirstName, loginData.LastName);

            loginData.AccessToken = result;
            loginData.Authorization = authModel;

            return Ok(loginData);
        }

        [HttpPost("Register")]
        public ActionResult Register(RequestRegisterUser userForRegisterDto)
        {
            var userExists = authBusiness.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
                return BadRequest(userExists.Message);

            var registerResult = authBusiness.Register(userForRegisterDto);

            if (registerResult.Success)
            {
                return Ok(registerResult);
            }
            else
                return BadRequest(registerResult.Message);
        }

        [HttpPost("ValidateToken")]
        public ActionResult ValidateToken(RequestValidateToken request)
        {
            var result = authBusiness.ValidateToken(request.Token);
            if (result.Success)
                return Ok(result);
            else 
                return BadRequest(result.Message);
        }

        [HttpGet("GetRoles")]
        public IActionResult GetRoles(long roleType)
        {
            var result = rolePermissionsBusiness.GetAllRoles(roleType);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result.Message);
        }

        [HttpPost("SetRoles")]
        public IActionResult SetRoles(RequestSetRoles request)
        {
            var result = rolePermissionsBusiness.AddRoleData(request);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result.Message);
        }
    }
}

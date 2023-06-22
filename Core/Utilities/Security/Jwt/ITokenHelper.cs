using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Core.Entities.Dtos.Models;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        RefreshToken CreateRefreshToken(long userId, string ipAddress);
        AccessToken CreateToken(long UserId, string Email, string FirstName, string LastName, List<ClaimModel> operationClaims);
        bool ValidateToken(string token);
    }
}

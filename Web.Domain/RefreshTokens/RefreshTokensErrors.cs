using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.RefreshTokens
{
    public class RefreshTokensErrors
    {

        public static readonly Error RefreshTokenIsnulll = Error.Validation(
                     code: "RefreshToken  is Null",
                     description: "RefreshToken is Null!");



        public static readonly Error DuplicatedRefreshToken = Error.Conflict(
            code: "RefreshToken is Duplicated",
            description: "RefreshToken is already Exist!");
    }
}

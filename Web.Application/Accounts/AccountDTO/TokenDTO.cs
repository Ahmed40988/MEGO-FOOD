using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Web.Application.Accounts.AccountDTO
{
    public class TokenDTO
    {
        public string UserId { get; set; }=string.Empty;
        public string Token { get; set; } = string.Empty;
        public int expiresIn { get; set; }

        public string RefreshToken {  get; set; }=string.Empty;
        public DateTime RefreshTokenExpiration {  get; set; }

    }
}

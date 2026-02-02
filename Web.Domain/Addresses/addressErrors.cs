using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.Addresses
{
    public class addressErrors
    {

        public static readonly Error addressIsnulll = Error.Validation(
                     code: "address  is Null",
                     description: "address is Null!");



        public static readonly Error Duplicatedaddress = Error.Conflict(
            code: "address is Duplicated",
            description: "address is already Exist!");
    }

}


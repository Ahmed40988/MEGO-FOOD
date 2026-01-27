using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Common.Interfaces
{
    public interface IFuzzySearchRepository
    {
        int CalculateSimilarity(string source, string target);
    }

}

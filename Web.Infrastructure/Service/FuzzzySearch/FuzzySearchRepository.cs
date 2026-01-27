using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzySharp;
using Web.Application.Common.Interfaces;

namespace Web.Infrastructure.Service.FuzzzySearch
{
    public class FuzzySearchRepository : IFuzzySearchRepository
    {
        public int CalculateSimilarity(string source, string target)
        {
            return Fuzz.Ratio(source, target);
        }
    }

}

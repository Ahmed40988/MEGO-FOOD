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

namespace Web.Application.Common.File
{
        public interface IBaseUrlService
        {
            string GetBaseUrl();
            string ToAbsoluteMediaUrl(string? path);

        }
}


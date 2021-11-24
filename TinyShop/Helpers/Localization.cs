using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace TinyShop.Helpers
{
    public static class Localization
    {
        public static RequestLocalizationOptions GetLocalizationOptions(IConfiguration config)
        {
            Dictionary<string, string> cultures = config
                .GetSection("Cultures")
                .GetChildren()
                .ToDictionary(x => x.Key, x => x.Value);

            var supportedCultures = cultures.Keys.ToArray();
            var localizationOptions = new RequestLocalizationOptions()
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            return localizationOptions;
        }
    }
}

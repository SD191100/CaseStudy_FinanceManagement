using Microsoft.Extensions.Configuration;

namespace Utility_Library
{
    public static class DBPropertyUtil
    {
        private static IConfigurationRoot _configuration;
        static string s = null;

        static DBPropertyUtil()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("C:\\Users\\shiva\\source\\repos\\FinanceManagementSystem\\Utility_Library\\appSettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
        }
        public static string GetConnectionString()
        {

            //return "Data Source=FSociety;Initial Catalog=FinanceMgt;Integrated Security=True;Trust Server Certificate=True";
            s = _configuration.GetConnectionString("dbCn");
            return s;
        }
    }
}

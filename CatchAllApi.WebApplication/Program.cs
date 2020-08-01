using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CatchAllApi.WebApplication
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					var path = Path.Combine(Path.DirectorySeparatorChar.ToString(), "run", "secrets", "kestrel_certificates_default_password");
					var kestrelCertificatesDefaultPassword = File.ReadAllText(path).Trim();

					webBuilder
						.UseSetting("Kestrel:Certificates:Default:Password", kestrelCertificatesDefaultPassword)
						.UseStartup<Startup>();
				});
	}
}

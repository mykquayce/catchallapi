using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CatchAllApi.WebApplication
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			var logger = app.ApplicationServices.GetService<ILogger<Startup>>();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapFallback(async context =>
				{
					await logger.LogRequestAsync(context.Request, context.Connection);
					await context.Response.WriteAsync("Hello World");
				});
			});
		}
	}

	public static class LoggerExtensions
	{
		public async static Task<ILogger> LogRequestAsync(this ILogger logger, HttpRequest request, ConnectionInfo connection)
		{
			var sb = new StringBuilder();

			sb
				.AppendLine(DateTime.UtcNow.ToString("O"))
				.AppendLine("RemoteIpAddress: " + connection.RemoteIpAddress.ToString())
				.AppendLine("Method: " + request.Method)
				.AppendLine("URL: " + request.GetDisplayUrl());

			foreach (var (key, values) in request.Headers)
			{
				var valuesString = string.Join(',', values);

				sb.AppendLine($"Header: {key} = {valuesString}");
			}

			using var stream = request.Body;
			using var reader = new StreamReader(stream);
			var body = await reader.ReadToEndAsync();
			sb.AppendLine("Body: " + body);

			logger.LogInformation(sb.ToString());

			return logger;
		}
	}
}

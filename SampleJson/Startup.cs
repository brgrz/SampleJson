using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SampleJson.DataSources.Northwind.Models;

namespace SampleJson
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
			services.AddRouting(options => options.LowercaseUrls = true);
			services.AddRouting(options => options.LowercaseQueryStrings = true);
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddDbContext<NorthwindDbContext>(options =>
		options.UseSqlServer(Configuration.GetConnectionString("northwind")));

			// Register the Swagger services
			services.AddSwaggerDocument(config =>
			{
				config.PostProcess = document =>
				{
					document.Info.Version = "v1";
					document.Info.Title = "SampleJson API";
					document.Info.Description = "Providing sample JSON data";
					document.Info.TermsOfService = "MIT license";
					document.Info.Contact = new NSwag.OpenApiContact
					{
						Name = "brgrz",
						Email = string.Empty,
						Url = "https://twitter.com/brgrz"
					};
				};
			});

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();

			// Register the Swagger generator and the Swagger UI middlewares
			app.UseOpenApi();
			app.UseSwaggerUi3();

		}
	}
}

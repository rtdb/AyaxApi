using System;
using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using AyaxApi;
using AyaxApi.Repository;

namespace TodoApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		/// <summary>
		/// Dependency injection with Autofac container.
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			string connString = null;
			try
			{
				connString = Utils.LoadFromSettings("AyaxApiSettings.json");
			}
			catch(FileNotFoundException ex)
			{
				throw new AyaxApiException("Сбой при загрузке конфигурации.", ex);
			}

			var builder = new ContainerBuilder();

			builder
				.RegisterType<EFRepository>()
				.As<IRepository>()
				.WithParameter("connString", connString);

			builder.Populate(services);

			var container = builder.Build();
			return new AutofacServiceProvider(container);
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
		}
	}
}
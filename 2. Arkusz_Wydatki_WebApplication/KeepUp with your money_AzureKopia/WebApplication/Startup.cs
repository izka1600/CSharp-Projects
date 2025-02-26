﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthDatabase;
using Microsoft.EntityFrameworkCore;
using AuthDatabase.Entities;
using Microsoft.AspNetCore.Identity;
using WebApplication.Services;
using AutoMapper;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace WebApplication
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
			services.AddDbContext<AuthDatabaseContext>(options =>
			options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
			b => b.MigrationsAssembly("AuthDatabase")));

			services.AddIdentity<AppUser, IdentityRole>()
			 .AddEntityFrameworkStores<AuthDatabaseContext>()
			 .AddDefaultTokenProviders();


			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddTransient<IArkuszService, ArkuszSerice>();
			services.AddAutoMapper();

			services.AddAuthentication();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

			var defaultDateCulture = "fr-FR";
			var modified = new CultureInfo(defaultDateCulture);
			modified.NumberFormat.CurrencySymbol = "RM";
			modified.NumberFormat.CurrencyDecimalDigits = 2;
			modified.NumberFormat.CurrencyDecimalSeparator = ".";
			modified.NumberFormat.NumberDecimalSeparator = ".";
			modified.NumberFormat.CurrencyGroupSeparator = ",";
			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture(modified),
				SupportedCultures = new List<CultureInfo>
				{
					modified,
				},
				SupportedUICultures = new List<CultureInfo>
				{
					modified,
				}
			});
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beanstalk.Db.Context;
using Beanstalk.Db.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace Beanstalk
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // note this 100% unacceptable parsing of EBS files
            // https://stackoverflow.com/questions/44855453/access-aws-elasticbeanstalk-custom-environment-variables-with-net-core-webapp
            var builder = new ConfigurationBuilder()
                                .SetBasePath(env.ContentRootPath)
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                                .AddJsonFile(@"C:\Program Files\Amazon\ElasticBeanstalk\config\containerconfiguration", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

            var config = builder.Build();

            builder.AddInMemoryCollection(ParseEbConfig(config));

            Configuration = builder.Build();
        }

        private static Dictionary<string, string> ParseEbConfig(IConfiguration config)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (IConfigurationSection pair in config.GetSection("iis:env").GetChildren())
            {
                string[] keypair = pair.Value.Split(new[] { '=' }, 2);
                dict.Add(keypair[0], keypair[1]);
            }

            return dict;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // When used with ASP.net core, add these lines to Startup.cs
            services.AddEntityFrameworkNpgsql()
                    .AddDbContext<BeanstalkDbContext>(options =>
                    {
                        options.UseNpgsql(GetRdsConnectionString(), builder => builder.MigrationsAssembly("Beanstalk.Db"));
                    });

            services.AddMvc();
        }

        private string GetRdsConnectionString()
        {
            // http://www.npgsql.org/doc/connection-string-parameters.html
            var dbname = GetEnv("RDS_DB_NAME", null);
            var username = GetEnv("RDS_USERNAME", null);
            var password = GetEnv("RDS_PASSWORD", null);
            var hostname = GetEnv("RDS_HOSTNAME", null);
            var port = GetEnv("RDS_PORT", null);
            var conn = $"Host={hostname};Port={port};Database={dbname};Username={username};Password={password};";
            return conn;
        }

        private string GetEnv(string key, string defaultValue = null)
        {
            var value = Configuration.GetSection("DbSettings").GetValue<string>(key, null);
            if (value == null)
            {
                value = System.Environment.GetEnvironmentVariable(key);
            }

            return value ?? defaultValue;
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
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            var seed = Configuration.GetSection("AppSettings").GetValue("Seed", false);
            if (seed)
            {
                try
                {
                    Seed(app);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Seeding messed up");
                    Console.WriteLine(e);
                }
            }
        }

        private void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BeanstalkDbContext>();
                if (context.Teams.Any())
                {
                    return;
                }

                context.Teams.Add(new Team
                {
                    Name = "Fulchester Rovers",
                    Won = 15,
                    Drawn = 0,
                    Lost = 0
                });
                context.Teams.Add(new Team
                {
                    Name = "Melchester Town",
                    Won = 8,
                    Drawn = 2,
                    Lost = 5
                });
                context.Teams.Add(new Team
                {
                    Name = "Pretty Shitty City",
                    Won = 1,
                    Drawn = 2,
                    Lost = 13
                });
                context.SaveChanges();
            }
        }
    }
}

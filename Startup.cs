using System;
using System.Text;
using APIBlog.AcessoPostgre;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace APIBlog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // This method gets called by the runtime. Use this method to add services to the container.
            // var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            // services.AddDbContext<APIBlogContext>(options =>
            //     options.UseNpgsql(
            //         connectionString
            //     )
            // );

            IServiceCollection serviceCollection1 = services.AddEntityFrameworkNpgsql().AddDbContext<APIBlogContext>(opt =>
               opt.UseNpgsql(Configuration.GetConnectionString("Development")));
            
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Api Blog",
                        Version = "v1",
                        Description = "API informações Comentarios",
                        Contact = new Contact
                        {
                            Name = "Luiz Filipe",
                            Url = "https://github.com/andfilipe1",
                            Email = "luiz.brandao@live.com.br"
                        }
                    });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = true;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["sKey"])),
                    ValidAudience = "https://localhost",
                    ValidIssuer = "APIBlog"
                };
            });

            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<APIBlogContext>().AddDefaultTokenProviders();
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
                app.UseHsts();
            }
            // Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agenda API V1");
            });
            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

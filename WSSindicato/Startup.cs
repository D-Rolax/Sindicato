using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSSindicato.Hubs;
using WSSindicato.Models;
using WSSindicato.Models.Common;
using WSSindicato.Services;
using WSSindicato.Services.GruposComunidad;
using WSSindicato.Services.HorarioViajes;

namespace WSSindicato
{
    public class Startup
    {
        readonly string MiCors = "Micors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SindicatoContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Cn")));
    
            services.AddCors(options => {
                options.AddPolicy(name: MiCors,
                                  builder =>
                                  {
                                      builder.WithHeaders("*");
                                      builder.WithOrigins("*");
                                      builder.WithMethods("*");
                                  });
            });

            services.AddControllers();

            var appSetingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSetingsSection);

            var appSetings = appSetingsSection.Get<AppSettings>();
            var llave = Encoding.ASCII.GetBytes(appSetings.Secreto);
            services.AddAuthentication(d =>
            {
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(d=> {
                d.RequireHttpsMetadata = false;
                d.SaveToken = true;
                d.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(llave),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            //services.AddIdentity<IdentityUser, IdentityRole>(options =>
            //{
            //    options.User.RequireUniqueEmail = false;
            //})
            //.AddEntityFrameworkStores<Providers.Database.EFProvider.DataContext>()
            //.AddDefaultTokenProviders();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVehiculoService, VehiculoService>();
            services.AddScoped<IAfiliadoService, AfiliadoService>();
            services.AddScoped<IComunidadService, ComunidadService>();
            services.AddScoped<IGrupoService, GrupoService>();
            services.AddScoped<IRutasService, RutasService>();
            services.AddSignalR();
            //services.AddHostedService<PopulationHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MiCors);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<TrakingHub>("/trakingHub");
            });
        }
    }
}

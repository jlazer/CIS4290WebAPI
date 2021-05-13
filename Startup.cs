using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WebAPI.Services;
using WebAPI.Entities;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
//above using is what the documentation from Hwang has
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
//using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddMvc(options=>
            options.EnableEndpointRouting = false);
            // changed the connections string from the Client side DB path to the API DB path. Since the it was using client side and the client side did not have the review table it was not finding the review table.
            // this was the connectionString we were using previously "ConnectionStringOnlineStore"
            var conn = Configuration["connectionStrings:sqlConnectionAPI"];

            //SqlDbContext is our connection to the DB using our connection string from secrets.json(conn)
            services.AddDbContext<SqlDbContext>(options =>
                options.UseSqlServer(conn));

            //Add Identity’s Database Context
            services.AddDbContextPool<ApplicationDbContext>(options =>
                            options.UseSqlServer(conn));

            //Implement Identity 
            services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>()
                           .AddEntityFrameworkStores<ApplicationDbContext>()
                           .AddDefaultTokenProviders();
            //Add JSON Web Token Authentication
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        //Recieve JWT config info from secrets.json
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            //Entity Framework allows interaction with DB, Generics allow data types to be assigned at runtime
            services.AddScoped(typeof(IGenericEFRepository), typeof(GenericEFRepository));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });

            //Using AutoMapper Package to Map Entities to DTOs AND vice versa
            //Entities represent tables in the DB
            //Data Transfer Object is used to turn entity data into a response object OR  convert request data into an entity model


            // https://stackoverflow.com/questions/59713215/mapper-does-not-contain-a-definition-for-initialize-automapper-c-sharp
            // the above stackoverflow link says that mapper.initialize is obsolete and to use something similar to below. we need to adapt the below code to instantiate all of the objects above.
            /* var config = new MapperConfiguration(cfg =>
             {
                 //cfg.CreateMap<Entities.Cart, Models.CartDTO>();
                 //cfg.CreateMap<Models.CartDTO, Entities.Cart>();

                 cfg.CreateMap<Entities.Product, Models.ProductDTO>();
                 cfg.CreateMap<Models.ProductDTO, Entities.Product>();
                 cfg.CreateMap<Entities.Product, Models.ProductUpdateDTO>();
                 cfg.CreateMap<Models.ProductUpdateDTO, Entities.Product>();

             });*/

            //var mapper = config.CreateMapper();
        }
        //the below lines were created by the original api template. if we are not using swagger we shouldnt need them.

        
        
     


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseAuthentication();
            app.UseMvc();
        

            //app.UseHttpsRedirection();
            //app.UseRouting();
            //app.UseAuthorization();

            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });*/
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Flashcards;
using FlashcardsApi.Models;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace FlashcardsApi
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.Issuer,

                            ValidateAudience = false,
                            ValidAudience = AuthOptions.Audience,

                            ValidateLifetime = false,

                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.ResourceAccess, policy => policy.Requirements.Add(new SameOwnerRequirement()));
            });
            services.AddSingleton<IAuthorizationHandler, OwnedResourcesAuthorizationHandler>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddMvc().AddJsonOptions(opt => opt.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto);

            AutoMapper.Mapper.Initialize(config =>
                config.CreateMap<Collection, CollectionDto>()
                    .ForMember(
                        dto => dto.CardIds, 
                        opt => opt.MapFrom(coll => coll.Cards.Select(card => card.Id))
                    )
            );
            services.AddSingleton<IStorage, Mongo>();
	        services.AddSingleton<IAnswersStorage>(new MongoAnswersStorage());
            services.AddSingleton<IUserStorage, DumbUserStorage>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

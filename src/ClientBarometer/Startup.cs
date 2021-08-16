using System;
using ClientBarometer.Configurations;
using ClientBarometer.DataAccess;
using ClientBarometer.Domain.Clients;
using ClientBarometer.Domain.Repositories;
using ClientBarometer.Domain.Services;
using ClientBarometer.Domain.UnitsOfWork;
using ClientBarometer.Extensions;
using ClientBarometer.Implementations.Clients;
using ClientBarometer.Implementations.Repositories;
using ClientBarometer.Implementations.Services;
using ClientBarometer.Implementations.UnitsOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ngrok.Adapter.Service;
using Telegram.Bot;

namespace ClientBarometer
{
    public class Startup
    {
        private readonly TelegramBotConfig _telegramBotConfig;
        private readonly PredictorConfig _predictorConfig;
        private readonly MemoryCacheConfig _memoryCacheConfig;
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _telegramBotConfig = configuration.GetSection("TelegramBot").Get<TelegramBotConfig>();
            _predictorConfig = configuration.GetSection("Predictor").Get<PredictorConfig>();
            _memoryCacheConfig = configuration.GetSection("MemoryCache").Get<MemoryCacheConfig>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("MySqlConnection");
            services
                .AddDbContext<ClientBarometerDbContext>(connectionString);

            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddCors(options => options.AddPolicy("AllowAll", conf =>
            {
                conf.AllowAnyOrigin();
                conf.AllowAnyHeader();
                conf.AllowAnyMethod();
            }));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Storage API", Version = "v1" });
                c.CustomSchemaIds(type => type.FullName);
            });

            // Telegram
            services.AddSingleton<INgrokService>(s => new NgrokService(_telegramBotConfig.NgrokHost));
            services.AddScoped(serv => new TelegramBotClient(_telegramBotConfig.Token));
            services.AddHostedService<TelegramBotInitService>();
            
            // Clients
            services.AddHttpClient<IPredictorClient, PredictorClient>("predictor", c =>
            {
                c.BaseAddress = new Uri(_predictorConfig.Host);
            });
            
            // Services
            services.AddMemoryCache(entry =>
            {
                entry.ExpirationScanFrequency = TimeSpan.FromMinutes(_memoryCacheConfig.ExpirationInMinutes);
            });
            services.AddScoped<ISourceProcessor, SourceProcessor>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBarometerService, BarometerService>();
            services.AddScoped<ISuggestionService, SuggestionService>();
            
            // Repositories
            services.AddScoped<IChatReadRepository, ChatReadRepository>();
            services.AddScoped<IBarometerReadRepository, BarometerReadRepository>();
            services.AddScoped<IMessageReadRepository, MessageReadRepository>();
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IChatUnitOfWork, ChatUnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var frontConfig = Configuration.GetSection("Front").Get<FrontConfig>();
            
            UpdateDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Storage API V1");
            });

            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("AllowAll"); 
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ClientBarometerDbContext>())
                {
                    context?.Database.Migrate();
                }
            }
        }
    }
}

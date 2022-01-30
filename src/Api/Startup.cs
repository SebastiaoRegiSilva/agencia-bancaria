using Agencia.Plataforma.Domain.Accounts;
using Agencia.Plataforma.Domain.Clients;
using Agencia.Plataforma.Infrastructure.Repositories.MongoDb.Accounts;
using Agencia.Plataforma.Infrastructure.Repositories.MongoDb.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Agencia.Plataforma
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
            string mongoConString = Configuration.GetValue<string>("DB:Mongo:ConString");
            string mongoDatabase = Configuration.GetValue<string>("DB:Mongo:Database");

            var clientRep = new ClientRepository(mongoConString, mongoDatabase);
            var clientService = new ClientService(clientRep);
            services.AddSingleton<ClientService>(clientService);

            var accountRep = new AccountRepository(mongoConString, mongoDatabase);
            services.AddSingleton<AccountService>(new AccountService(accountRep, clientService));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "agencia_bancaria", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "agencia_bancaria v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
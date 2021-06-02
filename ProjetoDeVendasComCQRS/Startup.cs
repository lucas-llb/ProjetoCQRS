using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ProjetoDeVendasComCQRS.Application.Interfaces.Mappers;
using ProjetoDeVendasComCQRS.Application.Interfaces.Services;
using ProjetoDeVendasComCQRS.Application.Mappers;
using ProjetoDeVendasComCQRS.Application.Messageria;
using ProjetoDeVendasComCQRS.Application.Services;
using ProjetoDeVendasComCQRS.Domain.Commands.Cliente;
using ProjetoDeVendasComCQRS.Domain.Commands.Pedido;
using ProjetoDeVendasComCQRS.Domain.Commands.Produto;
using ProjetoDeVendasComCQRS.Domain.Interfaces.Publisher;
using ProjetoDeVendasComCQRS.Domain.Interfaces.Repository;
using ProjetoDeVendasComCQRS.Domain.Models;
using ProjetoDeVendasComCQRS.Domain.Validation.Cliente;
using ProjetoDeVendasComCQRS.Domain.Validation.Pedido;
using ProjetoDeVendasComCQRS.Domain.Validation.Produto;
using ProjetoDeVendasComCQRS.Infra.Context;
using ProjetoDeVendasComCQRS.Infra.Publisher;
using ProjetoDeVendasComCQRS.Infra.Repository;

namespace ProjetoDeVendasComCQRS
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
            AddDbContextCollection(services);
            services.AddControllers();
            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMqConfig"));
            services.Configure<PedidosDatabaseSettings>(Configuration.GetSection(nameof(PedidosDatabaseSettings)));
            services.AddSingleton<IPedidosDatabaseSettings>(sp => sp.GetRequiredService<IOptions<PedidosDatabaseSettings>>().Value);
            DependencyInjection(services);
            services.AddScoped<AdicionarPedidoCommandHandler>();         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope
                    .ServiceProvider
                    .GetRequiredService<MainContext>();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void AddDbContextCollection(IServiceCollection services)
        {
            services.AddDbContext<MainContext>(opt => opt
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }
        private void DependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoMongoRepository, PedidoMongoRepository>();
            services.AddScoped<IValidator<AdicionarClienteCommand>, AdicionarClienteCommandValidator>();
            services.AddScoped<IValidator<EditarClienteCommand>, EditarClienteCommandValidator>();
            services.AddScoped<IValidator<RemoverClienteCommand>, RemoverClienteCommandValidator>();
            services.AddScoped<IValidator<AdicionarProdutoCommand>, AdicionarProdutoCommandValidator>();
            services.AddScoped<IValidator<EditarProdutoCommand>, EditarProdutoCommandValidator>();
            services.AddScoped<IValidator<RemoverProdutoCommand>, RemoverProdutoCommandValidator>();
            services.AddScoped<IValidator<AdicionarPedidoCommand>, AdicionarPedidoCommandValidator>();
            services.AddScoped<IValidator<EditarPedidoCommand>, EditarPedidoCommandValidator>();
            services.AddScoped<IValidator<RemoverPedidoCommand>, RemoverPedidoCommandValidator>();
            services.AddScoped<IClienteMapper, ClienteMapper>();
            services.AddScoped<IProdutoMapper, ProdutoMapper>();
            services.AddScoped<IPedidoMapper, PedidoMapper>();
            services.AddScoped<IPedidoDocumentMapper, PedidoDocumentMapper>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IAdicionarPedidoPublisher, AdicionarPedidoPublisher>();


        }
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Portal.Api.App;
using Portal.Api.Filtro;
using Portal.Api.Interfaces;
using Portal.Core.AzureStorage;
using Portal.Core.Identity;
using Portal.Core.Interfaces;
using Portal.Infra.Configuration;
using Portal.Infra.Repository;
using Portal.Storage;
using Portal.Storage.Util;
using Portal.Web.Models;
using Portal.Web.Services;
using Portal.Web.UtilWeb;
using System;
using System.Text;

namespace Portal.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Context Adicionado.
            services.AddDbContext<Context>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<DbContext, Context>();
            //ApplicationDbContext Adicionado.
            services.AddDbContext<ApplicationDbContext>(a =>
                a.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<StorageConfig>(Configuration.GetSection("StorageConfig"));

            services.Configure<IdentityOptions>(options =>
            {
                //Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                //Obtém ou define o TimeSpan para o qual um usuário fica bloqueado quando ocorre um bloqueio. O padrão é 5 minutos.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //Obtém ou define o número de tentativas de acesso com falha permitidas antes que um usuário seja bloqueado, supondo que o bloqueio esteja ativado. O padrão é 5
                options.Lockout.MaxFailedAccessAttempts = 5;
                //Obtém ou define um sinalizador indicando se um novo usuário pode ser bloqueado. O padrão é true.
                options.Lockout.AllowedForNewUsers = true;

                //User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                //Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/Login";
                options.SlidingExpiration = true;
            });

            //Interface Generic
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //Interfaces Dominio
            services.AddTransient<IRepositoryArquivos, RepositoryArquivos>();
            services.AddTransient<IRepositoryCheckLists, RepositoryCheckLists>();
            services.AddTransient<IRepositoryClientes, RepositoryClientes>();
            services.AddTransient<IRepositoryMotoristas, RepositoryMotoristas>();
            services.AddTransient<IRepositoryProdutores, RepositoryProdutores>();
            services.AddTransient<IRepositoryProdutores_Clientes, RepositoryProdutores_Clientes>();
            services.AddTransient<IRepositoryProdutos, RepositoryProdutos>();
            services.AddTransient<IRepositoryRamosSeguros, RepositoryRamosSeguros>();
            services.AddTransient<IRepositoryRel_Usuarios_Produtores, RepositoryRel_Usuarios_Produtores>();
            services.AddTransient<IRepositorySeguradoras, RepositorySeguradoras>();
            services.AddTransient<IRepositorySinistros, RepositorySinistros>();
            services.AddTransient<IRepositoryTiposArquivos, RepositoryTiposArquivos>();
            services.AddTransient<IRepositoryUfs, RepositoryUfs>();
            services.AddTransient<IRepositoryUnidades, RepositoryUnidades>();
            services.AddTransient<IRepositoryUsuarios, RepositoryUsuarios>();
            services.AddTransient<IRepositoryTiposSituacoes, RepositoryTiposSituacoes>();
            services.AddTransient<IRepositoryRegrasAcessos, RepositoryRegrasAcessos>();
            services.AddTransient<IRepositoryContatosPortal, RepositoryContatosPortal>();
            services.AddTransient<IRepositoryListaContatosPortal, RepositoryListaContatosPortal>();
            services.AddTransient<IRepositoryCargo, RepositoryCargo>();

            //Interfaces Api
            services.AddTransient<IAppArquivos, AppArquivos>();
            services.AddTransient<IAppClientes, AppClientes>();
            services.AddTransient<IAppSinistros, AppSinistros>();
            services.AddTransient<IAppTiposArquivos, AppTiposArquivos>();
            services.AddTransient<IAppUsuarios, AppUsuarios>();
            services.AddTransient<IAppLog, AppLog>();
            services.AddTransient<IAppContatosPortal, AppContatosPortal>();
            services.AddTransient<IAppListaContatosPortal, AppListaContatosPortal>();
            services.AddTransient<IAppCargos, AppCargos>();

            //Add application services.
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<MakeEmail>();
            services.AddTransient<UtilStorage>();
            services.AddTransient<AzureLogStorage>();

            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc();
            //services.AddMvc().AddSessionStateTempDataProvider();
            //services.AddMvc().AddRazorRuntimeCompilation();
            //services.AddDistributedMemoryCache();
            // Adiciona serviços do framework
            services.AddMvc(options =>
            {
                //adicionado por instância 
                options.Filters.Add(new PortalAuthorize());
                //adicionado por tipo  
                options.Filters.Add(typeof(PortalAuthorize));
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                ClockSkew = TimeSpan.Zero
            });
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            //services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            //esse comando é redireciona a pagina para https
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}");
            });
        }
    }
}

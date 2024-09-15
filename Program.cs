using CrudMVC.Data;
using CrudMVC.Helper;
using CrudMVC.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o DbContext ao contêiner de injeção de dependência
builder.Services.AddDbContext<BancoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));

//Toda vez que IContatoRepositorio for chamado, ContatoRepositorio será chamado
builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IEmail, Email>();

builder.Services.AddScoped<ISessao, Sessao>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSession(o => {
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
 });


// Adiciona os serviços ao contêiner
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura o pipeline de solicitação HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();

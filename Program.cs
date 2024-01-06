using BlazorLearWebApp.Components;
using BlazorLearWebApp.Service;
using BootstrapBlazor.Components;
using FreeSql;
using Console = System.Console;

var builder = WebApplication.CreateBuilder(args);
// 安装freeSql
var fsql = new FreeSql.FreeSqlBuilder()
    .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=document.db")
    .UseMonitorCommand(cmd=>Console.WriteLine($"Sql:{cmd.CommandText}"))
    .UseAutoSyncStructure(true) //automatically synchronize the entity structure to the database
    .Build();
// 设置baseEntity模式
BaseEntity.Initialization(fsql, null);


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddBootstrapBlazor();
builder.Services.AddScoped(typeof(IDataService<>), typeof(FreeSqlDataService<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

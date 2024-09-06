using Business.Implements;
using Business.Interfaces;
using Data.Implements;
using Data.interfaces;
using Data.Interfaces;
using Entity.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDBContext>(options =>
options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnection")));

builder.Services.AddScoped<IModuleData,ModuleData>();
builder.Services.AddScoped<IModuleBusiness,ModuleBusiness>();

builder.Services.AddScoped<ICityData,CityData>();
builder.Services.AddScoped<ICityBusiness, CityBusiness>();

builder.Services.AddScoped<ICountriesData, CountriesData>();
builder.Services.AddScoped<ICountriesBusiness, CountriesBusiness>();

builder.Services.AddScoped<IPersonData, PersonData>();
builder.Services.AddScoped<IPersonBusiness, PersonBusiness>();

builder.Services.AddScoped<IRoleData, RoleData>();
builder.Services.AddScoped<IRoleBusiness, RoleBusiness>();

builder.Services.AddScoped<IRoleViewData, RoleViewData>();
builder.Services.AddScoped<IRoleViewBusiness, RoleViewBusiness>();

builder.Services.AddScoped<IStateData, StateData>();
builder.Services.AddScoped<IStateBusiness, StateBusiness>();

builder.Services.AddScoped<IUserData, UserData>();
builder.Services.AddScoped<IUserBusiness, UserBusiness>();

builder.Services.AddScoped<IUserRoleData, UserRoleData>();
builder.Services.AddScoped<IUserRoleBusiness, UserRoleBusiness>();

builder.Services.AddScoped<IViewData, ViewData>();
builder.Services.AddScoped<IViewBusiness, ViewBusiness>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

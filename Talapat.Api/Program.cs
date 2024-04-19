using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talapat.Api.Errors;
using Talapat.Api.Extensions;
using Talapat.Api.Helpers;
using Talapat.Api.Middlwares;
using Talapat.BLL.Interfaces;
using Talapat.BLL.Repositories;
using Talapat.BLL.Services;
using Talapat.DAL.Contexts;
using Talapat.DAL.Entities.IdentityEntities;
using Talapat.DAL.Identity;
using Talapat.DAL.IdentityDbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy", policy =>
	policy.AllowAnyHeader().AllowAnyMethod().WithOrigins()
	);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<AppIdentityDbContet>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));


builder.Services.AddSingleton<IConnectionMultiplexer>(s => {
	var connection = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
	return ConnectionMultiplexer.Connect(connection);
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRpository));
builder.Services.AddScoped(typeof(ITokenService), typeof(TokenService));
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddSingleton<IResponseCacheService, ResponseCacheService>();
builder.Services.AddIdentityServies(builder.Configuration);


builder.Services.Configure<ApiBehaviorOptions>(options => {
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState.Where(m => m.Value.Errors.Count > 0)
                                            .SelectMany(m => m.Value.Errors)
                                            .Select(m => m.ErrorMessage).ToArray();
        var responseMessage = new ApiValidationErrorsResponse()
        {
            Errors = errors
        };
        return new BadRequestObjectResult(responseMessage);
    };
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	
    var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
	try
	{
		//Create Context From Existed Context That Defined Before from Bulider.services
		var context = services.GetRequiredService<StoreContext>();
		await context.Database.MigrateAsync();
		await StoreContextSeed.SeedAsync(context, LoggerFactory);
		
		var userManager = services.GetRequiredService<UserManager<AppUser>>();
		await AppIdentityDbContextSeed.SeedUserAsync(userManager);
		var IdentityContext = services.GetRequiredService<AppIdentityDbContet>();
		await IdentityContext.Database.MigrateAsync();
    }
	catch (Exception ex)
	{
		//Any Migrations Problem Will Be Logged in Console
		var logger = LoggerFactory.CreateLogger<Program>();
		logger.LogError(ex, "an error occur during applying migrations");
	}
}


// Configure the HTTP request pipeline.
app.UseMiddleware<ExeptionMiddlware>();

if (app.Environment.IsDevelopment())
{
	//app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.Run();

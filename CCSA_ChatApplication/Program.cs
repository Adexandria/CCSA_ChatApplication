using CCSA_ChatApp.Authentication.Services;
using CCSA_ChatApp.Db;
using CCSA_ChatApp.Db.Repositories;
using CCSA_ChatApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using AuthenticationService = CCSA_ChatApp.Authentication.Services.AuthenticationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAuthorizationHandler, AdminHandler>();
builder.Services.AddScoped<SessionFactory>();
builder.Services.AddScoped<MessageHistoryRepository>();
builder.Services.AddScoped<GroupChatRepository>();
builder.Services.AddScoped<UserProfileRepository>();
builder.Services.AddScoped<ITokenCredential, TokenCredential>();
builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<IGroupChatService, GroupChatService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IAuth, AuthService>();
AuthenticationService.ConfigureServices(builder.Configuration, builder.Services);
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("GroupAdmin", policy => policy.Requirements.Add(new AdminRequirement("Admin")));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

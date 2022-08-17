using CCSA_ChatApp.Authentication.Services;
using CCSA_ChatApp.Db;
using CCSA_ChatApp.Db.Repositories;
using CCSA_ChatApp.Domain.Models;
using CCSA_ChatApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using System.Reflection;
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
builder.Services.AddScoped<MessageRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<GroupChatRepository>();
builder.Services.AddScoped<UserProfileRepository>();
builder.Services.AddScoped<ITokenCredential, TokenCredential>();
builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<IGroupChatService, GroupChatService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IAuth, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
AuthenticationService.ConfigureServices(builder.Configuration, builder.Services);
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("GroupAdmin", policy => policy.Requirements.Add(new AdminRequirement("Admin")));
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("ChatPlatform", new OpenApiInfo()
    {
        Title = "Chat Platform API",
        Version = "1.0",
        Description = "A Rest API to chat with friends",
        Contact = new OpenApiContact()
        {
            Name = "Group 2"
        }

    });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {

        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        In = ParameterLocation.Header,
        BearerFormat = "bearer",
        Description = "Enter Token Only"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                         new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "bearer"
                                }
                            },
                          new string[] {}
                    }

                });
    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
    //... and tell Swagger to use those XML comments.
    c.IncludeXmlComments(xmlPath);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/ChatPlatform/swagger.json", "Chat Platform API");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

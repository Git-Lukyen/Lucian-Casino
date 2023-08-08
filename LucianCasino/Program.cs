using Firebase.Auth;
using Firebase.Auth.Providers;
using FirebaseAdmin;
using FireSharp;
using LucianCasino;
using LucianCasino.Authentication;
using LucianCasino.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddScheme<AuthenticationSchemeOptions,
        FirebaseAuthHandler>(JwtBearerDefaults.AuthenticationScheme, options => { });

builder.Services.AddSingleton(FirebaseApp.Create());

FirebaseClient defaultFirebaseClient = new FirebaseClient(Secrets.DbClientConfig);
FirebaseAuthClient defaultFirebaseAuthClient = new FirebaseAuthClient(Secrets.FirebaseAuthConfig);

builder.Services.AddSingleton(new FirebaseAuthService(defaultFirebaseAuthClient, defaultFirebaseClient));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();

app.Run();
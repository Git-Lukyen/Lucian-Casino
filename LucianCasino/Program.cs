using Firebase.Auth;
using Firebase.Auth.Providers;
using FirebaseAdmin;
using FireSharp.Config;
using LucianCasino.Authentication;
using LucianCasino.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FirebaseAuth = FirebaseAdmin.Auth.FirebaseAuth;

const string WEB_API_KEY = "AIzaSyDljO16NlyEziscGcm4WglKzjwIsda6dIQ";
const string FIREBASE_PROJECT_ID = "online-casino-fdb";
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

builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig
{
    ApiKey = WEB_API_KEY,
    AuthDomain = $"{FIREBASE_PROJECT_ID}.firebaseapp.com",
    Providers = new FirebaseAuthProvider[]
    {
        new EmailProvider()
    }
}));

builder.Services.AddSingleton<FirebaseAuthService>(); 

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
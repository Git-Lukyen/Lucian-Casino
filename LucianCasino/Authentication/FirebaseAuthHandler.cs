using System.Security.Claims;
using System.Text.Encodings.Web;
using Firebase.Auth;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using FirebaseAuth = FirebaseAdmin.Auth.FirebaseAuth;

namespace LucianCasino.Authentication;

public class FirebaseAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly FirebaseApp _firebaseApp;

    public FirebaseAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, FirebaseApp firebaseApp) : base(options, logger, encoder, clock)
    {
        _firebaseApp = firebaseApp;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Context.Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.NoResult();

        string bearerToken = Context.Request.Headers["Authorization"];
        if (bearerToken == null || !bearerToken.StartsWith("Bearer "))
            return AuthenticateResult.Fail("invalid scheme");

        string token = bearerToken.Substring("Bearer ".Length);

        try
        {
            FirebaseToken firebaseToken = await FirebaseAuth.GetAuth(_firebaseApp).VerifyIdTokenAsync(token);

            return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new List<ClaimsIdentity>()
            {
                new ClaimsIdentity(ToClaims(firebaseToken.Claims), nameof(FirebaseAuthHandler))
            }), JwtBearerDefaults.AuthenticationScheme));
        }
        catch (Exception e)
        {
            return AuthenticateResult.Fail(e);
        }
    }

    private IEnumerable<Claim> ToClaims(IReadOnlyDictionary<string, object> firebaseTokenClaims)
    {
        return new List<Claim>()
        {
            new Claim("id", firebaseTokenClaims["user_id"].ToString()),
            new Claim("email", firebaseTokenClaims["email"].ToString())
        };
    }
}
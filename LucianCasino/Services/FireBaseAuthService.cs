using Firebase.Auth;
using Firebase.Auth.Providers;

namespace LucianCasino.Services;

public class FireBaseAuthService
{
    private readonly FirebaseAuthClient _firebaseAuth;
    private string firebaseProjectName = "online-casino-fdb";

    public FireBaseAuthService()
    {
        _firebaseAuth = new FirebaseAuthClient(new FirebaseAuthConfig
        {
            ApiKey = "AIzaSyDljO16NlyEziscGcm4WglKzjwIsda6dIQ",
            AuthDomain = $"{firebaseProjectName}.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]
            {
                new EmailProvider(),
                new GoogleProvider()
            }
        });
    }

    public async Task<string?> SignUp(string email, string password)
    {
        var userCredentials = await _firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password);
        return userCredentials is null ? null : await userCredentials.User.GetIdTokenAsync();
    }

    public async Task<string?> Login(string email, string password)
    {
        var userCredentials = await _firebaseAuth.SignInWithEmailAndPasswordAsync(email, password);
        return userCredentials is null ? null : await userCredentials.User.GetIdTokenAsync();
    }

    public void SignOut() => _firebaseAuth.SignOut();
}
using Firebase.Auth;
using FireSharp;
using LucianCasino.DBObjects;
using LucianCasino.DBObjects.DTO;

namespace LucianCasino.Services;

public class FirebaseAuthService
{
    private readonly FirebaseAuthClient _firebaseAuth;
    private readonly FirebaseClient _firebaseClient;

    public FirebaseAuthService(FirebaseAuthClient firebaseAuth, FirebaseClient firebaseClient)
    {
        _firebaseAuth = firebaseAuth;
        _firebaseClient = firebaseClient;
    }

    public async Task<string?> SignUp(UserDTO userDto)
    {
        UserCredential userCredential =
            await _firebaseAuth.CreateUserWithEmailAndPasswordAsync(userDto.Email, userDto.Password, userDto.Name);

        if (userCredential != null)
        {
            string token = await userCredential.User.GetIdTokenAsync();
            await _firebaseClient.PushAsync("Users", new DbUser()
            {
                Id = token,
                Name = userDto.Name,
                Email = userDto.Email
            });

            return token;
        }

        return null;
    }

    public async Task<string?> SignIn(string email, string password)
    {
        UserCredential userCredential = await _firebaseAuth.SignInWithEmailAndPasswordAsync(email, password);
        return await userCredential.User.GetIdTokenAsync();
    }

    public void SignOut() => _firebaseAuth.SignOut();
}
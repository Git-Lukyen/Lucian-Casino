using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using LucianCasino.DBObjects;
using Newtonsoft.Json;

namespace LucianCasino.Services;

public class UserService
{
    private IFirebaseConfig config = new FirebaseConfig
    {
        AuthSecret = "LnaxDRLaaZ7Wlp8L2LYQpn8pAkP4vsfHauw63Zfy",
        BasePath = "https://online-casino-fdb-default-rtdb.europe-west1.firebasedatabase.app"
    };

    private readonly IFirebaseClient _client;

    public UserService()
    {
        _client = new FirebaseClient(config);
    }

    public async Task<IDictionary<string, User>> GetUsersAsync()
    {
        FirebaseResponse response = await _client.GetAsync("users");
        return JsonConvert.DeserializeObject<IDictionary<string, User>>(response.Body.ToString());
    }

    public async Task<string> AddUserAsync(User user)
    {
        PushResponse response = await _client.PushAsync("users", user);
        string ID = response.Result.name;
        user.ID = ID;
        _client.SetAsync("users/" + ID, user);
        return response.Result.name;
    }
}
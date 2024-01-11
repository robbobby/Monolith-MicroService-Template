using System.Threading.Tasks;
using Apis.Core.Model.Auth;
using Common.IdentityApi;
using Common.IdentityApi.Login;

namespace Client.Models.Apis;

public class AuthApi {
    public async Task<string?> Test() {
        return await ApiClient.GetAsync<string>("/Api/Identity/Identitys");
    }

    // TODO: Implement the return type
    public async Task<HttpResult?> Register(RegisterRequest request) {
        return await ApiClient.PostAsync<HttpResult>("/Api/Auth/Register", request);
    }

    public async Task<HttpResult<TokenResult?>?> Login(LoginRequest request) {
        var result = await ApiClient.PostAsync<HttpResult<TokenResult?>>("/Api/Auth/Login", request);
        if (result?.Succeeded == ResultType.Success) {
            ApiClient.SetTokens(result.Data!);
        }
        return result;
    }
    
    public async Task<HttpResult<TokenResult>?> UpdateToken() {
        var result = await ApiClient.PostAsync<HttpResult<TokenResult>>("/Api/Auth/UpdateToken");
        if (result?.Succeeded == ResultType.Success) {
            ApiClient.SetTokens(result.Data!);
        }
        
        return result;
    }
}
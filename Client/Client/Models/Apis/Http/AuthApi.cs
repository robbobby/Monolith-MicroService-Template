using System.Threading.Tasks;
using Common.Apis.Auth;
using Common.Apis.Auth.Login;
using Common.Apis.Auth.Register;
using Common.Apis.Auth.UpdateToken;

namespace Client.Models.Apis.Http;

public class AuthApi {
    public async Task<HttpResult?> Register(RegisterRequest request) {
        return await ApiClient.PostAsync<HttpResult>("/Api/Auth/Register", request);
    }

    public async Task<HttpResult<TokenResult?>?> Login(LoginRequest request) {
        var result = await ApiClient.PostAsync<HttpResult<TokenResult?>>("/Api/Auth/Login", request);
        if(result?.Succeeded == ResultType.Success) ApiClient.SetTokens(result.Data!);
        return result;
    }

    public async Task<HttpResult<TokenResult>?> UpdateToken(UpdateTokenRequest request) {
        var result = await ApiClient.PostAsync<HttpResult<TokenResult>>("/Api/Auth/UpdateToken", request);
        if(result?.Succeeded == ResultType.Success) ApiClient.SetTokens(result.Data!);

        return result;
    }

    public async Task<HttpResult?> SignOut() {
        var result = await ApiClient.PostAsync<HttpResult>("/Api/Auth/SignOut", ApiClient.RefreshToken);
        if(result?.Succeeded == ResultType.Success) ApiClient.ClearTokens();

        return result;
    }
}

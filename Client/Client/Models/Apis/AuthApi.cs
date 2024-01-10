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
        return await ApiClient.PostAsync<HttpResult<TokenResult?>>("/Api/Auth/Login", request);
    }

    public async Task<TokenResult?> RefreshToken(string refreshToken) {
        return await ApiClient.PostAsync<TokenResult>("/Api/Identity/RefreshToken", new { refreshToken });
    }

    public async Task ConfirmEmail(string userId, string code, string changeEmail) {
        await ApiClient.PostAsync<object>("/Api/Identity/ConfirmEmail", new { userId, code, changeEmail });
    }

    public async Task ResendConfirmationEmail(string email) {
        await ApiClient.PostAsync<object>("/Api/Identity/ResendEmailConfirmation", new { email });
    }

    public async Task ForgotPassword(string email) {
        await ApiClient.PostAsync<object>("/Api/Identity/ForgotPassword", new { email });
    }

    public async Task ResetPassword(string email, string resetCode, string newPassword) {
        await ApiClient.PostAsync<object>("/Api/Identity/ResetPassword",
            new { email, resetCode, password = newPassword });
    }
}

public class IdentityManageApi {
    public async Task<TwoFactorAuthDetails?> TwoFactorAuth(TwoFactoryAuthRequest request) {
        return await ApiClient.PostAsync<TwoFactorAuthDetails>("/Api/Identity/Manage/2fa");
    }

    public async Task<IdentityInfo?> IdentityInfo() {
        return await ApiClient.GetAsync<IdentityInfo>("/Api/Identity/Manage/IdentityInfo");
    }

    public async Task UpdateIdentityInfo(UpdateIdentityRequest request) {
        await ApiClient.PostAsync<object>("/Api/Identity/Manage/IdentityInfo", request);
    }
}
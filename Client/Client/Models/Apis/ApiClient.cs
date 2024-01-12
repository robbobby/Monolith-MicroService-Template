using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Text.Json;
using System.Threading.Tasks;
using Common.IdentityApi.Login;

namespace Client.Models.Apis;

public class ApiClient {
    private static readonly HttpClientHandler _handler = new();

    private static readonly HttpClient Client;

    static ApiClient() {
        if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
           RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
           RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
          ) {
            _handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            _handler.SslProtocols = SslProtocols.Tls12;
        }

        Client = new HttpClient(_handler) {
            BaseAddress = new Uri("https://localhost:7111")
        };
    }

    public static string RefreshToken { get; private set; } = null!;


    public static async Task<T?> GetAsync<T>(string uri) where T : class {
        try {
            var response = await Client.GetAsync(uri);
            Console.WriteLine(response.StatusCode);
            return await HandleResponse<T>(response);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<T?> PostAsync<T>(string uri, object? data = null) where T : class {
        try {
            var response = await Client.PostAsJsonAsync(uri, data);
            return await HandleResponse<T>(response);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<T?> PutAsync<T>(string uri, object? data) where T : class {
        try {
            var response = await Client.PutAsJsonAsync(uri, data);
            return await HandleResponse<T>(response);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<T?> DeleteAsync<T>(string uri) where T : class {
        try {
            var response = await Client.DeleteAsync(uri);
            return await HandleResponse<T>(response);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<T?> PatchAsync<T>(string uri, object? data) where T : class {
        try {
            var response = await Client.PatchAsJsonAsync(uri, data);
            return await HandleResponse<T>(response);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    private static async Task<T?> HandleResponse<T>(HttpResponseMessage response) where T : class {
        if(!response.IsSuccessStatusCode)
            LogOnError(response);
        else
            response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        if(string.IsNullOrEmpty(content))
            return default;

        if(typeof(T) == typeof(string))
            return content as T;

        if(typeof(T) == typeof(Guid))
            return Guid.Parse(content) as T;

        try {
            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (JsonException) {
            Console.WriteLine($"Error deserializing {content}");
            return null;
        }
    }

    private static void LogOnError(HttpResponseMessage response) {
        Console.WriteLine(response.StatusCode);
        Console.WriteLine(response.ReasonPhrase);
    }

    public static void SetTokens(TokenResult resultData) {
        AppState.User.FromToken(resultData.AccessToken);
        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", resultData.AccessToken);
        RefreshToken = resultData.RefreshToken;
    }

    public static void ClearTokens() {
        AppState.User = new User();
        Client.DefaultRequestHeaders.Authorization = null;
        RefreshToken = null!;
    }
}
using System;
using System.Threading.Tasks;
using Common.IdentityApi;

namespace Client.Models.Apis.Http;

public class UnitApi {
    public async Task<HttpResult<Guid?>?> CreateUnit(string name) {
        Console.WriteLine("Creating unit");
        return await ApiClient.PostAsync<HttpResult<Guid?>>("/Api/Unit", name);
    }
}

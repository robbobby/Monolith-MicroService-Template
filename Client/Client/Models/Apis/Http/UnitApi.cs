using System;
using System.Threading.Tasks;
using Common.Apis.Auth;
using Common.Apis.Project;

namespace Client.Models.Apis.Http;

public class UnitApi {
    public async Task<HttpResult<Guid?>?> CreateUnit(string name) {
        return await ApiClient.PostAsync<HttpResult<Guid?>>("/Api/Unit", name);
    }
}

public class ProjectApi {
    public async Task<HttpResult<Guid?>?> CreateProject(ProjectCreateRequest request) {
        return await ApiClient.PostAsync<HttpResult<Guid?>>("/Api/Project", request);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Apis.Auth;
using Common.Apis.Project;
using Common.Model;

namespace Client.Models.Apis.Http;

public class OrganisationsApi {
    public async Task<HttpResult<Guid?>?> CreateOrganisation(string name) {
        return await ApiClient.PostAsync<HttpResult<Guid?>>("/Api/Organisation", name);
    }
}

public class ProjectApi {
    public async Task<HttpResult<Guid?>?> CreateProject(ProjectCreateRequest request) {
        return await ApiClient.PostAsync<HttpResult<Guid?>>("/Api/Project", request);
    }
}

public class TicketApi {
    public async Task<HttpResult<IEnumerable<TicketDto>>?> GetProjectTickets(Guid projectId) {
        return await ApiClient.GetAsync<HttpResult<IEnumerable<TicketDto>>>($"/Api/Ticket", new { projectId });
    }

    public async Task<HttpResult<IEnumerable<TicketDto>>?> GetOrgTickets() {
        return await ApiClient.GetAsync<HttpResult<IEnumerable<TicketDto>>>($"/Api/Ticket");
    }

    public async Task<HttpResult<TicketDto>?> GetTicket(Guid ticketId) {
        return await ApiClient.GetAsync<HttpResult<TicketDto>>($"/Api/Ticket/{ticketId}");
    }
    
    public async Task<HttpResult<TicketDto>?> CreateTicket(CreateTicketRequest request) {
        return await ApiClient.PostAsync<HttpResult<TicketDto>>($"/Api/Ticket", request);
    }
    
    public async Task<HttpResult<TicketDto>?> UpdateTicket(UpdateTicketRequest request) {
        return await ApiClient.PutAsync<HttpResult<TicketDto>>($"/Api/Ticket", request);
    }
    
    public async Task<HttpResult<TicketDto>?> DeleteTicket(Guid ticketId) {
        return await ApiClient.DeleteAsync<HttpResult<TicketDto>>($"/Api/Ticket/{ticketId}");
    }
}

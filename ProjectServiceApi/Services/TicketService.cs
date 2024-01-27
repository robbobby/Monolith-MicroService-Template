using AutoMapper;
using Common.Apis.Auth;
using Common.Model;
using Core.Entity.Project;
using ProjectServiceApi.Repository;

namespace ProjectServiceApi.Services;

public class TicketService(TicketRepository _ticketRepository, IMapper _mapper) {
    public async Task<HttpResult<IEnumerable<TicketDto>>> GetAll(Guid orgId) {
        var tickets = _ticketRepository.Ticket.GetAll<TicketDto>()
            .Where(t => t.OrganisationId == orgId).ToArray();
        return new HttpResult<IEnumerable<TicketDto>> {
            Succeeded = ResultType.Success,
            Data = tickets,
        };
    }

    public async Task<HttpResult<TicketDto>> GetById(Guid id, Guid orgId) {
        var ticket = _ticketRepository.Ticket.Get<TicketDto>(id)
            .FirstOrDefault(t => t.OrganisationId == orgId);

        if(ticket == null) {
            return new HttpResult<TicketDto> {
                Succeeded = ResultType.NotFound,
                Errors = new[] { "Ticket not found" },
            };
        }

        return new HttpResult<TicketDto> {
            Succeeded = ResultType.Success,
            Data = ticket,
        };
    }

    public async Task<HttpResult<IEnumerable<TicketDto>>> GetByProjectId(Guid? projectId) {
        var tickets = _ticketRepository.Ticket.GetAll<TicketDto>()
            .Where(t => t.ProjectId == projectId).ToArray();
        return new HttpResult<IEnumerable<TicketDto>> {
            Succeeded = ResultType.Success,
            Data = tickets,
        };
    }

    public async Task<HttpResult<TicketDto>> Create(CreateTicketRequest ticketDto) {
        var ticket = _mapper.Map<TicketEntity>(ticketDto);
        ticket.Id = Guid.NewGuid();
        _ticketRepository.Ticket.Create(ticket);
        return new HttpResult<TicketDto> {
            Succeeded = ResultType.Success,
            Data = _mapper.Map<TicketDto>(ticket),
        };
    }

    public async Task<HttpResult<TicketDto>>
        Update(UpdateTicketRequest ticket, IEnumerable<Guid> allUserProjectClaims) {
        var orgTicket = _ticketRepository.Ticket.Get<TicketDto>(ticket.Id)
            .FirstOrDefault(t => t.OrganisationId == ticket.OrganisationId);
        if(orgTicket == null) {
            return new HttpResult<TicketDto> {
                Succeeded = ResultType.NotFound,
                Errors = new[] { "Ticket not found" },
            };
        }

        if(orgTicket?.ProjectId != null) {
            if(allUserProjectClaims.Any(p => p == orgTicket.ProjectId)) {
                return new HttpResult<TicketDto> {
                    Succeeded = ResultType.Forbidden,
                    Errors = new[] { "You do not have permission to update this ticket" },
                };
            }
        }

        orgTicket = _mapper.Map(ticket, orgTicket);
        var ticketEntity = _mapper.Map<TicketEntity>(orgTicket);
        _ticketRepository.Ticket.Update(ticketEntity);
        return new HttpResult<TicketDto> {
            Succeeded = ResultType.Success,
            Data = orgTicket,
        };
    }

    public async Task<HttpResult<TicketDto>> Delete(Guid id, Guid orgId, IEnumerable<Guid> projectIds) {
        var ticket = _ticketRepository.Ticket.Get<TicketDto>(id)
            .FirstOrDefault(t => t.OrganisationId == orgId);

        if(ticket == null) {
            return new HttpResult<TicketDto> {
                Succeeded = ResultType.NotFound,
                Errors = new[] { "Ticket not found" },
            };
        }

        if(projectIds.Any(p => p == ticket.ProjectId)) {
            return new HttpResult<TicketDto> {
                Succeeded = ResultType.Forbidden,
                Errors = new[] { "You do not have permission to delete this ticket" },
            };
        }

        await _ticketRepository.Ticket.Delete(ticket.Id);
        return new HttpResult<TicketDto> {
            Succeeded = ResultType.Success,
            Data = ticket,
        };
    }
}

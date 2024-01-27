using Common.Model;
using Core.Entity.Identity;
using OrganisationApi.Repository;

namespace OrganisationApi.Service;

public class OrganisationService(OrganisationRepository organisationRepository) {
    public IReadOnlyList<T> GetAll<T>() where T : class {
        return organisationRepository.Organisations.GetAll<T>().ToList();
    }

    public async Task<Guid> Create(string name, Guid userId) {
        var entity = new OrganisationEntity {
            Name = name
        };

        var organisation = await organisationRepository.Organisations.CreateAsync(entity);

        var userOrganisation = new UserOrganisationEntity {
            UserId = userId,
            OrganisationId = organisation.Id,
            Role = UserRole.Owner
        };

        await organisationRepository.UserOrganisations.CreateAsync(userOrganisation);

        return entity.Id;
    }
}

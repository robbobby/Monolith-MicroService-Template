namespace UserApi.Model;

public class UserDto {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public List<OrganisationDto> Organisations { get; set; }
}

namespace UserApi.Model;

public class UserDto {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public List<UnitDto> Units { get; set; }
}
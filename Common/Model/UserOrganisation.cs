using System.Collections.ObjectModel;

namespace Common.Model;

public class UserOrganisation {
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public UserRole Role { get; set; }
    public ObservableCollection<Project> Projects { get; init; } = [];
}

public class Project {
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
}

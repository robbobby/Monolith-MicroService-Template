using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Model;
using Core.Entity.Identity;
using Core.Entity.Interface;

namespace Core.Entity.Project;

[Table("Tickets")]
public class TicketEntity : IEntityId {
    [Key] public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public TicketType Type { get; set; }
    public TicketPriority Priority { get; set; }
    [ForeignKey(nameof(ProjectId))] public ProjectEntity? Project { get; set; }
    public Guid? ProjectId { get; set; }
    [ForeignKey(nameof(AssignedToId))] public UserEntity? AssignedTo { get; set; }
    public Guid? AssignedToId { get; set; }
    [ForeignKey(nameof(OrganisationId))] public OrganisationEntity Organisation { get; set; } = null!;
    public Guid OrganisationId { get; set; }
}
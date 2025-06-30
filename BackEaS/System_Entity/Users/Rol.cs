using System.ComponentModel.DataAnnotations;

namespace System.Entity.Users;

public class Rol
{
    private int _rolId;
    private string _rolName;
    private string _description;
    private bool _active;
    
    [Required]
    public int RolId { get => _rolId; set => _rolId = value; }
    [Required]
    public string RolName { get => _rolName; set => _rolName = value; }
    [Required]
    public string Description { get => _description; set => _description = value; }
    public bool Active { get => _active; set => _active = value; }
}
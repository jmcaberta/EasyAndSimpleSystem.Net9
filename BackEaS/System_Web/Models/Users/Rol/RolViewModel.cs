namespace System.Web.Models.Users.Rol;

public class RolViewModel
{
    private int _rolId;
    private string _rolName;
    private string _description;
    private bool _active;
    
    public int RolId { get => _rolId; set => _rolId = value; }
    public string RolName { get => _rolName; set => _rolName = value; }
    public string Description { get => _description; set => _description = value; }
    public bool Active { get => _active; set => _active = value; }
}
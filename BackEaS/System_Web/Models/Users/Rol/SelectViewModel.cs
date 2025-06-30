namespace System.Web.Models.Users.Rol;

public class SelectViewModel
{
    private int _rolId;
    private string _rolName;
    
    public int RolId { get => _rolId; set => _rolId = value; }
    public string RolName { get => _rolName; set => _rolName = value; }
}
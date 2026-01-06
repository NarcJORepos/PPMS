using DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class User
{
    public int ID { get; set; }
    public int EmployeeID { get; set; }
    public int RoleID { get; set; }  // هذا هو FK يشير إلى Roles.ID

    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsActive { get; set; }

    public virtual Employee? Employee { get; set; }
    public virtual Role? Role { get; set; }
}


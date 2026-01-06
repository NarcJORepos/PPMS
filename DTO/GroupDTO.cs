using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GroupDTO
    {
        public int GroupID { get; set; }

        [Required]
        [StringLength(50)]
        public string GroupName { get; set; } = string.Empty;
    }
}

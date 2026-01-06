using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DAL.Models.Entities
{
    public class MeasurementMethod
    {
        public int MeasurementMethodID { get; set; }

        [Column("MeasurementMethod")]
        public string MeasurementMethodName { get; set; } = String.Empty;
    }
}

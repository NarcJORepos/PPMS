using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Entities
{
    public class MeasurementIndicator
    {
        [Column("Measurement_IndicatorID")]
        public int MeasurementIndicatorID { get; set; }

        [Column("MeasurementIndicator")]
        public string MeasurementIndicatorName { get; set; } = string.Empty;

        public int SubIndicatorID { get; set; }

        // ربط مع Enum الوحدات من Project
       // public Project.enMeasurementUnit MeasurementUnitID { get; set; }
    }
}

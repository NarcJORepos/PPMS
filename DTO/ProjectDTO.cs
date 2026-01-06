namespace DTO
{
    // DTO للعرض
    public class ProjectDTO
    {
        public int ProjectID { get; set; }
        public string ProjectRefNumber { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public int EmployeeID { get; set; }
        public int CoordinatorID { get; set; }
        public int? PillarLeadID { get; set; }
        public int? FundingAgencyID { get; set; }
        public int ProjectDuration { get; set; }
        public decimal Cost { get; set; }
        public byte Status { get; set; }

        public byte FundType { get; set; }
        public int? ProgressPercentage { get; set; }
        public string? AnnualOperationalObjective { get; set; }
        public string? SubStrategicObjective { get; set; }

        public bool IsOngoing { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromDate { get; set; }
        public List<int> ParticipantIDs { get; set; }
    }

    // DTO للإضافة
    public class AddNewProjectDTO
    {
        public string ProjectRefNumber { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public int EmployeeID { get; set; }
        public int CoordinatorID { get; set; }
        public int PillarLeadID { get; set; }
        public int FundingAgencyID { get; set; }
        public int CurrencyID { get; set; }       
        public decimal Budget { get; set; }
        public byte? FundType { get; set; }

        public bool IsOngoing { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromDate { get; set; }

        public string? AnnualOperationalObjective { get; set; }
        public string? SubStrategicObjective { get; set; }

        public bool IsDraft { get; set; }
        public DateTime CreatedDate { get; set; }

        // للعرض في الواجهة
        public IEnumerable<MainPillarDTO> MainPillars { get; set; } = new List<MainPillarDTO>();

        public string MainGoal { get; set; }
        
        // Operational Objectives (MANY)
        public List<string> Objectives { get; set; } = new();

        public List<int> ParticipantIDs { get; set; } = new();


        // لاحقاً يمكن إضافة Measurement Indicators
        // public List<int> SelectedMeasurementIndicators { get; set; } = new();
    }

    // DTO للقوائم dropdown
    public class EmployeeDropdownDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
    }    

    public class FundingAgencyDropdownDTO
    {
        public int FundTypeID { get; set; }
        public int FundingAgencyID { get; set; }
        public string FundingAgencyName { get; set; } = string.Empty;
    }

    public class MainPillarDTO
    {
        public int MainPillarID { get; set; }
        public string MainPillarName { get; set; } = string.Empty;
    }

    public class SubKpiDTO
    {
        public int SubIndicatorID { get; set; }
        public string SubIndicatorName { get; set; } = string.Empty;
        public int MainPillarID { get; set; }
    }

    public class MeasurementMethodDTO
    {
        public int MeasurementMethodID { get; set; }
        public string MeasurementMethod { get; set; } = string.Empty;
    }

    public class ProjectObjectiveDTO
    {
        public string MainGoal { get; set; }
        public string Objective { get; set; }
    }    

    /*public class MeasurementIndicatorsDTO
    {
        public int Measurement_IndicatorID { get; set; }
        public string MeasurementIndicator { get; set; } = string.Empty;
        public int SubIndicatorID { get; set; }
    }*/
}

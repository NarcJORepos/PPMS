using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models.Entities;

public class Project
{   
    
    public enum enCurrency { JOD = 1 , EUR = 2, USD = 3 };
    public enCurrency Currency { get; set; }

    public enum enFundType { Internal = 1, External = 2 };

    public enFundType FundType { get; set; }

    public enum enMeasurementUnit
    {
        Percentage = 1,
        Number = 2,
        Currency = 3,
        Days = 4,
        Months = 5,
        Years = 6
    }
   
    public int ProjectID { get; set; }

    // الموظف الذي أنشأ المشروع
    public int EmployeeID { get; set; } //loggedIn user

    public string ProjectRefNumber { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;

    // المنسق
    public int CoordinatorID { get; set; }

    // رئيس المحور (اختياري)
    public int PillarLeadID { get; set; }
    public int FundingAgencyID { get; set; }   
    public decimal Budget { get; set; }  
    
    
    public DateTime CreatedDate { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public bool IsOngoing { get; set; }
    public bool IsDraft { get; set; }

    public string? SubStrategicObjective { get; set; }

    public string? AnnualOperationalObjective { get; set; }
    
    // Navigation Properties
    public virtual Employee? Employee { get; set; } // الموظف الذي أنشأ المشروع
    public virtual Employee? CoordinatorInfo { get; set; } // المنسق
    public virtual Employee? PillarLeadInfo { get; set; } // رئيس المحور
    public virtual FundingAgency? FundingAgency { get; set; }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using DAL.Data;
using DAL.Models.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace BL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDBContext _context;

        public EmployeeService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync()
        {
            try
            {
                return await _context.Employees
                    .Include(emp => emp.Directorate)
                    .Include(emp => emp.Group)
                    .OrderBy(emp => emp.EmployeeName)
                    .Select(emp => new EmployeeDTO
                    {
                        ID = emp.EmployeeId,
                        Name = emp.EmployeeName,
                        Position = emp.Position,
                        DirectorateID = emp.DirectorateId,
                        DirectorateName = emp.Directorate != null ? emp.Directorate.DirectorateName : "",
                        GroupID = emp.GroupId,
                        GroupName = emp.Group != null ? emp.Group.GroupName : "",
                        Gendor = (EmployeeDTO.enGendor)emp.Gendor
                    })

                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading employees: {ex.Message}");
                throw; // rethrow to see full error in dev mode
            }
        }

        public async Task<EmployeeDTO?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null) return null;

            return new EmployeeDTO
            {
                ID = employee.EmployeeId,
                Name = employee.EmployeeName,
                DirectorateID = employee.DirectorateId
            };
        }

        public async Task<int> AddNewEmployeeAsync(EmployeeDTO dto)
        {
            var newEmployee = new Employee
            {
                EmployeeName = dto.Name,
                DirectorateId = dto.DirectorateID,   // ✅ اربط مع الـ FK
                GroupId = dto.GroupID
            };

            _context.Employees.Add(newEmployee);
            return await _context.SaveChangesAsync();
        }

    }
}

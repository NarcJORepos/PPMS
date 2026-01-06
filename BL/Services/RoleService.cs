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
    public class RoleService : IRoleService
    {
        ApplicationDBContext _dbContext;

        public RoleService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<RoleDTO>> GetPagesRolesAsync()
        {
            try
            {
                return await _dbContext.Roles                   
                    .Include(rp => rp.Page)
                    .Include(rp => rp.Group)
                    .Select(rp => new RoleDTO
                    {                                        
                        PageID = rp.PageID,
                        PageName = rp.Page.PageName,
                        GroupID = rp.GroupID,
                        GroupName = rp.Group.GroupName,
                        CanAdd = rp.CanAdd,
                        CanEdit = rp.CanEdit,
                        CanDelete = rp.CanDelete,
                        CanView = rp.CanView
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading role pages: {ex.Message}");
                throw;
            }
        }

        //public async Task<IEnumerable<RolePageDTO>> GetPagesRolesAsync()
        //{
        //    try
        //    {
        //        return await _dbContext.RolePages
        //            .Include(rp => rp.Role)
        //            .Include(rp => rp.Page)
        //            .Include(rp => rp.Group)
        //            .Select(rp => new RolePageDTO
        //            {
        //                RoleID = rp.RoleID,
        //                RoleName = rp.Role.RoleName,
        //                PageID = rp.PageID,
        //                PageName = rp.Page.PageName,
        //                GroupID = rp.GroupID,
        //                GroupName = rp.Group.GroupName
        //            })
        //            .ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error loading role pages: {ex.Message}");
        //        throw;
        //    }
        //}

        //public async Task<int> AddRolesToGroupsAsync(RoleDTO dto)
        //{
        //   /* var role = await _dbContext.Roles.FindAsync(dto.RoleID);
        //    if (role == null) return 0;

        //    role.GroupID = dto.GroupID;
        //    return await _dbContext.SaveChangesAsync();*/
        //}


        //public async Task<int> AddRolesToGroupsAsync(RoleDTO dto)
        //{
        //    var newRole = new Role
        //    {
        //        RoleName = dto.RoleName,
        //        RoleID = dto.RoleID,   // ✅ اربط مع الـ FK
        //        GroupId = dto.GroupID
        //    };

        //    _dbContext.Roles.Add(newRole);
        //    return await _dbContext.SaveChangesAsync();
        //}
    }
}


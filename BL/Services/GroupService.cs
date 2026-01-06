using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Models.Entities;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BL.Interfaces;
using DAL.Data;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace BL.Services
{
    public class GroupService : IGroupService
    {
        private readonly ApplicationDBContext _context;      

        public GroupService(ApplicationDBContext context)
        {
           _context = context;
        }
        public async Task<int> AddNewGroupAsync(GroupDTO groupDTO)
        {
            var newGroup = new DAL.Models.Entities.Group
            {
                GroupName = groupDTO.GroupName,
            };

            _context.Groups.Add(newGroup);

            return await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<GroupDTO>> GetAllGroupsAsync()
        {
            return await _context.Groups.Select(
                group => new GroupDTO {
                    GroupID = group.GroupID,
                    GroupName = group.GroupName,
            }).ToListAsync();
        }
    }
}

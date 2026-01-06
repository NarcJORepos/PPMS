using BL.Interfaces;
using DAL.Data;
using DAL.Models.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;


namespace BL.Services
{
    public class PageService : IPageService
    {
        private readonly ApplicationDBContext _dbContext;

        public PageService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PageDTO>> GetAllPagesAsync()
        {
            return await _dbContext.Pages
                .Select(p => new PageDTO
                {
                    PageID = p.PageID,
                    PageName = p.PageName
                }).ToListAsync();
        }

        public async Task<PageDTO> GetPageByIdAsync(int pageId)
        {
            var page = await _dbContext.Pages.FindAsync(pageId);
            if (page == null) return null!;

            return new PageDTO
            {
                PageID = page.PageID,
                PageName = page.PageName
            };
        }

        public async Task<int> AddPageAsync(PageDTO page)
        {
            var newPage = new Page
            {
                PageName = page.PageName
            };

            _dbContext.Pages.Add(newPage);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdatePageAsync(PageDTO page)
        {
            var existing = await _dbContext.Pages.FindAsync(page.PageID);
            if (existing == null) return 0;

            existing.PageName = page.PageName;
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeletePageAsync(int pageId)
        {
            var page = await _dbContext.Pages.FindAsync(pageId);
            if (page == null) return 0;

            _dbContext.Pages.Remove(page);
            return await _dbContext.SaveChangesAsync();
        }
    }
}

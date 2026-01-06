using DTO;

namespace BL.Interfaces
{
    public interface IPageService
    {
        Task<List<PageDTO>> GetAllPagesAsync();
        Task<PageDTO> GetPageByIdAsync(int pageId);
        Task<int> AddPageAsync(PageDTO page);
        Task<int> UpdatePageAsync(PageDTO page);
        Task<int> DeletePageAsync(int pageId);
    }
}

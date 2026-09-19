using CoreDesk.API.Models.Entities;

namespace CoreDesk.API.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<bool> HasAssociatedTicketsAsync(int categoryId);
    }
}
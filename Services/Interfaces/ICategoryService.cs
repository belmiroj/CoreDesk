using CoreDesk.API.DTOs;

namespace CoreDesk.API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse> GetByIdAsync(int id);
        Task<CategoryResponse> CreateAsync(CreateCategoryRequest request);
        Task UpdateAsync(int id, UpdateCategoryRequest request);
        Task DeleteAsync(int id);
    }
}
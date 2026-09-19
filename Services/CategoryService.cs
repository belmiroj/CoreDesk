using CoreDesk.API.DTOs;
using CoreDesk.API.Models.Entities;
using CoreDesk.API.Repositories.Interfaces;
using CoreDesk.API.Services.Interfaces;

namespace CoreDesk.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(c => new CategoryResponse(c.Id, c.Name));
        }

        public async Task<CategoryResponse> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id) 
                ?? throw new KeyNotFoundException("Category not found.");

            return new CategoryResponse(category.Id, category.Name);
        }

        public async Task<CategoryResponse> CreateAsync(CreateCategoryRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Category name is required.");

            var category = new Category { Name = request.Name };
            var created = await _repository.AddAsync(category);

            return new CategoryResponse(created.Id, created.Name);
        }

        public async Task UpdateAsync(int id, UpdateCategoryRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Category name is required.");

            var category = await _repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Category not found.");

            category.Name = request.Name;
            await _repository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Category not found.");

            if (await _repository.HasAssociatedTicketsAsync(id))
                throw new InvalidOperationException("Cannot delete a category linked to existing tickets.");

            await _repository.DeleteAsync(category);
        }
    }
}
using Class_Programmation.DAL.Models;

namespace Class_Programmation.Repository.iRepository
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>>GetCategoriesAsync();//me retorna una lista de categoria
        Task<Category> GetCategoryAsync(int id);//me retorna una categoria por su id

        Task<bool> CategoryExistByidAsync(int id);

        Task<bool> CategoryExistByNameAsync(string name);

        Task<bool> CreateCategoryAsync(Category category);

        Task<bool> updateCategoryAsync(Category category);

        Task<bool> DeleteCategoryAsync(int id);
    }
}

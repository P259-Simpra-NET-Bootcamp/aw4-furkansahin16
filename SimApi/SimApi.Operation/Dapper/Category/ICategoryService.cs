using SimApi.Base;
using SimApi.Schema;

namespace SimApi.Operation.Category
{
    public interface ICategoryService
    {
        public ApiResponse Delete(int Id);

        public ApiResponse<List<CategoryResponse>> GetAll();

        public ApiResponse<CategoryResponse> GetById(int id);

        public ApiResponse Insert(CategoryRequest request);

        public ApiResponse Update(int Id, CategoryRequest request);
    }
}

using SimApi.Data.Context;
using SimApi.Data;

namespace SimApi.Data.Repository;

public class CategoryRepository : DapperRepository<Category>, ICategoryRepository
{
    public CategoryRepository(SimDapperDbContext context) : base(context)
    {

    }
}

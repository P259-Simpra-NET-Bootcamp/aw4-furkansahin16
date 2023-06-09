using AutoMapper;
using Serilog;
using SimApi.Base;
using SimApi.Data.Repository;
using SimApi.Data.Uow;
using SimApi.Schema;

namespace SimApi.Operation.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper mapper;
        private readonly IDapperRepository<Data.Category> repository;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = unitOfWork.DapperRepository<Data.Category>();
        }
        public virtual ApiResponse Delete(int Id)
        {
            try
            {
                var entity = repository.GetById(Id);
                if (entity is null)
                {
                    Log.Warning("Record not found for Id " + Id);
                    return new ApiResponse("Record not found");
                }

                repository.DeleteById(Id);
                return new ApiResponse();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Delete Exception");
                return new ApiResponse(ex.Message);
            }
        }

        public ApiResponse Insert(CategoryRequest request)
        {
            try
            {
                var category = mapper.Map<CategoryRequest, Data.Category>(request);
                category.CreatedAt = DateTime.UtcNow;
                category.CreatedBy = "sim@sim.com";

                repository.Insert(category);
                return new ApiResponse();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Insert Exception");
                return new ApiResponse(ex.Message);
            }
        }


        public ApiResponse Update(int Id, CategoryRequest request)
        {
            try
            {
                var entity = mapper.Map<CategoryRequest, Data.Category>(request);

                var exist = repository.GetById(Id);
                if (exist is null)
                {
                    Log.Warning("Record not found for Id " + Id);
                    return new ApiResponse("Record not found");
                }

                entity.Id = Id;
                entity.UpdatedAt = DateTime.UtcNow;

                repository.Update(entity);
                return new ApiResponse();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Update Exception");
                return new ApiResponse(ex.Message);
            }
        }

        ApiResponse<List<CategoryResponse>> ICategoryService.GetAll()
        {
            try
            {
                var entityList = repository.GetAll();
                var mapped = mapper.Map<List<Data.Category>, List<CategoryResponse>>(entityList);
                return new ApiResponse<List<CategoryResponse>>(mapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GetAll Exception");
                return new ApiResponse<List<CategoryResponse>>(ex.Message);
            }
        }

        ApiResponse<CategoryResponse> ICategoryService.GetById(int id)
        {
            try
            {
                var entity = repository.GetById(id);
                if (entity is null)
                {
                    Log.Warning("Record not found for Id " + id);
                    return new ApiResponse<CategoryResponse>("Record not found");
                }

                var mapped = mapper.Map<Data.Category, CategoryResponse>(entity);
                return new ApiResponse<CategoryResponse>(mapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GetById Exception");
                return new ApiResponse<CategoryResponse>(ex.Message);
            }
        }
    }
}

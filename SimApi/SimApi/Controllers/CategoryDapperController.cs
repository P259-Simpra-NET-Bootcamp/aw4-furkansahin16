using Microsoft.AspNetCore.Mvc;
using SimApi.Base;
using SimApi.Operation.Category;
using SimApi.Schema;

namespace SimApi.Service.Controllers;


[EnableMiddlewareLogger]
[ResponseGuid]
[Route("simapi/v1/[controller]")]
[ApiController]
public class CategoryDapperController : ControllerBase
{
    private ICategoryService categoryService;
    public CategoryDapperController(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    [HttpGet]
    public ApiResponse<List<CategoryResponse>> GetAll() => categoryService.GetAll();

    [HttpGet("{id}")]
    public ApiResponse<CategoryResponse> GetById(int id) => categoryService.GetById(id);

    [HttpPost]
    public ApiResponse Post([FromBody] CategoryRequest request) => categoryService.Insert(request);

    [HttpPut("{id}")]
    public ApiResponse Put(int id, [FromBody] CategoryRequest request) => categoryService.Update(id, request);

    [HttpDelete("{id}")]
    public ApiResponse Delete(int id) => categoryService.Delete(id);
}

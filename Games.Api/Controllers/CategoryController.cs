using Games.Api.Models.Category;
using Games.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Games.Api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new System.ArgumentNullException(nameof(categoryRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IList<Category>>> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAll(cancellationToken);
            return Ok(categories.Select(x=> Category.Create(x)));
        }
    }
}

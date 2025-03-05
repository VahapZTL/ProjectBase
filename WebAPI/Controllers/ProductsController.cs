using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductBusiness productBusiness;

        public ProductsController(IProductBusiness productBusiness)
        {
            this.productBusiness = productBusiness;
        }

        [HttpGet("GetListByCategory")]
        public IActionResult GetListByCategory(int categoryId)
        {
            var result = productBusiness.GetListByCategory(categoryId);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
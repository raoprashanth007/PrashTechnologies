using Microsoft.AspNetCore.Mvc;
using PrashTechnologies.Api.Filters;
using PrashTechnologies.Application.Interfaces;
using PrashTechnologies.Core.Entities;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PrashTechnologies.Api.Controllers
{
    /// <summary>
    /// Product API's 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// GET: api/Product?top=5
        /// API fetches list of products based on the given top count . Product cost is dispalyed in default currency 'USD' for all the products.
        /// If required, an optional parameter can be specified for desired currency in which the cost should be calculated. This calculation is based on the live currency exchange rates.
        /// </summary>
        /// <param name="top">number of rows to be dispalyed</param>
        /// <param name="exchangeCode">desired currency. suported currencies are: USD, GBP, CAD, EUR</param>
        /// <returns>list of products</returns>
        [ServiceFilter(typeof(DataTransformerFilter))]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int top = 5, [FromQuery] string exchangeCode = null)
        {
            var data = await unitOfWork.Products.GetTopAsync(top);
            return Ok(data);
        }

        /// <summary>
        /// GET: api/Product/1?exchangeCode='CAD'
        /// API fetches a product based on the given product id. Product cost is dispalyed in default currency 'USD.
        /// If required, an optional parameter can be specified for desired currency in which the cost should be calculated. This calculation is based on the live currency exchange rates.
        /// </summary>
        /// <param name="id">product id</param>
        /// <param name="exchangeCode">desired currency. suported currencies are: USD, GBP, CAD, EUR</param>
        /// <returns>a single product</returns>
        [ServiceFilter(typeof(DataTransformerFilter))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] string exchangeCode = null)
        {
            var data = await unitOfWork.Products.GetByIdAsync(id);
            if (data == null) return Ok();

            // click increment - this can go as action filter
            var stats = new Stats() { ProductCode = data.Code };
            var click = await unitOfWork.Stats.AddAsync(stats);

            return Ok(data);
        }

        /// <summary>
        /// POST: api/Product
        /// API to post the product details to server
        /// </summary>
        /// <param name="product">Product details</param>
        /// <returns>Success/failure indicator</returns>        
        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var data = await unitOfWork.Products.AddAsync(product);
            return Ok(data);
        }

        /// <summary>
        /// DELETE: api/Product/1
        /// </summary>
        /// <param name="id">product id to be deleted</param>
        /// <returns>Success/failure indicator</returns>      
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.Products.DeleteAsync(id);
            return Ok(data);
        }
    }
}

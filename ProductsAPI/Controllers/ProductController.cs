using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Data;
using ProductsAPI.Data.Dtos;
using ProductsAPI.Data.Repository;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/products/[controller]")]
    public class ProductsController : ControllerBase
    {
        private ProductContext _context;
        private IMapper _mapper;
        private IProductRepository _repository;

        public ProductsController(ProductContext context, IMapper mapper, ProductRepository repository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddProduct([FromBody] CreateProductDTO productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            _context.Products.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ReturnProductByCode),
                new { code = product.Id },
                product);
        }

        private void CreatedAtAction()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IEnumerable<Product> ListProducts([FromQuery]int skip,
        [FromQuery]int take)
        {
            return _context.Products.Skip(skip).Take(take);
        }

        [HttpGet("{code}")]
        public IActionResult ReturnProductByCode(int code)
        {
            var product = _context.Products.FirstOrDefault(product => product.Id == code);
            if (product == null) return NotFound();
            return Ok(product);
        }
    }
}
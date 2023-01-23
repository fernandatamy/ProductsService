using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Data.Dtos;
using ProductsAPI.Data.Repository;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/products/[controller]")]
    public class ProductsController : ControllerBase
    {
        private IMapper _mapper;
        private IProductRepository _repository;

        public ProductsController(IMapper mapper,IProductRepository repository
            )
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<ReadProductDTO> AddProduct([FromBody] CreateProductDTO productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            _repository.CreateProduct(product);
            _repository.SaveChanges();

            var productReadDto = _mapper.Map<ReadProductDTO>(product);
            return CreatedAtRoute(nameof(GetProductById), new { productReadDto.Id }, productReadDto);
        }

        [HttpGet]
        public IEnumerable<Product> ListProducts([FromQuery]int skip,
        [FromQuery]int take)
        {
            return _repository.GetProducts().Skip(skip).Take(take);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult<ReadProductDTO> GetProductById(int id)
        {
            var product = _repository.GetProductById(id);
            if (product == null) return NotFound();
            return Ok(_mapper.Map<ReadProductDTO>(product));
        }
    }
}
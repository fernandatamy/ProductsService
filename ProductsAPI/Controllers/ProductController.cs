using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Data;
using ProductsAPI.Data.Dtos;
using ProductsAPI.Data.Repository;
using ProductsAPI.RabbitMqClient;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/products/[controller]")]
    public class ProductsController : ControllerBase
    {
        private IMapper _mapper;
        private IProductRepository _repository;
        //private IRabbitMqClient _rabbitMqClient;

        public ProductsController(IMapper mapper,IProductRepository repository
            //,IRabbitMqClient rabbitMqClient
            )
        {
            _mapper = mapper;
            _repository = repository;
            //_rabbitMqClient = rabbitMqClient;
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
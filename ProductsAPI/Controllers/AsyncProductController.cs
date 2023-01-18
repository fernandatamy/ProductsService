using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Data.Dtos;
using ProductsAPI.Data.Repository;
using ProductsAPI.RabbitMqClient;
using ProductsAPI.ServiceHttpClient;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/products/async/[controller]")]
    public class AsyncProductController : ControllerBase
    {
        private IMapper _mapper;
        private IRabbitMqClient _rabbitMqClient;
        private IProductRepository _repository;
        private IProductServiceHttpClient _productServiceHttpClient;

        public AsyncProductController(
            IProductRepository repository,
            IMapper mapper, IProductServiceHttpClient productServiceHttpClient, IRabbitMqClient rabbitMqClient)
        {
            _repository = repository;
            _mapper = mapper;
            _productServiceHttpClient = productServiceHttpClient;
            _rabbitMqClient = rabbitMqClient;
        }

        [HttpPost]
        public ActionResult<ReadProductDTO> CreateAsyncProduct(CreateProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            _repository.CreateProduct(product);
            _repository.SaveChanges();

            var readProductDTO = _mapper.Map<ReadProductDTO>(product);
            _productServiceHttpClient.SendOrderToProductService(readProductDTO);


            return CreatedAtRoute(nameof(GetProductAsyncById), new { readProductDTO.Id }, readProductDTO);
        }

        [HttpGet("{id}", Name = "GetProductAsyncById")]
        public ActionResult<ReadProductDTO> GetProductAsyncById(int id)
        {
            var product = _repository.GetProductById(id);
            if (product != null)
            {
                return Ok(_mapper.Map<ReadProductDTO>(product));
            }

            return NotFound();
        }
    }
}

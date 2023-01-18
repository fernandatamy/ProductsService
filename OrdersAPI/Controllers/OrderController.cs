using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Data;
using OrdersAPI.Data.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using OrdersAPI.ServiceHttpClient;

namespace OrdersAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IProductServiceHttpClient _productServiceHttpClient;
        private readonly IOrderRepository _repository;

        public OrderController(
            IMapper mapper, 
            IProductServiceHttpClient productServiceHttpClient, 
            IOrderRepository repository)
        {
            _productServiceHttpClient = productServiceHttpClient;
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadOrderDTO>> GetAllOrders()
        {

            var orders = _repository.GetOrders();

            return Ok(_mapper.Map<IEnumerable<ReadOrderDTO>>(orders));
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        public ActionResult<ReadOrderDTO> GetOrderById(int id)
        {
            var order = _repository.GetOrderById(id);
            if (order != null)
            {
                return Ok(_mapper.Map<ReadOrderDTO>(order));
            }

            return NotFound();
        }


        [HttpPost]
        public async Task<ActionResult<ReadOrderDTO>> CreateOrder(CreateOrderDTO createOrderDTO)
        {
            var order = _mapper.Map<Order>(createOrderDTO);
            _repository.CreateOrder(order);
            _repository.SaveChanges();

            var readOrderDTO = _mapper.Map<ReadOrderDTO>(order);
            _productServiceHttpClient.SendOrderToProductService(readOrderDTO);


            return CreatedAtRoute(nameof(GetOrderById), new { readOrderDTO.Id }, readOrderDTO);
        }
    }    
}
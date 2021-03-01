using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using pizza_server.DTOs;
using pizza_server.Models;
using pizza_server.Services.OrderService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pizza_server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<GetOrderDTO>> AddOrder([FromBody] AddOrderDTO addOrder)
        {
            return Ok(await _orderService.AddOrder(addOrder));
        }

        [HttpGet]
        public async Task<ActionResult<List<GetOrderDTO>>> GetAllOrders()
        {
            return Ok(await _orderService.GetAllOrders());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetOrderDTO>>> GetOrderById(int id)
        {
            ServiceResponse<GetOrderDTO> response = await _orderService.GetOrderById(id);

            if(response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpPost("ChangeOrderState")]
        public async Task<ActionResult<ServiceResponse<GetOrderDTO>>> ChangeOrderState(int id, OrderState orderState)
        {
            ServiceResponse<GetOrderDTO> response = await _orderService.ChangeOrderState(id, orderState);

            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost("ChangeDeliveryState")]
        public async Task<ActionResult<ServiceResponse<GetOrderDTO>>> ChangeDeliveryState(int id, bool isDelivered)
        {
            ServiceResponse<GetOrderDTO> response = await _orderService.ChangeDeliveryState(id, isDelivered);

            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}

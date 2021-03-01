using pizza_server.DTOs;
using pizza_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<GetOrderDTO>> GetOrderById(int id);
        Task<ServiceResponse<List<GetOrderDTO>>> GetAllOrders();
        Task<ServiceResponse<GetOrderDTO>> AddOrder(AddOrderDTO order);
        Task<ServiceResponse<GetOrderDTO>> ChangeOrderState(int id, OrderState orderState);
        Task<ServiceResponse<GetOrderDTO>> ChangeDeliveryState(int id, bool isDelivered);
    }
}

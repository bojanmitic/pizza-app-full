using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using pizza_server.Data;
using pizza_server.DTOs;
using pizza_server.Helpers;
using pizza_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Order with meals and all other properties</returns>
        public async Task<ServiceResponse<GetOrderDTO>> AddOrder(AddOrderDTO order)
        {
            ServiceResponse<GetOrderDTO> response = new ServiceResponse<GetOrderDTO>();


            try
            {
                var userId = GetUser.GetUserId(_httpContextAccessor);
                var userDb = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                var orderDb = _mapper.Map<Order>(order);
                orderDb.User = userDb;

                await _context.Orders.AddAsync(orderDb);
                await _context.SaveChangesAsync();

                var orderResponse = await _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(x => x.Meal)
                    .FirstOrDefaultAsync(o => o.Id == orderDb.Id);

                response.Data = _mapper.Map<GetOrderDTO>(orderResponse);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Gets all orders for current user
        /// </summary>
        /// <returns>List of all orders for current user</returns>
        public async Task<ServiceResponse<List<GetOrderDTO>>> GetAllOrders()
        {
            ServiceResponse<List<GetOrderDTO>> response = new ServiceResponse<List<GetOrderDTO>>();

            try
            {
                var userId = GetUser.GetUserId(_httpContextAccessor);
                var userOrders = await _context.Orders
                    .Where(o => o.UserId == userId)
                    .Include(o => o.OrderItems)
                    .ThenInclude(x => x.Meal)
                    .Select(x => _mapper.Map<GetOrderDTO>(x))
                    .ToListAsync();

                response.Data = userOrders;


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Get specific order by id for current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Specific order by id for current user</returns>
        public async Task<ServiceResponse<GetOrderDTO>> GetOrderById(int id)
        {
            ServiceResponse<GetOrderDTO> response = new ServiceResponse<GetOrderDTO>();

            try
            {
                var userId = GetUser.GetUserId(_httpContextAccessor);
                var userOrders = await _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(x => x.Meal)
                    .FirstAsync(o => o.UserId == userId && o.Id == id);

                response.Data = _mapper.Map<GetOrderDTO>(userOrders);


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Changing order state, if it starts to prepare or is it ready to deliver
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderState"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<GetOrderDTO>> ChangeOrderState(int id, OrderState orderState)
        {
            ServiceResponse<GetOrderDTO> response = new ServiceResponse<GetOrderDTO>();
            try
            {
                var dbOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
                var userRole = GetUser.GetUserRole(_httpContextAccessor);

                if (dbOrder == null)
                {
                    response.Success = false;
                    response.Message = "Order not found.";
                    return response;
                }

                if (userRole == "Admin" || userRole == "Operator")
                {
                    dbOrder.OrderState = orderState;
                    response.Data = _mapper.Map<GetOrderDTO>(dbOrder);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not Authorized.";
                    return response;
                }


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetOrderDTO>> ChangeDeliveryState(int id, bool isDelivered)
        {
            ServiceResponse<GetOrderDTO> response = new ServiceResponse<GetOrderDTO>();
            try
            {
                var dbOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
                var userRole = GetUser.GetUserRole(_httpContextAccessor);

                if (dbOrder == null)
                {
                    response.Success = false;
                    response.Message = "Order not found.";
                    return response;
                }

                if (userRole == "Admin" || userRole == "Operator")
                {
                    dbOrder.IsDelivered = isDelivered;
                    response.Data = _mapper.Map<GetOrderDTO>(dbOrder);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not Authorized.";
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}

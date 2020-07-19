using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Создать заказ
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {
            var some = ModelState.IsValid;
            var orderId = _orderService.CreateOrder(order);
            return Ok(orderId);
        }

        /// <summary>
        /// Обновить заказ
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateOrder(Order order)
        {
            _orderService.UpdateOrder(order);
            return Ok();
        }

        /// <summary>
        /// Отменить заказ
        /// </summary>
        /// <param name="orderId">Номер заказа</param>
        /// <returns></returns>
        [HttpDelete("{orderId}")]
        public IActionResult CancelOrder(int orderId)
        {
            var reuslt = _orderService.CancelOrder(orderId);
            return Ok();
        }

        /// <summary>
        /// Получить информацию о заказе
        /// </summary>
        /// <param name="orderId">Номер заказа</param>
        /// <returns></returns>
        [HttpGet("{orderId}")]
        public IActionResult GetOrder(int orderId)
        {
            return Ok(_orderService.GetOrder(orderId));
        }
    }
}
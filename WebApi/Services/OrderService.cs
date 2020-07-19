using System.Collections.Generic;
using System.Linq;
using WebApi.Exceptions;
using WebApi.Models;

namespace WebApi.Services
{
    public class OrderService
    {
        private readonly List<Order> _orders;
        private readonly PostamatService _postamatService;

        public OrderService(List<Order> orders, PostamatService postamatService)
        {
            _orders = orders;
            _postamatService = postamatService;
        }

        public int CreateOrder(Order order)
        {
            var postamat = _postamatService.GetPostamat(order.PostamatId);
            if (postamat == null)
                throw new BadRequestException(
                    $"Postamat with id {order.PostamatId} is not found");
            if (!postamat.IsAlive)
                throw new ForbiddenException($"Postamat with id {order.PostamatId} is currently closed");

            var orderId = _orders.Count == 0
                ? 1
                : _orders.Max(x => x.Id) + 1;
            order.Id = orderId;
            order.Status = OrderStatusCode.Registred;
            _orders.Add(order);
            return orderId;
        }

        public bool CancelOrder(int orderId)
        {
            var order = _orders.FirstOrDefault(x => x.Id == orderId);
            if (order == null)
                throw new NotFoundException($"Order with ID = {orderId} not found");
            _orders.Remove(order);
            return true;
        }

        public void UpdateOrder(Order order)
        {
            var dbOrder = _orders.FirstOrDefault(x => x.Id == order.Id);
            if (dbOrder == null)
                throw new NotFoundException($"Order with ID = {order.Id} not found");
            if (order.Status != dbOrder.Status)
                throw new BadRequestException("Cannot change current status of order");
            var postamat = _postamatService.GetPostamat(order.PostamatId);
            if (postamat == null)
                throw new BadRequestException(
                    $"Postamat with id {order.PostamatId} is not found");
            if (!postamat.IsAlive)
                throw new ForbiddenException($"Postamat with id {order.PostamatId} is currently closed");
            dbOrder.Goods = order.Goods;
            dbOrder.Amount = order.Amount;
            dbOrder.RecipientName = order.RecipientName;
            dbOrder.RecipientPhone = order.RecipientPhone;
            dbOrder.PostamatId = order.PostamatId;
        }

        public object GetOrder(int orderId)
        {
            var order = _orders.FirstOrDefault(x => x.Id == orderId);
            if (order == null)
                throw new NotFoundException($"Order with id {orderId} not found");
            return order;
        }
    }
}
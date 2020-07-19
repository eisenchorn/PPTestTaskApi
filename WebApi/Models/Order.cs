using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.ValidationAttributes;

namespace WebApi.Models
{
    public class Order
    {
        /// <summary>
        /// Номер заказа
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Статус заказа
        /// </summary>
        public OrderStatusCode Status { get; set; }
        /// <summary>
        /// Список товаров
        /// </summary>
        [MaxLength(10)]
        public List<string> Goods { get; set; }
        /// <summary>
        /// Сумма заказа
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Номер постамата
        /// </summary>
        [Required]
        public string PostamatId { get; set; }
        
        /// <summary>
        /// Номер телефона получателя
        /// </summary>
        [Required]
        [PhoneValidation(@"^\+7\d{3}-\d{3}-\d{2}-\d{2}$")]
        public string RecipientPhone { get; set; }
        /// <summary>
        /// Ф.И.О. получателя
        /// </summary>
        [Required]
        public string RecipientName { get; set; }
    }
}
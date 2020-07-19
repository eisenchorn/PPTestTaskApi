using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Postamat
    {
        /// <summary>
        /// Номер постамата
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Активен
        /// </summary>
        public bool IsAlive { get; set; }
    }
}
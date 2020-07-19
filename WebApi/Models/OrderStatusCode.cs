namespace WebApi.Models
{
    public enum OrderStatusCode
    {
        Registred = 1,
        AcceptedByWarehouse = 2,
        AcceptedByCourier = 3,
        DeliveredToPostamat = 4,
        DeliveredToRecipient = 5
    }
}
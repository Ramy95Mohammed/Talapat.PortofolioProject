namespace Talapat.Api.DTOS
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public string? BuyerEmail { get; set; }
        public int deliveryMethodId { get; set; }
        public AddressDto ShipToAddress { get; set; }
    }
}

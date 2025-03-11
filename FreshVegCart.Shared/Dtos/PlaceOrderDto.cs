namespace FreshVegCart.Shared.Dtos;

public record PlaceOrderDto (int UserAddressId , string Address, string AddressName , OrderItemSaveDto[] Items) ;
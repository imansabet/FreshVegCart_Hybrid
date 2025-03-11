using FreshVegCart.Api.Data;
using FreshVegCart.Api.Data.Entities;
using FreshVegCart.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FreshVegCart.Api.Services;

public class OrderService
{
    private readonly DataContext _context;

    public OrderService(DataContext context)
    {
        _context = context;
    }
    public async Task<ApiResult> PlaceOrderAsync(PlaceOrderDto dto , int userId) 
    {
        if (dto.Items.Length == 0)
            return ApiResult.Fail("Order must Contain Items");
        
        var productIds = dto.Items.Select(i => i.ProductId).ToHashSet();

        var products = await _context
            .Products
            .Where(p => productIds
            .Contains(p.Id))
            .ToDictionaryAsync(p => p.Id);
        
        if(products.Count == dto.Items.Length) 
        {
            return ApiResult.Fail("Some Product Is Not Available . ");
        }

        var orderItems = dto.Items
            .Select(i => new OrderItem
        {
            ProductId = i.ProductId,
            Quantity = i.Quantity,
            ProductImageUrl = products[i.ProductId].ImageUrl,
            ProductName = products[i.ProductId].Name,
            Unit = products[i.ProductId].Unit
        }).ToArray();
        
        var now = DateTime.UtcNow;
        var order = new Order
        {
            Date = now,
            UserId = userId,
            UserAddressId = dto.UserAddressId,
            Address = dto.Address,
            AddressName = dto.AddressName,
            TotalItems = dto.Items.Length,
            TotalAmount = orderItems.Sum(oi => oi.Quantity * oi.ProductPrice),
            Items = orderItems
        };
        try 
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return ApiResult.Success();
        }
        catch (Exception ex) 
        {
            return ApiResult.Fail(ex.Message);
        }

    }
    public async Task<OrderDto[]> GetUserOrdersAsync(int userId, int startIndex, int pageSize) => await _context
        .Orders
        .AsNoTracking()
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.Id)
            .Skip(startIndex).Take(pageSize)
            .Select(a => new OrderDto
            {
               Address = a.Address,
               AddressName = a.AddressName,
               Date = a.Date,
               Id = a.Id,
               Remarks = a.Remarks,
               Status = a.Status,
               TotalAmount = a.TotalAmount,
               TotalItems = a.TotalItems
            })
            .ToArrayAsync();
    


    public async Task<ApiResult<OrderItemDto[]>> GetUserOrderItemsAsync(int orderId , int userId) 
    {
        var order = await _context
            .Orders
            .AsNoTracking()
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
            return ApiResult<OrderItemDto[]>.Fail("Order Not Found . ");

        if(order.UserId != userId)
            return ApiResult<OrderItemDto[]>.Fail("Order Not Found . ");

        var items=  order.Items.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                ProductId = oi.ProductId,
                ProductImageUrl = oi.ProductImageUrl,
                ProductName = oi.ProductName,
                ProductPrice = oi.ProductPrice,
                Quantity = oi.Quantity,
                Unit = oi.Unit
            }).ToArray();


        return ApiResult<OrderItemDto[]>.Success(items);

    }

}
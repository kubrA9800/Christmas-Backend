using ChristmasBackend.Helpers.Responses;
using ChristmasBackend.Models;
using ChristmasBackend.ViewModels.Cart;

namespace ChristmasBackend.Services.Interfaces
{
    public interface ICartService
    {
        List<CartVM> GetDatasFromCookie();
        void SetDatasToCookie(List<CartVM> carts, Product dbProduct, CartVM existProduct);
        Task<DeleteBasketResponses> DeleteData(int? id);
        Task<Cart> GetByUserIdAsync(string userId);
        Task<List<CartProduct>> GetAllByCartIdAsync(int? cartId);
        int GetCount();
    }
}

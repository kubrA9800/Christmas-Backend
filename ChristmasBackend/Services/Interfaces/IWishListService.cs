using ChristmasBackend.Models;
using ChristmasBackend.ViewModels.Wishlist;

namespace ChristmasBackend.Services.Interfaces
{
    public interface IWishListService
    {
        List<WishlistVM> GetDatasFromCookie();
        void SetDatasToCookie(List<WishlistVM> wishlists, Product dbProduct, WishlistVM existProduct);
        void DeleteData(int? id);
        Task<Wishlist> GetByUserIdAsync(string userId);
        Task<List<WishlistProduct>> GetAllByWishlistIdAsync(int? wishlistId);
    }
}

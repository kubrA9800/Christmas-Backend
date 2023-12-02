using ChristmasBackend.Areas.ViewModels.Review;

namespace ChristmasBackend.Services.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewVM>> GetAllWithIncludeAsync();
        Task<ReviewVM> GetByIdWithIncludeAsync(int id);
        Task DeleteAsync(int id);
    }
}

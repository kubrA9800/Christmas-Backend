using ChristmasBackend.Areas.ViewModels.Review;

namespace ChristmasBackend.Services.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewVM>> GetAllAsync();
    }
}

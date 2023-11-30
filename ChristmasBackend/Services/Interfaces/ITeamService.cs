using ChristmasBackend.Areas.ViewModels.Team;

namespace ChristmasBackend.Services.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamVM>> GetAllAsync();
        Task<TeamVM> GetByIdAsync(int id);
        Task Delete(int id);
        Task EditAsync(TeamEditVM slider);
    }
}

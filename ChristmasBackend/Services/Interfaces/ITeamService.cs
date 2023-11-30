using ChristmasBackend.Areas.ViewModels.Team;

namespace ChristmasBackend.Services.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamVM>> GetAllAsync();
    }
}

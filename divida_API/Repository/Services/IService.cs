using Entities.Models;
using Entities.Models.Dtos;

namespace Repository.Services
{
    public interface IService
    {
        Task InsertDebtTitle(DebtTitleDto debtTitle);
        Task<List<DebtTitleResultDto>> GetAllTitleWithCalculatedValues();
    }
}

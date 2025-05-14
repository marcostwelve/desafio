using AutoMapper;
using Entities.Models;
using Entities.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace Repository.Services
{
    public class Service : IService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public Service(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<DebtTitleResultDto>> GetAllTitleWithCalculatedValues()
        {
            try
            {
                var titles = await _context.DebtTitles
                .Include(t => t.Installments)
                .ToListAsync();

                var today = DateTime.Now;
                var result = new List<DebtTitleResultDto>();

                foreach (var title in titles)
                {
                    var originalValue = title.Installments.Sum(i => i.InstallmentValue);
                    decimal totalInterest = 0m;
                    int maxDaysLate = 0;

                    foreach (var inst in title.Installments)
                    {
                        var daysLate = (today - inst.DueDate).Days;

                        if (daysLate > 0)
                        {
                            maxDaysLate = Math.Max(maxDaysLate, daysLate);
                            decimal dailyInterestRate = (title.FessPercent / 100m) / 30m;
                            totalInterest += inst.InstallmentValue * dailyInterestRate * daysLate;
                        }
                    }

                    var fine = originalValue * (title.FinePercent / 100m);
                    var totalUpdatedValue = originalValue + fine + totalInterest;

                    result.Add(new DebtTitleResultDto
                    {
                        NumberTitle = title.NumberTitle,
                        DebtorName = title.DebtorName,
                        InstallmentsCount = title.Installments.Count,
                        OriginalValue = originalValue,
                        MaxDaysLate = maxDaysLate,
                        TotalUpdatedValue = Math.Round(totalUpdatedValue, 2)
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("um erro ocorreu ao buscar títulos.", ex);
            }

        }

        public async Task InsertDebtTitle(DebtTitleDto debtTitleDto)
        {
            try
            {
                var debtTitle = _mapper.Map<DebtTitle>(debtTitleDto);
                debtTitle.Installments = _mapper.Map<List<DebtInstallment>>(debtTitleDto.Installments);

                await _context.DebtTitles.AddAsync(debtTitle);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Um erro ocorreu ao inserir os títulos com parcela.", ex);
            }
        }
    }
}

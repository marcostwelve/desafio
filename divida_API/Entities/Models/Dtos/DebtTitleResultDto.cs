using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Dtos
{
    public class DebtTitleResultDto
    {
        public int NumberTitle { get; set; }
        public string? DebtorName { get; set; }
        public int InstallmentsCount  { get; set; }
        public decimal OriginalValue { get; set; }
        public int MaxDaysLate { get; set; }
        public decimal TotalUpdatedValue { get; set; }
    }
}

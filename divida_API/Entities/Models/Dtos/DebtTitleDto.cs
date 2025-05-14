using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Dtos
{
    public class DebtTitleDto
    {
        public int NumberTitle { get; set; }
        public string? DebtorName { get; set; }
        public decimal FessPercent { get; set; }
        public string? DebtorCPF { get; set; }
        public decimal FinePercent { get; set; }
        public List<DebtInstallmentDto> Installments { get; set; }
    }
}

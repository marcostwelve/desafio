using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class DebtTitle
    {
        public int Id { get; set; }
        public int NumberTitle { get; set; }

        [Required]
        public string DebtorName { get; set; }

        [Required]
        public string DebtorCPF { get; set; }

        [Required]
        public decimal FessPercent { get; set; }

        [Required]
        public decimal FinePercent { get; set; }
        public ICollection<DebtInstallment> Installments { get; set; }
    }
}

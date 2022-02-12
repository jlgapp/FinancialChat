using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.Application.Models
{
    public class Message
    {
        public string ClientUniqueId { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string MessageIncome { get; set; } = string.Empty;
        public DateTime? Date { get; set; }
        public string User { get; set; } = string.Empty;
    }
}

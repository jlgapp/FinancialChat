using FinancialChat.Domain.Common;

namespace FinancialChat.Domain.UserMessages
{
    public class UserMessage : BaseDomainModel
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }


    }
}

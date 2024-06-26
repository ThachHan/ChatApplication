using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Models.Message
{
    public class MessageCreateModel
    {
        public string Content { get; set; }
        public Guid ConversationId { get; set; }
    }
}

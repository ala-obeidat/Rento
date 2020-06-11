using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Entity
{
    public class Message : BaseEntity
    {
        public string Body { get; set; }
    }
    public class SMSMessageEntity : Message
    {
        public string Mobile { get; set; }
    }
    public class MobileMessage : Message
    {
        public string Title { get; set; }
        public int Type { get; set; }
    }

    public enum MobileType
    {
        All = 0,
        Android = 1,
        iPhone = 2
    }

    public class MessageRequest
    {
        public DateTime DataFrom { get; set; }
        public DateTime DataTo { get; set; }
    }
}

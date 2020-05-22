using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WereldService.MqMessages
{
    public class RegisterMessage
    {
        public Guid id { get; set; }
        public string username { get; set; }
    }
}

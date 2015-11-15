using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fortes.Web.Models
{
    public class AlertModel
    {
        public AlertTypes Type { get; set; }

        public string Message { get; set; }
    }
    public enum AlertTypes { Error, Warning, Info, Success }
}

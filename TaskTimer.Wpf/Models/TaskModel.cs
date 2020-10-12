using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTimer.Wpf.Models
{
    public class TaskModel
    {
        public string Subject { get; set; }

        public string Message { get; set; }

        public decimal TimeReported { get; set; }

        public decimal RealTime { get; set; }

        public DateTime StartDateReported { get; set; }

        public DateTime EndDateReported { get; set; }

        public string InvoiceSubject { get; set; }

        public string InvoiceMessage { get; set; }

        public bool IsInvoice { get; set; }
    }
}

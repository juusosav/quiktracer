using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuikTracer.Vsix.Models
{
    public class TraceNode
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
        public bool IsError { get; set; }
        public string? FilePath { get; set; }
        public int? LineNumber { get; set; }

    }
}

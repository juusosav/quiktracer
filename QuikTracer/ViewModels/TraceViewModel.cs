using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using QuikTracer.Vsix.Models;

namespace QuikTracer.Vsix.ViewModels
{
    public class TraceViewModel
    {
        public ObservableCollection<TraceNode> TraceNodes { get; set; } = [];
    }
}

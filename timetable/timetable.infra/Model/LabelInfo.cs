using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable.Infra
{
    public class LabelInfo : IGridInfo
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public string Label { get; set; }
    }
}

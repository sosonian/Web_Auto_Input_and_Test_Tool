using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Auto_Input_and_Test_Tool
{
    public abstract class InputModule
    {
        public abstract string ModuleName { get; }
        public abstract string[,] TitleOfColumns { get; }
    }
}

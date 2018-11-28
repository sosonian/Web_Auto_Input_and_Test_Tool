using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Auto_Input_and_Test_Tool
{    
       public class Module3 : InputModule
       {
        private string moduleName = "MovieMusic";
        public override string ModuleName
        {
            get { return moduleName; }
        }

        private string[,] titleOfColumns = new string[,] { };
        public override string[,] TitleOfColumns
        {
            get { return titleOfColumns; }
        }
    }   
}

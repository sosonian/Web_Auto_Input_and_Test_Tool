using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Auto_Input_and_Test_Tool
{
    public class Module1 : InputModule
    {
        private string moduleName = "FilmMusic";
        public override string ModuleName
        {
            get { return moduleName; }    
        }

        private string[,] titleOfColumns = new string[,] { {"FilmID","系統識別號" },{"NameOfUnit", "單元名稱"},{"NameOfSong","曲名"},{"BGM","配樂"},{"Composing","作曲"},{"Lyric","作詞"},{"MainVocal","主唱"},{"Musician","演奏"}};
        public override string[,] TitleOfColumns
        {
            get { return titleOfColumns;}
        }
    }
    
}

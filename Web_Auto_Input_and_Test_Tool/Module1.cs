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
       
        public new int NumberOfInputFields = 7;
            //public string[,] TitleOfColumn = new string[7,2] {{"NameOfUnit", "單元名稱"},{"NameOfSong","曲名"},{"BGM","配樂"},{"Composing","作曲"},{"Lyric","作詞"},{"MainVocal","主唱"},{"Musician","演奏"}};            
    }
    
}

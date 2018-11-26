using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Auto_Input_and_Test_Tool
{    
       public class Module1 : InputModule
        {
            public new string  ModuleName = "FilmMusic";
            public string[,] TitleOfColumn = new string[7,2] {{"NameOfUnit", "單元名稱"},{"NameOfSong","曲名"},{"BGM","配樂"},{"Composing","作曲"},{"Lyric","作詞"},{"MainVocal","主唱"},{"Musician","演奏"}};
           
            public class InputMainProcess
            {
            }
            public class RecheckProcess
            {
            }
        }
    
}

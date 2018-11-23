using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Auto_Input_and_Test_Tool
{
    public class ModuleInfomation
    {
        // The enterrance URL of target page
        public string EnteranceUrL = "http://www.google.com";

        // The Name of the Target
        public string TargetName = "Google";

        // Have to Login User and Password to enter the page
        public Boolean HasLoginFunction;

        // If have to Login, the HtmlElement Id of Username inputbox:
        public string UserNameId;

        // If have to Login, the HtmlElement Id of password inputbox:
        public string PasswordId;

        // If have to Login, the HtmlElement Id of submit button:
        public string SubmitButtonId;
    }
}

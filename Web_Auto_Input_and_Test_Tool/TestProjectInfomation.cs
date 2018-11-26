using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Auto_Input_and_Test_Tool
{
    public static class TestProjectInfomation
    {
        // The enterrance URL of target page
        public static string EnteranceUrL = "http://www.google.com";

        // The Name of the Target
        public static string TargetName = "Google";

        // Have to Login User and Password to enter the page
        public static Boolean HasLoginFunction;

        // If have to Login, the HtmlElement Id of Username inputbox:
        public static string UserNameId;

        // If have to Login, the HtmlElement Id of password inputbox:
        public static string PasswordId;

        // If have to Login, the HtmlElement Id of submit button:
        public static string LoginSubmitButtonId;

        //
        public static Boolean IsPageTheTarget;


    }
}

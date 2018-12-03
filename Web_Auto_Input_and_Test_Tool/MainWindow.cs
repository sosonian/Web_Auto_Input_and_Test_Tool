using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using IntExl = Microsoft.Office.Interop.Excel;

namespace Web_Auto_Input_and_Test_Tool
{        
    public partial class MainWindow : Form
    {       
        LoginProcess.IEModels IEMdls = new LoginProcess.IEModels();
        LoginProcess.ChromeModels ChromeMdls = new LoginProcess.ChromeModels();
        ChooseWebDriver cWDF = new ChooseWebDriver();
        LoadExcel ldExcel = new LoadExcel();
        InputProcess InputProcess1= new InputProcess();
        public string[,] TitleList;
        int moduleToken { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            var appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe";
            IEMdls.SetNewIEKeyforWebBrowserControl(appName);
            cWDF.m1 = this;
            cWDF.chromeDriver = ChromeMdls.webDriver;
            cWDF.createForm();
            label1.Text = "Auto Input and Test Target:  " + TestProjectInfomation.TargetName;
        }      
        private void button1_Click(object sender, EventArgs e)
        {          
                if (cWDF.WebDriverToken == 2)
                {
                    IEMdls.createIEBrowser();
                    IEMdls.loginWebPage(TestProjectInfomation.EnteranceUrL);
                }
                else if (cWDF.WebDriverToken == 1)
                {
                    ChromeMdls.loginWebPage(TestProjectInfomation.EnteranceUrL);
                }          
        }       
        private void button2_Click(object sender, EventArgs e)
        {
            if (cWDF.WebDriverToken == 2)
            {
                IEMdls.loginUp(textBox1.Text, textBox2.Text, TestProjectInfomation.UserNameId, TestProjectInfomation.PasswordId, TestProjectInfomation.LoginSubmitButtonId);
            }
            else if (cWDF.WebDriverToken == 1)
            {
                ChromeMdls.loginUp(textBox1.Text, textBox2.Text, TestProjectInfomation.UserNameId, TestProjectInfomation.PasswordId, TestProjectInfomation.LoginSubmitButtonId);
            }
        }   // Begin LoginProcess
        private void button3_Click(object sender, EventArgs e)
        {           
            ldExcel.m1 = this;     
            ldExcel.LoadExcelBegin();
        }   // Begin LoadExcel
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {           
            ldExcel.selectExlSheet();
            
        }
        private void button4_Click(object sender, EventArgs e)
        {
            InputProcess1.m1 = this;
            InputProcess1.mainProcess(moduleToken);
        }
       
        public class LoginProcess
        {
            public class IEModels
            { 
                public WebBrowser webBrowser1 = null;
                public void createIEBrowser()
                {
                    Form IEBrowserForm = new Form();
                    webBrowser1 = new WebBrowser();
                    webBrowser1.Size = new Size(1200, 800);
                    IEBrowserForm.StartPosition = FormStartPosition.CenterScreen;
                    IEBrowserForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    IEBrowserForm.Size = new Size(1200, 800);
                    IEBrowserForm.Controls.Add(webBrowser1);
                    IEBrowserForm.Show();
                }
                public void loginWebPage(string TargetUrl)
                {
                    webBrowser1.Navigate(TargetUrl);
                }
                public void loginUp(string username, string password, string UsernameInputboxID, string PasswordInputBoxID, string SubmitButtonID)
                {
                    webBrowser1.Document.GetElementById(UsernameInputboxID).SetAttribute("value", username);
                    webBrowser1.Document.GetElementById(PasswordInputBoxID).SetAttribute("value", password);
                    webBrowser1.Document.GetElementById(SubmitButtonID).InvokeMember("click");
                    webBrowser1.DocumentCompleted += (sender, e) => loginComplete(sender, e);
                }
                public void SetNewIEKeyforWebBrowserControl(string appName)
                {
                    RegistryKey RegKey = null;
                    try
                    {
                        if (Environment.Is64BitOperatingSystem)
                            RegKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Wow6432Node\\Microsoft\\Internet Explorer\\MAIN\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
                        else
                            RegKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);

                        if (RegKey == null)
                        {
                            MessageBox.Show("Unable to setting IE Emulation RegKey");
                            return;
                        }

                        string FindAppkey = Convert.ToString(RegKey.GetValue(appName));

                        if (FindAppkey == "11000")
                        {
                            MessageBox.Show("IE Emulation RegKey has been set already");
                            RegKey.Close();
                            return;
                        }

                        if (string.IsNullOrEmpty(FindAppkey))
                            RegKey.SetValue(appName, unchecked((int)0x2AF8), RegistryValueKind.DWord);

                        FindAppkey = Convert.ToString(RegKey.GetValue(appName));

                        if (FindAppkey == "11000")
                            MessageBox.Show("Application Settings Applied Successfully");
                        else
                            MessageBox.Show("Application Settings Failed, Ref: " + FindAppkey);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Application Settings Failed");
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {

                        if (RegKey != null)
                            RegKey.Close();
                    }
                }
                public void loginComplete(object sender, WebBrowserDocumentCompletedEventArgs e)
                {
                    string PageAfterLogin="";
                    webBrowser1.Navigate(PageAfterLogin);
                }
            }
            public class ChromeModels
            {
                public IWebDriver webDriver = null;

                public void loginWebPage(string TargetUrl)
                {                    
                    webDriver = new ChromeDriver();
                    webDriver.Url = TargetUrl;
                }
                public void loginUp(string username, string password, string UsernameInputboxID, string PasswordInputBoxID,string SubmitButtonID)
                {
                    webDriver.FindElement(By.Id(UsernameInputboxID)).SendKeys(username);
                    webDriver.FindElement(By.Id(PasswordInputBoxID)).SendKeys(password);
                    webDriver.FindElement(By.Id(SubmitButtonID)).Click();
                    WebPageComplete();                    
                }
                public void WebPageComplete()
                {
                    WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
                    wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
                }
            }
        }
        public class InputProcess
        {
            public MainWindow m1 { get; set; }
            public void mainProcess(int moduleToken)
            {
                if (m1.dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("please load the input data first");
                }
            }
        }
        public class CheckProcess
        {
        }
        public class ChooseWebDriver
        {
            public MainWindow m1 { get; set; }
            public IWebDriver chromeDriver { get; set;}
            public int WebDriverToken = 0;
            public void createForm()
            {
                Form chooseDialog = new Form();
                Button b1 = new Button();
                Button b2 = new Button();
                Label l1 = new Label();
                b1.Text = "Chorme";
                b1.Location = new Point(50, 50);
                b1.Click += (sender, e) => b1_click(sender, e, (Form)chooseDialog);

                b2.Text = "IE";
                b2.Location = new Point(b1.Left, b1.Height + b1.Top + 10);
                b2.Click += (sender, e) => b2_click(sender, e, (Form)chooseDialog);

                l1.Text = "Please Choose the Web Browser";
                l1.Size = new Size(200, 50);
                l1.Location = new Point(10, 10);
                chooseDialog.StartPosition = FormStartPosition.CenterScreen;
                chooseDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                chooseDialog.Size = new Size(200, 200);
                chooseDialog.Controls.Add(b1);
                chooseDialog.Controls.Add(b2);
                chooseDialog.Controls.Add(l1);
                chooseDialog.ShowDialog();
            }
            public void b1_click(object sender, EventArgs e, Form f)
            {
                WebDriverToken = 1;
                f.Dispose();
                ChooseInputModule CIS = new ChooseInputModule();
                CIS.m1 = m1;
                CIS.getListOfModule();
            }
            public void b2_click(object sender, EventArgs e, Form f)
            {
                WebDriverToken = 2;
                f.Dispose();
                ChooseInputModule CIS = new ChooseInputModule();
                CIS.m1 = m1;
                CIS.getListOfModule();
            }
        }
        public class ChooseInputModule
        {
            public MainWindow m1 { get; set; }
            
            private List<InputModule> ModuleList = new List<InputModule>();           
            
            public void getListOfModule()
            {
                  IEnumerable<InputModule> exporters = typeof(InputModule).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(InputModule)) && !t.IsAbstract).Select(t => (InputModule)Activator.CreateInstance(t));                 
                  foreach (var item in exporters)
                  {
                    ModuleList.Add(item);                                        
                  }
                  createForm();
            }
            public void createForm()
            {
                Form ChooseModuleDialog = new Form();
                ChooseModuleDialog.StartPosition = FormStartPosition.CenterScreen;
                ChooseModuleDialog.FormBorderStyle = FormBorderStyle.FixedDialog;                
                createLable(ChooseModuleDialog);
                createButton(ChooseModuleDialog);
            }
            public void createLable(Form f)
            {
                Label l1 = new Label();
                l1.Text = "Please Choose the input Module";
                l1.Size = new Size(200, 30);
                l1.Location = new Point(10, 10);
                f.Controls.Add(l1);
            }
            public void createButton(Form f)
            {                
                int totalHeight = 0;
                for (int i = 1; i-1 < ModuleList.Count; i++)
                {
                    if (i == ModuleList.Count)
                    {
                        Button bt = new Button();
                        bt.Location = new Point(50, 50 + (bt.Height + 10) * (i - 1));
                        bt.Click += (sender, e) => buttonClickEvent(sender, e, f);
                        bt.Tag = i;
                        bt.Text = ModuleList.ElementAt(i - 1).ModuleName;
                        f.Controls.Add(bt);
                        totalHeight = 50 + (bt.Height + 10) * (i - 1);                        
                    }
                    else
                    {
                        Button bt = new Button();
                        bt.Location = new Point(50, 50 + (bt.Height + 10) * (i - 1));
                        bt.Click += (sender, e) => buttonClickEvent(sender, e, f);
                        bt.Tag = i;
                        bt.Text = ModuleList.ElementAt(i - 1).ModuleName;
                        f.Controls.Add(bt);
                    }                                     
                }

               // Need to improve 
               // Button test = new Button();
               // test.Location = new Point(50, 250);
               // test.Click += (sender, e) => testClickEvent(sender, e, f);

               // test.Text = "test";
               // f.Controls.Add(test);

                f.Size = new Size(200, totalHeight + 100);
                f.ShowDialog();
            }
            public void buttonClickEvent(object sender, EventArgs e, Form f)
            {
                Button bt = (Button)sender;
                chooseInput(Convert.ToInt16(bt.Tag));
                f.Dispose();
            }
            public void chooseInput(int i)
            {            
                m1.TitleList = ModuleList.ElementAt(i - 1).TitleOfColumns;
                m1.label2.Text = "Choose Module: " + ModuleList.ElementAt(i - 1).ModuleName;
                m1.moduleToken = i;
            }
        }
        public class LoadExcel
        {
            public MainWindow m1 { get; set; }
            
            public IntExl.Application myApp = null;
            public IntExl.Workbook myBook = null;
            public IntExl.Worksheet mySheet = null;
            
            public void LoadExcelBegin()
            {                     
                m1.openFileDialog1.Filter = "Excel Files|*.xlsx; *.xls; *.xlsm";
                if (m1.openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    m1.textBox3.Text = m1.openFileDialog1.FileName;
                    m1.comboBox1.Items.Clear();
                    myApp = new IntExl.Application();
                    myBook = myApp.Workbooks.Open(m1.textBox3.Text);
                    foreach (IntExl.Worksheet everySheet in myBook.Sheets)
                    {
                        m1.comboBox1.Items.Add(everySheet.Name);
                    }
                }
            }
            public void selectExlSheet()
            {                
                SelectExlData form2 = new SelectExlData();
                // once form instance have been created, before completion of the process InitializeComponent(), the next line would never been executed.
                
                form2.transData(ExlTB(myBook.Worksheets[m1.comboBox1.SelectedItem].UsedRange));

                SelectExlData.InputFieldControls IFM = new SelectExlData.InputFieldControls();
                IFM.SED = form2;
                IFM.m1 = m1;
                IFM.TitleOfInputField = m1.TitleList;
                IFM.createControls();

                form2.IFD = IFM;
                form2.ShowDialog();
                myBook.Close();
                myApp.Quit();
            }                                    
            public DataTable ExlTB(IntExl.Range myRange)
            {
                DataTable exlTb = new DataTable();
                object[,] tempData = new object[1, 1];
                int ColCnt = myRange.Columns.Count;
                int RowCnt = myRange.Rows.Count;
                tempData = myRange.Value;
                string[] tArray = new string[ColCnt - 1];

                for (int i = 0; i<ColCnt - 1; i++)
                {
                        tArray[i] = Convert.ToString(tempData[1, i + 1]);
                }
                for (int i = 0; i<ColCnt - 1; i++)
                {
                        exlTb.Columns.Add(tArray[i]);
                }
                for (int x = 0; x<RowCnt - 1; x++)
                {
                        for (int i = 0; i<ColCnt - 1; i++)
                        {
                            tArray[i] = Convert.ToString(tempData[x + 2, i + 1]);
                        }
                        exlTb.Rows.Add(tArray);
                }
                return exlTb;
            }
        }
        public class TestReport
        {
        }
    }
}

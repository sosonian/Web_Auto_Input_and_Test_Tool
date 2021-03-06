﻿using System;
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

namespace TFI_AMS_WebBot_Film_Music
{        
    public partial class MainWindow : Form
    {       
        LoginProcess.IEModels IEMdls = new LoginProcess.IEModels();
        LoginProcess.ChromeModels ChromeMdls = new LoginProcess.ChromeModels();
        ChooseWebDriverForm cWDF = new ChooseWebDriverForm();
        LoadExcel ldExcel = new LoadExcel();
        InputProcess InputProcess1= new InputProcess();        

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            var appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe";
            IEMdls.SetNewIEKeyforWebBrowserControl(appName);
            cWDF.chromeDriver = ChromeMdls.webDriver;
            cWDF.chooseWebDriver();
        }      
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show("Please enter the URL of Target Page, including'http://'");
            }
            else
            {
                if (cWDF.WebDriverToken == 2)
                {
                    IEMdls.createIEBrowser();
                    IEMdls.loginWebPage(textBox4.Text);
                }
                else if (cWDF.WebDriverToken == 1)
                {
                    ChromeMdls.loginWebPage(textBox4.Text);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cWDF.WebDriverToken == 2)
            {
                IEMdls.loginUp(textBox1, textBox2);
            }
            else if (cWDF.WebDriverToken == 1)
            {
                ChromeMdls.loginUp(textBox1, textBox2);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {           
            ldExcel.tb3 = textBox3;
            ldExcel.cb1 = comboBox1;
            ldExcel.ofd1 = openFileDialog1;
            ldExcel.gv1 = dataGridView1;
            ldExcel.LoadExcelBegin();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ldExcel.selectExlSheet();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            // InputProcess1.mainProcess(1);
            // SelectExlData SED = new SelectExlData();
            // SED.ShowDialog();
            ChooseInputSector CIS = new ChooseInputSector();
            CIS.detectTotalNumberOfSector();

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
                public void loginUp(TextBox tb1, TextBox tb2)
                {
                    webBrowser1.Document.GetElementById("uname").SetAttribute("value", tb1.Text);
                    webBrowser1.Document.GetElementById("upass").SetAttribute("value", tb2.Text);
                    webBrowser1.Document.GetElementById("act_signin").InvokeMember("click");
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
                public void loginUp(TextBox tb1, TextBox tb2)
                {
                    webDriver.FindElement(By.Id("uname")).SendKeys(tb1.Text);
                    webDriver.FindElement(By.Id("upass")).SendKeys(tb2.Text);
                    webDriver.FindElement(By.Id("act_signin")).Click();
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
            public void mainProcess(int whichPageToInput)
            {
            }
        }
        public class RecheckProcess
        {
        }
        public class ChooseWebDriverForm
        {
            public IWebDriver chromeDriver { get; set;}
            public int WebDriverToken = 0;
            public void chooseWebDriver()
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
            }
            public void b2_click(object sender, EventArgs e, Form f)
            {
                WebDriverToken = 2;
                f.Dispose();                
            }
        }
        public class ChooseInputSector
        {
            public void detectTotalNumberOfSector()
            {
                InputModule.Module1 m1 = new InputModule.Module1();

                Type mytype = typeof(InputModule.Module1);
                FieldInfo ModName = mytype.GetField("ModuleName");
                string c = ModName.GetValue(m1).ToString();
                Console.WriteLine(c);

                //  int cm = 0;
                //  Type[] submodles = typeof(InputModule).GetNestedTypes();
                //  cm = submodles.Count();                               
                //  createListofModule(cm, submodles);
            }
            public void createListofModule(int CountModuleNumber, Type[] CollectionOfModule)
            {
                List<string> ListofNameOfModule = new List<string>();
                foreach (Type type in CollectionOfModule)
                {
                    
                    PropertyInfo ModName = type.GetProperty("ModuleName");
                    string c = ModName.GetValue(type).ToString();

                    //ListofNameOfModule.Add(ModName.GetValue(type).ToString());
                    Console.WriteLine(c);
                }
            }
        }
        public class LoadExcel
        {
            public TextBox tb3 { get; set; }
            public ComboBox cb1 { get; set; }
            public OpenFileDialog ofd1 { get; set; }
            public DataGridView gv1 { get; set; }

            public IntExl.Application myApp = null;
            public IntExl.Workbook myBook = null;
            public IntExl.Worksheet mySheet = null;           
           
            public void LoadExcelBegin()
            {                     
                ofd1.Filter = "Excel Files|*.xlsx; *.xls; *.xlsm";
                if (ofd1.ShowDialog() == DialogResult.OK)
                {
                    tb3.Text = ofd1.FileName;
                    cb1.Items.Clear();
                    myApp = new IntExl.Application();
                    myBook = myApp.Workbooks.Open(tb3.Text);
                    foreach (IntExl.Worksheet everySheet in myBook.Sheets)
                    {
                        cb1.Items.Add(everySheet.Name);
                    }
                }
            }
            public void selectExlSheet()
            {               
                DataTable exlTb = new DataTable();
                IntExl.Range myRange = null;
                
                mySheet = myBook.Worksheets[cb1.SelectedItem];
                myRange = mySheet.UsedRange;
                int ColCnt = myRange.Columns.Count;
                int RowCnt = myRange.Rows.Count;
                object[,] tempData = new object[1, 1];
                tempData = myRange.Value;
                string[] tArray = new string[ColCnt-1];

                for (int i = 0; i < ColCnt-1; i++)
                {
                    tArray[i] = Convert.ToString(tempData[1,i+1]);
                }
                for (int i = 0; i < ColCnt-1; i++)
                {                    
                    exlTb.Columns.Add(tArray[i]);
                }
                for (int x = 0; x < RowCnt-1; x++)
                {
                    for (int i = 0; i < ColCnt-1; i++)
                    {
                        tArray[i] = Convert.ToString(tempData[x+2, i+1]);
                    }
                    exlTb.Rows.Add(tArray);
                }

                myBook.Close();
                myApp.Quit();

                SelectExcelData form2 = new SelectExcelData();
                form2.dg1 = gv1;
                form2.dataGridView1.DataSource = exlTb;
                form2.dataGirdView1Tb = exlTb.Copy();
                form2.ShowDialog();                
            }                        
        }      
    }
}

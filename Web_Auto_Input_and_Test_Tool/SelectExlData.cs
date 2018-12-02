using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Web_Auto_Input_and_Test_Tool
{
    public partial class SelectExlData : Form
    {       
        public DataTable DGVTb = new DataTable();
        
        public InputFieldControls IFD { get; set; }
        
        public SelectExlData()
        {
            InitializeComponent();                      
        }
        private void SelectExlData_Load(object sender, EventArgs e)
        {
            
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {           
            IFD.dataGridCellSelect(dataGridView1.CurrentCell.ColumnIndex);
        }    
        public void transData(DataTable dt1)
        {
            this.dataGridView1.DataSource = dt1;
            //this.DGVTb = dt1.Copy();           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            IFD.sendDataBackToMain1();
        }

        public partial class InputFieldControls
        {
            public MainWindow m1 { get; set; }
            private TextBox tb;
            private int selectToken = 0;
            public void TextBoxClickEvent(object sender, EventArgs e)
            {
                tb = (TextBox)sender;
                clickLogic();
            }
            public void clickLogic()
            {
                selectToken = Convert.ToInt16(tb.Tag);
            }
            public void dataGridCellSelect(int cellIndex)
            {                
                if (selectToken == 0)
                {
                    MessageBox.Show("Please Click TextBoxes Below First !");
                }
                else
                {
                    foreach (TextBox TB in tbList)
                    {
                        int token = Convert.ToInt16(TB.Tag);
                        if (token == selectToken)
                        {
                            TB.Text = cellIndex.ToString();
                        }
                    }
                }
            }
            public void sendDataBackToMain1() //copy data by looping throught cells of DataGridView, not very efficient.
            {
                List<string> ColumnNameList = new List<string>();
                for (int i = 0; i < tbList.Count(); i++)
                {
                    SED.DGVTb.Columns.Add(lbList.ElementAt(i).Text);                   
                }
                for (int j = 1; j < SED.dataGridView1.Rows.Count; j++)
                {
                    string[] tArray = new string[tbList.Count()];
                    for (int i = 0; i < tbList.Count(); i++)
                    {
                        string temp = Convert.ToString(SED.dataGridView1.Rows[j].Cells[Convert.ToInt16(tbList.ElementAt(i).Text)].Value);
                        tArray[i] = temp;
                    }
                    SED.DGVTb.Rows.Add(tArray);
                }
                m1.dataGridView1.DataSource = SED.DGVTb;
                SED.Close();
            }
            public void sendDataBackToMain2()
            {


            }
        }       
    }
}

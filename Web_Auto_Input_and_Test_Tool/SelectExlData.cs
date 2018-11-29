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
        public DataTable dataGirdView1Tb = new DataTable();
        
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
            this.dataGirdView1Tb = dt1.Copy();           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            IFD.sendDataBackToMain();
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
            public void sendDataBackToMain()
            {
                for (int i = SED.dataGirdView1Tb.Columns.Count - 1; i > -1; i--)
                {
                    if (SelectColumnIndex().IndexOf(i) == -1)
                    {
                        SED.dataGirdView1Tb.Columns.RemoveAt(i);
                    }
                }
                m1.dataGridView1.DataSource = SED.dataGirdView1Tb;
                SED.Close();
            }
            public List<int> SelectColumnIndex()
            {
                List<int> result = new List<int>();
                foreach (TextBox TB in tbList)
                {                    
                    result.Add(Convert.ToInt16(TB.Text));
                }
                return result;
            }
        }

        
    }
}

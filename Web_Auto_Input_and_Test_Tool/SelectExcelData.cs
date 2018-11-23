using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TFI_AMS_WebBot_Film_Music
{
    public partial class SelectExcelData : Form
    {       
        modelSelectExcelData mSED = new modelSelectExcelData();
        public DataTable dataGirdView1Tb = new DataTable();
        public DataGridView dg1 { get; set; }

        public SelectExcelData()
        {
            InitializeComponent();                     
        }
        private void SelectExcelData_Load(object sender, EventArgs e)
        {
            mSED.s1 = this;
        }

        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int cellIndex = dataGridView1.CurrentCell.ColumnIndex;
            mSED.dataGridCellSelect(cellIndex);
        }
        private void textBox1_Click(object sender, EventArgs e)
        {
            mSED.textBoxSelection(1);
        }
        private void textBox2_Click(object sender, EventArgs e)
        {
            mSED.textBoxSelection(2);
        }
        private void textBox3_Click(object sender, EventArgs e)
        {
            mSED.textBoxSelection(3);
        }
        private void textBox4_Click(object sender, EventArgs e)
        {
            mSED.textBoxSelection(4);
        }
        private void textBox5_Click(object sender, EventArgs e)
        {
            mSED.textBoxSelection(5);
        }
        private void textBox6_Click(object sender, EventArgs e)
        {
            mSED.textBoxSelection(6);
        }
        private void textBox7_Click(object sender, EventArgs e)
        {
            mSED.textBoxSelection(7);
        }
        private void textBox8_Click(object sender, EventArgs e)
        {
            mSED.textBoxSelection(8);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            mSED.transDataBackToMain();
        }

        public class modelSelectExcelData
        {
            //public List<int> selectColumnIndex = new List<int>();
            public int selectToken = 0;
            public SelectExcelData s1 { get; set; }            

            public void dataGridCellSelect(int cellIndex)
            {
                if (selectToken == 0)
                {
                    MessageBox.Show("Please Click TextBoxes Below First !");
                }
                else
                {
                    Control ctn = s1.Controls["TextBox" + selectToken];
                    ctn.Text = cellIndex.ToString();
                }
            }
            public void textBoxSelection(int st)
            {
                selectToken = st;                
            }
            public void transDataBackToMain()
            {
               
               for (int i= s1.dataGirdView1Tb.Columns.Count-1; i>-1; i--)
                {
                    if (SelectColumnIndex().IndexOf(i) == -1)
                    {
                       s1.dataGirdView1Tb.Columns.RemoveAt(i);
                    }
                }                              
                s1.dg1.DataSource = s1.dataGirdView1Tb;                
                s1.Close();
            }
            public List<int> SelectColumnIndex()
            {
                List<int> result =new List<int>();
                for (int i =1; i <9; i++)
                {
                    Control ctn = s1.Controls["TextBox" + i];
                    result.Add(Convert.ToInt16(ctn.Text)); 
                }
                return result;
            }
        }       
    }
}

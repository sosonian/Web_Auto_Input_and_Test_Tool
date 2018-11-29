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
        
    
        public SelectExlData()
        {
            InitializeComponent();                      
        }
        private void SelectExlData_Load(object sender, EventArgs e)
        {
            
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int cellIndex = dataGridView1.CurrentCell.ColumnIndex;          
        }
    
        public void transData(DataTable dt1)
        {
            this.dataGridView1.DataSource = dt1;
            this.dataGirdView1Tb = dt1.Copy();
            
        }

        public partial class InputFilmMusic
        {                      
            public void TextBoxClickEvent(object sender, EventArgs e)
            {
                ClickEventLogic();
            }
            public void ClickEventLogic()
            {

            }
        }

       
    }
}

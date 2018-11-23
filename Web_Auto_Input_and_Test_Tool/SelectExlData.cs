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
    public partial class SelectExlData : Form
    {
        InputFilmMusic IFM = new InputFilmMusic();

        public SelectExlData()
        {
            InitializeComponent();
            IFM.SED = this;
            IFM.DGV1 = dataGridView1;           
            IFM.createLayout();
        }
        private void SelectExlData_Load(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int cellIndex = dataGridView1.CurrentCell.ColumnIndex;

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

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Web_Auto_Input_and_Test_Tool
{
    partial class SelectExlData
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(776, 289);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // SelectExlData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 511);
            this.Controls.Add(this.dataGridView1);
            this.Name = "SelectExlData";
            this.Text = "SelectExlData";
            this.Load += new System.EventHandler(this.SelectExlData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DataGridView dataGridView1;

        public partial class InputFilmMusic
        {
            public Form SED { get; set; }
            public DataGridView DGV1 { get; set; }
            public string[,] TitleOfInputField { get; }


            List<string> namesInputCells = new List<string>()
            { "系統識別號","單元名稱","曲名","配樂","作曲","作詞","主唱","演奏"};

            public void createLayout()
            {
                createTextBox();
                // createLabel();                
            }
            public void createTextBox()
            {
                for (int i = 1; i < TitleOfInputField.Length + 1; i++)
                {
                    TextBox tb = new TextBox();
                   
                    if (i < 7)
                    {
                        tb.Location = new Point(DGV1.Left, (DGV1.Top + DGV1.Height) + (30 * (i - 1) + 10));
                    }
                    else if (i < 13)
                    {
                        tb.Location = new Point(DGV1.Left + 2 * tb.Width, (DGV1.Top + DGV1.Height) + (30 * (i - 7) + 10));
                    }

                    tb.Click += new EventHandler(TextBoxClickEvent);
                    tb.Tag = "tb" + i;
                    tb.Text = TitleOfInputField [0,i-1];
                    SED.Controls.Add(tb);
                }
            }
            public void createLabel()
            {
                for (int i = 1; i < namesInputCells.Count + 1; i++)
                {
                    Label lb = new Label();
                    lb.Text = namesInputCells[i - 1];

                    if (i < 7)
                    {
                        lb.Location = new Point();
                    }
                    else if (i < 13)
                    {
                        lb.Location = new Point();
                    }

                }
            }
        }

    }
}
﻿using System;
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
            this.button1 = new System.Windows.Forms.Button();
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(704, 455);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 44);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SelectExlData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 511);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "SelectExlData";
            this.Text = "SelectExlData";
            this.Load += new System.EventHandler(this.SelectExlData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DataGridView dataGridView1;
        private Button button1;

        public partial class InputFieldControls
        {
            public SelectExlData SED { get; set; }
            public string[,] TitleOfInputField { get; set; }
            public List<TextBox> tbList = new List<TextBox>();

            public void createControls()
            {
                for (int i = 1; i < TitleOfInputField.Length / 2 + 1; i++)
                {
                    TextBox tb = new TextBox();
                    Label lb = new Label();

                    if (i < 7)
                    {
                        tb.Location = new Point(SED.dataGridView1.Left, (SED.dataGridView1.Top + SED.dataGridView1.Height) + (30 * (i - 1) + 10));
                        lb.Location = new Point(tb.Left+tb.Width+10, tb.Top + 5);
                    }
                    else if (i < 13)
                    {
                        tb.Location = new Point(SED.dataGridView1.Left + 2 * (tb.Width+10), (SED.dataGridView1.Top + SED.dataGridView1.Height) + (30 * (i - 7) + 10));
                        lb.Location = new Point(tb.Left + tb.Width + 10, tb.Top + 5);
                    }
                    else if (i < 19)
                    {
                        tb.Location = new Point(SED.dataGridView1.Left + 3 * (tb.Width + 10), (SED.dataGridView1.Top + SED.dataGridView1.Height) + (30 * (i - 7) + 10));
                        lb.Location = new Point(tb.Left + tb.Width + 10, tb.Top + 5);
                    }
                    else if (i < 25)
                    {
                        tb.Location = new Point(SED.dataGridView1.Left + 3 * (tb.Width + 10), (SED.dataGridView1.Top + SED.dataGridView1.Height) + (30 * (i - 7) + 10));
                        lb.Location = new Point(tb.Left + tb.Width + 10, tb.Top + 5);
                    }

                    tb.Click += new EventHandler(TextBoxClickEvent);
                    tb.Tag = i;
                    //tb.Text = TitleOfInputField[i - 1, 0];
                    lb.Tag = i;
                    lb.Text = TitleOfInputField[i - 1, 1];

                    tbList.Add(tb);
                    SED.Controls.Add(tb);
                    SED.Controls.Add(lb);
                }
            }       
        }
    }
}
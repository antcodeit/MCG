﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIN2Chassis_Converter
{
    public partial class mainForm : Form
    {
        bool submitClick = false;
        bool headerClick = false;
        public static bool errorThrown = false;

        string selectedChassis;

        methods m = new methods();
        BackgroundWorker bw = new BackgroundWorker();

        public mainForm()
        {  
            InitializeComponent();
            this.yearBox.IntegralHeight = false;
            this.modelBox.IntegralHeight = false;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
        }

        private void yearBox_TextUpdate(object sender, EventArgs e)
        {
            m.updateModel();
        }

        private void yearBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            m.updateModel();
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            m.getChassis();

            if (errorThrown != true)
            {
                selectedChassis = chassis.baseChassis[modelBox.SelectedIndex, 0];
            }
            else
            {
                errorThrown = false;
                return;
            }
            

            if (bw.IsBusy != true)
            {
                submitClick = true;
                bw.RunWorkerAsync();
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            carInfo.Rows.Clear(); 
        }
        
        private void carInfo_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewRow row in carInfo.SelectedRows)
            {
                Clipboard.SetText(row.Cells[1].Value.ToString().Replace(".", ""));
                selectedChassis = row.Cells[1].Value.ToString();
            }

            if (bw.IsBusy != true)
            {
                headerClick = true;
                bw.RunWorkerAsync();
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (submitClick)
            {
                statusLabel.Invoke((MethodInvoker)delegate
                {
                    statusLabel.Text = "Status: " + selectedChassis + " copied to clipboard.";
                });
                System.Threading.Thread.Sleep(5000);
                statusLabel.Invoke((MethodInvoker)delegate
                {
                    statusLabel.Text = "Status: ";
                });

                submitClick = false;
                return;
            }
            else if (headerClick)
            {
                statusLabel.Invoke((MethodInvoker)delegate
                {
                    statusLabel.Text = "Status: " + selectedChassis + " copied to clipboard.";
                });
                System.Threading.Thread.Sleep(5000);
                statusLabel.Invoke((MethodInvoker)delegate
                {
                    statusLabel.Text = "Status: ";
                });

                headerClick = false;
                return;
            }
            else
                return;
        }
    }

   
}

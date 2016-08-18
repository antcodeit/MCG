using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace VIN2Chassis_Converter
{
    class methods
    {
        chassis c = new chassis();

        public void getChassis()
        {
            var f = mainForm.ActiveForm as mainForm;

            //Remove first 'if' statement when project is complete
            if (f.yearBox.SelectedIndex >= 14) //2001 NOT ADDED YET
            {
                throwError(2);
                return;
            }
            else if (f.modelBox.SelectedIndex < 0)
            {
                throwError(1);
                return;
            }

            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(f.carInfo);

            string carData = f.yearBox.Text + " | " + f.modelBox.Text;
            row.Cells[0].Value = carData;

            for (int i = 0; i <= 2; i++)
            {
                row.Cells[1 + i].Value = chassis.baseChassis[f.modelBox.SelectedIndex, i];
            }
            
            f.carInfo.Rows.Add(row);

            Clipboard.SetText(chassis.baseChassis[f.modelBox.SelectedIndex, 0].Replace(".", ""));
        }
        
        
        public void updateModel()
        {
            var f = mainForm.ActiveForm as mainForm;

            //Remove 'if' statement when project is complete
            if(f.yearBox.SelectedIndex >= 14) //2001 NOT ADDED YET
            {
                f.modelBox.DataSource = null;
                f.modelBox.Items.Clear();
                return;
            }

            c.setModel(f.yearBox.SelectedIndex);
            c.setChassis(f.yearBox.SelectedIndex);
            f.modelBox.DataSource = chassis.model;
        }

        public void throwError(int errorType)
        {
            if(errorType == 1) //No model has been selected.
            {
                mainForm.errorThrown = true;
                MessageBox.Show("Error: Please select a model type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (errorType == 2) //Selected year that has not been added yet.
            {
                mainForm.errorThrown = true;
                MessageBox.Show("Error: Year 2001 and under is not yet added.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Cs.Exam3.Business;

namespace Cs.Exam3.UI
{
    public partial class frmPayroll : Form
    {
        CPayrolls oPayrolls;

        public frmPayroll()
        {
            InitializeComponent();
        }

        private void frmPayroll_Load(object sender, EventArgs e)
        {
            oPayrolls = new CPayrolls();
            oPayrolls.GetData();

            dgvPayroll.DataSource = oPayrolls.Items;

            btnClear_Click(sender, e);
        }

        private void dgvPayroll_SelectionChanged(object sender, EventArgs e)
        {
            txtEmployeeId.Text = oPayrolls.Items[dgvPayroll.CurrentRow.Index].EmployeeId;
            txtCheckNo.Text = oPayrolls.Items[dgvPayroll.CurrentRow.Index].CheckNo.ToString();
            dtCheckDate.Value = oPayrolls.Items[dgvPayroll.CurrentRow.Index].CheckDate;
            txtCheckAmount.Text = oPayrolls.Items[dgvPayroll.CurrentRow.Index].CheckAmount.ToString();
            txtHoursWorked.Text = oPayrolls.Items[dgvPayroll.CurrentRow.Index].HoursWorked.ToString();

            btnAdd.Text = "Update";
        }

        private void dgvPayroll_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgvPayroll.Columns[0].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeId.Text.Length > 0 &&
                    txtCheckNo.Text.Length > 0 &&
                    txtCheckAmount.Text.Length > 0 &&
                    txtHoursWorked.Text.Length > 0)
                {
                    if (btnAdd.Text == "Add")
                    {
                        CPayroll oPayroll = new CPayroll(txtEmployeeId.Text, int.Parse(txtCheckNo.Text), dtCheckDate.Value, decimal.Parse(txtCheckAmount.Text), decimal.Parse(txtHoursWorked.Text));

                        oPayroll.Insert();
                        oPayrolls.Add(oPayroll);

                        dgvPayroll.DataSource = null;
                        dgvPayroll.DataSource = oPayrolls.Items;
                        dgvPayroll.ClearSelection();
                        dgvPayroll.Rows[oPayrolls.Count - 1].Selected = true;

                        oPayroll = null;
                    }
                    else if (btnAdd.Text == "Update")
                    {
                        oPayrolls.Items[dgvPayroll.CurrentRow.Index].EmployeeId = txtEmployeeId.Text;
                        oPayrolls.Items[dgvPayroll.CurrentRow.Index].CheckNo = int.Parse(txtCheckNo.Text);
                        oPayrolls.Items[dgvPayroll.CurrentRow.Index].CheckDate = dtCheckDate.Value;
                        oPayrolls.Items[dgvPayroll.CurrentRow.Index].CheckAmount = decimal.Parse(txtCheckAmount.Text);
                        oPayrolls.Items[dgvPayroll.CurrentRow.Index].HoursWorked = decimal.Parse(txtHoursWorked.Text);
                        oPayrolls.Items[dgvPayroll.CurrentRow.Index].Update();

                        dgvPayroll.DataSource = null;
                        dgvPayroll.DataSource = oPayrolls.Items;
                    }
                }
            }
            catch (Exception ex)
            {
                txtStatus.Text = ex.Message;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult oResult = MessageBox.Show("Are you sure you want to delete this record?", "Delete Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                if (oResult == DialogResult.Yes)
                {
                    int iIndex = dgvPayroll.CurrentRow.Index;

                    btnClear_Click(sender, e);

                    oPayrolls.Items[iIndex].Delete();
                    oPayrolls.RemoveAt(iIndex);

                    dgvPayroll.DataSource = null;
                    dgvPayroll.DataSource = oPayrolls.Items;
                }
            }
            catch (Exception ex)
            {
                txtStatus.Text = ex.Message;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvPayroll.ClearSelection();

            txtEmployeeId.Clear();
            txtCheckNo.Clear();
            dtCheckDate.Value = DateTime.Now;
            txtCheckAmount.Clear();
            txtHoursWorked.Clear();

            btnAdd.Text = "Add";
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private double currentValue = 0;
        private string currentOperator = "";
        private bool isNewEntry = true;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
        }

        private void numbers_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                if (isNewEntry)
                {
                    txtMain.Text = "";
                    isNewEntry = false;
                }

                if (btn.Text == "." && !txtMain.Text.Contains("."))
                {
                    txtMain.Text += ".";
                }
                else if (btn.Text != ".")
                {
                    txtMain.Text += btn.Text;
                }

                lblStatus.Text = "وضعیت: در حال ورود";
            }
        }

        private void Operators_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && !isNewEntry)
            {
                if (currentOperator != "")
                {
                    Calculate();
                }
                else
                {
                    currentValue = double.Parse(txtMain.Text);
                }

                currentOperator = btn.Text;
                isNewEntry = true;
                lblStatus.Text = $"وضعیت: {currentValue} {currentOperator}";
            }
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            Calculate();
            currentOperator = "";
            isNewEntry = true;
            lblStatus.Text = $"وضعیت: نتیجه = {txtMain.Text}";
        }

        private void Calculate()
        {
            if (currentOperator == "" || isNewEntry)
                return;

            double secondValue = double.Parse(txtMain.Text);
            double result = 0;

            switch (currentOperator)
            {
                case "+":
                    result = currentValue + secondValue;
                    break;
                case "-":
                    result = currentValue - secondValue;
                    break;
                case "*":
                    result = currentValue * secondValue;
                    break;
                case "/":
                    if (secondValue != 0)
                        result = currentValue / secondValue;
                    else
                    {
                        txtMain.Text = "خطا";
                        lblStatus.Text = "وضعیت: خطا";
                        currentOperator = "";
                        isNewEntry = true;
                        return;
                    }
                    break;
            }

            txtMain.Text = result.ToString();
            currentValue = result;
        }

        private void BtnCE_Click(object sender, EventArgs e)
        {
            txtMain.Text = "0";
            isNewEntry = true;
            lblStatus.Text = "وضعیت: آخرین عملیات پاک شد";
        }

        private void BtnC_Click(object sender, EventArgs e)
        {
            currentValue = 0;
            currentOperator = "";
            txtMain.Text = "0";
            isNewEntry = true;
            lblStatus.Text = "وضعیت: همه چیز پاک شد";
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9))
            {
                int num = e.KeyCode - Keys.NumPad0;
                Button fakeBtn = new Button();
                fakeBtn.Text = num.ToString();
                numbers_Click(fakeBtn, null);
                e.Handled = true;
            }
            else if ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9))
            {
                int num = e.KeyCode - Keys.D0;
                Button fakeBtn = new Button();
                fakeBtn.Text = num.ToString();
                numbers_Click(fakeBtn, null);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Decimal || e.KeyCode == Keys.OemPeriod)
            {
                Button fakeBtn = new Button();
                fakeBtn.Text = ".";
                numbers_Click(fakeBtn, null);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Add)
            {
                Button fakeBtn = new Button();
                fakeBtn.Text = "+";
                Operators_Click(fakeBtn, null);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
            {
                Button fakeBtn = new Button();
                fakeBtn.Text = "-";
                Operators_Click(fakeBtn, null);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Multiply)
            {
                Button fakeBtn = new Button();
                fakeBtn.Text = "*";
                Operators_Click(fakeBtn, null);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Divide)
            {
                Button fakeBtn = new Button();
                fakeBtn.Text = "/";
                Operators_Click(fakeBtn, null);
                e.Handled = true;
            }

            else if (e.KeyCode == Keys.Enter)
            {
                btnEqual_Click(null, null);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.C)
            {
                BtnC_Click(null, null);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.E)
            {
                BtnCE_Click(null, null);
                e.Handled = true;
            }
        }

        private void btnPoint_Click(object sender, EventArgs e)
        {
            if (!txtMain.Text.Contains("."))
            {
                txtMain.Text += ".";
                Calculate();
            }
        }
    }
}
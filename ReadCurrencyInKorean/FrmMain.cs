using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadCurrencyInKorean
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        #region 사용자 함수
        private void SetValues()
        {
            StringBuilder sb = new StringBuilder();
            foreach (char chr in TxtNumber.Text)
            {
                if (Char.IsDigit(chr)) { sb.Append(chr.ToString()); }
            }
            string inputStr = sb.ToString();

            if (inputStr == "" || inputStr.Length > 20) { inputStr = "0"; }
            if (inputStr.Length > 1 && inputStr[0] == '0')
            {
                inputStr = inputStr.Substring(1);
            }
            Globals.CurrencyString = inputStr;
            LblFormatted.Text = FormatString(Globals.CurrencyString);
            TxtInKorean.Text = Globals.ReadNumber();
        }

        private string FormatString(string aStr)
        {
            char[] tmpChr = aStr.ToCharArray();
            Array.Reverse(tmpChr);
            StringBuilder sb = new StringBuilder();
            int count = 1;
            foreach (var chr in tmpChr)
            {
                sb.Insert(0, chr);
                if (count == 3)
                {
                    sb.Insert(0, ',');
                    count = 1;
                }
                else { count++; }
            }

            string result = sb.ToString();
            if (result[0] == ',') { result = result.Substring(1); }
            return result;
        }
        #endregion 사용자 함수

        private void TxtNumber_TextChanged(object sender, EventArgs e)
        {
            SetValues();
        }

        private void TxtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            SetValues();
        }
    }
}

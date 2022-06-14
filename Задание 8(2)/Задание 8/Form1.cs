using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Задание_8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        GameBox MOD = new GameBox();
        private void buttonRead_Click(object sender, EventArgs e)
        {
            MOD.Reader();
            MOD.Writer(richTextBox1);
            MOD.Poisk();
            MOD.MoveBons();
            MOD.Writeruthere(richTextBox2);
            MessageBox.Show("Максимальное число костей : "+ MOD.TotalRemBones.ToString());
        }
    }
}

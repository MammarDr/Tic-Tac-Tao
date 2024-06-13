using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tict_Tao
{
    public partial class startGame : Form
    {
        public startGame()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pvp_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(Form1.enGameMode.enPvp);
            form1.ShowDialog();
        }

        private void pvc_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(Form1.enGameMode.enNpc);
            form1.ShowDialog();
        }
    }
}

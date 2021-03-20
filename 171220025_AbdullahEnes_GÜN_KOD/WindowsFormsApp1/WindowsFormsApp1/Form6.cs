using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        public void ResimDegistir(Image rsm)
        {
            pictureBox1.Image = rsm;
        }

        public Image resim;
        public Image resim2;

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.resim = resim2;
            frm.ResimDegistir(resim2);
            frm.Show();

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                pictureBox1.Image.Save(Application.StartupPath + "\\resimler\\Deneme.jpg");
            }
            else if(comboBox1.SelectedIndex == 1)
            {
                pictureBox1.Image.Save(Application.StartupPath + "\\resimler\\Deneme.bmp");

            }
            else if(comboBox1.SelectedIndex == 2)
            {
                pictureBox1.Image.Save(Application.StartupPath + "\\resimler\\Deneme.png");
            }
            Application.Exit();
        }
    }
}

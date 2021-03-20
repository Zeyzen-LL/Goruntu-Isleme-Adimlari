using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Image resim;

        public void ResimDegistir(Image rsm)
        {
            pictureBox1.Image = rsm;
        }

        public void buttonIleriOnOff()
        {
            button2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |" + "*.bmp;*.jpeg;*.jpg;*.png";
            dosya.Title = "Resim Seçme";
            dosya.ShowDialog();
            string DosyaYolu = dosya.FileName;
            resim = Image.FromFile(DosyaYolu);

            pictureBox1.Image = resim;
            button2.Enabled = true;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.resim = resim;
            frm.resim2 = resim;
            frm.ResimDegistir(resim);
            frm.comboboxEnabled();
            frm.Show();
            this.Hide();
        }
    }
}

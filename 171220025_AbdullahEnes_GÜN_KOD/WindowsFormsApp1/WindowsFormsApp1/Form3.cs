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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        
        public Image resim;
        public Image resim2;

        public void ResimDegistir(Image rsm)
        {
            pictureBox1.Image = rsm;
        }

        public void comboboxEnabled()
        {
            if (radioButton2.Checked == true) comboBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.resim = resim2;
            frm.ResimDegistir(resim2);
            frm.Show();

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resim = pictureBox1.Image;
            Form6 frm = new Form6();
            frm.resim = resim;
            frm.resim2 = resim;
            frm.ResimDegistir(resim);
            frm.Show();

            this.Hide();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) comboBox1.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                pictureBox1.Image = resim2;
                Color clr;
                Bitmap orjPic, blurredPic;
                orjPic = new Bitmap(resim);
                blurredPic = new Bitmap(orjPic.Width, orjPic.Height);

                int filtreBoyut = 7;
                int x, y, i, j, toplamR, toplamG, toplamB, ortalamaR, ortalamaG, ortalamaB;

                for (x = (filtreBoyut - 1) / 2; x < orjPic.Width - (filtreBoyut - 1) / 2; x++)
                {
                    for (y = (filtreBoyut - 1) / 2; y < orjPic.Height - (filtreBoyut - 1) / 2; y++)
                    {
                        toplamR = 0;
                        toplamG = 0;
                        toplamB = 0;
                        for (i = -((filtreBoyut - 1) / 2); i <= (filtreBoyut - 1) / 2; i++)
                        {
                            for (j = -((filtreBoyut - 1) / 2); j <= (filtreBoyut - 1) / 2; j++)
                            {
                                clr = orjPic.GetPixel(x + i, y + j);
                                toplamR = toplamR + clr.R;
                                toplamG = toplamG + clr.G;
                                toplamB = toplamB + clr.B;
                            }
                        }
                        ortalamaR = toplamR / (filtreBoyut * filtreBoyut);
                        ortalamaG = toplamG / (filtreBoyut * filtreBoyut);
                        ortalamaB = toplamB / (filtreBoyut * filtreBoyut);
                        blurredPic.SetPixel(x, y, Color.FromArgb(ortalamaR, ortalamaG, ortalamaB));
                    }
                }
                pictureBox1.Image = blurredPic;

            }
            else if (comboBox1.SelectedIndex == 1)
            {
                pictureBox1.Image = resim2;
                Color clr;
                Bitmap orjPic, sharpPic;
                orjPic = new Bitmap(resim);
                sharpPic = new Bitmap(orjPic.Width, orjPic.Height);

                int filtreBoyut = 3;
                int x, y, i, j, toplamR, toplamG, toplamB;
                int R, G, B;
                int[] Matris = { 0, -2, 0, -2, 11, -2, 0, -2, 0 };

                for (x = (filtreBoyut - 1) / 2; x < orjPic.Width - (filtreBoyut - 1) / 2; x++)
                {
                    for (y = (filtreBoyut - 1) / 2; y < orjPic.Height - (filtreBoyut - 1) / 2; y++)
                    {
                        toplamR = 0;
                        toplamG = 0;
                        toplamB = 0;

                        int k = 0;
                        for (i = -((filtreBoyut - 1) / 2); i <= (filtreBoyut - 1) / 2; i++)
                        {
                            for (j = -((filtreBoyut - 1) / 2); j <= (filtreBoyut - 1) / 2; j++)
                            {
                                clr = orjPic.GetPixel(x + i, y + j);
                                toplamR += clr.R * Matris[k];
                                toplamG += clr.G * Matris[k];
                                toplamB += clr.B * Matris[k];
                                k++;
                            }
                        }
                        R = toplamR/3;
                        G = toplamG/3;
                        B = toplamB/3;
                        
                        if (R > 255) R = 255;
                        else if (R < 0) R = 0;
                        if (G > 255) G = 255;
                        else if (G < 0) G = 0;
                        if (B > 255) B = 255;
                        else if (B < 0) B = 0;

                        sharpPic.SetPixel(x, y, Color.FromArgb(R, G, B));
                    }
                }
                pictureBox1.Image = sharpPic;

            }
            else if (comboBox1.SelectedIndex == 2)
            {
                pictureBox1.Image = resim2;
                Color clr;
                Bitmap orjPic, rbtPic;
                orjPic = new Bitmap(resim);
                rbtPic = new Bitmap(orjPic.Width, orjPic.Height);

                int x, y;
                int P1, P2, P3, P4;

                for (x = 0; x < orjPic.Width - 1; x++) 
                {
                    for (y = 0; y < orjPic.Height - 1; y++)
                    {
                        clr = orjPic.GetPixel(x, y);
                        P1 = (clr.R + clr.G + clr.B) / 3;
                        clr = orjPic.GetPixel(x + 1, y);
                        P2 = (clr.R + clr.G + clr.B) / 3;
                        clr = orjPic.GetPixel(x, y + 1);
                        P3 = (clr.R + clr.G + clr.B) / 3;
                        clr = orjPic.GetPixel(x + 1, y + 1);
                        P4 = (clr.R + clr.G + clr.B) / 3;

                        int Gx = Math.Abs(P1 - P4); 
                        int Gy = Math.Abs(P2 - P3); 
                        int RobertCrossDegeri = Gx + Gy; 
                   
                        if (RobertCrossDegeri > 255) RobertCrossDegeri = 255; 

                        rbtPic.SetPixel(x, y, Color.FromArgb(RobertCrossDegeri, RobertCrossDegeri, RobertCrossDegeri));
                    }
                }
                pictureBox1.Image = rbtPic;

            }
        }
    }
}

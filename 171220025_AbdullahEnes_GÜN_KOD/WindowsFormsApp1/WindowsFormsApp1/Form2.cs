using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
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
            Form1 frm = new Form1();
            frm.resim = resim2;
            frm.ResimDegistir(resim2);
            frm.buttonIleriOnOff();
            frm.Show();
            
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resim = pictureBox1.Image;
            Form3 frm = new Form3();
            frm.resim = resim;
            frm.resim2 = resim;
            frm.ResimDegistir(resim);
            frm.comboboxEnabled();
            frm.Show();

            this.Hide();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) comboBox1.Enabled = true;
        }

        public int[,] histogram(Bitmap resim)
        {
            int[,] hist = new int[3, 256];    
            int a = 0;

            if (resim.PixelFormat == PixelFormat.Format32bppArgb)          // burda ise resmin pixel formatı her 8bit alfa, kırmızı, yeşil ve mavi bileşenleri içeriyorsa a değerini 4 yapıyoruz
                a = 4;
            
            else if (resim.PixelFormat == PixelFormat.Format24bppRgb)      // burda da her 8 bit kırmızı, yeşil ve mavi bileşenleri içeriyorsa a'yı 3 yapıyoruz
                a = 3;
            
            unsafe
            {
                BitmapData data = resim.LockBits(new Rectangle(0, 0, resim.Width, resim.Height), ImageLockMode.ReadWrite, resim.PixelFormat);
                
                byte* z = (byte*)data.Scan0;         

                for (int i = 0; i < data.Width-1; i++)   
                {
                    for (int j = 0; j < data.Height-1; j++)  
                    {
                        hist[0, z[0]]++;          
                        hist[1, z[1]]++;
                        hist[2, z[2]]++;
                        z += a;

                    }
                }
                resim.UnlockBits(data);         
            }
            return hist;                      
        }

        public void histogramciz(ref int[,] hist)  
        {
            pictureBox1.Refresh();
            Graphics g = pictureBox1.CreateGraphics();      
            Pen pp = new Pen(Color.Red, 1);                            
            for (int i = 0; i < 256; i++)             
            {
                g.DrawLine(pp, new Point(i, pictureBox1.Height), new Point(i, pictureBox1.Height - hist[2, i] / 10));  
            }
            g.Dispose(); 

        }
        
        Point RectStartPoint;
        Point RectEndPoint;      
        Rectangle Rect;
        Pen selectionPen = new System.Drawing.Pen(Color.Blue, 2);
        bool drawing = false;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (drawing == true)
            {
                RectStartPoint = e.Location;
                Invalidate();
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                if (e.Button != MouseButtons.Left)
                    return;
                Point RectEndPoint = e.Location;
                Rect.Location = new Point(
                    Math.Min(RectStartPoint.X, RectEndPoint.X),
                    Math.Min(RectStartPoint.Y, RectEndPoint.Y));
                Rect.Size = new Size(
                    Math.Abs(RectStartPoint.X - RectEndPoint.X),
                    Math.Abs(RectStartPoint.Y - RectEndPoint.Y));
                pictureBox1.Invalidate();
            }
        }
        
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
            RectEndPoint = e.Location;
            Color clr;
            Bitmap orjPic = new Bitmap(pictureBox1.Image);
            int oranW = (int)(orjPic.Width / pictureBox1.Width);
            int oranH = (int)(orjPic.Height / pictureBox1.Height);

            Bitmap yeni = new Bitmap((RectEndPoint.X - RectStartPoint.X) * oranW, (RectEndPoint.Y - RectStartPoint.Y) * oranH);
            
            for(int i = RectStartPoint.X * oranW; i < RectEndPoint.X * oranW; i++)
            {
                for(int j = RectStartPoint.Y * oranH; j < RectEndPoint.Y * oranH ; j++)
                {
                    clr = orjPic.GetPixel(i, j);
                    yeni.SetPixel(i - RectStartPoint.X * oranW, j - RectStartPoint.Y * oranH, clr);
                }
            }
            pictureBox1.Image = yeni;
            Invalidate();
        }
        
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (drawing == true)
            {
                if (pictureBox1.Image != null)
                {
                    if (Rect != null && Rect.Width > 0 && Rect.Height > 0)
                    {
                        e.Graphics.DrawRectangle(selectionPen, Rect);
                    }
                }
            }
        }
        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                pictureBox1.Image = resim2;
                Bitmap clone = new Bitmap(resim);
                for (int i = 0; i < clone.Height - 1; i++) 
                {
                    for (int j = 0; j < clone.Width - 1; j++)  
                    {
                        int deger = Convert.ToInt32(clone.GetPixel(j, i).R * 0.299 + clone.GetPixel(j, i).G * 0.587 + clone.GetPixel(j, i).B * 0.114);
                        Color renk;
                        renk = Color.FromArgb(deger, deger, deger);
                        clone.SetPixel(j, i, renk); 
                    }
                }
                pictureBox1.Image = clone;
            }
            else if(comboBox1.SelectedIndex == 1)
            {
                pictureBox1.Image = resim2;
                drawing = true;

            }
            else if(comboBox1.SelectedIndex == 2)
            {
                pictureBox1.Image = null;
                Bitmap r = new Bitmap(resim);
                int[,] a = histogram(r);
                histogramciz(ref a);
            }
        }

    }
}

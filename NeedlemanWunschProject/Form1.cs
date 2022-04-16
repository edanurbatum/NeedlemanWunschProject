using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeedlemanWunschProject
{
    public partial class Form1 : Form
    {
        int sayac=0;
        public Form1()
        {
            InitializeComponent();
        }
        public void degerleriAl()
        {
            int match, mismatch, gap;

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

            if (textBox1.Text.Length == 0)
            {
                match = 1;
            }
            else
            {
                match = Convert.ToInt32(textBox1.Text);
            }

            if (textBox2.Text.Length == 0)
            {
                mismatch = -1;
            }
            else
            {
                mismatch = Convert.ToInt32(textBox2.Text);
            }

            if (textBox3.Text.Length == 0)
            {
                gap = -2;
            }
            else
            {
                gap = Convert.ToInt32(textBox3.Text);
            }

            textBox1.Text = match.ToString();
            textBox2.Text = mismatch.ToString();
            textBox3.Text = gap.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text|*.txt|All|*.*";
            openFileDialog.Multiselect = false;

            string filePath = openFileDialog.FileName;


            if (openFileDialog.ShowDialog() == DialogResult.OK) //dialog açıldıktan sonra
            {
                textBox4.Clear();//text kutusunu boşaltma
                filePath = openFileDialog.FileNames[0];

                //dosyayı okuma modunda açıyoruz
                FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);

                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 0)
                {
                    textBox6.Text = lines[0];
                    textBox4.Text = lines[1];
                }
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {

            int boyut1 = Convert.ToInt32(textBox6.Text);
            int boyut2 = Convert.ToInt32(textBox7.Text);

            //textboxlarda yazan dizilimleri diziye atma
            string metin = textBox4.Text;
            string[] dizin1 = new string[boyut1];

            string metin2 = textBox5.Text;
            string[] dizin2 = new string[boyut2];

            for (int i = 0; i < metin.Length; i++)
            {
                dizin1[i] = metin[i].ToString();
            }

            for (int i = 0; i < metin2.Length; i++)
            {
                dizin2[i] = metin2[i].ToString();
            }

            degerleriAl();
            gridviewDuzenle(dizin1, dizin2);
            hizala(dizin1, dizin2);
            toparla(dizin1, dizin2);
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text|*.txt|All|*.*";
            openFileDialog.Multiselect = false;

            string filePath = openFileDialog.FileName;


            if (openFileDialog.ShowDialog() == DialogResult.OK) //dialog açıldıktan sonra
            {
                textBox5.Clear();//text kutusunu boşaltma
                filePath = openFileDialog.FileNames[0];

                //dosyayı okuma modunda açıyoruz
                FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);

                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 0)
                {
                    textBox7.Text = lines[0];
                    textBox5.Text = lines[1];
                }

            }
        }

        void gridviewDuzenle(string[] dizin1, string[] dizin2)
        {

            DataTable tablo = new DataTable();

            string header1 = " ";
            string header2 = "";
            tablo.Columns.Add(header1);
            tablo.Columns.Add(header1 + header1);

            DataRow row1 = tablo.NewRow();
            tablo.Rows.Add(row1);
            tablo.Rows.Add(header1);

            dataGridView1.DataSource = tablo;

            for (int i = 0; i < dizin1.Length; i++)
            {
                tablo.Columns.Add(header2);
                header2 += header2;
            }

            for (int i = 0; i < dizin2.Length; i++)//aşağı doğru olanlar
            {
                DataRow row = tablo.NewRow();
                row[header1] = dizin2[i];
                tablo.Rows.Add(row);
                dataGridView1.DataSource = tablo;
            }


            for (int i = 1; i < dizin1.Length + 1; i++)
            {
                dataGridView1.Rows[0].Cells[i + 1].Value = dizin1[i - 1];
            }

            dataGridView1.Rows[1].Cells[1].Value = 0;

        }

        public int dizilimKarsilastirma(int j, int i)
        {
            int match = Convert.ToInt32(textBox1.Text);
            int mismatch = Convert.ToInt32(textBox2.Text);

            int sonuc = 0;

            if (String.Compare(dataGridView1.Rows[0].Cells[i + 1].Value.ToString(), dataGridView1.Rows[j + 1].Cells[0].Value.ToString()) == 0)
            {
                sonuc = match;
            }
            else
            {
                sonuc = mismatch;
            }
            return sonuc;
        }

        public int islemlerSonuc(int formul1, int formul2, int formul3)
        {
            int enbuyuk = formul1;
            int sonuc = formul1;//geçici değişken atıyoruz

            if (formul1 > formul2 && formul1 > formul3)
            {
                enbuyuk = formul1;
            }
            else if (formul2 > formul3)
            {
                enbuyuk = formul2;
            }
            else if (formul3 > formul2)
            {
                enbuyuk = formul3;
            }
            sonuc = enbuyuk;
            return sonuc;
        }
        void hizala(string[] dizin1, string[] dizin2)
        {
            int gap = Convert.ToInt32(textBox3.Text);
            int t1 = 0, t2 = 0, t3 = 0;
            Random rs = new Random(1);

            for (int j = 1; j < dizin2.Length + 2; j++)//cell
            {

                for (int i = 1; i < dizin1.Length + 2; i++)//row
                {
                    if (i == 1 && j == 1)
                    {

                    }
                    else if (i - 1 >= 1 && j - 1 >= 1)
                    {
                        int karsilastirma = dizilimKarsilastirma(i - 1, j - 1);

                        int parca1 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j - 1].Value);
                        t1 = karsilastirma + parca1;

                        int parca2 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value);
                        t2 = gap + parca2;

                        int parca3 = Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value);
                        t3 = gap + parca3;
                    }
                    else if (i - 1 >= 1 && j >= 1)
                    {
                        int parca2 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value);
                        t2 = gap + parca2;
                        t1 = rs.Next(-50, t2);
                        t3 = rs.Next(-50, t2);
                    }
                    else if (i >= 1 && j - 1 >= 1)
                    {
                        int parca3 = Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value);
                        t3 = gap + parca3;
                        t1 = rs.Next(-50, t3);
                        t2 = rs.Next(-50, t3);
                    }

                    int sonucc = islemlerSonuc(t1, t2, t3);
                    dataGridView1.Rows[i].Cells[j].Value = sonucc;
                }
            }
        }


        void toparla(string[] dizin1, string[] dizin2)
        {
            int i = (dizin1.Length) + 1;
            int j = (dizin2.Length) + 1;

            ArrayList iDegerleri = new ArrayList();
            ArrayList jDegerleri = new ArrayList();
            ArrayList komsular = new ArrayList();

            int ilkDeger = Convert.ToInt32(dataGridView1.Rows[j].Cells[i].Value);
            dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.LightGreen;
            int sonDeger = Convert.ToInt32(dataGridView1.Rows[1].Cells[1].Value);
            dataGridView1.Rows[1].Cells[1].Style.BackColor = Color.LightGreen;

            iDegerleri.Add(i);
            jDegerleri.Add(j);
            komsular.Add(ilkDeger);
            komsular.Add(sonDeger);

            int komsuDeger1, komsuDeger2, komsuDeger3;
            int karsilastirma = 0;

            while (i > 1 && j > 1)
            {
                komsuDeger1 = Convert.ToInt32(dataGridView1.Rows[j].Cells[i - 1].Value);
                komsuDeger2 = Convert.ToInt32(dataGridView1.Rows[j - 1].Cells[i - 1].Value);
                komsuDeger3 = Convert.ToInt32(dataGridView1.Rows[j - 1].Cells[i].Value);

                karsilastirma = dizilimKarsilastirma(j - 1, i - 1);

                if (karsilastirma == 1)
                {
                    j = j - 1;
                    i = i - 1;
                    iDegerleri.Add(i);
                    jDegerleri.Add(j);
                    komsular.Add(komsuDeger2);
                    dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.LightGreen;
                }
                else if (karsilastirma == -1)
                {
                    int enBuyukKomsu = enBuyukKomsuyuBul(komsuDeger1, komsuDeger2, komsuDeger3);

                    if (enBuyukKomsu == komsuDeger2)
                    {
                        j = j - 1;
                        i = i - 1;
                        iDegerleri.Add(i);
                        jDegerleri.Add(j);
                        komsular.Add(komsuDeger2);
                        dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.LightGreen;
                    }

                    else if (enBuyukKomsu == komsuDeger1)
                    {
                        i = i - 1;
                        iDegerleri.Add(i);
                        jDegerleri.Add(j);
                        komsular.Add(komsuDeger1);
                        dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.LightGreen;
                    }
                    else if (enBuyukKomsu == komsuDeger3)
                    {
                        j = j - 1;
                        iDegerleri.Add(i);
                        jDegerleri.Add(j);
                        komsular.Add(komsuDeger3);
                        dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.LightGreen;
                    }
                }
            }
            dizilimiYap(iDegerleri, jDegerleri);
        }

        public int enBuyukKomsuyuBul(int komsu1, int komsu2, int komsu3)
        {
            int geciciEnBuyuk = komsu1;
            int enBuyuk = komsu1;//geçici değişken atıyoruz

            if (komsu1 > komsu2 && komsu1 > komsu3)
            {
                geciciEnBuyuk = komsu1;
            }
            else if (komsu2 > komsu1 && komsu2 > komsu3)
            {
                geciciEnBuyuk = komsu2;
            }
            else/* if (komsu3 > komsu2 && komsu3 > komsu1)*/
            {
                geciciEnBuyuk = komsu3;
            }
            enBuyuk = geciciEnBuyuk;
            return enBuyuk;
        }

        void dizilimiYap(ArrayList liste1, ArrayList liste2)
        {
            ArrayList dizilim1 = new ArrayList();
            ArrayList dizilim2 = new ArrayList();

            for (int a = liste1.Count - 1; a >= 0; a--)//a=listelerde de gezienen indis değeri
            {
                int i = Convert.ToInt32(liste1[a]);
                int j = Convert.ToInt32(liste2[a]);

                if (dataGridView1.Rows[0].Cells[i].Value.ToString() == "")
                {
                    dizilim1.Add("--");
                }
                else if (dataGridView1.Rows[j].Cells[0].Value.ToString() == "")
                {
                    dizilim2.Add("--");
                }
                else if (Convert.ToInt32(liste1[a]) == Convert.ToInt32(liste1[a + 1]))
                {
                    dizilim1.Add("--");
                }
                else if (Convert.ToInt32(liste2[a]) == Convert.ToInt32(liste2[a + 1]))
                {
                    dizilim2.Add("--");
                }
                else
                {
                    dizilim1.Add(dataGridView1.Rows[0].Cells[i].Value);
                    dizilim2.Add(dataGridView1.Rows[j].Cells[0].Value);
                }
            }

            foreach (var item in dizilim1)
            {
                textBox8.Text += item.ToString();
            }

            foreach (var item in dizilim2)
            {
                textBox9.Text += item.ToString();
            }
            int match = Convert.ToInt32(textBox1.Text);
            int mismatch = Convert.ToInt32(textBox2.Text);
            int gap = Convert.ToInt32(textBox3.Text);
            int skor = 0;

            for (int i = 0; i < dizilim1.Count-1; i++)
            {
                if (dizilim1[i].ToString() == dizilim2[i].ToString())
                {
                    skor += match;
                }
                else if (dizilim1[i].ToString() != dizilim2[i].ToString())
                {
                    skor += mismatch;
                }
                else if (dizilim1[i].ToString() == "--" || dizilim2[i].ToString() == "--")
                {
                    skor += gap;
                }
            }

            textBox10.Text = skor.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            label9.Text = sayac.ToString();
        }
    }
}

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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            degerleriAl();

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

            gridviewDuzenle(dizin1, dizin2);
            hizala(dizin1, dizin2);
            toparla2(dizin1, dizin2);
            //toparla3(dizin1, dizin2);

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
        public void degerleriAl()
        {
            int match, mismatch, gap;

            if (textBox1.Text == "")
            {
                match = 1;
                textBox1.Text = match.ToString();
            }
            else
            {
                match = Convert.ToInt32(textBox1.Text);
            }

            if (textBox2.Text == "")
            {
                mismatch = -1;
                textBox2.Text = mismatch.ToString();
            }
            else
            {
                mismatch = Convert.ToInt32(textBox2.Text);
            }

            if (textBox3.Text == "")
            {
                gap = -2;
                textBox3.Text = gap.ToString();
            }
            else
            {
                gap = Convert.ToInt32(textBox3.Text);
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
            listBox1.Items.Add(dataGridView1.Rows[0].Cells[2].Value);

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
            List<int> iDegerleri = new List<int>();
            List<int> jDegerleri = new List<int>();

            int ilkJ = dizin1.Length + 1;
            int ilkI = dizin2.Length + 1;
            int ilkDeger = Convert.ToInt32(dataGridView1.Rows[ilkJ].Cells[ilkI].Value);
            iDegerleri.Add(ilkI);
            jDegerleri.Add(ilkJ);
            int sonrakiDeger, komsuDeger1, komsuDeger2, komsuDeger3;

            for (int i = ilkI; i > 1; i--)//cell
            {
                for (int j = ilkJ; j > 1; j--)//row
                {
                    komsuDeger1 = Convert.ToInt32(dataGridView1.Rows[j - 1].Cells[i].Value);
                    komsuDeger2 = Convert.ToInt32(dataGridView1.Rows[j - 1].Cells[i - 1].Value);
                    komsuDeger3 = Convert.ToInt32(dataGridView1.Rows[j].Cells[i - 1].Value);
                    int enBuyukKomsu = enBuyukKomsuyuBul(komsuDeger1, komsuDeger2, komsuDeger3);

                    if (enBuyukKomsu == komsuDeger1)
                    {
                        j = j - 1;
                        jDegerleri.Add(j - 1);
                        iDegerleri.Add(i);
                    }
                    else if (enBuyukKomsu == komsuDeger2)
                    {
                        jDegerleri.Add(j - 1);
                        iDegerleri.Add(i - 1);
                    }
                    else if (enBuyukKomsu == komsuDeger3)
                    {
                        jDegerleri.Add(j);
                        iDegerleri.Add(i - 1);
                    }

                }
            }

            //foreach (var item in iDegerleri)
            //{
            //    listBox1.Items.Add("i"+item);
            //}
            //foreach (var item in jDegerleri)
            //{
            //    listBox1.Items.Add("j"+item);
            //}

        }

        void toparla2(string[] dizin1, string[] dizin2)
        {
            ArrayList iDegerleri = new ArrayList();
            ArrayList jDegerleri = new ArrayList();
            ArrayList komsular = new ArrayList();

            int i = (dizin1.Length) + 1;
            int j = (dizin2.Length) + 1;

            int ilkDeger = Convert.ToInt32(dataGridView1.Rows[j].Cells[i].Value);
            dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.LightGreen;

            iDegerleri.Add(i);
            jDegerleri.Add(j);
            komsular.Add(ilkDeger);

            int komsuDeger1, komsuDeger2, komsuDeger3;

            while (i > 1 && j > 1)
            {
                komsuDeger1 = Convert.ToInt32(dataGridView1.Rows[j].Cells[i - 1].Value);
                komsuDeger2 = Convert.ToInt32(dataGridView1.Rows[j - 1].Cells[i - 1].Value);
                komsuDeger3 = Convert.ToInt32(dataGridView1.Rows[j - 1].Cells[i].Value);

                int enBuyukKomsu = enBuyukKomsuyuBul(komsuDeger1, komsuDeger2, komsuDeger3);


                if (enBuyukKomsu == komsuDeger2)
                {
                    j = j - 1;
                    i = i - 1;
                    iDegerleri.Add(i);
                    jDegerleri.Add(j);
                    komsular.Add(komsuDeger2);
                    dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.LightGreen;
                    //listBox1.Items.Add(enBuyukKomsu);
                }

                else if (enBuyukKomsu == komsuDeger1)
                {
                    i = i - 1;
                    iDegerleri.Add(i);
                    jDegerleri.Add(j);
                    komsular.Add(komsuDeger1);
                    dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.LightGreen;
                    //listBox1.Items.Add(enBuyukKomsu);
                }
                else if (enBuyukKomsu == komsuDeger3)
                {
                    j = j - 1;
                    iDegerleri.Add(i);
                    jDegerleri.Add(j);
                    komsular.Add(komsuDeger3);
                    dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.LightGreen;
                    //listBox1.Items.Add(enBuyukKomsu);
                }
            }
            //foreach (var item in jDegerleri)
            //{
            //    listBox1.Items.Add("j=  " + item);
            //}
            //foreach (var item in iDegerleri)
            //{
            //    listBox2.Items.Add("i=  " + item);
            //}
            foreach (var item in komsular)
            {
                listBox1.Items.Add(item);
            }
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
            else if (komsu3 > komsu2 && komsu3 > komsu1)
            {
                geciciEnBuyuk = komsu3;
            }
            enBuyuk = geciciEnBuyuk;
            return enBuyuk;
        }
    }
}

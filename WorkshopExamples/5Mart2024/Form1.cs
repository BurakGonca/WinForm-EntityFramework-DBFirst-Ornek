using _5Mart2024.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace _5Mart2024
{
    public partial class Form1 : Form
    {
        GoncacompanydbContext db = new GoncacompanydbContext();

        Customer secilenMusteri;

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = db.Customers.ToList();

            
        }

        private void button1_Click(object sender, EventArgs e) //ekleme butonu
        {
            if (secilenMusteri != null)
            {
                Customer newCustomer = new Customer();
                newCustomer.CustomerId = textBox1.Text;
                newCustomer.CompanyName = textBox2.Text;

                db.Customers.Add(newCustomer);
                db.SaveChanges();
                MessageBox.Show("Basariyla Eklenmistir!");
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = newCustomer;
                dataGridView1.DataSource = db.Customers.ToList();

            }
            else
                MessageBox.Show("Secili müsteri yok!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //silme butonu
        {

            if (secilenMusteri != null)
            {
                //mouse clickle secim islemi yapiyoruz
                var selectedIndex = dataGridView1.CurrentRow.Cells[0].Value;

                //var selectedIndex2 = dataGridView1.SelectedRows[0].DataBoundItem;
                //diger secim yontemi

                //burada yakalama yapiyoruz
                var selectCustomer = db.Customers.FirstOrDefault(e => e.CustomerId == selectedIndex);

                //silme islemi ve listelemeyi refresh'leme islemi asagida
                db.Customers.Remove(selectCustomer);
                db.SaveChanges();
                MessageBox.Show("Ilgili musteri basariyla silinmistir.");
                dataGridView1.DataSource = db.Customers.ToList();

            }
            else
                MessageBox.Show("Secili müsteri yok!");


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            secilenMusteri = (Customer)dataGridView1.SelectedRows[0].DataBoundItem;
            textBox1.Text = secilenMusteri.CustomerId;
            textBox2.Text = secilenMusteri.CompanyName;

        }

        private void button3_Click(object sender, EventArgs e) // güncelle butonu
        {
            //secilenMusteri dataGridView1 de seciliyor zaten , tekrar yazmiyoruz
            if (secilenMusteri != null)
            {

                //secilenMusteri.CustomerId = textBox1.Text; //degistiremiyoruz cünkü ýd primary
                secilenMusteri.CompanyName = textBox2.Text;
                db.Customers.Update(secilenMusteri);
                db.SaveChanges();
                textBox1.Clear();
                textBox2.Clear();
                MessageBox.Show("Güncelleme basariyla yapilmistir.");
                dataGridView1.DataSource = db.Customers.ToList();
                secilenMusteri = null;
            }
            else
                MessageBox.Show("Secili müsteri yok!");

        }





        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (comboBox1.SelectedItem.ToString().Trim())
            {
                case "Tüm employee":
                    var tumEmployee = db.Employees.ToList();
                    ComboBoxSecim(tumEmployee);
                    break;
                case "Ýlk employee":
                    var ilkEmployee = db.Employees.FirstOrDefault();
                    ComboBoxSecim(ilkEmployee);
                    break;
                case "Ýsmi a ile baslayan employee'ler":
                    var ismiAileBaslayan = db.Employees.Where(a => a.FirstName.StartsWith("A")).ToList();
                    ComboBoxSecim(ismiAileBaslayan);
                    break;
                case "Ýsmi içerisinde A harfini iceren employeeler":
                    var ismiIcýndeAolanlar = db.Employees.Where(a => a.FirstName.Contains("A")).ToList();
                    ComboBoxSecim(ismiIcýndeAolanlar);
                    break;
                case "Ýsmi içerisinde A harfini iceren ilk employee":
                    var ismiIcýndeAolanIlk = db.Employees.Where(a => a.FirstName.Contains("A")).FirstOrDefault();
                    ComboBoxSecim(ismiIcýndeAolanIlk);
                    break;
                case "Adý Andrew olan employee'ler":
                    var adiAndrewOlanlar = db.Employees.Where(andrew => andrew.FirstName.Contains("Andrew")).ToList();
                    ComboBoxSecim(adiAndrewOlanlar);
                    break;
                case "En pahalý product":
                    var enPahaliProduct = db.Products.Max(p => p.UnitPrice);
                    ComboBoxSecim(enPahaliProduct);
                    break;
                case "En ucuz product":
                    var enUcuzProduct = db.Products.Min(p => p.UnitPrice);
                    ComboBoxSecim(enUcuzProduct);
                    break;
                case "Product'larýn ortalama fiyatý":
                    var ortalamaProduct = db.Products.Average(p => p.UnitPrice);
                    ComboBoxSecim(ortalamaProduct);
                    break;
                case "Product'larýn fiyatlarý toplamý":
                    var productFiyatToplami = db.Products.Sum(p => p.UnitPrice);
                    ComboBoxSecim(productFiyatToplami);
                    break;
                case "Ýsmi a ile baslayan ilk employee":
                    var aileBaslayanIlkKisi = db.Employees.Where(a => a.FirstName.StartsWith("A")).FirstOrDefault();
                    ComboBoxSecim(aileBaslayanIlkKisi);
                    break;
                default:
                    break;
            }



        }



        private void comboBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {


        }

        private void ComboBoxSecim(Employee employee)
        {
            List<Employee> liste = new List<Employee>();
            liste?.Add(employee);
            dataGridView2.DataSource = liste?.ToList();

        }

        private void ComboBoxSecim(List<Employee> listEmployee)
        {

            dataGridView2.DataSource = listEmployee;

        }

        private void ComboBoxSecim(decimal? sonuc)
        {
            label3.Text = sonuc.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}


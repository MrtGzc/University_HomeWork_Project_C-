using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Runtime.Remoting.Lifetime;



namespace WindowsFormsApp2
{
    public partial class Form1 : Form

    {
        SQLiteConnection conn;
        SQLiteCommand cmd;
        public Form1()
        {
            InitializeComponent();

            // kütüphane adında bir veri tabanı oluşturduk
            conn = new SQLiteConnection("Data Source=kutuphane.db;Version=3;");
            // Veritabanı bağlantısı oluştur
            using (SQLiteConnection connection = new SQLiteConnection(conn))
            {
                conn.Open();
                connection.Open();

                // Tablo oluşturma sorgusu
                string createTableQuery = @"CREATE TABLE IF NOT EXISTS books (
                                            BookId INTEGER PRIMARY KEY,
                                            Title NVARCHAR(100),
                                            Author NVARCHAR(100),
                                            ISBN NVARCHAR(50),
                                            Available BIT
                                        )";

                // Tablo oluşturma sorgusunu yürütme
                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Veritabanı ve tablo başarıyla oluşturuldu.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void buttonEkle_Click(object sender, EventArgs e)
        {
            string veriTitle = textBoxTitle.Text;
            string veriAuthor = textBoxAuthor.Text;
            string veriISBN = textBoxISBN.Text;
            string veriAvailable = textBoxAvailable.Text;
            if (string.IsNullOrWhiteSpace(veriTitle) || string.IsNullOrWhiteSpace(veriAuthor) || string.IsNullOrWhiteSpace(veriISBN) || string.IsNullOrWhiteSpace(veriAvailable))
            {
                MessageBox.Show("Hata: Lütfen tüm bilgileri doldurun.");
                return;
            }
            try
            {
                string insertQuery = "INSERT INTO Books (Title, Author, ISBN, Available ) VALUES (@Title, @Author, @ISBN, @Available)";
                SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@Title", veriTitle);
                cmd.Parameters.AddWithValue("@Author", veriAuthor);
                cmd.Parameters.AddWithValue("@ISBN", veriISBN);
                cmd.Parameters.AddWithValue("@Available", veriAvailable);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Veri başarıyla eklendi.");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void buttonSil_Click(object sender, EventArgs e)
        {
            string title= textBoxTitle.Text;
            string author= textBoxAuthor.Text;
            string ISBN = textBoxISBN.Text;
            string available = textBoxAvailable.Text;
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) || string.IsNullOrWhiteSpace(ISBN) || string.IsNullOrWhiteSpace(available))
            {
                MessageBox.Show("Hata: Lütfen tüm bilgileri doldurun.");
                return;
            }
            try
            {
                // Silme sorgusunu hazırla
                string deleteQuery = "DELETE FROM Books WHERE Title = @Title AND Author = @Author AND ISBN = @ISBN AND Available = @Available";

                // SQL sorgusunu çalıştırmak için SQLiteCommand nesnesi oluştur
                SQLiteCommand cmd = new SQLiteCommand(deleteQuery, conn);

                // Parametreleri ekleyerek sorguyu hazırla
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Author", author);
                cmd.Parameters.AddWithValue("@ISBN", ISBN);
                cmd.Parameters.AddWithValue("@Available", available);


                // SQL sorgusunu veritabanında çalıştır
                int affectedRows = cmd.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    MessageBox.Show("Kitap başarıyla silindi.");
                }
                else
                {
                    MessageBox.Show("Silinecek kitap bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }

        private void buttonListele_Click(object sender, EventArgs e)
        {
            ShowBooks();
            
        }

        private void ShowBooks()
        {
            listBoxListe.Items.Clear();
            string selectQuery = @"SELECT * FROM Books";
            cmd = new SQLiteCommand(selectQuery, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string bookInfo = $"{reader["BookId"]}: {reader["Title"]} - {reader["Author"]} - {reader["ISBN"]} ";

            //    string bookInfo = $"{reader["BookId"]}): {reader["Title"]}" +
            //        $"{reader["Author"]}" +
            //        $"{reader["ISBN"]}" +
            //        $"{reader["Available"]}";   
            listBoxListe.Items.Add(bookInfo);
        }
        reader.Close();
            conn.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void labelAuthor_Click(object sender, EventArgs e)
        {

        }

        private void labelTitle_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    } 
}

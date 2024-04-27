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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace proje2
{
    public partial class Form1 : Form

    {
        private const string connectionString = "Data Source=StudentRegistration.db;Version=3;";

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonKayit_Click(object sender, EventArgs e)
        {
            string firstName = textBoxFirstName.Text;
            string lastName = textBoxLastName.Text;
            string email = textBoxEmail.Text;
            DateTime dateOfBirth = DateOfBirth.Value;
            DateTime enrolledDate = DateTime.Now;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName) && !string.IsNullOrWhiteSpace(email) )
                    {
                        connection.Open();
                        string createTableQuery = "CREATE TABLE IF NOT EXISTS Students (StudentId INTEGER PRIMARY KEY AUTOINCREMENT, FirstName TEXT, LastName TEXT, Email TEXT, DateOfBirth DATE, EnrolledDate DATETIME)";
                        using (SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            createTableCommand.ExecuteNonQuery();
                        }
                        string insertQuery = "INSERT INTO Students (FirstName, LastName, Email, DateOfBirth, EnrolledDate) VALUES (@FirstName, @LastName, @Email, @DateOfBirth, @EnrolledDate)";
                        SQLiteCommand command = new SQLiteCommand(insertQuery, connection);
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                        command.Parameters.AddWithValue("@EnrolledDate", enrolledDate);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Öğrenci başarıyla eklendi.");
                    }
                    else
                    {
                        MessageBox.Show("lütfen bütün kutuları doldurunuz");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void buttonListe_Click(object sender, EventArgs e)
        {

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string selectQuery = "SELECT FirstName, LastName, Email, DateOfBirth, EnrolledDate FROM Students";
                    SQLiteCommand command = new SQLiteCommand(selectQuery, connection);
                    SQLiteDataReader reader = command.ExecuteReader();

                    listBoxListe.Items.Clear();
                    listBoxListe.Items.Add("FirstName  LastName  Email  DateOfBirth  EnrolledDate ");


                    while (reader.Read())
                    {
                        string studentInfo = $"{reader["FirstName"]}" +
                                             $" {reader["LastName"]}" +
                                             $" {reader["Email"]}" +
                                             $" {reader["DateOfBirth"]}" +
                                             $" {reader["EnrolledDate"]}";

                        listBoxListe.Items.Add(studentInfo);


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void listBoxListe_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelFirstName_Click(object sender, EventArgs e)
        {

        }

        private void labelLastName_Click(object sender, EventArgs e)
        {

        }

        private void labelEmail_Click(object sender, EventArgs e)
        {

        }

        private void labelDateOfBirth_Click(object sender, EventArgs e)
        {

        }

        private void DateOfBirth_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
# Library Management System

![ProjectImage](./projectsImages/Ekran%20görüntüsü%202024-03-24%20185044.png)

This project is utilized to create a library management system in a Windows Forms application.

- **Adding Books:** Users can add books to the database by entering book information in the required fields on the form.
- **Deleting Books:** When users want to delete a book, they can do so by entering the title, author, ISBN, and availability status of the book they want to delete from the database.
- **Listing Books:** Users can click the "List" button to list all existing books. This retrieves the information of all books from the database and displays them in a list box.

## Code Functionality:

1. **Establishing Database Connection:**
   - An object of the SQLiteConnection class is created to connect to the kutuphane.db SQLite database.
   - The connection string specifies the database file name.
2. **Creating Database and Table:**
   - After establishing the database connection, a table named "books" is created to store the book information.
   - The CREATE TABLE IF NOT EXISTS statement checks if the table exists and creates it if it doesn't.
3. **Adding Books:**

   - The `buttonEkle_Click` method executes when the "Add" button is clicked.
   - It retrieves the book information entered by the user.
   - If all necessary fields are filled, it prepares an INSERT query to add the book information to the Books table in the database.

4. **Deleting Books:**

   - Users enter the title, author, ISBN, and availability status of the book they want to delete and click the "Delete" button.
   - The necessary fields are checked, and if they are not empty, a DELETE FROM query is created with the entered information to delete the book from the database.

5. **Listing Books:**
   - When the "List" button is clicked, all books from the database are retrieved using a SELECT query.
   - The retrieved book information is added to the listBoxListe control to display to the user.

---

# Student Registration System

![ProjectImage](./projectsImages/Ekran%20görüntüsü%202024-03-24%20185143.png)

This C# code utilizes a Windows Forms application to create a student registration system.

- **Adding Student Records:** Users can add students to the database by entering information such as first name, last name, email, and date of birth.
- **Creating Student List:** Clicking the "List" button displays all students' information (first name, last name, email, date of birth, enrollment date) from the database in a list box.

## Functionality:

1. **Establishing Database Connection:**

   - The code uses the SQLiteConnection class to connect to the StudentRegistration.db SQLite database.
   - The connection string specifies the database file name.

2. **Adding Student Records:**

   - The `buttonKayit_Click` method executes when the "Register" button is clicked.
   - It retrieves the student information entered by the user.
   - If all necessary fields are filled, it prepares an INSERT query to add the student information to the Students table in the database.

3. **Listing Students:**
   - When the "List" button is clicked, all students' information from the database is retrieved using a SELECT query.
   - The retrieved student information is added to the listBoxListe control to display to the user.

---

# Kitaplık Yönetim Sistemi

![ProjectImage](./projectsImages/Ekran%20görüntüsü%202024-03-24%20185044.png)

Bu proje, bir Windows Forms uygulamasında bir kitaplık yönetim sistemi oluşturmak için kullanılmıştır.

- **Kitapların Eklenmesi:** Kullanıcılar formdaki gerekli alanlara kitap bilgilerini girerek kitapları veritabanına ekleyebilirler.
- **Kitapların Silinmesi:** Kullanıcılar bir kitabı silmek istediklerinde, kitabın başlığını, yazarını, ISBN numarasını ve mevcut durumunu girerek o kitabı veritabanından silebilirler.
- **Kitapların Listelenmesi:** Kullanıcılar mevcut kitapları listelemek için "Listele" düğmesine tıklayabilirler. Bu, veritabanındaki tüm kitapların bilgilerini bir liste kutusuna getirir.

## Kod İşlevselliği:

1. **Veritabanı Bağlantısı Kurma:**
   - SQLiteConnection sınıfından bir nesne oluşturulur ve kutuphane.db adlı SQLite veritabanına bağlanmak için kullanılır.
   ```C#
   // kütüphane adında bir veri tabanı oluşturduk
        conn = new SQLiteConnection("Data Source=kutuphane.db;Version=3;");
   ```
2. **Veritabanı ve Tablo Oluşturma:**

   - Veritabanı bağlantısı yapıldıktan sonra, Books adında bir tablo oluşturmak için bir sorgu çalıştırılır.
   - CREATE TABLE IF NOT EXISTS ifadesiyle başlayan tablo sorgusu, tablonun var olup olmadığını kontrol eder ve yoksa oluşturur.
   - CREATE TABLE sorgusu, kitapların bilgilerini saklamak için bir tablo tanımlar: BookId, Title, Author, ISBN, Available sütunlarıyla.

   ```C#
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
   ```

3. **Kitap Ekleme:**
   - Kullanıcı, textBox kontrollerine kitap bilgilerini girer ve "Ekle" düğmesine tıklar.
   - Girilen bilgiler kontrol edilir, eğer herhangi bir alan boşsa kullanıcıya bir hata mesajı gösterilir.
   - Girilen bilgiler, SQLite INSERT sorgusu ile Books tablosuna eklenir.

```C#
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
```

4. **Kitap Silme:**

   - Kullanıcılar formdaki ilgili alanlara kitabın başlığını, yazarını, ISBN numarasını ve mevcut durumunu girer ve "Sil" butonuna tıklar.
   - Gerekli alanlar kontrol edilir ve herhangi biri boşsa kullanıcıya hata mesajı gösterilir.
   - Eğer tüm alanlar doluysa, girilen bilgilerle bir DELETE FROM sorgusu oluşturulur.
   - Bu sorgu, veritabanındaki ilgili kitabı siler.

   ```C#
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

   ```

5. **Kitap Listeleme:**
   - "Listele" butonuna tıklandığında, veritabanındaki tüm kitaplar SELECT sorgusuyla alınır.
   - SQLiteDataReader sınıfı kullanılarak sorgu çalıştırılır ve dönen sonuçlar okunur.
   - Her bir kitap bilgisi, listBoxListe kontrolüne eklenir ve kullanıcıya listelenir.

```C#
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
```

---

# Öğrenci Kayıt Sistemi

![ProjectImage](./projectsImages/Ekran%20görüntüsü%202024-03-24%20185143.png)

Bu C# kodu, bir öğrenci kayıt sistemi oluşturmak için Windows Forms uygulaması kullanır.

- **Öğrenci Kaydı Eklenmesi:** Kullanıcılar ad, soyad, e-posta ve doğum tarihi gibi bilgileri girerek öğrencileri veritabanına ekleyebilirler.
- **Öğrenci Listesi Oluşturulması:** Kullanıcılar "Listele" düğmesine tıkladıklarında, veritabanındaki tüm öğrencilerin bilgileri (ad, soyad, e-posta, doğum tarihi, kayıt tarihi) bir liste kutusunda görüntülenir.

## İşlevselliği:

1. **Veritabanı Bağlantısı Kurma:**

   - Kod, SQLite veritabanına bağlanmak için SQLiteConnection sınıfını kullanır. Bağlantı StudentRegistration.db adlı bir SQLite veritabanına kurulur.

   ```C#
   private const string connectionString = "Data Source=StudentRegistration.db;Version=3;";
   ```

2. **Öğrenci Kaydı Ekleme:**

   - buttonKayit_Click metodu, "Kayıt" butonuna tıklandığında çalışır.
   - Metot, kullanıcının girdiği ad, soyad, e-posta ve doğum tarihi bilgilerini alır.
   - Ardından bir SQLiteConnection nesnesi oluşturulur ve using bloğu içinde tanımlanır. Bu, bağlantının otomatik olarak kapatılmasını sağlar.
   - Eğer gerekli alanlar boş değilse, önce tablo oluşturulur (CREATE TABLE IF NOT EXISTS) ve sonra INSERT sorgusu ile öğrenci bilgileri eklenir.
   - Herhangi bir hata oluşursa, try-catch bloğu içinde yakalanır ve kullanıcıya hata mesajı gösterilir.

   ```C#
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
   ```

3. **Öğrenci Listeleme:**
   - "Listele" düğmesine tıklandığında, mevcut öğrenciler SELECT sorgusuyla alınır ve listBoxListe kontrolüne eklenir.

```C#
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
```

using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace KullanıcıTabloOlusturma
{
    public partial class KayıtOl : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Formdan alınan değerleri al
            string ad = firstName.Text;
            string soyad = lastName.Text;
            string email = this.email.Text;
            string kullaniciAdi = username.Text;
            string sifre = password.Text;

            // Kullanıcı bilgilerini kaydet
            bool isSuccess = KullaniciKaydet(ad, soyad, email, kullaniciAdi, sifre);

            // Kayıt başarılıysa mesaj göster
            if (isSuccess)
            {
                // Kullanıcıya başarı mesajı göster
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sisteme başarıyla kaydoldunuz.');", true);
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Kayıt sırasında bir problem oluştu. Lütfen girdiğiniz bilgilerin doğruluğundan emin olunuz.');", true);

            }
        }

        // Kullanıcı bilgilerini veritabanına kaydeden yöntem
        public bool KullaniciKaydet(string ad, string soyad, string email, string kullaniciAdi, string sifre)
        {
            try
            {
                // Veritabanı bağlantısını kontrol et
                VeriTabaniBaglantisi.BaglantiKontrolu();

                // SQL komutu oluştur
                string query = "INSERT INTO Kullanicilar (Ad, Soyad, Email, KullaniciAdi, Sifre) VALUES (@Ad, @Soyad, @Email, @KullaniciAdi, @Sifre)";

                using (var command = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
                {
                    // Parametreleri ekle
                    command.Parameters.AddWithValue("@Ad", ad);
                    command.Parameters.AddWithValue("@Soyad", soyad);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                    command.Parameters.AddWithValue("@Sifre", sifre);

                    // SQL komutunu çalıştır
                    command.ExecuteNonQuery();
                }

                // Veritabanı bağlantısını kapat
                VeriTabaniBaglantisi.baglanti.Close();

                return true; // İşlem başarılı
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine("Veritabanına kaydedilirken bir hata oluştu: " + ex.Message);
                return false; // İşlem başarısız
            }
        }
    }
}

using System;
using System.Data.SqlClient;
using System.Web;

namespace KullanıcıTabloOlusturma
{
    public partial class GirisYap
    {
        public bool GirisKontrol(string kullaniciAdi, string password)
        {
            VeriTabaniBaglantisi.BaglantiKontrolu();
            string query = "SELECT COUNT(*) FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi AND Sifre = @Password";

            using (SqlCommand komut = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                komut.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                komut.Parameters.AddWithValue("@Password", password);

                int sonuc = (int)komut.ExecuteScalar();

                return sonuc > 0;
            }
        }
    }
}

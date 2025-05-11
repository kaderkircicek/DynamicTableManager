using System;
using System.Web.UI;

namespace KullanıcıTabloOlusturma
{
    public partial class Islem : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string kullaniciEmail = Session["Kullanici"]?.ToString();
            if (string.IsNullOrEmpty(kullaniciEmail))
            {
                // Uyarıyı göster ve ardından yönlendir
                string script = "<script>alert('Oturum süresi dolmuş. Lütfen tekrar giriş yapın.'); window.location = 'Anasayfa.aspx';</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "SessionExpired", script);
                return;
            }
        }

        // Çıkış yap butonunun tıklama olayı
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear(); // Tüm oturum verilerini temizle
            Response.Redirect("Anasayfa.aspx"); // Kullanıcıyı anasayfaya yönlendir
        }
    }
}

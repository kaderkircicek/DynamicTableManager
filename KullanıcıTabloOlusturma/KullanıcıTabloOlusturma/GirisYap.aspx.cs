using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web;

namespace KullanıcıTabloOlusturma
{
    public partial class GirisYap : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cookie'den kullanıcı adını al ve TextBox'a yaz
                if (Request.Cookies["KullaniciAdi"] != null)
                {
                    username.Text = Request.Cookies["KullaniciAdi"].Value;
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = username.Text;
            string sifre = password.Text;

            bool girisBasarili = GirisKontrol(kullaniciAdi, sifre);

            if (girisBasarili)
            {
                // Başarılı giriş
                Session["Kullanici"] = kullaniciAdi;

                if (chkRememberMe.Checked)
                {
                    HttpCookie cookie = new HttpCookie("KullaniciAdi", kullaniciAdi);
                    cookie.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    if (Request.Cookies["KullaniciAdi"] != null)
                    {
                        HttpCookie cookie = new HttpCookie("KullaniciAdi");
                        cookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(cookie);
                    }
                }

                Response.Redirect("Islem.aspx");
            }
            else
            {
                // Hata mesajı
                Response.Write("<script>alert('Böyle bir kullanıcı bulunamadı.');</script>");
            }
        }


    }
}

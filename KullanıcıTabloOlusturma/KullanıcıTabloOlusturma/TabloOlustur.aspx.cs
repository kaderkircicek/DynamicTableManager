using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KullanıcıTabloOlusturma
{
    [Serializable]
    public class SutunBilgi
    {
        public string SutunAdi { get; set; }
        public string VeriTuru { get; set; }

        public SutunBilgi(string sutunAdi, string veriTuru)
        {
            SutunAdi = sutunAdi;
            VeriTuru = veriTuru;
        }
    }

    public partial class TabloOlustur : Page
    {
        // Sayfa yenilense dahi bilgilerin kaybolmasını önlemek için yazdın
        private List<SutunBilgi> SutunListesi
        {
            get
            {
                if (ViewState["SutunListesi"] == null)
                {
                    ViewState["SutunListesi"] = new List<SutunBilgi>();
                }
                return (List<SutunBilgi>)ViewState["SutunListesi"];
            }
            set
            {
                ViewState["SutunListesi"] = value;
            }
        }

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

            if (!IsPostBack)
            {
                SutunListesi = new List<SutunBilgi>();
                RepeateriBagla();
            }
        }

        protected void btnAddColumn_Click(object sender, EventArgs e)
        {
            RepeateriGuncelle();

            SutunListesi.Add(new SutunBilgi("", ""));

            RepeateriBagla();
        }

        protected void btnCreateTable_Click(object sender, EventArgs e)
        {
            RepeateriGuncelle();

            string tabloAdi = this.tableName.Text; // Güncel isim

            try
            {
                VeriTabaniBaglantisi.BaglantiKontrolu();
                // ID sütununu ekleyin ve otomatik artıran olarak ayarlayın
                string tabloOlusturQuery = $"CREATE TABLE {tabloAdi} (ID INT IDENTITY(1,1) PRIMARY KEY,";

                foreach (var sutun in SutunListesi)
                {
                    tabloOlusturQuery += $"{sutun.SutunAdi} {sutun.VeriTuru},";
                }
                tabloOlusturQuery = tabloOlusturQuery.TrimEnd(',') + ")";

                using (SqlCommand komut = new SqlCommand(tabloOlusturQuery, VeriTabaniBaglantisi.baglanti))
                {
                    komut.ExecuteNonQuery();
                }

                string kullaniciEmail = Session["Kullanici"].ToString(); // Giriş yapan kullanıcının emaili
                string tabloKayitQuery = "INSERT INTO KullaniciTablolari (KullaniciEmail, TabloAdi) VALUES (@KullaniciEmail, @TabloAdi)";

                using (SqlCommand komut = new SqlCommand(tabloKayitQuery, VeriTabaniBaglantisi.baglanti))
                {
                    komut.Parameters.AddWithValue("@KullaniciEmail", kullaniciEmail);
                    komut.Parameters.AddWithValue("@TabloAdi", tabloAdi);
                    komut.ExecuteNonQuery();
                }

                Response.Write("<script>alert('Tablo başarıyla oluşturuldu.');</script>");
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Hata: {ex.Message}');</script>");
            }
        }

        private void RepeateriGuncelle()
        {
            var sutunlar = new List<SutunBilgi>();

            foreach (RepeaterItem item in rptColumns.Items) // Güncel isim
            {
                TextBox txtSutunAdi = (TextBox)item.FindControl("txtColumnName"); // Güncel isim
                DropDownList ddlVeriTuru = (DropDownList)item.FindControl("ddlDataType"); // Güncel isim

                if (txtSutunAdi != null && ddlVeriTuru != null)
                {
                    sutunlar.Add(new SutunBilgi(txtSutunAdi.Text, ddlVeriTuru.SelectedValue));
                }
            }

            SutunListesi = sutunlar;
        }

        private void RepeateriBagla()
        {
            rptColumns.DataSource = SutunListesi; // Güncel isim
            rptColumns.DataBind();
        }

        protected void rptColumns_ItemDataBound(object sender, RepeaterItemEventArgs e) // Güncel isim
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var sutunVerisi = (SutunBilgi)e.Item.DataItem;
                TextBox txtSutunAdi = (TextBox)e.Item.FindControl("txtColumnName"); // Güncel isim
                DropDownList ddlVeriTuru = (DropDownList)e.Item.FindControl("ddlDataType"); // Güncel isim

                if (txtSutunAdi != null && ddlVeriTuru != null)
                {
                    txtSutunAdi.Text = sutunVerisi.SutunAdi;
                    ddlVeriTuru.SelectedValue = sutunVerisi.VeriTuru;
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Oturumdan çıkış yap
            Session.Abandon(); // Oturumu sonlandır

            // Kullanıcıyı Anasayfa.aspx sayfasına yönlendir
            Response.Redirect("Anasayfa.aspx");
        }
    }
}

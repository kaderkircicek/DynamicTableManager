using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KullanıcıTabloOlusturma
{
    public partial class TabloSilmeGuncelleme : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Kullanıcının tablolarını Dropdown'a doldur
                KullaniciTablolariniGetir();
            }
        }

        // Kullanıcının oluşturduğu tabloları Dropdown'a getir
        private void KullaniciTablolariniGetir()
        {
            string kullaniciEmail = Session["Kullanici"]?.ToString();
            if (string.IsNullOrEmpty(kullaniciEmail))
            {
                // Uyarıyı göster ve ardından yönlendir
                string script = "<script>alert('Oturum süresi dolmuş. Lütfen tekrar giriş yapın.'); window.location = 'Anasayfa.aspx';</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "SessionExpired", script);
                return;
            }

            string query = "SELECT TabloAdi FROM KullaniciTablolari WHERE KullaniciEmail = @KullaniciEmail";
            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@KullaniciEmail", kullaniciEmail);
                VeriTabaniBaglantisi.BaglantiKontrolu();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ddlTablolar.DataSource = reader;
                    ddlTablolar.DataTextField = "TabloAdi";
                    ddlTablolar.DataValueField = "TabloAdi";
                    ddlTablolar.DataBind();
                }
            }

            ddlTablolar.Items.Insert(0, new ListItem("-- Tablo Seçin --", "0"));
        }

        // Seçilen tablonun sütun isimlerini ve veri türlerini getir
        protected void ddlTablolar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTable = ddlTablolar.SelectedValue;
            if (selectedTable != "0")
            {
                TablodakiSutunlariGetir(selectedTable);
            }
        }

        // Tablonun sütun bilgilerini getir ve GridView'de göster
        private void TablodakiSutunlariGetir(string tabloAdi)
        {
            string query = @"
                SELECT COLUMN_NAME AS SutunAdi, DATA_TYPE AS VeriTuru 
                FROM INFORMATION_SCHEMA.COLUMNS 
                WHERE TABLE_NAME = @TabloAdi AND COLUMN_NAME != 'ID'";

            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@TabloAdi", tabloAdi);
                VeriTabaniBaglantisi.BaglantiKontrolu();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    gvTablo.DataSource = dt;
                    gvTablo.DataBind();
                }
            }
        }

        // GridView'de düzenleme moduna geçiş
        protected void gvTablo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTablo.EditIndex = e.NewEditIndex;
            TablodakiSutunlariGetir(ddlTablolar.SelectedValue);
        }

        // GridView'deki güncelleme işlemi
        protected void gvTablo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string tabloAdi = ddlTablolar.SelectedValue;
            string eskiSutunAdi = gvTablo.DataKeys[e.RowIndex].Value.ToString();
            string yeniSutunAdi = ((TextBox)gvTablo.Rows[e.RowIndex].FindControl("txtSutunAdi")).Text;
            string yeniVeriTuru = ((DropDownList)gvTablo.Rows[e.RowIndex].FindControl("ddlVeriTuruGuncelleme")).SelectedValue;

            // 1. Sütun adını değiştirme (sp_rename ile)
            if (eskiSutunAdi != yeniSutunAdi)
            {
                string renameQuery = $"EXEC sp_rename '{tabloAdi}.{eskiSutunAdi}', '{yeniSutunAdi}', 'COLUMN'";
                using (SqlCommand cmdRename = new SqlCommand(renameQuery, VeriTabaniBaglantisi.baglanti))
                {
                    VeriTabaniBaglantisi.BaglantiKontrolu();
                    cmdRename.ExecuteNonQuery();
                }
            }

            // 2. Veri türünü değiştirme (ALTER COLUMN ile)
            string alterQuery = $"ALTER TABLE {tabloAdi} ALTER COLUMN {yeniSutunAdi} {yeniVeriTuru}";
            using (SqlCommand cmdAlter = new SqlCommand(alterQuery, VeriTabaniBaglantisi.baglanti))
            {
                VeriTabaniBaglantisi.BaglantiKontrolu();
                cmdAlter.ExecuteNonQuery();
            }

            gvTablo.EditIndex = -1;
            TablodakiSutunlariGetir(tabloAdi);
        }

        // GridView'de düzenleme iptali
        protected void gvTablo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTablo.EditIndex = -1;
            TablodakiSutunlariGetir(ddlTablolar.SelectedValue);
        }

        // GridView'de satır silme işlemi
        protected void gvTablo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string tabloAdi = ddlTablolar.SelectedValue;
            string sutunAdi = gvTablo.DataKeys[e.RowIndex].Value.ToString();

            string deleteQuery = $"ALTER TABLE {tabloAdi} DROP COLUMN {sutunAdi}";
            using (SqlCommand cmd = new SqlCommand(deleteQuery, VeriTabaniBaglantisi.baglanti))
            {
                VeriTabaniBaglantisi.BaglantiKontrolu();
                cmd.ExecuteNonQuery();
            }

            TablodakiSutunlariGetir(tabloAdi);
        }

        // Yeni sütun ekleme işlemi
        protected void btnSutunEkle_Click(object sender, EventArgs e)
        {
            string tabloAdi = ddlTablolar.SelectedValue;
            string yeniSutunAdi = txtYeniSutun.Text;
            string veriTuru = ddlVeriTuru.SelectedValue;

            string addQuery = $"ALTER TABLE {tabloAdi} ADD {yeniSutunAdi} {veriTuru}";
            using (SqlCommand cmd = new SqlCommand(addQuery, VeriTabaniBaglantisi.baglanti))
            {
                VeriTabaniBaglantisi.BaglantiKontrolu();
                cmd.ExecuteNonQuery();
            }

            txtYeniSutun.Text = "";
            TablodakiSutunlariGetir(tabloAdi);
        }

        protected void btnCikisYap_Click(object sender, EventArgs e)
        {
            // Oturumu sonlandır
            Session.Abandon();
            // Anasayfa sayfasına yönlendir
            Response.Redirect("Anasayfa.aspx");
        }
    }
}

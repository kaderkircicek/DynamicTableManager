using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KullanıcıTabloOlusturma
{
    public partial class TabloyaVeriEkleme : Page
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



            if (!IsPostBack)
            {
                // Sayfa ilk yüklendiğinde, kullanıcının tablolarını getir
                KullaniciTablolariniGetir();
            }
            else
            {
                // Eğer bir tablo seçilmişse sütunları tekrar oluştur
                if (ddlTablolar.SelectedValue != "0" && !string.IsNullOrEmpty(ddlTablolar.SelectedValue))
                {
                    SütunlariGetir(ddlTablolar.SelectedValue);
                }
            }
        }

        private void KullaniciTablolariniGetir()
        {
            string kullaniciEmail = Session["Kullanici"]?.ToString();
            if (string.IsNullOrEmpty(kullaniciEmail))
            {
                Response.Write("<script>alert('Oturum süresi dolmuş. Lütfen tekrar giriş yapın.');</script>");
                return;
            }

            string tabloGetirQuery = "SELECT TabloAdi FROM KullaniciTablolari WHERE KullaniciEmail = @KullaniciEmail";

            using (SqlCommand komut = new SqlCommand(tabloGetirQuery, VeriTabaniBaglantisi.baglanti))
            {
                komut.Parameters.AddWithValue("@KullaniciEmail", kullaniciEmail);
                VeriTabaniBaglantisi.BaglantiKontrolu();

                using (SqlDataReader reader = komut.ExecuteReader())
                {
                    ddlTablolar.DataSource = reader;
                    ddlTablolar.DataTextField = "TabloAdi";
                    ddlTablolar.DataValueField = "TabloAdi";
                    ddlTablolar.DataBind();
                }
            }

            ddlTablolar.Items.Insert(0, new ListItem("-- Tablo Seçin --", "0"));
        }

        protected void ddlTablolar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTable = ddlTablolar.SelectedValue;
            if (selectedTable != "0")
            {
                SütunlariGetir(selectedTable);
            }
        }

        private void SütunlariGetir(string tabloAdi)
        {
            string sütunGetirQuery = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TabloAdi";

            using (SqlCommand komut = new SqlCommand(sütunGetirQuery, VeriTabaniBaglantisi.baglanti))
            {
                komut.Parameters.AddWithValue("@TabloAdi", tabloAdi);
                VeriTabaniBaglantisi.BaglantiKontrolu();

                using (SqlDataReader reader = komut.ExecuteReader())
                {
                    // Önceki sütunları temizleyelim
                    phFields.Controls.Clear(); // PlaceHolder'daki eski kontrolleri temizle

                    while (reader.Read())
                    {
                        string columnName = reader["COLUMN_NAME"].ToString();

                        // ID sütununu eklemeden geçelim
                        if (columnName != "ID")
                        {
                            Label lbl = new Label { Text = columnName + ": " };
                            TextBox txtBox = new TextBox { ID = "txt" + columnName };

                            phFields.Controls.Add(lbl);
                            phFields.Controls.Add(txtBox);
                            phFields.Controls.Add(new LiteralControl("<br/>")); // Yeni satır
                        }
                    }
                }
            }

            // Paneli görünür yap
            pnlSutunlar.Visible = phFields.Controls.Count > 0;
        }

        protected void btnVeriEkle_Click(object sender, EventArgs e)
        {
            string selectedTable = ddlTablolar.SelectedValue;

            // Kullanıcı bir tablo seçmediyse
            if (selectedTable == "0" || string.IsNullOrEmpty(selectedTable))
            {
                Response.Write("<script>alert('Lütfen bir tablo seçin.');</script>");
                return;
            }

            List<string> columnNames = new List<string>();
            List<string> values = new List<string>();

            // Paneldeki tüm TextBox'ları kontrol edelim ve sütunları alalım
            foreach (Control control in phFields.Controls)
            {
                if (control is TextBox txtBox)
                {
                    // Sütun adını alalım
                    string columnName = txtBox.ID.Replace("txt", "");

                    // Eğer TextBox boş değilse ekleyelim
                    if (!string.IsNullOrWhiteSpace(txtBox.Text))
                    {
                        columnNames.Add(columnName);
                        values.Add(txtBox.Text);
                    }
                }
            }

            // Eğer hiçbir değer girilmediyse
            if (values.Count == 0)
            {
                Response.Write("<script>alert('Lütfen veri girin.');</script>");
                return;
            }

            // Insert komutunu oluştur
            string insertQuery = $"INSERT INTO {selectedTable} ({string.Join(", ", columnNames)}) VALUES ({string.Join(", ", columnNames.Select(c => $"@{c}"))})";

            using (SqlCommand komut = new SqlCommand(insertQuery, VeriTabaniBaglantisi.baglanti))
            {
                for (int i = 0; i < columnNames.Count; i++)
                {
                    komut.Parameters.AddWithValue($"@{columnNames[i]}", values[i]);
                }

                // Bağlantıyı kontrol et ve aç
                VeriTabaniBaglantisi.BaglantiKontrolu();

                try
                {
                    komut.ExecuteNonQuery();
                    Response.Write("<script>alert('Veri başarıyla eklendi.');</script>");
                }
                catch (SqlException ex)
                {
                    // SQL hatasını yakala ve ekrana yazdır
                    Response.Write($"<script>alert('Veri eklenirken bir hata oluştu: {ex.Message}');</script>");
                }
            }
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

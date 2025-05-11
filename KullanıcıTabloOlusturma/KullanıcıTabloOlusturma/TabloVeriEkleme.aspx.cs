using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KullanıcıTabloOlusturma
{
    public partial class TabloListeleme : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                KullaniciTablolariniGetir();
            }
        }

        private void KullaniciTablolariniGetir()
        {
            string kullaniciEmail = Session["Kullanici"].ToString();
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
            string secilenTablo = ddlTablolar.SelectedValue;
            if (secilenTablo != "0")
            {
                TablonunAlanlariniYukle(secilenTablo);
            }
            else
            {
                pnlForm.Visible = false;
            }
        }

        private void TablonunAlanlariniYukle(string tabloAdi)
        {
            string kolonGetirQuery = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TabloAdi";

            using (SqlCommand komut = new SqlCommand(kolonGetirQuery, VeriTabaniBaglantisi.baglanti))
            {
                komut.Parameters.AddWithValue("@TabloAdi", tabloAdi);

                VeriTabaniBaglantisi.BaglantiKontrolu();
                using (SqlDataReader reader = komut.ExecuteReader())
                {
                    phFields.Controls.Clear();
                    while (reader.Read())
                    {
                        string kolonAdi = reader["COLUMN_NAME"].ToString();
                        Label lbl = new Label { Text = kolonAdi };
                        TextBox txt = new TextBox { ID = "txt_" + kolonAdi };

                        phFields.Controls.Add(lbl);
                        phFields.Controls.Add(new Literal { Text = "<br />" });
                        phFields.Controls.Add(txt);
                        phFields.Controls.Add(new Literal { Text = "<br /><br />" });
                    }
                }
            }

            pnlForm.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string tabloAdi = ddlTablolar.SelectedValue;
            string insertQuery = $"INSERT INTO {tabloAdi} (";

            foreach (Control control in phFields.Controls)
            {
                if (control is TextBox)
                {
                    TextBox txt = (TextBox)control;
                    string columnName = txt.ID.Substring(4);
                    insertQuery += $"{columnName},";
                }
            }

            insertQuery = insertQuery.TrimEnd(',') + ") VALUES (";

            foreach (Control control in phFields.Controls)
            {
                if (control is TextBox)
                {
                    TextBox txt = (TextBox)control;
                    insertQuery += $"@{txt.ID},";
                }
            }

            insertQuery = insertQuery.TrimEnd(',') + ")";

            using (SqlCommand komut = new SqlCommand(insertQuery, VeriTabaniBaglantisi.baglanti))
            {
                foreach (Control control in phFields.Controls)
                {
                    if (control is TextBox)
                    {
                        TextBox txt = (TextBox)control;
                        string columnName = txt.ID.Substring(4);
                        komut.Parameters.AddWithValue($"@{txt.ID}", txt.Text);
                    }
                }

                VeriTabaniBaglantisi.BaglantiKontrolu();
                try
                {
                    komut.ExecuteNonQuery();
                    Response.Write("<script>alert('Veriler başarıyla kaydedildi.');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('Hata: {ex.Message}');</script>");
                }
            }
        }
    }
}
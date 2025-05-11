using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KullanıcıTabloOlusturma
{
    public partial class EditYap : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string kullaniciEmail = Session["Kullanici"]?.ToString();
                if (string.IsNullOrEmpty(kullaniciEmail))
                {
                    // Uyarıyı göster ve ardından yönlendir
                    string script = "<script>alert('Oturum süresi dolmuş. Lütfen tekrar giriş yapın.'); window.location = 'Anasayfa.aspx';</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "SessionExpired", script);
                    return;
                }
                // Kullanıcının tablolarını Dropdown'a doldur
                KullaniciTablolariniGetir();
            }
        }

        // Kullanıcının tablolarını Dropdown'a getir
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

        // Dropdown'dan seçilen tabloyu GridView'de göster
        protected void ddlTablolar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTable = ddlTablolar.SelectedValue;
            if (selectedTable != "0")
            {
                TablodakiVerileriGetir(selectedTable);
            }
        }

        // Veritabanındaki tabloyu GridView'de göster
        private void TablodakiVerileriGetir(string tabloAdi)
        {
            string query = $"SELECT * FROM {tabloAdi}";
            using (SqlCommand komut = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                VeriTabaniBaglantisi.BaglantiKontrolu();
                using (SqlDataReader reader = komut.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // GridView'e dinamik olarak sütun ekle
                    gvTablo.Columns.Clear();
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ColumnName == "ID")
                        {
                            BoundField boundField = new BoundField
                            {
                                DataField = column.ColumnName,
                                HeaderText = column.ColumnName,
                                Visible = false // ID sütununu gizle
                            };
                            gvTablo.Columns.Add(boundField);
                        }
                        else
                        {
                            BoundField boundField = new BoundField
                            {
                                DataField = column.ColumnName,
                                HeaderText = column.ColumnName
                            };
                            gvTablo.Columns.Add(boundField);
                        }
                    }

                    // Sil ve Güncelle butonlarını ekle
                    gvTablo.Columns.Add(new CommandField { ShowEditButton = true, ShowCancelButton = true });
                    gvTablo.Columns.Add(new ButtonField { CommandName = "Delete", Text = "Sil" });

                    gvTablo.DataSource = dt;
                    gvTablo.DataBind();
                }
            }
        }

        // GridView'de düzenleme moduna geçiş
        protected void gvTablo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTablo.EditIndex = e.NewEditIndex;
            TablodakiVerileriGetir(ddlTablolar.SelectedValue);
        }

        // Güncelleme işlemi
        protected void gvTablo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string selectedTable = ddlTablolar.SelectedValue;
            int id = Convert.ToInt32(gvTablo.DataKeys[e.RowIndex].Value); // ID değerini al

            for (int i = 1; i < gvTablo.Columns.Count - 2; i++)
            {
                string columnName = gvTablo.Columns[i].HeaderText;
                string newValue = ((TextBox)gvTablo.Rows[e.RowIndex].Cells[i].Controls[0]).Text;

                string updateQuery = $"UPDATE {selectedTable} SET {columnName} = @NewValue WHERE ID = @ID";
                using (SqlCommand komut = new SqlCommand(updateQuery, VeriTabaniBaglantisi.baglanti))
                {
                    komut.Parameters.AddWithValue("@NewValue", newValue);
                    komut.Parameters.AddWithValue("@ID", id);
                    VeriTabaniBaglantisi.BaglantiKontrolu();
                    komut.ExecuteNonQuery();
                }
            }

            gvTablo.EditIndex = -1;
            TablodakiVerileriGetir(selectedTable);
        }

        // Düzenlemeyi iptal et
        protected void gvTablo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTablo.EditIndex = -1;
            TablodakiVerileriGetir(ddlTablolar.SelectedValue);
        }

        // Silme işlemi
        protected void gvTablo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string selectedTable = ddlTablolar.SelectedValue;
            int id = Convert.ToInt32(gvTablo.DataKeys[e.RowIndex].Value);

            string deleteQuery = $"DELETE FROM {selectedTable} WHERE ID = @ID";
            using (SqlCommand komut = new SqlCommand(deleteQuery, VeriTabaniBaglantisi.baglanti))
            {
                komut.Parameters.AddWithValue("@ID", id);
                VeriTabaniBaglantisi.BaglantiKontrolu();
                komut.ExecuteNonQuery();
            }

            TablodakiVerileriGetir(selectedTable);
        }

        // Çıkış yap butonunun olay işleyicisi
        protected void btnCikisYap_Click(object sender, EventArgs e)
        {
            // Kullanıcının oturumunu sonlandır
            Session.Clear(); // Tüm oturum verilerini temizle
            Session.Abandon(); // Oturumu sonlandır

            // Anasayfa'ya yönlendir
            Response.Redirect("Anasayfa.aspx");
        }
    }
}

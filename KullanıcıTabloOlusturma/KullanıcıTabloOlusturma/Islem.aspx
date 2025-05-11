<%@ Page Title="" Language="C#" MasterPageFile="~/Anasayfa.Master" AutoEventWireup="true" CodeBehind="Islem.aspx.cs" Inherits="KullanıcıTabloOlusturma.Islem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #ede4e4; /* Sayfa arka plan rengi */
        }

        .card-custom {
            height: 250px; /* Kart yüksekliği */
            display: flex;
            flex-direction: column;
            justify-content: center;
        }

        .card-custom h5 {
            font-size: 1.5rem; /* Başlık font büyüklüğü */
        }

        .full-height {
            height: 100vh; /* Tam ekran yüksekliği */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex align-items-center justify-content-center full-height">
        <div class="row justify-content-center">
            <div class="col-md-6 col-lg-4 mb-4">
                <div class="card text-center shadow-sm card-custom" style="background-color: #003f7f;">
                    <div class="card-body text-white">
                        <h5 class="card-title">Tablo Oluşturma</h5>
                        <p class="card-text">Yeni tablo oluşturun ve yönetim için hazır hale getirin.</p>
                        <a href="TabloOlustur.aspx" class="btn btn-light">Tablo Oluştur</a>
                    </div>
                </div>
            </div>
             <div class="col-md-6 col-lg-4 mb-4">
                 <div class="card text-center shadow-sm card-custom" style="background-color: #003f7f;">
                     <div class="card-body text-white">
                         <h5 class="card-title"> Oluşturduğunuz Tablolara Veri Girişi Yapma </h5>
                         <p class="card-text">Oluşturulmuş tüm tabloları görüntüleyin ve istediğiniz tabloya veri girişi yapın.</p>
                         <a href="TabloyaVeriEkleme.aspx" class="btn btn-light">Tablolara Veri Ekle</a>
                     </div>
                 </div>
             </div>
            <div class="col-md-6 col-lg-4 mb-4">
                <div class="card text-center shadow-sm card-custom" style="background-color: #003f7f;">
                    <div class="card-body text-white">
                        <h5 class="card-title">Tabloları Listeleme Ve Edit Yapma</h5>
                        <p class="card-text">Mevcut tablolarda veri ekleyin, düzenleyin veya silin.</p>
                        <a href="EditYap.aspx" class="btn btn-light"> Tablo Verilerinde Güncelleme Yap</a>
                    </div>
                </div>
            </div>
             <div class="col-md-6 col-lg-4 mb-4">
                  <div class="card text-center shadow-sm card-custom" style="background-color: #003f7f;">
                      <div class="card-body text-white">
                          <h5 class="card-title"> Oluşturduğunuz Tabloları Silme Veya İsim Güncelleme </h5>
                          <p class="card-text">Oluşturulmuş tüm tabloları görüntüleyin ve istediğiniz tabloyu silin veya ismini güncelleyin.</p>
                          <a href="TabloSilmeGüncelleme.aspx" class="btn btn-light">Tabloları Silme Ve Güncelleme</a>
                     </div>
                   </div>
            </div>
        </div>
    </div>

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Button ID="btnLogout" runat="server" Text="Çıkış Yap" CssClass="btn btn-danger" OnClick="btnLogout_Click" style="position: absolute; top: 10px; left: 10px;" />
            </div>
        </div>
    </div>

    <!-- Include Bootstrap JavaScript (optional if you want to use Bootstrap JS components) -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.4/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</asp:Content>

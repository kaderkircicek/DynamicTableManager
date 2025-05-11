<%@ Page Title="Giriş Yap" Language="C#" MasterPageFile="~/Anasayfa.Master" AutoEventWireup="true" CodeBehind="GirisYap.aspx.cs" Inherits="KullanıcıTabloOlusturma.GirisYap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .login-container {
            max-width: 500px; /* Daha geniş bir maksimum genişlik */
            margin: 100px auto; /* Üstten daha fazla boşluk */
            padding: 40px; /* İç boşlukları artırma */
            background-color: #fff;
            border-radius: 15px; /* Daha yuvarlak köşeler */
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.1); /* Daha belirgin gölge */
        }
        .login-container h2 {
            font-size: 2.5rem; /* Başlık boyutunu artırma */
            text-align: center;
            margin-bottom: 30px; /* Başlık altı boşluğu artırma */
        }
        .login-container .form-control {
            border-radius: 10px; /* Form kontrol köşelerini yuvarlama */
            font-size: 1.25rem; /* Form kontrol metin boyutunu artırma */
            padding: 15px; /* İç boşlukları artırma */
        }
        .login-container .btn-primary {
            width: 100%;
            border-radius: 10px; /* Buton köşelerini yuvarlama */
            font-size: 1.25rem; /* Buton metin boyutunu artırma */
            padding: 15px; /* İç boşlukları artırma */
        }
        .form-label {
            font-size: 1.25rem; /* Etiket metin boyutunu artırma */
        }
        .invalid-feedback {
            color: #dc3545; /* Kırmızı renkli hata mesajları */
            display: none; /* Hata mesajlarını varsayılan olarak gizle */
        }
        .was-validated .form-control:invalid {
            border-color: #dc3545; /* Hatalı alanın kenar rengini kırmızı yap */
        }
        .was-validated .form-control:invalid ~ .invalid-feedback {
            display: block; /* Hata mesajlarını göster */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container">
        <h2>Giriş Yap</h2>
        <asp:PlaceHolder runat="server" ID="PlaceHolder1">
            <div class="mb-3">
                <label for="username" class="form-label">Kullanıcı Adı</label>
                <asp:TextBox ID="username" CssClass="form-control" runat="server" Placeholder="Kullanıcı adınızı girin"></asp:TextBox>
                <div class="invalid-feedback">Lütfen kullanıcı adınızı girin.</div>
            </div>
            <div class="mb-3">
                <label for="password" class="form-label">Şifre</label>
                <asp:TextBox ID="password" TextMode="Password" CssClass="form-control" runat="server" Placeholder="Şifrenizi girin"></asp:TextBox>
                <div class="invalid-feedback">Lütfen şifrenizi girin.</div>
            </div>
            <div class="mb-3">
                <asp:CheckBox ID="chkRememberMe" runat="server" Text="Beni Hatırla" />
            </div>
            <div class="d-grid">
                <asp:Button ID="btnRegister" runat="server" Text="Giriş Yap" OnClick="btnRegister_Click" CssClass="btn btn-primary" />
            </div>
        </asp:PlaceHolder>
    </div>
    <script>
        // Bootstrap's validation script
        (function () {
            'use strict';
            var forms = document.querySelectorAll('.needs-validation');
            Array.prototype.slice.call(forms).forEach(function (form) {
                form.addEventListener('submit', function (event) {
                    if (!form.checkValidity()) {
                        event.preventDefault();
                        event.stopPropagation();
                    }
                    form.classList.add('was-validated');
                }, false);
            });
        })();
    </script>
</asp:Content>

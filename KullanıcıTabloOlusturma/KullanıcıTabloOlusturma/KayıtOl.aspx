<%@ Page Title="Kayıt Ol" Language="C#" MasterPageFile="~/Anasayfa.Master" AutoEventWireup="true" CodeBehind="KayıtOl.aspx.cs" Inherits="KullanıcıTabloOlusturma.KayıtOl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .register-container {
            max-width: 600px;
            margin: 50px auto;
            padding: 20px;
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }
        .register-container h2 {
            text-align: center;
            margin-bottom: 20px;
        }
        .register-container .form-control {
            border-radius: 5px;
        }
        .register-container .btn-primary {
            width: 100%;
            border-radius: 5px;
        }
        .invalid-feedback {
            display: none;
            color: red;
        }
        .invalid-feedback.show {
            display: block;
        }
        .success-message {
            color: green;
            font-weight: bold;
            text-align: center;
            margin-top: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="register-container">
        <h2>Kayıt Ol</h2>
        <asp:PlaceHolder runat="server" ID="PlaceHolder1">
            <div class="mb-3">
                <label for="firstName" class="form-label">Ad</label>
                <asp:TextBox ID="firstName" CssClass="form-control" runat="server" Placeholder="Adınızı girin"></asp:TextBox>
                <div class="invalid-feedback">
                    Lütfen adınızı girin.
                </div>
            </div>
            <div class="mb-3">
                <label for="lastName" class="form-label">Soyad</label>
                <asp:TextBox ID="lastName" CssClass="form-control" runat="server" Placeholder="Soyadınızı girin"></asp:TextBox>
                <div class="invalid-feedback">
                    Lütfen soyadınızı girin.
                </div>
            </div>
            <div class="mb-3">
                <label for="email" class="form-label">Email</label>
                <asp:TextBox ID="email" CssClass="form-control" runat="server" Placeholder="Email adresinizi girin"></asp:TextBox>
                <div class="invalid-feedback" id="emailFeedback">
                    Lütfen geçerli bir email adresi girin.
                </div>
            </div>
            <div class="mb-3">
                <label for="username" class="form-label">Kullanıcı Adı</label>
                <asp:TextBox ID="username" CssClass="form-control" runat="server" Placeholder="Kullanıcı adınızı girin"></asp:TextBox>
                <div class="invalid-feedback">
                    Lütfen kullanıcı adınızı girin.
                </div>
            </div>
            <div class="mb-3">
                <label for="password" class="form-label">Şifre</label>
                <asp:TextBox ID="password" TextMode="Password" CssClass="form-control" runat="server" Placeholder="Şifrenizi girin"></asp:TextBox>
                <div class="invalid-feedback">
                    Lütfen şifrenizi girin.
                </div>
            </div>
            <div class="d-grid">
                <asp:Button ID="btnRegister" runat="server" Text="Kayıt Ol" OnClick="btnRegister_Click" OnClientClick="return validateForm();" />
            </div>
            <asp:Label ID="lblSuccessMessage" runat="server" CssClass="success-message"></asp:Label>
        </asp:PlaceHolder>
    </div>
    <script>
        function validateForm() {
            var email = document.getElementById('<%= email.ClientID %>');
            var emailFeedback = document.getElementById('emailFeedback');

            if (!email.value.includes('@')) {
                emailFeedback.classList.add('show');
                return false; // Prevent form submission
            } else {
                emailFeedback.classList.remove('show');
                return true; // Allow form submission
            }
        }
    </script>
</asp:Content>

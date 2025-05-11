<%@ Page Title="Tablo Oluştur" Language="C#" MasterPageFile="~/Anasayfa.Master" AutoEventWireup="true" CodeBehind="TabloOlustur.aspx.cs" Inherits="KullanıcıTabloOlusturma.TabloOlustur" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-container {
            max-width: 800px;
            margin: 50px auto;
            padding: 20px;
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }
        .form-container h2 {
            text-align: center;
            margin-bottom: 20px;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .form-group label {
            display: block;
            margin-bottom: 5px;
        }
        .form-group input, .form-group select {
            width: 100%;
            padding: 8px;
            font-size: 1rem;
            border-radius: 5px;
            border: 1px solid #ccc;
        }
        .btn-add-column {
            display: block;
            width: 100%;
            padding: 10px;
            font-size: 1rem;
            margin-top: 10px;
            background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
        .btn-logout {
            display: block;
            width: 100%;
            padding: 10px;
            font-size: 1rem;
            margin-top: 20px;
            background-color: #dc3545; /* Kırmızı arka plan */
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <h2>Tablo Oluştur</h2>
        <div class="form-group">
            <label for="tableName">Tablo Adı</label>
            <asp:TextBox ID="tableName" runat="server" CssClass="form-control" Placeholder="Tablo adını girin"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>Sütunlar</label>
            <asp:Repeater ID="rptColumns" runat="server" OnItemDataBound="rptColumns_ItemDataBound">
                <ItemTemplate>
                    <div class="form-group">
                        <asp:TextBox ID="txtColumnName" runat="server" CssClass="form-control" Placeholder="Sütun adı"></asp:TextBox>
                        <asp:DropDownList ID="ddlDataType" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Karakter" Value="varchar"></asp:ListItem>
                            <asp:ListItem Text="Tam Sayı" Value="int"></asp:ListItem>
                            <asp:ListItem Text="Ondalıklı Sayı" Value="decimal"></asp:ListItem>
                            <asp:ListItem Text="Tarih" Value="datetime"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <asp:Button ID="btnAddColumn" runat="server" Text="Yeni Sütun Ekle" OnClick="btnAddColumn_Click" CssClass="btn-add-column" />
        <asp:Button ID="btnCreateTable" runat="server" Text="Tablo Oluştur" OnClick="btnCreateTable_Click" />
        <asp:Button ID="btnLogout" runat="server" Text="Çıkış Yap" OnClick="btnLogout_Click" CssClass="btn-logout" />
    </div>
</asp:Content>

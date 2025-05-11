<%@ Page Title="" Language="C#" MasterPageFile="~/Anasayfa.Master" AutoEventWireup="true" CodeBehind="TabloSilmeGuncelleme.aspx.cs" Inherits="KullanıcıTabloOlusturma.TabloSilmeGuncelleme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <h3 class="card-title">Tablo Silme/Güncelleme İşlemleri</h3>
                        
                    </div>
                    <div class="card-body">
                        <asp:DropDownList ID="ddlTablolar" runat="server" CssClass="form-select mb-3" AutoPostBack="true" OnSelectedIndexChanged="ddlTablolar_SelectedIndexChanged">
                        </asp:DropDownList>

                        <asp:GridView ID="gvTablo" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvTablo_RowDeleting" OnRowEditing="gvTablo_RowEditing" OnRowUpdating="gvTablo_RowUpdating" OnRowCancelingEdit="gvTablo_RowCancelingEdit" DataKeyNames="SutunAdi" CssClass="table">
                            <Columns>
                                <asp:BoundField DataField="SutunAdi" HeaderText="Sütun Adı" />
                                <asp:BoundField DataField="VeriTuru" HeaderText="Veri Türü" />
                                <asp:TemplateField HeaderText="İşlemler">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" runat="server" Text="Güncelle" CommandName="Edit" CssClass="btn btn-warning" />
                                        <asp:Button ID="btnDelete" runat="server" Text="Sil" CommandName="Delete" CssClass="btn btn-danger" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSutunAdi" runat="server" Text='<%# Bind("SutunAdi") %>' CssClass="form-control" />
                                        <asp:DropDownList ID="ddlVeriTuruGuncelleme" runat="server" CssClass="form-select">
                                            <asp:ListItem Text="Karakter" Value="nvarchar(50)"></asp:ListItem>
                                            <asp:ListItem Text="Tam Sayı" Value="int"></asp:ListItem>
                                            <asp:ListItem Text="Ondalikli Sayı" Value="float"></asp:ListItem>
                                            <asp:ListItem Text="Tarih" Value="datetime"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Button ID="btnUpdate" runat="server" Text="Güncelle" CommandName="Update" CssClass="btn btn-success" />
                                        <asp:Button ID="btnCancel" runat="server" Text="İptal Et" CommandName="Cancel" CssClass="btn btn-secondary" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <div class="mt-4">
                            <h4>Yeni Sütun Ekle</h4>
                            <asp:TextBox ID="txtYeniSutun" runat="server" CssClass="form-control mb-2" placeholder="Yeni sütun adı"></asp:TextBox>
                            <asp:DropDownList ID="ddlVeriTuru" runat="server" CssClass="form-select mb-2">
                                <asp:ListItem Text="Karakter" Value="nvarchar(50)"></asp:ListItem>
                                <asp:ListItem Text="Tam Sayı" Value="int"></asp:ListItem>
                                <asp:ListItem Text="Ondalikli Sayı" Value="float"></asp:ListItem>
                                <asp:ListItem Text="Tarih" Value="datetime"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnSutunEkle" runat="server" Text="Sütun Ekle" CssClass="btn btn-success" OnClick="btnSutunEkle_Click" />

                        </div>
                    </div>
                    <asp:Button ID="btnCikisYap" runat="server" Text="Çıkış Yap" CssClass="btn btn-danger mb-3" OnClick="btnCikisYap_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

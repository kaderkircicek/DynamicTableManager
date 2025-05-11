<%@ Page Title="" Language="C#" MasterPageFile="~/Anasayfa.Master" AutoEventWireup="true" CodeBehind="EditYap.aspx.cs" Inherits="KullanıcıTabloOlusturma.EditYap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <h3 class="card-title">Kullanıcı Tabloları</h3>
                    </div>
                    <div class="card-body">
                        <asp:DropDownList ID="ddlTablolar" runat="server" CssClass="form-select mb-3" AutoPostBack="true" OnSelectedIndexChanged="ddlTablolar_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:GridView ID="gvTablo" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvTablo_RowDeleting" OnRowEditing="gvTablo_RowEditing" OnRowUpdating="gvTablo_RowUpdating" OnRowCancelingEdit="gvTablo_RowCancelingEdit" DataKeyNames="ID" CssClass="table">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" />
                                <asp:TemplateField HeaderText="İşlemler">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" runat="server" Text="Güncelle" CommandName="Edit" CssClass="btn btn-warning" />
                                        <asp:Button ID="btnDelete" runat="server" Text="Sil" CommandName="Delete" CssClass="btn btn-danger" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Button ID="btnCikisYap" runat="server" Text="Çıkış Yap" CssClass="btn btn-danger mt-3" OnClick="btnCikisYap_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

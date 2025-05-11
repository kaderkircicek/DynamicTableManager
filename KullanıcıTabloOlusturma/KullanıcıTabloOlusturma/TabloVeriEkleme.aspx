<%@ Page Title="" Language="C#" MasterPageFile="~/Anasayfa.Master" AutoEventWireup="true" CodeBehind="TabloListeleme.aspx.cs" Inherits="KullanıcıTabloOlusturma.TabloListeleme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">
        <div class="row">
            <div class="col-md-6 offset-md-3">
                <div class="card">
                    <div class="card-header">
                        <h3 class="card-title">Kullanıcı Tabloları</h3>
                    </div>
                    <div class="card-body">
                        <asp:DropDownList ID="ddlTablolar" runat="server" CssClass="form-select mb-3" AutoPostBack="true" OnSelectedIndexChanged="ddlTablolar_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Panel ID="pnlForm" runat="server" Visible="false">
                            <div class="form-group">
                                <asp:PlaceHolder ID="phFields" runat="server"></asp:PlaceHolder>
                            </div>
                            <asp:Button ID="btnSave" runat="server" Text="Kaydet" CssClass="btn btn-primary mt-3" OnClick="btnSave_Click" />
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
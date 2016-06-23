<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InicioSesion.aspx.cs" Inherits="InicioSesion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace_Form" runat="Server">
    <div>
        <div>
            <label>Usuario</label>
            <asp:TextBox ID="txt_usuario" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <br />
        <div>
            <label>Clave</label>
            <asp:TextBox ID="txt_clave" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
            <br />
        </div>
        <asp:Button ID="btn_login" runat="Server" CssClass="btn btn-default" Text="Iniciar sesión" OnClick="btn_login_Click" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlace_Pie" runat="Server">
</asp:Content>


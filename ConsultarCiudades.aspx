<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConsultarCiudades.aspx.cs" Inherits="Ciudades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace_Form" Runat="Server">
    <div>
        <h1>Informe de Ciudades</h1>
    </div>
    <div>
        <asp:GridView class="table table-bordered" ID="gv_ciudades" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="nombreDestino" HeaderText="Nombre Destino" />
                <asp:BoundField DataField="nombreOrigen" HeaderText="Nombre Origen" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlace_Pie" Runat="Server">
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConsultarViaje.aspx.cs" Inherits="ConsultarViaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace_Form" Runat="Server">
    
    <div>
        <h1>Informe de Viajes</h1>
    </div>
    <div>

        <asp:GridView ID="gv_viaje" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False">
            <Columns>
                <asp:CommandField SelectText="Elegir" ShowSelectButton="True" />
                <asp:BoundField DataField="idViaje" HeaderText="Nro Viaje" />
                <asp:BoundField DataField="nombreViaje" HeaderText="Nombre" />
                <asp:BoundField DataField="origen" HeaderText="Origen" />
                <asp:BoundField DataField="destino" HeaderText="Destino" />
                <asp:BoundField DataField="fechaDesde" DataFormatString="{0:d}" HeaderText="Fecha Desde" />
                <asp:BoundField DataField="fechaHasta" DataFormatString="{0:d}" HeaderText="Fecha Hasta" />
                <asp:BoundField DataField="precioTotal" HeaderText="Precio" />
                <asp:BoundField DataField="soloIda" HeaderText="Solo ida" />
            </Columns>
        </asp:GridView>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlace_Pie" Runat="Server">
</asp:Content>


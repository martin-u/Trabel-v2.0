<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReporteViaje.aspx.cs" Inherits="ReporteViaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace_Form" Runat="Server">

   <%-- a) Sistema de transporte mas elegido

b) Destino mas elegido de acuerdo a si es por tierra o aire.

c) Fecha mayores concurrencias--%>

    <div>
        <label>Transporte</label>
        <asp:DropDownList ID="ddl_transporte" runat="server" CssClass="form-control"></asp:DropDownList>
    </div>
    <div>
        <label>Solo Ida</label>
        <asp:CheckBox ID="ckb_soloIda" runat="server"  CssClass="form-control"/>
    </div>
    <div>
        <label>Destino</label>
        <asp:TextBox id="txt_destino" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div>
        <label>Fecha</label>
        <asp:TextBox ID="txt_fecha" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <br />
    <div>
        <asp:Button Text="Consultar" runat="server" id="btn_consultar" OnClick="btn_consultar_Click" CssClass="btn btn-success"/>
    </div>
    <br />
    <asp:GridView ID="gv_reporte" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="idVIaje" HeaderText="Nro Viaje" />
            <asp:BoundField DataField="fechaDesde" HeaderText="Fecha" />
            <asp:BoundField DataField="soloIda" HeaderText="Solo ida" />
            <asp:BoundField DataField="nombre" HeaderText="Destino" />
            <asp:BoundField DataField="nombre" HeaderText="Transporte" />
        </Columns>

    </asp:GridView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlace_Pie" Runat="Server">
</asp:Content>


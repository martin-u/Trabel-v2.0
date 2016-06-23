<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReporteViaje.aspx.cs" Inherits="ReporteViaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace_Form" Runat="Server">

   <%-- a) Sistema de transporte mas elegido

b) Destino mas elegido de acuerdo a si es por tierra o aire.

c) Fecha mayores concurrencias--%>

    <div>
        <label>Transporte</label>
        <asp:DropDownList ID="ddl_transporte" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
        <label class="checkbox-inline"><asp:CheckBox runat="server" type="checkbox" name="" AutoPostBack="true" ID="chk_activarTransporte" OnCheckedChanged="chk_activarTransporte_CheckedChanged" />Activar</label>
        <asp:CustomValidator ErrorMessage="Seleccionar un transporte" ControlToValidate="ddl_transporte" Text="*" OnServerValidate="cv_transporte_ServerValidate" ID="cv_transporte" runat="server" />            
    
    </div>    
    <div>
        <label>Destino</label>
        <asp:TextBox id="txt_destino" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
        <label class="checkbox-inline"><asp:CheckBox runat="server" type="checkbox" name="" AutoPostBack="true" ID="chk_activarDestino" OnCheckedChanged="chk_activarDestino_CheckedChanged" />Activar</label>
        <asp:CustomValidator ErrorMessage="Destino inexistente" ControlToValidate="txt_destino" Text="*" OnServerValidate="cv_destino_ServerValidate" ID="cv_destino" runat="server" />            
               
    </div>
    <div>
        <label>Fecha</label>
        <asp:TextBox ID="txt_fecha" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
        <label class="checkbox-inline"><asp:CheckBox runat="server" type="checkbox" name="" AutoPostBack="true" ID="chk_activarFecha" OnCheckedChanged="chk_activarFecha_CheckedChanged" />Activar</label>
        <asp:CompareValidator Type="Date" Operator="DataTypeCheck" ID="cv_fecha" runat="server" ControlToValidate="txt_fecha" ErrorMessage="Formato de fecha invalida" Text="*" />
       
    </div>
    <br />
    <div>
        <asp:Button Text="Consultar" Enabled="false" runat="server" id="btn_consultar" OnClick="btn_consultar_Click" CssClass="btn btn-success"/>
        <asp:ValidationSummary runat="server"/>
    </div>
    <br />
    <asp:GridView ID="gv_reporte" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" AllowPaging="true" PageSize="2" OnPageIndexChanging="gv_reporte_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="idVIaje" HeaderText="Nro Viaje" />
            <asp:BoundField DataField="fechaDesde" HeaderText="Fecha" DataFormatString="{0:d}" />            
            <asp:BoundField DataField="destino" HeaderText="Destino" />
            <asp:BoundField DataField="transporte" HeaderText="Transporte" />
        </Columns>

    </asp:GridView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlace_Pie" Runat="Server">
    <script>
        $(function () {
            $("#<%= txt_fecha.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' }).val();;
        });
  </script>
</asp:Content>


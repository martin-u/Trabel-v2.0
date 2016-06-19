<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Form_Viaje.aspx.cs" Inherits="Form_Viaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Formulario Viaje</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace_Form" runat="Server">

    <div>
        <h1>Formulario Viaje</h1>
    </div>
    <div>
        <label>Ciudad de Origen</label>
        <asp:TextBox runat="server" ID="txt_origen" CssClass="form-control" />
        <asp:RequiredFieldValidator ErrorMessage="Origen obligatorio" Text="*" ID="rfv_origen" ValidationGroup="A" ControlToValidate="txt_origen" runat="server" />
    </div>
    <div>
        <label>Ciudad de Destino</label>
        <asp:TextBox runat="server" ID="txt_destino" CssClass="form-control" />
        <asp:RequiredFieldValidator ErrorMessage="Destino obligatorio" Text="*" ID="rfv_destino" ValidationGroup="A" ControlToValidate="txt_destino" runat="server" />
    </div>


    <div>
        <label>Fecha Desde</label>
        <asp:TextBox CssClass="form-control" ID="txt_fechaDesde" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ErrorMessage="Fecha Desde obligatorio" Text="*" ID="rfv_fechaDesde" ValidationGroup="A" ControlToValidate="txt_fechaDesde" runat="server" />
        <asp:CompareValidator Type="Date" Operator="DataTypeCheck" ID="cv_fechaDesde" runat="server" ValidationGroup="A" ControlToValidate="txt_fechaDesde" ErrorMessage="Formato de fecha Desde invalida" Text="*" />
        <asp:CompareValidator Type="Date" Operator="LessThan" ID="cv2_fechaDesde" runat="server" ValidationGroup="A" ControlToCompare="txt_fechaHasta" ControlToValidate="txt_fechaDesde" ErrorMessage="Tipo de fecha Desde inconsistente" Text="*" />
        <br />
        
        <label>Fecha Hasta</label>
        <asp:TextBox CssClass="form-control ui-datepicker-buttonpane" ID="txt_fechaHasta" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ErrorMessage="Fecha Desde obligatorio" Text="*" ID="rfv_fechaHasta" ValidationGroup="A" ControlToValidate="txt_fechaHasta" runat="server" />
        <asp:CompareValidator Type="Date" Operator="DataTypeCheck" ID="cv_fechaHasta" runat="server" ValidationGroup="A" ControlToValidate="txt_fechaHasta" ErrorMessage="Formato de fecha Hasta invalida" Text="*" />
        <asp:CompareValidator Type="Date" Operator="GreaterThan" ID="cv2_fechaHasta" runat="server" ValidationGroup="A" ControlToCompare="txt_fechaDesde" ControlToValidate="txt_fechaHasta" ErrorMessage="Tipo de fecha Hasta inconsistente" Text="*" />
</div>


    <div>
        <asp:CheckBox Text="Solo Ida" runat="server" ID="ckb_soloIda" CssClass="form-control" />
    </div>
    <div>
        <label>Precio Total</label>
        <asp:TextBox ID="txt_precio" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ErrorMessage="Precio obligatorio" Text="*" ID="rfv_precio" ControlToValidate="txt_precio" runat="server" ValidationGroup="A"/>
        <asp:CompareValidator Type="Double" Operator="DataTypeCheck" ID="cv_precio" runat="server" ControlToValidate="txt_precio" ErrorMessage="Tipo de precio invalido" Text="*" ValidationGroup="A" />

    </div>
    <div>
        <label>Transporte</label>
        <asp:DropDownList runat="server" ID="ddl_transporte" CssClass="form-control"></asp:DropDownList>
    </div>
    <div>
        <label>Temporada</label>
        <asp:DropDownList runat="server" ID="ddl_temporada" CssClass="form-control"></asp:DropDownList>
    </div>
    <br />
    <div>
        <asp:Button Text="Guardar" ID="btn_guardar" OnClick="btn_guardar_Click" runat="server" CssClass="btn btn-success" ValidationGroup="A" />
        <asp:Button Text="Nuevo" ID="btn_nuevo" OnClick="btn_nuevo_Click" runat="server" CssClass="btn btn-info"/>
        <asp:Button Text="Consultar" ID="btn_consultar" OnClick="btn_consultar_Click" runat="server" CssClass="btn btn-primary" />
        <asp:Button Text="Eliminar" ID="btn_eliminar" OnClick="btn_elimnar_Click" runat="server" CssClass="btn btn-danger" />
        <asp:Button Text="Modificar" ID="btn_modificar" OnClick="bnt_modificar_Click" runat="server" CssClass="btn btn-success" ValidationGroup="A" visible="false" />
    </div>
    <br />
    <div>
        <asp:ValidationSummary runat="server" ValidationGroup="A"/>    
    </div>
    <div>

        <asp:GridView ID="dgv_viajes" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dgv_viajes_SelectedIndexChanged" CssClass="table table-bordered">
            <Columns>
                <asp:CommandField SelectText="Elegir" ShowSelectButton="True" />
                <asp:BoundField DataField="idViaje" HeaderText="Nro viaje" />
                <asp:BoundField DataField="origen" HeaderText="Origen" />
                <asp:BoundField DataField="destino" HeaderText="Destino" />
                <asp:BoundField DataField="fechaDesde" HeaderText="Fecha Desde" DataFormatString="{0:D}" />
            </Columns>
        </asp:GridView>

    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlace_Pie" runat="Server">
       <script>
           $(function () {
               $("#txt_fechaDesde2").datepicker();
           });
  </script>
</asp:Content>


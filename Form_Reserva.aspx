<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Form_Reserva.aspx.cs" Inherits="Form_Reserva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Formulario de Reserva</title>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace_Form" runat="Server">
    <div id="div_Reserva">
        <div>
            <h2>Reserva</h2>
            <!--RESERVA-->

            <div class="form-inline">
                <div class="form-group">
                    <!--Fecha-->
                    <label>Fecha de Reserva</label>
                    <asp:TextBox runat="server" ID="txt_fechaReserva" CssClass="form-control" Enabled="false" />
                </div>

                <div class="form-group">
                    <!--Estado-->
                    <label>Estado</label>
                    <asp:DropDownList ID="ddl_estado" runat="server" CssClass="form-control"></asp:DropDownList>
                    <asp:RequiredFieldValidator ErrorMessage="Estado obligatorio" Text="*" ID="RequiredFieldValidator3" ValidationGroup="A" ControlToValidate="ddl_estado" runat="server" />

                </div>
                <br />
                <br />
            </div>

            <div>
                <!--Viaje-->
                <label>Viaje</label>
                <asp:DropDownList ID="ddl_viaje" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_viaje_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ErrorMessage="Viaje obligatorio" Text="*" ID="RequiredFieldValidator4" ValidationGroup="A" ControlToValidate="ddl_viaje" runat="server" />

            </div>
            <div class="form-inline">            
                <div class="form-group">
                    <!--Fecha Ida-->
                    <label>Fecha Ida</label>
                    <asp:TextBox ID="txt_fechaIda" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Fecha Desde obligatorio" Text="*" ID="RequiredFieldValidator1" ValidationGroup="A" ControlToValidate="txt_fechaIda" runat="server" />
                    <asp:CompareValidator Type="Date" Operator="DataTypeCheck" ID="CompareValidator1" runat="server" ValidationGroup="A" ControlToValidate="txt_fechaIda" ErrorMessage="Formato de fecha Ida invalida" Text="*" />
                    <asp:CompareValidator Type="Date" Operator="LessThan" ID="CompareValidator2" runat="server" ValidationGroup="A" ControlToCompare="txt_fechaVuelta" ControlToValidate="txt_fechaIda" ErrorMessage="Tipo de fecha Desde inconsistente" Text="*" />
                    <asp:CustomValidator Text="*" ErrorMessage="El viaje no esta disponible para esa fecha de ida" ControlToValidate="txt_fechaIda" OnServerValidate="cv_fechaIda_ServerValidate" ValidationGroup="A" ID="cv_fechaIda" runat="server" />
                </div>

                <div class="form-group">
                    <!--Fecha Vuelta-->
                    <label>Fecha Vuelta</label>
                    <asp:TextBox ID="txt_fechaVuelta" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Fecha Desde obligatorio" Text="*" ID="RequiredFieldValidator2" ValidationGroup="A" ControlToValidate="txt_fechaVuelta" runat="server" />
                    <asp:CompareValidator Type="Date" Operator="DataTypeCheck" ID="CompareValidator3" runat="server" ValidationGroup="A" ControlToValidate="txt_fechaVuelta" ErrorMessage="Formato de fecha Hasta invalida" Text="*" />
                    <asp:CompareValidator Type="Date" Operator="GreaterThan" ID="CompareValidator4" runat="server" ValidationGroup="A" ControlToCompare="txt_fechaIda" ControlToValidate="txt_fechaVuelta" ErrorMessage="Tipo de fecha Vuelta inconsistente" Text="*" />
                    <asp:CustomValidator Text="*" ErrorMessage="El viaje no esta disponible para esa fecha de vuelta" ControlToValidate="txt_fechaVuelta" OnServerValidate="cv_fechaVuelta_ServerValidate" ValidationGroup="A" ID="cv_fechaVuelta" runat="server" />
                   
                </div>
            </div>            
            <br />

            <div>
                <!--Hotel-->
                <label>Hotel</label>
                <asp:DropDownList ID="ddl_hotel" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_hotel_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                <asp:RequiredFieldValidator ErrorMessage="Hotel obligatorio" Text="*" ID="RequiredFieldValidator5" ValidationGroup="A" ControlToValidate="ddl_hotel" runat="server" />

            </div>

            <!--DETALLE DE RESERVA-->
            <div id="div_DetalleReserva" class="form-group">
                <div>
                    <div>
                        <h2>Detalle Reserva</h2>
                        <br />
                        <!--Turista-->
                        <label>Tipo DNI</label>
                        <asp:DropDownList Enabled="false" ID="ddl_tipoDNI" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ErrorMessage="Tipo Documento obligatorio" Text="*" ID="RequiredFieldValidator6" ValidationGroup="B" ControlToValidate="ddl_tipoDNI" runat="server" />

                        <label>DNI Turista</label>
                        <asp:TextBox ID="txt_dniTurista" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="Documento obligatorio" Text="*" ID="RequiredFieldValidator7" ValidationGroup="B" ControlToValidate="txt_dniTurista" runat="server" />

                        <br />
                        <div>
                            <label>Nombre</label>
                            <asp:TextBox runat="server" Enabled="false" ID="txt_nombre" CssClass="form-control" />
                            <asp:RequiredFieldValidator ErrorMessage="Nombre obligatorio" Text="*" ID="RequiredFieldValidator8" ValidationGroup="B" ControlToValidate="txt_nombre" runat="server" />

                        </div>
                        <div>
                            <label>Apellido</label>
                            <asp:TextBox runat="server" Enabled="false" ID="txt_apellido" CssClass="form-control" />
                            <asp:RequiredFieldValidator ErrorMessage="Apellido obligatorio" Text="*" ID="RequiredFieldValidator9" ValidationGroup="B" ControlToValidate="txt_apellido" runat="server" />

                        </div>
                        <div>
                            <label>Fecha de Nacimiento</label>
                            <asp:TextBox runat="server" Enabled="false" ID="txt_fechaNacimiento" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Fecha de nacimiento obligatoria" Text="*" ID="RequiredFieldValidator10" ValidationGroup="B" ControlToValidate="txt_fechaNacimiento" runat="server" />
                            <asp:CompareValidator Type="Date" Operator="DataTypeCheck" ID="CompareValidator5" runat="server" ValidationGroup="B" ControlToValidate="txt_fechaNacimiento" ErrorMessage="Formato de fecha de nacimiento invalida" Text="*" />

                        </div>
                        <br />
                        <div>
                            <!--AGREGAR-->
                            <asp:Button ID="btn_agregar" Enabled="false" runat="server" ValidationGroup="B" CssClass="btn btn-primary" Text="AGREGAR" OnClick="btn_agregar_Click" />
                            <asp:ValidationSummary runat="server" ValidationGroup="B" />
                        </div>
                    </div>
                </div>
                <br />
                <div>
                    <!--Grilla-->
                    <asp:GridView runat="server" ID="dgv_detalleReserva" AutoGenerateColumns="False" CssClass="table table-bordered">
                        <Columns>
                            <asp:BoundField DataField="DNI" HeaderText="DNI" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                            <asp:BoundField DataField="fechaNacimiento" DataFormatString="{0:d}" HeaderText="Fecha de Nacimiento" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div>
                <!--PRECIO-->
                <label>Precio Total</label>
                <asp:TextBox runat="server" ID="txt_precioTotal" Enabled="false" CssClass="form-control" />
                <asp:RequiredFieldValidator ErrorMessage="Precio obligatorio" Text="*" ID="RequiredFieldValidator11" ValidationGroup="A" ControlToValidate="txt_precioTotal" runat="server" />
                <asp:CompareValidator Type="Double" Operator="DataTypeCheck" ID="CompareValidator6" runat="server" ValidationGroup="A" ControlToValidate="txt_precioTotal" ErrorMessage="Formato de precio invalido" Text="*" />

            </div>
            <br />
            <div>
                <!--Guardar-->
                <asp:Button Text="GUARDAR" runat="server" ID="btn_guardar" ValidationGroup="A" CssClass="btn btn-success" OnClick="btn_guardar_Click" />
                <asp:ValidationSummary runat="server" ValidationGroup="A" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlace_Pie" runat="Server">
    <script>
        $(function () {
            $("#<%= txt_fechaIda.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' }).val();;
        });
    </script>
    <script>
        $(function () {
            $("#<%= txt_fechaVuelta.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' }).val();;
        });
    </script>
</asp:Content>


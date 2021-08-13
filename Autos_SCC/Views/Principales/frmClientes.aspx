<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmClientes.aspx.cs" uiCulture="es" Culture="es-MX" Inherits="Autos_SCC.Views.Principales.frmClientes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../../jquery/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="../../jquery/jquery-ui-1.9.2.custom.min.js"></script>
    <link rel="stylesheet" href="../../jquery/jquery-ui-1.9.2.custom.min.css" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">
        function MostrarMensaje(mensaje, titulo)
        {
            var ventana = $('<div id="errortitulo" title="' + titulo + '"><span id="errormensaje">' + mensaje + '</span></div>');

            ventana.dialog({
                modal: true, 
                buttons: { "Aceptar": function () { $(this).dialog("close"); } },
                show: "fold",
                hide: "scale",
            });
        }

    </script>
        <div class="card">
            <div class="card-block" style="text-align:center;">
                <h3>Clientes</h3>
            </div>
        </div>
    <div class="card" style="min-height:70vh;">
    <asp:UpdatePanel ID="upaCotizacion" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HidIdCliente" runat="server" Value="0" />
            <asp:HiddenField ID="HidIdCP" runat="server" Value="0" />
            <asp:HiddenField ID="HidIdCPAval" runat="server" Value="0" />
            <fieldset style="text-align:left">
                
                <div style="width:100%; text-align:center;">
                    <h4>
                        Búsqueda de cliente
                    </h4>
                </div><br />
                
                    <table style="width:100%">
                        <tr>
                            <td style="width:20%">
                            </td>
                            <td style="width:20%">
                                <asp:Label ID="lblSucursal" runat="server" Text="Sucursal:" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:DropDownList ID="ddlSucursal" runat="server" Width="97%" CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="width:20%">
                            </td>
                            <td style="width:20%">
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="lblCotizacion" runat="server" Text="Cotización:" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCotizacion" runat="server" Width="97%" CssClass="form-control"
                                     AutoPostBack="true" OnSelectedIndexChanged="ddlCotizacion_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table><br />
                <div style="width:100%; background-color:#d0e7ff; border-radius:5px; padding:5px;">
                    <table style="width:100%">
                        <tr>
                            <td style="width:40%">
                                <asp:Label ID="lblCliente" runat="server" Text="" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:Label ID="lblTipoAuto" runat="server" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:Label ID="lblPrecio" runat="server" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:Label ID="lblPlazo" runat="server" CssClass="inputLabel"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                    
            </fieldset>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlSucursal" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <fieldset style="text-align:left"><br />
        <div style="width:100%; text-align:center;">
            <h4>
                Datos generales...
            </h4>
        </div><br />
        <asp:UpdatePanel ID="upaDatos" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td valign="top">
                            <table width="100%">
                                <tr>
                                    <td width="40%">
                                        <asp:Label ID="lblEstado" runat="server" Text="Estado: " CssClass="inputLabel" ></asp:Label>
                                    </td>
                                    <td width="60%">
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblReqEstado" runat="server" Text="*" CssClass="inputReqLabel"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblColonia" runat="server" Text="Colonia/Poblaci&oacute;n:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:DropDownList ID="ddlColonia" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlColonia_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblReqColonia" runat="server" Text="*" CssClass="inputReqLabel"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCalle" runat="server" Text="Calle:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtCalle" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2" style="text-align:left;">
                                                <asp:Label ID="lblReqCalle" runat="server" Text="*" CssClass="inputReqLabel"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNacionalidad" runat="server" Text="Nacionalidad:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:DropDownList ID="ddlNacionalidad" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="Mexicana" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Extranjera" Value="2"></asp:ListItem>
                                                </asp:DropDownList> 
                                            </div>
                                            <div class="col-md-2">

                                            </div>
                                        </div>
                                                                               
			                        </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNumeroIfe" runat="server" CssClass="inputLabel" 
                                            Text="Numero IFE:"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtNumeroIfe" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="ftbNumeroIfe" runat="server" TargetControlID="txtNumeroIfe"
                                             FilterMode="ValidChars" ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789"></cc1:FilteredTextBoxExtender></div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblReqNumeroIfe" runat="server" CssClass="inputReqLabel" 
                                            Text="*"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFechaNacimiento" runat="server" Text="Fec. Nacimiento:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td style="vertical-align:top">
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" Width="100%" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2" style="text-align:left;">
                                                <asp:ImageButton ID="imbFechaNacimiento" runat="server" ImageUrl="~/Images/Botones/Calendar.ico" Width="24px" Height="24px" style="margin-bottom:-15px;margin-left:5px;" />
                                                <cc1:CalendarExtender ID="calFechaNacimiento" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        PopupButtonID="imbFechaNacimiento" TargetControlID="txtFechaNacimiento">
                                                </cc1:CalendarExtender>
                                                <cc1:MaskedEditExtender ID="mskFechaNacimiento" runat="server" ClearTextOnInvalid="True"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFechaNacimiento"
                                                    UserDateFormat="DayMonthYear">
                                                </cc1:MaskedEditExtender>
                                            </div>
                                        </div>
                                        
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>                        
                                        <asp:Label ID="lblSexo" runat="server" Text="Sexo:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                            <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Hombre" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Mujer" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">

                                            </div>
                                        </div>
                                        
			                        </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblLada" runat="server" Text="Lada:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtLada" runat="server" MaxLength="3" CssClass="form-control"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="ftbLada" runat="server" FilterType="Numbers" FilterMode="ValidChars"
                                                    ValidChars="0123456789" TargetControlID="txtLada"></cc1:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-md-2">

                                            </div>
                                        </div>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>                        
                                        <asp:Label ID="lblTelefonoCel" runat="server" Text="Tel&eacute;fono Celular:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtTelefonoCel" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="ftbTelefonoCel" runat="server" TargetControlID="txtTelefonoCel"
                                                    FilterMode="ValidChars" ValidChars="0123456789()-"></cc1:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-md-2">

                                            </div>
                                        </div>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 51%; vertical-align:top">
                            <table style="text-align:left" width="100%">
                                <tr>
                                    <td width="40%">
                                        <asp:Label ID="lblDelegacion" runat="server" Text="Delegación/Municipio:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td width="60%">
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:DropDownList ID="ddlMunicipio" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlMunicipio_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblReqMunicipio" runat="server" Text="*" CssClass="inputReqLabel"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCP" runat="server" Text="C.P.:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtCP" runat="server" MaxLength="5" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblReqCP" runat="server" Text="*" CssClass="inputReqLabel"></asp:Label>
                                                <cc1:FilteredTextBoxExtender ID="ftbCP" runat="server" FilterType="Numbers" TargetControlID="txtCP"
                                                    ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNoExt" runat="server" Text="No. Ext:" CssClass="inputLabel"></asp:Label> 
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtNumExt" runat="server" MaxLength="20" CssClass="form-control" Width="100%"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblNoInt" runat="server" Text="No. Int : " CssClass="inputLabel"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtNumInt" runat="server" MaxLength="20" CssClass="form-control" Width="100%"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTipoIdentificacion" runat="server" Text="Tipo identificación:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" AutoPostBack="true" CssClass="form-control"
                                                    OnSelectedIndexChanged="ddlTipoIdentificacion_SelectedIndexChanged">
                                                    <asp:ListItem Text="IFE" Value = "0"></asp:ListItem>
                                                    <asp:ListItem Text="Licencia" Value = "1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">

                                            </div>
                                        </div>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCURP" runat="server" Text="CURP:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtCURP" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblReqCURP" runat="server" Text="*" CssClass="inputReqLabel"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblRfc" runat="server" Text="RFC:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtRFC" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">

                                            </div>
                                        </div>
                                        
		                            </td>
                                    </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEstadoCivil" runat="server" Text="Estado Civil:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:DropDownList ID="ddlEstadoCivil" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text ="Soltero" Value="1"></asp:ListItem> 
                                                    <asp:ListItem Text="Casado" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">

                                            </div>
                                        </div>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTelefono" runat="server" Text="Tel&eacute;fono:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtTelefono" runat="server" MaxLength="15" CssClass="form-control"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="ftbTelefono" runat="server" TargetControlID="txtTelefono"
                                                    FilterMode="ValidChars" ValidChars="0123456789()-"></cc1:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-md-2">

                                            </div>
                                        </div>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTiempoVivirDom" runat="server" Text="Tiempo de vivir Dom." CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:DropDownList ID="ddlTiempoVivirDomicilio" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="6 Meses" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="1 Año" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="2 Años" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Mas de 2 Años" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">

                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center"><br />
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlMunicipio" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlColonia" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </fieldset>
    
    <fieldset style="text-align:left"><br />
    <div style="width:100%; text-align:center;">
        <h4>
            Captura de aval...
        </h4>
    </div>
        <asp:UpdatePanel ID="upaCapturaAval" runat="server">
            <ContentTemplate><br />
                <div style="width:100%; text-align:center;">
                    <asp:Label ID="lblDeseaCapAval" runat="server" Text="¿Desea captura el aval del crédito?" CssClass="inputLabel"></asp:Label>
                    <asp:Button ID="btnCapAval" runat="server" Text="Capturar" CssClass="btn btn-success" Enabled="false" OnClick="btnCapAval_Click" />
                </div>
                <br />
                <br />
                <asp:GridView ID="gvAval" runat="server" AutoGenerateColumns="false" DataKeyNames="fi_Id" Width="80%"
                    PageSize="10" Font-Size="Small" BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464"
                    HeaderStyle-ForeColor="white" AllowSorting="True">
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="fc_NombreCompleto" HeaderText="Nombre del Aval" />
                        <asp:BoundField DataField="fc_TipoIdentificacion" HeaderText="Tipo identificación" />
                        <asp:BoundField DataField="fc_NoIFE" HeaderText="No. Identificacion" />
                    </Columns>
                </asp:GridView>
                <br />
                <div style="width:100%; text-align:center;">
                    <asp:Label ID="lblFormalizar" runat="server" Text="¿Enviar crédito a formalizar?" CssClass="inputLabel"></asp:Label>
                    <asp:Button ID="btnAceptarFormalizar" runat="server" Text="Aceptar" CssClass="btn btn-success" Enabled="false" OnClick="btnAceptarFormalizar_Click" />
                </div>
                <br />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAceptarFormalizar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </fieldset>

    </div>
    <%--Modal del Avales--%>
    <asp:HiddenField ID="hdCapturaAvalTarget" runat="server" />
    <cc1:ModalPopupExtender ID="mpeCapturaAval" runat="server" TargetControlID="hdCapturaAvalTarget" 
        PopupControlID="pnlCapturaAval" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlCapturaAval" runat="server" BorderColor="Black" BackColor="#e4e4e4" Height="570px"
        Width="880px" HorizontalAlign="Center" Style="display: none; border-radius:25px; padding:5px; box-shadow:3px 3px 3px #00000030;">
        <asp:UpdatePanel ID="upaAvales" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <h4><asp:Label ID="lblTituloModalAval" runat="server" Text="Captura de Aval" CssClass="labelTitle"></asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <table width="100%">
                                <tr>
                                    <td width="40%">
                                        <asp:Label ID="lblNombreAval" runat="server" Text="Nombre: " CssClass="inputLabel" ></asp:Label>
                                    </td>
                                    <td width="60%">
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtNombreAval" runat="server" MaxLength="100" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblReqNombreAval" runat="server" Text="*" CssClass="inputReqLabel"></asp:Label>
                                            </div>
                                        </div>  
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblApePaternoAval" runat="server" Text="Apellido paterno: " CssClass="inputLabel" ></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtApePaternoAval" runat="server" MaxLength="100" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblReqPaternoAval" runat="server" Text="*" CssClass="inputReqLabel"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEstadoAval" runat="server" Text="Estado: " CssClass="inputLabel" ></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:DropDownList ID="ddlEstadoAval" runat="server" CssClass="form-control" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlEstadoAval_SelectedIndexChanged" TabIndex="5"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblReqEstadoAval" runat="server" Text="*" CssClass="inputReqLabel"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblColoniaAval" runat="server" Text="Colonia/Poblaci&oacute;n:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:DropDownList ID="ddlColoniaAval" runat="server" CssClass="form-control" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlColoniaAval_SelectedIndexChanged" TabIndex="7"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblReqColoniaAval" runat="server" Text="*" CssClass="inputReqLabel"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCalleAval" runat="server" Text="Calle:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtCalleAval" runat="server" MaxLength="100" CssClass="form-control" TabIndex="9"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblReqCalleAval" runat="server" Text="*" CssClass="inputReqLabel"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNacionalidadAval" runat="server" Text="Nacionalidad:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:DropDownList ID="ddlNacionalidadAval" runat="server" CssClass="form-control" TabIndex="12">
                                                    <asp:ListItem Text="Mexicana" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Extranjera" Value="2"></asp:ListItem>
                                                </asp:DropDownList> 
                                            </div>
                                            <div class="col-md-2">

                                            </div>
                                        </div>
                                                                               
			                        </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNoIfeAval" runat="server" Text="Numero IFE:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtNoIfeAval" runat="server" CssClass="form-control" TabIndex="14"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblReNoIfeAval" runat="server" Text="*" CssClass="inputReqLabel"></asp:Label>
                                                <cc1:FilteredTextBoxExtender ID="ftbNoIfeAval" runat="server" TargetControlID="txtNoIfeAval"
                                                    FilterMode="ValidChars" FilterType="Numbers" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFechaNacimientoAval" runat="server" Text="Fec. Nacimiento:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td style="vertical-align:top">
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtFechaNacimientoAval" runat="server" CssClass="form-control" Width="100%" ReadOnly="true" TabIndex="16"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:ImageButton ID="imbFechaNacimientoAval" runat="server" ImageUrl="~/Images/Botones/Calendar.ico" Width="24px" Height="24px" style="margin-bottom:-15px;" />
                                                <cc1:CalendarExtender ID="calFechaNacimientoAval" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        PopupButtonID="imbFechaNacimientoAval" TargetControlID="txtFechaNacimientoAval">
                                                </cc1:CalendarExtender>
                                                <cc1:MaskedEditExtender ID="mskFechaNacimientoAval" runat="server" ClearTextOnInvalid="True"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFechaNacimientoAval"
                                                    UserDateFormat="DayMonthYear">
                                                </cc1:MaskedEditExtender>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>                        
                                        <asp:Label ID="lblSexoAval" runat="server" Text="Sexo:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:DropDownList ID="ddlSexoAval" runat="server" CssClass="form-control" TabIndex="18">
                                                    <asp:ListItem Text="Hombre" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Mujer" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">

                                            </div>
                                        </div>
                                        
			                        </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblLadaAval" runat="server" Text="Lada:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtLadaAval" runat="server" MaxLength="3" CssClass="form-control" TabIndex="20"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="ftbLadaAval" runat="server" FilterType="Numbers" FilterMode="ValidChars"
                                                ValidChars="0123456789" TargetControlID="txtLadaAval"></cc1:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-md-2">
                                                
                                            </div>
                                        </div> 
                                    </td>
                                </tr>
                                <tr>
                                    <td>                        
                                        <asp:Label ID="lblTelefonoCelAval" runat="server" Text="Tel&eacute;fono Celular:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtTelefonoCelAval" runat="server" MaxLength="10" CssClass="form-control" TabIndex="22"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="ftbTelefonoCelAval" runat="server" TargetControlID="txtTelefonoCelAval"
                                                FilterMode="ValidChars" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-md-2">

                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 51%; vertical-align:top">
                            <table style="text-align:left" width="100%">
                                <tr>
                                    <td width="40%">
                                        <asp:Label ID="lblSegNombreAval" runat="server" Text="Seg. Nombre: " CssClass="inputLabel" ></asp:Label>
                                    </td>
                                    <td width="60%">
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtSegNombreAval" runat="server" MaxLength="100" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">

                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblApeMaternoAval" runat="server" Text="Apellido materno: " CssClass="inputLabel" ></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtApeMaternoAval" runat="server" MaxLength="100" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblReqApeMaternoAval" runat="server" Text="*" CssClass="inputReqLabel"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMunicipioAval" runat="server" Text="Delegación/Municipio:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:DropDownList ID="ddlMunicipioAval" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlMunicipioAval_SelectedIndexChanged" TabIndex="6"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblReqMunicipioAval" runat="server" Text="*" CssClass="inputReqLabel"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCPAval" runat="server" Text="C.P.:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtCPAval" runat="server" MaxLength="5" CssClass="form-control" TabIndex="8"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblReqCPAval" runat="server" Text="*" CssClass="inputReqLabel"></asp:Label>
                                                <cc1:FilteredTextBoxExtender ID="ftbCPAval" runat="server" FilterType="Numbers" TargetControlID="txtCPAval"
                                            ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                            </div>
                                            </div>
                                        </div>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNoExteriorAval" runat="server" Text="No. Ext:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtNoExteriorAval" runat="server" MaxLength="20" CssClass="form-control" Width="100%" TabIndex="10"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblNoInteriorAval" runat="server" Text="No. Int : " CssClass="inputLabel"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtNoInteriorAval" runat="server" MaxLength="20" CssClass="form-control" Width="100%" TabIndex="11"></asp:TextBox>
                                            </div>
                                        </div> 
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTipoIdentificacionAval" runat="server" Text="Tipo identificación:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                              <asp:DropDownList ID="ddlTipoIdentificacionAval" runat="server" AutoPostBack="true" CssClass="form-control"
                                                    OnSelectedIndexChanged="ddlTipoIdentificacionAval_SelectedIndexChanged" TabIndex="13">
                                                    <asp:ListItem Text="IFE" Value = "0"></asp:ListItem>
                                                    <asp:ListItem Text="Licencia" Value = "1"></asp:ListItem>
                                              </asp:DropDownList>  
                                            </div>
                                            <div class="col-md-2">
                                                
                                            </div>
                                        </div>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCURPAval" runat="server" Text="CURP:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtCURPAval" runat="server" CssClass="form-control" TabIndex="15"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lblReqCURPAval" runat="server" Text="*" CssClass="inputReqLabel"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblREFAval" runat="server" Text="RFC:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtRFCAval" runat="server" MaxLength="10" CssClass="form-control" TabIndex="17"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                
                                            </div>
                                        </div>
                                        
		                            </td>
                                    </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEstadoCivilAval" runat="server" Text="Estado Civil:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:DropDownList ID="ddlEstadoCivilAval" runat="server" CssClass="form-control" TabIndex="19">
                                                    <asp:ListItem Text ="Soltero" Value="1"></asp:ListItem> 
                                                    <asp:ListItem Text="Casado" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                
                                            </div>
                                        </div>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTelefonoAval" runat="server" Text="Tel&eacute;fono:" CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtTelefonoAval" runat="server" MaxLength="8" CssClass="form-control" TabIndex="21"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="ftbTelefonoAval" runat="server" TargetControlID="txtTelefonoAval" ValidChars="0123456789"
                                                    FilterMode="ValidChars" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-md-2">
                                                
                                            </div>
                                        </div>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTiempoVivirDomAval" runat="server" Text="Tiempo de vivir Dom." CssClass="inputLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:DropDownList ID="ddlTiempoVivirDomAval" runat="server" CssClass="form-control" TabIndex="23">
                                                    <asp:ListItem Text="6 Meses" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="1 Año" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="2 Años" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Mas de 2 Años" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                
                                            </div>
                                        </div>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnGuardarAval" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardarAval_Click" />
                            <asp:Button ID="btnCancelarCapturaAval" runat="server" Text="Cancelar" OnClick="btnCancelarCapturaAval_Click" CssClass="btn btn-danger" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlEstadoAval" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlMunicipioAval" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlColoniaAval" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    
    
</asp:Content>

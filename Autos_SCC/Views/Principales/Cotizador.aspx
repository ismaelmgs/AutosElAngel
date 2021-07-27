<%@ Page Title="Cotizador de créditos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="es" Culture="es-MX"
    EnableEventValidation="false" CodeBehind="Cotizador.aspx.cs" Inherits="Autos_SCC.Views.Principales.Cotizador" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../ControlesUsuario/ucModalConfirm.ascx" TagName="ucModalConfirm" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../../Styles/EstilosGrid.css" rel="Stylesheet" type="text/css" />
    <link href="../../Styles/StyleGeneral.css" rel="Stylesheet" type="text/css" />
    <link href="../../Styles/EstilosModal.css" rel="Stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../jquery/jquery-ui-1.9.2.custom.min.css" type="text/css" />
    <script type="text/javascript" src="../../jquery/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="../../jquery/jquery-ui-1.9.2.custom.min.js"></script>
    <script type="text/javascript" language="javascript">
        function switchViews(obj, row) {
            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);

            if (div.style.display == "none") {
                div.style.display = "inline";
                if (row == 'alt') {
                    img.src = "../../Images/Botones/flecha_cierra.png";
                }
                else {
                    img.src = "../../Images/Botones/flecha_cierra.png";
                }

                img.alt = "Cerrar para visualizar otros integrantes";
            }
            else {
                div.style.display = "none";
                if (row == 'alt') {
                    img.src = "../../Images/Botones/flecha_abre1.png";
                }
                else {
                    img.src = "../../Images/Botones/flecha_abre1.png";
                }
                img.alt = "Ampliar para visualizar integrantes";
            }

        }

        function ValorPlazo(valor, plazoPDF) 
        {
            eval("form1.HidPlazo.value = " + valor + ";");
            eval("form1.HidPlazoPDF.value = " + plazoPDF + ";");

        }

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

        function MostrarCambioTasa()
        {
            var modalId = '<%=mpeCambioTasa.ClientID%>';
            var modal = $find(modalId);
            modal.show();
        }

        function OcultarModal()
        {
            var txtTasa = '<%=txtTasaInteres.ClientID%>';
            txtTasa.value = "";

            var modalId = '<%=mpeCambioTasa.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }

    </script>

    <asp:UpdatePanel id="UpdatePanelHid" runat="server" updatemode="Always">
        <ContentTemplate>
            <asp:HiddenField ID="HidPrecio" Value="100000" runat="server" />
            <asp:HiddenField ID="HidPlazo" Value="0" runat="server" />
            <asp:HiddenField ID="HidPlazoPDF" Value="0" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <table style="width: 100%;">
        <tr style="height:25px">
            <td width="5%">
            </td>
            <td width="20%">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="EtiquetaSimple" Font-Size="Small"></asp:Label>
            </td>
            <td width="30%">
                <asp:TextBox ID="txtNombre" runat="server" Width="90%" CssClass="CajaSimple"></asp:TextBox>
            </td>
            <td width="40%">
            </td>
            <td width="5%">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="lblSegNombre" runat="server" Text="Segundo nombre:" CssClass="EtiquetaSimple" Font-Size="Small"></asp:Label>
            </td>
            <td>
            <asp:TextBox ID="txtSegNombre" runat="server" Width="90%" CssClass="CajaSimple"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="lblApePaterno" runat="server" Text="Apellido paterno:" 
                    CssClass="EtiquetaSimple" Font-Size="Small"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtApaPaterno" runat="server" Width="90%" CssClass="CajaSimple"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="lblApeMaterno" runat="server" Text="Apellido materno:" 
                    CssClass="EtiquetaSimple" Font-Size="Small"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtApeMaterno" runat="server" Width="90%" CssClass="CajaSimple"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="lblSeleccionaAuto" runat="server" Text="Selecciona auto:" 
                    CssClass="EtiquetaSimple" Font-Size="Small"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAuto" runat="server" Width="81%" CssClass="CajaSimple"></asp:TextBox>
                &nbsp;<asp:ImageButton ID="imbBuscarAuto" runat="server" ImageUrl="~/Images/Botones/Find.ico" OnClick="imbBuscarAuto_Click"
                    style="vertical-align:middle"/>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="lblPlazo" runat="server" Text="Plazo:" CssClass="EtiquetaSimple" 
                    Font-Size="Small"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlPlazo" runat="server" Width="90%" CssClass="ComboSimple">
                    <asp:ListItem Text="Seleccione" Value="0"></asp:ListItem>
                    <asp:ListItem Text="6 Meses" Value="1"></asp:ListItem>
                    <asp:ListItem Text="12 Meses" Value="2"></asp:ListItem>
                    <asp:ListItem Text="18 Meses" Value="3"></asp:ListItem>
                    <asp:ListItem Text="24 Meses" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="lblPecio" runat="server" Text="Precio:" 
                    CssClass="EtiquetaSimple" Font-Size="Small"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPrecio" runat="server" Width="50%" CssClass="CajaSimple"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="lblEnganche" runat="server" Text="Enganche:" 
                    CssClass="EtiquetaSimple" Font-Size="Small"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEnganche" runat="server" Width="50%" CssClass="CajaSimple"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
                
            </td>            
            <td>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                
                &nbsp;</td>            
            <td>
            &nbsp;</td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="center" colspan="3">
                <br />
                <asp:updatepanel id="updCotizar" runat="server" updatemode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvCotizar" runat="server" AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false"
                            PagerSettings-Mode="NumericFirstLast" PageSize="100" OnRowDataBound="GridCotizar_RowDataBound"
                            AutoUpdateAfterCallBack="True" PagerStyle-CssClass="pgr" CssClass="mGrid"
                            AlternatingRowStyle-CssClass="alt" Width="100%" DataKeyNames="Plazo">
                            <Columns>
                                <asp:TemplateField>
                                    <itemtemplate>
                                        <a href="javascript:switchViews('div<%# Eval("Plazo") %>', 'one');">
                                            <img id="imgdiv<%# Eval("Plazo") %>" alt="Click para visualizar integrantes" border="0" src="../../Images/Botones/flecha_abre1.png" />
                                        </a>
                                    </itemtemplate>
                                    <alternatingitemtemplate>
                                        <a href="javascript:switchViews('div<%# Eval("Plazo") %>', 'alt');">
                                            <img id="imgdiv<%# Eval("Plazo") %>" alt="Click para visualizar integrantes" border="0" src="../../Images/Botones/flecha_abre1.png" />
                                        </a>
                                    </alternatingitemtemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Seleccionar">
                                    <itemtemplate>
                                        <input name="OptPlazo" type="radio" value='<%# Eval("Plazo") %>' />
                                        <%--<input name="OptPlazo" type="radio" value='<%# Eval("Plazo") %>' onclick="JavaScript:ValorPlazo(this.value,'<%# Eval("DescPlazo") %>');" />--%>
                                    </itemtemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DescPlazo" HeaderText="Plazo"/>
                                <asp:BoundField DataField="Importe" HeaderText="Importe del Credito" DataFormatString="{0:c}"/>
                                <asp:BoundField DataField="Intereses" HeaderText="Interes del crédito con iva" DataFormatString="{0:c}"/>
                                <asp:BoundField DataField="TotalPagar" HeaderText="Total a pagar" DataFormatString="{0:c}"/>
                                <asp:BoundField DataField="PrimerPago" HeaderText="Pago cumplido" DataFormatString="{0:c}"/>
                                <asp:BoundField DataField="Ahorro" HeaderText="Importe a Ahorrar" DataFormatString="{0:c}"/>
                                <asp:TemplateField>
                                    <itemtemplate>
                                        <tr>
                                            <td colspan="100%" align="left">
                                                <div id="div<%# Eval("Plazo") %>" style="display:none;position:relative;left:100px;"  >
                                                    <asp:GridView ID="GrdCotizaDetalle" runat="server" Width="80%"
                                                        AutoGenerateColumns="false" DataKeyNames="Plazo"
                                                        OnRowDataBound="GrdCotizaDetalle_RowDataBound"
                                                        ShowFooter = "true" PagerStyle-CssClass="pgr" CssClass="mGrid"
                                                        AlternatingRowStyle-CssClass="alt" 
                                                        EmptyDataText="No hay resultados para era busqueda." >                                                        
                                                        <Columns>
                                                            <asp:BoundField DataField="NoPago" HeaderText="No. Pago"/>
                                                            <asp:BoundField DataField="PagoNormal" HeaderText="Pago normal"/>
                                                            <asp:BoundField DataField="PagoAdelantado" HeaderText="Pago adelantado"/>
                                                            <asp:BoundField DataField="PagoMora" HeaderText="Pago en Mora"/>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </itemtemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" />
                        </asp:GridView>
                        <br />                        
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnGenerar" EventName="Click" />
                    </Triggers>
                </asp:updatepanel>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="3">                
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2">
                <asp:Button ID="btnGenerar" runat="server" Text=" GENERAR " OnClick="btnGenerar_Click"
                    CssClass="ButtonStyle" Font-Size="X-Small" />
                &nbsp;<asp:Button ID="btnImprimir" runat="server" Text=" IMPRIMIR " 
                    CssClass="ButtonStyle" Font-Size="X-Small" />
                &nbsp;<asp:Button ID="btnGuardar" runat="server" Text=" GUARDAR " 
                    CssClass="ButtonStyle" Font-Size="X-Small" />
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="3">
                <asp:UpdatePanel ID="upaAgregarPagos" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlAgregarPagos" runat="server" GroupingText="Opciones de cotización" Visible="false">
                            <asp:Label ID="lblAgregarPago" runat="server" Text="¿Deseas agregar pagos individuales?"></asp:Label>
                            <asp:Button ID="btnAgregarPago" runat="server" Text="  SI  " 
                                onclick="btnAgregarPago_Click" />

                            <br />

                            <asp:GridView ID="gvPagosIndividuales" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="30%" 
                                Font-Size="10px" OnRowDeleting="gvPagosIndividuales_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:c}"/>
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha de pago"  />
                                    <asp:CommandField ButtonType="Image" ShowDeleteButton="true" DeleteImageUrl="~/Images/Iconos/Erase.ico" />
                                </Columns>
                                <EmptyDataTemplate>
                                    No se encontraron registros
                                </EmptyDataTemplate>
                            </asp:GridView>

                            <br />

                            <asp:Label ID="lblCambioTasa" runat="server" Text="¿Cambiar la tasa de Interes auna preferncial?"></asp:Label>
                            <asp:Button ID="btnCambioTasa" runat="server" Text="  SI  " OnClientClick="MostrarCambioTasa();" />
                            <br />
                            <asp:Panel ID="pnlTasaPreferencial" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTasaPreferencial" runat="server" Text="Nueva tasa de interes:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTasaPreferencial" runat="server" Enabled="false"></asp:TextBox>
                                            <asp:ImageButton ID="imbTasaPreferencial" runat="server" ImageUrl="~/Images/Botones/revert.png" />

                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>            
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="3">
                
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>

    <%--Modal de Pagos individuales--%>
    <asp:HiddenField ID="hdTarget" runat="server" />
    <cc1:ModalPopupExtender ID="mpeAgregarPago" runat="server" TargetControlID="hdTarget" 
        PopupControlID="Panel1" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BackColor="White" Height="160px"
        Width="300px" HorizontalAlign="Center" Style="display: none">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="header">
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblCaption" runat="server" Text="Pagos Individuales" CssClass="TituloEtiquetas"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <center>
                    <table width="90%">
                        <tr>
                            <td align="left" width="35%">
                                <asp:Label ID="lblImportePago" runat="server" Text="Importe:" Font-Size="Small" CssClass="EtiquetaSimple"></asp:Label>
                            </td>
                            <td align="left" width="55%">
                                <asp:TextBox ID="txtImportePago" runat="server" Width="96%" CssClass="CajaSimple"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvImportePago" runat="server" ErrorMessage="El campo es requerido" Display="Dynamic"
                                    ForeColor="Red" ValidationGroup="PagoIndividual" ControlToValidate="txtImportePago"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblFechaPago" runat="server" Text="Fecha del pago:" Font-Size="Small" CssClass="EtiquetaSimple"></asp:Label>                            
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFechaPago" runat="server" Width="85%" CssClass="CajaSimple"></asp:TextBox>
                                <asp:ImageButton ID="imbFechaPago" runat="server" ImageUrl="~/Images/Botones/Calendar.ico" Width="16px" Height="16px" />
                                <cc1:CalendarExtender ID="calFechaPago" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imbFechaPago" TargetControlID="txtFechaPago">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvFechaPago" runat="server" ControlToValidate="txtFechaPago"
                                        Display="Dynamic" ErrorMessage="El campo es requerido" ForeColor="Red" ValidationGroup="PagoIndividual">
                                    </asp:RequiredFieldValidator>
                                <cc1:MaskedEditExtender ID="mskFechaPago" runat="server" ClearTextOnInvalid="True"
                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFechaPago"
                                    UserDateFormat="DayMonthYear">
                                </cc1:MaskedEditExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height:10px">
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnAgregarPagoModal" runat="server" Text=" AGREGAR " CssClass="ButtonStyle" OnClick="btnAgregarPagoModal_Click" />
                            </td>
                            <td align="left">
                                <asp:Button ID="btnCancelarPagoModal" runat="server" Text=" CANCELAR " CssClass="ButtonStyle" OnClick="btnCancelarPagoModal_Click" />
                            </td>
                        </tr>
                    </table>
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>


    <asp:HiddenField ID="hdTargetCambioTasa" runat="server" />
    <cc1:ModalPopupExtender ID="mpeCambioTasa" runat="server" TargetControlID="hdTargetCambioTasa" 
        PopupControlID="pnlCambioTasa" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    
    <asp:Panel ID="pnlCambioTasa" runat="server" BorderColor="Black" BackColor="White" Height="115px"
        Width="250px" HorizontalAlign="Center" Style="display: none">
        <asp:UpdatePanel ID="upaCambioTasa" runat="server">
            <ContentTemplate>
                <div class="header">
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblTituloCambioTasa" runat="server" Text="Cambio de tasa de interes" CssClass="TituloEtiquetas"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <center>
                    <table width="90%">
                        <tr>
                            <td width="60%">
                                <asp:Label ID="lblTasaInteres" runat="server" Text="Interes Mensual (%):" CssClass="EtiquetaSimple"></asp:Label>
                            </td>
                            <td width="40%">
                                <asp:TextBox ID="txtTasaInteres" runat="server" CssClass="CajaSimple"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height:10px"></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnCambiarTasa" runat="server" Text=" CAMBIAR " CssClass="ButtonStyle" OnClick="btnCambiarTasa_Click" />
                            </td>
                            <td align="left">
                                <asp:Button ID="btnCancelarCambioTasa" runat="server" Text=" CANCELAR " CssClass="ButtonStyle" OnClientClick="OcultarModal();" />
                            </td>
                        </tr>
                    </table>
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

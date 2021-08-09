<%@ Page Title="Cotizador de créditos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="es" Culture="es-MX"
    EnableEventValidation="false" CodeBehind="Cotizador.aspx.cs" Inherits="Autos_SCC.Views.Principales.Cotizador" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../ControlesUsuario/ucModalConfirm.ascx" TagName="ucModalConfirm" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <link href="../../jquery/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../jquery/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="../../jquery/jquery-ui-1.9.2.custom.min.js"></script>
    <%--<script type="text/javascript" language="javascript">
        var ModalProgress = '<%= ModalProgress.ClientID %>';         
	</script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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

//        function MostrarCambioTasa()
//        {
//            var modalId = '<%=mpeCambioTasa.ClientID%>';
//            var modal = $find(modalId);
//            modal.show();
//        }

        function OcultarModal()
        {
            var txtTasa = '<%=txtTasaInteres.ClientID%>';
            txtTasa.value = "";

            var modalId = '<%=mpeCambioTasa.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }

        function OcultaModalBusquedaAuto()
        {
             var txtTasa = '<%=txtTextoBusqueda.ClientID%>';
            txtTasa.value = "";

            var modalId = '<%=mpeBuscarAuto.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }

        function OcultaBusquedaCliente()
        {
            var txtNombreCli = '<%=txtBusCliNombre.ClientID%>';
            txtNombreCli.value = "";

            var modalId = '<%=mpeBusquedaCliente.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }

    </script>
    
    <asp:UpdatePanel ID="upaPrincipal" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HidAuto" Value="0" runat="server" />
            <asp:HiddenField ID="HidTasa" Value="0" runat="server" />
            <asp:HiddenField ID="HidClienteE" Value="0" runat="server" />
            <asp:HiddenField ID="HidGenerar" Value="0" runat="server" />
            <!-- Button trigger modal -->
            <div class="card">
                <div class="card-block" style="text-align:center;">
                    <h3>Cotizador</h3>
                </div>
            </div>
            <div class="card">
            <table class="table table-hover" style="width:90%;margin:0 auto;">
                <tr>
                    <td colspan="5">
                        &nbsp;
                    </td>
                </tr>
                <tr style="height:25px;">
                    <td width="5%">
                    </td>
                    <td>
                        <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="inputLabel"></asp:Label>
                    </td>
                    <td>
                        <div class="row">
                            <div class="col-md-11" style="text-align:right; padding-left:2.5%;">
                                <asp:TextBox ID="txtNombre" runat="server" Width="" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-1" style="text-align:left;">
                                <asp:ImageButton ID="imbBuscaCliente" runat="server" ImageUrl="~/Images/Botones/Find.ico" OnClick="imbBuscaCliente_Click"
                            style="vertical-align:middle" ToolTip="Selecciona un auto existente"/>
                            </div>
                        </div>
                    </td>
                    <td width="1%">
                        <asp:Label ID="lblReqNombre" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        &nbsp;&nbsp;<asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                            ControlToValidate="txtNombre" Display="Dynamic" 
                            ErrorMessage="El nombre es requerido" ForeColor="Red" 
                            ValidationGroup="VCotizar"></asp:RequiredFieldValidator>
                    </td>
                    <td width="5%">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblSegNombre" runat="server" Text="Segundo nombre:" CssClass="inputLabel"></asp:Label>
                    </td>
                    <td>
                        <div class="row">
                            <div class="col-md-11" style="text-align:right; padding-left:2.5%;">
                                <asp:TextBox ID="txtSegNombre" runat="server" Width="" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-1" style="text-align:left;">
                                &nbsp;
                            </div>
                        </div>
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
                        <asp:Label ID="lblApePaterno" runat="server" Text="Apellido paterno:" CssClass="inputLabel"></asp:Label>
                    </td>
                    <td>
                        <div class="row">
                            <div class="col-md-11" style="text-align:right; padding-left:2.5%;">
                                <asp:TextBox ID="txtApePaterno" runat="server" Width="" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-1" style="text-align:left;">
                                &nbsp;
                            </div>
                        </div>
                    </td>
                    <td>
                        <asp:Label ID="lblReqApePaterno" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        &nbsp;<asp:RequiredFieldValidator ID="rfvApePaterno" runat="server" 
                            ControlToValidate="txtApePaterno" Display="Dynamic" 
                            ErrorMessage="El apellido es requerido" ForeColor="Red" 
                            ValidationGroup="VCotizar"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblApeMaterno" runat="server" Text="Apellido materno:" 
                            CssClass="inputLabel"  ></asp:Label>
                    </td>
                    <td>
                        <div class="row">
                            <div class="col-md-11" style="text-align:right; padding-left:2.5%;">
                                <asp:TextBox ID="txtApeMaterno" runat="server" Width="" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-1" style="text-align:left;">
                                &nbsp;
                            </div>
                        </div>
                        
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
                            CssClass="inputLabel"  ></asp:Label>
                    </td>
                    <td>
                        <div class="row">
                            <div class="col-md-11" style="text-align:right; padding-left:2.5%;">
                                <asp:TextBox ID="txtAuto" runat="server" Width="" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-1" style="text-align:left;">
                                <asp:ImageButton ID="imbBuscarAuto" runat="server" ImageUrl="~/Images/Botones/Find.ico" OnClick="imbBuscarAuto_Click"
                            style="vertical-align:middle" ToolTip="Selecciona un auto existente"/>
                            </div>
                        </div>
                    </td>
                    <td>
                        <asp:Label ID="lblReqAuto" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        &nbsp;&nbsp;<asp:RequiredFieldValidator ID="rfvAuto" runat="server" 
                            ControlToValidate="txtAuto" Display="Dynamic" 
                            ErrorMessage="El auto es requerido" ForeColor="Red" ValidationGroup="VCotizar"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblPlazo" runat="server" Text="Plazo:" CssClass="inputLabel" 
                             ></asp:Label>
                    </td>
                    <td>
                        <div class="row">
                            <div class="col-md-11" style="text-align:right; padding-left:2.5%;">
                                <asp:DropDownList ID="ddlPlazo" runat="server" Width="95%" CssClass="form-control">
                        </asp:DropDownList>
                            </div>
                            <div class="col-md-1" style="text-align:left;">
                                &nbsp;
                            </div>
                        </div>
                    </td>
                    <td>
                        <asp:Label ID="lblReqPlazo" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        &nbsp;<asp:RequiredFieldValidator ID="rfvPlazo" runat="server" 
                            ControlToValidate="ddlPlazo" Display="Dynamic" 
                            ErrorMessage="El plazo es requerido" ForeColor="Red" InitialValue="0" 
                            ValidationGroup="VCotizar"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblPecio" runat="server" Text="Precio:" 
                            CssClass="inputLabel"  ></asp:Label>
                    </td>
                    <td>
                        <div class="row">
                            <div class="col-md-11" style="text-align:right; padding-left:2.5%;">
                                <asp:TextBox ID="txtPrecio" runat="server" Width="50%" CssClass="form-control" Enabled="false"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="ftbPrecio" runat="server" FilterMode="ValidChars" FilterType="Numbers"
                            ValidChars="0123456789" TargetControlID="txtPrecio"></cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1" style="text-align:left;">
                                &nbsp;
                            </div>
                        </div>
                        
                    </td>
                    <td>
                        <asp:Label ID="lblReqPrecio" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        &nbsp;<asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                            ControlToValidate="txtPrecio" Display="Dynamic" 
                            ErrorMessage="El precio es requerido" ForeColor="Red" 
                            ValidationGroup="VCotizar"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblEnganche" runat="server" Text="Enganche:" 
                            CssClass="inputLabel"  ></asp:Label>
                    </td>
                    <td>
                        <div class="row">
                            <div class="col-md-11" style="text-align:right; padding-left:2.5%;">
                               <asp:TextBox ID="txtEnganche" runat="server" Width="50%" CssClass="form-control"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="rfbEngancge" runat="server" FilterMode="ValidChars" FilterType="Numbers"
                            ValidChars="0123456789" TargetControlID="txtEnganche"></cc1:FilteredTextBoxExtender> 
                            </div>
                            <div class="col-md-1" style="text-align:left;">
                                &nbsp;
                            </div>
                        </div>
                    </td>
                    <td>
                        <asp:Label ID="lblReqEnganche" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        &nbsp;<asp:RequiredFieldValidator ID="rfvEnganche" runat="server" 
                            ControlToValidate="txtEnganche" Display="Dynamic" 
                            ErrorMessage="El enganche es requerido" ForeColor="Red" 
                            ValidationGroup="VCotizar"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblSucursal" runat="server" Text="Sucursal cotización:" CssClass="inputLabel"></asp:Label>
                    </td>
                    <td>
                        <div class="row">
                            <div class="col-md-11" style="text-align:right; padding-left:2.5%;">
                                <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="form-control" 
                            Width="95%"></asp:DropDownList>
                            </div>
                            <div class="col-md-1" style="text-align:left;">
                                &nbsp;
                            </div>
                        </div>
                    </td>
                    <td>
                        <asp:Label ID="lblReqSucursal" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        &nbsp;<asp:RequiredFieldValidator ID="rfvSucursal" runat="server" 
                            ControlToValidate="ddlSucursal" Display="Dynamic" InitialValue="0"
                            ErrorMessage="La sucursal es requerida" ForeColor="Red" 
                            ValidationGroup="VCotizar"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblCorreoElectronico" runat="server" CssClass="inputLabel" 
                              Text="Correo electronico:"></asp:Label>
                    </td>
                    <td>
                        <div class="row">
                            <div class="col-md-11" style="text-align:right; padding-left:2.5%;">
                                <asp:TextBox ID="txtCorreoElectronico" runat="server" CssClass="form-control" Width=""></asp:TextBox>
                            </div>
                            <div class="col-md-1" style="text-align:left;">
                                &nbsp;
                            </div>
                        </div>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="3" style="text-align:center;">
                        <asp:Label ID="lblInformacion" runat="server" Text="(Los campos marcados con * son obligatorios)" ForeColor="Red" CssClass="EtiquetaInformativa"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="center" colspan="3">
                        <br />
                        <asp:updatepanel id="updCotizar" runat="server" updatemode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="gvCotizar" runat="server" 
                                    AllowPaging="false" AutoGenerateColumns="false"
                                    OnRowDataBound="GridCotizar_RowDataBound"
                                    PageSize="10" BorderStyle="None" 
                                    BorderWidth="0px" HeaderStyle-BackColor="#646464"
                                    HeaderStyle-ForeColor="white" AllowSorting="True"
                                    Width="100%" DataKeyNames="Plazo">
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <itemtemplate>
                                                <a href="javascript:switchViews('div<%# Eval("Plazo") %>', 'one');">
                                                    <img id="imgdiv<%# Eval("Plazo") %>" alt="Click para visualizar la tabla de amortización" border="0" src="../../Images/Botones/flecha_abre1.png" />
                                                </a>
                                            </itemtemplate>
                                            <alternatingitemtemplate>
                                                <a href="javascript:switchViews('div<%# Eval("Plazo") %>', 'alt');">
                                                    <img id="imgdiv<%# Eval("Plazo") %>" alt="Click para visualizar la tabla de amortización" border="0" src="../../Images/Botones/flecha_abre1.png" />
                                                </a>
                                            </alternatingitemtemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Seleccionar">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rbPlazo" runat="server" AutoPostBack="true" OnCheckedChanged="rbPlazo_CheckedChanged" />
                                            </ItemTemplate>
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
                                                                OnRowDataBound="GrdCotizaDetalle_RowDataBound" ShowFooter = "true" 
                                                                EmptyDataText="No hay resultados para esta busqueda." PageSize="10" 
                                                                BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" HeaderStyle-ForeColor="white" AllowSorting="True">
                                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                                                                <AlternatingRowStyle BackColor="White" />
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
                    <td colspan="2"  style="text-align:center">
                        <asp:Button ID="btnGenerar" runat="server" Text=" GENERAR " OnClick="btnGenerar_Click"
                            CssClass="btn btn-primary" Font-Size="X-Small" />
                        &nbsp;<asp:Button ID="btnImprimir" runat="server" Text=" IMPRIMIR " OnClick="btnImprimir_Click"
                            CssClass="btn btn-primary" Font-Size="X-Small" />
                        &nbsp;<asp:Button ID="btnGuardar" runat="server" Text=" GUARDAR " 
                            CssClass="btn btn-primary" Font-Size="X-Small" OnClick="btnGuardar_Click" />
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
                    <td colspan="3" style="text-align:left">
                        <asp:UpdatePanel ID="upaAgregarPagos" runat="server">
                            <ContentTemplate>
                                <fieldset id="fPagos" runat="server" visible="false" style="text-align:left">
                                    <legend>
                                        <span>
                                            Opciones de cotización
                                        </span>
                                    </legend>
                            
                                    <asp:Panel ID="pnlAgregarPagos" runat="server"  Visible="false">
                                        <div style="text-align:left">
                                            
                                            <asp:Label ID="lblSinIntereses" runat="server" Text="Meses sin Intereses"></asp:Label>
                                            <asp:CheckBox ID="chkSinIntereses" runat="server" />
                                            
                                            <br />
                                            <br />

                                            <asp:Label ID="lblAgregarPago" runat="server" Text="¿Deseas agregar pagos individuales?"></asp:Label>
                                            <asp:Button ID="btnAgregarPago" runat="server" Text="  SI  " CssClass="button"
                                                onclick="btnAgregarPago_Click" />

                                            <br />

                                            <asp:GridView ID="gvPagosIndividuales" runat="server" AutoGenerateColumns="false" Width="30%" 
                                                Font-Size="10px" OnRowDeleting="gvPagosIndividuales_RowDeleting"
                                                PageSize="10" BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" HeaderStyle-ForeColor="white" AllowSorting="True">
                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                                                <AlternatingRowStyle BackColor="White" />
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

                                            <asp:Label ID="lblCambioTasa" runat="server" Text="¿Cambiar la tasa de Interes a una preferencial?"></asp:Label>
                                            <asp:Button ID="btnCambioTasa" runat="server" Text="  SI  " OnClick="btnCambioTasa_Click" CssClass="button" />
                                            <br />
                                            <asp:Panel ID="pnlTasaPreferencial" runat="server" Visible="false">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTasaPreferencial" runat="server" Text="Nueva tasa de interes:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTasaPreferencial" runat="server" Enabled="false"></asp:TextBox>
                                                            <asp:ImageButton ID="imbTasaPreferencial" runat="server" ImageUrl="~/Images/Botones/revert.png" Width="16px" Height="16px" OnClick="imbTasaPreferencial_Click" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                    </asp:Panel>
                                </fieldset>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnCambioTasa" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
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
            </div>
<!-- RLR Modal inicia-->
            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModalCenter">
              Abrir Modal
            </button>

            <!-- Modal -->
            <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
              <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                  <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle">Título Modal</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body">
                    ...
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-success" data-dismiss="modal">Cerrar</button>
                  </div>
                </div>
              </div>
            </div>
<!-- RLR Modal fin-->

            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnImprimir" />
        </Triggers>
    </asp:UpdatePanel>
    

    <%--Modal para Buscar Autos--%>
    <asp:HiddenField ID="hdTargetBAuto" runat="server" />
    <cc1:ModalPopupExtender ID="mpeBuscarAuto" runat="server" TargetControlID="hdTargetBAuto" 
        PopupControlID="pnlBuscaAuto" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlBuscaAuto" runat="server" BorderColor="Black" BackColor="White" Height="400px"
        Width="550px" HorizontalAlign="Center" Style="display: none">
        <asp:UpdatePanel ID="upaBuscaAuto" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td colspan="2" style="text-align:center">
                            <asp:Label ID="lblTituloBuscaAuto" runat="server" Text="Búsqueda de autos" CssClass="labelTitleModal"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <center>
                    <table width="99%">
                        <tr>
                            <td style="text-align:left; width:25%">
                                <asp:Label ID="lblBusquedaPor" runat="server" Text="Busque por:" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="text-align:left; width:45%">
                                <asp:DropDownList ID="ddlTipoBusqueda" runat="server" Width="97%" AutoPostBack="true" CssClass="listInput" OnSelectedIndexChanged="ddlTipoBusqueda_SelectedIndexChanged">
                                    <asp:ListItem Text="Seleccione" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Placa" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Marca" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Modelo" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Color" Value="4"></asp:ListItem>      
                                </asp:DropDownList>
                            </td>
                            <td width="20%" style="text-align:left">
                                <asp:Button ID="btnBuscarAuto" runat="server" Text=" BUSCAR " CssClass="button"
                                    OnClick="btnBuscarAuto_Click"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left; width:25%">
                                <asp:Label ID="lblTextoBusqueda" runat="server" Text="Texto a buscar:" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="text-align:left; width:45%">
                                <asp:DropDownList ID="ddlMarcas" runat="server" AutoPostBack="false" Width="97%" CssClass="listInput" Visible="false"></asp:DropDownList>
                                <asp:TextBox ID="txtTextoBusqueda" runat="server" Width="95%" CssClass="inputCampo" Visible="false"></asp:TextBox>
                            </td>
                            <td width="20%">
                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height:10px; text-align:right">
                                <asp:RequiredFieldValidator ID="rfvTextoBusqueda" runat="server" ControlToValidate="txtTextoBusqueda"
                                    Display="Dynamic" ErrorMessage="El campo es requerido" ForeColor="Red" ValidationGroup="VBusquedaAuto"></asp:RequiredFieldValidator>
                            </td>
                            <td width="20%">
                                
                            </td>
                        </tr>
                    </table>
                    <center>
                        <table width="99%">
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlBusquedaAuto" runat="server" Width="98%" Height="200px" ScrollBars="Auto">
                                        <fieldset style="text-align:left">
                                            <legend>
                                                <span>
                                                    Resultados...
                                                </span>
                                            </legend>
                                            <asp:GridView ID="gvAutos" runat="server" AutoGenerateColumns="false" Width="99%" Font-Size="10px" 
                                                    OnRowEditing="gvAutos_RowEditing" PageSize="10" BorderStyle="None" BorderWidth="0px" 
                                                    HeaderStyle-BackColor="#646464" HeaderStyle-ForeColor="white" AllowSorting="True">
                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="fc_TipoAuto" HeaderText="Auto"/>
                                                    <asp:BoundField DataField="fc_Marca" HeaderText="Marca"  />
                                                    <asp:BoundField DataField="fc_Version" HeaderText="Versión"/>
                                                    <asp:BoundField DataField="fm_Precio" HeaderText="Precio" DataFormatString="{0:c}"/>
                                                    <asp:BoundField DataField="fc_Placa" HeaderText="Placa"  />
                                                    <asp:BoundField DataField="fc_Color" HeaderText="Color"/>
                                                    <asp:BoundField DataField="fc_Sucursal" HeaderText="Sucursal"  />
                                                    <asp:CommandField EditText="Seleccionar" ShowEditButton="true" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No se encontraron registros
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </fieldset>                                    
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="height:5px">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center">
                                    <asp:Button ID="btnCerrarBusquedaAuto" runat="server" Text=" CERRAR " CssClass="button" OnClientClick="OcultaModalBusquedaAuto();" />
                                </td>
                            </tr>
                        </table>
                    </center>
                </center>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBuscarAuto" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="upgBusquedaAuto" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="upaBuscaAuto">
            <ProgressTemplate>
                <div style="text-align:left">
                    <asp:Label ID="lblProgresBusquedaAuto" runat="server" Text="Por favor espere..."></asp:Label>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>


    <%--Modal de Pagos individuales--%>
    <asp:HiddenField ID="hdTarget" runat="server" />
    <cc1:ModalPopupExtender ID="mpeAgregarPago" runat="server" TargetControlID="hdTarget" 
        PopupControlID="Panel1" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BackColor="White" Height="190px"
        Width="350px" HorizontalAlign="Center" Style="display: none">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td colspan="2" style="text-align:center">
                            <asp:Label ID="lblCaption" runat="server" Text="Pagos Individuales" CssClass="labelTitleModal"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <center>
                    <table width="90%">
                        <tr>
                            <td style="text-align: left; width:35%">
                                <asp:Label ID="lblImportePago" runat="server" Text="Importe:" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="text-align: left; width:55%">
                                <asp:TextBox ID="txtImportePago" runat="server" Width="96%" CssClass="inputCampo"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvImportePago" runat="server" ErrorMessage="El campo es requerido" Display="Dynamic"
                                    ForeColor="Red" ValidationGroup="PagoIndividual" ControlToValidate="txtImportePago"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width:35%">
                                <asp:Label ID="lblFechaPago" runat="server" Text="Fecha del pago:" CssClass="inputLabel"></asp:Label>                            
                            </td>
                            <td style="text-align: left; width:55%">
                                <asp:TextBox ID="txtFechaPago" runat="server" Width="75%" CssClass="inputCampo"></asp:TextBox>
                                <asp:ImageButton ID="imbFechaPago" runat="server" ImageUrl="~/Images/Botones/Calendar.ico" Width="24px" Height="24px" />
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
                            <td style="text-align: right">
                                <asp:Button ID="btnAgregarPagoModal" runat="server" Text=" AGREGAR " CssClass="button" OnClick="btnAgregarPagoModal_Click" />
                            </td>
                            <td style="text-align: left">
                                <asp:Button ID="btnCancelarPagoModal" runat="server" Text=" CANCELAR " CssClass="button" OnClick="btnCancelarPagoModal_Click" />
                            </td>
                        </tr>
                    </table>
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>


    <%--Modal de Cambio de tasa--%>
    <asp:HiddenField ID="hdTargetCambioTasa" runat="server" />
    <cc1:ModalPopupExtender ID="mpeCambioTasa" runat="server" TargetControlID="hdTargetCambioTasa" 
        PopupControlID="pnlCambioTasa" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlCambioTasa" runat="server" BorderColor="Black" BackColor="White" Height="140px"
        Width="330px" HorizontalAlign="Center" Style="display: none">
        <asp:UpdatePanel ID="upaCambioTasa" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td colspan="2" style="text-align:center">
                            <asp:Label ID="lblTituloCambioTasa" runat="server" Text="Cambio de tasa de interes" CssClass="labelTitleModal"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <center>
                    <table width="90%">
                        <tr>
                            <td style="text-align:left; width:60%">
                                <asp:Label ID="lblTasaInteres" runat="server" Text="Interes Mensual (%):" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="text-align:left; width:40%">
                                <asp:TextBox ID="txtTasaInteres" runat="server" CssClass="inputCampo" Width="80%"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ftbTasaInteres" runat="server" FilterMode="ValidChars" TargetControlID="txtTasaInteres"
                                    ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height:10px; text-align:right">
                                <asp:RequiredFieldValidator ID="rfvTasaInteres" runat="server" ControlToValidate="txtTasaInteres"
                                    Display="Dynamic" ErrorMessage="El campo es requerido" ForeColor="Red" ValidationGroup="VCambioTasa"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center">
                                <asp:Button ID="btnCambiarTasa" runat="server" Text=" CAMBIAR " CssClass="button" OnClick="btnCambiarTasa_Click" />                            
                                <asp:Button ID="btnCancelarCambioTasa" runat="server" Text=" CANCELAR " CssClass="button" OnClientClick="OcultarModal();" />
                            </td>
                        </tr>
                    </table>
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <%--Modal Busqueda de cliente--%>
    
    <asp:HiddenField ID="hdTargetBusCliente" runat="server" />
    <cc1:ModalPopupExtender ID="mpeBusquedaCliente" runat="server" TargetControlID="hdTargetBusCliente" 
        PopupControlID="pnlBusquedaCliente" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlBusquedaCliente" runat="server" BorderColor="Black" BackColor="#eeeeee" Height="400px"
        Width="820px" HorizontalAlign="Center" Style="display: none; border-radius:25px;">
        <asp:UpdatePanel ID="upaBusquedaCliente" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td colspan="2" style="text-align:center">
                            <h3><asp:Label ID="lblTituloBusCliente" runat="server" Text="Búsqueda de cliente" CssClass="modal-title"></asp:Label></h3>
                        </td>
                    </tr>
                </table>
                <br />
                <center>
                    <table style="width:100%; text-align:center">
                        <tr>
                            <td>
                                <asp:Label ID="lblBusCliNombre" runat="server" Text="Nombre:" CssClass="inputLabel"></asp:Label> 
                            </td>
                            <td valign="middle">                         
                                <asp:TextBox ID="txtBusCliNombre" runat="server" Width="" CssClass="form-control" AutoPostBack="false"></asp:TextBox>
                            </td>
                            <td valign="middle">
                                <asp:Button ID="btnBuscarBusCliente" runat="server" Text="BUSCAR" CssClass="btn btn-primary" OnClick="btnBuscarBusCliente_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <fieldset><br />
                                    <h4>
                                        <span>
                                            Resultados...
                                        </span>
                                    </h4>
                                        <asp:Panel ID="pnlBusCliente" runat="server" ScrollBars="Auto" Height="150px" CssClass="form-control">
                                        <asp:GridView ID="gvBusClientes" runat="server" AutoGenerateColumns="false" Width="99%" Font-Size="10px" 
                                                    OnSelectedIndexChanged="gvBusClientes_SelectedIndexChanged"
                                                    PageSize="10" BorderStyle="None" BorderWidth="0px" 
                                                    HeaderStyle-BackColor="#646464" HeaderStyle-ForeColor="white" AllowSorting="True" DataKeyNames="fi_Id" CssClass="table table-hover" style="border:1px solid #efefef;">
                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#448aff" CssClass="titleHeader" />
                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                                                <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="fc_Nombre" HeaderText="Nombre"/>
                                                <asp:BoundField DataField="fc_Nombre2" HeaderText="Seg. Nombre"/>
                                                <asp:BoundField DataField="fc_ApePaterno" HeaderText="Ape. Paterno"/>
                                                <asp:BoundField DataField="fc_ApeMaterno" HeaderText="Ape. Materno"/>
                                                <asp:BoundField DataField="fc_RFC" HeaderText="RFC"  />
                                                <asp:BoundField DataField="fc_CURP" HeaderText="CURP"/>
                                                <asp:BoundField DataField="fd_FechaNacimiento" HeaderText="Fecha Nacimiento"/>
                                                <asp:CommandField ButtonType="Link" SelectText="Seleccionar" ShowSelectButton="true" />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                No se encontraron registros
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </asp:Panel>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <br />
                                <asp:Button ID="btnCancelarBusCliente" runat="server" Text=" CANCELAR " CssClass="btn btn-primary" OnClientClick="OcultaBusquedaCliente();" />
                            </td>
                        </tr>
                    </table>
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

</asp:Content>

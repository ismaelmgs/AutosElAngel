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
            <div class="card" style="min-height:70vh;">
                <div style="width:70%; margin:0 auto;">
                <div class="row">
                    <div class="col-md-2">
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="inputLabel"></asp:Label>
                    </div>
                    <div class="col-md-4">
                            <div class="row">
                            <div class="col-md-10" style="text-align:right; padding-left:2.5%;">
                                <asp:TextBox ID="txtNombre" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                            ControlToValidate="txtNombre" Display="Dynamic" 
                            ErrorMessage="El nombre es requerido" ForeColor="Red" 
                            ValidationGroup="VCotizar"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-1" style="text-align:left;">
                                <asp:ImageButton ID="imbBuscaCliente" runat="server" ImageUrl="~/Images/Botones/Find.ico" OnClick="imbBuscaCliente_Click"
                            style="vertical-align:middle" ToolTip="Selecciona un cliente existente"/>
                            </div>
                            <div class="col-md-1" style="text-align:left;">
                            <asp:Label ID="lblReqNombre" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblSegNombre" runat="server" Text="Segundo nombre:" CssClass="inputLabel"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtSegNombre" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblApePaterno" runat="server" Text="Apellido paterno:" CssClass="inputLabel"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-11" style="text-align:right; padding-left:2.5%;">
                                <asp:TextBox ID="txtApePaterno" runat="server" Width="" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvApePaterno" runat="server" 
                                ControlToValidate="txtApePaterno" Display="Dynamic" 
                                ErrorMessage="El apellido es requerido" ForeColor="Red" 
                                ValidationGroup="VCotizar"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lblReqApePaterno" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            
                                </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblApeMaterno" runat="server" Text="Apellido materno:" 
                            CssClass="inputLabel"  ></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtApeMaterno" runat="server" Width="" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblSeleccionaAuto" runat="server" Text="Selecciona auto:" 
                            CssClass="inputLabel"  ></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-10" style="text-align:right; padding-left:2.5%;">
                                <asp:TextBox ID="txtAuto" runat="server" Width="" CssClass="form-control" Enabled="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvAuto" runat="server" 
                                ControlToValidate="txtAuto" Display="Dynamic" 
                                ErrorMessage="El auto es requerido" ForeColor="Red" ValidationGroup="VCotizar"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-1" style="text-align:left;">
                                <asp:ImageButton ID="imbBuscarAuto" runat="server" ImageUrl="~/Images/Botones/Find.ico" OnClick="imbBuscarAuto_Click"
                            style="vertical-align:middle" ToolTip="Selecciona un auto existente"/>
                            </div>
                            <div class="col-md-1" style="text-align:left;">
                                <asp:Label ID="lblReqAuto" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPlazo" runat="server" Text="Plazo:" CssClass="inputLabel" ></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-11" style="text-align:right; padding-left:0%;">
                            <asp:DropDownList ID="ddlPlazo" runat="server" Width="100%" CssClass="form-control">
                        </asp:DropDownList>   
                                <asp:RequiredFieldValidator ID="rfvPlazo" runat="server" 
                            ControlToValidate="ddlPlazo" Display="Dynamic" 
                            ErrorMessage="El plazo es requerido" ForeColor="Red" InitialValue="0" 
                            ValidationGroup="VCotizar"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-1" style="text-align:left;">
                             <asp:Label ID="lblReqPlazo" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        
                            </div>
                        </div>
                        
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPecio" runat="server" Text="Precio:" CssClass="inputLabel"  ></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-11" style="text-align:right; padding-left:2%;">
                                <asp:TextBox ID="txtPrecio" runat="server" Width="100%" CssClass="form-control" Enabled="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" ControlToValidate="txtPrecio" Display="Dynamic" 
                            ErrorMessage="El precio es requerido" ForeColor="Red" ValidationGroup="VCotizar"></asp:RequiredFieldValidator>
                        <cc1:FilteredTextBoxExtender ID="ftbPrecio" runat="server" FilterMode="ValidChars" FilterType="Numbers"
                            ValidChars="0123456789" TargetControlID="txtPrecio"></cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1" style="text-align:left;">
                            <asp:Label ID="lblReqPrecio" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEnganche" runat="server" Text="Enganche:" CssClass="inputLabel"  ></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-11" style="text-align:left; padding-left:0%;">
                            <asp:TextBox ID="txtEnganche" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEnganche" runat="server" 
                            ControlToValidate="txtEnganche" Display="Dynamic" 
                            ErrorMessage="El enganche es requerido" ForeColor="Red" 
                            ValidationGroup="VCotizar"></asp:RequiredFieldValidator>
                        <cc1:FilteredTextBoxExtender ID="rfbEngancge" runat="server" FilterMode="ValidChars" FilterType="Numbers"
                            ValidChars="0123456789" TargetControlID="txtEnganche"></cc1:FilteredTextBoxExtender> 
                            </div>
                            <div class="col-md-1" style="text-align:left;">
                            <asp:Label ID="lblReqEnganche" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblSucursal" runat="server" Text="Sucursal cotización:" CssClass="inputLabel"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-11" style="text-align:right; padding-left:2.5%;">
                                <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="form-control" Width="100%"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSucursal" runat="server" 
                                ControlToValidate="ddlSucursal" Display="Dynamic" InitialValue="0"
                                ErrorMessage="La sucursal es requerida" ForeColor="Red" 
                                ValidationGroup="VCotizar"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-1" style="text-align:left;">
                                <asp:Label ID="lblReqSucursal" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCorreoElectronico" runat="server" CssClass="inputLabel" 
                              Text="E-mail:"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtCorreoElectronico" runat="server" CssClass="form-control" Width=""></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCorreoElectronico" Display="Dynamic" 
                            ErrorMessage="El E-mail es requerido" ForeColor="Red" ValidationGroup="VCotizar"></asp:RequiredFieldValidator>
                    </div>
                </div>
                </div>
                
                <br />
                        <asp:updatepanel id="updCotizar" runat="server" updatemode="Conditional">
                            <ContentTemplate>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvCotizar" runat="server" 
                                        AllowPaging="false" AutoGenerateColumns="false"
                                        OnRowDataBound="GridCotizar_RowDataBound"
                                        PageSize="10" BorderStyle="None" 
                                        BorderWidth="0px" HeaderStyle-BackColor="#646464"
                                        HeaderStyle-ForeColor="white" AllowSorting="True"
                                        Width="100%" DataKeyNames="Plazo" CssClass="table table-hover">
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
                                                            <div id="div<%# Eval("Plazo") %>" style="display:none;position:relative;"  >
                                                                <asp:GridView ID="GrdCotizaDetalle" runat="server" Width="100%"
                                                                    AutoGenerateColumns="false" DataKeyNames="Plazo"
                                                                    OnRowDataBound="GrdCotizaDetalle_RowDataBound" ShowFooter = "true" 
                                                                    EmptyDataText="No hay resultados para esta busqueda." PageSize="10" 
                                                                    BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" HeaderStyle-ForeColor="white" AllowSorting="True" CssClass="table table-bordered table-hover" HorizontalAlign="Center">
                                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#01609F" CssClass="titleHeader" HorizontalAlign="Center" />
                                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="center" CssClass="" />
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="No. Pago" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblNoPAgo" Text='<%# Bind("NoPago") %>' runat="server" Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Pago normal" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPagoNormal" Text='<%# Bind("PagoNormal") %>' runat="server" Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Pago adelantado" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPagoAdelantado" Text='<%# Bind("PagoAdelantado") %>' runat="server" Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Pago en Mora" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPagoMora" Text='<%# Bind("PagoMora") %>' runat="server" Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <%--<asp:TemplateField HeaderText="Plazo" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPlazoDet" Text='<%# Bind("Plazo") %>' runat="server" Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>

                                                                        <%--<asp:BoundField DataField="NoPago" HeaderText="No. Pago"/>--%>
                                                                        <%--<asp:BoundField DataField="PagoNormal" HeaderText="Pago normal"/>
                                                                        <asp:BoundField DataField="PagoAdelantado" HeaderText="Pago adelantado"/>
                                                                        <asp:BoundField DataField="PagoMora" HeaderText="Pago en Mora"/>--%>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </itemtemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PlazoS" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPlazoS" Text='<%# Bind("Plazo") %>' runat="server" Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings Mode="NumericFirstLast" />
                                    </asp:GridView>
                                </div>
                                
                                <br />                        
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnGenerar" EventName="Click" />
                            </Triggers>
                        </asp:updatepanel>
                        <div style="text-align:center; width:100%;">
                            <asp:Label ID="lblInformacion" runat="server" Text="(Los campos marcados con * son obligatorios)" ForeColor="Red" CssClass="EtiquetaInformativa"></asp:Label><br /><br />
                        </div>
            <table class="table" style="width:90%;margin:0 auto;">
                
                <tr>
                    <td style="text-align:left">
                        <asp:UpdatePanel ID="upaAgregarPagos" runat="server">
                            <ContentTemplate>
                                <fieldset id="fPagos" runat="server" visible="false" style="text-align:left">
                                    <div style="text-align:center;">
                                        <h4>
                                            Opciones de cotización
                                        </h4>
                                    </div>
                            
                                    <asp:Panel ID="pnlAgregarPagos" runat="server"  Visible="false">
                                        <div style="text-align:center; width:100%;">
                                            
                                            <asp:Label ID="lblSinIntereses" runat="server" Text="Meses sin Intereses"></asp:Label>
                                            <asp:CheckBox ID="chkSinIntereses" runat="server" />
                                            
                                            <br />
                                            <br />

                                            <asp:Label ID="lblAgregarPago" runat="server" Text="¿Deseas agregar pagos individuales?"></asp:Label>
                                            <asp:Button ID="btnAgregarPago" runat="server" Text="  SI  " CssClass="btn btn-success"
                                                onclick="btnAgregarPago_Click" />

                                            <br />
                                            <div style="text-align:center; width:100%; margin:0 auto;">
                                                <asp:GridView ID="gvPagosIndividuales" runat="server" AutoGenerateColumns="false" Width="400px" 
                                                    Font-Size="14px" OnRowDeleting="gvPagosIndividuales_RowDeleting"
                                                    PageSize="10" BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" HeaderStyle-ForeColor="white" AllowSorting="True" CssClass="table table table-hover" HorizontalAlign="Center">
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="center" CssClass=""/>
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
                                            </div>
                                            <br />

                                            <asp:Label ID="lblCambioTasa" runat="server" Text="¿Cambiar la tasa de Interes a una preferencial?"></asp:Label>
                                            <asp:Button ID="btnCambioTasa" runat="server" Text="  SI  " OnClick="btnCambioTasa_Click" CssClass="btn btn-success" />
                                            <br />
                                            <asp:Panel ID="pnlTasaPreferencial" runat="server" Visible="false">
                                                <table class="table table-hover" style="margin:0 auto; width:450px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTasaPreferencial" runat="server" Text="Nueva tasa de interes:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTasaPreferencial" runat="server" Enabled="false"></asp:TextBox>
                                                            <asp:ImageButton ID="imbTasaPreferencial" runat="server" ImageUrl="~/Images/Iconos/Erase.ico" Width="16px" Height="16px" OnClick="imbTasaPreferencial_Click" />
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
                </tr>
                <tr>
                    <td style="text-align:center">
                        <br />
                        <div>
                            <asp:Button ID="btnGenerar" runat="server" Text=" GENERAR " OnClick="btnGenerar_Click"
                            CssClass="btn btn-success" Font-Size="" />
                        &nbsp;<asp:Button ID="btnImprimir" runat="server" Text=" IMPRIMIR " OnClick="btnImprimir_Click"
                            CssClass="btn btn-secondary" Font-Size="" />
                        &nbsp;<asp:Button ID="btnGuardar" runat="server" Text=" GUARDAR " 
                            CssClass="btn btn-primary" Font-Size="" OnClick="btnGuardar_Click" />
                        </div>
                        
                    </td>
                </tr>
            </table>
            </div>
<!-- RLR Modal inicia-->
            <%--<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModalCenter">
              Abrir Modal
            </button>--%>

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

<asp:Panel ID="pnlBuscaAuto" runat="server" BorderColor="Black" BackColor="#e4e4e4" Height="450px"
        Width="820px" HorizontalAlign="Center" Style="display: none; border-radius:25px; box-shadow:3px 3px 3px #00000030;">
        <asp:UpdatePanel ID="upaBuscaAuto" runat="server">

            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td colspan="2" style="text-align:center"><br />
                            <h3><asp:Label ID="lblTituloBuscaAuto" runat="server" Text="Búsqueda de autos" CssClass="labelTitleModal"></asp:Label></h3>
                        </td>
                    </tr>
                </table>
                <br />
                <center>
                    <table width="99%">
                        <tr>
                            <td style="text-align:center; width:25%">
                                <asp:Label ID="lblBusquedaPor" runat="server" Text="Búsqueda por:" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="text-align:left; width:45%">
                                <asp:DropDownList ID="ddlTipoBusqueda" runat="server" Width="97%" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlTipoBusqueda_SelectedIndexChanged">
                                    <asp:ListItem Text="Seleccione" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Placa" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Marca" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Modelo" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Color" Value="4"></asp:ListItem>      
                                </asp:DropDownList>
                            </td>
                            <td width="20%" style="text-align:left">
                                <asp:Button ID="btnBuscarAuto" runat="server" Text=" BUSCAR " CssClass="btn btn-success"
                                    OnClick="btnBuscarAuto_Click"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center; width:25%">
                                <asp:Label ID="lblTextoBusqueda" runat="server" Text="" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="text-align:left; width:45%">
                                <asp:DropDownList ID="ddlMarcas" runat="server" AutoPostBack="false" Width="97%" CssClass="form-control" Visible="false"></asp:DropDownList>
                                <asp:TextBox ID="txtTextoBusqueda" runat="server" Width="97%" CssClass="form-control" Visible="false"></asp:TextBox>
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
                                <td style="text-align:center;">
                                    <h4>
                                        <span>
                                            Resultados...
                                        </span>
                                    </h4>
                                    <asp:Panel ID="pnlBusquedaAuto" runat="server" Width="98%" Height="200px" ScrollBars="Auto" CssClass="form-control">
                                        <fieldset style="text-align:center">
                                            
                                            <asp:GridView ID="gvAutos" runat="server" AutoGenerateColumns="false" Width="100%" Font-Size="10px" 
                                                    OnRowEditing="gvAutos_RowEditing" PageSize="10" BorderStyle="None" BorderWidth="0px" 
                                                    HeaderStyle-BackColor="#646464" HeaderStyle-ForeColor="white" AllowSorting="True" CssClass="table table-hover">
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
                                    <asp:Button ID="btnCerrarBusquedaAuto" runat="server" Text=" CERRAR " CssClass="btn btn-danger" OnClientClick="OcultaModalBusquedaAuto();" />
                                </td>
                            </tr>
                        </table>
                    </center><br />
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
    <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BackColor="#efefef" Height="300px"
        Width="820px" HorizontalAlign="Center" Style="display: none; border-radius:25px; box-shadow:3px 3px 3px #00000030;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td colspan="2" style="text-align:center"><br />
                            <h3><asp:Label ID="lblCaption" runat="server" Text="Pagos Individuales" CssClass="labelTitleModal"></asp:Label></h3>
                        </td>
                    </tr>
                </table>
                <br />
                <center>
                    <table width="80%" style="margin:0 auto;">
                        <tr>
                            <td style="text-align: left; width:35%">
                                <asp:Label ID="lblImportePago" runat="server" Text="Importe:" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="text-align: left; width:55%">
                                <asp:TextBox ID="txtImportePago" runat="server" Width="96%" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvImportePago" runat="server" ErrorMessage="El campo es requerido" Display="Dynamic"
                                    ForeColor="Red" ValidationGroup="PagoIndividual" ControlToValidate="txtImportePago"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width:35%">
                                <asp:Label ID="lblFechaPago" runat="server" Text="Fecha del pago:" CssClass="inputLabel"></asp:Label>                            
                            </td>
                            <td style="text-align: left; width:55%">
                                <div class="form-row">
                                    <div class="col-md-10">
                                        <asp:TextBox ID="txtFechaPago" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
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
                                    </div>
                                    <div class="col-md-2">
                                        <asp:ImageButton ID="imbFechaPago" runat="server" ImageUrl="~/Images/Botones/Calendar.ico" Width="24px" Height="24px" style="margin-bottom:-15px;margin-left:5px;" />
                                    </div>
                                </div>
                                
                                
                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height:10px">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center"><br />
                                <asp:Button ID="btnAgregarPagoModal" runat="server" Text=" AGREGAR " CssClass="btn btn-success" OnClick="btnAgregarPagoModal_Click" />
                                <asp:Button ID="btnCancelarPagoModal" runat="server" Text=" CANCELAR " CssClass="btn btn-danger" OnClick="btnCancelarPagoModal_Click" />
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
    <asp:Panel ID="pnlCambioTasa" runat="server" BorderColor="Black" BackColor="#e4e4e4" Height="250px"
        Width="820px" HorizontalAlign="Center" Style="display: none; border-radius:25px; box-shadow:3px 3px 3px #00000030;">
        <asp:UpdatePanel ID="upaCambioTasa" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td colspan="2" style="text-align:center"><br />
                            <h3><asp:Label ID="lblTituloCambioTasa" runat="server" Text="Cambio de tasa de interes" CssClass="labelTitleModal"></asp:Label></h3>
                        </td>
                    </tr>
                </table>
                <br />
                <center>
                    <table style="width:400px; margin:0 auto;">
                        <tr>
                            <td style="text-align:center; width:40%">
                                <asp:Label ID="lblTasaInteres" runat="server" Text="Interes Mensual (%):&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="text-align:left; width:60%">
                                <asp:TextBox ID="txtTasaInteres" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
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
                            <td colspan="2" style="text-align:center"><br />
                                <asp:Button ID="btnCambiarTasa" runat="server" Text=" CAMBIAR " CssClass="btn btn-success" OnClick="btnCambiarTasa_Click" />                            
                                <asp:Button ID="btnCancelarCambioTasa" runat="server" Text=" CANCELAR " CssClass="btn btn-danger" OnClientClick="OcultarModal();" />
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
    <asp:Panel ID="pnlBusquedaCliente" runat="server" BackColor="#e4e4e4" Height="420px"
        Width="820px" HorizontalAlign="Center" Style="display: none; border-radius:25px; box-shadow:3px 3px 3px #00000030;" CssClass="">
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
                                <asp:Button ID="btnBuscarBusCliente" runat="server" Text="BUSCAR" CssClass="btn btn-success" OnClick="btnBuscarBusCliente_Click" />
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
                                        <asp:GridView ID="gvBusClientes" runat="server" AutoGenerateColumns="false" Width="" Font-Size="10px" 
                                                    OnSelectedIndexChanged="gvBusClientes_SelectedIndexChanged"
                                                    PageSize="10" BorderStyle="None" BorderWidth="0px" 
                                                    HeaderStyle-BackColor="#646464" HeaderStyle-ForeColor="white" AllowSorting="True" DataKeyNames="fi_Id" CssClass="table table-hover table-responsive" style="border:1px solid #efefef;">
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
                                <asp:Button ID="btnCancelarBusCliente" runat="server" Text=" CANCELAR " CssClass="btn btn-danger" OnClientClick="OcultaBusquedaCliente();" />
                            </td>
                        </tr>
                    </table>
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

</asp:Content>

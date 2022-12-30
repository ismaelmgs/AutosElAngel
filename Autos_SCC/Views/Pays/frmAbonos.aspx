<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAbonos.aspx.cs" EnableEventValidation="false" UICulture="es" Culture="es-MX" Inherits="Autos_SCC.Views.Pays.frmAbonos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../ControlesUsuario/ucModalAlert.ascx" TagName="ucModalAlert" TagPrefix="uc1" %>
<%@ Register Src="../ControlesUsuario/ucModalConfirm.ascx" TagName="ucModalConfirm" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../jquery/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../jquery/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="../../jquery/jquery-ui-1.9.2.custom.min.js"></script>
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

        function MostrarMensaje(mensaje, titulo) {
            var ventana = $('<div id="errortitulo" title="' + titulo + '"><span id="errormensaje">' + mensaje + '</span></div>');

            ventana.dialog({
                modal: true,
                buttons: { "Aceptar": function () { $(this).dialog("close"); } },
                show: "fold",
                hide: "scale",
            });
        }
    </script>
    <asp:UpdatePanel ID="upaCotizacion" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HidIdCliente" runat="server" Value="0" />
            <asp:HiddenField ID="HidIdCP" runat="server" Value="0" />
            <asp:HiddenField ID="HidIdCPAval" runat="server" Value="0" />
            <div class="card">
                <div class="card-block" style="text-align: center;">
                    <h3>
                        <asp:Label ID="lblTituloPantalla" runat="server" Text="Módulo de Abonos" CssClass="labelTitle"></asp:Label></h3>
                </div>
            </div>
            <div class="card" style="min-height: 65vh;">
                <fieldset style="text-align: left">
                    <div style="width: 100%; text-align: center;">
                        <h4>Búsqueda cliente
                        </h4>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-3">
                            &nbsp;
                        </div>
                        <div class="col-md-2" style="text-align: right; padding-top: 12px;">
                            <asp:Label ID="lblOpcion" runat="server" Text="Opción de Búsqueda:"
                                CssClass="inputLabel"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlOpcion" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlOpcion_SelectedIndexChanged" Width="97%">
                                <asp:ListItem Text="Seleccione" Value="0"></asp:ListItem>
                                <asp:ListItem Text="No. Cotización" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Por Sucursal" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            &nbsp;
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            &nbsp;
                        </div>
                        <div class="col-md-2" style="text-align: right; padding-top: 16px;">
                            <asp:Label ID="lblNoCotizacion" runat="server" CssClass="inputLabel" Text="No. Cotización" Visible="false"></asp:Label>
                            <asp:Label ID="lblSucursal" runat="server" CssClass="inputLabel" Text="Sucursal:" Visible="false"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <div class="row">
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtNoCotizacion" runat="server" CssClass="form-control" AutoPostBack="true"
                                        Visible="false" Width="100%" Style="margin-left: -1px;"></asp:TextBox>
                                </div>
                                <div class="col-md-2" style="text-align: center;">
                                    <asp:ImageButton ID="imbBuscaCliente" runat="server" ImageUrl="~/Images/Botones/Find.ico" OnClick="imbBuscaCliente_Click"
                                        Style="vertical-align: middle; margin-top: 12px;" ToolTip="Selecciona un auto existente" Visible="false" />
                                </div>
                            </div>
                            <asp:DropDownList ID="ddlSucursal" runat="server" AutoPostBack="true"
                                CssClass="form-control" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged"
                                Width="97%" Visible="false">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            &nbsp;
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            &nbsp;
                        </div>
                        <div class="col-md-2" style="text-align: right; padding-top: 12px;">
                            <asp:Label ID="lblCotizacion" runat="server" Text="Nombre Cliente:" CssClass="inputLabel" Visible="false"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlCotizacion" runat="server" Width="97%" CssClass="form-control" Visible="false"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlCotizacion_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            &nbsp;
                        </div>
                    </div>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 40%">
                                <asp:Label ID="lblCliente" runat="server" Text="" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="lblTipoAuto" runat="server" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="lblPrecio" runat="server" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="lblPlazo" runat="server" CssClass="inputLabel"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <div style="width: 100%; text-align: center;" runat="server" visible="false" id="divResultado">
                    <h5>Resultados...
                    </h5>
                </div>
                <br />
                <center>
                        <div class="table-responsive">
                        <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="false" Width="100%" 
                            Font-Size="14px" PageSize="10" BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" 
                            HeaderStyle-ForeColor="white" AllowSorting="True"
                            DataKeyNames="fi_IdCotizacion, fb_TrajeMedida" OnRowDataBound="gvClientes_RowDataBound" 
                            onrowcommand="gvClientes_RowCommand" CssClass="table table-hover">
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <itemtemplate>
                                        <a href="javascript:switchViews('div<%# Eval("fi_IdCotizacion") %>', 'one');">
                                            <img id="imgdiv<%# Eval("fi_IdCotizacion") %>" alt="Click para visualizar los movimientos del crédito" border="0" src="../../Images/Botones/flecha_abre1.png" />
                                        </a>
                                    </itemtemplate>
                                    <alternatingitemtemplate>
                                        <a href="javascript:switchViews('div<%# Eval("fi_IdCotizacion") %>', 'alt');">
                                            <img id="imgdiv<%# Eval("fi_IdCotizacion") %>" alt="Click para visualizar los movimientos del crédito" border="0" src="../../Images/Botones/flecha_abre1.png" />
                                        </a>
                                    </alternatingitemtemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Cliente"  />
                                <asp:BoundField DataField="fc_TipoAuto" HeaderText="Tipo de auto" />
                                <asp:BoundField DataField="fc_Plazo" HeaderText="Plazo" />
                                <asp:BoundField DataField="TotalCredito" HeaderText="Total del crédito" DataFormatString="{0:c}" />
                                <asp:BoundField DataField="TotalPagado" HeaderText="Total pagado" DataFormatString="{0:c}" />
                                <asp:BoundField DataField="DeudaAlDia" HeaderText="Deuda al día" DataFormatString="{0:c}" />
                                <asp:BoundField DataField="MontoCompromiso" HeaderText="Monto Compromiso" DataFormatString="{0:c}" />
                                <%--<asp:BoundField DataField="fb_TrajeMedida" HeaderText="Traje Medida" />--%>
                                <asp:TemplateField HeaderText="Acciones" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAbonar" runat="server" CommandName="Abonar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Abonar" CssClass="btn btn-success btn-mini waves-effect waves-light" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <itemtemplate>
                                        <tr>
                                            <td colspan="100%" align="left">
                                                <div id="div<%# Eval("fi_IdCotizacion") %>" style="display:none;position:relative;"  >
                                                    <div class="table">
                                                    <asp:GridView ID="gvDetalle" runat="server" Width="100%"
                                                        AutoGenerateColumns="false" ShowFooter = "true" OnRowDataBound="gvDetalle_RowDataBound" 
                                                        EmptyDataText="No hay resultados para esta busqueda." PageSize="10" 
                                                        BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" HeaderStyle-ForeColor="white" AllowSorting="True"  CssClass="table table-hover">
                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Codigo" HeaderText="Operación"/>
                                                            <asp:BoundField DataField="fc_Monto" HeaderText="Importe" DataFormatString="{0:c}"/>
                                                            <asp:BoundField DataField="fc_UsuarioCreacion" HeaderText="Usuario registró" ItemStyle-HorizontalAlign="Center"/>
                                                            <asp:BoundField DataField="fd_FechaCreacion" HeaderText="Fecha de registro"/>
                                                        </Columns>
                                                    </asp:GridView>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </itemtemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron registros
                            </EmptyDataTemplate>
                        </asp:GridView>
                        </div>
                        <br />
                        <br />
                        <div style="text-align:center; width:100%;" runat="server" id="divPagosI" visible="false">
                            <h5><asp:Label ID="lblTituloPagosInd" runat="server" Text="Pagos Individuales" CssClass="labelSubTitle"></asp:Label></h5>
                        </div>
                        <div class="table-responsive">
                        <asp:GridView ID="gvPagosInd" runat="server" AutoGenerateColumns="false" Width="100%" 
                            Font-Size="14px" PageSize="10" BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" OnRowDataBound="gvPagosInd_RowDataBound"
                            HeaderStyle-ForeColor="white" AllowSorting="True" DataKeyNames="fi_IdAMortizacion,fi_IdCotizacion" onrowcommand="gvPagosInd_RowCommand" CssClass="table table-hover">
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Cliente"  />
                                <asp:BoundField DataField="fc_TipoAuto" HeaderText="Tipo de auto" />
                                <asp:BoundField DataField="fm_MontoCompromiso" HeaderText="Monto Compromiso" DataFormatString="{0:c}" />
                                <asp:BoundField DataField="fd_FechaCompromiso" HeaderText="Fecha Compromiso" />
                                <asp:BoundField DataField="fm_MontoPagado" HeaderText="Monto Pagado" DataFormatString="{0:c}" />
                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Image ID="imStatus" runat="server" Width="16" Height="16" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAbonarInd" runat="server" CommandName="Abonar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Abonar" CssClass="btn btn-success btn-mini waves-effect waves-light" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron registros
                            </EmptyDataTemplate>
                        </asp:GridView>
                        </div>
                    </center>
                <uc1:ucModalConfirm ID="omb" runat="server" />
                <uc1:ucModalAlert runat="server" ID="omb2" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlSucursal" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>


    <%--Modal para hacer pagos--%>
    <asp:HiddenField ID="hdTargetPago" runat="server" />
    <cc1:ModalPopupExtender ID="mpePagos" runat="server" TargetControlID="hdTargetPago"
        PopupControlID="pnlPagos" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPagos" runat="server" Width="100%" Height="100%" Style="background-color: #00000070; display: none; margin-left: -6px; padding-top: 10%;">
        <asp:UpdatePanel ID="upaPagos" runat="server" BorderColor="Black" BackColor=""
            HorizontalAlign="Center" Style="border-radius: 25px; box-shadow: 3px 3px 3px #00000050; background-color: #eeeeee; width: 50%; margin: 0 auto;">
            <ContentTemplate>
                <table width="80%" style="margin: 0 auto;">
                    <tr>
                        <td style="width: 150"></td>
                        <td style="width: 180"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <h4>
                                <asp:Label ID="lblTituloBuscaAuto" runat="server" Text="Cargar Abono" CssClass="labelTitleModal"></asp:Label></h4>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                            <asp:Label ID="lblTextDebe" runat="server" Text="Debe:" CssClass="inputLabel"></asp:Label>
                        </td>
                        <td style="height: 5px">
                            <asp:Label ID="lblDebe" runat="server" Text="" CssClass="inputLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <br />
                            <asp:Label ID="lblTipoMov" runat="server" Text="Tipo movimiento:" CssClass="inputLabel"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlTipoMov" runat="server" CssClass="form-control" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoMov_SelectedIndexChanged">
                                <asp:ListItem Text="PAGO" Value="PAY"></asp:ListItem>
                                <asp:ListItem Text="REVERSO" Value="RPAY"></asp:ListItem>
                                <asp:ListItem Text="PAGO ANTICIPADO" Value="APAY"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="lblImporte" runat="server" Text="Importe:" CssClass="inputLabel"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtImporte" runat="server" CssClass="form-control" Width="100%" MaxLength="15"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftbImporte" runat="server" ValidChars="-.0123456789"
                                TargetControlID="txtImporte" FilterMode="ValidChars">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td style="text-align: right; width: 165">
                            <asp:Button ID="btnAceptarPago" runat="server" Text="Aceptar" OnClick="btnAceptarPago_Click" CssClass="btn btn-success" />
                        </td>
                        <td style="text-align: left; width: 165">
                            <asp:Button ID="btnCancelarPago" runat="server" Text="Cerrar" OnClick="btnCancelarPago_Click" CssClass="btn btn-danger" />
                        </td>
                    </tr>
                </table>
                <br />
                <br />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlTipoMov" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="upgPagos" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="upaPagos">
            <ProgressTemplate>
                <div style="text-align: left">
                    <asp:Label ID="lblProgresBusquedaAuto" runat="server" Text="Por favor espere..."></asp:Label>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>

</asp:Content>

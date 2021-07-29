<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAbonos.aspx.cs" UICulture="es" Culture="es-MX" Inherits="Autos_SCC.Views.Pays.frmAbonos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <asp:UpdatePanel ID="upaCotizacion" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HidIdCliente" runat="server" Value="0" />
            <asp:HiddenField ID="HidIdCP" runat="server" Value="0" />
            <asp:HiddenField ID="HidIdCPAval" runat="server" Value="0" />
            <br />
            <table width="100%">
                <tr>
                    <td style="text-align:center; width:100%">
                        <asp:Label ID="lblTituloPantalla" runat="server" Text="Módulo de Abonos" CssClass="labelTitle"></asp:Label>
                    </td>
                </tr>
            </table>
            <fieldset style="text-align:left">
                <legend>
                    <span>
                        Búsqueda cliente
                    </span>
                </legend>
                    
                    <table style="width:100%">
                        <tr>
                            <td style="width:20%">
                            </td>
                            <td style="width:20%">
                                <asp:Label ID="lblOpcion" runat="server" Text="Opción de Búsqueda:" 
                                    CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:DropDownList ID="ddlOpcion" runat="server" AutoPostBack="true" CssClass="listInput" OnSelectedIndexChanged="ddlOpcion_SelectedIndexChanged" Width="97%">
                                <asp:ListItem Text="Seleccione" Value="0"></asp:ListItem>
                                <asp:ListItem Text="No. Cotización" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Por Sucursal" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width:20%">
                            </td>
                            <td style="width:20%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width:20%">
                            </td>
                            <td style="width:20%">
                                <asp:Label ID="lblNoCotizacion" runat="server" CssClass="inputLabel" Text="No. Cotización" Visible="false"></asp:Label>
                                <asp:Label ID="lblSucursal" runat="server" CssClass="inputLabel" Text="Sucursal:" Visible="false"></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:TextBox ID="txtNoCotizacion" runat="server" CssClass="inputCampo" AutoPostBack="true"
                                    Visible="false" Width="75%"></asp:TextBox>
                                <asp:ImageButton ID="imbBuscaCliente" runat="server" ImageUrl="~/Images/Botones/Find.ico" OnClick="imbBuscaCliente_Click"
                                    style="vertical-align:middle" ToolTip="Selecciona un auto existente" Visible="false"/>
                                <asp:DropDownList ID="ddlSucursal" runat="server" AutoPostBack="true" 
                                    CssClass="listInput" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged" 
                                    Width="97%" Visible="false">
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
                                <asp:Label ID="lblCotizacion" runat="server" Text="Cotización:" CssClass="inputLabel" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCotizacion" runat="server" Width="97%" CssClass="listInput" Visible="false"
                                     AutoPostBack="true" OnSelectedIndexChanged="ddlCotizacion_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
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
            </fieldset>

            <fieldset style="text-align:left">
                <legend>
                    <span>
                        Resultados...
                    </span>
                </legend>
                    <center>
                        <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="false" Width="80%" 
                            Font-Size="10px" PageSize="10" BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" 
                            HeaderStyle-ForeColor="white" AllowSorting="True" 
                            DataKeyNames="fi_IdCotizacion" OnRowDataBound="gvClientes_RowDataBound" 
                            onrowcommand="gvClientes_RowCommand">
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
                                <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAbonar" runat="server" CommandName="Abonar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Abonar" CssClass="button" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <itemtemplate>
                                        <tr>
                                            <td colspan="100%" align="left">
                                                <div id="div<%# Eval("fi_IdCotizacion") %>" style="display:none;position:relative;left:100px;"  >
                                                    <asp:GridView ID="gvDetalle" runat="server" Width="80%"
                                                        AutoGenerateColumns="false" ShowFooter = "true" OnRowDataBound="gvDetalle_RowDataBound" 
                                                        EmptyDataText="No hay resultados para esta busqueda." PageSize="10" 
                                                        BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" HeaderStyle-ForeColor="white" AllowSorting="True">
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
                                            </td>
                                        </tr>
                                    </itemtemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron registros
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <br />
                        <br />
                        <div style="text-align:left; width:80%">
                            <asp:Label ID="lblTituloPagosInd" runat="server" Text="Pagos Individuales" CssClass="labelSubTitle"></asp:Label>
                        </div>
                        <asp:GridView ID="gvPagosInd" runat="server" AutoGenerateColumns="false" Width="80%" 
                            Font-Size="10px" PageSize="10" BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" OnRowDataBound="gvPagosInd_RowDataBound"
                            HeaderStyle-ForeColor="white" AllowSorting="True" DataKeyNames="fi_IdAMortizacion,fi_IdCotizacion" onrowcommand="gvPagosInd_RowCommand">
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
                                        <asp:Button ID="btnAbonarInd" runat="server" CommandName="Abonar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Abonar" CssClass="button" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron registros
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </center>
            </fieldset>

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
    <asp:Panel ID="pnlPagos" runat="server" BorderColor="Black" BackColor="White" Height="180px"
        Width="330px" HorizontalAlign="Center" Style="display: none">
        <asp:UpdatePanel ID="upaPagos" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td style="width:150">
                        </td>
                        <td style="width:180">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:center">
                            <asp:Label ID="lblTituloBuscaAuto" runat="server" Text="Búsqueda de autos" CssClass="labelTitleModal"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height:5px">
                        </td>
                        <td style="height:5px">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left">
                            <asp:Label ID="lblTipoMov" runat="server" Text="Tipo movimiento:" CssClass="inputLabel"></asp:Label>
                        </td>
                        <td style="text-align:left">
                            <asp:DropDownList ID="ddlTipoMov" runat="server" CssClass="listInput" Width="173">
                                <asp:ListItem Text="PAGO" Value="PAY"></asp:ListItem>
                                <asp:ListItem Text="REVERSO" Value="RPAY"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left">
                            <asp:Label ID="lblImporte" runat="server" Text="Importe:" CssClass="inputLabel"></asp:Label>
                        </td>
                        <td style="text-align:left">
                            <asp:TextBox ID="txtImporte" runat="server" CssClass="inputCampo" Width="160" MaxLength="15"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftbImporte" runat="server" ValidChars="-.0123456789"
                                TargetControlID="txtImporte" FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td style="text-align:right; width:165">
                            <asp:Button ID="btnAceptarPago" runat="server" Text="Aceptar" OnClick="btnAceptarPago_Click" CssClass="button" />
                        </td>
                        <td style="text-align:left; width:165">
                            <asp:Button ID="btnCancelarPago" runat="server" Text="Cancelar" OnClick="btnCancelarPago_Click" CssClass="button" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="upgPagos" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="upaPagos">
            <ProgressTemplate>
                <div style="text-align:left">
                    <asp:Label ID="lblProgresBusquedaAuto" runat="server" Text="Por favor espere..."></asp:Label>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>

</asp:Content>

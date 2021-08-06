<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="es" Culture="es-MX" CodeBehind="frmAutos.aspx.cs" EnableEventValidation="false" Inherits="Autos_SCC.Views.Catalogos.frmAutos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../ControlesUsuario/ucModalConfirm.ascx" TagName="ucModalConfirm" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="../../jquery/jquery-ui-1.9.2.custom.min.css" type="text/css" />
    <script type="text/javascript" src="../../jquery/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="../../jquery/jquery-ui-1.9.2.custom.min.js"></script>
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
        
        function On(GridView) {
            if (GridView != null) {
                GridView.originalBgColor = GridView.style.backgroundColor;
                GridView.style.backgroundColor = "#C0BCBC";
            }
        }

        function Off(GridView) {
            if (GridView != null) {
                GridView.style.backgroundColor = GridView.originalBgColor;
            }
        }

    </script>
    <asp:UpdatePanel ID="upaTab" runat="server">
        <ContentTemplate>
            <center>
                <table width="99%">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lblTitulo" runat="server" CssClass="labelTitle" Text="Catálogo de Autos"></asp:Label>
                        </td>
                        <td>
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" rowspan="2">
                            <cc1:TabContainer ID="TabContainer1" Width="290px" Height="500px" runat="server"
                                ActiveTabIndex="0">
                                <cc1:TabPanel runat="server" HeaderText="Registro de autos" ID="TabPanel1">
                                    <ContentTemplate>
                                        <br />
                                        <table width="100%">
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblId" runat="server" Text="Id:" CssClass="inputLabel" />
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:TextBox ID="txtId" runat="server" Width="80%" class="inputCampo" 
                                                        Enabled="False" CssClass="inputCampo"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblMarca" runat="server" Text="Marca:" CssClass="inputLabel"></asp:Label>
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:DropDownList ID="ddlMarca" runat="server" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged"
                                                        AutoPostBack="True" CssClass="listInput" Width="88%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblTipoAuto" runat="server" Text="Tipo:" CssClass="inputLabel"></asp:Label>
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:DropDownList ID="ddlTipoAuto" runat="server" CssClass="listInput" Width="88%"
                                                        AutoPostBack="True" 
                                                        OnSelectedIndexChanged="ddlTipoAuto_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblVersion" runat="server" Text="Version:" CssClass="inputLabel"></asp:Label>
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:DropDownList ID="ddlVersion" runat="server" CssClass="listInput" Width="88%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblPlaca" runat="server" Text="Placa:" CssClass="inputLabel"></asp:Label>
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:TextBox ID="txtPlaca" runat="server" CssClass="inputCampo" Width="80%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblNoSerie" runat="server" Text="No. Serie:" CssClass="inputLabel"></asp:Label>
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:TextBox ID="txtNoSerie" runat="server" CssClass="inputCampo" Width="80%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblModelo" runat="server" Text="Modelo:" CssClass="inputLabel"></asp:Label>
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:TextBox ID="txtModelo" runat="server" MaxLength="4" CssClass="inputCampo" Width="80%"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ftbModelo" runat="server" ValidChars="0123456789" FilterType="Numbers"
                                                        FilterMode="ValidChars" TargetControlID="txtModelo"></cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblColor" runat="server" Text="Color:" CssClass="inputLabel"></asp:Label>
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:TextBox ID="txtColor" runat="server" CssClass="inputCampo" Width="80%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblSucursal" runat="server" Text="Sucursal:" CssClass="inputLabel"></asp:Label>
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="listInput" Width="88%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblEstatus" runat="server" Text="Estatus:" CssClass="inputLabel" />
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:DropDownList ID="ddlEstatus" runat="server" CssClass="listInput" Width="88%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblPrecio" runat="server" Text="Precio:" CssClass="inputLabel" />
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:TextBox ID="txtPrecio" runat="server" Width="80%" CssClass="inputCampo"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Búsqueda">
                                    <ContentTemplate>
                                        <br />
                                        <table width="99%">
                                            <tr>
                                                <td width="40%">
                                                    <asp:Label ID="lblBusqueda" runat="server" Text="Palabra a buscar:" CssClass="inputLabel" />
                                                </td>
                                                <td width="60%">
                                                    <asp:TextBox ID="txtBuqueda" runat="server" Width="80%" CssClass="inputCampo"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSucursalBus" runat="server" Text="Sucursal:" CssClass="inputLabel"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSucursalBus" runat="server" Width="87%" CssClass="listInput"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblStatusBus" runat="server" Text="Estatus:" CssClass="inputLabel"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEstatusBus" runat="server" Width="87%" CssClass="listInput"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnBuscar" runat="server" Text="BUSCAR" CssClass="button" OnClick="btnBuscar_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </td>
                        <td width="80%">
                            <div class="DivBotones">
                                <asp:Button ID="btnNuevo" runat="server" Text="NUEVO" CssClass="button" OnClick="btnNuevo_Click"
                                    ToolTip="Prepara los campos para un registro nuevo" />
                                &nbsp;<asp:Button ID="btnGuardar" runat="server" Text="GUARDAR" CssClass="button"
                                    OnClick="btnGuardar_Click" ToolTip="Guarda los cambios realizados sobre el registro" />
                                &nbsp;<asp:Button ID="btnEliminar" runat="server" Text="ELIMINAR" CssClass="button"
                                    OnClick="btnEliminar_Click" ToolTip="Elimina el registro seleccionado" />
                                &nbsp;<asp:Button ID="btnLimpiar" runat="server" Text="LIMPIAR" CssClass="button"
                                    OnClick="btnLimpiar_Click" ToolTip="Limpia los campos para un registro nuevo" />
                                &nbsp;<asp:Button ID="btnExportar" runat="server" Text="EXPORTAR" CssClass="button"
                                    OnClick="btnExportar_Click" ToolTip="Exporta un grid a excel" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="80%">
                            <asp:Panel ID="pnlCatalogo" runat="server" ScrollBars="Horizontal" Height="515px"
                                Width="600px">
                                <asp:GridView ID="gvCatalogo" runat="server" AutoGenerateColumns="false" DataKeyNames="fi_Id"
                                    OnRowCommand="gvCatalogo_RowCommand" Width="1100px" OnRowDataBound="gvCatalogo_RowDataBound" 
                                    OnSelectedIndexChanged="gvCatalogo_SelectedIndexChanged" PageSize="10" Font-Size="Small"
                                    BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464"
                                    HeaderStyle-ForeColor="white" AllowSorting="True">
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div id="divPrueba" runat="server">
                                                <asp:ImageButton ID="imbAgregarGasto" runat="server" ImageUrl="~/Images/Botones/icon_agregar.ico"
                                                    CommandName="AgregarGasto" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Agregar gastos al automovil" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="fi_Id" HeaderText="Id" />
                                        <asp:BoundField DataField="fc_Marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="fc_Version" HeaderText="Versión" />
                                        <asp:BoundField DataField="fc_TipoAuto" HeaderText="Tipo" />
                                        <asp:BoundField DataField="fm_Precio" HeaderText="Precio" DataFormatString="{0:c}" />
                                        <asp:BoundField DataField="fc_Placa" HeaderText="Placa" />
                                        <asp:BoundField DataField="fi_Modelo" HeaderText="Modelo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="fc_NoSerie" HeaderText="No. Serie" />
                                        <asp:BoundField DataField="fc_Sucursal" HeaderText="Sucursal" />
                                        <asp:BoundField DataField="fc_Color" HeaderText="Color" />
                                        <asp:BoundField DataField="fc_Status" HeaderText="Estatus" />
                                        <asp:BoundField DataField="fc_Usuario" HeaderText="Usuario modifico" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="fd_FechaUltMovimiento" HeaderText="Fecha Ult. Movimiento" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                            <div style="display:none">
                                <asp:GridView ID="gvCopia" runat="server" AutoGenerateColumns="false"
                                    Width="1000px" PageSize="10" Font-Size="Small"
                                    BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464"
                                    HeaderStyle-ForeColor="white" AllowSorting="True">
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="fi_Id" HeaderText="Id" />
                                        <asp:BoundField DataField="fc_Marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="fc_Version" HeaderText="Versión" />
                                        <asp:BoundField DataField="fc_TipoAuto" HeaderText="Tipo" />
                                        <asp:BoundField DataField="fm_Precio" HeaderText="Precio"/>
                                        <asp:BoundField DataField="fc_Placa" HeaderText="Placa" />
                                        <asp:BoundField DataField="fi_Modelo" HeaderText="Modelo" />
                                        <asp:BoundField DataField="fc_NoSerie" HeaderText="No. Serie" />
                                        <asp:BoundField DataField="fc_Sucursal" HeaderText="Sucursal" />
                                        <asp:BoundField DataField="fc_Color" HeaderText="Color" />
                                        <asp:BoundField DataField="fc_Usuario" HeaderText="Usuario modifico" />
                                        <asp:BoundField DataField="fd_FechaUltMovimiento" HeaderText="Fecha Ult. Movimiento" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <uc1:ucModalConfirm ID="omb" runat="server" />
                        </td>
                    </tr>
                </table>
            </center>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportar" />
        </Triggers>
    </asp:UpdatePanel>

    <%--Modal de Gastos--%>
    <asp:HiddenField ID="hdTarget" runat="server" />
    <cc1:ModalPopupExtender ID="mpeAgregarGasto" runat="server" EnableViewState="true"
        TargetControlID="hdTarget" PopupControlID="Panel1" BackgroundCssClass="overlayy"
        CancelControlID="can">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BackColor="White" Height="300px"
        Width="400px" HorizontalAlign="Center" Style="display: none">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="header">
                    <table width="100%">
                        <tr>
                            <td colspan="2" style="text-align:center">
                                <asp:Label ID="lblCaption" runat="server" Text="Gastos del vehiculo" CssClass="TituloEtiquetas"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <center>
                    <table width="90%">
                        <tr>
                            <td style="text-align:left; width:35%">
                                <asp:Label ID="lblDescripcionGasto" runat="server" Text="Descripción:"   CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="text-align:left; width:55%">
                                <asp:TextBox ID="txtDescripcionGasto" runat="server" Width="95%" CssClass="inputCampo"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                <asp:Label ID="lblMonto" runat="server" Text="Monto:"  ></asp:Label>
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtMonto" runat="server" Width="50%" CssClass="inputCampo"></asp:TextBox>
                                <asp:Button ID="btnGuardarGasto" runat="server" Text=" GUARDAR " OnClick="btnGuardarGasto_Click"
                                    CssClass="button" />
                            </td>
                        </tr>
                    </table>
                    <table width="90%">
                        <tr>
                            <td colspan="2" style="text-align:center">
                                <asp:Panel ID="pnlGastos" runat="server" Width="375px" Height="120px" ScrollBars="Vertical">
                                    <asp:GridView ID="gvGastos" runat="server" AutoGenerateColumns="false" 
                                        Width="100%" OnRowDataBound="gvGastos_RowDataBound" ShowFooter="true"
                                        OnRowDeleting="gvGastos_RowDeleting" PageSize="10" BorderStyle="None" 
                                        BorderWidth="0px" HeaderStyle-BackColor="#646464"
                                        HeaderStyle-ForeColor="white" AllowSorting="True">
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="sDescripcion" HeaderText="Descripción" />
                                            <asp:BoundField DataField="iMonto" HeaderText="Monto" DataFormatString="{0:c}" />
                                            <asp:CommandField ButtonType="Image" ShowDeleteButton="true" DeleteImageUrl="~/Images/Iconos/Erase.ico" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No se encontraron registros
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 15px">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center">
                                <asp:Button ID="can" runat="server" Text=" CERRAR " CssClass="button" OnClick="can_Click" />
                            </td>
                        </tr>
                    </table>
                </center>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnGuardarGasto" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

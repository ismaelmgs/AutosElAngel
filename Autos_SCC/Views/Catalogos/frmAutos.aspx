<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAutos.aspx.cs" EnableEventValidation="false" Inherits="Autos_SCC.Views.Catalogos.frmAutos" %>
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
                            <asp:Label ID="lblTitulo" runat="server" CssClass="TituloEtiquetas" Text="Catálogo de Autos"></asp:Label>
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
                                <cc1:TabPanel runat="server" HeaderText="Registro de marcas" ID="TabPanel1">
                                    <ContentTemplate>
                                        <br />
                                        <table width="100%">
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblId" runat="server" Text="Id:" CssClass="EtiquetaSimple" />
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:TextBox ID="txtId" runat="server" Width="95%" class="CajaSimple" 
                                                        Enabled="False" CssClass="CajaSimple"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblMarca" runat="server" Text="Marca:" CssClass="EtiquetaSimple"></asp:Label>
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:DropDownList ID="ddlMarca" runat="server" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged"
                                                        AutoPostBack="True" CssClass="ComboSimple" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblTipoAuto" runat="server" Text="Tipo:" CssClass="EtiquetaSimple"></asp:Label>
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:DropDownList ID="ddlTipoAuto" runat="server" CssClass="ComboSimple" Width="96%"
                                                        AutoPostBack="True" 
                                                        OnSelectedIndexChanged="ddlTipoAuto_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblVersion" runat="server" Text="Version:" CssClass="EtiquetaSimple"></asp:Label>
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:DropDownList ID="ddlVersion" runat="server" CssClass="ComboSimple" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblPlaca" runat="server" Text="Placa:" CssClass="EtiquetaSimple"></asp:Label>
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:TextBox ID="txtPlaca" runat="server" CssClass="CajaSimple" Width="95%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblNoSerie" runat="server" Text="No. Serie:" CssClass="EtiquetaSimple"></asp:Label>
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:TextBox ID="txtNoSerie" runat="server" CssClass="CajaSimple" Width="95%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblColor" runat="server" Text="Color:" CssClass="EtiquetaSimple"></asp:Label>
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:TextBox ID="txtColor" runat="server" CssClass="CajaSimple" Width="95%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblSucursal" runat="server" Text="Sucursal:" CssClass="EtiquetaSimple"></asp:Label>
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="ComboSimple" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblEstatus" runat="server" Font-Bold="True" Text="Estatus:" 
                                                        CssClass="EtiquetaSimple" />&nbsp;
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:DropDownList ID="ddlEstatus" runat="server" CssClass="ComboSimple" Width="96%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSimpleIzquiera">
                                                    <asp:Label ID="lblPrecio" runat="server" Font-Bold="True" Text="Precio:" 
                                                        CssClass="EtiquetaSimple" />&nbsp;
                                                </td>
                                                <td class="TdSimpleDerecha">
                                                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="CajaSimple"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Búsqueda">
                                    <ContentTemplate>
                                        <br />
                                        <table width="100%">
                                            <tr>
                                                <td style="height: 30px;" align="right" width="35%">
                                                    <asp:Label ID="lblBusqueda" runat="server" Font-Bold="True" Text="Palabra a buscar:" />&nbsp;
                                                </td>
                                                <td width="65%">
                                                    <asp:TextBox ID="txtBuqueda" Width="95%" onkeyup="ValidaNum(this.id, this.value, this.id);"
                                                        runat="server" MaxLength="6" CssClass="CajaSimple"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </td>
                        <td width="80%">
                            <div class="DivBotones">
                                <asp:Button ID="btnNuevo" runat="server" Text=" NUEVO " CssClass="ButtonStyle" OnClick="btnNuevo_Click"
                                    ToolTip="Prepara los campos para un registro nuevo" />
                                &nbsp;<asp:Button ID="btnGuardar" runat="server" Text=" GUARDAR " CssClass="ButtonStyle"
                                    OnClick="btnGuardar_Click" ToolTip="Guarda los cambios realizados sobre el registro" />
                                &nbsp;<asp:Button ID="btnEliminar" runat="server" Text=" ELIMINAR " CssClass="ButtonStyle"
                                    OnClick="btnEliminar_Click" ToolTip="Elimina el registro seleccionado" />
                                &nbsp;<asp:Button ID="btnLimpiar" runat="server" Text=" LIMPIAR " CssClass="ButtonStyle"
                                    OnClick="btnLimpiar_Click" ToolTip="Limpia los campos para un registro nuevo" />
                                &nbsp;<asp:Button ID="btnExportar" runat="server" Text=" EXPORTAR " CssClass="ButtonStyle"
                                    OnClick="btnExportar_Click" ToolTip="Exporta un grid a excel" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="80%">
                            <asp:Panel ID="pnlCatalogo" runat="server" ScrollBars="Horizontal" Height="515px"
                                Width="600px">
                                <asp:GridView ID="gvCatalogo" runat="server" AutoGenerateColumns="false" DataKeyNames="fi_Id"
                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnRowCommand="gvCatalogo_RowCommand"
                                    Width="820px" OnRowDataBound="gvCatalogo_RowDataBound" OnSelectedIndexChanged="gvCatalogo_SelectedIndexChanged"
                                    Font-Size="10px">
                                    <Columns>
                                        <asp:BoundField DataField="fi_Id" HeaderText="Id" />
                                        <asp:BoundField DataField="fc_Marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="fc_Version" HeaderText="Modelo" />
                                        <asp:BoundField DataField="fc_TipoAuto" HeaderText="Tipo" />
                                        <asp:BoundField DataField="fc_Placa" HeaderText="Placa" />
                                        <asp:BoundField DataField="fc_NoSerie" HeaderText="No. Serie" />
                                        <asp:BoundField DataField="fc_Color" HeaderText="Color" />
                                        <asp:BoundField DataField="fc_Sucursal" HeaderText="Sucursal" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imbAgregarGasto" runat="server" ImageUrl="~/Images/Botones/icon_agregar.ico"
                                                    CommandName="AgregarGasto" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Agregar gastos al automovil" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="fc_Usuario" HeaderText="Usuario modifico" />
                                        <asp:BoundField DataField="fd_FechaUltMovimiento" HeaderText="Fecha Ult. Movimiento" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                            <uc1:ucModalConfirm ID="omb" runat="server" />
                        </td>
                    </tr>
                </table>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--Modal de Gastos--%>
    <asp:HiddenField ID="hdTarget" runat="server" />
    <cc1:ModalPopupExtender ID="mpeAgregarGasto" runat="server" EnableViewState="true"
        TargetControlID="hdTarget" PopupControlID="Panel1" BackgroundCssClass="modalBackground"
        CancelControlID="can">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BackColor="White" Height="300px"
        Width="400px" HorizontalAlign="Center" Style="display: none">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="header">
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblCaption" runat="server" Text="Gastos del vehiculo" CssClass="TituloEtiquetas"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <center>
                    <table width="90%">
                        <tr>
                            <td align="left" width="35%">
                                <asp:Label ID="lblDescripcionGasto" runat="server" Text="Descripción:" Font-Size="Small" CssClass="EtiquetaSimple"></asp:Label>
                            </td>
                            <td align="left" width="55%">
                                <asp:TextBox ID="txtDescripcionGasto" runat="server" Width="95%" CssClass="CajaSimple"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblMonto" runat="server" Text="Monto:" Font-Size="Small"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtMonto" runat="server" Width="50%" CssClass="CajaSimple"></asp:TextBox>
                                <asp:Button ID="btnGuardarGasto" runat="server" Text=" GUARDAR " OnClick="btnGuardarGasto_Click"
                                    CssClass="ButtonStyle" />
                            </td>
                        </tr>
                    </table>
                    <table width="90%">
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlGastos" runat="server" Width="375px" Height="120px" ScrollBars="Vertical">
                                    <asp:GridView ID="gvGastos" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                        DataKeyNames="iId" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        Width="100%" Font-Size="10px" OnRowDataBound="gvGastos_RowDataBound" ShowFooter="true"
                                        OnRowDeleting="gvGastos_RowDeleting">
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
                            <td colspan="2" align="center">
                                <asp:Button ID="can" runat="server" Text=" CERRAR " CssClass="ButtonStyle" OnClick="can_Click" />
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

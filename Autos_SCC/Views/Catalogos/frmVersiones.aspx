<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmVersiones.aspx.cs" EnableEventValidation="false" Inherits="Autos_SCC.Views.Catalogos.frmVersiones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../ControlesUsuario/ucModalConfirm.ascx" tagname="ucModalConfirm" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <title></title>
    <link href="../../Styles/EstilosGrid.css" rel="Stylesheet" type="text/css" />
    <link href="../../Styles/StyleGeneral.css" rel="Stylesheet" type="text/css" />
    <link href="../../Styles/EstilosModal.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="../../jquery/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="../../jquery/jquery-ui-1.9.2.custom.min.js"></script>
    <link rel="stylesheet" href="../../jquery/jquery-ui-1.9.2.custom.min.css" type="text/css" />

    <script language="javascript" type="text/javascript">
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

    <asp:UpdatePanel ID="upaTab" runat="server" >
        <ContentTemplate>
            <center>
                <table width="99%">
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="lblTitulo" runat="server" CssClass="TituloEtiquetas" Text="Catálogo de Versiones"></asp:Label>
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
                        <cc1:TabContainer ID="TabContainer1" Width="300px" Height="500px" runat="server" ActiveTabIndex="0">
                
                            <cc1:TabPanel runat="server" HeaderText="Registro de modelos" ID="TabPanel1">
                            <ContentTemplate>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td class="TdSimpleIzquiera">
                                            <asp:Label ID="lblId" runat="server" Font-Bold="True" Text="Id:" CssClass="EtiquetaSimple"/>&nbsp;
                                        </td>
                
                                        <td class="TdSimpleDerecha">
                                            <asp:TextBox ID="txtId" Width="95%" class="box" onkeyup="ValidaNum(this.id, this.value, this.id);"
                                                runat="server" MaxLength="6" Enabled="false" CssClass="CajaSimple"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMarca" runat="server" Font-Bold="True" Text="Descripción:" CssClass="EtiquetaSimple"/>&nbsp;
                                        </td>
                
                                        <td>
                                            <asp:DropDownList ID="ddlMarca" runat="server" CssClass="ComboSimple" AutoPostBack="true" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTipoAuto" runat="server" Font-Bold="True" Text="Descripción:" CssClass="EtiquetaSimple"/>&nbsp;
                                        </td>
                
                                        <td>
                                            <asp:DropDownList ID="ddlTipoAuto" runat="server" CssClass="ComboSimple"></asp:DropDownList>
                                        </td>
                                    </tr>
                
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNombre" runat="server" Font-Bold="True" Text="Descripción:" CssClass="EtiquetaSimple"/>&nbsp;
                                        </td>
                
                                        <td>
                                            <asp:TextBox ID="txtDescripcion" Width="95%" class="box" runat="server" Rows="4" TextMode="MultiLine" CssClass="CajaSimple"></asp:TextBox>
                                        </td>
                                    </tr>
                
                                    <tr>
                                        <td>
                                            <asp:Label ID="Activo" runat="server" Font-Bold="True" Text="¿Activo?" CssClass="EtiquetaSimple"/>&nbsp;
                                        </td>
                
                                        <td>
                                            <asp:CheckBox ID="chkActivo" runat="server" />
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
                                            <asp:Label ID="lblBusqueda" runat="server" Font-Bold="True" Text="Palabra a buscar:" CssClass="EtiquetaSimple"/>&nbsp;
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
                            <asp:Button ID="btnNuevo" runat="server" Text=" NUEVO " 
                                CssClass="ButtonStyle" OnClick="btnNuevo_Click" ToolTip="Prepara los campos para un registro nuevo" />
                            &nbsp;<asp:Button ID="btnGuardar" runat="server" Text=" GUARDAR " 
                                CssClass="ButtonStyle" onclick="btnGuardar_Click" ToolTip="Guarda los cambios realizados sobre el registro" />
                            &nbsp;<asp:Button ID="btnEliminar" runat="server" Text=" ELIMINAR " 
                                CssClass="ButtonStyle" onclick="btnEliminar_Click" ToolTip="Elimina el registro seleccionado" />
                            &nbsp;<asp:Button ID="btnLimpiar" runat="server" Text=" LIMPIAR " 
                                CssClass="ButtonStyle" onclick="btnLimpiar_Click" ToolTip="Limpia los campos para un registro nuevo" />
                            &nbsp;<asp:Button ID="btnExportar" runat="server" Text=" EXPORTAR " 
                                CssClass="ButtonStyle" onclick="btnExportar_Click" ToolTip="Exporta un grid a excel" />
                        </div>
                    </td>
                </tr>                                                                                                                                                                                                                                                                    <tr>
                    <td width="80%">
                        <asp:Panel ID="pnlCatalogo" runat="server" ScrollBars="Auto" Width="600px" Height="500px">
                            <asp:GridView ID="gvCatalogo" runat="server" AutoGenerateColumns="false" RowStyle-VerticalAlign="Top"
                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%"
                                OnRowDataBound="gvCatalogo_RowDataBound" OnSelectedIndexChanged="gvCatalogo_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="fi_Id" HeaderText="Id" />
                                    <asp:BoundField DataField="fc_Marca" HeaderText="Marca" />
                                    <asp:BoundField DataField="fc_TipoAuto" HeaderText="Tipo de auto" />
                                    <asp:BoundField DataField="fc_Descripcion" HeaderText="Descripción" />
                                    <asp:BoundField DataField="fi_Activo" HeaderText="¿Activo?" />
                                    <asp:BoundField DataField="fc_Usuario" HeaderText="Usuario modifico" />
                                    <asp:BoundField DataField="fd_FechaUltMovimiento" HeaderText="Fecha Ult. Movimiento" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
                <uc1:ucModalConfirm ID="omb" runat="server" />
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
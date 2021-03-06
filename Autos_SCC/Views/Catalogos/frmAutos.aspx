<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="es" Culture="es-MX" CodeBehind="frmAutos.aspx.cs" EnableEventValidation="false" Inherits="Autos_SCC.Views.Catalogos.frmAutos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../ControlesUsuario/ucModalConfirm.ascx" TagName="ucModalConfirm" TagPrefix="uc1" %>
<%@ Register Src="../ControlesUsuario/ucModalAlert.ascx"  TagName="ucModalAlert" TagPrefix="uc1"%>


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

    <asp:UpdatePanel ID="upaTab" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="card">
                <div class="card-block" style="text-align:center;">
                    <h3><asp:Label ID="lblTitulo" runat="server" CssClass="labelTitle" Text="Catálogo de Autos"></asp:Label></h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <h3 style="text-align:center;">Búsqueda</h3>
                           <ContentTemplate>
                                <br />
                                <table width="50%" style="margin:0 auto;">
                                    <tr>
                                        <td width="30%">
                                            <asp:Label ID="lblBusqueda" runat="server" Text="Palabra a buscar:" CssClass="inputLabel" />
                                        </td>
                                        <td width="70%">
                                            <asp:TextBox ID="txtBuqueda" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSucursalBus" runat="server" Text="Sucursal:" CssClass="inputLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSucursalBus" runat="server" Width="100%" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblStatusBus" runat="server" Text="Estatus:" CssClass="inputLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlEstatusBus" runat="server" Width="100%" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <br />
                                            <asp:Button ID="btnBuscar" runat="server" Text="BUSCAR" CssClass="btn btn-success" OnClick="btnBuscar_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate> <br /><br />
                    </div>
                </div>
            </div>
            <center>
                <div class="card" style="padding:20px;">
                   <div class="table-responsive" style="width:100%;">
                       <div class="DivBotones" style="padding-bottom:15px; text-align:right">
                                <asp:Button ID="btnNuevo" runat="server" Text="NUEVO" CssClass="btn btn-success" OnClick="btnNuevo_Click"
                                    ToolTip="Prepara los campos para un registro nuevo" />
                                &nbsp;<asp:Button ID="btnExportar" runat="server" Text="EXPORTAR" CssClass="btn btn-secondary"
                                    OnClick="btnExportar_Click" ToolTip="Exporta un grid a excel" />
                        </div>
                        <asp:Panel ID="pnlCatalogo" runat="server" ScrollBars="Horizontal" Height="515px"
                            Width="100%">
                            <asp:GridView ID="gvCatalogo" runat="server" AutoGenerateColumns="false" DataKeyNames="fi_Id"
                                OnRowCommand="gvCatalogo_RowCommand" Width="100%" OnRowDataBound="gvCatalogo_RowDataBound" 
                                OnSelectedIndexChanged="gvCatalogo_SelectedIndexChanged" PageSize="10" Font-Size="Small"
                                BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464"
                                HeaderStyle-ForeColor="white" AllowSorting="True" CssClass="table table-hover">
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
                                    <asp:BoundField DataField="fi_Id" HeaderText="Id" Visible="false"/>
                                    <asp:BoundField DataField="fc_Marca" HeaderText="Marca" />
                                    <asp:BoundField DataField="fc_TipoAuto" HeaderText="Tipo" />
                                    <asp:BoundField DataField="fc_Version" HeaderText="Versión" />
                                    <asp:BoundField DataField="fm_Precio" HeaderText="Precio" DataFormatString="{0:c}" />
                                    <asp:BoundField DataField="fc_Placa" HeaderText="Placa" />
                                    <asp:BoundField DataField="fi_Modelo" HeaderText="Modelo" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="fc_NoSerie" HeaderText="No. Serie" />
                                    <asp:BoundField DataField="fc_Sucursal" HeaderText="Sucursal" />
                                    <asp:BoundField DataField="fc_Color" HeaderText="Color" />
                                    <asp:BoundField DataField="fi_Kilometraje" HeaderText="Kilometraje" />
                                    <asp:BoundField DataField="fc_Status" HeaderText="Estatus" />
                                    <asp:BoundField DataField="fc_Usuario" HeaderText="Usuario modifico" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                                    <asp:BoundField DataField="fd_FechaUltMovimiento" HeaderText="Fecha Ult. Movimiento" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnEliminar" runat="server" Text="ELIMINAR" CssClass="btn btn-danger" ToolTip="Elimina el registro seleccionado" CommandName="EliminarAuto" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                            No se encontraron registros para mostrar...
                                </EmptyDataTemplate>
                            </asp:GridView>
                            </asp:Panel>
                            <div style="display:none">
                                <asp:GridView ID="gvCopia" runat="server" AutoGenerateColumns="false"
                                    Width="100%" PageSize="10" Font-Size="Small"
                                    BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464"
                                    HeaderStyle-ForeColor="white" AllowSorting="True" CssClass="table table-responsive">
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
                                        <asp:BoundField DataField="fi_Kilometraje" HeaderText="Kilometraje" />
                                        <asp:BoundField DataField="fc_Usuario" HeaderText="Usuario modifico" />
                                        <asp:BoundField DataField="fd_FechaUltMovimiento" HeaderText="Fecha Ult. Movimiento" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <uc1:ucModalConfirm ID="omb" runat="server" />
                            <uc1:ucModalAlert runat="server" ID="omb2" />
                    </div>
                </div>
            </center><br />
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
    
        <asp:Panel ID="Panel1" runat="server" Width="100%" Height="100%" Style="background-color:#00000070; display: none; margin-left:-6px; padding-top:10%;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" BorderColor="Black" BackColor=""
        HorizontalAlign="Center" Style="border-radius:25px; box-shadow:3px 3px 3px #00000050; background-color:#eeeeee; width:50%;margin:0 auto;">
            <ContentTemplate>
                <div class="header">
                    <table width="100%">
                        <tr>
                            <td colspan="2" style="text-align:center"><br />
                                <h3><asp:Label ID="lblCaption" runat="server" Text="Gastos del vehiculo" CssClass="TituloEtiquetas"></asp:Label></h3>
                            </td>
                        </tr>
                    </table>
                    <br />
                </div>
                <br />
                <center>
                    <table width="80%" style="margin:0 auto;">
                        <tr>
                            <td style="text-align:left; width:35%">
                                <asp:Label ID="lblDescripcionGasto" runat="server" Text="Descripción:"   CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="text-align:left; width:55%">
                                <asp:TextBox ID="txtDescripcionGasto" runat="server" Width="95%" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                <asp:Label ID="lblMonto" runat="server" Text="Monto:"  ></asp:Label>
                            </td>

                            <td style="text-align:left">
                                <asp:TextBox ID="txtMonto" runat="server" Width="50%" CssClass="form-control"></asp:TextBox>
                                
                            </td>
  
                            <td align="center">
                                    <asp:Button ID="btnGuardarGasto" runat="server" Text=" GUARDAR " OnClick="btnGuardarGasto_Click"
                                    CssClass="btn btn-success" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="80%" style="margin:0 auto;">
                        <tr>
                            <td colspan="2" style="text-align:center">
                                <asp:Panel ID="pnlGastos" runat="server" Width="100%" Height="120px" ScrollBars="Vertical" CssClass="form-control">
                                    <asp:GridView ID="gvGastos" runat="server" AutoGenerateColumns="false" 
                                        Width="100%" OnRowDataBound="gvGastos_RowDataBound" ShowFooter="true"
                                        OnRowDeleting="gvGastos_RowDeleting" PageSize="10" BorderStyle="None" 
                                        BorderWidth="0px" HeaderStyle-BackColor="#646464"
                                        HeaderStyle-ForeColor="white" AllowSorting="True" CssClass="table table-hover" style="border:1px solid #efefef;">
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
                                <asp:Button ID="can" runat="server" Text=" CERRAR " CssClass="btn btn-danger" OnClick="can_Click" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </center>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnGuardarGasto" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    
   <%--Modal de Agregar Vehiculo--%>
    <asp:HiddenField ID="hdAgregarVehiculo" runat="server" />
    <cc1:ModalPopupExtender ID="mpeAgregarVehiculo" runat="server"
        TargetControlID="hdAgregarVehiculo" PopupControlID="pnlAgregarVehiculo" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlAgregarVehiculo" runat="server" BorderColor="Black" BackColor="#eeeeee" Height="100%" 
        Width="100%" HorizontalAlign="Center"  Style="display: none; background-color:#00000073; margin-left:-6px;">
        <asp:UpdatePanel ID="UpaAgregarVehiculo" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12" style="padding-top:10%;">
                        <div class="card" style="width:70%; margin:0 auto;">
                        <h3 style="text-align:center;" id="ttlAuto" runat="server">Registro de Autos</h3>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblId" runat="server" Text="Id:" CssClass="inputLabel" />
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtId" runat="server" Width="100%" class="inputCampo" 
                                            Enabled="False" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblMarca" runat="server" Text="Marca:" CssClass="inputLabel"></asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlMarca" runat="server" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged"
                                            AutoPostBack="True" CssClass="form-control" Width="100%">
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblTipoAuto" runat="server" Text="Tipo:" CssClass="inputLabel"></asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlTipoAuto" runat="server" CssClass="form-control" Width="100%"
                                            AutoPostBack="True" 
                                            OnSelectedIndexChanged="ddlTipoAuto_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblVersion" runat="server" Text="Version:" CssClass="inputLabel"></asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlVersion" runat="server" CssClass="form-control" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblPlaca" runat="server" Text="Placa:" CssClass="inputLabel"></asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtPlaca" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblNoSerie" runat="server" Text="No. Serie:" CssClass="inputLabel"></asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtNoSerie" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblModelo" runat="server" Text="Modelo:" CssClass="inputLabel"></asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtModelo" runat="server" MaxLength="4" CssClass="form-control" Width="100%"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ftbModelo" runat="server" ValidChars="0123456789" FilterType="Numbers"
                                            FilterMode="ValidChars" TargetControlID="txtModelo"></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblColor" runat="server" Text="Color:" CssClass="inputLabel"></asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtColor" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblSucursal" runat="server" Text="Sucursal:" CssClass="inputLabel"></asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="form-control" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblEstatus" runat="server" Text="Estatus:" CssClass="inputLabel" />
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlEstatus" runat="server" CssClass="form-control" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblPrecio" runat="server" Text="Precio:" CssClass="inputLabel" />
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtPrecio" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblKilometraje" runat="server" Text="Kilometraje:" CssClass="inputLabel"></asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtKilometraje" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    &nbsp;<asp:Button ID="btnGuardar" runat="server" Text="GUARDAR" CssClass="btn btn-primary"
                        OnClick="btnGuardar_Click" ToolTip="Guarda los cambios realizados sobre el registro" />
                                    &nbsp;<asp:Button ID="btnLimpiar" runat="server" Text="LIMPIAR" CssClass="btn btn-info"
                        OnClick="btnLimpiar_Click" ToolTip="Limpia los campos para un registro nuevo" />
                                    &nbsp;<asp:Button ID="btnCancelar" runat="server" Text="CERRAR" CssClass="btn btn-danger"
                        OnClick="btnCancelar_Click" ToolTip="Limpia los campos para un registro nuevo" />
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlMarca" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlTipoAuto" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlVersion" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlSucursal" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlEstatus" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

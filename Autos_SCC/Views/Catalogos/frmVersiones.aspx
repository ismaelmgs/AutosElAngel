<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmVersiones.aspx.cs" EnableEventValidation="false" Inherits="Autos_SCC.Views.Catalogos.frmVersiones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../ControlesUsuario/ucModalConfirm.ascx" tagname="ucModalConfirm" tagprefix="uc1" %>
<%@ Register Src="../ControlesUsuario/ucModalAlert.ascx"  TagName="ucModalAlert" TagPrefix="uc1"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title></title>
<%--<link rel="stylesheet" href="../../jquery/jquery-ui-1.9.2.custom.min.css" type="text/css" />
    <script type="text/javascript" src="../../jquery/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="../../jquery/jquery-ui-1.9.2.custom.min.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        //function MostrarMensaje(mensaje, titulo)
        //{
        //    var ventana = $('<div id="errortitulo" title="' + titulo + '"><span id="errormensaje">' + mensaje + '</span></div>');

        //    ventana.dialog({
        //        modal: true, 
        //        buttons: { "Aceptar": function () { $(this).dialog("close"); } },
        //        show: "fold",
        //        hide: "scale",
        //    });
        //}

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
                    <h3><asp:Label ID="lblTitulo" runat="server" CssClass="labelTitle" Text="Catálogo de Versiones"></asp:Label></h3>
                </div>
            </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="card" style="min-height:220px;">
                            <h3 style="text-align:center;">Búsqueda</h3>
                            <ContentTemplate>  
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                    <table width="80%" style="margin:0 auto;">
                                        <tr>
                                            <td width="40%">
                                                <asp:Label ID="lblBusqueda" runat="server" Font-Bold="True" Text="Palabra a buscar:" CssClass="inputLabel"/>
                                            </td>
                
                                            <td width="60%">
                                                <asp:TextBox ID="txtBuqueda" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:RadioButtonList ID="rblActivo" runat="server" Width="100%" RepeatDirection="Horizontal" CssClass="form-control">
                                            <asp:ListItem Text="TODOS&nbsp;&nbsp;" Value="2" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="ACTIVOS&nbsp;&nbsp;" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="INACTIVOS&nbsp;&nbsp;" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="text-align:center;">
                                        <br /><br />
                                        <asp:Button ID="btnBuscar" runat="server" Text="BUSCAR" CssClass="btn btn-success" OnClick="btnBuscar_Click" />
                                    </div>
                                </div>         
                            </ContentTemplate>
                        </div>
                     </div>
                </div>

            <div class="card" style="padding:20px;">
                        <asp:Panel ID="pnlCatalogo" runat="server" ScrollBars="Auto" Width="" Height="500px">
                            <div class="table-responsive" style="overflow-x: scroll; width:100%;">
                                <div class="DivBotones" style="padding-bottom:15px; text-align:right">
                                    <asp:Button ID="btnNuevo" runat="server" Text="NUEVO" 
                                        CssClass="btn btn-success" OnClick="btnNuevo_Click" ToolTip="Prepara los campos para un registro nuevo" />
                                    &nbsp;<asp:Button ID="btnExportar" runat="server" Text="EXPORTAR" 
                                        CssClass="btn btn-secondary" onclick="btnExportar_Click" ToolTip="Exporta un grid a excel" />
                                </div>
                                <asp:GridView ID="gvCatalogo" runat="server" AutoGenerateColumns="false" RowStyle-VerticalAlign="Top"
                                    Width="100%" OnRowDataBound="gvCatalogo_RowDataBound" OnSelectedIndexChanged="gvCatalogo_SelectedIndexChanged" 
                                    BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" Font-Size="Small" OnRowCommand="gvCatalogo_RowCommand"
                                    HeaderStyle-ForeColor="white" AllowSorting="True" CssClass="table table-hover" style="background-color:#ffffff;">
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="fi_Id" HeaderText="Id" />
                                        <asp:BoundField DataField="fc_Marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="fc_TipoAuto" HeaderText="Tipo de auto" />
                                        <asp:BoundField DataField="fc_Descripcion" HeaderText="Versión" />
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Eval("fi_Activo").ToString() == "1" ? "Activo" : "Inactivo" %>'
                                                    runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="fc_Usuario" HeaderText="Usuario modifico" />
                                        <asp:BoundField DataField="fd_FechaUltMovimiento" HeaderText="Fecha Ult. Movimiento" />
                                        <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnEliminar" runat="server" Text="ELIMINAR" CssClass="btn btn-danger" ToolTip="Elimina el registro seleccionado" CommandName="EliminarVersion" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                            No se encontraron registros para mostrar...
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                            
                        </asp:Panel>
                <uc1:ucModalConfirm ID="omb" runat="server" />
                <uc1:ucModalAlert runat="server" ID="omb2" />
            </div>
            <br />  
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportar" />
        </Triggers>
    </asp:UpdatePanel>
    <%--Modal de Adición y edicion--%>
<asp:HiddenField ID="hdAgregarVersion" runat="server" />
    <cc1:ModalPopupExtender ID="mpeAgregarVersion" runat="server"
        TargetControlID="hdAgregarVersion" PopupControlID="pnlAgregarVersion" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlAgregarVersion" runat="server" Width="100%" Height="100%" Style="background-color:#00000070; display: none; margin-left:-6px; padding-top:10%;">
        <asp:UpdatePanel ID="UpaAgregarVersion" runat="server" UpdateMode="Conditional" BorderColor="Black" BackColor=""
        HorizontalAlign="Center"  Style="border-radius:25px; box-shadow:3px 3px 3px #00000050; background-color:#eeeeee; width:50%;margin:0 auto;">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        
                            <h3 style="text-align:center;" id="ttlRegVersion" runat="server">Registro de modelos</h3>
                            <br />
                            <div class="row" style="padding:10px;">
                                <table style="text-align:left; width:100%">
                                    <tr>
                                        <td>
                                    <div class="row">
                                        <div class="col-md-2" style="text-align:left;">
                                            <asp:Label ID="lblId" runat="server" Font-Bold="True" Text="Id:" CssClass="inputLabel"/>&nbsp;
                                        </div>
                
                                        <div class="col-md-10" style="text-align:left;">
                                            <asp:TextBox ID="txtId" runat="server" Width="100%" Enabled="False" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-2" style="text-align:left">
                                            <asp:Label ID="lblMarca" runat="server" Font-Bold="True" Text="Marca:" 
                                                CssClass="inputLabel"/>&nbsp;
                                        </div>
                
                                        <div class="col-md-4" style="text-align:left">
                                            <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control" Width="100%"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged"></asp:DropDownList>
                                        </div>

                                        <div class="col-md-2" style="text-align:left">
                                            <asp:Label ID="lblTipoAuto" runat="server" Font-Bold="True" 
                                                Text="Tipo:" CssClass="inputLabel"/>&nbsp;
                                        </div>
                
                                        <div class="col-md-4" style="text-align:left">
                                            <asp:DropDownList ID="ddlTipoAuto" runat="server"  Width="100%" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                
                                    <div class="row">
                                        <div class="col-md-2" style="text-align:left">
                                            <asp:Label ID="lblNombre" runat="server" Font-Bold="True" Text="Descripción:" CssClass="inputLabel"/>&nbsp;
                                        </div>
                
                                        <div class="col-md-10" style="text-align:left">
                                            <asp:TextBox ID="txtDescripcion" runat="server" Width="100%" Rows="1" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2" style="text-align:left">
                                            <asp:Label ID="Activo" runat="server" Font-Bold="True" Text="¿Activo?" CssClass="inputLabel"/>&nbsp;
                                        </div>
                                        <div class="col-md-10" style="text-align:left">
                                            &nbsp;<asp:CheckBox ID="chkActivo" runat="server" />
                                        </div>
                                    </div>
                                      </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12" style="text-align:center;">
                                    &nbsp;<asp:Button ID="btnGuardar" runat="server" Text="GUARDAR" 
                                    CssClass="btn btn-primary" onclick="btnGuardar_Click" ToolTip="Guarda los cambios realizados sobre el registro" />
                                     &nbsp;<asp:Button ID="btnLimpiar" runat="server" Text="LIMPIAR" 
                                    CssClass="btn btn-info" onclick="btnLimpiar_Click" ToolTip="Limpia los campos para un registro nuevo" />
                                    &nbsp;<asp:Button ID="btnCancelar" runat="server" Text="CERRAR" CssClass="btn btn-danger"
                                    OnClick="btnCancelar_Click" ToolTip="Limpia los campos para un registro nuevo" />
                                </div>
		                    </div>
                        <br />
		            </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlMarca" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlTipoAuto" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

</asp:Content>
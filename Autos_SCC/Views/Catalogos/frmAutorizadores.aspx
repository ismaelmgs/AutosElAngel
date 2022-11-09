<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="es" Culture="es-MX" CodeBehind="frmAutorizadores.aspx.cs" EnableEventValidation="false" Inherits="Autos_SCC.Views.Catalogos.frmAutorizadores" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../ControlesUsuario/ucModalConfirm.ascx" TagName="ucModalConfirm" TagPrefix="uc1" %>
<%@ Register Src="../ControlesUsuario/ucModalAlert.ascx" TagName="ucModalAlert" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../../jquery/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="../../jquery/jquery-ui-1.9.2.custom.min.js"></script>
    <link rel="stylesheet" href="../../jquery/jquery-ui-1.9.2.custom.min.css" type="text/css" />
    <style>
        .dataCell {
            font-size:9pt !important;
        }
        th {
            font-size:9pt !important;
        }
        .hiddenRow {
            display:none !important;
        }
    </style>
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
                <div class="card-block" style="text-align: center;">
                    <h3>
                        <asp:Label ID="lblTitulo" runat="server" CssClass="labelTitle" Text="Usuarios con permisos de Administrador"></asp:Label></h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <h3 style="text-align: center;">Búsqueda</h3>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <table width="60%" style="margin: 0 auto;">
                                    <tr>
                                        <td width="30%">
                                            <asp:Label ID="lblSucursal" runat="server" Font-Bold="True" Text="Sucursal:" CssClass="inputLabel" />&nbsp;
                                        </td>
                                        <td width="70%">
                                            <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="form-control" Width="100%"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-3">&nbsp;</div>
                        </div>
                        <div class="col-md-12">
                            <table width="60%" style="margin: 0 auto;">
                                <tr>
                                    <td width="30%">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Nombre(s) a buscar:" CssClass="inputLabel" />
                                    </td>
                                    <td width="70%">
                                        <asp:TextBox ID="txtBusqueda" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="row">
                            <div class="col-md-12" style="text-align: center;">
                                <asp:Button ID="btnBuscar" runat="server" Text="BUSCAR" CssClass="btn btn-success" OnClick="btnBuscar_Click" />
                            </div>
                        </div>
                        <br />
                        </con>
                    </div>
                </div>
            </div>
            <center>
                <div class="card" style="padding:20px;">
                <asp:Panel ID="pnlCatalogo" runat="server" ScrollBars="Auto" Width="" Height="500px">
                    <div class="table-responsive" style="overflow-x: scroll; width:100%;">
                    <asp:GridView ID="gvCatalogo" runat="server" AutoGenerateColumns="false" RowStyle-VerticalAlign="Middle" DataKeyNames="fi_Id,fi_IdSucursal"
                        Width="100%" BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" OnSelectedIndexChanged="gvCatalogo_SelectedIndexChanged"
                        OnRowDataBound="gvCatalogo_RowDataBound" OnRowCommand="gvCatalogo_RowCommand"
                        HeaderStyle-ForeColor="white" AllowSorting="True" CssClass="table table-hover" style="background-color:#ffffff;">
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="fc_PrimNombre" HeaderText="Nombre(s)"/>
                            <asp:BoundField DataField="fc_SegNombre" HeaderText="" />
                            <asp:BoundField DataField="fc_PrimApellido" HeaderText="Apellido Paterno" /> 
                            <asp:BoundField DataField="fc_SegApellido" HeaderText="Apellido Materno" />
                            <asp:BoundField DataField="fc_Descripcion" HeaderText="Perfil" />
                            <asp:BoundField DataField="fc_Descripcion_Sucursal" HeaderText="Sucursal" />
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
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hdEdicionPerfil" runat="server" />
    <cc1:ModalPopupExtender ID="mpeEdicionPerfil" runat="server"
        TargetControlID="hdEdicionPerfil" PopupControlID="pnlEdicionPerfil" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlEdicionPerfil" runat="server" BorderColor="Black" BackColor="#eeeeee" Height="100%" 
        Width="100%" HorizontalAlign="Center"  Style="display: none; background-color:#00000073; margin-left:-6px;">
        <asp:UpdatePanel ID="UpaEdicionPerfil" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12" style="padding-top:3%;">
                        <div class="card" style="width:70%; margin:0 auto; background-color:#eeeeee;">
                        <h3 style="text-align:center;" id="ttlPerfil" runat="server">Edición de Perfil</h3>
                            <br />
                            <div class="row">
                                <div class="col-md-10">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lbluser" runat="server" Text="Nombre Usuario:" CssClass="inputLabel" />
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtUser" runat="server" Width="100%" class="inputCampo" 
                                            Enabled="False" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblSucursalMod" runat="server" Text="Sucursal:" CssClass="inputLabel" />
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtSucursal" runat="server" Width="100%" class="inputCampo" 
                                            Enabled="False" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblPerfil" runat="server" Text="Perfil:" CssClass="inputLabel"></asp:Label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlPerfil" runat="server" OnSelectedIndexChanged="ddlPerfil_SelectedIndexChanged"
                                            AutoPostBack="True" CssClass="form-control" Width="100%">
                                        </asp:DropDownList>
                                        </div>
                                    </div>                                 
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    &nbsp;<asp:Button ID="btnGuardar" runat="server" Text="GUARDAR" CssClass="btn btn-primary"
                        OnClick="btnGuardar_Click" ToolTip="Guarda los cambios realizados sobre el registro" />
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
                <asp:AsyncPostBackTrigger ControlID="ddlPerfil" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

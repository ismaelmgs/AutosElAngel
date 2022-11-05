<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAutorizadores.aspx.cs" Inherits="Autos_SCC.Views.Catalogos.frmAutorizadores" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../ControlesUsuario/ucModalConfirm.ascx" TagName="ucModalConfirm" TagPrefix="uc1" %>
<%@ Register Src="../ControlesUsuario/ucModalAlert.ascx" TagName="ucModalAlert" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../../jquery/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="../../jquery/jquery-ui-1.9.2.custom.min.js"></script>
    <link rel="stylesheet" href="../../jquery/jquery-ui-1.9.2.custom.min.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                    <asp:GridView ID="gvCatalogo" runat="server" AutoGenerateColumns="false" RowStyle-VerticalAlign="Middle" DataKeyNames="fi_Id"
                        Width="100%" BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" 
                        HeaderStyle-ForeColor="white" AllowSorting="True" CssClass="table table-hover" style="background-color:#ffffff;">
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="fc_PrimNombre" HeaderText="Nombre"/>
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
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCalificacionCliente.aspx.cs" EnableEventValidation="false" UICulture="es" Culture="es-MX" Inherits="Autos_SCC.Views.Cobranza.frmCalificacionCliente" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../ControlesUsuario/ucModalAlert.ascx" TagName="ucModalAlert" TagPrefix="uc1" %>
<%@ Register Src="../ControlesUsuario/ucModalConfirm.ascx" TagName="ucModalConfirm" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../../jquery/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="../../jquery/jquery-ui-1.9.2.custom.min.js"></script>
    <link rel="stylesheet" href="../../jquery/jquery-ui-1.9.2.custom.min.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card">
        <div class="card-block" style="text-align: center;">
            <h3>Calificación de Clientes</h3>
        </div>
    </div>
    <div class="card" style="min-height: 70vh;">
        <asp:UpdatePanel ID="upaCalificacion" runat="server">
            <ContentTemplate>
                <fieldset style="text-align: left">
                    <div style="width: 100%; text-align: center;">
                        <h4>Búsqueda de cliente
                        </h4>
                    </div>
                    <br />
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 30%"></td>
                            <td style="width: 10%; text-align: center;">
                                <asp:Label ID="lblSucursal" runat="server" Text="Sucursal:" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="width: 20%">
                                <asp:DropDownList ID="ddlSucursal" runat="server" Width="97%" CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20%"></td>
                            <td style="width: 20%"></td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align: center;">
                                <asp:RadioButton ID="rdbNoCalificado" runat="server" GroupName="calificacion" Text="Cliente no calificado" />
                            </td>
                            <td style="text-align: center;">
                                <asp:RadioButton ID="rdbCalificado" runat="server" GroupName="calificacion" Text="Cliente calificado" />
                            </td>
                            <td>
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-success" OnClick="btnBuscar_Click" /></td>
                            <td></td>
                        </tr>
                    </table>
                </fieldset>
                <br />
                <center>
                    <div class="table-responsive">
                        <asp:GridView ID="gvClientesCal" runat="server" AutoGenerateColumns="false" DataKeyNames="fi_Id"
                            Width="100%" ShowFooter="true" PageSize="10" BorderStyle="None"
                            BorderWidth="0px" HeaderStyle-BackColor="#646464" OnRowCommand="gvClientesCal_RowCommand"
                            HeaderStyle-ForeColor="white" AllowSorting="True" CssClass="table table-hover" Style="border: 1px solid #efefef;">
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre cliente" />
                                <asp:BoundField DataField="fc_Plazo" HeaderText="Plazo" />
                                <asp:BoundField DataField="fc_TipoAuto" HeaderText="Vehículo" />
                                <asp:BoundField DataField="fm_Precio" HeaderText="Precio" DataFormatString="{0:c}" />
                                <asp:BoundField DataField="fc_Descripcion" HeaderText="Sucursal" />
                                <asp:BoundField DataField="Des_Calificacion" HeaderText="Calificación" />
                                <asp:TemplateField HeaderText="Acciones" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnCalificar" runat="server" CommandName="Calificar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Calificar" CssClass="btn btn-success btn-mini waves-effect waves-light" Enabled='<%# IsEditEnabled(Eval("fi_idCalificacion").ToString()) %>' />
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
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--Modal para hacer pagos--%>
        <asp:HiddenField ID="hdTargetCalificacion" runat="server" />
        <cc1:ModalPopupExtender ID="mpeCalificacion" runat="server" TargetControlID="hdTargetCalificacion"
            PopupControlID="pnlCalificacion" BackgroundCssClass="overlayy">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlCalificacion" runat="server" Width="100%" Height="100%" Style="background-color: #00000070; display: none; margin-left: -6px; padding-top: 10%;">
            <asp:UpdatePanel ID="upaPopCalificacion" runat="server" BorderColor="Black" BackColor=""
                HorizontalAlign="Center" Style="border-radius: 25px; box-shadow: 3px 3px 3px #00000050; background-color: #eeeeee; width: 50%; margin: 0 auto;">
                <ContentTemplate>
                    <table width="80%" style="margin: 0 auto;">
                        <tr>
                            <td style="width: 150"></td>
                            <td style="width: 180"></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center">
                                <h4>
                                    <asp:Label ID="lblTituloCalifica" runat="server" Text="Calificar Credito del Cliente" CssClass="labelTitleModal"></asp:Label></h4>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center">
                                <h4>
                                    <asp:Label ID="lblMensajeCalificar" runat="server" Text="¿ Cual fue el comportamiento del cliente durante su credito ?" CssClass="labelTitleModal"></asp:Label></h4>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center">
                                <h4>
                                    <asp:Label ID="lblClienteCalifica" runat="server"  CssClass="labelTitleModal"></asp:Label></h4>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton runat="server" ID="ibtnBueno" OnClick="ibtnBueno_Click" />
                            </td>
                            <td>
                                <asp:ImageButton runat="server" ID="ibtnRegular" OnClick="ibtnRegular_Click" />
                            </td>
                            <td>
                                <asp:ImageButton runat="server" ID="ibtnMalo" OnClick="ibtnMalo_Click"/>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="upgCalificacion" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="upaCalificacion">
                <ProgressTemplate>
                    <div style="text-align: left">
                        <asp:Label ID="lblProgresBusquedaCalificacion" runat="server" Text="Por favor espere..."></asp:Label>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </asp:Panel>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmFormalizacion.aspx.cs" UICulture="es" Culture="es-MX" Inherits="Autos_SCC.Views.Principales.frmFormalizacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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

        function OcultarModal()
        {
            var modalId = '<%=mpePagosInd.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }

        function Selrdbtn(id)
        {
            var rdBtn = document.getElementById(id);
            var List = document.getElementsByTagName("input");
            for (i = 0; i < List.length; i++) {
                if (List[i].type == "radio" && List[i].id != rdBtn.id) {
                    List[i].checked = false;
                }
            }
        }

    </script>

    <asp:UpdatePanel ID="upaCotizacion" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HidIdCliente" runat="server" Value="0" />
            <asp:HiddenField ID="HidIdCP" runat="server" Value="0" />
            <asp:HiddenField ID="HidIdCPAval" runat="server" Value="0" />
            <div class="card">
                <div class="card-block" style="text-align:center;">
                    <h3><asp:Label ID="lblTituloPantalla" runat="server" Text="Formalización de Créditos" CssClass="labelTitle"></asp:Label></h3>
                </div>
            </div>
            <div class="card" style="min-height:70vh;">
            <fieldset style="text-align:left">
                <div style="width:100%; text-align:center;">
                    <h4>
                        Búsqueda cliente
                    </h4>
                </div>
                <br />
                    <table style="width:100%">
                        <tr>
                            <td style="width:20%">
                            </td>
                            <td style="width:20%">
                                <asp:Label ID="lblSucursal" runat="server" Text="Sucursal:" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:DropDownList ID="ddlSucursal" runat="server" Width="97%" CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
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
                                <asp:Label ID="lblCotizacion" runat="server" Text="Cotización:" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCotizacion" runat="server" Width="97%" CssClass="form-control"
                                     AutoPostBack="true" OnSelectedIndexChanged="ddlCotizacion_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                <br />
                <div style="width:100%; background-color:#d0e7ff; border-radius:5px; padding:5px;">
                    <table style="width:100%">
                        <tr>
                            <td style="width:40%; text-align:center;">
                                <asp:Label ID="lblCliente" runat="server" Text="" CssClass="inputLabel" style="font-weight:bold; font-size:16px;"></asp:Label>
                            </td>
                            <td style="width:20%; text-align:center;"">
                                <asp:Label ID="lblTipoAuto" runat="server" CssClass="inputLabel" style="font-weight:bold; font-size:16px;"></asp:Label>
                            </td>
                            <td style="width:20%; text-align:center;"">
                                <asp:Label ID="lblPrecio" runat="server" CssClass="inputLabel" style="font-weight:bold; font-size:16px;"></asp:Label>
                            </td>
                            <td style="width:20%; text-align:center;"">
                                <asp:Label ID="lblPlazo" runat="server" CssClass="inputLabel" style="font-weight:bold; font-size:16px;"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>

            <fieldset id="fsImprimir" runat="server" visible="false" style="text-align:left">
                <br />
                <div style="width:100%; text-align:center;">
                    <h4>
                        Imprimir
                    </h4>
                </div>
                    <table class="table table-hover" style="margin:0 auto; width:40%;">
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Seleccionar Acreedor:" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btnSelAcreedor" runat="server" Text="Acreedor" CssClass="btn btn-warning btn-sm waves-effect waves-light" OnClick="btnSelAcreedor_Click" />
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lblRespAcreedor" runat="server" Text="[Sin definir]" CssClass="inputLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Seleccionar Sucursal:" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btnSelSucursal" runat="server" Text="Sucursal" CssClass="btn btn-primary btn-sm waves-effect waves-light" OnClick="btnSelSucursal_Click" />
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lblRespSucursal" runat="server" Text="[Sin definir]" CssClass="inputLabel"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblImprimirMens" runat="server" Text="Imprimir mensualidades:" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btnImprimirMes" runat="server" Text=" Imprimir  " CssClass="btn btn-secondary btn-sm waves-effect waves-light" OnClick="btnImprimirMes_Click" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblImprimirPagosInd" runat="server" Text="Imprimir pagos individuales:" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btnImprimirPagosInd" runat="server" Text=" Imprimir  " CssClass="btn btn-secondary btn-sm waves-effect waves-light" OnClick="btnImprimirPagosInd_Click" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Imprimir Contrato de Crédito:" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btnImprimirContratoCred" runat="server" Text=" Imprimir  "  OnClick="btnImprimirContratoCred_Click" />
                                
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEntregarAuto" runat="server" Text="¿Desea entregar el automovil?" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btnEntregarAuto" runat="server" Text=" Aceptar  " CssClass="btn btn-success btn-sm waves-effect waves-light" OnClick="btnEntregarAuto_Click" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <br />
            </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlSucursal" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnEntregarAuto" EventName="Click" />
            <asp:PostBackTrigger ControlID="btnImprimirPagosInd" />
            <asp:PostBackTrigger ControlID="btnImprimirContratoCred" />
        </Triggers>
    </asp:UpdatePanel>


    <%--Modal del Pagos Individuales--%>
    <asp:HiddenField ID="hdPagosIndTarget" runat="server" />
    <cc1:ModalPopupExtender ID="mpePagosInd" runat="server" TargetControlID="hdPagosIndTarget" 
        PopupControlID="pnlPagosInd" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPagosInd" runat="server" BorderColor="Black" BackColor="#e4e4e4" Height="300px"
        Width="820px" HorizontalAlign="Center" Style="display: none; border-radius:25px; box-shadow:3px 3px 3px #00000030;">
        <asp:UpdatePanel ID="UpaPagosInd" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td align="center"><br />
                            <h4><asp:Label ID="lblTitPagosInd" runat="server" Text="Mensualidades" CssClass="labelTitleModal"></asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <br />
                            <asp:Label ID="Label1" runat="server" Text="" CssClass="labelTitleModal"></asp:Label>
                        </td>
                    </tr
                    <tr>
                        <td>
                            <asp:Label ID="lblMensaje" runat="server" Text="Existen pagos individuales que se empalman con las fechas de mensualidades, ¿Que acción desea realizar?" CssClass="inputLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><br /><br />
                            <asp:RadioButtonList ID="rblPagosInd" runat="server" RepeatDirection="Vertical" Width="100%" Enabled="false">
                                <asp:ListItem Text="Meses con pagos dobles" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Separar pagos Ind. de mensualidades" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td><br />
                            <asp:Button ID="btnAceptarPagosInd" runat="server" Text="Aceptar" OnClientClick="OcultarModal();" OnClick="btnAceptarPagosInd_Click" CssClass="btn btn-success" />
                            <asp:Button ID="btnCancelarModalPagInd" runat="server" Text="Cancelar" OnClientClick="OcultarModal();" CssClass="btn btn-danger" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAceptarPagosInd" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>


    <%--Modal de Acreedores--%>
    <asp:HiddenField ID="hdTargetAcreedor" runat="server" />
    <cc1:ModalPopupExtender ID="mpeAcreedor" runat="server" TargetControlID="hdTargetAcreedor" 
        PopupControlID="pnlAcreedor" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlAcreedor" runat="server" BorderColor="Black" BackColor="#e4e4e4" Height="300px"
        Width="820px" HorizontalAlign="Center" Style="display: none; border-radius:25px;box-shadow:3px 3px 3px #00000030;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td colspan="2" style="text-align:center"><br />
                            <h4><asp:Label ID="lblCaption" runat="server" Text="Lista de Acreedores" CssClass="labelTitleModal"></asp:Label></h4>
                        </td>
                    </tr>
                </table>
                <br />
                <center>
                    <table width="90%">
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlGridAcreedor" runat="server" Width="100%" Height="150" ScrollBars="Auto">
                                    <div class="table">
                                    <asp:GridView ID="gvAcreedor" runat="server" AutoGenerateColumns="false" Width="100%" 
                                        Font-Size="10px" PageSize="10" BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" 
                                        HeaderStyle-ForeColor="white" AllowSorting="True" CssClass="table table-hover" style="background-color:#ffffff;">
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText=" Seleccione ">
                                                <ItemTemplate>
                                                     <asp:RadioButton ID="rbSelecciona" runat="server" OnClick="javascript:Selrdbtn(this.id)" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="fc_Descripcion" HeaderText="Nombre"/>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No se encontraron registros
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    </div>
                                </asp:Panel>
                            </td>                            
                        </tr>
                        <tr>
                            <td colspan="2" style="height:10px">
                                <asp:Label ID="lblErrorAcreedor" runat="server" CssClass="errorLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="50%">
                                <div style="text-align:right; float:right">
                                    <asp:Button ID="btnAceptarAcreedor" runat="server" Text="Aceptar" OnClick="btnAceptarAcreedor_Click" CssClass="btn btn-success"/>
                                </div>
                             </td>
                             <td width="50%">
                                <div style="text-align:left; float:left">
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger"/>
                                </div>
                             </td>
                        </tr>
                    </table>
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <%--Modal de Direcciones--%>
    <asp:HiddenField ID="hdTargetSucursales" runat="server" />
    <cc1:ModalPopupExtender ID="mpeSucursales" runat="server" TargetControlID="hdTargetSucursales" 
        PopupControlID="pnlSucursales" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlSucursales" runat="server" BorderColor="Black" BackColor="#e4e4e4" Height="300px"
        Width="820px" HorizontalAlign="Center" Style="display: none; border-radius:25px; box-shadow:3px 3px 3px #00000030;">
        <asp:UpdatePanel ID="upaSucursales" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td colspan="2" style="text-align:center"><br />
                            <h4><asp:Label ID="Label4" runat="server" Text="Lista de Direcciones" CssClass="labelTitleModal"></asp:Label></h4>
                        </td>
                    </tr>
                </table>
                <br />
                <center>
                    <table width="90%">
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlListaDirecciones" runat="server" Width="100%" Height="150" ScrollBars="Auto">
                                    <div class="table">
                                    <asp:GridView ID="gvDirecciones" runat="server" AutoGenerateColumns="false" Width="100%" 
                                        Font-Size="10px" PageSize="10" BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" 
                                        HeaderStyle-ForeColor="white" AllowSorting="True" CssClass="table table-hover" style="background-color:#ffffff;">
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText=" Seleccione ">
                                                <ItemTemplate>
                                                     <asp:RadioButton ID="rbSelecciona" runat="server" OnClick="javascript:Selrdbtn(this.id)" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="fc_Descripcion" HeaderText="Dirección"/>
                                            <asp:BoundField DataField="fc_TelefonoSucursal" HeaderText="Telefono"/>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No se encontraron registros
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    </div>
                                </asp:Panel>
                            </td>                            
                        </tr>
                        <tr>
                            <td colspan="2" style="height:10px">
                                <asp:Label ID="lblErrorDir" runat="server" CssClass="errorLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="50%">
                                <div style="text-align:right; float:right">
                                    <asp:Button ID="btnAceptarDir" runat="server" Text="Aceptar" OnClick="btnAceptarDir_Click" CssClass="btn btn-success"/>
                                </div>
                             </td>
                             <td width="50%">
                                <div style="text-align:left; float:left">
                                    <asp:Button ID="btnCancelarDir" runat="server" Text="Cancelar" CssClass="btn btn-danger"/>
                                </div>
                             </td>
                        </tr>
                    </table>
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

</asp:Content>

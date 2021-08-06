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
            <br />
            <table width="100%">
                <tr>
                    <td style="text-align:center; width:100%">
                        <asp:Label ID="lblTituloPantalla" runat="server" Text="Formalización de Créditos" CssClass="labelTitle"></asp:Label>
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
                                <asp:Label ID="lblSucursal" runat="server" Text="Sucursal:" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:DropDownList ID="ddlSucursal" runat="server" Width="97%" CssClass="listInput"
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
                                <asp:DropDownList ID="ddlCotizacion" runat="server" Width="97%" CssClass="listInput"
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

            <fieldset id="fsImprimir" runat="server" visible="false" style="text-align:left">
                <legend>
                    <span>
                        Imprimir
                    </span>
                </legend>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Seleccionar Acreedor:" CssClass="inputLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btnSelAcreedor" runat="server" Text="Acreedor" CssClass="button" OnClick="btnSelAcreedor_Click" />
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
                                <asp:Button ID="btnSelSucursal" runat="server" Text="Sucursal" CssClass="button" OnClick="btnSelSucursal_Click" />
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
                                <asp:Button ID="btnImprimirMes" runat="server" Text="Imprimir" CssClass="button" OnClick="btnImprimirMes_Click" />
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
                                <asp:Button ID="btnImprimirPagosInd" runat="server" Text="Imprimir" CssClass="button" OnClick="btnImprimirPagosInd_Click" />
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
                                <asp:Button ID="btnEntregarAuto" runat="server" Text="Aceptar" CssClass="button" OnClick="btnEntregarAuto_Click" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    
            </fieldset>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlSucursal" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnEntregarAuto" EventName="Click" />
            <asp:PostBackTrigger ControlID="btnImprimirPagosInd" />
        </Triggers>
    </asp:UpdatePanel>


    <%--Modal del Pagos Individuales--%>
    <asp:HiddenField ID="hdPagosIndTarget" runat="server" />
    <cc1:ModalPopupExtender ID="mpePagosInd" runat="server" TargetControlID="hdPagosIndTarget" 
        PopupControlID="pnlPagosInd" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPagosInd" runat="server" BorderColor="Black" BackColor="White" Height="250px"
        Width="400px" HorizontalAlign="Center" Style="display: none">
        <asp:UpdatePanel ID="UpaPagosInd" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblTitPagosInd" runat="server" Text="Pagos individuales" CssClass="labelTitleModal"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="Label1" runat="server" Text="" CssClass="labelTitleModal"></asp:Label>
                        </td>
                    </tr
                    <tr>
                        <td>
                            <asp:Label ID="lblMensaje" runat="server" Text="Existen pagos individuales que se empalman con las fechas de mensualidades, ¿Que acción desea realizar?" CssClass="inputLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rblPagosInd" runat="server" RepeatDirection="Vertical" Width="100%">
                                <asp:ListItem Text="Meses con pagos dobles" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Separar pagos Ind. de mensualidades" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnAceptarPagosInd" runat="server" Text="Aceptar" OnClientClick="OcultarModal();" OnClick="btnAceptarPagosInd_Click" CssClass="button" />
                            <asp:Button ID="btnCancelarModalPagInd" runat="server" Text="Cancelar" OnClientClick="OcultarModal();" CssClass="button" />
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
    <asp:Panel ID="pnlAcreedor" runat="server" BorderColor="Black" BackColor="White" Height="240px"
        Width="370px" HorizontalAlign="Center" Style="display: none">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td colspan="2" style="text-align:center">
                            <asp:Label ID="lblCaption" runat="server" Text="Lista de Acreedores" CssClass="labelTitleModal"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <center>
                    <table width="90%">
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlGridAcreedor" runat="server" Width="350" Height="150" ScrollBars="Auto">
                                    <asp:GridView ID="gvAcreedor" runat="server" AutoGenerateColumns="false" Width="100%" 
                                        Font-Size="10px" PageSize="10" BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" 
                                        HeaderStyle-ForeColor="white" AllowSorting="True">
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
                                    <asp:Button ID="btnAceptarAcreedor" runat="server" Text="Aceptar" OnClick="btnAceptarAcreedor_Click" CssClass="button"/>
                                </div>
                             </td>
                             <td width="50%">
                                <div style="text-align:left; float:left">
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button"/>
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
    <asp:Panel ID="pnlSucursales" runat="server" BorderColor="Black" BackColor="White" Height="240px"
        Width="350px" HorizontalAlign="Center" Style="display: none">
        <asp:UpdatePanel ID="upaSucursales" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td colspan="2" style="text-align:center">
                            <asp:Label ID="Label4" runat="server" Text="Lista de Direcciones" CssClass="labelTitleModal"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <center>
                    <table width="90%">
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlListaDirecciones" runat="server" Width="340" Height="150" ScrollBars="Auto">
                                    <asp:GridView ID="gvDirecciones" runat="server" AutoGenerateColumns="false" Width="700" 
                                        Font-Size="10px" PageSize="10" BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" 
                                        HeaderStyle-ForeColor="white" AllowSorting="True">
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
                                    <asp:Button ID="btnAceptarDir" runat="server" Text="Aceptar" OnClick="btnAceptarDir_Click" CssClass="button"/>
                                </div>
                             </td>
                             <td width="50%">
                                <div style="text-align:left; float:left">
                                    <asp:Button ID="btnCancelarDir" runat="server" Text="Cancelar" CssClass="button"/>
                                </div>
                             </td>
                        </tr>
                    </table>
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

</asp:Content>

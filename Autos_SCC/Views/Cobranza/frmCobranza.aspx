<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCobranza.aspx.cs"  UICulture="es" Culture="es-MX" Inherits="Autos_SCC.Views.Cobranza.frmCobranza" contentType="text/html; charset=UTF-8"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .overlayy
        {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            filter: alpha(opacity=80);
            opacity: 0.8;
            background: rgba(0,0,0,0.8);
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

        function switchViews(obj, row) {
            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);

            if (div.style.display == "none") {
                div.style.display = "inline";
                if (row == 'alt') {
                    img.src = "../../Images/Botones/flecha_cierra.png";
                }
                else {
                    img.src = "../../Images/Botones/flecha_cierra.png";
                }

                img.alt = "Cerrar para visualizar otros integrantes";
            }
            else {
                div.style.display = "none";
                if (row == 'alt') {
                    img.src = "../../Images/Botones/flecha_abre1.png";
                }
                else {
                    img.src = "../../Images/Botones/flecha_abre1.png";
                }
                img.alt = "Ampliar para visualizar integrantes";
            }

        }

    </script>

    <asp:UpdatePanel ID="upaCotizacion" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-block" style="text-align:center;">
                    <h3><asp:Label ID="lblTituloPantalla" runat="server" Text="Módulo de Cobranza" CssClass="labelTitle"></asp:Label></h3>
                </div>
            </div>
            <div class="card" style="height:70vh;">
            <fieldset style="text-align:left">
                <div style="width:100%; text-align:center;">
                    <h4>
                        Sucursal
                    </h4>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-4">
                        &nbsp;
                    </div>
                    <div class="col-md-1" style="text-align:right;padding-top:12px;">
                        <asp:Label ID="lblSucursal" runat="server" Text="Sucursal:" CssClass="inputLabel"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlSucursal" runat="server" Width="100%" CssClass="form-control"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        &nbsp;
                    </div>
                </div>
                    <table style="width:100%">
                        <tr>
                            <td style="width:20%">
                            </td>
                            <td style="width:20%">
                                
                            </td>
                            <td style="width:20%">
                                
                            </td>
                            <td style="width:20%">
                            </td>
                            <td style="width:20%">
                            </td>
                        </tr>
                    </table>
            </fieldset>
            
            <br />
            <asp:Panel ID="pnlCreditos" runat="server" Width="100%" Height="500" ScrollBars="Auto">
                <table style="width:100%">
                        <tr>
                            <td style="width:1%"></td>
                            <td style="width:98%; text-align:right">
                            <asp:Button ID="btnExportar" runat="server" Text="Exportar a Excel" OnClick="btnExportar_Click" CssClass="btn btn-secondary" Visible="false"/>
                            </td>
                            <td style="width:1%"></td>
                        </tr>
                    </table>
                    <br />
                <table style="width:100%">
                    <tr>
                        <td>
                            <div class="table">
                            <asp:GridView ID="gvCreditos" runat="server" 
                                AllowPaging="false" AutoGenerateColumns="false"
                                OnRowDataBound="gvCreditos_RowDataBound"
                                PageSize="10" BorderStyle="None" 
                                BorderWidth="0px" HeaderStyle-BackColor="#646464"
                                HeaderStyle-ForeColor="white" AllowSorting="True"
                                Width="100%" DataKeyNames="fi_Id" CssClass="table table-hover">
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <itemtemplate>
                                            <a href="javascript:switchViews('div<%# Eval("fi_Id") %>', 'one');">
                                                <img id="imgdiv<%# Eval("fi_Id") %>" alt="Click para visualizar la tabla de amortización" border="0" src="../../Images/Botones/flecha_abre1.png" />
                                            </a>
                                        </itemtemplate>
                                        <alternatingitemtemplate>
                                            <a href="javascript:switchViews('div<%# Eval("fi_Id") %>', 'alt');">
                                                <img id="imgdiv<%# Eval("fi_Id") %>" alt="Click para visualizar la tabla de amortización" border="0" src="../../Images/Botones/flecha_abre1.png" />
                                            </a>
                                        </alternatingitemtemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre completo"/>
                                    <asp:BoundField DataField="TipoAuto" HeaderText="Tipo de auto"/>
                                    <asp:BoundField DataField="DescPlazo" HeaderText="Plazo"/>
                                    <asp:BoundField DataField="fm_Precio" HeaderText="Precio auto" DataFormatString="{0:c}"/>
                                    <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Image ID="imEstatus" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FechaProxPago" HeaderText="Fecha prox. pago"/>
                                    <asp:BoundField DataField="MontoProxPago" HeaderText="Monto de pago" DataFormatString="{0:c}"/>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbMensaje" runat="server" ImageUrl="~/Images/Iconos/message.png" Width="29px" Height="29px"
                                                ToolTip="Clic aqui para enviar recordatorio de pago" OnClick="imbMensaje_Click"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <itemtemplate>
                                            <tr>
                                                <td colspan="100%" align="left">
                                                    <div id="div<%# Eval("fi_Id") %>" style="display:none; position:relative;">
                                                        <div class="table">
                                                        <asp:GridView ID="gvDetalle" runat="server" Width="100%"
                                                            AutoGenerateColumns="false" ShowFooter = "true" EmptyDataText="No hay resultados para era busqueda." 
                                                            PageSize="10" BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" 
                                                            HeaderStyle-ForeColor="white" AllowSorting="True" OnRowDataBound="gvDetalle_RowDataBound" CssClass="table table-hover">
                                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <Columns>
                                                                <asp:BoundField DataField="fi_NoPago" HeaderText="No. Pago"/>
                                                                <asp:BoundField DataField="fm_MontoCompromiso" HeaderText="Monto compromiso" DataFormatString="{0:c}"/>
                                                                <asp:BoundField DataField="fm_MontoPagado" HeaderText="Monto pagado" DataFormatString="{0:c}"/>
                                                                <asp:BoundField DataField="fd_FechaCompromiso" HeaderText="Fecha compromiso"/>
                                                                <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="imEstatus" runat="server" Width="16" Height="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="fd_FechaPago" HeaderText="Fecha de pago"/>
                                                                <asp:BoundField DataField="fc_UsuarioRegistroPago" HeaderText="Usuario reg. pago"/>
                                                            </Columns>
                                                        </asp:GridView>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </itemtemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings Mode="NumericFirstLast" />
                            </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlSucursal" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="btnExportar" />
        </Triggers>
    </asp:UpdatePanel>


    <%--Modal de Cambio de tasa--%>
    <asp:HiddenField ID="hdTargetMensaje" runat="server" />
    <cc1:ModalPopupExtender ID="mpeMensaje" runat="server" TargetControlID="hdTargetMensaje" 
        PopupControlID="pnlMensaje" BackgroundCssClass="overlayy">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlMensaje" runat="server" Width="100%" Height="100%" Style="background-color:#00000070; display: none; margin-left:-6px; padding-top:10%;">
        <asp:UpdatePanel ID="upaMensaje" runat="server" BorderColor="Black" BackColor=""
        HorizontalAlign="Center" Style="border-radius:25px; box-shadow:3px 3px 3px #00000050; background-color:#eeeeee; width:50%;margin:0 auto;">
            <ContentTemplate>
                <table width="90%">
                    <tr>
                        <td colspan="2" style="text-align:center"><br />
                            <h3><asp:Label ID="lblTituloMensaje" runat="server" Text="Escribe el mensaje a enviar" CssClass="labelTitleModal"></asp:Label></h3>
                        </td>
                    </tr>
                </table>
                <center>
                    <table style="width:80%; margin:0 auto;">
                        <tr>
                            <td style="text-align:left;" colspan="2">
                                <asp:Label ID="lblTasaInteres" runat="server" Text="&nbsp;&nbsp;Mensaje:" CssClass="inputLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;" colspan="2">
                                <asp:TextBox ID="txtMensaje" runat="server" CssClass="form-control" Width="95%" TextMode="MultiLine" Rows="3" MaxLength="159"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ftbTasaInteres" runat="server" FilterMode="ValidChars" TargetControlID="txtMensaje"
                                    ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ 123456789.%"></cc1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height:10px; text-align:right">
                                <asp:RequiredFieldValidator ID="rfvTasaInteres" runat="server" ControlToValidate="txtMensaje"
                                    Display="Dynamic" ErrorMessage="El campo es requerido" ForeColor="Red" ValidationGroup="VCambioTasa"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center">
                                <asp:Button ID="btnEnviarMensaje" runat="server" Text=" Enviar " CssClass="btn btn-success" OnClick="btnEnviarMensaje_Click" />                            
                                <asp:Button ID="btnCancelarMensaje" runat="server" Text=" Cancelar " CssClass="btn btn-danger" OnClick="btnCerrarMensaje_Click" />
                            </td>
                        </tr>
                    </table><br />
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmTotalesCartera.aspx.cs" UICulture="es" Culture="es-MX" Inherits="Autos_SCC.Views.Report.frmTotalesCartera" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../../jquery/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="../../jquery/jquery-ui-1.9.2.custom.min.js"></script>
    <link rel="stylesheet" href="../../jquery/jquery-ui-1.9.2.custom.min.css" type="text/css" />
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

    </script>
    <asp:UpdatePanel ID="upaCotizacion" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-block" style="text-align:center;">
                    <h3><asp:Label ID="lblTituloPantalla" runat="server" Text="Reporte de Totales de Cartera" CssClass="labelTitle"></asp:Label></h3>
                </div>
            </div>
            <div class="card" style="min-height:60vh;">
                <fieldset style="text-align:left">
                    <legend>
                        <div style="width:100%; text-align:center;">
                            
                        </div>
                    </legend>
                    <div class="row">
                        <%--<div class="col-md-2">
                            &nbsp;
                        </div>--%>
                        <div class="col-md-4">
                            <div class="row">
                            <%--<div class="col-md-3">
                                
                            </div>--%>
                            <%--<div class="col-md-8">
                                 
                            </div>--%>
                            <div class="col-md-1">
                                
                            </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblSucursal" runat="server" Text="Sucursal:" CssClass="inputLabel"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="form-control" Width="100%">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1" style="text-align:center;">
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-success" OnClick="btnBuscar_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            &nbsp;
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-3">
                            &nbsp;
                        </div>
                        <div class="col-md-3" style="text-align:center;">
                            <%--<asp:Label ID="lblTipoRep" runat="server" Text="Tipo:" CssClass="inputLabel"></asp:Label>--%>
                        </div>
                        <div class="col-md-3" style="text-align:center;">
                            
                                <%--<asp:RadioButtonList id="rbReporte" runat="server"  RepeatDirection="Horizontal">
                                <asp:ListItem Text="&nbsp;Semanal" Value="2" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="&nbsp;Mensual" Value="1"></asp:ListItem>
                                <asp:ListItem Text="&nbsp;Anual" Value="3"></asp:ListItem>
                            </asp:RadioButtonList> --%>                           
                        </div>
                        <div class="col-md-3">
                            &nbsp;
                        </div>
                    </div>

                </fieldset>
                <asp:Panel ID="pnlReporte" runat="server" Visible="false">
                    <fieldset style="text-align:left" >
                    <br />
                    <div style="text-align:center;">
                        <h4>
                            <asp:Label ID="lblRepText" runat="server" Text="Reporte"></asp:Label>
                            
                        </h4>
                    </div>
                        <table style="width:100%">
                            <tr>
                                <td style="width:1%"></td>
                                <td style="width:98%; text-align:right">
                                    <%--<asp:Button ID="btnExportar" runat="server" Text="Exportar a Excel" OnClick="btnExportar_Click" CssClass="btn btn-secondary"/>--%>
                                </td>
                                <td style="width:1%"></td>
                            </tr>
                        </table>
                        <asp:UpdatePanel ID="upaReporte" runat="server">
                            <ContentTemplate>
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:21%"></td>
                                        <td style="width:29%"></td>
                                        <td style="width:29%"></td>
                                        <td style="width:21%"></td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblTextTotal" runat="server" Text="Total Cartera:  " Font-Size="XX-Large"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbltotalT" runat="server" Text="" CssClass="inputLabel" Font-Size="XX-Large"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblTextTotalVehi" runat="server" Text="Total Vehículos:  " CssClass="inputLabel" Font-Size="XX-Large"></asp:Label>

                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblTotalvehi" runat="server" Text="" CssClass="inputLabel" Font-Size="XX-Large"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td colspan="2">
                                            <%--<center>
                                               <asp:Label ID="lblTitulo1" runat="server" Text="AUTOS EL ANGEL DE PUEBLA" CssClass="inputLabel"></asp:Label>
                                            </center> --%>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td colspan="2">
                                            <%--<center>
                                                <asp:Label ID="Label1" runat="server" Text="LISTADO DE PAGOS POR RECIBIR EN EL PERIODO COMPRENDIDO" CssClass="inputLabel"></asp:Label>
                                            </center> --%>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvReporte" runat="server" AutoGenerateColumns="false" DataKeyNames="fi_Id" ShowFooter="true"
                                        Font-Size="Small" BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" 
                                        Width="100%" HeaderStyle-ForeColor="white" AllowSorting="True" CssClass="table table-hover"
                                        OnPreRender="gvReporte_PreRender" EmptyDataText="No se encontraron registros para mostrar..." >
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre Cliente" />
                                            <asp:BoundField DataField="Vehiculo" HeaderText="Vehículo" />
                                            <asp:BoundField DataField="MontoTotal" HeaderText="Monto Compromiso" DataFormatString="{0:C}" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                </fieldset>
                </asp:Panel>
            </div>
            </div><br />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            <%--<asp:PostBackTrigger ControlID="btnExportar" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

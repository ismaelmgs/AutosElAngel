﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmPaysPeriod.aspx.cs" UICulture="es" Culture="es-MX" Inherits="Autos_SCC.Views.Report.frmPaysPeriod" %>
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
                    <h3><asp:Label ID="lblTituloPantalla" runat="server" Text="Reporte de Pagos Recibidos" CssClass="labelTitle"></asp:Label></h3>
                </div>
            </div>
            <div class="card" style="min-height:60vh;">
            <fieldset style="text-align:left">
                <div style="width:100%; text-align:center;">
                    <h4>
                        Periodo de reporte
                    </h4>
                </div><br />
                <div class="row">
                    <div class="col-md-1">
                        &nbsp;
                    </div>
                    <div class="col-md-5">
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha inicial:" CssClass="inputLabel"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="form-control" Width="100%" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:ImageButton ID="imbFechaInicial" runat="server" ImageUrl="~/Images/Botones/Calendar.ico" Width="24px" Height="24px" style="margin-bottom:-14px" />
                                <cc1:CalendarExtender ID="calFechaInicial" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imbFechaInicial" TargetControlID="txtFechaInicial">
                                </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="mskFechaInicial" runat="server" ClearTextOnInvalid="True"
                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFechaInicial"
                                    UserDateFormat="DayMonthYear">
                                </cc1:MaskedEditExtender>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha final:" CssClass="inputLabel"></asp:Label>
                            </div>
                            <div class="col-md-8">
                        <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="form-control" Width="100%" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:ImageButton ID="imbFechaFinal" runat="server" ImageUrl="~/Images/Botones/Calendar.ico" Width="24px" Height="24px" style="margin-bottom:-14px" />
                                <cc1:CalendarExtender ID="calFechaFinal" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imbFechaFinal" TargetControlID="txtFechaFinal">
                                </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="mskFechaFinal" runat="server" ClearTextOnInvalid="True"
                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFechaFinal"
                                    UserDateFormat="DayMonthYear">
                                </cc1:MaskedEditExtender>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-1">
                        &nbsp;
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-1">
                        &nbsp;
                    </div>
                    <div class="col-md-3" style="text-align:center;">
                        <asp:Label ID="lblTodasFechas" runat="server" Text ="Todas las Fechas" CssClass="inputLabel"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkTodas" runat="server" AutoPostBack="true" OnCheckedChanged="chkTodas_CheckedChanged" />
                    </div>
                    <div class="col-md-5" style="text-align:center;">
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblSucursal" runat="server" Text="Sucursal:" CssClass="inputLabel"></asp:Label>
                            </div>
                            <div class="col-md-10">
                                <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="form-control" Width="100%">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2" style="text-align:center;">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-success" OnClick="btnBuscar_Click" />
                    </div>
                    <div class="col-md-1">
                        &nbsp;
                    </div>
                </div>
            </fieldset>

            <asp:Panel ID="pnlReporte" runat="server" Visible="false">
            <fieldset style="text-align:left" >
                <div style="width:100%; text-align:center;">
                    <h4>
                        Reporte
                    </h4>
                </div>
                    <table style="width:100%">
                        <tr>
                            <td style="width:1%"></td>
                            <td style="width:98%; text-align:right">
                            <asp:Button ID="btnExportar" runat="server" Text="Exportar a Excel" OnClick="btnExportar_Click" CssClass="btn btn-secondary"/>
                            </td>
                            <td style="width:1%"></td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="upaReporte" runat="server">
                        <ContentTemplate>
                            <table style="width:100%">
                        <tr>
                            <td style="width:1%"></td>
                            <td style="width:98%"></td>
                            <td style="width:1%"></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <center>
                                    <asp:Label ID="lblTitulo1" runat="server" Text="AUTOS EL ANGEL DE PUEBLA" CssClass="inputLabel"></asp:Label>
                                </center> 
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <center>
                                    <asp:Label ID="Label1" runat="server" Text="CONCENTRADO DE PAGOS RECIBIDOS EN EL PERIODO COMPRENDIDO" CssClass="inputLabel"></asp:Label>
                                </center> 
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <center>
                                    <asp:Label ID="lblDiaInicial" runat="server" Text="" CssClass="inputLabel"></asp:Label>
                                    <asp:Label ID="lblMesInicial" runat="server" Text="" CssClass="inputLabel"></asp:Label>
                                    <asp:Label ID="LblAnioInicial" runat="server" Text="" CssClass="inputLabel"></asp:Label>
                                    <asp:Label ID="lblAl" runat="server" Text="" CssClass="inputLabel"></asp:Label>
                                    <asp:Label ID="lblDiaFinal" runat="server" Text="" CssClass="inputLabel"></asp:Label>
                                    <asp:Label ID="lblMesFinal" runat="server" Text="" CssClass="inputLabel"></asp:Label>
                                    <asp:Label ID="lblAnioFinal" runat="server" Text="" CssClass="inputLabel"></asp:Label>
                                </center> 
                            </td>
                            <td>
                            </td>
                        </tr>

                    </table>
                            <div class="table-responsive">
                                <asp:GridView ID="gvReporte" runat="server" AutoGenerateColumns="false" DataKeyNames="fi_Id"
                                    Font-Size="Small" BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" ShowFooter="true" 
                                    Width="100%" HeaderStyle-ForeColor="white" AllowSorting="True" OnRowDataBound="gvReporte_RowDataBound" CssClass="table table-hover">
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Cliente" />
                                        <asp:BoundField DataField="fc_Marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="fc_Version" HeaderText="Versión" />
                                        <asp:BoundField DataField="fc_TipoAuto" HeaderText="Tipo" />
                                        <asp:BoundField DataField="fm_Precio" HeaderText="Precio" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" />
                                        <asp:BoundField DataField="fc_Placa" HeaderText="Placa" />
                                        <asp:BoundField DataField="fi_Modelo" HeaderText="Modelo" />
                                        <asp:BoundField DataField="fc_NoSerie" HeaderText="No. Serie" />
                                        <asp:BoundField DataField="fc_Sucursal" HeaderText="Sucursal" />
                                        <asp:BoundField DataField="fm_MontoPagado" HeaderText="Monto pagado" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}"/>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            
                            <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
            </fieldset>
            </asp:Panel>
            </div><br />
            
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            <asp:PostBackTrigger ControlID="btnExportar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

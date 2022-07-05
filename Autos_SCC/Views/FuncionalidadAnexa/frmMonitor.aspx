<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmMonitor.aspx.cs" 
    Inherits="Autos_SCC.Views.FuncionalidadAnexa.frmMonitor" UICulture="es" Culture="es-MX" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upaCotizacion" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HidIdCliente" runat="server" Value="0" />
            <asp:HiddenField ID="HidIdCP" runat="server" Value="0" />
            <asp:HiddenField ID="HidIdCPAval" runat="server" Value="0" />
            <div class="card">
                <div class="card-block" style="text-align:center;">
                    <h3><asp:Label ID="lblTituloPantalla" runat="server" Text="Monitor de Créditos" CssClass="labelTitle"></asp:Label></h3>
                </div>
            </div>
            <div class="card" style="min-height:65vh;">
            <fieldset style="text-align:left">
                <div style="width:100%; text-align:center;">
                    <h4>
                        Búsqueda cliente
                    </h4>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-3">
                        &nbsp;
                    </div>
                    <div class="col-md-2" style="text-align:right;padding-top:12px;">
                        <asp:Label ID="lblOpcion" runat="server" Text="Opción de Búsqueda:" 
                                    CssClass="inputLabel"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlOpcion" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlOpcion_SelectedIndexChanged" Width="97%">
                        <asp:ListItem Text="Seleccione" Value="0"></asp:ListItem>
                        <asp:ListItem Text="No. Cotización" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Por Sucursal" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        &nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        &nbsp;
                    </div>
                    <div class="col-md-2" style="text-align:right;padding-top:12px;">
                        <asp:Label ID="lblNoCotizacion" runat="server" CssClass="inputLabel" Text="No. Cotización" Visible="false"></asp:Label>
                        <asp:Label ID="lblSucursal" runat="server" CssClass="inputLabel" Text="Sucursal:" Visible="false"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-md-10">
                                <asp:TextBox ID="txtNoCotizacion" runat="server" CssClass="form-control" AutoPostBack="true"
                                    Visible="false" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-md-2" style="text-align:center;">
                                <asp:ImageButton ID="imbBuscaCliente" runat="server" ImageUrl="~/Images/Botones/Find.ico" OnClick="imbBuscaCliente_Click"
                                    style="vertical-align:middle; margin-top:12px;" ToolTip="Selecciona un auto existente" Visible="false"/>
                            </div>
                        </div>
                            <asp:DropDownList ID="ddlSucursal" runat="server" AutoPostBack="true" 
                                CssClass="form-control" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged" 
                                Width="97%" Visible="false">
                            </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        &nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        &nbsp;
                    </div>
                    <div class="col-md-2" style="text-align:right;padding-top:12px;">
                        <asp:Label ID="lblCotizacion" runat="server" Text="Cotización:" CssClass="inputLabel" Visible="false"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlCotizacion" runat="server" Width="97%" CssClass="form-control" Visible="false"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlCotizacion_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        &nbsp;
                    </div>
                </div>
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

            <fieldset style="text-align:left"> <br />
                <div style="width:100%; text-align:center;">
                    <h4>
                        Resultados...
                    </h4>
                </div>
                    <center>
                        <div class="table-responsive">
                        <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="false" Width="100%" 
                            Font-Size="14px" PageSize="10" BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" HeaderStyle-ForeColor="white" AllowSorting="True" CssClass="table table-hover">
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#01609F" CssClass="titleHeader" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" CssClass="" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="fi_Id" HeaderText="No. Cotización"/>
                                <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre"  />
                                <asp:BoundField DataField="fc_TipoAuto" HeaderText="Tipo de auto" />
                                <asp:BoundField DataField="fm_Precio" HeaderText="Precio" DataFormatString="{0:c}" />
                                <asp:BoundField DataField="fc_Plazo" HeaderText="Plazo" />
                                <asp:BoundField DataField="fc_EstatusCredito" HeaderText="Estatus" />
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron registros
                            </EmptyDataTemplate>
                        </asp:GridView>
                        </div>
                    </center>
            </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlSucursal" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

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
            <br />
            <table width="100%">
                <tr>
                    <td style="text-align:center; width:100%">
                        <asp:Label ID="lblTituloPantalla" runat="server" Text="Monitor de Créditos" CssClass="labelTitle"></asp:Label>
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
                                <asp:Label ID="lblOpcion" runat="server" Text="Opción de Búsqueda:" 
                                    CssClass="inputLabel"></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:DropDownList ID="ddlOpcion" runat="server" AutoPostBack="true" CssClass="listInput" OnSelectedIndexChanged="ddlOpcion_SelectedIndexChanged" Width="97%">
                                <asp:ListItem Text="Seleccione" Value="0"></asp:ListItem>
                                <asp:ListItem Text="No. Cotización" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Por Sucursal" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width:20%">
                            </td>
                            <td style="width:20%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width:20%">
                            </td>
                            <td style="width:20%">
                                <asp:Label ID="lblNoCotizacion" runat="server" CssClass="inputLabel" Text="No. Cotización" Visible="false"></asp:Label>
                                <asp:Label ID="lblSucursal" runat="server" CssClass="inputLabel" Text="Sucursal:" Visible="false"></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:TextBox ID="txtNoCotizacion" runat="server" CssClass="inputCampo" AutoPostBack="true"
                                    Visible="false" Width="75%"></asp:TextBox>
                                <asp:ImageButton ID="imbBuscaCliente" runat="server" ImageUrl="~/Images/Botones/Find.ico" OnClick="imbBuscaCliente_Click"
                                    style="vertical-align:middle" ToolTip="Selecciona un auto existente" Visible="false"/>
                                <asp:DropDownList ID="ddlSucursal" runat="server" AutoPostBack="true" 
                                    CssClass="listInput" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged" 
                                    Width="97%" Visible="false">
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
                                <asp:Label ID="lblCotizacion" runat="server" Text="Cotización:" CssClass="inputLabel" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCotizacion" runat="server" Width="97%" CssClass="listInput" Visible="false"
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

            <fieldset style="text-align:left">
                <legend>
                    <span>
                        Resultados...
                    </span>
                </legend>
                    <center>
                        <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="false" Width="80%" 
                            Font-Size="10px" PageSize="10" BorderStyle="None" BorderWidth="0px" HeaderStyle-BackColor="#646464" HeaderStyle-ForeColor="white" AllowSorting="True">
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
                    </center>
            </fieldset>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlSucursal" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

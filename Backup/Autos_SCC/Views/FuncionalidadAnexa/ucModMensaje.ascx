<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucModMensaje.ascx.cs" Inherits="Autos_SCC.Views.FuncionalidadAnexa.ucModMensaje" %>
<table style="width:400px;">
    <tr>
        <td width="10%">
        </td>
        <td width="80%">
        </td>
        <td width="10%">
        </td>
    </tr>
    <tr>
        <td class="derecha">
            <asp:Image ID="imgIcono" runat="server" ImageUrl="~/Images/Iconos/info.gif" />
        </td>
        <td colspan="2">
            <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td style="text-align: center">
            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
</table>


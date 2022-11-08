<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucModalAlert.ascx.cs" Inherits="Autos_SCC.Views.ControlesUsuario.ucModalAlert" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<ajax:ModalPopupExtender ID="mpext2" runat="server" BackgroundCssClass="overlayy"
    TargetControlID="pnlPopupAlert" PopupControlID="pnlPopupAlert" OkControlID="btnOk2" CancelControlID="btnOk2">
</ajax:ModalPopupExtender>

<asp:Panel ID="pnlPopupAlert" runat="server" BackColor="" Style="display: none; background-color:#00000073; width:100%; height:100%;" DefaultButton="btnOk2">
    <div class="card" style=" background-color:#e4e4e4; border-radius:20px; padding:10px; width:40%; margin:0 auto; margin-top:10%;">
        <table width="100%">
            <tr class="topHandle">
                <td colspan="2" align="center" runat="server" id="tdCaption" style="background-color:#01609F">
                    <h3><asp:Label ID="lblCaption" runat="server" ForeColor="White"></asp:Label></h3>
                </td>
            </tr>
            <tr>
                <td style="width: 60px" valign="middle" align="center">
                    <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Images/Botones/Info-48x48.png" />
                </td>
                <td valign="middle" align="left">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button ID="btnOk2" runat="server" Text="Aceptar" OnClick="btnOk2_Click" CssClass="btn btn-success" />
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
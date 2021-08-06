<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="Autos_SCC.frmLogin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Autos el ángel de puebla</title>
    <link rel="stylesheet" type="text/css" href="Styles/styles_login.css" media="screen" />
</head>
<body>
    <script type="text/javascript">

        function Redirecciona(cad) {
            location.href = cad;
        }

        function fnCheckValue() {
            var myVal = document.getElementById('<%=txtPassword.ClientID%>').value;
            var myUsu = document.getElementById('<%=txtUsuario.ClientID%>').value;

            if (myUsu = "") {
                alert("El Usuario es requerido");
                return false;
            }
            else if (myVal == "") {
                alert("La contraseña es requerida");
                return false;
            }
            else {
                return true;
            }
        }

        function EnterEvent(e) {

            if (e.keyCode == 13) {
                if (fnCheckValue()) {
                    var obj = document.getElementById('<%=btnEntrar.ClientID%>');

                    if (obj) {
                        obj.click();
                    }
                }
                else {
                    return false;
                }
            }
        }

	</script>

    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>

        <asp:UpdatePanel ID="upaPrincipal" runat="server">
            <ContentTemplate>

                <div id="logo_ale" class="logo">
	                <asp:Image ID="imLogo" runat="server" ImageUrl="~/Images/Fondos/logo.png" width="250" height="56" />
                </div>

                <div id="bg" class="dvCentro">
                    <div id="log">
                        <p class="titulo bienvenido">Bienvenido</p>
                        <p class="userLabel">Usuario:</p>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="textUser" ClientIDMode="Static"></asp:TextBox>

                        <p class="passLabel">Contrase&ntilde;a:</p>
                        <asp:TextBox ID="txtPassword" runat="server" MaxLength="20" TextMode="Password" ClientIDMode="Static" onkeypress="return EnterEvent(event);"
                            CssClass="textPassword"></asp:TextBox>

                        <p class="rememberLabel">&iquest;Olvidaste tu Contrase&ntilde;a?</p>
                        <asp:Button ID="btnEntrar" runat="server" Text="Entrar" CssClass="boton botLog" ClientIDMode="Static" OnClick="btnEntrar_Click" />
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>        
    </form>
</body>
</html>

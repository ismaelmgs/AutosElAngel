<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Header.aspx.cs" Inherits="Autos_SCC.Views.Header" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Styles/StyleGeneral.css" rel="Stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">
			
			.text_blank {
	            font-family: Arial, Helvetica, sans-serif;
	            font-size: 14px;
	            color:#FFFFFF;
	            letter-spacing: 1pt;
            }
            
            .text_blank_fecha {
	            font-family: Arial, Helvetica, sans-serif;
	            font-size: 11px;
	            color:#FFFFFF;
	            letter-spacing: 1pt;
            }
			
			* {
				margin:.5px;
				padding:0px;
			}
			
			#header {
				margin:auto;
				width:700px;
				font-family:Arial, Helvetica, sans-serif;
			}
			
			ul, ol {
				list-style:none;
			}
			
			.nav > li {
				float:left;
			}
			
			.nav li a {
				background-color:#48489A;
				color:#fff;
				text-decoration:none;
				padding:10px 12px;
				display:block;
			}
			
			.nav li a:hover {
				background-color:#434343;
			}
			
			.nav li ul {
				display:none;
				position:absolute;
				min-width:140px;
			}
			
			.nav li:hover > ul {
				display:block;
			}
			
			.nav li ul li {
				position:relative;
			}
			
			.nav li ul li ul {
				right:-140px;
				top:0px;
			}
			
		</style>

        <script type="text/javascript" language="javascript">
            function Redirecciona(sPath) 
            {
                window.parent.basefrm.location.href = "'" + sPath + "'";
            }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="165" align="center" valign="bottom" background="../Images/bg_principaltop.jpg" style="width: 1277px;">
                        <table width="930" border="0" cellspacing="0" cellpadding="0">
                            <tr class="text_blank">
                                <td class="text_blank" style="font-weight: bold; font-size: 10pt;" align="left">
                                    Bienvenido:
                                    <asp:Label ID="lblNomUsuario" runat="server" Font-Bold="True" ToolTip="Usuario Micronegocio Azteca"
                                         ></asp:Label>
                                </td>
                                <td class="text_blank" style="font-size: 10pt" align="center">
                                    <asp:Label ID="lblPerfil" runat="server" Font-Bold="True" ForeColor="White" ToolTip="Perfil de Usuario"  ></asp:Label>; &nbsp;
                                    <asp:Label ID="lblNumSuc" runat="server" Font-Bold="True" ToolTip="Número y Nombre de la Sucursal"  ></asp:Label>
                                </td>
                                <td align="right" valign="bottom" class="text_blank_fecha">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:Label ID="lblHora" runat="server" Font-Size="Smaller" Font-Bold="True" Width="200px"
                                                ToolTip="Fecha del Sistema" Height="20px"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        <table width="930" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="height: 15px;" colspan="2">
                                    <img src="../Images/top_sup.jpg" width="927" height="13" alt="" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2">
                                    <table width="927" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="169">
                                                <img src="../Images/log_top_micronegocio.jpg" width="346" height="61" />
                                            </td>
                                            <td width="581" height="60" align="right" valign="bottom" background="../Images/bg_top2.jpg">
                                                <table width="120" border="0" cellspacing="0" cellpadding="0" style="vertical-align:top">
                                                    <tr>
                                                        <td width="16">
                                                            <img src="../Images/close.png" width="15" height="15" />
                                                        </td>
                                                        <td width="61" height="20" class="text_path_over">
                                                            <asp:LinkButton ID="lknEndSession" runat="server" Font-Bold="True" Font-Size="X-Small"
                                                                Font-Underline="False" ForeColor="Purple" BorderColor="White" BorderStyle="None"
                                                                ToolTip="Cerrar Sesión">&nbsp;Cerrar Sesión&nbsp;</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="42" colspan="2" align="left" valign="top" background="../Images/bg_menu_gris.jpg">                                                
                                                <div id="header">
			                                        <ul class="nav">
				                                        <li><a href="">Inicio</a></li>
				                                        <li><a href="">Documentos</a>
					                                        <ul>
						                                        <li><a href="">Cotizador</a></li>
						                                        <li><a href="">Datos Clientes</a></li>
						                                        <li><a href="">Formalización</a></li>						                                        
					                                        </ul>
				                                        </li>
				                                        <li><a href="">Control de Cartera</a>
					                                        <ul>
						                                        <li><a href="">Abonos</a></li>
						                                        <li><a href="">Cobranza</a></li>
					                                        </ul>
				                                        </li>
				                                        <li><a href="">Reportes</a>
                                                            <ul>
                                                                <li><a href="">Sucursal</a></li>
                                                            </ul>
                                                        </li>
                                                        <li><a href="">Catalogos</a>
                                                            <ul>
                                                                <li><a onclick="Redirecciona(../Views/Catalogos/frmAutos.aspx);">Autos</a></li>
                                                                <li><a href="">Marcas</a></li>
                                                                <li><a href="">Tipos de Autos</a></li>
                                                                <li><a href="">Reporte por sucursal</a></li>
                                                                <li><a href="">Versiones</a></li>
                                                            </ul>
                                                        </li>
			                                        </ul>
		                                        </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="width: 1277px">
                        <table width="950" border="0" align="center" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="12" valign="top" background="../Images/bg_prinizq.jpg">
                                    <img src="../Images/bg_prinizq.jpg" width="12" height="1" /></td>
                                <td width="929" bgcolor="#FFFFFF">
                                    <table class="tablas_modulo">
                                        <%--<iframe name="pagvacia" src="pagvacia.htm" width="900" height="423" align="center"></iframe>--%>
                                        <iframe name="pagvacia" src="../pagvacia.htm" frameborder="0" framespacing="0" border="0"
                                            marginheight="0" marginwidth="0" width="910" height="570" align="center" scrolling="yes">
                                            El navegador de Internet no conoce la etiqueta iframe.
                                        </iframe>
                                    </table>
                                </td>
                                <td valign="top" background="../Images/bg_prinder.jpg" style="width: 1px">
                                    <img src="../Images/bg_prinder.jpg" width="12" height="1" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>

<%--<body>
    <form id="form1" runat="server">
        <div>
            <div id="EndSession" style="top: 4px; right: 20px; position:absolute;">
                <asp:LinkButton ID="lknEndSession" runat="server" Font-Bold="True"   Font-Underline="False" ForeColor="#006F4E" BorderColor="White" BorderStyle="Ridge" OnClick="lknEndSession_Click" ToolTip="Cerrar Sesión">&nbsp;Cerrar Sesión&nbsp;</asp:LinkButton>
            </div>

            <table style="table-layout: auto; border-collapse: collapse;">
                <tr>
                    <td style="width:100%" >
                        <div id="bienvenida1" style="background:url(../Images/Fondos/Banner.png)">
                            <asp:Label ID="lblNomUsuario" runat="server" Font-Bold="True" CssClass="EtiquetasDerecha" ToolTip="Usuario Autos el angel"></asp:Label>
                            <asp:Label ID="lblBienvenido" runat="server" Text="BIENVENIDO: " CssClass="EtiquetasDerecha" ToolTip="Bienvenida"></asp:Label>
                            <br />
                            <asp:Label ID="lblPerfil" runat="server" CssClass="EtiquetasDerecha" ToolTip="Perfil de Usuario"></asp:Label>
                            <br />
                            <asp:Label ID="lblSuc" runat="server" Text="SUCURSAL: " CssClass="EtiquetasDerecha"></asp:Label>
                            <asp:Label ID="lblNumSuc" runat="server" Font-Bold="True" ToolTip="Numero y Nombre de la Sucursal" ></asp:Label>
                        </div>
                    </td>
                    <td>
                        <img class="derecha" src="../Images/Fondos/logo-chico.png" alt="Autos el angel de puebla" />
                    </td>
                </tr>
            </table>
        </div>        
    </form>
</body>--%>
</html>

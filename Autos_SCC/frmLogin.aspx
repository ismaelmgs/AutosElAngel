<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="Autos_SCC.frmLogin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Autos el ángel de puebla</title>
    <!-- HTML5 Shim and Respond.js IE10 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 10]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
      <![endif]-->
      <!-- Meta -->
      <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui">
      <meta http-equiv="X-UA-Compatible" content="IE=edge" />
      <link rel="stylesheet" type="text/css" href="Styles/styles_login.css" media="screen" />
      <link rel="icon" href="assets/images/favicon.ico" type="image/x-icon">
      <!-- Google font-->     
      <link href="https://fonts.googleapis.com/css?family=Roboto:400,500" rel="stylesheet">
      <!-- Required Fremwork -->
      <link rel="stylesheet" type="text/css" href="assets/css/bootstrap/css/bootstrap.min.css">
      <!-- waves.css -->
      <link rel="stylesheet" href="assets/pages/waves/css/waves.min.css" type="text/css" media="all">
      <!-- themify-icons line icon -->
      <link rel="stylesheet" type="text/css" href="assets/icon/themify-icons/themify-icons.css">
      <!-- ico font -->
      <link rel="stylesheet" type="text/css" href="assets/icon/icofont/css/icofont.css">
      <!-- Font Awesome -->
      <link rel="stylesheet" type="text/css" href="assets/icon/font-awesome/css/font-awesome.min.css">
      <!-- Style.css -->
      <link rel="stylesheet" type="text/css" href="assets/css/style.css">
</head>
<body style="height:100vh; background-color:#ffffff;background-image: url('../images/bkg_login.jpg');
  background-repeat: no-repeat;
  width:100%;
  position: relative;
  margin-left: auto;
  margin-right: auto;">
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
                <div class="row">
                    <div class="col-md-3">
                        &nbsp;
                    </div>
                    <div class="col-md-2" style="background-color:#ffffff;-webkit-box-shadow: 3px 3px 5px 0px rgba(0,0,0,0.21);
-moz-box-shadow: 3px 3px 5px 0px rgba(0,0,0,0.21);
box-shadow: 3px 3px 5px 0px rgba(0,0,0,0.21); height:100vh; border-radius:20px;">
                        <div class="row">
                            <div class="col-md-12">
                                <hr /><br />
                                <asp:Image ID="imLogo" runat="server" ImageUrl="~/Images/Fondos/logo.png" width="100%" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12" style="text-align:center;">
                                <h3>Bienvenido</h3><br />
                                <lavel>Usuario:</lavel>
                            <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div><br />
                        <div class="row" style="text-align:center;">
                            <div class="col-md-12">
                                <lavel>Contrase&ntilde;a:</lavel>
                            <asp:TextBox ID="txtPassword" runat="server" MaxLength="20" TextMode="Password" ClientIDMode="Static" onkeypress="return EnterEvent(event);"
                                CssClass="form-control"></asp:TextBox>
                            </div>
                        </div><br />
                         <div class="row" style="text-align:center;">
                            <div class="col-md-12">
                                <asp:Button ID="btnEntrar" runat="server" Text="Entrar" CssClass="btn btn-primary btn-md btn-block waves-effect text-center m-b-20" ClientIDMode="Static" OnClick="btnEntrar_Click" />
                            </div>
                         </div>
                        <%--<div class="row" style="text-align:center;">
                            <div class="col-md-12">
                                <lavel>&iquest;Olvidaste tu Contrase&ntilde;a?</lavel>
                            </div>
                        </div>  --%> 
                        <div class="row">
                    <div class="col-md-12" style="text-align:center;position:absolute; left:0px; right:0px; bottom:0px; font-size:10px;">
                            <script>
                                document.write(new Date().getFullYear())
                            </script>© Autos el Ángel Puebla
                        <hr />
                    </div>
                </div>
                    </div>
                    <div class="col-md-7">
                        &nbsp;
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>        
    </form>
    <!-- Warning Section Ends -->
<!-- Required Jquery -->
    <script type="text/javascript" src="assets/js/jquery/jquery.min.js"></script>     <script type="text/javascript" src="assets/js/jquery-ui/jquery-ui.min.js "></script>     <script type="text/javascript" src="assets/js/popper.js/popper.min.js"></script>     <script type="text/javascript" src="assets/js/bootstrap/js/bootstrap.min.js "></script>
<!-- waves js -->
<script src="assets/pages/waves/js/waves.min.js"></script>
<!-- jquery slimscroll js -->
<script type="text/javascript" src="assets/js/jquery-slimscroll/jquery.slimscroll.js "></script>
<!-- modernizr js -->
    <script type="text/javascript" src="assets/js/SmoothScroll.js"></script>     <script src="assets/js/jquery.mCustomScrollbar.concat.min.js "></script>
<!-- i18next.min.js -->
<script type="text/javascript" src="bower_components/i18next/js/i18next.min.js"></script>
<script type="text/javascript" src="bower_components/i18next-xhr-backend/js/i18nextXHRBackend.min.js"></script>
<script type="text/javascript" src="bower_components/i18next-browser-languagedetector/js/i18nextBrowserLanguageDetector.min.js"></script>
<script type="text/javascript" src="bower_components/jquery-i18next/js/jquery-i18next.min.js"></script>
<script type="text/javascript" src="assets/js/common-pages.js"></script>
</body>
</html>

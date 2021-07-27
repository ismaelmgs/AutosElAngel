<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="Autos_SCC.Views.Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/StyleGeneral.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        var sw = 0;

        function MuestraOcult(s) {
            var rcFile = new Array();
            rcFile = s.split("/");

            var imagen = rcFile[rcFile.length - 1];
            var imgObj = document.getElementById('cntMosOcul');

            if (imagen == 'Ocultar.ico' && sw == 0) {
                parent.document.getElementById('menu').cols = "18.5,*";
                imgObj.src = "Images/Mostrar.ico";
                imgObj.alt = "Mostrar Menú";
                sw = 1;
            }
            else if (imagen == 'Mostrar.ico' && sw == 1) {
                parent.document.getElementById('menu').cols = "223,* ";
                imgObj.src = "Images/Ocultar.ico";
                imgObj.alt = "Ocultar Menú";
                sw = 0;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="border-width:0px; height:900px; width:100%">
            <tr style="height:100%">
                <td style="height:100%; width:18px;"> 
                    <div id="EtiqMenu" style="width:18px; height:100%; background-color:#4F2683;">    
                        <img id="cntMosOcul" src="../Images/Ocultar.ico" alt="Ocultar Menú" width="17" height="17"
                            onclick="MuestraOcult(this.src);" style="cursor:hand"/>                        
                    </div>             
                </td>
                <td style="height:100%">
                    <div id="Div1" style="width:100%; height:100%; background-color:#4F2683">                         
                        <div id="ContMenu" style="width:203px; height:100%; background-color:White; margin-left:0px;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">                                
                                <ContentTemplate>
                                    <asp:TreeView ID="trMenu" runat="server" onselectednodechanged="trMenu_SelectedNodeChanged">
                                        <LeafNodeStyle CssClass="leafNode" />
                                        <NodeStyle CssClass="treeNode" />
                                        <RootNodeStyle CssClass="rootNode" />
                                        <SelectedNodeStyle CssClass="selectNode" />
                                    </asp:TreeView>
                                </ContentTemplate>
                            </asp:UpdatePanel>                                
                        </div>                    
                    </div>                        
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
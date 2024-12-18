<%@ Page Language="vb" src="../include/sysadm.aspx.vb" Inherits="sysadm" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GG-Menu</title>
    <link href="include/css/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>

<body style="padding-right: 0px; padding-left: 0px; margin-left: 0px; margin-right: 0px">

    <form id="form1" runat="server"  >

        <div style="position:absolute; right:5px; top:5px; width:650px; padding:0px 0px 0px 0px; display:block; height:100px">
          
           <div id="navigationContainer" class="navigationContainer">
            <div class="navigationButton"><a href="../login.aspx">
		<asp:Image runat="server" ID="imgLogOff" ImageUrl="images/thumbs/LogOff.gif" ToolTip="Log Off"/></a>
            </div>
            
          </div>
          
          <table border="0" cellspacing="0" cellpadding="0" width="100%" >
             <tr>
               <td align="right" style="height: 25px">
                
               </td>
             </tr>
           </table>
         </div> 
                       
         <div style="height:1000px; width:100%; position:absolute; top:90px; z-index:500" >
   
           <igtab:UltraWebTab   ID="UltraWebTab1" runat="server" Width="100%"  Height="100%" BorderStyle="None" 
            Font-Names="Tahoma" Font-Size="8pt" Font-Bold="True"
            ThreeDEffect="False"  TabOrientation="TopRight" SelectedTab="0" >
            <DisabledTabStyle BackColor="Silver">
            </DisabledTabStyle>
            <DefaultTabStyle Height="29px">
            </DefaultTabStyle>
            <HoverTabStyle CssClass="ContentTabHover" ></HoverTabStyle>
            <RoundedImage LeftSideWidth="6" RightSideWidth="6" SelectedImage="./images/thumbs/ig_tab_winXP1.gif"
                NormalImage="./images/thumbs/ig_tab_winXP3.gif" HoverImage="./images/thumbs/ig_tab_winXP2.gif"
                FillStyle="LeftMergedWithCenter"></RoundedImage>
            <SelectedTabStyle CssClass="ContentTabSelected">
            </SelectedTabStyle>
            <Tabs>
                <igtab:Tab Key="trx" Text="Setup" Tooltip="Transaction Application">
                    <ContentPane BorderStyle="None" TargetUrl="sysadm_menu.aspx"  Scrollable="Hidden">
                    </ContentPane>
                    
                </igtab:Tab>
                
            </Tabs>
            </igtab:UltraWebTab>
        </div>
        
    <div class="BackgroundTopCornerSA"></div>
    </form>
</body>
</html>


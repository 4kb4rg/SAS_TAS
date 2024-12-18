<%@ Page Language="vb" src="../include/menudetail1.aspx.vb" Inherits="menudetail1" %>
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
<body>
    <form id="form1" runat="server">

        <div style="position:absolute; right:5px; top:5px; width:650px; padding:0px 0px 0px 0px; display:block">
          
           <div id="navigationContainer" class="navigationContainer">
            <div class="navigationButton"><asp:Image runat="server" ID="image7" ImageUrl="images/thumbs/navthumb.gif" /></div>
            <div class="navigationButton styleButton"><asp:Image ID="image8" runat="server" ImageUrl="images/thumbs/themethumb.gif" ToolTip="Change The Current Style" /></div>
            <div class="navigationButton styleButton2"><asp:Image ID="image9" runat="server" ImageUrl="images/thumbs/themethumb.gif" ToolTip="Change The Current Style" /></div>
          </div>
          
          <table border="0" cellspacing="0" cellpadding="0" width="100%" >
             <tr>
               <td align="right" style="height: 25px">
                 <asp:Label id="lblUser" runat="server" cssclass="login"  />
               </td>
             </tr>
           </table>
         </div>
                       
        <div  style="height:500px" class="view2" >
   
        <igtab:UltraWebTab   ID="UltraWebTab1" runat="server" Width="98%"  Height="100%" BorderStyle="None" 
            BorderWidth="0px" BorderColor="#CCCCCC" Font-Names="Tahoma" Font-Size="8pt" Font-Bold="True"
            ThreeDEffect="False" CssClass="ContentViewer" TabOrientation="TopRight" >
            <DisabledTabStyle BackColor="Silver">
            </DisabledTabStyle>
            <DefaultTabStyle Height="29px" Font-Size="8pt" Font-Names="Microsoft Sans Serif"
                BorderStyle="None" BorderColor="Silver" CssClass="ContentTab" BackColor="#FEFCFD">
            </DefaultTabStyle>
            <HoverTabStyle CssClass="ContentTabHover" ></HoverTabStyle>
            <RoundedImage LeftSideWidth="6" RightSideWidth="6" SelectedImage="./images/thumbs/ig_tab_winXP1.gif"
                NormalImage="./images/thumbs/ig_tab_winXP3.gif" HoverImage="./images/thumbs/ig_tab_winXP2.gif"
                FillStyle="LeftMergedWithCenter"></RoundedImage>
            <SelectedTabStyle CssClass="ContentTabSelected">
            </SelectedTabStyle>
            <Tabs>
                <igtab:Tab Key="trx" Text="Transaction" Tooltip="Transaction Application">
                    <ContentPane BorderStyle="None" TargetUrl="menudetail1_trx.aspx"  Scrollable="hidden">
                    </ContentPane>
                    <ContentTemplate>
                        <br />
                    </ContentTemplate>
                </igtab:Tab>
                <igtab:TabSeparator>
                </igtab:TabSeparator>
                <igtab:Tab Key="stp" Text="Setup" Tooltip="Setup Application">
                    <ContentPane BorderStyle="None" TargetUrl="menudetail1_stp.aspx" Scrollable="Hidden">
                    </ContentPane>
                </igtab:Tab>
                <igtab:TabSeparator>
                </igtab:TabSeparator>
                <igtab:Tab Key="rpt" Text="Reports" Tooltip="Reports Application">
                    <ContentPane BorderStyle="None" TargetUrl="menudetail1_rpt.aspx" Scrollable="Hidden">
                    </ContentPane>
                </igtab:Tab>
                 <igtab:TabSeparator>
                </igtab:TabSeparator>
                <igtab:Tab key="mth" Text="Month End" Tooltip="Month End Processing">
                    <ContentPane BorderStyle="None" TargetUrl="menudetail1_mth.aspx" Scrollable="Hidden">
                    </ContentPane>
                </igtab:Tab>
            </Tabs>
        </igtab:UltraWebTab>
       
      
       
        </div>

     
    <div class="BackgroundTopCorner">
    </div>
    </form>
</body>
</html>


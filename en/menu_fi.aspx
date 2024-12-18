<%@ Page Language="vb" src="../include/menu_fi.aspx.vb" Inherits="menu_fi" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GG-Menu</title>
    <link href="include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .login
        {
            text-align: right;
        }
        .style1
        {
            height: 16px;
            text-align: right;
        }
        .style2
        {
            width: 100%;
        }
    </style>
</head>

<body style="padding-right: 0px; padding-left: 0px; margin-left: 0px; margin-right: 0px">

  <form id="form1" runat="server">
 
<table cellpadding="0" cellspacing="0" style="width: 100%">
	<tr>
		<td class="cell-left" valign="middle">
		<table cellpadding="1" cellspacing="1" style="width: 254px">
			<tr>
				<td class="cell-right" style="height: 110px" valign="Middle">
				<img height="65" src="../images/lgsubmenu.png" width="234" />
				</td>
			</tr>
			 
		</table>

		</td>

		<td class="cell-left" valign="top">	

		</td>
		<td class="main-header" style="width: 100%" valign="top">
		<table cellpadding="0" cellspacing="0" style="width: 100%; height: 0px;">
			<tr>
				<td class="style1" valign="bottom" >
               <asp:Label id="lblUser" runat="server" cssclass="FontUserName"/>&nbsp;&nbsp; &nbsp;<a href="menu.aspx"><asp:Image ID="Image3" runat="server" ImageUrl="images/thumbs/bthome.png" ToolTip="Back To Main Menu" CssClass="button" /></a>
                            <a href="system/user/setlocation.aspx">
                <asp:Image ID="Image2" runat="server" ImageUrl="images/thumbs/btlogin.png" ToolTip="Change Active Location" CssClass="button"  /></a>
                            <a href="../login.aspx">
		 <asp:Image runat="server" ID="Image1" ImageUrl="images/thumbs/btlogout.png" ToolTip="Log Off" CssClass="button" /></a>
                     &nbsp;</td>
			</tr>
			
						<tr>
				
				<td class="style2" valign="center"><img height="35px" src="images/iconmm.png" width="40px" /><span class="font14">F I N A N C I A L &nbsp; M A N A G E M E N T</span></td>
				<td valign="top">
				    &nbsp;</td>
				<td valign="top">
					<table cellpadding="0" cellspacing="0" style="width: 20px">
						<tr>
							<td>&nbsp;</td>
						</tr>
 					
					</table>
				</td>
			</tr>
		</table>
		
		<div style="padding: 10px 0 6px 0">
 		</div>
			 
			</td>
				<td>
		            &nbsp;</td>
			</tr>
		</table>

        <div style="width:100%; height:1000px; position:absolute; top:40px; z-index:500; left: 0px;" >
          <igtab:UltraWebTab   ID="UltraWebTab1" runat="server" Width="100%"  Height="100%" BorderStyle="None" 
            Font-Names="Tahoma" Font-Size="8pt" Font-Bold="True"
            ThreeDEffect="False"  TabOrientation="TopRight" SelectedTab="0" >
            <DisabledTabStyle BackColor="Silver">
            </DisabledTabStyle>
            <DefaultTabStyle Height="54px">
            </DefaultTabStyle>
            <HoverTabStyle CssClass="button"></HoverTabStyle>
            <RoundedImage LeftSideWidth="0" RightSideWidth="0" SelectedImage="./images/thumbs/ig_tab_winXP1.gif"
                NormalImage="./images/thumbs/ig_tab_winXP3.gif" HoverImage="./images/thumbs/ig_tab_winXP2.gif"
                FillStyle="LeftMergedWithCenter"></RoundedImage>
            <SelectedTabStyle CssClass="button">
            </SelectedTabStyle>
            <Tabs>
                <igtab:Tab Key="trx" DefaultImage="../images/icon1.png"  Tooltip="Transaction Application">
                    <ContentPane BorderStyle="None"   TargetUrl="menu_fitrx.aspx"  Scrollable="Hidden">
                    </ContentPane>
                    
                </igtab:Tab>
                <igtab:TabSeparator>
                </igtab:TabSeparator>
                <igtab:Tab Key="stp" DefaultImage="../images/icon2.png"  Tooltip="Setup Application">
                    <ContentPane BorderStyle="None" TargetUrl="menu_fistp.aspx" Scrollable="Hidden">
                    </ContentPane>
 		    
                </igtab:Tab>
                <igtab:TabSeparator>
                </igtab:TabSeparator>
                <igtab:Tab Key="rpt" DefaultImage="../images/icon3.png" Tooltip="Reports Application">
                    <ContentPane BorderStyle="None" TargetUrl="menu_firpt.aspx" Scrollable="Hidden">
                    </ContentPane>
                    
                </igtab:Tab>
                 <igtab:TabSeparator>
                </igtab:TabSeparator>
                <igtab:Tab Key="rpt" DefaultImage="../images/icon4.png" Tooltip="Data Transfer Application">
                    <ContentPane BorderStyle="None" TargetUrl="menu_fidt.aspx" Scrollable="Hidden">
                    </ContentPane>
                    
                </igtab:Tab>
                 <igtab:TabSeparator>
                </igtab:TabSeparator>
                <igtab:Tab key="mth" DefaultImage="../images/icon5.png"  Tooltip="Month End Processing">
                    <ContentPane BorderStyle="None" TargetUrl="menu_fimth.aspx" Scrollable="Hidden">
                    </ContentPane>
                    
                </igtab:Tab>
            </Tabs>
            </igtab:UltraWebTab>
        </div>
 
    </form>
</body>

</html>


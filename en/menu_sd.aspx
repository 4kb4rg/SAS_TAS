<%@ Page Language="vb" src="../include/menu_sd.aspx.vb" Inherits="menu_sd" %>
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

  <form id="form1" runat="server"  >
 

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
				
				<td class="style2" valign="center"><img height="35px" src="images/iconmm.png" width="40px" /><span class="font14">SALES &  DISTRIBUTION MANAGEMENT</span></td>
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
                    <ContentPane BorderStyle="None"   TargetUrl="menu_sdtrx.aspx"  Scrollable="Hidden">
                    </ContentPane>
                    
                </igtab:Tab>
                <igtab:TabSeparator>
                </igtab:TabSeparator>
                <igtab:Tab Key="stp" DefaultImage="../images/icon2.png"  Tooltip="Setup Application">
                    <ContentPane BorderStyle="None" TargetUrl="menu_sdstp.aspx" Scrollable="Hidden">
                    </ContentPane>
 		    
                </igtab:Tab>
                <igtab:TabSeparator>
                </igtab:TabSeparator>
                <igtab:Tab Key="rpt" DefaultImage="../images/icon3.png" Tooltip="Reports Application">
                    <ContentPane BorderStyle="None" TargetUrl="menu_sdrpt.aspx" Scrollable="Hidden">
                    </ContentPane>
                    
                </igtab:Tab>
   
  
            </Tabs>
            </igtab:UltraWebTab>
        </div>
 
    </form>
</body>


<%--<body style="padding-right: 0px; padding-left: 0px; margin-left: 0px; margin-right: 0px">

    <form id="form1" runat="server">
   <table cellpadding="0" cellspacing="0" style="width: 100%">
	<tr>
		<td class="cell-left" valign="top">
		<table cellpadding="0" cellspacing="0" style="width: 254px">
			<tr>
				<td class="cell-right" style="height: 110px" valign="bottom">
				<img height="63" src="../images/lgsubmenu.png" width="234" />
				</td>
			</tr>
			 
		</table>

		</td>

		<td class="cell-left" valign="top">
		<table cellpadding="0" cellspacing="0" style="width: 20px">
			<tr>
				<td>&nbsp;</td>
			</tr>
		</table>
		</td>
		<td class="sub-bg" style="width: 100%" valign="top">
		<table cellpadding="0" cellspacing="0" style="width: 100%">
			<tr>
				<td class="cell-right" style="height: 40px" valign="bottom">
				<table cellpadding="0" cellspacing="0" style="width: 100%">
					<tr>
						<td style="width: 100%" class="cell-right"><span class="font9"><strong><asp:Label id="lblUser" runat="server" cssclass="login"  /></strong></span></td>
						<td valign="bottom">
							<table align="right" cellpadding="0" cellspacing="0" style="width: 120px">
								<tr>
									<td>
										<div class="dropdown">	
											<img height="27" src="images/btlogin.png" width="23" class="button" />	
											<div class="dropdown-content">
											
												<table cellpadding="0" cellspacing="0" style="width: 455px; height: 20px">
													<tr>
													
														<td class="cell-left" valign="middle" style="width: 210px; height:20px">
															<select class="font9" name="milloc" onchange="javascript:handleSelect(this)">
																<option value="seloct.html">-- SELECT MILL LOCATION --</option>
										  						<option value="locmdn.html">Head Office Medan</option>
										  						<option value="seloct.html">Kebun Pasir Jenjang</option>
										  						<option value="seloct.html">Pabrik Muko Muko</option>
										  						<option value="seloct.html">Kebun Silaut</option>
															</select>
														</td>
														
														<td class="cell-left" valign="middle" style="width: 55px; height:20px">
															<span class="font9">Periode :</span>
														</td>
														
														<td class="cell-left" valign="middle" style="width: 85px; height:20px">
															<select name="D1" class="font9">
										  						<option>January</option>
										  						<option>February</option>
										  						<option>March</option>
										  						<option>April</option>
										  						<option>May</option>
										  						<option>June</option>
										  						<option>July</option>
										  						<option>August</option>
										  						<option>September</option>
										  						<option>October</option>
										  						<option>November</option>
										  						<option>December</option>
															</select>
														</td>
														<td class="cell-left" valign="middle" style="width: 40px; height:20px">
															<select name="D2" class="font9">
										  						<option>2016</option>
										  						<option>2017</option>
										  						<option>2018</option>
										  						<option>2019</option>
										  						<option>2020</option>
															</select>
														</td>
														
														<td class="cell-right" valign="middle" style="width: 65px; height:20px">
															<input class="button-form" type="submit" value="APPLY" />
														</td>
														
													</tr>
												</table>
													
											</div>
										</div>
										
										<a href="mainmenu.html">
											<img height="27" src="images/bthome.png" width="27" class="button" />
										</a>
										<a href="default.html">
											<img height="27" src="images/btlogout.png" width="23" class="button" />
										</a>&nbsp;&nbsp;&nbsp;&nbsp;
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
				</td>
			</tr>
		</table>
		
		<div style="padding: 10px 0 6px 0">
		<table style="width: 100%; " cellpadding="0" cellspacing="0">
			<tr>
				<td valign="bottom">
				<table cellpadding="0" cellspacing="0" style="width: 54px; height: 54px">
					<tr>
						<td class="cell-center" valign="bottom">
						<img height="54px" src="images/iconmm.png" width="54px" /></td>
					</tr>
				</table>
				</td>
				<td style="width: 100%"><span class="font14">&nbsp;&nbsp;Sales & Distribution</span></td>
				<td valign="bottom">
				<table cellpadding="0" cellspacing="0" style="width: 290px; height: 54px" align="right">
					<tr>
						<td style="width: 54px">
						<a href="menu_mmtrx.aspx"><img height="54px" src="images/icon1.png" width="54px" class="button" /></a></td>
						<td>&nbsp;</td>
						<td style="width: 54px">
						<img height="54px" src="images/icon2.png" width="54px" class="button" /></td>
						<td>&nbsp;</td>
						<td style="width: 54px">
						<img height="54px" src="images/icon3.png" width="54px" class="button" /></td>
						<td>&nbsp;</td>
						<td style="width: 54px">
						<img height="54px" src="images/icon4.png" width="54px" class="button" /></td>
						<td>&nbsp;</td>
						<td style="width: 54px">
						<img height="54px" src="images/icon5.png" width="54px" class="button" /></td>
					</tr>
				</table>
				</td>
				<td valign="bottom">
					<table cellpadding="0" cellspacing="0" style="width: 20px">
						<tr>
							<td>&nbsp;</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		</div>
		
		 
				</div>
				</td>
				<td>
		<table cellpadding="0" cellspacing="0" style="width: 20px">
			<tr>
				<td>&nbsp;</td>
			</tr>
		</table>
				</td>
			</tr>
		</table>
 

   <div style="position:absolute; right:5px; top:5px; width:650px; padding:0px 0px 0px 0px; display:block; height:100px">
          
           <div id="Div1" class="navigationContainer">
            <div class="navigationButton"><a href="../login.aspx">
		<asp:Image runat="server" ID="Image1" ImageUrl="images/thumbs/LogOff.gif" ToolTip="Log Off"/></a>
            </div>
            <div class="navigationButton styleButton"><a href="system/user/setlocation.aspx">
                <asp:Image ID="Image2" runat="server" ImageUrl="images/thumbs/Loc.gif" ToolTip="Change Active Location" /></a>
            </div>
            <div class="navigationButton styleButton2"><a href="menu.aspx">
                <asp:Image ID="Image3" runat="server" ImageUrl="images/thumbs/Home.gif" ToolTip="Back To Main Menu" /></a>
            </div>
          </div>
          
          
         </div>
     
          <div style="position:absolute; right:5px; top:5px; width:650px; padding:0px 0px 0px 0px; display:block; height:100px">
          
           <div id="navigationContainer" class="navigationContainer">
            <div class="navigationButton"><a href="../login.aspx">
		<asp:Image runat="server" ID="imgLogOff" ImageUrl="images/thumbs/LogOff.gif" ToolTip="Log Off"/></a>
            </div>
            <div class="navigationButton styleButton"><a href="system/user/setlocation.aspx">
                <asp:Image ID="imgLoc" runat="server" ImageUrl="images/thumbs/Loc.gif" ToolTip="Change Active Location" /></a>
            </div>
            <div class="navigationButton styleButton2"><a href="menu.aspx">
                <asp:Image ID="imgHome" runat="server" ImageUrl="images/thumbs/Home.gif" ToolTip="Back To Main Menu" /></a>
            </div>
          </div>
          
          
         </div>
                       
         <div style="width:100%; height:1000px; position:absolute; top:90px; z-index:500" >
   
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
                <igtab:Tab Key="trx" Text="Transaction" Tooltip="Transaction Application">
                    <ContentPane BorderStyle="None" TargetUrl="menu_sdtrx.aspx"  Scrollable="Hidden">
                    </ContentPane>
                    
                </igtab:Tab>
                <igtab:TabSeparator>
                </igtab:TabSeparator>
                <igtab:Tab Key="stp" Text="Setup" Tooltip="Setup Application">
                    <ContentPane BorderStyle="None" TargetUrl="menu_sdstp.aspx" Scrollable="Hidden">
                    </ContentPane>
 		    
                </igtab:Tab>
                <igtab:TabSeparator>
                </igtab:TabSeparator>
                <igtab:Tab Key="rpt" Text="Reports" Tooltip="Reports Application">
                    <ContentPane BorderStyle="None" TargetUrl="menu_sdrpt.aspx" Scrollable="Hidden">
                    </ContentPane>
                </igtab:Tab>  
               
            </Tabs>
            </igtab:UltraWebTab>
        </div>
        
    <div class="BackgroundTopCornerSD">
<%--
        <div style="position:absolute; right:5px; top:5px; width:650px; padding:0px 0px 0px 0px; display:block; height:100px">
          
           <div id="navigationContainer" class="navigationContainer">
            <div class="navigationButton"><a href="../login.aspx">
		<asp:Image runat="server" ID="imgLogOff" ImageUrl="images/thumbs/LogOff.gif" ToolTip="Log Off"/></a>
            </div>
            <div class="navigationButton styleButton"><a href="system/user/setlocation.aspx">
                <asp:Image ID="imgLoc" runat="server" ImageUrl="images/thumbs/Loc.gif" ToolTip="Change Active Location" /></a>
            </div>
            <div class="navigationButton styleButton2"><a href="menu.aspx">
                <asp:Image ID="imgHome" runat="server" ImageUrl="images/thumbs/Home.gif" ToolTip="Back To Main Menu" /></a>
            </div>
          </div>
          
          <table border="0" cellspacing="0" cellpadding="0" width="100%" >
             <tr>
               <td align="right" style="height: 25px">
                 <asp:Label id="lblUser" runat="server" cssclass="login"  />
               </td>
             </tr>
           </table>
         </div>
                       
         <div style="width:100%; height:1000px; position:absolute; top:90px; z-index:500" >
   
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
                <igtab:Tab Key="trx" Text="Transaction" Tooltip="Transaction Application">
                    <ContentPane BorderStyle="None" TargetUrl="menu_sdtrx.aspx"  Scrollable="Hidden">
                    </ContentPane>
                    
                </igtab:Tab>
                <igtab:TabSeparator>
                </igtab:TabSeparator>
                <igtab:Tab Key="stp" Text="Setup" Tooltip="Setup Application">
                    <ContentPane BorderStyle="None" TargetUrl="menu_sdstp.aspx" Scrollable="Hidden">
                    </ContentPane>
 		    
                </igtab:Tab>
                <igtab:TabSeparator>
                </igtab:TabSeparator>
                <igtab:Tab Key="rpt" Text="Reports" Tooltip="Reports Application">
                    <ContentPane BorderStyle="None" TargetUrl="menu_sdrpt.aspx" Scrollable="Hidden">
                    </ContentPane>
                </igtab:Tab>  
               
            </Tabs>
            </igtab:UltraWebTab>
        </div>
        
    <div class="BackgroundTopCornerSD"></div>--%>
    </form>
</body>--%>
</html>


<%@ Page Language="vb"  src="../../../include/system_user_setlocation.aspx.vb" Inherits="system_user_setlocation"  %>
<%@ Register TagPrefix="UserControl" Tagname="MenuSetLoc" src="../../menu/menu_user_setlocation.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%><html enableviewstate="false">
	<head>
		<title>Location Setting</title>
   
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />


	</head>
	<script language="JavaScript">
      javascript:window.history.forward(1);
    </script>
	<body  >
 
    <form id="frmSetLo" runat="server" >
        
  <table cellpadding="0" cellspacing="0" style="width: 100%; height: 96%;" align="left" class="main-modul-bg">
	<tr>
		<td class="cell-left" valign="top">
		
		<table cellpadding="0" cellspacing="0" style="width: 100%; height: 50px" class="font9Tahoma">
			<tr>
				<td class="cell-right" valign="bottom">
				<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
					<tr>
						<td style="width: 100%" class="cell-right">&nbsp;</td>
						<td valign="bottom">
							<table align="right" cellpadding="0" cellspacing="0" style="width: 120px">
								<tr>
									<td style="height: 60px">
										<div class="dropdown">	
											&nbsp;<div class="dropdown-content">
											
												<table cellpadding="0" cellspacing="0" style="width: 455px; height: 20px">
													<tr>
													
														<td class="cell-left" valign="middle" style="width: 210px; height:20px">
															&nbsp;</td>
														
														<td class="cell-left" valign="middle" style="width: 55px; height:20px">
															&nbsp;</td>
														
														<td class="cell-left" valign="middle" style="width: 85px; height:20px">
															&nbsp;</td>
														<td class="cell-left" valign="middle" style="width: 40px; height:20px">
															&nbsp;</td>
														
														<td class="cell-right" valign="middle" style="width: 65px; height:20px">
															&nbsp;</td>
														
													</tr>
												</table>
													
											</div>
										</div>
										
										<a href="mainmenu.html">
											&nbsp;</a><a href="default.html">
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


          <div class="view" id="view">
        
        <div style="position:absolute; right:10px; top:20px; width:650px;">
          
	   <div id="navigationContainer" class="navigationContainer">
           	<a href="../login.aspx"> </a>
	           <span class="font9">
						<strong>Welcome, 
                 <asp:Label id="lblUser" runat="server" cssclass="login"  />
		</strong>! Please choose your location &amp;	periode </span>
		
			<a href="../../../changepassword.aspx?referer=login.aspx"><asp:Image runat="server" ID="imgcpwd" ImageUrl="../../images/thumbs/cpwd.gif" ToolTip="Change Password" /></a>
		</div>      
         </div>
        </div>


		<table cellpadding="0" cellspacing="0" style="width:100%">
			<tr>
				<td style="width: 90px" valign="top">
				</td>
				<td style="width: 534px" valign="top">
				    &nbsp;</td>
				<td style="width: 204px" valign="top">
				&nbsp;</td>
				<td style="width: 426px" valign="top">
				    &nbsp;</td>
				<td style="width: 96px" valign="top">
				&nbsp;</td>
			</tr>
		</table>
		</td>
	</tr>
</table>
      
    
        
               <div  style="left:50px; position: absolute; top:200px; width:325px; z-index:400" > 
               <table border="0" cellspacing="0" cellpadding="0" width="325px" class="font9Tahoma">
				<tr style="height:25px" class="login">
					<td colspan=2>
						<asp:Label id="lblMultiLoc" visible="false" runat="server" CssClass="login"/>
						<asp:Label id="lblSingleLoc" visible="false" runat="server" CssClass="login"/>
					</td>
				</tr>
				<tr style="height:25px" class="login">
					<td valign="top" align="left" style="width: 100%; height: 110px;">
						<asp:RadioButtonList id="rblLocList"   repeatdirection="vertical" runat="server" EnableViewState="true" CssClass="navigatioLocation"/>
						<asp:Label id="lblDefaultLoc" visible="false" runat="server" CssClass="login"/>
						<asp:Label id="lblErrLoc" forecolor="Red" visible="false" runat="server" CssClass="login"/>
						<asp:Label id="lblErrMsgPeriodLoc" visible="false" text="WARNING: There is no period configuration at " runat="server" CssClass="login"/>
						<asp:Label id="lblErrMsgPeriodYr" visible="false" text=" for accounting year " runat="server" CssClass="login"/>
						<asp:Label id="lblErrMsgPeriodSet" visible="false" text=". Kindly proceed to period configuration under Administration." runat="server" CssClass="login"/>
						<asp:Label id="lblErrMaxPeriod" visible="false" forecolor="Red" runat="server" CssClass="login"/>
					</td>
				</tr>
				</table>
               
               </div>
               
                              
               
               <div style="left:400px; position: absolute; top:200px; width:500px; z-index:400">
               
               <table style="width:500px; text-align:left; vertical-align:top" border="0" cellspacing="0" cellpadding="0" class="font9Tahoma">
               
               
               <tr style="height:25px">
                <td style="width:20%">
               	<asp:Label id="lblKodeUnit" text="Kode Unit" runat="server" CssClass="login"/>
                </td>
                <td style="width:70%">
                 <asp:Label id="lblKodeUnitI" text="" runat="server" CssClass="login"/>
                </td>
               </tr>
               <tr style="height:25px">
                <td style="width:20%">
               	    <asp:Label id="lblNamaUnit" text="Nama Unit" runat="server" CssClass="login"/>
                </td>
                <td style="width:70%">
                    <asp:Label id="lblNamaUnitI" text="" runat="server" CssClass="login"/>
                </td>
               </tr>
               <tr style="height:25px">
                <td style="width:20%">
                <asp:Label id="lblManager" text="Manager" runat="server" CssClass="login"/>
                </td>
                <td style="width:70%">
                <asp:Label id="lblManagerI"  runat="server" CssClass="login"/>
                </td>
               </tr>
               <tr style="height:25px">
                <td style="width:20%">
                <asp:Label id="lblKTU" text="KTU" runat="server" CssClass="login"/>
                </td>
                <td style="width:70%">
                <asp:Label id="lblKTUI"  runat="server" CssClass="login"/>
                </td>
               </tr>
               <tr style="height:25px">
                <td style="width:20%; vertical-align:top">
               	    <asp:Label id="lblAlamat" text="Alamat" runat="server" CssClass="login"/>
                </td>
                <td style="width:70%">
                    <asp:Label id="lblAlamatI" text="" runat="server" CssClass="login"/>
                </td>
               </tr>
               <tr style="height:25px">
                <td style="width:20%">&nbsp;</td>
                <td style="width:70%">
                 <asp:Label id="lblKotaI" text="" runat="server" CssClass="login"/>
                   <asp:Label id="lblKodePosI" text="" runat="server" CssClass="login"/>
                </td>
               </tr>
               
               <tr style="height:25px">
                <td style="width:20%">
               	    <asp:Label id="lblNoTelp" text="No. Telepon" runat="server" CssClass="login"/>
                </td>
                <td style="width:70%">
                    <asp:Label id="lblNoTelpI" text="" runat="server" CssClass="login"/>
                </td>
               </tr>
               <tr style="height:25px">
                <td style="width:20%">
               	    <asp:Label id="lblNoFax" text="No. Fax" runat="server" CssClass="login"/>
                </td>
                <td style="width:70%">
                    <asp:Label id="lblNoFaxI" text="" runat="server" CssClass="login"/>
                </td>
               </tr>
               <tr style="height:25px">
                <td style="width:20%">
               	    <asp:Label id="lblNoNPWP" text="No. NPWP" runat="server" CssClass="login"/>
                </td>
                <td style="width:70%">
                    <asp:Label id="lblNoNPWPI" text="" runat="server" CssClass="login"/>
                </td>
               </tr>
                <tr style="height:25px">
                <td style="width:35%">
               	<asp:Label id="lblAccPeriod" text="Period To Process " runat="server" CssClass="login"/>
                </td>
                <td style="width:70%">
                     <asp:DropDownList id="ddlAccMonth" width=13% runat=server forecolor=black CssClass="login">
										<asp:ListItem value="1">1</asp:ListItem>
										<asp:ListItem value="2">2</asp:ListItem>										
										<asp:ListItem value="3">3</asp:ListItem>
										<asp:ListItem value="4">4</asp:ListItem>
										<asp:ListItem value="5">5</asp:ListItem>
										<asp:ListItem value="6">6</asp:ListItem>
										<asp:ListItem value="7">7</asp:ListItem>
										<asp:ListItem value="8">8</asp:ListItem>
										<asp:ListItem value="9">9</asp:ListItem>
										<asp:ListItem value="10">10</asp:ListItem>
										<asp:ListItem value="11">11</asp:ListItem>
										<asp:ListItem value="12">12</asp:ListItem>
									</asp:DropDownList>
					<asp:DropDownList id=ddlAccYear width=20% runat=server forecolor=black CssClass="login"/>
					<asp:label id=lblErrPeriod Text="Invalid period." forecolor=red Visible = false Runat="server" CssClass="login"/> 
                </td>
               </tr>
               <tr style="height:25px">
               <td></td>
               <td></td>
               </tr>
               
              <tr style="height:25px">
              <td></td>
              <td>
                  <asp:Button ID="btnLaunch" runat="server" Text="E N T E R" 
                      CssClass="button-small" OnClick="btnLaunch_Click" Height="28px" Width="107px" /> </td>
              </tr>
			  
			   
               </table>
               
               <div style="position: relative;  width:300px; z-index:400">
              
                <asp:Label id="lblErrMesage" visible="false" Text="Error while initiating component." runat="server" />
			    <asp:label id="lblErrSelect" visible="false" text="<br>Please select one " runat="server"/>
			    <asp:label id="lblSelectLoc" visible="false" text="Please select the " runat="server"/>
			    <asp:label id="lblAccess" visible="false" text=" you wish to access." runat="server"/>
			    <asp:label id="lblYour" visible="false" text="Your " runat="server" />
			    <asp:label id="lblSetting" visible="false" text=" setting." runat="server" />
			    <asp:label id="lblLocation" visible="false" runat="server" />
              
              
              </div>
               
              </div>
              
                      &nbsp;</td>
            </tr>
        </table>
         
    </form>
	
       

	</body>
</html>

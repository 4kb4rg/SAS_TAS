<%@ Page Language="vb" src="../../../include/CM_setup_ExchangeRateDet.aspx.vb" Inherits="CM_setup_ExchangeRateDet" %>
<%@ Register TagPrefix="UserControl" Tagname="menu_cm_setup" src="../../menu/menu_cmsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Exchange Rate Detail</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	
		<body>
		    <form id="frmMain" runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">



			<asp:label id="SortExpression" visible="False" runat="server"></asp:label>
			<asp:label id="blnUpdate" visible="false" runat="server"></asp:label>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="sortcol" visible="False" runat="server"></asp:label>
			<asp:Label id="ErrorMessage" runat="server" />
			
			<table border="0" cellspacing="1" cellpadding="1" width="100%" class="font9Tahoma">
				<tr>
					<td colspan="2"><UserControl:menu_cm_setup id=menu_cm_setup runat="server" /></td>
				</tr>
				<tr>
					<td  ><strong> EXCHANGE RATE DETAILS</strong></td>
					<td align="right" ><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=2>
                        <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=2 width=100%>
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma">
						    <tr class="mb-t">
					            <td width=15% valign=top>Transaction Date :*</td>
					            <td width="25%">
						            <asp:TextBox id=txtDate runat="server" width=50% maxlength="20"/>                       
						            <a href="javascript:PopCal('txtDate');">
						            <asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>					
						            <asp:RequiredFieldValidator 
							            id="rfvDate" 
							            runat="server"  
							            ControlToValidate="txtDate" 
							            text = "Field cannot be blank"
							            display="dynamic"/>
						            <asp:label id=lblDate Text="<br>Date Entered should be in the format" forecolor=red Visible=false Runat="server"/> 
						            <asp:label id=lblFmt forecolor=red Visible=false Runat="server"/> 
						            <asp:label id="lblDupMsg" Text="Exchange Rate for the day already exist" Visible=false forecolor=red Runat="server"/>								
						       </td>
					            <td width=10%>									
								</td>
								<td width=10%>Status :
								</td>
								<td width=30%><asp:Label id=lblStatus runat=server />
								</td>
				            </tr>
							<tr class="mb-t">
								<td width=15% valign=top>First Currency :*</td>
								<td width=30%>
									<asp:DropDownList id="ddlFirstCurr" width=100% runat="server"/>
									<asp:RequiredFieldValidator id=rfvFirstCurr display=Dynamic runat=server 
								ErrorMessage="<br>Please Select Date."
								ControlToValidate=ddlFirstCurr />
								</td>
								<td width=10%>									
								</td>
								<td valign=top>Date Created :
								</td>
								<td><asp:Label id=lblDateCreated runat=server />									
								</td>
							</tr>
							<tr class="mb-t">
								<td width=15% valign=top>
									Second Currency :*
								</td>
								<td width=30%>
									<asp:DropDownList id="ddlSecCurr" width=100% runat="server"/>
									<asp:RequiredFieldValidator id=rfvSecCurr display=Dynamic runat=server 
								ErrorMessage="<br>Please Select Date."
								ControlToValidate=ddlSecCurr />
								</td>
								<td width=10%>									
								</td>
								<td valign=top>Last Update :
								</td>
								<td valign=top><asp:Label id=lblLastUpdate runat=server />
								</td>
							</tr>
							<tr class="mb-t" >
								<td valign=top rowspan=2>
									Exchange Rate :*
								</td>
								<td rowspan=2  valign=top> 
									<asp:TextBox id="txtExchangeRate" maxlength="20" width=50% runat="server"/>
									<asp:RequiredFieldValidator id=rfvExchangeRate display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter Exchange Rate."
										ControlToValidate=txtExchangeRate/>	
									<asp:RegularExpressionValidator id="revExchangeRate" 
										ControlToValidate="txtExchangeRate"
										ValidationExpression="^(\+|-)?\d{1,18}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 18 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvExchangeRate"
										ControlToValidate="txtExchangeRate"
										MinimumValue="0"
										MaximumValue="99999999999999999999999"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must be greater than 0!"
										runat="server" display="dynamic"/>							
								</td>
								<td width=10%>									
								</td>
								<td valign=top>
									Update By :
								</td>
								<td valign=top>
									<asp:Label id=lblUpdatedBy runat=server />
								</td>
							</tr>
							<!--tr class="mb-t">
								
							</tr-->
						</table>
					</td>
				</tr>			
				<tr>
					<td colspan=2><asp:label id=lblErrDup visible=false forecolor=red runat="server"/>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete "  CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText=" Undelete " imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />						
					</td>
				</tr>			
				<Input Type=Hidden id=tbcode runat=server />
				<asp:Label id=lblHiddenSts visible=false text=0 runat=server/>
				<tr>
					<td colspan="2">
                                            &nbsp;</td>
				</tr>			
				</table>


        <br />
        </div>
        </td>
        </tr>
        </table>


			</FORM>
		</body>
</html>

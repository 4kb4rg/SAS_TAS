<%@ Page Language="vb" trace="false" src="../../../include/PR_setup_AttdDet.aspx.vb" Inherits="PR_setup_AttdDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_PRsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Attendance Code Details</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain runat="server" class="main-modul-bg-app-list-pu">



        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">



			<Input Type=Hidden id=tbcode runat=server />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>		
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRSetup id=MenuPRSetup runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">ATTENDANCE CODE DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" />  </td>
				</tr>
				<tr>
					<td width=20% height=25>Attendance Code :* </td>
					<td width=30%>
						<asp:Textbox id=txtAttCode width=50% maxlength=8 runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
							ErrorMessage="<br>Please enter Attendance Code."
							ControlToValidate=txtAttCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtAttCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>This code has been used, please try another Attendance Code." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Description : </td>
					<td><asp:Textbox id=txtDescription maxlength=128 width=100% runat=server/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
							ErrorMessage="Please enter Description."
							ControlToValidate=txtDescription />
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Attendance Hour :*</td>
					<td><asp:Textbox id=txtHour maxlength=2 width=50% runat=server/>
						<asp:RequiredFieldValidator id=requireHour display=Dynamic runat=server 
							ErrorMessage="<br>Please enter Hour."
							ControlToValidate=txtHour />
						<asp:CompareValidator id="validateHour" display=dynamic runat="server" 
							ControlToValidate="txtHour" Text="<br>Hour must be in integer. " 
							Type="integer" Operator="DataTypeCheck"/>					
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 valign=top>Attendance Hours will be used as checkroll costing :</td>
					<td valign=top>
						<asp:checkbox id=cbCheckrollCostInd checked=false text=" Yes" runat=server /><br>
						(note: Actual Hours wil be used as checkroll costing if the attendance hours checkbox is not in used.)
					</td>
					<td>&nbsp;</td>
					<td valign=top>Updated By : </td>
					<td valign=top><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 valign=top>Pay Type :*</td>
					<td><asp:radiobuttonlist id=rdPayType AutoPostBack=true runat=server class="font9Tahoma"/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 valign=top>Applicable For :*</td>
					<td><asp:radiobuttonlist id=rdDayType runat=server class="font9Tahoma"/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>OT Allowed : </td>
					<td><asp:checkbox id=cbxOT value="1" text="Yes" OnCheckedChanged="displayOTClaimType" AutoPostBack=true runat=server /></td>
					<td colspan=4>&nbsp;</td>
				</tr>				
				<tr>
					<td height=25 valign=top>OT Claim Type :*</td>
					<td><asp:radiobutton id="rdNotApply" value="4" text="Not Applicable" groupname="OTClaimType" runat="server" /><br>
						<asp:radiobutton id="rdNormalDay" value="1" text="Normal Day " groupname="OTClaimType" runat="server" /><br>
						<asp:radiobutton id="rdRestDay" value="2" text="Off Day" groupname="OTClaimType" runat="server" /><br>
						<asp:radiobutton id="rdHoliday" vlaue="3" text="Public Holiday" groupname="OTClaimType" runat="server" />
					</td>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 valign=top>Counted as :*</td>
					<td><asp:radiobutton id=rdWorking value="1" text="Working Manday" groupname="CountDayType" AutoPostBack=true runat=server /><br>
						<asp:radiobutton id=rdAbsent value="2" text="Absent Day" groupname="CountDayType" AutoPostBack=true runat=server /><br>
						<asp:radiobutton id=rdAnnual value="4" text="Annual Leave" groupname="CountDayType" AutoPostBack=true runat=server /><br>
						<asp:radiobutton id=rdSick value="5" text="Sick Leave" groupname="CountDayType" AutoPostBack=true runat=server /><br>
						<asp:radiobutton id=rdOthers value="3" text="Others" groupname="CountDayType" AutoPostBack=true runat=server />
					</td>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblRiceAllowed" runat="server" /> Allowed : </td>
					<td><asp:checkbox id=cbxRiceAllow value="1" text="Yes " runat=server /></td>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblIncentiveAllowed" runat="server" /> Allowed : </td>
					<td><asp:checkbox id=cbxIncAllow value="1" text="Yes " runat=server /></td>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				
				<tr>
					<td colspan="6">
                                            <asp:Button ID="AddBtn3" runat="server" 
                            class="button-small" Text="save" onclick="AddBtn3_Click" />							
                                            <asp:Button ID="AddBtn4" runat="server" 
                            class="button-small" Text="delete" />							
                                            <asp:Button ID="AddBtn5" runat="server" 
                            class="button-small" Text="undelete" />							
                                            <asp:Button ID="AddBtn6" runat="server" 
                            class="button-small" Text="back" />							
					</td>
				</tr>
			</table>

        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>
	</body>
</html>

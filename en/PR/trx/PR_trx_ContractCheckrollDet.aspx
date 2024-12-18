<%@ Page Language="vb" src="../../../include/PR_trx_ContractCheckrollDet.aspx.vb" Inherits="PR_trx_ContractCheckrollDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Contractor Checkroll Details</title>
            <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
            <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<Input Type=Hidden id=hidAttdID runat=server />
			<Input Type=Hidden id=hidSuppCode runat=server />
			<Input Type=Hidden id=hidAttdCode runat=server />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<asp:Label id=lblIsUpdate visible=false text=false runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRTrx id=MenuPRTrx runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">CONTRACTOR CHECKROLL DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" /> </td>
				</tr>
				<tr>
					<td width=20% height=25>Supplier Code :* </td>
					<td width=30% height=25>
						<asp:DropDownList id=ddlSuppCode width=100% autopostback=true OnSelectedIndexChanged=OnChange_Reload runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
							ErrorMessage="<br>Please select Supplier Code"
							ControlToValidate=ddlSuppCode />
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15% valign=top>Status : </td>
					<td width=25% valign=top><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Attendance Date :*</td>
					<td><asp:textbox id=txtAttdDate width=50% onTextChanged=OnChange_Reload AutoPostBack=True maxlength=10 runat=server />
						<asp:RequiredFieldValidator id=validateAttdDate display=Dynamic runat=server 
							ErrorMessage="<BR>Please enter Attendance Date."
							ControlToValidate=txtAttdDate />
						<a href="javascript:PopCal('txtAttdDate');">
						<asp:Image id="btnAttdDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:label id=lblErrAttdDate Text ="<br>Date entered should be in the format " forecolor=red visible=false runat="server"/>
						<asp:label id=lblDateFormat forecolor=red visible=false runat=server />							
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Attendance Code :*</td>
					<td><asp:DropDownList id=ddlAttdCode width=100% runat=server/>
						<asp:RequiredFieldValidator id=validateAttdCode display=Dynamic runat=server 
							ErrorMessage="<br>Please select Attendance Code"
							ControlToValidate=ddlAttdCode />
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td colspan=6><asp:Label id=lblErrDup visible=false forecolor=red text="The Supplier Code already exists for this attendance date and code." runat=server/></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_savenext.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=NewBtn AlternateText="  New  " imageurl="../../images/butt_new.gif" onclick=Button_Click CommandArgument=New runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					    <br />
					</td>
				</tr>
			</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>

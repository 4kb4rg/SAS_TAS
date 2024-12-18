<%@ Page Language="vb" src="../../../include/HR_setup_Deptdet.aspx.vb" Inherits="HR_setup_Deptdet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Department Details</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%">
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:Label id="lblSelect" visible="false" text="Please select " runat="server" />
			<asp:Label id="lblList" visible="false" text="Select " runat="server" />
			
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5"><asp:label id="lblTitle" runat="server" /> DETAILS</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20%><asp:label id="lblDepartment" runat="server" /> Code :* </td>
					<td width=30%>
						<asp:DropDownList id=ddlDeptCode width=100% runat=server/>
						<asp:Label id=lblNoDeptCode forecolor=red visible=false runat=server/>
						<asp:Label id=lblErrDupDept forecolor=red visible=false text="<br>The code has been used, please select another code." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=20%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td><asp:label id="lblCompany" runat="server" /> Code :* </td>
					<td>
						<asp:DropDownList id=ddlCompCode width=100% onSelectedIndexChanged=onChange_CompCode runat=server/>
						<asp:Label id=lblNoCompCode forecolor=red visible=false runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td align="left"><asp:label id="lblLocation" runat="server" /> Code :*</td>
					<td align="left">
						<asp:DropDownList id=ddlLocCode width=100% onSelectedIndexChanged=onChange_LocCode runat=server/>
						<asp:Label id=lblNoLocCode forecolor=red visible=false runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td align="left"><asp:label id="lblDeptHead" runat="server" /> :</td>
					<td align="left">
						<asp:DropDownList id=ddlDeptHead width=80% runat=server/> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','ddlDeptHead','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>

				<td colspan="5">&nbsp;</tr>
				<tr>
					<td colspan="5">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " imageurl="../../images/butt_delete.gif" CausesValidation=False onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<Input Type=Hidden id=deptcode runat=server />
				<asp:Label id=lblNoRecord visible=false text="Department details not found." runat=server/>
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<tr>
					<td colspan="5">
                                            &nbsp;</td>
				</tr>
				</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>

        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>
	</body>
</html>

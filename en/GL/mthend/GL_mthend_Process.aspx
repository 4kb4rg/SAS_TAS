<%@ Page Language="vb" src="../../../include/GL_MthEnd_Process.aspx.vb" Inherits="GL_mthend_Process" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLMthEnd" src="../../menu/menu_GLMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>General Ledger Month End Process</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
        <Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	
	<body >
		<form id=frmMain  runat=server >
 <div class="kontenlist">
		<table border="0" width="100%" cellpadding="0" cellspacing="0" class="font9Tahoma"  >
			<tr>
				<td colspan=5 align=center><UserControl:MenuGLMthEnd id=MenuGLMthEnd runat="server" /></td>
			</tr>
			<tr>
				<td  colspan=5><strong> GENERAL LEDGER MONTH END PROCESS</strong></td>
			</tr>
			<tr>
				<td  colspan=5>
                         <hr style="width :100%" />
					</td>
			</tr>
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5 height=25>
					<font color=red>Important notes before month end process:</font><p>
					1. Please backup up the database before proceed.<br>
					2. Ensure no user is in the system.<br>
					3. All the required pre-month end reports have been generated.
				</td>
			</tr>
			<tr>
				<td colspan=5 height=25>&nbsp;</td>
			</tr>
			<tr>
				<td width=20% height=25>Current Accounting Period :</td> 
				<td width=30%><asp:Label id=lblCurrAccPeriod runat=server/></td>
				<td width=5%>&nbsp;</td>
				<td width=15%>&nbsp;</td>
				<td width=30%>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5 height=25>&nbsp;</td>
			</tr>
			<tr>
				<td width=20% height=25>Month End Accounting Period :</td> 
				<td width=30%><asp:Label id=lblAccPeriod runat=server/></td>
				<td width=5%>&nbsp;</td>
				<td width=15%>&nbsp;</td>
				<td width=30%>&nbsp;</td>
			</tr>
			<tr>
				<td width=20% height=25>Month End Status :</td> 
				<td width=30%><asp:Label id=lblStatus runat=server/></td>
				<td width=5%>&nbsp;</td>
				<td width=15%>&nbsp;</td>
				<td width=30%>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Last Processed Date :</td> 
				<td><asp:Label id=lblLastProcessDate runat=server/></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
			    <td align="left">Closing Method?</td>
			    <td align="left">
					<asp:RadioButton id="CloseInd_Final" 
						Checked="False"
						GroupName="CloseInd"
						Text="Final"
						TextAlign="Right"
						AutoPostBack="True"
						oncheckedchanged=CloseInd_OnCheckChange 
						runat="server" /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>
					<asp:RadioButton id="CloseInd_Temporary" 
						Checked="True"
						GroupName="CloseInd"
						Text="Locking Period"
						TextAlign="Right"
						AutoPostBack="True"
						oncheckedchanged=CloseInd_OnCheckChange 
						runat="server" /></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>
					<asp:RadioButton id="CloseInd_RollBack" 
						Checked="false"
						GroupName="CloseInd"
						Text="Unlocking Period"
						TextAlign="Right"
						AutoPostBack="True"
						oncheckedchanged=CloseInd_OnCheckChange 
						runat="server" /></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5 height=25>
					<asp:Label id=lblErrVehNoDist visible=false forecolor=red text="General Ledger month end will not be able to process if some vehicle(s) has no distribution. You can re-configure for allowing month end with no distribution or, enter some run unit for following vehicle(s)" runat=server/>
					<asp:Label id=lblErrVehList visible=false forecolor=red runat=server />
					<asp:Label id=lblErrNotClose visible=false forecolor=red text="There are some modules waiting for month end process before General Ledger can be processed." runat=server/>
					<asp:Label id=lblErrProcess visible=false forecolor=red text="There are some errors when processing month end." runat=server/>
					<asp:Label id=lblErrJournal visible=false forecolor=red text="There are some pending journal documents waiting to post." runat=server/>
					<asp:Label id=lblErrGCFail visible=false forecolor=red text="Month end processs has terminated because there are some errors when distributing general charges." runat=server/>
					<asp:Label id=lblErrGCNoAllocation visible=false forecolor=red text="Month end process has terminated when distributing general charges. Kindly define the general charges allocation for mature / immature before try again." runat=server/>
					<asp:Label id=lblErrGCNoLocation visible=false forecolor=red text="Month end process has terminated when distributing general charges. Kindly define the location(s) to which the general charges will be distributed. " runat=server/>
					<asp:Label id=lblErrMsg visible=false forecolor=red runat=server />
					<asp:Label id=lblWSMessage visible=false forecolor=blue runat=server />
				</td>
			</tr>
			<tr>
				<td colspan=5>
					<asp:ImageButton id=btnProceed imageurl="../../images/butt_proceed_mthend.gif" AlternateText="Proceed with month end process" OnClick="btnProceed_Click" runat="server" />
				&nbsp;</td>
			</tr>
		</table>
		<Input type=hidden id=hidClsAccMonth value="" runat=server visible="true"/>
		<Input type=hidden id=hidClsAccYear value="" runat=server visible="true"/>
 </div>
		</form>
	</body>
</html>

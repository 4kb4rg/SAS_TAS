<%@ Page Language="vb"  src="../../../include/WS_Setup_WorkCodeDet.aspx.vb" Inherits="WS_WorkCodeDet" clienttarget="downlevel" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWS" src="../../menu/menu_wssetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Work Code Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
			<Form id=frmWorkCodeDet runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<table border=0 cellpadding=2 width=100% class="font9Tahoma">
				<asp:label id=lblCode text=" Code" visible=false runat=server/>
				<asp:label id=lblPleaseEnter text="<br>Please enter " visible=false runat=server />
				<asp:label id=lblSelect visible=false text="Select " runat=server />
				<Input Type=Hidden id=WorkCode runat=server />				
				<tr>
					<td colspan="6"><UserControl:MenuWS id=menuWS runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6"><asp:label id=lblTitle runat=server /> CODE DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20%><asp:label id=lblWork runat=server /> Code :*</td>
					<td width=30%>
						<asp:TextBox id=txtWorkCode width=50% maxlength=8 runat=server />
						<asp:RequiredFieldValidator id=rfvCode display=dynamic runat=server
							ControlToValidate=txtWorkCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtWorkCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>						
						<asp:label id=lblErrBlankWC visible=false forecolor=red runat=server />
						<asp:label id=lblErrDupWC text="Code already existed. Please try again." visible=false forecolor=red runat=server />
						<asp:label id=lblWorkCode visible=false runat=server />
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /> </td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblWorkDesc runat=server /> :* </td>
					<td>
						<asp:TextBox id=txtDescription width=100% maxlength=256 runat=server /> 
						<asp:label id=lblErrWorkDesc text="" visible=false forecolor=red runat=server />
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblServType runat=server /> :*</td>
					<td>
						<asp:DropDownList id="lstServType" Style="Width:100%;" runat="server" />
						<asp:label id=lblErrServType text="" visible=false forecolor=red runat=server />
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblAccCode runat=server /> :*</td>
					<td>
						<table Width="100%" CellSpacing="0" CellPadding="0" Border="0">
							<tr>
								<td Width="99%"><asp:DropDownList id="lstAccCode" Enabled="False" Style="Width:100%;" AutoPostBack="True" OnSelectedIndexChanged="lstAccCode_OnSelectedIndexChanged" runat="server" /></td>
								<td>&nbsp;<Input Type="Button" Value=" ... " ID="btnFindAccCode" OnClick="javascript:findcode('frmWorkCodeDet','','lstAccCode','6','lstBlock','','','','','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation="False" RunAt="Server" /></td>								
							</tr>
						</table>
						<asp:label id=lblErrAccCode text="" visible=false forecolor=red runat=server />
					</td>
					<td>&nbsp;</td>	
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblBlkTag runat=server /> :</td>
					<td>
						<asp:DropDownList id="lstBlock" Style="Width:100%;" runat="server" />
						<asp:label id=lblErrBlock text="" visible=false forecolor=red runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6>
						<asp:DataGrid Runat=server Width=100% ID=dgWorkCodeDet GridLines="None" CellPading="2" AutoGenerateColumns="False"
                            class="font9Tahoma">	
							 
							<HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<Columns>
								<asp:BoundColumn DataField="ServTypeCode"/>
								<asp:BoundColumn DataField="Description"/>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onClick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn CausesValidation=false AlternateText=" Delete " imageurl="../../images/butt_delete.gif" onClick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onClick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " imageurl="../../images/butt_back.gif" onClick=BackBtn_Click runat=server />
					</td>
				</tr>
				<tr>
					<td colspan="6">
						&nbsp;</td>
				</tr>
			</table>
			<input Type=Hidden ID="hidBlockCharge" Value="" RunAt="Server" />
			<input Type=Hidden ID="hidChargeLocCode" Value="" RunAt="Server" />	
            
          <br />
        </div>
        </td>
        </tr>
        </table>
        
        		
			</form>
	</body>
</html>

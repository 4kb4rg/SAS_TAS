<%@ Page Language="vb" src="../../../include/system_config_syssetting.aspx.vb" Inherits="system_config_syssetting"%>
<%@ Register TagPrefix="UserControl" Tagname="MenuSYS" src="../../menu/menu_sys.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>System Configuration</title>		
                <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body leftmargin="10" topmargin="10" marginwidth="10" marginheight="10">
		<form id=frmSysSetting enctype="multipart/form-data" class="main-modul-bg-app-list-pu" runat=server>
            <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top" class="font9Tahoma" >
			    <div class="kontenlist"> 

		<table border="0" width="100%" cellspacing="0" cellpadding="2" class="font9Tahoma">
			<tr>
				<td colspan="5">
					<UserControl:MenuSYS id=MenuSYS runat="server" />
				</td>
			</tr>
			<tr>
				<td align=left class="font9Tahoma" colspan="5"><strong> SYSTEM CONFIGURATION</strong></td>
			</tr>
			<tr>
				<td colspan=5><hr style="width :100%" /></td>
			</tr>
			<tr class="mr-h">
				<td class="normalBold" colspan="5">Step 1 : Select <asp:label id="lblcomp1" runat="server" />, language preference and data format.&nbsp;
				</td>
			</tr>
			<tr>
				<td align="left" valign=top><asp:label id="lblComp2" runat="server" />:*</td>
				<td align="left" valign=top>
					<asp:DropDownList id=SelectedComp width=100% runat=server />
					<asp:Label id=lblErrCompany forecolor=red visible=false text="<br>Please select one company." runat=server/>
				</td>
				<td align="left">&nbsp;</td>
				<td valign=top>Date Created :</td>
				<td valign=top><asp:Label id=DateCreated runat=server /></td>
			</tr>
			<tr>
				<td valign=top>Language:*</td>
				<td class="normalbold" valign=top>
					<asp:DropDownList id=SelectedLanguage width=100% runat=server>
						<asp:listitem id=en Value="EN" Text="English" />
					</asp:DropDownList>
				</td>
				<td>&nbsp;</td>
				<td valign=top>Last Updated:</td>
				<td valign=top><asp:Label id=LastUpdated runat=server /></td>
			</tr>
			<tr>
				<td valign=top>Input Date Format:*</td>
				<td valign=top>
					<asp:RadioButton id="DateFmt_DMY" 
						Checked="True"
						GroupName="DateFmt"
						Text="dd/mm/yyyy"
						TextAlign="Right"
						runat="server" />
				</td>
				<td>&nbsp;</td>
				<td valign=top>Updated By:
				</td>
				<td valign=top><asp:Label id=UpdatedBy runat=server /></td>
			</tr>
			<tr>
				<td class="normalbold" valign=top>&nbsp;</td>
				<td valign=top>
					<asp:RadioButton id="DateFmt_MDY" 
						Checked="False"
						GroupName="DateFmt"
						Text="mm/dd/yyyy"
						TextAlign="Right"
						runat="server" />					
				</td>
				<td>&nbsp;</td>
				<td valign=top>&nbsp;</td>
				<td valign=top>&nbsp;</td>
			</tr>
			<tr>
			    <td align="left">Filterized Period?</td>
			    <td align="left">
					<asp:RadioButton id="FilterPeriod_Yes" 
						Checked="True"
						GroupName="FilterPeriodInd"
						Text="Yes (Only Activated Selected Period)"
						TextAlign="Right"
						AutoPostBack="false"
						runat="server" /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>
					<asp:RadioButton id="FilterPeriod_No" 
						Checked="False"
						GroupName="FilterPeriodInd"
						Text="No "
						TextAlign="Right"
						AutoPostBack="false"
						runat="server" /></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr><td valign=top>Round Number:*</td>
			    <td>
	                <asp:TextBox id=RoundNo width=10% maxlength=2 runat=server /> 
	                <asp:RegularExpressionValidator 
						id="RegularExpressionValidator2" 
						ControlToValidate="RoundNo"
						ValidationExpression="^(\-|)\d{1,2}$"
						Display="Dynamic"
						text = "<BR>Maximum length 2 digits"
						runat="server"/>
					<asp:Label id=lblErrRoundNo visible=false forecolor=red text="<BR>Please enter DPP amount" runat=server/>
                </td>
			</tr>

			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr class="mr-h">
				<td class="normalbold" align="left" Colspan="5">&nbsp;Step 
					2 : Add <asp:label id="lblLoc1" runat="server" />&nbsp;
				</td>
			</tr>
			<tr>
				<td align="left" colspan="5">
					<asp:ImageButton imageurl="../../images/butt_add.gif" id=AddLocBtn text="Add Location" onclick=AddLocBtn_Click runat=server />
					<asp:Label id=lblErrAddLoc visible=false forecolor=red text="<br>Please select one company first before add any location." runat=server/>
				</td>
			</tr>
			<tr>
				<td align="middle" colspan="5">
					<asp:DataGrid id=ActiveLocation 
								  BorderColor=black
								  BorderWidth=0
								  GridLines=both
								  CellPadding=0
								  CellSpacing=1
								  width=100% 
								  AutoGenerateColumns=false 
								  OnItemCommand=editLocation_Click
								  runat=server class="font9Tahoma">
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
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:Label id=lblLocCode visible=false text=<%# Container.DataItem("LocCode") %> runat=server />
										<asp:Label id=lblLocDesc visible=false text=<%# Container.DataItem("LocDesc") %> runat=server />
										<asp:LinkButton id=editLocation Text='<%# Container.DataItem("LocCode") %>' runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="LocDesc" />								
								<asp:BoundColumn HeaderText="Automated Data Transfer" DataField="DataTransferInd" />
							</Columns>
					</asp:DataGrid>
				</td>
			</tr>
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr class="mr-h">
				<td class="normalbold" colspan="5">
					Step 3 : Centralize Control over the <asp:label id="lblAcc1" runat="server" /> Format and Transaction Parameters 
				</td>
			</tr>
			<tr>
			    <td align="left">Centralized?</td>
			    <td align="left">
					<asp:RadioButton id="Central_Yes" 
						Checked="True"
						GroupName="CentralizeInd"
						Text="Yes (COA is generalized)"
						TextAlign="Right"
						AutoPostBack="false"
						runat="server" /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>
					<asp:RadioButton id="Central_No" 
						Checked="False"
						GroupName="CentralizeInd"
						Text="No (COA for each location)"
						TextAlign="Right"
						AutoPostBack="false"
						runat="server" /></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
			    <td colspan=5>&nbsp;</td>
			</tr>
			<tr>
				<td align="left">Do you want to control?</td>
				<td align="left">
					<asp:RadioButton id="LocalControlInd_Yes" 
						Checked="True"
						GroupName="LocalControlInd"
						Text="Yes (proceed to step 4, 5 &amp; 6)"
						TextAlign="Right"
						AutoPostBack="True"
						onCheckedChanged="onselect_change"
						runat="server" /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>
					<asp:RadioButton id="LocalControlInd_No" 
						Checked="False"
						GroupName="LocalControlInd"
						Text="No (proceed to step 7)"
						TextAlign="Right"
						AutoPostBack="True"
						onCheckedChanged="onselect_change"
						runat="server" /></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr class="mr-h">
				<td class="normalbold" colspan="5">Step 4 : Define the <asp:label id="lblAcc7" runat="server" /> format for the <asp:label id="lblAcc2" runat="server" /> Code length &nbsp;</td>
			</tr>
			<tr>
				<td height=25 colspan="3">
					<u>General Ledger</u>
					<asp:label id=lblErrStructure visible=false forecolor=red text="<br>Duplicate code being selected, please select again." runat=server />
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>
					<asp:DropDownList id=ddl1st width=80% runat=server>
						<asp:listitem Value="1" Selected />
						<asp:listitem Value="2" />
						<asp:listitem Value="3" />
						<asp:listitem Value="4" />
					</asp:DropDownlist>
					:*
				</td>
				<td colspan=4>
					<asp:TextBox id=AccClsCodeFormat width=10% maxlength=1 runat=server /> 
					<asp:RequiredFieldValidator id=validateAccCls display=dynamic runat=server 
					     ControlToValidate=AccClsCodeFormat />															
					<asp:RangeValidator display=dynamic id="rangeAccCls"
						 ControlToValidate="AccClsCodeFormat"
						 MinimumValue="1"
						 MaximumValue="8"
						 Type="Integer"
						 EnableClientScript="true"
						 Text="The value must be from 1 to 8. "
						 runat="server"/>
				</td>
			</tr>
			<tr>
				<td>
					<asp:DropDownList id=ddl2nd width=80% runat=server>
						<asp:listitem Value="1" />
						<asp:listitem Value="2" Selected />
						<asp:listitem Value="3" />
						<asp:listitem Value="4" />
					</asp:DropDownlist>
					:*
				</td>
				<td colspan=4>
					<asp:TextBox id=ActCodeFormat width=10% maxlength=1 runat=server /> 
					<asp:RequiredFieldValidator id=validateAct display=dynamic runat=server  
					     ControlToValidate=ActCodeFormat />															
					<asp:RangeValidator display=dynamic id="rangeAct"
						ControlToValidate="ActCodeFormat"
						MinimumValue="1"
						MaximumValue="8"
						Type="Integer"
						EnableClientScript="true"
						Text="The value must be from 1 to 8. "
						runat="server"/>
				</td>					     
			</tr>
			<tr>
				<td>
					<asp:DropDownList id=ddl3rd width=80% runat=server>
						<asp:listitem Value="1" />
						<asp:listitem Value="2" />
						<asp:listitem Value="3" Selected />
						<asp:listitem Value="4" />
					</asp:DropDownlist>
					:*
				</td>
				<td colspan=4>
					<asp:TextBox id=SubActCodeFormat width=10% maxlength=1 runat=server /> 
					<asp:RequiredFieldValidator display=dynamic id=validateSubAct runat=server 
					     ControlToValidate=SubActCodeFormat />															
					<asp:RangeValidator display=dynamic id="rangeSubAct"
						ControlToValidate="SubActCodeFormat"
						MinimumValue="1"
						MaximumValue="8"
						Type="Integer"
						EnableClientScript="true"
						Text="The value must be from 1 to 8. "
						runat="server"/>
				</td>
			</tr>
			<tr>
				<td>
					<asp:DropDownList id=ddl4th width=80% runat=server>
						<asp:listitem Value="1" />
						<asp:listitem Value="2" />
						<asp:listitem Value="3" />
						<asp:listitem Value="4" Selected />
					</asp:DropDownlist>
					:*
				</td>
				<td colspan=4>
					<asp:TextBox id=ExpCodeFormat width=10% maxlength=1 runat=server /> 
					<asp:RequiredFieldValidator display=dynamic id=validateExp runat=server 
					     ControlToValidate=ExpCodeFormat />															
					<asp:RangeValidator display=dynamic id="rangeExp"
						ControlToValidate="ExpCodeFormat"
						MinimumValue="1"
						MaximumValue="8"
						Type="Integer"
						EnableClientScript="true"
						Text="The value must be from 1 to 8. "
						runat="server"/>
				</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25><asp:label id="lblAcc3" runat="server" /> Code:</td>
				<td><asp:Label id=AccountLength runat=server /></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td width=25%>&nbsp;</td>
				<td width=30%>&nbsp;</td>
				<td width=5%>&nbsp;</td>
				<td width=15%>&nbsp;</td>
				<td width=30%>&nbsp;</td>
			</tr>
			<tr class="mr-h">
				<td class="normalbold" colspan="5">Step 5 : Download the 
					<asp:label id="lblAcc4" runat="server" /> format and Transaction parameters </td>
			</tr>
			<tr>
				<td colspan="2">
					Note : To duplicate the code length data to another application.</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td colspan="2">
					<asp:Hyperlink id=lnkDownload text="Click here to download" target=_new runat=server />
					 code length data</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="normalbold" colspan="5">&nbsp;</td>
			</tr>
			<tr class="mr-h">
				<td class="normalbold" colspan="5">Step 6 : Upload <asp:label id="lblAcc5" runat="server" /> format 
					and Transaction parameters</td>
			</tr>
			<tr>
				<td colspan="5">Note : To upload the code length data from 
					another application, click 'Browse' follow with 'Upload'.</td>
			</tr>
			<tr>
				<td colspan="5">
					Filename:
				</td>
			</tr>
			<tr>
				<td colspan="5">
					<input type=file width=50% id=flUpload runat=server />
					<asp:Label id=lblErrUpload text="There was an error when uploading the file.<br>" forecolor=red visible=false runat=server />
					<asp:Label id=lblErrNoFile text="Click browse button to select your file.<br>" forecolor=red visible=false runat=server />
				</td>
			</tr>
			<tr>
				<td colspan="2">
					<asp:Label id=lblOKUpload text="Format for chart of account has been successfully uploaded.<p>" visible=false runat=server />
					<asp:ImageButton imageurl="../../images/butt_upload.gif" id=UploadBtn AlternateText=Upload onclick=UploadBtn_Click runat=server />
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="normalbold" colspan="5">&nbsp;</td>
			</tr>
			<tr class="mr-h">
				<td class="normalbold" colspan="5">Step 7 : Save system configuration and proceed to Parameters Setting&nbsp;</td>
			</tr>
			<tr>
				<td colspan="2">
					<asp:ImageButton id=SaveBtn imageurl="../../images/butt_save.gif" alternatetext=Save onclick=SaveBtn_Click runat=server />
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
		</table>
		<input type=hidden id=DateCreateInd value="" runat=server />
		<asp:label id=lblWSCostDist visible=false text=0 runat=server />
		<asp:label id=lblConfigSetting visible=false text=0 runat=server />
		<asp:label id=lblCode visible=false text=" Code" runat=server />
		<asp:label id=lblSelect visible=false text="Select one " runat=server />
		<asp:label id=lblErrSelect visible=false text="Please select one " runat=server/>
		<asp:label id=lblErrEnterLen visible=false text="Please enter the length for " runat=server/>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</Script>
</html>

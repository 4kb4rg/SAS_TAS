<%@ Page Language="vb" src="../../../include/HR_setup_SalGradeDet.aspx.vb" Inherits="HR_setup_SalGradeDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Salary Grade Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmCPDet runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5">SALARY GRADE DETAILS</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20%>Employee Category :* </td>
					<td width=30%>
						<asp:DropDownList id=ddlSalScheme width=100% runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=20%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td>Salary Grades Code :* </td>
					<td>
						<asp:Textbox id=txtSalGradeCode width=50% maxlength=8 runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
								ErrorMessage="<br>Please Enter Salary Grade Code"
								ControlToValidate=txtSalGradeCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtSalGradeCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>This code has been used, please try another salary grade code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td align="left">Description :*</td>
					<td align="left">
						<asp:Textbox id=txtDescription maxlength=64 width=100% runat=server/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
								ErrorMessage="<br>Please Enter Salary Grade Description"
								ControlToValidate=txtDescription />
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td align="left" valign=top>Yearly Increment Limit :*</td>
					<td align="left" valign=top>
						<!-- Modified BY ALIM maxLength = 22 -->
						<asp:Textbox id=txtMaxSalary maxlength=22 width=100% text=0 runat=server/>
						<asp:RequiredFieldValidator id=validateMaxSalary display=Dynamic runat=server 
								ErrorMessage="Please Enter Yearly Increment Limit."
								ControlToValidate=txtMaxSalary />
						<asp:CompareValidator id="cvValidateMaxSalary" display=dynamic runat="server" 
							ControlToValidate="txtMaxSalary" Text="<br>The value must whole number. " 
							Type="Double" Operator="DataTypeCheck"/>
						<!-- Added BY ALIM -->		
						<asp:RangeValidator id="rvMaxSalary"
								ControlToValidate="txtMaxSalary"
								MinimumValue="0"
								MaximumValue="9999999999999999999.99"
								Type="Double"
								EnableClientScript="True"
								Text="Exceeded Value"
								runat="server"/>
						<!-- End of Added BY ALIM -->		
					</td>
					<td>&nbsp;</td>
					<td valign=top>Updated By : </td>
					<td valign=top><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td align="left" valign=top>Yearly Increment Amount :</td>
					<td align="left" valign=top>
						<!-- Modified BY ALIM maxLength = 22 -->
						<asp:Textbox id=txtYearIncAmount maxlength=22 width=100% text=0 runat=server/>
						<asp:CompareValidator id="cvValidateIncAmt" display=dynamic runat="server" 
							ControlToValidate="txtYearIncAmount" Text="<br>The value must whole number. " 
							Type="Double" Operator="DataTypeCheck"/>
						<!-- Added BY ALIM -->		
						<asp:RangeValidator id="rvYearIncAmount"
								ControlToValidate="txtYearIncAmount"
								MinimumValue="0"
								MaximumValue="9999999999999999999.99"
								Type="Double"
								EnableClientScript="True"
								Text="Exceeded Value"
								runat="server"/>
						<!-- End of Added BY ALIM -->	
						<asp:Label id=lblErrIncrement visible=false forecolor=red text="Key in either Yearly increment amount or, Yearly increment %." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td valign=top>Yearly Increment % :</td>
					<td valign=top>
						<!-- Modified BY ALIM maxLength = 5 -->
						<asp:Textbox id=txtYearIncRate maxlength=5 width=100% text=0 runat=server/>
						<asp:CompareValidator id="cvValidateIncRate" display=dynamic runat="server" 
							ControlToValidate="txtYearIncRate" Text="<br>The value must whole number. " 
							Type="Double" Operator="DataTypeCheck"/>
						<asp:Label id=lblErrPercent visible=false forecolor=red text="The rate should not more than 100 percent." runat=server/>
					</td>
				</tr>
				<tr>
					<td align="left" valign=top>Standard OT Hourly Rate :</td>
					<td align="left" valign=top>
						<!-- Modified BY ALIM maxLength = 22 -->
						<asp:Textbox id=txtOTRate maxlength=22 width=100% text=0 runat=server/>
						<asp:CompareValidator id="cvValidateOTRate" display=dynamic runat="server" 
							ControlToValidate="txtOTRate" Text="<br>The value must whole number. " 
							Type="Double" Operator="DataTypeCheck"/>
						<!-- Added BY ALIM -->		
						<asp:RangeValidator id="rvOTRate"
								ControlToValidate="txtOTRate"
								MinimumValue="0"
								MaximumValue="9999999999999999999.99"
								Type="Double"
								EnableClientScript="True"
								Text="Exceeded Value"
								runat="server"/>
						<!-- End of Added BY ALIM -->		
					</td>
					<td>&nbsp;</td>
					<td valign=top>Overtime Daily Limit :</td>
					<td valign=top>
						<!-- Modified BY ALIM maxLength = 22 -->
						<asp:Textbox id=txtOTLimit maxlength=22 width=100% text=0 runat=server/>
						<asp:CompareValidator id="cvValidateOTLimit" display=dynamic runat="server" 
							ControlToValidate="txtOTLimit" Text="<br>The value must whole number. " 
							Type="Double" Operator="DataTypeCheck"/>
						<!-- Added BY ALIM -->		
						<asp:RangeValidator id="rvOTLimit"
								ControlToValidate="txtOTLimit"
								MinimumValue="0"
								MaximumValue="9999999999999999999.99"
								Type="Double"
								EnableClientScript="True"
								Text="Exceeded Value"
								runat="server"/>
						<!-- End of Added BY ALIM -->		
					</td>
				</tr>

				<tr>
					<td colspan="5">&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan="5">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " imageurl="../../images/butt_delete.gif" onclick=Button_Click CausesValidation=false CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<Input Type=Hidden id=sgcode runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				
				<tr>
					<td colspan="5">
                                            &nbsp;</td>
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

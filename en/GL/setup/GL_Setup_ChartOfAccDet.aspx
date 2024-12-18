<%@ Page Language="vb" src="../../../include/GL_Setup_ChartofAccDet.aspx.vb" Inherits="GL_Setup_ChartOfAccDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_GLsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Chart Of Account Details</title>
                <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmChartOfAccDet runat="server" class="main-modul-bg-app-list-pu" >

			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<table id=tblID border=0 cellspacing=0 cellpadding=2 width=100%  class="font9Tahoma" runat=Server>
				<tr>
					<td colspan="6">
						<UserControl:MenuGLSetup id=MenuGLSetup runat="server" />
					</td>
				</tr>
				<tr>
					<td class="font9Tahoma" colspan="6"><strong> <asp:label id="lblTitle" runat="server" /> DETAILS</strong><hr style="width :100%" />   
                            </td>
				</tr>
				<tr>
					<td colspan=6> </td>
				</tr>
           
				<tr>
					<td valign=top width=20%><asp:label id=lblMethod text="Input Method :*" runat=server/></td>
					<td align=left valign=top colspan=5>
						<asp:radiobuttonlist id=rdMethod autopostback=true OnSelectedIndexChanged=Change_Method  CssClass="font9Tahoma" runat=server>
							<asp:listitem id="item1" value="1" text="Enter Chart of Account Code" runat=server/>
							<asp:listitem id="item2" value="2" text="Select Account Class, Activity, Sub Activity and Expense Code" runat=server/>
						</asp:radiobuttonlist>
					</td>
				</tr>
				<tr>
					<td width=20% height=25><asp:label id="lblAccCode" runat="server" /> :*</td>
					<td width=30%>
						<asp:Textbox id=txtAccCode width=50% maxlength=15 CssClass="fontObject" runat=server/>
						<!--asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtAccCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,10}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/-->
						<asp:Label id=lblErrAccCode visible=false forecolor=red text="<br>Please enter Chart of Account Code." runat=server/>														
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>This code has been used. Please try again." runat=server/>
						<asp:Label id=lblErrLen visible=false forecolor=red runat=server/>
						<asp:Label id=lblErrValidate visible=false forecolor=red runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblAccDesc" runat="server" /> :*</td>
					<td>
						<asp:Textbox id=txtDescription maxlength=128 width=100%  CssClass="fontObject" runat=server/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
								ControlToValidate=txtDescription />
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblAccount" runat="server" /> Type :*</td>
					<td><asp:DropDownList id=ddlAccType width=100% autopostback=true OnSelectedIndexChanged=CallChange_AccType  CssClass="fontObject" runat=server/>
						<asp:Label id=lblErrAccType visible=false forecolor=red display=dynamic runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Update : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25><asp:label id="lblAccGrp" runat="server" /> :*</td>
					<td><asp:DropDownList id=ddlAccGrpCode width=100%  CssClass="fontObject" runat=server/>
						<asp:Label id=lblErrAccGrpCode visible=false forecolor=red display=dynamic runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>COA Level :*</td>
					<td><asp:DropDownList id="ddlCOALevel" width=100%  CssClass="fontObject" runat=server>
							<asp:ListItem value="1">General</asp:ListItem>
							<asp:ListItem value="2">Detail</asp:ListItem>
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td height=25>General COA :</td>
					<td><asp:DropDownList id="ddlCOAGeneral" width=100%   CssClass="fontObject" runat=server>
						</asp:DropDownList>
						<asp:Label id="lblErrCOAGeneral" visible="false" forecolor="red" display="dynamic" text = "Please Select COA General" runat="server"/>
					</td>
				</tr>
				<tr>
					<td height=25>Saldo Normal :*</td>
					<td><asp:DropDownList id="ddlSaldoNormal" width=100%   CssClass="fontObject" runat=server>
							<asp:ListItem value="1">Debit</asp:ListItem>
							<asp:ListItem value="2">Credit</asp:ListItem>
						</asp:DropDownList>
						<asp:Label id="lblErrSaldoNormal" visible="false" forecolor="red" display="dynamic" text = "Please Select Saldo Normal" runat="server"/>
					</td>
				</tr>
				<tr>
					<td height=25>Group Cashflow :*</td>
					<td><asp:DropDownList id="ddlGrpCashflow" width=100%  CssClass="fontObject" runat=server>
							<asp:ListItem value="1">Operasi</asp:ListItem>
							<asp:ListItem value="2">Investasi</asp:ListItem>
							<asp:ListItem value="3">Pendanaan</asp:ListItem>
							<asp:ListItem value="4">Aktifitas yang tidak mempengaruhi kas</asp:ListItem>
							<asp:ListItem value="9">N/A</asp:ListItem>
						</asp:DropDownList>
						<asp:Label id="lblErrGrpCashflow" visible="false" forecolor="red" display="dynamic" text = "Please Select Group Cashflow" runat="server"/>
					</td>
				</tr>
				<tr>
					<td align="left" valign=top><asp:Label id=lbl01 visible=true display=dynamic runat=server/></td>
					<td align="left" valign=top>
						<asp:DropDownList id=ddl01 width=100% OnSelectedIndexChanged=LoadSubActivity  CssClass="fontObject" runat=server/>
						<asp:Label id=lblErr01 visible=false forecolor=red display=dynamic runat=server/>
					</td>
				</tr>
				<tr>
					<td align="left" valign=top><asp:Label id=lbl02 visible=true display=dynamic runat=server/></td>
					<td align="left" valign=top>
						<asp:DropDownList id=ddl02 width=100% OnSelectedIndexChanged=LoadSubActivity  CssClass="fontObject" runat=server/>
						<asp:Label id=lblErr02 visible=false forecolor=red display=dynamic runat=server/>
					</td>
				</tr>				
				<tr>
					<td align="left" valign=top><asp:Label id=lbl03 visible=true display=dynamic runat=server/></td>
					<td align="left" valign=top>
						<asp:DropDownList id=ddl03 width=100% OnSelectedIndexChanged=LoadSubActivity  CssClass="fontObject" runat=server/>
						<asp:Label id=lblErr03 visible=false forecolor=red display=dynamic runat=server/>
					</td>
				</tr>				
				<tr>
					<td align="left" valign=top><asp:Label id=lbl04 visible=true display=dynamic runat=server/></td>
					<td align="left" valign=top>
						<asp:DropDownList id=ddl04 width=100% OnSelectedIndexChanged=LoadSubActivity  CssClass="fontObject" runat=server/>
						<asp:Label id=lblErr04 visible=false forecolor=red display=dynamic runat=server/>
					</td>
				</tr>	
				<tr id=rowpurpose >
					<td align=left valign=top>Purpose :*</td>
					<td align=left valign=top>
						<asp:radiobuttonlist id=rdPurpose runat=server autopostback=true OnSelectedIndexChanged=Change_Purpose>
							<asp:listitem id="option1" value="1" text="Non Vehicle related" runat="server" />
							<asp:listitem id="option2" value="2" text="Vehicle related with distribution" runat="server" />
						</asp:radiobuttonlist>
					</td>
					<td colspan="4">&nbsp;</td>
				</tr>
				<tr id=rowNurseryUse>
					<td align=left valign=top>For Capital Expenditure : </td>
					<td align=left valign=top>
						<asp:Checkbox id=cbNursery text=" Yes" runat=server />
					</td>
					<td colspan="4">&nbsp;</td>
				</tr>				
				<tr id=rowWorkshopUse>
					<td width=22% align=left valign=top>Workshop Account Distribution Use : </td>
					<td align=left valign=top>
						<asp:Checkbox id=cbWSAccDist text=" Yes" autopostback=true oncheckedchanged=Change_WSAccDist runat=server />
					</td>
					<td colspan="4">&nbsp;</td>
				</tr>
				
					<tr id=rowMedicalUse>
					<td width=22% align=left valign=top>Medical Account Distribution Use : </td>
					<td align=left valign=top>
						<asp:Checkbox id=cbMedAccDist text=" Yes" autopostback=true oncheckedchanged=Change_MedAccDist runat=server />
					</td>
					<td colspan="4">&nbsp;</td>
				</tr>
				
				<tr id=rowHousingUse>
					<td width=22% align=left valign=top>Housing Account Distribution Use : </td>
					<td align=left valign=top>
						<asp:Checkbox id=cbHousingAccDist text=" Yes" autopostback=true oncheckedchanged=Change_HousingAccDist runat=server />
					</td>
					<td colspan="4">&nbsp;</td>
				</tr>
				
				<tr>
					<td align=left valign=top><asp:label id="lblFinAccCode" runat="server" /> : </td>
					<td align=left valign=top>
						<asp:textbox id=txtFinAccCode width=50% runat=server />
					</td>
					<td colspan="4">&nbsp;</td>
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
			</table>
				
			<Input Type=Hidden id=tbcode runat=server />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<asp:label id=lblAccClass visible=false runat=server />
			<asp:label id=lblActivity visible=false runat=server />
			<asp:label id=lblSubActivity visible=false runat=server />
			<asp:label id=lblExpense visible=false runat=server />
			<asp:label id=lblDdlActivity visible=false runat=server />
			<asp:label id=lblCode text=" Code" visible=false runat=server />
			<asp:label id=lblPleaseEnter text="Please enter " visible=false runat=server />
			<asp:label id=lblPleaseSelect text="Please select " visible=false runat=server />
			<asp:label id=lblSelect text="Select " visible=false runat=server />
			<asp:label id=lblEnter text="Enter " visible=false runat=server />
			<asp:label id=lblType text=" Type" visible=false runat=server />
			<asp:label id=lblAnd text=" and " visible=false runat=server />
			<asp:label id=lblCodeShouldBeIn text=" Code should be in " visible=false runat=server />
			<asp:label id=lblCharacter text=" character(s)." visible=false runat=server />
		</form>
	</body>
</html>

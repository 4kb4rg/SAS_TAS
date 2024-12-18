<%@ Page Language="vb" trace="False" src="../../../include/BD_trx_NurseryActivityDist_Distribute.aspx.vb" Inherits="BD_NurseryActivity_Dist" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Nursery Activity Calenderisation</title>
	</head>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<body onload="this.focus()" >
		    <form runat="server" ID="Form1">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:label id="sortcol" Visible="False" Runat="server" />
			<asp:label id="lblAccCode" visible=False runat="server"/>
			<asp:label id="lblNurseryActDistID" visible=False runat="server"/>
			<asp:label id="lblCode" visible=False text=" Code" runat="server"/>
			<Input type=hidden id=hidDistByBlk value="" runat=server />
			<Input type=hidden id=hidBlkCode value="" runat=server />
			<table border="0" cellspacing="1" cellpadding="1" width="100%" runat=server>
				<tr>
					<td class="mt-h" colspan="4" width=60%><asp:Label id="lblTitle" runat="server" /></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<!--<tr>
					<td colspan=6>&nbsp;</td>
				</tr>	-->			
				<tr>
					<td colspan="4" width=60%><asp:label id="lblLocTag" runat="server"/> : <asp:label id="lblLocCode" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4" width=60%><asp:label id="lblBgtTag" text="Budgeting Period " runat="server"/> : <asp:label id="lblBgtPeriod" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4" width=60%><asp:label id="lblBlockTag" runat="server"/> : <asp:label id="lblBlkCode" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr id="RowSubBlk">
					<td colspan="4" width=60%><asp:label id="lblSubBlkTag" runat="server"/> : <asp:label id="lblSubBlkCode" runat="server" /></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>																		
				<tr>
					<td colspan="4" width=60%><asp:label id="lblFigTag" text="Distribution Amount " runat="server"/> : <asp:label id="lblDistFig" runat="server"/></td>
					<td align="right" colspan="2" width=40%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="3" width=25%>Distribution Method</td>
					<td colspan="3"  width=30% >
					<asp:DropDownList id="ddlDistribute" AutoPostback=True OnSelectedIndexChanged=ddlDistributeSelect width=100%  runat=server/>
							<asp:RequiredFieldValidator 
								id="validateddl" 
								runat="server" 
								ErrorMessage="<BR>Please select a method " 
								ControlToValidate="ddlDistribute" 
								display="dynamic"/>
					</td>
				</tr>
				<tr>
					<TD colspan = 6 align=center>					
							<asp:Label id=lblOvrMsgTop Text="Number Too Big" Forecolor=Red Visible=False runat=server />
							<asp:Label id=lblFigureErrTop Text="Distribution Figure not fulfilled" Forecolor=Red Visible=False runat=server />
							<asp:Label id=lblPrcntErrTop Text="Distribution Percentage not fulfilled" Forecolor=Red Visible=False runat=server />
					</td>
				</tr>				
				<tr>
					<TD colspan = 6 align=center>					
					<asp:DataGrid id="MonthList"
						AutoGenerateColumns="false" width="60%" runat="server"
						GridLines = none
						Cellpadding = "2"
						Pagerstyle-Visible="False">
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />						
					<Columns>
					
					<asp:TemplateColumn HeaderText="Month" >
						<ItemTemplate>
							<asp:label id="lblMonth" Text='<%# Container.DataItem("AccMonth") %>' runat="server"/> / 
							<asp:label id="lblYear" Text='<%# Container.DataItem("AccYear") %>' runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
	
					<asp:TemplateColumn HeaderText="Figure" >
						<ItemTemplate>
							<asp:TextBox id="TxFig" width=100% maxlength="25" runat="server"/>						
							<asp:RegularExpressionValidator id="RegularExpressionValidatorQtyReq" 
								ControlToValidate="TxFig"
								ValidationExpression="\d{1,19}"
								Display="Dynamic"
								text = "Maximum length 19 digits and 0 decimal points"
								runat="server"/>
							<asp:RangeValidator id="Range1"
								ControlToValidate="TxFig"
								MinimumValue="-9999999999999999999"
								MaximumValue="9999999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="The value is out of range !"
								runat="server" display="dynamic"/>
							<asp:RequiredFieldValidator 
								id="validateCost" 
								runat="server" 
								ErrorMessage="<BR>Please Specify Cost to adjust" 
								ControlToValidate="TxFig" 
								display="dynamic"/>

						</ItemTemplate>
					</asp:TemplateColumn>
					</Columns>
					</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<TD colspan = 6 align=center>					
							<asp:Label id=lblOvrMsg Text="Number Too Big" Forecolor=Red Visible=False runat=server />
							<asp:Label id=lblFigureErr Text="Distribution Figure not fulfilled" Forecolor=Red Visible=False runat=server />
							<asp:Label id=lblPrcntErr Text="Distribution Percentage not fulfilled" Forecolor=Red Visible=False runat=server />
					</td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<asp:ImageButton id=ibConfirm imageurl="../../images/butt_confirm.gif" onClick=btnConfirm_Click AlternateText=Confirm runat="server"/>
						<asp:ImageButton id=ibCancel imageurl="../../images/butt_cancel.gif" onClick=btnCancel_Click  AlternateText=Cancel CausesValidation=false runat="server"/>
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</html>

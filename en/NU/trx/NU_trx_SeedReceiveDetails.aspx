<%@ Page Language="vb" Src="../../../include/NU_trx_SeedReceiveDetails.aspx.vb" Inherits="NU_trx_SeedReceiveDetails" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuNUTrx" src="../../menu/menu_NUtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>NURSERY - Seeds Receive Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id="frmMain" runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">

				

			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="lblOper" Visible="False" Runat="server" />
			<table cellpadding="2" cellsapcing=0 width="100%" border="0" class="font9Tahoma">
 				<tr>
					<td colspan="6"><UserControl:MenuNUTrx id=MenuNUTrx runat="server" /></td>
				</tr>
				<tr>
					<td  colspan="6"><strong>SEEDS RECEIVE DETAILS</strong> </td>
				</tr>
				<tr>
					<td colspan=6>
					     <hr style="width :100%" />
                    </td>
				</tr>				
				<tr>
					<td width="25%" height=25><asp:label id=lblTxIDTag text="Receive ID" Runat="server"/> :*</td>
					<td width="30%">
					<asp:label id=lblTxID visible="False" Runat="server"/>
					<asp:DropDownList id="ddlTxID" Width=70% AutoPostBack=True OnSelectedIndexChanged=CallFillBatchNo runat=server />
					</td>
					<td width="5%">&nbsp;</td>
					<td width="15%">Period : </td>
					<td width="20%"><asp:Label id="lblAccPeriod" runat="server"/></td>
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblBlkCodeTag text="Nursery Block Code" Runat="server"/> :*</td>
					<td><asp:label id="lblBlkCode" Width=100% runat=server />
					    <asp:DropDownList id="ddlBlkCode" Width=70% AutoPostBack=True OnSelectedIndexChanged=CallFillBatchNo visible=False runat=server />
						<asp:label id=lblBlkCodeErr Visible=False forecolor=red Runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>Status :</td>
					<td><asp:Label id="lblStatus" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblBatchNoTag text="Batch No" Runat="server"/> :*</td>
					<td><asp:label id="lblBatchNo" Width=100% runat=server />
					    <asp:DropDownList id="ddlBatchNo" Width=100% AutoPostBack=True OnSelectedIndexChanged=CallRefreshBatchNo visible=False runat=server />
						<asp:label id=lblBatchNoErr Visible=False forecolor=red Runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label id="lblCreateDate" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Receive Date :*</td>
					<td>
						<asp:TextBox id="txtDate" runat="server" width=70% maxlength="10"/>                       
						<a href="javascript:PopCal('txtDate');">
						<asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../../Images/calendar.gif"/></a>					
						<asp:RequiredFieldValidator 
							id="rfvDate" 
							runat="server"  
							ControlToValidate="txtDate" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:label id=lblDate Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
						<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
						<asp:label id="lblDupMsg"  Text="Transaction for today already exist" Visible=false forecolor=red Runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id="lblLastUpdate" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Quantity :*</td>
					<td valign=center><asp:TextBox id="txtQty" runat="server" width=100% maxlength="9"/>
						<asp:RequiredFieldValidator 
							id="rfvQty" 
							runat="server"  
							ControlToValidate="txtQty" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revQty" 
							ControlToValidate="txtQty"
							ValidationExpression="\d{1,9}"
							Display="Dynamic"
							text = "Maximum length 9 digits"
							runat="server"/>
						<asp:RangeValidator id="rvQty"
							ControlToValidate="txtQty"
							MinimumValue="1"
							MaximumValue="9999999999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>Update By : </td>
					<td><asp:Label id="lblUpdateBy" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
				<td height=25>Plant Material :</td>
					<td valign=center><asp:TextBox id="txtPlantMat" runat="server" width=100% maxlength="64"/>
					</td>
				</tr>	
				<tr>
				<td height=25>DO Ref :</td>
					<td valign=center><asp:TextBox id="txtDORef" runat="server" width=100% maxlength="16"/>
					</td>
				</tr>	
				
				<!-- #2- START -->
				<tr>
					<td height=25><asp:label id=lblCostSeedsTag text="Cost per Seeds " Runat="server"/> :*</td>
					<td><asp:label id=lblCostSeeds Runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<!-- #2- END -->
				
				<tr>
					<td colspan=6><asp:label id=lblDeleteErr text="Insufficient quantity in the batch to perform operation!" Visible=False forecolor=red Runat="server" /></td>				
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id="Confirm" imageurl="../../images/butt_confirm.gif" onclick="btnConfirm_Click" runat="server" AlternateText="Confirm"/>
						<asp:ImageButton id="Delete" imageurl="../../images/butt_delete.gif" onclick="btnDelete_Click" runat="server" CausesValidation="False" AlternateText="Delete"/>
						<asp:ImageButton id="Back" imageurl="../../images/butt_back.gif" onclick="btnBack_Click" runat="server" AlternateText="Back" CausesValidation="False"/>
					</td>
				</tr>
				<tr>
					<td colspan="6">
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

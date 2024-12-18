<%@ Page Language="vb" Src="../../../include/NU_trx_SeedTransplantDetails.aspx.vb" Inherits="NU_trx_SeedTransplantDetails" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuNUTrx" src="../../menu/menu_NUtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>NURSERY - Seedlings Transplanting To Main Nursery Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id="frmMain" runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="lblOper" Visible="False" Runat="server" />
			<table cellpadding="2" cellspacing=0 width="100%" border="0" class="font9Tahoma">
 				<tr>
					<td colspan="6"><UserControl:MenuNUTrx id=MenuNUTrx runat="server" /></td>
				</tr>
				<tr>
					<td   colspan="6"><strong>SEEDLINGS TRANSPLANTING DETAILS</strong> </td>
				</tr>
				<tr>
					<td colspan=6>
                        <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td width="20%" height=25><asp:label id=lblTxIDTag text="Transplanting ID" Runat="server"/> :*</td>
					<td width="25%"><asp:label id=lblTxID Runat="server"/></td>
					<td width="5%">&nbsp;</td>
					<td width="15%">Period : </td>
					<td width="20%"><asp:Label id="lblAccPeriod" runat="server"/></td>
					<td width="5%">&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Transplanting Date :*</td>
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
					<td>Date Created :</td>
					<td><asp:Label id="lblCreateDate" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblBlkCodeTag text="Nursery Block Code" Runat="server"/> :*</td>
					<td><asp:DropDownList id="ddlBlkCode" Width=100% AutoPostBack=True OnSelectedIndexChanged=CallFillBatchNo runat=server />
						<asp:label id=lblBlkCodeErr Visible=False forecolor=red Runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>Status :</td>
					<td><asp:Label id="lblStatus" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>From Pre-Nursery Batch No :*</td>
					<td><asp:DropDownList id="ddlBatchNo" Width=100%  AutoPostBack=True OnSelectedIndexChanged=CallRefreshBatchNo runat=server />
						<asp:label id=lblBatchNoErr Visible=False forecolor=red Runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id="lblLastUpdate" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
			
				<tr>
					<td height=25>To Main Nursery Batch No :*</td>
					<td><asp:DropDownList id="ddlToBatchNo" Width=100% runat=server />
						<asp:label id=lblToBatchNoErr Visible=False forecolor=red Runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>Update By : </td>
					<td><asp:Label id="lblUpdateBy" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
			
				<tr>
					<td height=25>Quantity - Seedlings :*</td>
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
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<!-- #2- START -->
				<tr>
					<td height=25><asp:label id=lblCostSeedsTag text="Cost per Seeds " Runat="server"/> :*</td>
					<td><asp:label id=lblCostSeeds Runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<!-- #2- END -->
				<tr>
			        <td height=25 valign=top>Additional Note :</td>
                    <td colSpan="5"><textarea rows=6 id=txtAddNote cols=60 runat=server></textarea></td>
                    <td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
			    </tr>
				<tr>
					<td colspan=6><asp:label id=lblDeleteErr text="Insufficient quantity in the batch to perform operation !" Visible=False forecolor=red Runat="server" />
					</td>				
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

<%@ Page Language="vb" Trace=False Src="../../../include/WS_DirectMaster_Details.aspx.vb" Inherits="WS_DirectMasterDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWSSetup" src="../../menu/menu_WSsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
    <title>Workshop Direct Charge Master Details</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <Preference:PrefHdl id=PrefHdl runat="server" />
</head>
<body>
    <form id="frmSysSetting" runat="server"  class="main-modul-bg-app-list-pu"> 

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<asp:label id=ErrorMessage visible=false runat=server />
		<asp:label id=lblCode visible=false text=" Code" runat=server />
		<asp:label id=lblChargeTo visible=false text="Charge to " runat=server />
		<asp:label id=lblPleaseEnter visible=false text="Please enter " runat=server />
        <asp:Label id="DiffAverageCost" runat="server" Visible="False"/>
        <asp:Label id="blnUpdate" runat="server" Visible="False"/>
        <table cellpadding="1" width="100%" border="0" class="font9Tahoma">
 			<tr>
				<td colspan="6"><UserControl:MenuWSSetup id=menuWS runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan="6">WORKSHOP <asp:label id="lblTitle" runat=server /> DETAILS</td>
		 	</tr>
			<tr>
				<td colspan=6><hr size="1" noshade></td>
			</tr>
			<tr>
                <td height=25><asp:label id="lblDCItemCode" Runat="server" /> :*</td>
                <td><asp:TextBox id="DCCode" runat="server" maxlength="20" width=50%/><Br>
                    <asp:RequiredFieldValidator 
						id="validateCode" 
						runat="server" 
						ControlToValidate="DCCode" 
						display="dynamic"/>
					<asp:RegularExpressionValidator id=revCode 
						ControlToValidate="DCCode"
						ValidationExpression="[a-zA-Z0-9\-]{1,20}"
						Display="Dynamic"
						text="<br>Alphanumeric without any space in between only."
						runat="server"/>
					<asp:label id="lblDupMsg" Text="Code already exist" Visible = false forecolor=red Runat="server"/>
                </td>
                <td>&nbsp;</td>
                <td colspan="2">Status :</td>
                <td><asp:Label id="Status" runat="server"/></td>
            </tr>
			<tr>
                <td height=25><asp:label id="lblDesc" runat=server /> :*</td>
                <td><asp:TextBox id="Desc" runat="server" maxlength="64"  width=100%/><Br>
                    <asp:RequiredFieldValidator 
						id="validateDesc" 
						runat="server"  
						ControlToValidate="Desc" 
						display="dynamic"/>
				</td>
                <td>&nbsp;</td>
                <td colspan="2">Date Created :</td>
                <td><asp:Label id="CreateDate" runat="server"/>&nbsp;</td>
            </tr>
			<tr>
                <td height=25>Unit Of Measurement :</td>
                <td><asp:DropDownList id="lstUOM" runat="server" width=100% /></td>
                <td>&nbsp;</td>
                <td colspan="2">Last Update :</td>
                <td><asp:Label id="UpdateDate" runat="server"/>
                </td>
            </tr>
			<tr>
                <!--td height=25><asp:label id="lblAccCodeTag" Runat="server" /> :</td>
                <td><asp:DropDownList id="lstAccCode" runat="server" width=100% size="1"/></td-->
                <td colspan=3>&nbsp;</td>
                <td colspan="2">Updated By :</td>
                <td><asp:Label id="UpdateBy" runat="server"/>
                </td>
            </tr>
			<tr>
                <td height=25><u><b>Financial Options</b></u></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td colspan="2"><u><b>Finance Account Number</b></u></td>
                <td>&nbsp;</td>
            </tr>
			<tr>
                <td width=15% height=25>Initial Cost :</td>
                <td width=30%><asp:label id="lblInitialCost" Runat="server" /></td>
                <td width=5%>&nbsp;</td>
                <td width=5%>&nbsp;</td>
				<td width=15%>Purchase A/C no :</td>
                <td width=25%><asp:TextBox id="purchaseACNo" runat="server" maxlength="32" width=100%/></td>
            </tr>
			<tr>
                <td height=25>Highest Cost :</td>
                <td><asp:Label id="lblHighestCost" runat="server"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>Issue A/C Number :</td>
                <td><asp:TextBox id="IssueACNo" runat="server" maxlength="32" width=100%/></td>
            </tr>
			<tr>
                <td height=25>Lowest Cost :</td>
                <td><asp:Label id="lblLowestCost" runat="server"/></td>
                <td>&nbsp;</td>
                <td colspan="2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
			<tr>
                <td height=25>Average Cost :</td>
                <td><asp:Label id="lblAvrgCost" runat="server"/></td>
                <td>&nbsp;</td>
                <td colspan="2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
			<tr>
                <td height=25>Latest Cost :</td>
                <td><asp:Label id="lblLatestCost" runat="server"/></td>
                <td>&nbsp;</td>
                <td colspan="2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="6">&nbsp;</td>
            </tr>
			<tr>
                <td colspan="6">
					<asp:ImageButton id="Save" imageurl="../../images/butt_save.gif" onclick="btnSave_Click" runat="server" alternatetext="Save"/>
                    <asp:ImageButton id="Delete" imageurl="../../images/butt_delete.gif" onclick="btnDelete_Click" runat="server" alternatetext="Delete"/>
                    <asp:ImageButton id="Undelete" imageurl="../../images/butt_undelete.gif" onclick="btnDelete_Click" runat="server" alternatetext="Undelete"/>
                    <asp:ImageButton id="Back" imageurl="../../images/butt_back.gif" onclick="btnBack_Click" runat="server" alternatetext="Back" CausesValidation="False"/>
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

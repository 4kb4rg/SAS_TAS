<%@ Page Language="vb" Trace="False" Src="../../../include/FA_Setup_AssetItem_Details.aspx.vb" Inherits="FA_AssetItemDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuFASetup" src="../../menu/menu_FAsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
    <title>Asset Item Details</title>
    <Preference:PrefHdl id=PrefHdl runat="server" />
       <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        </style>
</head>
<body>
    <form id="frmSysSetting" class="main-modul-bg-app-list-pu"  runat="server">

           <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

        <asp:Label id="ErrorMessage" runat="server"/>
		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
        <asp:Label id="DiffAverageCost" runat="server" Visible="False"/>
        <asp:Label id="blnUpdate" runat="server" Visible="False"/>
        <table cellpadding="2" width="100%" border="0" class="font9Tahoma">
 				<tr>
					<td colspan="6">
						<UserControl:MenuFASetup id=menuIN runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                  <strong>  <asp:label id="lblTitle" runat="server" /> DETAILS </strong></td>
                                <td class="font9Header" style="text-align: right">
                                    Status : <asp:Label id="Status" runat="server"/>&nbsp;| Date Created : <asp:Label id="CreateDate" runat="server"/>&nbsp;| Last Update : <asp:Label id="UpdateDate" runat="server"/>&nbsp;| Updated By : <asp:Label id="UpdateBy" runat="server"/>
                                </td>
                            </tr>
                        </table>
                         <hr style="width :100%" />
                    </td>
		 		</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
                    <td><asp:label id="lblDCCode" Runat="server" text="Asset Item Code "/> :*</td>
                    <td>
	                    <asp:DropDownList id="lstItemCode" width=180% runat="server" AutoPostback=True OnSelectedIndexChanged=LoadItemMaster size="1" CssClass="font9Tahoma"/>
                        <asp:TextBox id="DCCode" runat="server" Visible=False maxlength="20" width=180% CssClass="font9Tahoma" /><Br>
                        <asp:RequiredFieldValidator 
								id="validateCode" 
								runat="server" 
								ErrorMessage="Please Enter Asset Item Code" 
								ControlToValidate="DCCode" 
								display="dynamic"/>
						<asp:label id="lblDupMsg"  Text="Code already exist" Visible = false forecolor=red Runat="server"/>
								
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td colspan="1">&nbsp;</td>
                </tr>
				<tr>
                    <td><asp:label id="lblDescription" runat="server" /> :*</td>
                    <td>
                        <asp:TextBox id="Desc" runat="server" maxlength="64"  width=180% CssClass="font9Tahoma"/><Br>
                        <asp:RequiredFieldValidator 
								id="validateDesc" 
								runat="server" 
								ErrorMessage="Please Enter Description" 
								ControlToValidate="Desc" 
								display="dynamic"/>
					</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td colspan="1">&nbsp;</td>
                </tr>
				<tr>
                    <td><asp:label id="Label2" Runat="server" text="Unit Of Measurement"/> :</td>
                    <td><asp:DropDownList id="lstUOM" runat="server" width=180% size="1" CssClass="font9Tahoma"/></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td colspan="1">&nbsp;</td>
                </tr>
				<tr>
                    <td><asp:label id="lblAccount" Runat="server" text="Inventory Account"/> :</td>
                    <td><asp:DropDownList id="lstAccCode" runat="server" width=180% size="1" CssClass="font9Tahoma"/></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td colspan="1">&nbsp;</td>
                </tr>
                
				<tr>
                    <td ><u><b>Financial Options</b></u></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td colspan="2"><u><b>Finance Account Number</b></u></td>
                    <td>&nbsp;</td>
                </tr>
				<tr>
                    <td width=15%><asp:label id="Label5" Runat="server" text="Initial Cost "/> :</td>
                    <td width=30%><asp:label id="lblInitialCost" Runat="server" /></td>
                    <td width=5%>&nbsp;</td>
                    <td width=5%>&nbsp;</td>
					<td width=15%><asp:label Runat="server" text="Purchase A/C no :"/></td>
                    <td width=25%><asp:TextBox id="purchaseACNo" runat="server" maxlength="32" width=100% CssClass="font9Tahoma"/></td>
                </tr>
				<tr>
                    <td><asp:label id="Label6" Runat="server" text="Highest Cost"/> :</td>
                    <td><asp:Label id="lblHighestCost" runat="server"/></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td><asp:label Runat="server" text="Issue A/C Number"/></td>
                    <td><asp:TextBox id="IssueACNo" runat="server" maxlength="32" width=100% CssClass="font9Tahoma"/></td>
                </tr>
				<tr>
                    <td height=20><asp:label id="Label7" Runat="server" text="Lowest Cost"/> :</td>
                    <td><asp:Label id="lblLowestCost" runat="server"/></td>
                    <td>&nbsp;</td>
                    <td colspan="2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
				<tr>
                    <td height=20><asp:label Runat="server" text="Average Cost"/> :</td>
                    <td><asp:Label id="lblAvrgCost" runat="server"/></td>
                    <td>&nbsp;</td>
                    <td colspan="2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
				<tr>
                    <td height=20><asp:label Runat="server" text="Latest Cost"/> :</td>
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
                        <asp:ImageButton id="Delete" CausesValidation="false" imageurl="../../images/butt_delete.gif" onclick="btnDelete_Click" runat="server" alternatetext="Delete"/>
                        <asp:ImageButton id="SynchronizeData" imageurl="../../images/butt_synchronize_data.gif" onclick="btnSynchronizeData_Click" runat="server" alternateText="Synchronize Data"/>
                        <asp:ImageButton id="Back" imageurl="../../images/butt_back.gif" onclick="btnBack_Click" runat="server" alternatetext="Back" CausesValidation="False"/>
                        <br />
                    </td>
                </tr>
        </table>
    </div>
    </td>
    </tr>
    </table>
    </form>
</body>
</html>

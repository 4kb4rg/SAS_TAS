<%@ Page Language="vb" src="../../../include/PR_Setup_VehicleDet_Estate.aspx.vb" Inherits="PR_Setup_VehicleDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_GLsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Vehicle Details</title>
                <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain  class="main-modul-bg-app-list-pu"  runat="server">
<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
<tr>
	<td style="width: 100%; height: 1500px" valign="top">
	<div class="kontenlist">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<Input Type=Hidden id=tbcode runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuGLSetup id=MenuGLSetup runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">
                        &nbsp;<table cellpadding="0" cellspacing="0" 
                            class="style1">
                            <tr>
                                <td class="font9Tahoma">
     <strong>KENDARAAN DETAILS</strong> </td>
                                <td class="font9Header" style="text-align: right">
                                    Status : <asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Last Update : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Updated By : <asp:Label id=lblUpdatedBy runat=server />
                                </td>
                            </tr>
                        </table>
                    </td>
				</tr>
				<tr>
					<td colspan=6>
                    <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td width=20% height=25>
                        &nbsp;Kendaraan Code:*
                    </td>
					<td width=30%>
						<asp:Textbox id=txtVehicleCode width=50% maxlength=8 CssClass="font9Tahoma" runat=server/>
						<asp:dropdownlist id=ddlVehicleCode width=50% maxlength=8 OnSelectedIndexChanged="ddlVehicleCode_OnSelectedIndexChanged" AutoPostBack=true   CssClass="font9Tahoma"  runat=server/>
						<input type="hidden" runat="server" id="hidRecStatus">
						<input type="hidden" runat="server" id="hidOriVehCode">
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
							ControlToValidate=txtVehicleCode />
						
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>Silakan pilih kendaraan code." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>&nbsp;</td>
					<td width=25%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>
                        &nbsp;No.Polisi:*</td>
					<td><asp:Textbox id=txtPol maxlength=25 width=100%  CssClass="font9Tahoma"  runat=server/>
					<asp:RequiredFieldValidator id=validatePol display=Dynamic runat=server 
							ControlToValidate=txtPol />
						</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>
                        &nbsp;Deskripsi:*</td>
					<td><asp:Textbox id=txtDescription maxlength=128 width=100%  CssClass="font9Tahoma"  runat=server/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
							ControlToValidate=txtDescription />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>
                        &nbsp;Type :*</td>
					<td><asp:dropdownlist id=ddlVehType width=100%   CssClass="font9Tahoma"  runat=server/>
						<asp:Label id=lblErrVehType visible=false forecolor=red runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td height=25>Model : </td>
					<td><asp:textbox id=txtModel maxlength=32 width=50%  CssClass="font9Tahoma"  runat=server /></td>
					<td>&nbsp;</td>
					<td>Tgl. Pembelian : </td>
					<td><asp:textbox id=txtPurchaseDate maxlength=10 width=50%  CssClass="font9Tahoma"  runat=server />
					    <a href="javascript:PopCal('txtPurchaseDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:label id=lblDate Text="<br>Date entered should be in the format " forecolor=red Visible=false displaye=dynamic Runat="server"/> 
						<asp:label id=lblFmt forecolor=red Visible=false display=dynamic Runat="server"/></td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td height=25>HP/CC - Tahun &nbsp;</td>
					<td><asp:textbox id=txtHPCC maxlength=16 width=50%  CssClass="font9Tahoma"  runat=server />
                        -
                        <asp:textbox id=txtthn maxlength=4 width="30%"  CssClass="font9Tahoma"  runat=server /></td>
					<td>&nbsp;</td>
					<td> </td>
					<td>
                        &nbsp;<a href="javascript:PopCal('txtPurchaseDate');"></a>
					</td>
					<td>&nbsp;</td>						
				</tr>
				
				<tr>
					<td height=25>Running in UOM :*</td>
					<td><GG:AutoCompleteDropDownList id=ddlUOM width="100%"  CssClass="font9Tahoma"  runat=server/>
						<asp:Label id=lblErrUOM visible=false forecolor=red text="Please select Unit of Measurement." display=dynamic runat=server/>
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>
                        &nbsp;COA Upah:*</td>
					<td><GG:AutoCompleteDropDownList id=ddlAccCode width="100%"  CssClass="font9Tahoma"  runat=server OnSelectedIndexChanged="ddlAccCode_OnSelectedIndexChanged" AutoPostBack=true/> 
						<asp:Label id=lblErrAccCode visible=false forecolor=red display=dynamic runat=server/>
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>
                        &nbsp;COA Premi:*</td>
					<td><GG:AutoCompleteDropDownList id=ddlAccCode_premi width="100%"  CssClass="font9Tahoma"  runat=server /> 
						</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>
                        &nbsp;COA Astek:*</td>
					<td><GG:AutoCompleteDropDownList id=ddlAccCode_astek width="100%"  CssClass="font9Tahoma"  runat=server /> 
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>
                        &nbsp;COA JHT:*</td>
					<td><GG:AutoCompleteDropDownList id=ddlAccCode_jht width="100%"  CssClass="font9Tahoma"  runat=server /> 
						</td>
					<td colspan=3>&nbsp;</td>
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
					    <br />
					</td>
				</tr>
			</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
 	</div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>

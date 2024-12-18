<%@ Page Language="vb" src="../../../include/gl_trx_CTDet.aspx.vb" Inherits="gl_trx_CTDet" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Contractor Work Order Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
          <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            .button-small {
	border: thin #009EDB solid;
	text-align:center;
	text-decoration:none;
	padding: 5px 10px 5px 10px;
	font-size: 7pt;
	font-weight:bold;
	color: #FFFFFF;
	background-color: #009EDB;
}
                        
            .button-small {
	border: thin #009EDB solid;
	text-align:center;
	text-decoration:none;
	padding: 5px 10px 5px 10px;
	font-size: 7pt;
	font-weight:bold;
	color: #FFFFFF;
	background-color: #009EDB;
}
            
            .button-small {
	border: thin #009EDB solid;
	text-align:center;
	text-decoration:none;
	padding: 5px 10px 5px 10px;
	font-size: 7pt;
	font-weight:bold;
	color: #FFFFFF;
	background-color: #009EDB;
}
            
            .button-small {
	border: thin #009EDB solid;
	text-align:center;
	text-decoration:none;
	padding: 5px 10px 5px 10px;
	font-size: 7pt;
	font-weight:bold;
	color: #FFFFFF;
	background-color: #009EDB;
}
            </style>
	</head>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>
             <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

		<table id="tblHeader" cellSpacing="0" cellPadding="2" width="100%" border="0" class="font9Tahoma">
			<tr>
				<td class="mt-h" colspan="5"><table cellpadding="0" 
                        cellspacing="0" class="style1">
                    <tr>
                        <td class="font9Tahoma">
                         <strong>  CONTRACTOR WORK ORDER DETAILS</strong> </td>
                        <td class="font9Header" style="text-align: right">
                            Status : <asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Last Update : <asp:Label ID=lblLastUpdate runat=server />&nbsp;Updated By : 
                            | <asp:Label ID=lblUpdatedBy runat=server /></td>
                    </tr>
                    </table>
                </td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
				<td height=25 width="20%">
                    Work Order ID :</td>
				<td width="30%"><asp:TextBox id=txtWorkOrderId width=100% maxlength=20 runat=server />
                    <asp:RequiredFieldValidator ID="reqWorkOrderId" runat="server" ErrorMessage="Please fill work order id" ControlToValidate="txtWorkOrderId" Display="Dynamic"></asp:RequiredFieldValidator></td>
				<td width="5%"></td>
				<td width="20%"> &nbsp;</td>
				<td width="20%">&nbsp;</td>
				<td width="5%"></td>
			</tr>
			<tr>
				<td height=25>Supplier Code :* </td>
				<td>
                    <asp:DropDownList id=ddlSupplier width=100%  runat=server />
					<asp:Label id=lblErrSupplier forecolor=red visible=true text="Please select Supplier Code" runat=server/></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td style="height: 25px">
                    Start Date : *</td>
				<td style="height: 25px"><asp:TextBox id=txtStartDate width=50% maxlength=10 runat=server />
					<a href="javascript:PopCal('txtStartDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:Label id=lblErrDate visible=False forecolor=Red text="<br>Date format should be in " runat=server/>
				</td>
				<td style="height: 25px">&nbsp;</td>
				<td style="height: 25px">&nbsp;</td>
				<td style="height: 25px">&nbsp;</td>
				<td style="height: 25px">&nbsp;</td>
			</tr>
			<tr>
				<td height=25>
                    Expected Date : *</td>
				<td><asp:TextBox id=txtExpectedDate width=50% maxlength=10 runat=server />
				<a href="javascript:PopCal('txtExpectedDate');"><asp:Image id="btnSelDate1" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
				<asp:Label id=lblErrDate1 visible=false forecolor=red text="<br>Date format should be in " runat=server/></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>
                    Finish Date : *</td>
				<td><asp:TextBox id=txtFinishDate width=50% maxlength=10 runat=server />
				<a href="javascript:PopCal('txtFinishDate');"><asp:Image id="btnSelDate2" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
				<asp:Label id=lblErrDate2 visible=false forecolor=red text="<br>Date format should be in " runat=server/></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td style="height: 25px">
                    Contractor Contact</td>
				<td style="height: 25px">
                    <asp:TextBox id=txtContact width=100% maxlength=25 runat=server /></td>
				<td style="height: 25px">&nbsp;</td>
				<td style="height: 25px"> </td>
				<td style="height: 25px"></td>
				<td style="height: 25px">&nbsp;</td>
			</tr>
			<tr>
				<td valign =top style="height: 75px">Remarks : </td>
				<td valign =top colSpan="5" style="height: 75px"><asp:TextBox ID=txtRemark maxlength=200 width=80% Height = "100%" Runat=server TextMode="MultiLine" /></td>
			</tr>
			<tr>
				<td style="height: 25px">Amount : </td>
				<td colSpan="5" style="height: 25px"><asp:TextBox ID=txtAmount maxlength=200 width=30%  Runat=server /></td>
			</tr>
			
			<tr>
				<td colSpan="6">&nbsp;</td>
			</tr>			
			<tr>
				<td colSpan="6">
					<asp:ImageButton ID=SaveBtn onclick=SaveBtn_Click ImageUrl="../../images/butt_save.gif" CausesValidation=false AlternateText=Save Runat=server /> 
					<asp:ImageButton ID=RefreshBtn CausesValidation=False onclick=RefreshBtn_Click ImageUrl="../../images/butt_refresh.gif" AlternateText=Refresh Runat=server />
					<asp:ImageButton ID=ConfirmBtn onclick=ConfirmBtn_Click ImageUrl="../../images/butt_confirm.gif" CausesValidation=false AlternateText=Confirm Runat=server />
					<asp:ImageButton ID=PrintBtn ImageUrl="../../images/butt_print.gif" AlternateText=Print visible=false Runat=server />
					<asp:ImageButton ID=CancelBtn onclick=CancelBtn_Click ImageUrl="../../images/butt_cancel.gif" AlternateText=Cancel Runat=server />
					<asp:ImageButton ID=DeleteBtn onclick=DeleteBtn_Click CausesValidation=false ImageUrl="../../images/butt_delete.gif" AlternateText=Delete Runat=server />
					<asp:ImageButton ID=UnDeleteBtn onclick=UnDeleteBtn_Click ImageUrl="../../images/butt_undelete.gif" AlternateText=Undelete Runat=server />
					<asp:ImageButton ID=BackBtn CausesValidation=False onclick=BackBtn_Click ImageUrl="../../images/butt_back.gif" AlternateText=Back Runat=server />
					<Input type=hidden id=cnid value="" runat=server />
                    <br />
                    </td>
			</tr>
			<asp:Label id=lblStatusHidden visible=false runat=server />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>

<%@ Page Language="vb" src="../../../include/PU_trx_PrintDocs.aspx.vb" Inherits="PU_trx_PrintDocs" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<head>
    <title>Print Documents</title> 
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <Script Language="Javascript">
		function keypress() {
			if (event.keyCode == 27)	//escape key press
				window.close();			//close window
		}
    </Script>
    <style type="text/css">
        .style1
        {
            width: 22%;
        }
    </style>
</head>
<Preference:PrefHdl id=PrefHdl runat="server" />

<body onload="javascript:self.focus();document.frmMain.txtFromId.focus();" leftmargin="2" topmargin="2">
    <form id=frmMain class="main-modul-bg-app-list-pu" runat="server" >

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 470px" valign="top">
			    <div class="kontenlist">



    <asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<table id=tblMain width=100% border=0 cellspacing="1" cellpadding="1"  runat=server class="font9Tahoma">
			<tr>
				<td colspan=2 class="font12Tahoma"><strong> <asp:Label id=lblDocName runat=server/></strong></td>
			</tr>
		<tr>
				<td colspan="5" style="width: 49%">
                        <hr style="width :100%" />
                </td>
			</tr>	
			
			<tr>
				<td class="style1">From : </td>
				<td width=39%><asp:TextBox id=txtFromId width=100% maxlength=25 CssClass="fontObject" runat=server />
					<asp:Label id=lblErrCodeFrom visible=false forecolor=red text="Invalid code given.<br>" runat=server/></td>		
				<td>&nbsp;</td>
				<td width=8%>To : </td>
				<td width=34%><asp:TextBox id=txtToId width=100% maxlength=25 CssClass="fontObject"  runat=server/>
					<asp:Label id=lblErrCode visible=false forecolor=red text="Invalid code given.<br>" runat=server/></td>
			</tr>	
			<tr>
				<td class="style1">Periode : </td>
				<td width=39%><asp:TextBox id=txtDateFrom width=50% maxlength=10 CssClass="fontObject"  runat=server />
							<a href="javascript:PopCal('txtDateFrom');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
							<asp:Label id=lblDateFrom visible=False forecolor=red text="<br>Invalid date format." runat=server />
							<asp:RegularExpressionValidator id="rfvDateFrom" 
							runat="server" 
							ControlToValidate="txtDateFrom" 
							display="dynamic"/></td>
				<td>&nbsp;</td>
				<td width=8%>To : </td>
				<td width=34%><asp:Label id="lblDateTo" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />				
							  <asp:Label id="lblDateFormatTo" forecolor=red visible="false" runat="server" /></td>
			</tr>
			<tr id=TrPOType visible=false>
				<td class="style1">PO Type : </td>
				<td width=30%><asp:dropdownlist id=ddlPOType CssClass="fontObject" runat=server /></td>
			</tr>
			<tr id=TrGRNType visible=false>
				<td class="style1">GRN Type : </td>
				<td width=30%><asp:dropdownlist id=ddlGRNType CssClass="fontObject" runat=server /></td>
			</tr>
			<tr id=TrDAType visible=false>
				<td class="style1">DA Type : </td>
				<td width=30%><asp:dropdownlist id=ddlDAType CssClass="fontObject" runat=server /></td>
			</tr>
			<tr id=TrRPHType visible=false>
				<td class="style1">RPH Type : </td>
				<td width=30%><asp:dropdownlist id=ddlRPHType CssClass="fontObject" runat=server /></td>
			</tr>
			<tr id=TrPOPIC1 visible=false>
				<td class="style1">PIC I for Sign Of: </td>
				<td width=30%><asp:textbox id="txtPIC1"  maxlength=128 width=50%  CssClass="fontObject" runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr id=TrPOJbt1 visible=false>
				<td class="style1">Jabatan: </td>
				<td width=30%><asp:textbox id=txtJabatan1  maxlength=128 width=50% CssClass="fontObject" runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr id=TrPOPIC2 visible=false>
				<td class="style1">PIC II for Sign Of: </td>
				<td width=30%><asp:textbox id=txtPIC2 maxlength=128 width=50% CssClass="fontObject" runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr id=TrPOJbt2 visible=false>
				<td class="style1">Jabatan: </td>
				<td width=30%><asp:textbox id=txtJabatan2 maxlength=128 width=50% CssClass="fontObject" runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr id=TrPOCat visible=false>
				<td class="style1">Catatan : </td>
				<td width=39%><asp:TextBox id=txtCatatan size=1 width=100% Text="Barang yang di supply dalam keadaan baik dan baru" textmode="Multiline" CssClass="fontObject" runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr id=TrPOPeriod visible=false>
				<td class="style1">Tanggal Pengiriman : </td>
				<td width=39%><asp:textbox id=txtSentPeriod maxlength=128 width=100% CssClass="fontObject" runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr id=TrSyarat visible=True>
				<td class="style1">Syarat Pembayaran : </td>
				<td width=39%><asp:textbox id=TxtSyaratBayar Text=" - " maxlength=128 width=100% textmode="Multiline" CssClass="fontObject" runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr id=TrPOLok visible=false>
				<td class="style1">Tempat Penyerahan : </td>
				<td width=39%><asp:DropDownList id="ddlSentLoc" AutoPostBack="True" onSelectedIndexChanged="SentLocChanged" width=100% CssClass="fontObject" runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr id=TrPOLokDet visible=false>
			    <td class="style1"></td>
				<td width=39%><asp:TextBox id=txtLokasi size=1 width=100% textmode="Multiline" CssClass="fontObject" runat=server /></td>
				<td>&nbsp;</td>
			</tr>
		
			<tr><td colspan=2>&nbsp;</td></tr>
			<tr>
				<td colspan=2 align=center>
					<asp:ImageButton id=ibConfirm onClick="btnPreview_Click" alternatetext="Confirm" imageurl="../../images/butt_confirm.gif" runat=server/> 
					<input type=image src="../../images/butt_cancel.gif" alt=Cancel onclick="javascript:window.close();" width="58" height="20">
					<asp:Label id=lblPO visible=false text="Purchase Order" runat=server/>
					<asp:Label id=lblGRN visible=false text="Goods Return" runat=server/>
					<asp:Label id=lblDispAdv visible=false text="Dispatch Advise" runat=server/>
					<asp:Label id=lblRPH visible=false text="RPH" runat=server/>
				</td>
			</tr>
			<tr>
				<td colspan=2 align=center>
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

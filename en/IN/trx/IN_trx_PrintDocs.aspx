<%@ Page Language="vb" src="../../../include/IN_trx_PrintDocs.aspx.vb" Inherits="IN_trx_PrintDocs" %>
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
            width: 30%;
        }
    </style>
</head>
<Preference:PrefHdl id=PrefHdl runat="server" />

<body onload="javascript:self.focus();document.frmMain.txtFromId.focus();" leftmargin="2" topmargin="2">
    <form id=frmMain runat="server" class="main-modul-bg-app-list-pu">


        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">

    <asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<table id=tblMain width=100% border=0 runat=server class="font9Tahoma">
			<tr>
				<td colspan=2 class="mt-h"><asp:Label id=lblDocName runat=server/></td>
			</tr>
			<tr>
				<td colspan=2><hr size="1" noshade></td>
			</tr>
			<tr>
				<td width=20%>From : </td>
				<td class="style1">
					<asp:TextBox id=txtFromId width=100% maxlength=20 onkeypress="javascript:keypress()" runat=server />
					<asp:RequiredFieldValidator id=rfvFrom display=dynamic runat=server 
						ErrorMessage="<br>Please enter document ID." 
						ControlToValidate=txtFromId />					
					<asp:RegularExpressionValidator id=revFrom 
						ControlToValidate="txtFromId"
						ValidationExpression="[a-zA-Z0-9\-]{1,20}"
						Display="Dynamic"
						text="<br>Alphanumeric without any space in between only."
						runat="server"/>
				</td>
			    <td width=5%>&nbsp;</td>
				<td width=20%>To : </td>
				<td width=25%>
					<asp:TextBox id=txtToId width=100% maxlength=20 onkeypress="javascript:keypress()" runat=server/>
					<asp:RegularExpressionValidator id=revTo 
						ControlToValidate="txtToId"
						ValidationExpression="[a-zA-Z0-9\-]{1,20}"
						Display="Dynamic"
						text="<br>Alphanumeric without any space in between only."
						runat="server"/>
					<asp:Label id=lblErrCode visible=false forecolor=red text="Invalid code given.<br>" runat=server/>
				</td>
			</tr>	
			<tr>
				<td width=20%>Periode : </td>
				<td class="style1"><asp:TextBox id=txtDateFrom width=70% maxlength=10 runat=server />
							<a href="javascript:PopCal('txtDateFrom');"><asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
							<asp:Label id=lblDateFrom visible=False forecolor=red text="<br>Invalid date format." runat=server />
							</td>
				<td width=5%>&nbsp;</td>
				<td width=20%>To : </td>
				<td width=25%><asp:TextBox id=txtDateTo width=70% maxlength=10 runat=server />
							<a href="javascript:PopCal('txtDateTo');"><asp:Image id="btnSelDateTo" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
							<asp:Label id=lblDateTo visible=False forecolor=red text="<br>Invalid date format." runat=server />
							</td>
			</tr>
			<tr id=TrIssueType>
				<td width=20%>Issue Type : </td>
				<td class="style1"><asp:dropdownlist id=ddlIssueType runat=server /></td>
			</tr>
			<tr id=TrDispCost>
				<td colspan=2>		
					<asp:checkboxlist id="cblDisplayCost" runat="server" class="font9Tahoma">
						<asp:listitem id=option1 value="Display Unit Price in Stock Issue Slip." selected runat="server"/>
					</asp:checkboxlist>
				</td>
			</tr>
			<tr><td colspan=2>&nbsp;</td></tr>
			<tr>
				<td colspan=2 align=center>
					<asp:ImageButton id=ibConfirm alternatetext="Confirm" imageurl="../../images/butt_confirm.gif" runat=server/> 
					<input type=image src="../../images/butt_cancel.gif" alt=Cancel onclick="javascript:window.close();" width="58" height="20">
					<asp:Label id=lblPR visible=false text="Purchase Request" runat=server/>
					<asp:Label id=lblSRA visible=false text="Stock Return Advise" runat=server/>
					<asp:Label id=lblST visible=false text="Stock Transfer" runat=server/>
					<asp:Label id=lblSI visible=false text="Stock Issue" runat=server/>
					<asp:Label id=lblFI visible=false text="Fuel Issue" runat=server/>
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

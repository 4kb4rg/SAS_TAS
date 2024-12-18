<%@ Page Language="vb" trace=false src="../../../include/PopUpFindNew.aspx.vb" Inherits="PopUpFindNew" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../preference/preference_handler.ascx"%>

<head>
    <title>Green Golden - Find</title> 
        <link href="../css/gopalms.css" rel="stylesheet" type="text/css" />
    <Script Language="Javascript">
		function keypress() {
			if (event.keyCode == 27)	//escape key press
				window.close();			//close window
		}
    </Script>
</head>
<Preference:PrefHdl id=PrefHdl runat="server" />

<body onload="javascript:self.focus();onload_setfocus();" leftmargin="2" topmargin="2">
    <form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
   <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top" class="font9Tahoma" >
			    <div class="kontenlist"> 

		<table id=tblMain width=100% class="font9Tahoma"  runat=server>
			<tr>
				<td class="mt-h">FIND</td>
				<td></td>
			</tr>
			<tr>
				<td colspan=2><hr size="1" noshade></td>
			</tr>
			<tr visible=false id=trAcc>
				<td width=15%><asp:Label id="lblChartOfAccount" runat="server" />&nbsp;Code : </td>
				<td>
					<asp:DropDownList id=ddlAccount width=100% runat=server/> 
					<asp:Label id=lblErrAccount visible=false forecolor=red runat=server/>
				</td>
			</tr>
			<tr visible=false id=trBlk>
				<td width=15%><asp:Label id="lblBlock" runat="server" />&nbsp;Code : </td>
				<td>
					<asp:DropDownList id=ddlBlock visible=true width=100% runat=server/>
					<asp:Label id=lblErrBlock visible=false forecolor=red runat=server/>
				</td>
			</tr>
			<tr visible=false id=trSubBlk>
				<td width=15%><asp:Label id="lblSubBlock" runat="server" />&nbsp;Code : </td>
				<td>
					<asp:DropDownList id=ddlSubBlock visible=true width=100% runat=server/>
					<asp:Label id=lblErrSubBlock visible=false forecolor=red runat=server/>
				</td>
			</tr>
			<tr visible=false id=trVeh>
				<td width=15%><asp:Label id="lblVehicle" runat="server" />&nbsp;Code : </td>
				<td>
					<asp:DropDownList id=ddlVeh width=100% runat=server/>
					<asp:Label id=lblErrVeh visible=false forecolor=red runat=server/>
				</td>
			</tr>
			<tr visible=false id=trVehExp>
				<td width=15%><asp:Label id="lblVehicleExpense" runat="server" />&nbsp;Code : </td>
				<td>
					<asp:DropDownList id=ddlVehExp width=100% runat=server/>
					<asp:Label id=lblErrVehExp visible=false forecolor=red runat=server/>
				</td>
			</tr>
			<tr visible=false id=trDenda>
				<td width=15%><asp:Label id="lblDendaCode" runat="server" />&nbsp;Code : </td>
				<td>
					<asp:DropDownList id=ddlDendaCode width=100% runat=server/> 
					<asp:Label id=lblErrDendaCode visible=false forecolor=red runat=server/>
				</td>
			</tr>
			<tr>
				<td></td>
				<td>					
					<asp:ImageButton id=ibConfirm alternatetext="Confirm" imageurl="../../images/butt_confirm.gif" runat=server/> 
					<input type=image src="../../images/butt_cancel.gif" alt=Cancel onclick="javascript:window.close();" width="58" height="20">
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

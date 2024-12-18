<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_MutTeKer.aspx.vb" Inherits="GL_StdRpt_MutTeKer" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Detailed Account Listing Report</title>
         <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu"  ID="frmMain">
       		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<input type=Hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=Hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=Hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>
			<table border="0" cellspacing="1" cellpadding="1" width="100%" class="font9Tahoma" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3">GENERAL LEDGER - MANAGERIAL REPORT (MUTASI TENAGA KERJA)</td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:GL_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>
				<tr>
					<td width=17%>Physical Period : </td>  
					<td width=39%>
						<asp:DropDownList id="ddlPhyMonthFrom" size=1 width=20% runat=server />
						<asp:TextBox id="txtPhyYearFrom" size=1 width=20% runat=server />
					</td>
					<td>&nbsp;</td>
					<td width=17%></td> 
					<td width=39%>
						<asp:DropDownList id="ddlPhyMonthTo" size=1 width=20% visible=false runat=server />
						<asp:TextBox id="txtPhyYearTo" size=1 width=20% visible=false runat=server />
					</td>

				</tr>
				<tr>
					<td width=17%>Remarks : </td>  
					<td width=39%>
						<asp:TextBox id="txtRemarks" size=1 width=100% textmode="Multiline" runat=server />
					</td>					
				</tr>
				<tr>
					<td colspa=3>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>					
				</tr>				
			</table>
                    </div>
            </td>
            </tr>
            </table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
	</body>
</HTML>

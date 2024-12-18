<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_MthLabOverheadDist.aspx.vb" Inherits="PR_StdRpt_MthLabOverheadDist" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_StdRpt_Selection_Ctrl" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Monthly Labour Overhead Distribution</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server"  class="main-modul-bg-app-list-pu" ID="frmMain">
      		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" class="font9Tahoma" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>PAYROLL - MONTHLY LABOUR OVERHEAD DISTRIBUTION</strong> </td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:PR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><hr size="1" noshade></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>			
				<tr>
					<td width=17%>Sort By :</td>
					<td width=39%><asp:dropdownlist id="lstSortBy" width="50%" runat="server" /></td>
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td>Order By :</td>
					<td><asp:dropdownlist id="lstOrderBy" width="50%" runat="server" >
						<asp:listitem value=asc text="Ascending" />
						<asp:listitem value=desc text="Descending" />
						</asp:dropdownlist>
					</td>
					<td>&nbsp;</td>
				</tr>																				
				<tr>
					<td colspan=4>&nbsp;</td>
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
		<asp:label id=lblErrMessage visible=false text="Error while initiating component." runat=server/>
		<asp:label id=lblLocation visible=false runat=server/>
		<asp:label id=lblAccCode visible=false runat=server/>
		<asp:label id=lblBlkCode visible=false runat=server/>
		<asp:label id=lblVehCode visible=false runat=server/>
		<asp:label id=lblVehExpCode visible=false runat=server/>
		<asp:Label id=lblCode text=" Code" visible=false runat=server/>
	</body>
</HTML>

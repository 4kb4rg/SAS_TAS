<%@ Page Language="vb" src="../../include/reports/NU_StdRpt_SeedlingsIssueList.aspx.vb" Inherits="NU_StdRpt_SeedlingsIssueList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="NU_StdRpt_Selection_Ctrl" src="../include/reports/NU_StdRpt_Selection_Ctrl.ascx"%>
<html>
	<head>
		<title>Nursery - Seedlings Issue Listing</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl ID="PrefHdl" Runat="Server" />
	</head>
	<body>
		<form ID="frmMain" class="main-modul-bg-app-list-pu"  Runat="Server" >
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table Border="0" CellSpacing="1" CellPadding="1" Width="100%" class="font9Tahoma">
				<tr>
					<td Class="font9Tahoma" ColSpan="3"><strong>NURSERY - SEEDLINGS ISSUE LISTING</strong> </td>
					<td Align="right" ColSpan="3"><asp:Label ID="lblTracker" Runat="Server" /></td>
				</tr>
				<tr>
					<td ColSpan="6"><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td ColSpan="6">&nbsp;</td>
				</tr>
				<tr>
					<td ColSpan="6"><UserControl:NU_StdRpt_Selection_Ctrl ID="RptSelect" Runat="Server" /></td>
				</tr>
				<tr>
					<td ColSpan="6">&nbsp;</td>
				</tr>
				<tr>
					<td ColSpan="6"><asp:Label ID="lblDate" ForeColor="Red" Visible="False" Text="Incorrect Date Format. Date Format is " Runat="Server" />
									<asp:Label ID="lblDateFormat" ForeColor="Red" Visible="False" Runat="Server" />
									<asp:Label ID="lblErrAccMonth" ForeColor="Red" Visible="False" Text="Accounting Month from cannot be bigger than Accounting Month To if same year." Runat="Server" />
									<asp:Label ID="lblErrAccYear" ForeColor="Red" Visible="False" Text="Accounting Year from cannot be bigger than Accounting Year To." Runat="Server" /></td>
				</tr>
				<tr>
					<td ColSpan="6">&nbsp;</td>
				</tr>
				<tr>
					<td ColSpan="6"><hr style="width :100%" /></td>
				</tr>
			</table>
			<table Width=100% Border="0" CellSpacing="1" CellPadding="1" class="font9Tahoma" Runat="Server">
				<tr>
					<td Width=17%>Issue ID From :</td>
					<td Width=39%><asp:TextBox ID="txtIssueIDFrom" Width="50%" Runat="Server" /> (blank for all)</td>
					<td Width=4%>To :</td>
					<td Width=40%><asp:TextBox ID="txtIssueIDTo" Width="50%" Runat="Server" /> (blank for all)</td>
				</tr>
				<tr>
					<td>Document Ref. No. :</td>
					<td><asp:TextBox ID="txtDocRefNo" Width="50%" Runat="Server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Issue Date From :</td>
					<td><asp:TextBox ID="txtIssueDateFrom" MaxLength="10" Width="50%" Runat="Server"/>
  								  <a href="javascript:PopCal('txtIssueDateFrom');">
								  <asp:Image ID="btnSelTxDateFrom" Runat="Server" ImageUrl="../Images/calendar.gif"/></a></td>
					<td>To :</td>
					<td><asp:TextBox ID="txtIssueDateTo" MaxLength="10" Width="50%" Runat="Server"/>
  								  <a href="javascript:PopCal('txtIssueDateTo');">
								  <asp:Image ID="btnSelTxDateTo" Runat="Server" ImageUrl="../Images/calendar.gif"/></a></td>
				</tr>
				<tr>
					<td><asp:Label ID=lblNUBlkCode Runat="Server" /> :</td>
					<td><asp:TextBox ID="txtNUBlkCode" MaxLength="8" Width="50%" Runat="Server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:Label ID=lblBatchNo Runat="Server" /> :</td>
					<td><asp:TextBox ID="txtBatchNo" MaxLength="2" Width="50%" Runat="Server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:Label ID=lblAccCode Runat="Server" /> :</td>
					<td><asp:TextBox ID="txtAccCode" MaxLength="20" Width="50%" Runat="Server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:Label ID=lblVehType Runat="Server" /> :</td>
					<td><asp:TextBox ID="txtVehType" MaxLength="8" Width="50%" Runat="Server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>								
				<tr>
					<td><asp:Label ID=lblVehCode Runat="Server" /> :</td>
					<td><asp:TextBox ID="txtVehCode" MaxLength="20" Width="50%" Runat="Server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:Label ID=lblVehExpCode Runat="Server" /> :</td>
					<td><asp:TextBox ID="txtVehExpCode" MaxLength="20" Width="50%" Runat="Server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:Label ID=lblBlkType Runat="Server" /> :</td>
					<td><asp:DropDownList ID="ddlBlkType" AutoPostBack="True" Width="50%" OnSelectedIndexChanged="ddlBlkType_OnSelectedIndexChanged" Runat="Server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>									
				</tr>			
				<tr ID=trBlkGrp>
					<td><asp:Label ID=lblBlkGrp Runat="Server" /> :</td>
					<td><asp:TextBox ID="txtBlkGrp" MaxLength="8" Width="50%" Runat="Server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>										
				</tr>										
				<tr ID=trBlk>
					<td><asp:Label ID=lblBlkCode Runat="Server" /> :</td>
					<td><asp:TextBox ID="txtBlkCode" MaxLength=8 Width="50%" Runat="Server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr ID=trSubBlk>
					<td><asp:Label ID=lblSubBlkCode Runat="Server" /> :</td>
					<td><asp:TextBox ID="txtSubBlkCode" MaxLength="8" Width="50%" Runat="Server" /> (blank for all)</td>			
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
				</tr>
				<tr>
					<td>Status :</td>
					<td><asp:DropDownList ID="ddlStatus" Size="1" Width="50%" Runat="Server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td ColSpan=4><asp:Label ID="lblLocation" Visible="False" Runat="Server" /></td>
				</tr>
				<tr>
					<td ColSpan=4>&nbsp;</td>
				</tr>	
				<tr>
					<td><asp:ImageButton ID="ibPrintPreview" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" OnClick="ibPrintPreview_OnClick" Runat="Server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" />
                    </td>
				</tr>
			</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
		<asp:Label ID="lblCode" Visible="False" Text=" Code" Runat="Server" />
		<asp:Label ID="lblErrMessage" Visible="False" Text="Error while initiating component." Runat="Server" />
	</body>
</html>

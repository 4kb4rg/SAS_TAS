<%@ Page Language="vb" Src="../../include/reports/WS_StdRpt_CompletedJobSheet.aspx.vb" Inherits="WS_StdRpt_CompletedJobSheet" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../include/preference/preference_handler.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="WS_STDRPT_SELECTION_CTRL" Src="../include/reports/WS_StdRpt_Selection_Ctrl.ascx" %>
<html>
	<head>
		<title>Workshop - Completed Job Sheet</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl ID="PrefHdl" RunAt="server" />
	</head>
	<body>
		<form ID="frmMain" class="main-modul-bg-app-list-pu" RunAt="server">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table ID="tblMain" Border="0" CellSpacing="1" CellPadding="1" Width="100%">
				<tr>
					<td Class="font9Tahoma" ColSpan="6"><strong>WORKSHOP - COMPLETED JOB SHEET</strong> </td>
				</tr>
				<tr>
					<td ColSpan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
				<tr>
					<td ColSpan="6">&nbsp;</td>
				</tr>
				<tr>
					<td ColSpan="6"><UserControl:WS_StdRpt_Selection_Ctrl ID="RptSelect" RunAt="server" /></td>
				</tr>
				<tr>
					<td ColSpan="6">&nbsp;</td>
				</tr>
				<tr>
					<td ColSpan="6">&nbsp;</td>
				</tr>
				<tr>
					<td ColSpan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table ID="tblSub" Width="100%" Border="0" CellSpacing="1" CellPadding="1" class="font9Tahoma" RunAt="server">
				<tr>
					<td Width="15%">Job ID From :</td>
					<td Width="35%"><asp:TextBox ID="txtJobIDFrom" MaxLength="20" Width="50%" RunAt="server" /> (blank for all)</td>
					<td Width="15%">To :</td>
					<td Width="35%"><asp:TextBox ID="txtJobIDTo" MaxLength="20" Width="50%" RunAt="server" /> (blank for all)</td>
				</tr>
				<tr>
					<td Width="15%">Job Start Date From :</td>
					<td Width="35%">
					    <table Width="100%" CellSpacing="0" CellPadding="0" Border="0">
							<tr>
							    <td Width="50%"><asp:TextBox ID="txtJobStartDateFrom" Width="100%" MaxLength="10" RunAt="Server" /></td>
						        <td Width="50%">&nbsp;<a HRef="javascript:PopCal('txtJobStartDateFrom');"><asp:Image ID="imgJobStartDateFrom" ImageUrl="../Images/calendar.gif" RunAt="Server" /></a></td>
						    </tr>
						</table>
						<asp:Label ID="lblJobStartDateFromErr" ForeColor="Red" RunAt="Server" />
					</td>
					<td Width="15%">To :</td>
					<td Width="35%">
					    <table Width="100%" CellSpacing="0" CellPadding="0" Border="0">
							<tr>
							    <td Width="50%"><asp:TextBox ID="txtJobStartDateTo" Width="100%" MaxLength="10" RunAt="Server" /></td>
						        <td Width="50%">&nbsp;<a HRef="javascript:PopCal('txtJobStartDateTo');"><asp:Image ID="imgJobStartDateTo" ImageUrl="../Images/calendar.gif" RunAt="Server" /></a></td>
						    </tr>
						</table>
						<asp:Label ID="lblJobStartDateToErr" ForeColor="Red" RunAt="Server" />
					</td>
				</tr>
				<tr>
					<td Width="15%">Job End Date From :</td>
					<td Width="35%">
					    <table Width="100%" CellSpacing="0" CellPadding="0" Border="0">
							<tr>
							    <td Width="50%"><asp:TextBox ID="txtJobEndDateFrom" Width="100%" MaxLength="10" RunAt="Server" /></td>
						        <td Width="50%">&nbsp;<a HRef="javascript:PopCal('txtJobEndDateFrom');"><asp:Image ID="imgJobEndDateFrom" ImageUrl="../Images/calendar.gif" RunAt="Server" /></a></td>
						    </tr>
						</table>
						<asp:Label ID="lblJobEndDateFromErr" ForeColor="Red" RunAt="Server" />
					</td>
					<td Width="15%">To :</td>
					<td Width="35%">
					    <table Width="100%" CellSpacing="0" CellPadding="0" Border="0">
							<tr>
							    <td Width="50%"><asp:TextBox ID="txtJobEndDateTo" Width="100%" MaxLength="10" RunAt="Server" /></td>
						        <td Width="50%">&nbsp;<a HRef="javascript:PopCal('txtJobEndDateTo');"><asp:Image ID="imgJobEndDateTo" ImageUrl="../Images/calendar.gif" RunAt="Server" /></a></td>
						    </tr>
						</table>
						<asp:Label ID="lblJobEndDateToErr" ForeColor="Red" RunAt="Server" />
					</td>
				</tr>
				<tr>
					<td Width="15%">Job Type :</td>
					<td Width="35%"><asp:DropDownList ID="ddlJobType" AutoPostBack="True" OnSelectedIndexChanged="ddlJobType_OnSelectedIndexChanged" Width="50%" RunAt="server" /></td>
					<td Width="15%">&nbsp;</td>
					<td Width="35%">&nbsp;</td>
				</tr>
				<tr ID="trBillPartyCode">
					<td Width="15%"><asp:Label ID="lblBillParty" RunAt="server" /> Code :</td>
					<td Width="35%"><asp:TextBox ID="txtBillPartyCode" MaxLength="8" Width="50%" RunAt="server" /> (blank for all)</td>
					<td Width="15%">&nbsp;</td>
					<td Width="35%">&nbsp;</td>
				</tr>
				<tr ID="trEmpCode">
					<td Width="15%">Employee Code :</td>
					<td Width="35%"><asp:TextBox ID="txtEmpCode" MaxLength="20" Width="50%" RunAt="server" /> (blank for all)</td>
					<td Width="15%">&nbsp;</td>
					<td Width="35%">&nbsp;</td>
				</tr>
				<tr>
					<td Width="15%">Job Status :</td>
					<td Width="35%"><asp:DropDownList ID="ddlStatus" Width="50%" Size="1" RunAt="server" /></td>
					<td Width="15%">&nbsp;</td>
					<td Width="35%">&nbsp;</td>
				</tr>
				<tr>
					<td ColSpan=4>&nbsp;</td>
				</tr>
				<tr>
					<td ColSpan=4>&nbsp;</td>
				</tr>
				<tr>
					<td ColSpan=4><asp:ImageButton ID="ibPrintPreview" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" OnClick="ibPrintPreview_OnClick" RunAt="server" /></td>
				</tr>
			</table>
			<asp:Label ID="lblLocation" Visible="false" RunAt="server" />
			<asp:Label ID="lblWork" Visible="false" RunAt="server" />
			<asp:Label ID="lblVehicle" Visible="false" RunAt="server" />
		    <asp:Label ID="lblErrMessage" Visible="false" Text="Error while initiating component." RunAt="server" />
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>

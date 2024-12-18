<comment>%@ Page Language="vb" Src="../../../include/WS_Trx_Job_Detail_Popup.aspx.vb" Inherits="WS_TRX_JOB" %</comment>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<html>
	<head>
		<title>Workshop Job Detail - Generate DN/CN</title>
		<script language="javascript">
		    window.returnValue = "";
		    function fnConfirm() {
		        if (window.frmMain.rblOption[0].checked == true) {
		            window.returnValue = "0";
		            window.close();
		        }
		        else if (window.frmMain.rblOption[1].checked == true) {
		            window.returnValue = "1";
		            window.close();
		        }
		        else if (window.frmMain.rblOption[2].checked == true) {
		            window.returnValue = "2";
		            window.close();
		        }
		        return false;
		    }
		    
		    function fnCancel() {
		        window.close();
		        return false;
		    }
		</script>
	</head>
	<preference:PrefHdl ID=PrefHdl RunAt="Server" />
	<body LeftMargin="2" TopMargin="2">
		<form ID=frmMain Name=frmMain RunAt="Server">
			<table Border="0" CellSpacing="1" CellPadding="1" Width="100%">
				<tr>
					<td Class="mt-h">WORKSHOP JOB : GENERATE DN/CN</td>
				</tr>
				<tr>
					<td><hr Size="1" NoShade></td>
				</tr>
				<tr>
					<td>Issue Debit Note for</td>
				</tr>
				<tr>
					<td Width="100%" Class="mb-c">
						<asp:RadioButtonList ID="rblOption" RepeatDirection="Vertical" RunAt="Server" >
    						<asp:ListItem Selected="True">Labours Only</asp:ListItem>
    						<asp:ListItem>Parts Only</asp:ListItem>
    						<asp:ListItem>Labours and Parts</asp:ListItem>
						</asp:RadioButtonList>
					</td>
				</tr>
				<tr>
				    <td>
				        &nbsp;
				    </td>
				</tr>
				<tr>
					<td Align="Left">
					    <input Type="Image" Src="../../images/butt_confirm.gif" Alt="Confirm" OnClick="javascript:return fnConfirm();" width="76" height="20">
					    <input Type="Image" Src="../../images/butt_cancel.gif" Alt="Cancel" OnClick="javascript:return fnCancel();" width="58" height="20">
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

<%@ Page Language="vb" src="../include/Data_Upload_Budget.aspx.vb" Inherits="Data_Upload_Budget" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="include/preference/preference_handler.ascx"%>
<html>

<head>
<Preference:PrefHdl id=PrefHdl runat="server" />
<title>Upload Reference File</title>
</head>

<body style="font-size: 12pt">

<form id=frmUpload enctype="multipart/form-data" runat=server>
	<table border="0" cellspacing="0" width="100%">
		<tr>
			<td class="mt-h" colspan="2">UPLOAD REFERENCE FILE</td>
		</tr>
		<tr>
			<td colspan=2><hr size="1" noshade></td>
		</tr>
		<tr>
			<td colspan="2" style="height: 21px">Upload Reference File</td>
		</tr>
		<tr>
			<td colspan="2">&nbsp;</td>
		</tr>
		<tr>
			<td colspan="2">
				<table id=tblBefore border=0 cellpadding=2 cellspacing=0 width=100% runat=server>
					<tr>
						<td colspan="2">Steps</td>
					</tr>
					<tr>
						<td colspan="2">1.&nbsp; Click &quot;Browse&quot; button to select your file location.</td>
					</tr>
					<tr>
						<td colspan="2">2.&nbsp; Click &quot;Upload&quot; button to save your file, data into database.</td>
					</tr>
					<tr>
						<td colspan="2">Note : The process may take a couple of minutes.<br />
                            <hr size="1" noshade></td>
					</tr>
					<tr>
						<td colspan="2" style="height: 23px">Filename :</td>
					</tr>
					<tr>
						<td colspan="2" style="height: 99px" valign="top">
                            &nbsp;
                            <br  / >
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr style="font-size: 12pt">
                                    <td style="width: 160px; height: 44px" valign="top">
                                        <span style="font-size: 10pt"><span style="font-size: 9pt">Upload Budget&nbsp; Estate
                                            :</span></span></td>
                                    <td style="width: 251px; height: 44px; text-align: LEft;" align="left" valign="top">
                                        <input type=file id=flUpload runat=server style="width: 248px" />
                                        <asp:Label id=lblErrNoFile text="Please enter file name." visible=false forecolor=red runat=server/></td>
                                    <td colspan="6" style="height: 44px" valign="top">
                                        <asp:ImageButton id=UploadBtn imageurl="images/butt_upload.gif" alternatetext=" Upload " onclick=UploadBtn_Click runat=server />
                                        <asp:ImageButton ID="SaveBtn" runat="server" AlternateText="Save" ImageUrl="images/butt_save.gif"
                                OnClick="SaveBtn_Click" UseSubmitBehavior="false" />
                                        <asp:ImageButton ID="Cancel" runat="server" AlternateText="Cancel" CausesValidation="False"
                                ImageUrl="images/butt_cancel.gif" OnClick="CancelBtn_Click" UseSubmitBehavior="false" />
                                        &nbsp;<br />
                                    </td>
                                    <td colspan="3" style="height: 44px" valign="top">
                                    </td>
                                </tr>
                                <tr style="font-size: 12pt">
                                    <td colspan="11" style="height: 11px; text-align: center" valign="top"></td>
                                </tr>
                                <tr style="font-size: 12pt">
                                    <td style="width: 160px; height: 13px" valign="top">
                                        <span style="font-size: 10pt">
                                        Upload Budget Poduksi :</span></td>
                                    <td style="width: 251px; height: 13px; text-align: left" align="left">
                                        <input type=file id=flUploadPD runat=server style="width: 248px" /><br />
                                        <asp:Label id=lblErrNoFilePD text="Please enter file name." visible=False forecolor=Red runat=server/><br />
                                        </td>
                                    <td style="height: 13px" colspan="6" valign="top">
                                        <asp:ImageButton id=UploadBtnPd imageurl="images/butt_upload.gif" alternatetext=" Upload " onclick=UploadBtnPD_Click runat=server />
                                        <asp:ImageButton ID="SaveBtnPD" runat="server" AlternateText="Save" ImageUrl="images/butt_save.gif"
                                OnClick="SaveBtnPD_Click" UseSubmitBehavior="false" />
                                        <asp:ImageButton ID="CancelBtnPD" runat="server" AlternateText="Cancel" CausesValidation="False"
                                ImageUrl="images/butt_cancel.gif" OnClick="CancelBtnPD_Click" UseSubmitBehavior="false" />
                                        &nbsp;</td>
                                    <td colspan="1" style="height: 13px" valign="top">
                                    </td>
                                    <td colspan="1" style="height: 13px" valign="top">
                                    </td>
                                    <td colspan="1" style="height: 13px" valign="top">
                                    </td>
                                </tr>
                                <tr style="font-size: 12pt" valign="top">
                                    <td style="width: 160px; height: 43px; text-align: left;">
                                        <span style="font-size: 10pt"><span style="font-size: 9pt">Upload Budget&nbsp; Mill
                                            :&nbsp;</span></span></td>
                                    <td style="width: 251px; height: 43px; text-align: left" valign="top">
                                        <input type=file id=flUploadMill runat=server style="width: 248px" />
                                        <asp:Label id=lblErrNoFileMill text="Please enter file name." visible=False forecolor=Red runat=server/></td>
                                    <td style="height: 43px" colspan="6" valign="top">
                                    <asp:ImageButton id=UploadMill imageurl="images/butt_upload.gif" alternatetext=" Upload " onclick=UploadMill_Click runat=server />
                                        <asp:ImageButton ID="SaveMill" runat="server" AlternateText="Save" ImageUrl="images/butt_save.gif"
                                OnClick="SaveBtnMill_Click" UseSubmitBehavior="false" />
                                        <asp:ImageButton ID="CancelMill" runat="server" AlternateText="Cancel" CausesValidation="False"
                                ImageUrl="images/butt_cancel.gif" OnClick="CancelBtn_Click" UseSubmitBehavior="false" /></td>
                                    <td style="width: 61px; height: 43px">
                                    </td>
                                    <td style="width: 61px; height: 43px">
                                    </td>
                                    <td style="width: 61px; height: 43px">
                                    </td>
                                </tr>
                            </table>
                            </td>
					</tr>
					<tr style="font-size: 12pt">
						<td colspan="2" style="height: 42px">
							<asp:Label id=lblSuccess forecolor=Blue visible=False runat=server Font-Size="Small" />
							<asp:Label id=lblError text="" forecolor=red runat=server />
                            <asp:Label id=lblSuccessRec text="Successfully uploaded " visible=false runat=server />
							<asp:Label id=lblSuccessPath text=" records for the file in " visible=false runat=server /><br>
							<asp:Label id=lblPathExcel forecolor=Blue runat=server Font-Size="Small" /></td>
					</tr>
				</table>
    <div id="divgd" style="overflow: auto; width: 1070px; height: 435px; font-size: 12pt;">
    <asp:DataGrid ID="dgDataList" runat="server" AllowSorting="True" CellPadding="4"
        PageSize="100" OnPageIndexChanged=OnPageChanged  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Font-Size="Small" ForeColor="Black" GridLines="Horizontal">
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" PageButtonCount="11" Position="TopAndBottom" />
        <AlternatingItemStyle CssClass="mr-r" />
        <ItemStyle CssClass="mr-l" />
        <HeaderStyle CssClass="mr-h" BackColor="#333333" Font-Bold="True" ForeColor="White" />
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <SelectedItemStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
    </asp:DataGrid>
        <br />
        <br /><br /><br />
    </div>
    <p><asp:Label id=Label1 forecolor=Blue runat=server Font-Bold="True" Font-Size="Larger" >Validasi Key Double</asp:Label>&nbsp;</p>
        <div id="div1" style="overflow: auto; width: 1070px; height: 500px; font-size: 12pt;">
        <asp:DataGrid ID="dgCek" runat="server" AllowSorting="True" CellPadding="3"
        PageSize="100" OnPageIndexChanged=OnPageChanged  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Font-Size="Small">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" Mode="NumericPages" PageButtonCount="11" Position="TopAndBottom" />
            <AlternatingItemStyle CssClass="mr-r" />
            <ItemStyle CssClass="mr-l" ForeColor="#000066" />
            <HeaderStyle CssClass="mr-h" BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:DataGrid><br />
            <br />
    </div>
                <br />
        </td>
		</tr>
	</table>
    <br />
    <br />
    <br />
    &nbsp;
</form>
</body>
</html>
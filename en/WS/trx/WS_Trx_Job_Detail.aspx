<%@ Page Language="vb" Trace="False" Src="../../../include/WS_Trx_Job_Detail.aspx.vb" Inherits="WS_TRX_JOB_DETAIL" %>
<%@ Register TagPrefix="UserControl" TagName="MenuWSTrx" Src="../../menu/menu_WSTrx.ascx" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<html>
	<head>
		<title>Workshop Job Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl ID="PrefHdl" RunAt="Server" />
		<script language="javascript">
		    function fnGenerateDNCN() {
		        var objLink = document.getElementById("hlDummy");
		        var strRet = window.showModalDialog("WS_Trx_Job_Detail_Popup.aspx", "", "dialogHeight:300px; dialogWidth:450px; center:yes; edge:raised; help:no; resizable:no; scroll:no; status:no;");
		        switch (strRet) {
		            case "0":
		                objLink.href = "WS_Trx_Job_Detail.aspx?flag=act1&jobid=" + lblJobID.innerText;
		                objLink.click();
		                break;
		            case "1":
		                objLink.href = "WS_Trx_Job_Detail.aspx?flag=act2&jobid=" + lblJobID.innerText;
		                objLink.click();
		                break;
		            case "2":
		                objLink.href = "WS_Trx_Job_Detail.aspx?flag=act3&jobid=" + lblJobID.innerText;
		                objLink.click();
		                break;
		        }
		        return false;
		    }
		</script>
	</head>
	<body>
		<form ID="frmMain" RunAt="Server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


		    <a ID="hlDummy" Style="display:none;">Hyperlink</a>
			<asp:label ID="lblCode" Visible="False" Text=" Code" RunAt="Server" />
			<asp:label ID="lblSelect" Visible="False" Text="Select " RunAt="Server" />
			<asp:label ID="lblPleaseSelect" Visible="False" Text="Please select " RunAt="Server" />
			<asp:label ID="lblPayrollPosted" Visible="False" RunAt="Server" />
			<asp:label ID="lblPrintDate" Visible="False" RunAt="Server" />
			<table ID="tblMain" Border="0" Width="100%" CellSpacing="0" CellPadding="1" RunAt="Server" class="font9Tahoma">
				<tr>
					<td ColSpan="6"><UserControl:MenuWSTrx ID="menuWSTrx" RunAt="Server" /></td>
				</tr>
				<tr>
					<td Class="mt-h" ColSpan="6">WORKSHOP JOB DETAILS</td>
				</tr>
				<tr>
					<td ColSpan="6"><hr Size="1" NoShade></td>
				</tr>
				<tr>
					<td Width="17%" Height="25">Job ID :</td>
					<td Width="40%"><asp:Label ID="lblJobID" RunAt="Server"/>&nbsp;</td>
					<td Width="3%">&nbsp;</td>
					<td Width="15%">Period :</td>
					<td Width="15%"><asp:Label ID="lblPeriod" RunAt="Server" />&nbsp;</td>
					
				</tr>
				<tr>
					<td Height="25">Job Type :*</td>
					<td>
					    <asp:DropDownList ID="ddlJobType" Width="100%" RunAt="Server" />
					    <asp:Label ID="lblJobTypeText" Visible="False" Width="100%" RunAt="Server" />
					    <asp:Label ID="lblJobType" Visible="False" Width="100%" RunAt="Server" />
					</td>
					<td>&nbsp;</td>
					<td>Status :</td>
					<td>
					    <asp:Label ID="lblStatusText" RunAt="Server" />
					    <asp:Label ID="lblStatus" Visible="False" RunAt="Server" />
					    <asp:label ID="lblJobStockCount" Visible="False" Text="0" RunAt="Server" />
			            <asp:label ID="lblMechHourCount" Visible="False" Text="0" RunAt="Server" />
			            <asp:label ID="lblEditDependency" Visible="False" Text="0" RunAt="Server" />&nbsp;
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td Height="25">Description :*</td>
					<td>
					    <asp:TextBox ID="txtDescription" MaxLength="128" Width="100%" RunAt="Server" />
					    <asp:Label ID="lblDescriptionErr" Visible="False" Text="Description cannot be blank" ForeColor="Red" RunAt="Server" />
					</td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label ID="lblCreateDate" RunAt="Server" />&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td Height="25">Document Ref No :*</td>
					<td>
					    <asp:Textbox ID="txtDocRefNo" MaxLength="20" Width="50%" RunAt="Server" />
					    <asp:Label ID="lblDocRefNoErr" Visible="False" Text="<br>Document Ref No cannot be blank" ForeColor="Red" RunAt="Server" />
					</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label ID="lblUpdateDate" RunAt="Server" />&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td Height="25">Document Ref Date :*</td>
					<td>
					    <table Width="100%" CellSpacing="0" CellPadding="0" Border="0">
							<tr>
							    <td Width="50%"><asp:TextBox ID="txtDocRefDate" Width="100%" MaxLength="10" RunAt="Server" /></td>
						        <td Width="50%">&nbsp;<a HRef="javascript:PopCal('txtDocRefDate');"><asp:Image ID="imgDocRefDate" ImageUrl="../../Images/calendar.gif" RunAt="Server" /></a></td>
						    </tr>
						</table>
					    <asp:Label ID="lblDocRefDateErr" ForeColor="Red" RunAt="Server" />
					</td>
					<td>&nbsp;</td>
					<td>Updated By :</td>
					<td><asp:Label ID="lblUserName" RunAt="Server" />&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td Height="25">Job Start Date :*</td>
					<td>
					    <table Width="100%" CellSpacing="0" CellPadding="0" Border="0" class="font9Tahoma">
							<tr>
								<td Width="50%"><asp:TextBox ID="txtJobStartDate" Width="100%" MaxLength="10" RunAt="Server" /></td>
								<td Width="20%">&nbsp;<a HRef="javascript:PopCal('txtJobStartDate');"><asp:Image ID="imgJobStartDate" ImageUrl="../../Images/calendar.gif" RunAt="Server" /></a></td>
								<td Width="30%" Align="Right">Start Time :</td>
							</tr>
						</table>
						<asp:Label ID="lblJobStartDateErr" ForeColor="Red" RunAt="Server" />
					</td>
					<td ColSpan="4">
					    <table Width="100%" CellSpacing="0" CellPadding="0" Border="0" class="font9Tahoma">
							<tr>
								<td Width="200">
								    <asp:TextBox ID="txtJobStartTimeHour" Width="33" MaxLength="2" RunAt="Server" /> &nbsp;:&nbsp;
								    <asp:TextBox ID="txtJobStartTimeMinute" Width="33" MaxLength="2" RunAt="Server" /> &nbsp;
								    <asp:DropDownList ID="ddlJobStartTimeDayNight" class="font9Tahoma" Width="50" RunAt="Server">
								        <asp:ListItem>AM</asp:ListItem>
                                        <asp:ListItem>PM</asp:ListItem>
								    </asp:DropDownList> &nbsp;HH:MM
								</td>
								<td><asp:ImageButton ID="ibJobStartSetCurrentDateTime" ImageUrl="../../images/butt_curr_dttime.gif" OnClick="ibJobStartSetCurrentDateTime_OnClick" CausesValidation="False" AlternateText="Set Current Date/Time" RunAt="Server" /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td Height="25">Job End Date :</td>
					<td>
					    <table Width="100%" CellSpacing="0" CellPadding="0" Border="0" class="font9Tahoma">
							<tr>
								<td Width="50%"><asp:TextBox ID="txtJobEndDate" Width="100%" MaxLength="10" RunAt="Server" /></td>
								<td Width="20%">&nbsp;<a HRef="javascript:PopCal('txtJobEndDate');"><asp:Image ID="imgJobEndDate" ImageUrl="../../Images/calendar.gif" RunAt="Server" /></a></td>
								<td Width="30%" Align="Right">End Time :</td>
							</tr>
						</table>
						<asp:Label ID="lblJobEndDateErr" ForeColor="Red" RunAt="Server" />
					</td>
					<td ColSpan="4">
					    <table Width="100%" CellSpacing="0" CellPadding="0" Border="0" class="font9Tahoma">
							<tr>
								<td Width="200">
								    <asp:TextBox ID="txtJobEndTimeHour" Width="33" MaxLength="2" RunAt="Server" /> &nbsp;:&nbsp;
								    <asp:TextBox ID="txtJobEndTimeMinute" Width="33" MaxLength="2" RunAt="Server" /> &nbsp;
								    <asp:DropDownList ID="ddlJobEndTimeDayNight" class="font9Tahoma" Width="50" RunAt="Server">
								        <asp:ListItem>AM</asp:ListItem>
                                        <asp:ListItem>PM</asp:ListItem>
								    </asp:DropDownList> &nbsp;HH:MM
								</td>
								<td><asp:ImageButton ID="ibJobEndSetCurrentDateTime" ImageUrl="../../images/butt_curr_dttime.gif" OnClick="ibJobEndSetCurrentDateTime_OnClick" CausesValidation="False" AlternateText="Set Current Date/Time" RunAt="Server" /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td Height="25">Labour Hourly Rate :*</td>
					<td>
					    <asp:textbox ID="txtChrgRate" MaxLength="19" Width="50%" RunAt="Server" />
                       	<asp:RegularExpressionValidator ID="revChrgRate" 
													    ControlToValidate="txtChrgRate"
													    ValidationExpression="\d{1,19}"
													    Display="Dynamic"
													    Text = "<br>Maximum length 19 digits and 0 decimal points"
													    RunAt="Server"/>
													    
                        <asp:Label ID="lblChrgRateErr" Visible="False" ForeColor="Red" RunAt="Server" />
					</td>
					<td ColSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td Height="25"><asp:Label ID="lblServTypeCodeTag" RunAt="Server" /> :*</td>
					<td>
						<table Width="170%" CellSpacing="0" CellPadding="0" Border="0">
						<tr>
							<td Width="55%"><asp:DropDownList ID="ddlServTypeCode" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlServTypeCode_OnSelectedIndexChanged" RunAt="Server" /></td>
							<td>&nbsp;</td>
						</tr>
					    </table>
					    <asp:Label ID="lblServTypeCodeErr" Visible="False" ForeColor="Red" RunAt="Server" />
					</td>
					<td ColSpan="4">&nbsp;</td>
				</tr>
				<tr ID="trChargeTo">
					<td Height="25">Charge To :*</td>
					<td>
						<table Width="170%" CellSpacing="0" CellPadding="0" Border="0">
						<tr>
							<td Width="55%"><asp:DropDownList ID="ddlChargeTo" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlChargeTo_OnSelectedIndexChanged" RunAt="Server" /></td>
							<td>&nbsp;</td>
						</tr>
					    </table>
					    <asp:Label ID="lblChargeToErr" Visible="False" ForeColor="Red" RunAt="Server" />
						<asp:Label ID="lblLocationTag" Visible="False" RunAt="Server" />
					</td>
					<td ColSpan="4">&nbsp;</td>
				</tr>
				<tr ID="trAccCode">
					<td Height=25><asp:label ID="lblAccCodeTag" RunAt="Server" /> :*</td>
					<td>
					    <table Width="170%" CellSpacing="0" CellPadding="0" Border="0">
							<tr>
								<td Width="52%"><asp:DropDownList ID="ddlAccCode" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlAccCode_OnSelectedIndexChanged" RunAt="Server" /></td>
						        <td>&nbsp;<Input Type="Button" Value=" ... " ID="btnFindAccCode" OnClick="javascript:findcode('frmMain','','ddlAccCode','9','ddlBlkCode','','ddlVehCode','ddlVehExpCode','','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation="False" RunAt="Server" /></td>
						    </tr>
						</table>
						<asp:Label ID="lblAccCodeErr" Visible="False" ForeColor="Red" RunAt="Server" />
					</td>
					<td ColSpan="4">&nbsp;</td>
				</tr>
				<tr ID="trBlkCode">
					<td Height=25><asp:label ID="lblBlkCodeTag" RunAt="Server" /> :</td>
					<td>
						<table Width="170%" CellSpacing="0" CellPadding="0" Border="0">
							<tr>
								<td Width="55%"><asp:DropDownList ID="ddlBlkCode" Width="100%" RunAt="Server" /></td>
								<td>&nbsp;</td>
							</tr>
						</table>	
					    <asp:Label ID="lblBlkCodeErr" Visible="False" ForeColor="Red" RunAt="Server" />
					</td>
					<td ColSpan="4">&nbsp;</td>
				</tr>
				<tr ID="trVehCode">
					<td Height=25><asp:label ID="lblVehCodeTag" RunAt="Server" /> :</td>
					<td>
						<table Width="170%" CellSpacing="0" CellPadding="0" Border="0">
							<tr>
								<td Width="55%"><asp:DropDownList ID="ddlVehCode" Width="100%" RunAt="Server" /></td>
								<td>&nbsp;</td>
							</tr>
						</table>		
						<asp:label ID="lblVehCodeErr" Visible="False" ForeColor="Red" RunAt="Server" />
					</td>
					<td ColSpan="4">&nbsp;</td>
				</tr>
				<tr ID="trVehExpCode">
					<td Height=25><asp:label ID="lblVehExpCodeTag" RunAt="Server" /> (Labour) :</td>
					<td>
						<table Width="170%" CellSpacing="0" CellPadding="0" Border="0">
							<tr>
								<td Width="55%"><asp:DropDownList ID="ddlVehExpCode" Width="100%" RunAt="Server" /></td>
								<td>&nbsp;</td>
							</tr>
						</table>	
						<asp:label ID="lblVehExpCodeErr" Visible="False" ForeColor="Red" RunAt="Server" />
					</td>
					<td ColSpan="4">&nbsp;</td>
				</tr>
				<tr ID="trEmpCode">
					<td Height="25">Employee Code :*</td>
					<td>
					    <asp:DropDownList ID="ddlEmpCode" Width="100%" RunAt="Server" />
					    <asp:Label ID="lblEmpCodeErr" Visible="False" Text="Employee Code cannot be blank" ForeColor="Red" RunAt="Server" />
					</td>
					<td ColSpan="4">&nbsp;</td>
				</tr>
				<tr ID="trBillPartyCode">
					<td Height="25"><asp:Label ID="lblBillPartyCode" RunAt="Server" /> :*</td>
					<td>
					    <asp:DropDownList ID="ddlBillPartyCode" Width="100%" RunAt="Server" />
					    <asp:Label ID="lblBillPartyCodeErr" Visible="False" ForeColor="Red" RunAt="Server" />
					</td>
					<td ColSpan="4">&nbsp;</td>
				</tr>
				<tr ID="trVehRegNo">
					<td Height="25"><asp:Label ID="lblVehRegNoTag" RunAt="Server" /> :*</td>
					<td>
					    <asp:TextBox ID="txtVehRegNo" MaxLength="20" Width="100%" RunAt="Server" />
					    <asp:Label ID="lblVehRegNoErr" Visible="False" ForeColor="Red" RunAt="Server" />
					</td>
					<td ColSpan="4">&nbsp;</td>
				</tr>
				<tr>
				    <td ColSpan="6">&nbsp;</td>
				</tr>
				<tr>
				    <td ColSpan="6">
				        <table ID="tblAdd" Border="0" Width="100%" CellSpacing="0" CellPadding="4" RunAt="Server">
				            <tr Class="mb-c">
				                <td Width="20%" Height="25"><asp:Label ID="lblWorkCodeTag" RunAt="Server" /> :*</td>
					            <td Width="80%">
					                <asp:DropDownList ID="ddlWorkCode" Width="100%" RunAt="Server" />
					                <asp:Label ID="lblWorkCodeErr" Visible="False" ForeColor="Red" RunAt="Server" />
					            </td>
				            </tr>
				            <tr Class="mb-c">
					            <td ColSpan=2><asp:ImageButton Text="Add" ID="ibAdd" ImageURL="../../images/butt_add.gif" OnClick="ibAdd_OnClick" RunAt="Server" />
                                            <asp:Button ID="AddBtn2" runat="server" class="button-small" Text="Add" />							
                                            </td>
				            </tr>
				            <tr>
					            <td ColSpan=2>&nbsp;</td>
				            </tr>
				        </table>
				    </td>
				</tr>
				<tr ID="trWorkCode">
					<td ColSpan="6"> 
						<asp:DataGrid ID="dgWorkCode"
							OnItemDataBound="dgWorkCode_OnItemDataBound"
							AutoGenerateColumns="False" 
							Width="100%" 
							RunAt="Server"
							GridLines = "None"
							Cellpadding = "2"
							Pagerstyle-Visible="False"
							OnDeleteCommand="dgWorkCode_OnDeleteCommand"
							AllowSorting="True"
                            class="font9Tahoma">	
							 
							<HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<Columns>
								<asp:TemplateColumn HeaderText="Work Code">
									<ItemStyle Width="20%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("WorkCode")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Description">
									<ItemStyle Width="70%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("Description")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle HorizontalAlign="Right" Width="10%"/>
									<ItemTemplate>
										<asp:LinkButton ID="lbDelete" CommandName="Delete" Text="Delete" CausesValidation="False" RunAt="Server" />
										<asp:Label Text='<%# Trim(Container.DataItem("DeleteDependency")) %>' ID="lblDeleteDependency" Visible="False" RunAt="Server" />
										<asp:Label Text='<%# Trim(Container.DataItem("WorkCode")) %>' ID="lblWorkCode" Visible="False" RunAt="Server" />
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
				    <td Width="20%" Height="25">Remark :</td>
					<td Width="80%" ColSpan="5"><asp:TextBox ID="txtRemark" Width="100%" MaxLength="128" RunAt="Server"/></td>
				</tr>
				<tr>
					<td ColSpan=6><asp:Label ID="lblActionResult" Visible="False" ForeColor="Red" RunAt="Server" />&nbsp;</td>
				</tr>
				<tr>
					<td align="left" ColSpan="6">
						<asp:ImageButton ID="ibSave"              AlternateText="Save"                 OnClick="ibSave_OnClick"              ImageURL="../../images/butt_save.gif"                 CausesValidation="False" RunAt="Server" />
						<asp:ImageButton ID="ibDelete"            AlternateText="Delete"               OnClick="ibDelete_OnClick"            ImageURL="../../images/butt_delete.gif"               CausesValidation="False" RunAt="Server" />
						<asp:ImageButton ID="ibPartsIssueReturn"  AlternateText="Parts Issue / Return" OnClick="ibPartsIssueReturn_OnClick"  ImageURL="../../images/butt_new_partsissuereturn.gif" CausesValidation="False" RunAt="Server" />
						<asp:ImageButton ID="ibProceedToJobClose" AlternateText="Proceed To Job Close" OnClick="ibProceedToJobClose_OnClick" ImageURL="../../images/butt_proceed_jobclose.gif"     CausesValidation="False" RunAt="Server" />
						<asp:ImageButton ID="ibGenerateDNCN"      AlternateText="Generate DN / CN"                                           ImageURL="../../images/butt_gen_dncn.gif"             CausesValidation="False" RunAt="Server" />
						<asp:ImageButton ID="ibNew"               AlternateText="New"                  OnClick="ibNew_OnClick"               ImageURL="../../images/butt_new.gif"                  CausesValidation="False" RunAt="Server" />
						<asp:ImageButton ID="ibBack"              AlternateText="Back"                 OnClick="ibBack_OnClick"              ImageURL="../../images/butt_back.gif"                 CausesValidation="False" RunAt="Server" />
					</td>
				</tr>
				<tr>
					<td align="left" ColSpan="6">
                                            &nbsp;</td>
				</tr>
			</table>
			<input Type=Hidden ID="hidBlockCharge" Value="" RunAt="Server" />
			<input Type=Hidden ID="hidChargeLocCode" Value="" RunAt="Server" />

        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>
	</body>
</html>

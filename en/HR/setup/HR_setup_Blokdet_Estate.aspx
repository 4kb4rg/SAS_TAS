<%@ Page Language="vb" src="../../../include/HR_setup_Blokdet_Estate.aspx.vb" Inherits="HR_setup_Blokdet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Block of Division Details</title>
                <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">

.button-small {
	border: thin #009EDB solid;
	text-align:center;
	text-decoration:none;
	padding: 5px 10px 5px 10px;
	font-size: 7pt;
	font-weight:bold;
	color: #FFFFFF;
	background-color: #009EDB;
}
        </style>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
                   <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:Label id="lblSelect" visible="false" text="Please select " runat="server" />
			<asp:Label id="lblList" visible="false" text="Select " runat="server" />
			
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>                    
					<td colspan="5"><strong> <asp:label id="lblTitle" runat="server" /> BLOCK DETAILS </strong></td>
				</tr>
				<tr>
					<td colspan=6>
                        <hr style="width :100%" />
                    </td>
				</tr>
										
				<tr>
					<td width=20%><asp:label id="lblDepartment" runat="server" >Division </asp:label> ID :* </td>
					<td width=30%>
						<asp:DropDownList id=ddlDivId width="60%" CssClass="font9Tahoma" runat=server/>
                        <br />
						<asp:Label id=lblNoDeptCode forecolor=red visible=false runat=server/>
						<%--<asp:Label id=lblErrDupDept forecolor=red visible=false text="<br>The code has been used, please select another code." runat=server/>--%>
					</td>
					<td style="width: 79px">&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>								
				</tr>				
				
				<tr>
					<td align="left">No Blok :* </td>
					<td align="left">
						<asp:Textbox id=txtNoBlok maxlength=4 width="60%" CssClass="font9Tahoma" runat=server />
						<asp:Label id=validateNoBlok forecolor=red visible=false runat=server />
					</td>
					<td style="width: 79px">&nbsp;</td>
					<td width=20%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>								
				</tr>	
				
				<tr>
					<td align="left">Luas :</td>
					<td align="left">
                        <asp:Textbox id=txtLuas maxlength=64 width="60%" onkeypress="javascript:return isNumberKey(event)" CssClass="font9Tahoma" runat=server></asp:Textbox>
					</td>
					<td style="width: 79px">&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td align="left">Total PKK :</td>
					<td align="left">
                        <asp:Textbox id=txtTotPKK maxlength=64 width="60%" onkeypress="javascript:return isNumberKey(event)"  CssClass="font9Tahoma" runat=server></asp:Textbox>
					</td>
					<td style="width: 79px">&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td>
                        <asp:label id="lblDeptHead" runat="server" >Year Plan : *</asp:label>
                        :</td>
					<td>                        
						<asp:DropDownList id=ddlYearPlan width="60%" OnSelectedIndexChanged="ddlYearPlan_selectedindexchanges" AutoPostBack=true CssClass="font9Tahoma" runat=server/>
                        <br />
                        <asp:Label id=lblYearPlant forecolor=Red visible=False runat=server/><%--<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','ddlDeptHead','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/></td>--%><td style="width: 79px">&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="left">Berat Jenis Rata - rata :</td>
					<td align="left">
                        <asp:Textbox id=txtBJR maxlength=64 width="60%" onkeypress="javascript:return isNumberKey(event)" CssClass="font9Tahoma" runat=server></asp:Textbox>
					</td>
					<td style="width: 79px">&nbsp;</td>					
					<td><asp:Label id=lblCodediv Visible=false runat=server /></td>
					<td><asp:Label id=lblDivHead Visible=false runat=server /></td>
				</tr>

				<td colspan="5" style="height: 23px">&nbsp;</td>
				<tr>
					<td colspan="5" style="height: 28px">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " imageurl="../../images/butt_delete.gif" CausesValidation=False onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					    <br />
					</td>
				</tr>
				<Input Type=Hidden id=BlokCode runat=server />
				<asp:Label id=lblNoRecord visible=false text="Blok details not found." runat=server/>
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>								
			</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>

<%@ Page Language="vb" src="../../../include/PR_setup_THRDet.aspx.vb" Inherits="PR_setup_THRDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head> 
		<title>ASTEK Details</title>
                <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />

	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>

	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">
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
						<UserControl:MenuPRSetup id=MenuPRSetup runat="server" />
					</td>
				</tr>
				<tr>                    
					<td  colspan="5">
                        &nbsp;<table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                  <strong> THR DETAILS</strong> </td>
                                <td class="font9Header" style="text-align: right">
                                    Status : <asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Last Updated : <asp:Label id=lblLastUpdate runat=server />&nbsp;| 
                                    Update By : <asp:Label id=lblUpdatedBy runat=server />
                                </td>
                            </tr>
                        </table>
                         <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td style="width: 320px">
                        THR.Id</td>
					<td style="width: 296px">                        
                        <asp:TextBox ID="txtAstekCd" CssClass="font9Tahoma" runat="server" MaxLength="64" enabled=False Width="80%"></asp:TextBox>
                        <asp:Label id=lblAstekCd forecolor=Red visible=False runat=server/>                        
					<td width=5%>&nbsp;</td>
					<td width=20%>&nbsp;</td>
					<td width=25%>&nbsp;</td>								
				</tr>		
				<tr>
					<td style="width: 320px">
                        Periode Bayar</td>
					<td style="width: 296px">                        
                        <asp:DropDownList id="ddlMonth" width="50%" runat=server>
										<asp:ListItem value="01">January</asp:ListItem>
										<asp:ListItem value="02">February</asp:ListItem>
										<asp:ListItem value="03">March</asp:ListItem>
										<asp:ListItem value="04">April</asp:ListItem>
										<asp:ListItem value="05">May</asp:ListItem>
										<asp:ListItem value="06">June</asp:ListItem>
										<asp:ListItem value="07">July</asp:ListItem>
										<asp:ListItem value="08">Augustus</asp:ListItem>
										<asp:ListItem value="09">September</asp:ListItem>
										<asp:ListItem value="10">October</asp:ListItem>
										<asp:ListItem value="11">November</asp:ListItem>
										<asp:ListItem value="12">December</asp:ListItem>
									</asp:DropDownList>
									<asp:DropDownList id=ddlyear width="30%" runat="server" /></td>
                        
					<td width=5%>&nbsp;</td>
					<td width=20%>&nbsp;</td>
					<td width=25%>&nbsp;</td>								
				</tr>				
				<tr>
					<td style="height: 45px; width: 320px;">
                        Periode cutoff Start-End : </td>
					<td align="left" style="width: 296px">
                        <asp:TextBox ID="txtPStart" runat=server MaxLength="10" Width="35%"></asp:TextBox>
                        <a href="javascript:PopCal('txtPStart');"><asp:Image  runat="server" ImageUrl="../../images/calendar.gif"/></a> 
						- 
						<asp:TextBox ID="txtPEnd" runat=server MaxLength="10" Width="35%"></asp:TextBox>
                        <a href="javascript:PopCal('txtPEnd');"><asp:Image  runat="server" ImageUrl="../../images/calendar.gif"/></a> 
						
						</td>
					<td width=5% style="height: 45px">&nbsp;</td>
					<td style="height: 45px">&nbsp;</td>
					<td style="height: 45px">&nbsp;</td>								
				</tr>
				<tr>
					<td style="height: 45px; width: 320px;">
                        Agama : </td>
					<td align="left" style="width: 296px">
                        <asp:DropDownList id="ddlreligion" width="80%" runat=server /></td>
					<td width=5% style="height: 45px">&nbsp;</td>
					<td style="height: 45px">&nbsp;</td>
					<td style="height: 45px">&nbsp;</td>								
				</tr>
				
				<tr>
					<td align="left" style="width: 320px">Rp.Tambahan</td>
					<td align="left" style="width: 296px"> <asp:TextBox ID="txtdaging" runat="server" Width="80%" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
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
				<Input Type=Hidden id=AstekCd runat=server />
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

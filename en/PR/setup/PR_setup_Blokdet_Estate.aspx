<%@ Page Language="vb" src="../../../include/PR_setup_Blokdet_Estate.aspx.vb" Inherits="PR_setup_Blokdet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Block of Division Details</title>
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
           <table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>                    
					<td  colspan="5">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                     <strong>   BLOCK DETAILS </strong></td>
                                <td class="font9Header" style="text-align: right">
                                    Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Status : <asp:Label id=lblStatus runat=server />&nbsp;| Last Updated : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Updated By : <asp:Label id=lblUpdatedBy runat=server />
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
					<td width=20% >
                        Block Code</td>
					<td style="width: 281px" >
						<asp:Textbox id=txtNoBlok maxlength=8 width="60%" CssClass="font9Tahoma" runat=server />
					<td style="width: 79px">&nbsp;</td>
					<td >&nbsp;</td>
					<td >&nbsp;</td>								
				</tr>				
				
				<tr>
					<td align="left">
                        Deskripsi Block</td>
					<td align="left" style="width: 281px">
                        <asp:Textbox id=txtdeskripsi maxlength=64 width="100%" CssClass="font9Tahoma" runat=server></asp:Textbox></td>
					<td style="width: 79px">&nbsp;</td>
					<td width=20%>&nbsp;</td>
					<td width=25%>&nbsp;</td>								
				</tr>	
				
				<tr>
					<td align="left" style="height: 22px">
                        Tahun Tanam Code</td>
					<td align="left" style="height: 22px; width: 281px;">
                        <asp:DropDownList ID="ddlypcode" CssClass="font9Tahoma" runat="server" Width="100%">
                        </asp:DropDownList></td>
					<td style="width: 79px; height: 22px;">&nbsp;</td>
					<td style="height: 22px">&nbsp;</td>
					<td style="height: 22px">&nbsp;</td>
				</tr>
				<tr>
					<td align="left">
                        Tanggal Tanam</td>
					<td align="left" style="width: 281px">
                        <asp:Textbox id=txtdtplant maxlength=64 width="60%" CssClass="font9Tahoma" runat=server></asp:Textbox>
                        <a href="javascript:PopCal('txtdtplant');"><asp:Image id="btnSelPlantDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
                    </td>
					<td style="width: 79px">&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>
                        Type Block</td>
					<td style="width: 281px">                        
                        <asp:RadioButton ID="rbTypeInMatureField" runat="server" GroupName="blocktype" Text=" Immature Field" />
                        <br />
                        <asp:RadioButton ID="rbTypeMatureField"   runat="server" Checked="true" GroupName="blocktype" Text=" Mature Field" /><br />
                        <asp:RadioButton ID="rbTypeNursery"       runat="server" GroupName="blocktype" Text=" Nursery" />
                    <td style="width: 79px">&nbsp;</td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td align="left">Total Area</td>
					<td align="left" style="width: 281px">
                        <asp:Textbox id=txttarea maxlength=64 width="60%" onkeypress="javascript:return isNumberKey(event)" CssClass="font9Tahoma" runat=server></asp:Textbox>
					</td>
					<td style="width: 79px">&nbsp;</td>					
					<td></td>
					<td></td>
				</tr>
				
				<tr>
					<td align="left" style="height: 28px">Total Pokok</td>
					<td align="left" style="width: 281px; height: 28px">
                        <asp:Textbox id=txttpokok maxlength=64 width="60%" onkeypress="javascript:return isNumberKey(event)" CssClass="font9Tahoma" runat=server></asp:Textbox>
					</td>
					<td style="width: 79px; height: 28px;">&nbsp;</td>					
					<td style="height: 28px"></td>
					<td style="height: 28px"></td>
				</tr>
				
				<tr>
					<td align="left">Bjr</td>
					<td align="left" style="width: 281px">
                        <asp:Textbox id=txtbjr maxlength=64 width="60%" onkeypress="javascript:return isNumberKey(event)" CssClass="font9Tahoma" runat=server></asp:Textbox></td>
					<td style="width: 79px">&nbsp;</td>					
					<td></td>
					<td></td>
				</tr>

				<tr>
					<td align="left"></td>
					<td align="left" style="width: 281px">
                        </td>
					<td style="width: 79px">&nbsp;</td>					
					<td></td>
					<td></td>
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
				<Input Type=Hidden id=isNew runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>								
			</table>
         	</div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>

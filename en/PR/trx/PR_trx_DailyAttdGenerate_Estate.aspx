<%@ Page Language="vb" src="../../../include/PR_trx_DailyAttdGenerate_Estate.aspx.vb" Inherits="PR_trx_DailyAttdGenerate_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Attendance Details</title>
                     <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
				<script language="javascript">
		          
          
            function OnCheckedChanged(obj)     
            {
                     
              if (!obj.checked) return;  
              if (obj.value == "rbbulan") 
              {
               document.getElementById('dv1').style.display = "block";  
               document.getElementById('dv2').style.display = "none";  
              }
              
              if (obj.value == "rbtanggal")
            {
               document.getElementById('dv2').style.display = "block";  
               document.getElementById('dv1').style.display = "none";  
              }
                
             }

        </script>
	</head>
	<body onunload="window.opener.location=window.opener.location;" onload="javascript:self.focus();">
		<Preference:PrefHdl id=PrefHdl runat="server" />
		
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
          <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component."  ForeColor=red runat=server />
			<asp:Label id="lblRedirect" visible="false" runat="server" />
			<asp:Label id="lblReback" visible="false" runat="server" />
			<table border=0 cellspacing=0 cellpadding=2 width=99% id="TABLE1" class="font9Tahoma">
				<tr>
					<td colspan="5"><UserControl:MenuPRTrx id=MenuPRTrx runat="server" /></td>
				</tr>
				<tr>
					<td  colspan="5">
                      <strong> GENERATE ABSENSI </strong> <br /> <hr style="width :100%" /> </td>
				</tr>
				<tr>
					<td colspan=5 style="height: 38px">&nbsp;</td>
				</tr>
				<tr>
					<td height=25 style="width: 142px">
                        Divisi :
                        </td>
                    <td colspan="4">
                        <asp:DropDownList ID="ddldivisicode" CssClass="font9Tahoma" runat="server" Width="100%" OnSelectedIndexChanged="ddldivisicode_OnSelectedIndexChanged" AutoPostBack=true>
                        </asp:DropDownList></td>
					
				</tr>
				
				<tr>
					<td height=25 style="width: 142px">
                        Mandor :
                    </td>
                    <td colspan="4">
                        <GG:AutoCompleteDropDownList ID="ddlmandorcode"  CssClass="font9Tahoma"  runat="server" Width="100%" OnSelectedIndexChanged="ddlmandorcode_OnSelectedIndexChanged" AutoPostBack=true />
                    </td>
				</tr>
				
				<tr>
					<td height=25 style="width: 142px">
                        Karyawan :
                    </td>
                    <td colspan="4" >
                        <GG:AutoCompleteDropDownList ID="ddlempcode"  CssClass="font9Tahoma"  runat="server" Width="100%" />
                    </td>
				</tr>
				
				<tr>
					<td style="height: 53px; width: 142px;">
                        <asp:RadioButton ID="rbbulan"  onclick="javascript:OnCheckedChanged(this)" runat="server" GroupName="GrpDate" Text="Periode Bulan"  />
                        <asp:RadioButton ID="rbtanggal" onclick="javascript:OnCheckedChanged(this)" runat="server" GroupName="GrpDate" Text="Periode Tanggal"  /></td>
                    <td colspan="4" style="height: 53px">
                    <div id="dv1" style="display:none">
                        <asp:DropDownList ID="ddlMonth"  runat="server" Width="70%">
                            <asp:ListItem Value="01">January</asp:ListItem>
                            <asp:ListItem Value="02">February</asp:ListItem>
                            <asp:ListItem Value="03">March</asp:ListItem>
                            <asp:ListItem Value="04">April</asp:ListItem>
                            <asp:ListItem Value="05">May</asp:ListItem>
                            <asp:ListItem Value="06">June</asp:ListItem>
                            <asp:ListItem Value="07">July</asp:ListItem>
                            <asp:ListItem Value="08">Augustus</asp:ListItem>
                            <asp:ListItem Value="09">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtyear" runat="server" MaxLength="4" Width="25%"></asp:TextBox><br />
                       </div>
                       <div id="dv2" style="display:none">
                        <asp:TextBox ID="txtstartdt" runat="server" MaxLength="10" Width="50%"></asp:TextBox>&nbsp;
                        <a href="javascript:PopCal('txtstartdt');"><asp:Image id="btnSelDOJ" runat="server" ImageUrl="../../Images/calendar.gif"/></a>&nbsp; s/d
                        <asp:TextBox ID="txtenddt" runat="server" MaxLength="10" Width="50%"></asp:TextBox>&nbsp;
                        <a href="javascript:PopCal('txtenddt');"><asp:Image id="Image1" runat="server" ImageUrl="../../Images/calendar.gif"/></a>&nbsp;
                        </div>
                        </td>
                       
				</tr>
				<tr>
					<td style="width: 142px; height: 27px">
                        Absent</td>
                    <td colspan="4" style="height: 27px"><asp:DropDownList ID="ddlabsent"  CssClass="font9Tahoma"  runat="server" Width="50%">
                    <asp:ListItem Value="K" Selected>Kerja</asp:ListItem>
                    <asp:ListItem Value="M">Mangkir</asp:ListItem>
                    </asp:DropDownList></td>
				</tr>
				
				<tr >
					<td colspan=5 style="height: 28px">
					<asp:ImageButton id=SaveBtn OnClick=SaveBtn_Click AlternateText="  Save  " CausesValidation=False imageurl="~/en/images/butt_process.gif" runat=server />
                    <asp:ImageButton id=BackBtn OnClick=BackBtn_Click AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_close.gif"  runat=server />
					<asp:HiddenField ID=ref runat=server />
					&nbsp;</td>
				</tr>
				       </table>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>

<%@ Page Language="vb" src="../../../include/PR_trx_DailyAttdDet_Estate.aspx.vb" Inherits="PR_DailyAttdDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Attendance Details</title>
             <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">
		  function OnCheckedChanged(obj)     
          {
                     
              if (obj.checked) 
              {
               document.getElementById('divhk').style.display = "block";  
              }
              else
              {
               document.getElementById('divhk').style.display = "none";  
              }
                
          }
             
		  function keypress() {
			if (event.keyCode == 27)	//escape key press
				window.close();			//close window
		}
		
   		 function CheckValue()     
            {
                     
            var frm = document.forms[0];
              
              for(i=0;i< frm.length;i++)
              {
                e=frm.elements[i];
                if (e.id='txtHk')
                {
                  if (e.value >2 )     
                  {
                            e.value = 1
                            alert('Max value 2')
                            exit;
                   }
                }
              }
                         
            }
            
            function ddlchange()
            {
             var frm = document.forms[0];
              
              for(i=0;i< frm.length;i++)
              {
                e=frm.elements[i];
                    if (e.id=='ddlabsent' && e.value != 'M')
                    {
                        if (frm.elements[i+1].value=='BHL')
                         frm.elements[i+2].value = '0.71';
                        else
                          frm.elements[i+2].value = '1';
                    }  

                   	if (e.id=='ddlabsent' && (e.value=='M' || e.value=='P1'))				
                    {
                     frm.elements[i+2].value = '0';
                    }             
                        
               }
            }
		</script>
		
 	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
		
 	</head>
	<body onunload="window.opener.location=window.opener.location;" onload="javascript:self.focus();">
		<Preference:PrefHdl id=PrefHdl runat="server" />
		
		
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">

                  <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 270px" valign="top">
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
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                  <strong>  ABSENSI HARIAN DETAIL</strong></td>
                            </tr>
                        </table>
                        <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=5 style="height: 38px">&nbsp;</td>
				</tr>
				<tr>
					<td width=15% height=25>
                        Kode Absensi :
                        </td>
					<td style="width: 425px"><asp:Label id=Lblattcode runat=server />
                        </td>
					<td>&nbsp;</td>
					<td width=15%>Period : </td>
					<td style="width: 267px"><asp:Label id=lblPeriod runat=server /></td>
					
				</tr>
				<tr>
					<td height=25>
                        NIK :
                    </td>
					<td style="width: 425px; height: 27px"><asp:Label id=LblEmpCode runat=server /></td>
					<td >&nbsp;</td>
					<td >Status : </td>
					<td style="width: 267px;"><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td style="height: 25px">
                        Nama :</td>
					<td style="width: 425px; height: 25px;" valign="top"><asp:Label id=LblEmpName runat=server /></td>
					<td style="height: 25px">&nbsp;</td>
					<td style="height: 25px">Tgl Buat : </td>
					<td style="width: 267px;"><asp:Label id=lblCreateDate runat=server /></td>
				</tr>
				<tr>
					<td height=25>
                        Tanggal :</td>
					<td style="width: 425px; height: 27px">
					    <asp:Label id=LblAttDate runat=server />
                        <asp:Label id=LblAttDatetmp Visible=false runat=server />
                        <asp:Label id=lblaa runat=server Visible="False" /></td>       
					<td >&nbsp;</td>
					<td >Tgl update : </td>
					<td style="width: 267px;"><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td height=25>
                        Absent</td>
					<td style="width: 425px; height: 27px;">
                        <asp:DropDownList ID="ddlabsent" runat="server" Width="40%" onchange="ddlchange()" />
                        <asp:HiddenField ID="ttt" runat="server"/>
                    </td>
					<td >&nbsp;</td>
					<td >Diupdate : </td>
					<td style="width: 267px;"><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td height=25>
                        HK</td>
					<td style="width: 425px; height: 27px;">
                        <asp:TextBox ID="txtHk" MaxLength=4 ReadOnly=true Width="40%" CssClass="mr-h" runat="server"></asp:TextBox>
                        <asp:CheckBox ID="chk_rev" Text=" Koreksi HK" runat="server" OnCheckedChanged="chk_rev_changed" AutoPostBack="true" /></td>
					<td >&nbsp;</td>
					<td >&nbsp;</td>
					<td style="width: 267px;">&nbsp;</td>
				</tr>
				<tr>
					<td height=25></td>
					<td style="width: 425px; height: 27px;">
                    <div id="divhk" visible="False" runat="server" class="mb-c">
                    <table border="0" cellspacing="1" cellpadding="1" width="100%">
                     <tr>
                        <td style="width: 30%">Hk koreksi : </td><td style="width: 70%">
						<asp:DropDownList ID="txtrev_hk" Width="60%" runat="server">
						<asp:ListItem value="0">0 Jam (0 HK)</asp:ListItem>
						<asp:ListItem value="0.14">1 Jam (0.14HK)</asp:ListItem>
						<asp:ListItem value="0.29">2 Jam (0.29 HK)</asp:ListItem>
						<asp:ListItem value="0.43">3 Jam (0.43 HK)</asp:ListItem>
						<asp:ListItem value="0.57">4 Jam (0.57 HK)</asp:ListItem>
						<asp:ListItem value="0.71">5 Jam (0.71 HK)</asp:ListItem>
						<asp:ListItem value="0.86">6 Jam (0.86 HK)</asp:ListItem>
						</asp:DropDownList>
						
						</td>
                     </tr>
                     <tr>
                        <td style="width: 30%; height: 26px;">Keterangan : </td><td style="width: 100%; height: 26px;"><asp:TextBox ID="txtrev_ket" Width="100%" MaxLength=100 runat="server"></asp:TextBox></td>
                     </tr>
                     <tr>
                        <td style="width: 30%"></td><td style="width: 70%"></td>
                     </tr>
                     <tr>
                        <td style="width: 30%"></td><td style="width: 70%"></td>
                     </tr>
                     </table>
                    </div>     
                    </td>
					<td >&nbsp;</td>
					<td >&nbsp;</td>
					<td style="width: 267px;">&nbsp;</td>
				</tr>
				
				<tr >
					<td colspan=5 style="height: 28px">
					<asp:ImageButton id=SaveBtn OnClick=SaveBtn_Click1 AlternateText="  Save  " CausesValidation=False imageurl="../../images/butt_save.gif" runat=server />
                    <asp:ImageButton id=DelBtn OnClick=DelBtn_Click AlternateText="  Delete  " CausesValidation=False imageurl="../../images/butt_delete.gif"  runat=server />
                    <asp:ImageButton id=BackBtn OnClick=BackBtn_Click AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_close.gif"  runat=server />
					    <br />
					</td>
				</tr>
				       </table>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>

<%@ Page Language="vb" src="../../../include/HR_trx_EmployeeDet_Estate.aspx.vb" Inherits="HR_EmployeeDet" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Employee Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            .zoom {
            padding: 10px;
            transition: transform .3s; /* Animation */
            width: auto;
            height: auto;
            margin: 0 auto;
            }

            .zoom:hover {
            transform: scale(2.8); /* (150% zoom - Note: if the zoom is too large, it will go outside of the viewport) */
            }
            </style>
	</head>
	<body>
	<script language="JavaScript">
	   
	function FormatCurrency(objNum)
               {
                    var num = objNum.value.replace('$','');
                    var ent, dec, dot;
					
										
                    if (num != '' && num != objNum.oldvalue)
                    {
					  					 
					     num = MoneyToNumber(num);
											 
																 
						 if (!isNaN(num)||num.toString().substr(0,1)=='-')
                         {
						 
						      var ev = (navigator.appName.indexOf('Netscape') != -1)?Event:event;
                              ent = num.split('.')[0];
                              dec = num.split('.')[1];
						
						    if (dec || ev.keyCode == 190)
                            {
                               dot = '.';
                               if (dec.toString().length > 2) dec = dec.toString().substr(0,2);
                            }
                            else
                            {
                               dec = '';
                               dot = '';
                            }
						    
 							  objNum.value = AddCommas(ent) + dot + dec;
                              objNum.oldvalue = objNum.value;
                         }
                      objNum.value = '' + objNum.oldvalue;
                    }
               }
             
            function MoneyToNumber(num)
                {
                    return (num.replace(/,/g, ''));
                 }
         
            function AddCommas(num)
               {
			        
                    numArr=new String(num).split('').reverse();
					
					if (numArr[numArr.length-1]== '-')
					{
						for (i=4;i<numArr.length;i+=3)
						{ 
					    numArr[i-1]+=',';
						}
					}
					else
					{
						for (i=3;i<numArr.length;i+=3)
						{ 
					    numArr[i]+=',';
						}
					}
                    return numArr.reverse().join('');
               }
         
    </script>


		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>
               <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 800px" valign="top">
			    <div class="kontenlist"> 
			<asp:Label id=lblErrMessage visible=False text="Error while initiating component." runat=server Font-Bold="True" Font-Size="X-Small" ForeColor="Red" />
			<asp:Label id=lblRedirect visible=false runat=server />
			<table border=0 cellspacing="1" width="99%" id="TABLE1"  class="font9Tahoma">
				<tr>
					<td class="mt-h" colspan="5" style="height: 21px">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                   <strong>DETAIL KARYAWAN </strong> </td>
                                <td class="font9Header" style="text-align: right">
                        Status : <asp:Label id=lblStatus runat=server />&nbsp;| Tgl Buat :
                        <asp:Label id=lblDateCreated runat=server />&nbsp;|
                        Tgl Update :<asp:Label id=lblLastUpdate runat=server />&nbsp;|
                        Diupdate :<asp:Label id=lblUpdateBy runat=server />
                                </td>
                            </tr>
                        </table>
                        <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5 align="center"></td>
				</tr>
				<tr id="TRMutasi" runat=server>
					<td width=20% height=25>
                        <asp:CheckBox ID="chkmutasi" runat="server" Text="  Mutasi dari Unit: " OnCheckedChanged="chkmutasi_OnCheckedChanged" AutoPostBack=true /></td>
					<td style="width: 295px">
						 <asp:DropDownList id=ddlmutasi width=100% runat=server OnSelectedIndexChanged="DDListRedirect2" AutoPostBack="True"/>
					</td>
					<td style="width: 17px">&nbsp;</td>
					<td style="width: 120px">
                        </td>
					<td style="width: 157px">
                        </td>
				</tr>
              	<tr>
					<td width=20% height=25>
                        NIK / ID Absen : *</td>
					<td style="width: 295px">
						<asp:Textbox id=txtEmpCode   width=50% maxlength=20  runat=server CssClass="font9Tahoma" /> &nbsp;
						<asp:Textbox id=txtEmpAttid width=45% maxlength=20 runat=server CssClass="font9Tahoma" /> 
					</td>
					<td style="width: 17px">&nbsp;</td>
					<td style="width: 120px">
                        &nbsp;</td>
					<td style="width: 157px">
                        &nbsp;</td>
				</tr>
                <tr>
                    <td style="height: 24px">
                        Tipe Karyawan :*
                        </td>
                    <td style="width: 295px; height: 24px;">
                       <asp:DropDownList id=ddlEmpType width=100% OnSelectedIndexChanged="DDListRedirect" AutoPostBack="True" runat=server /></td>
                    <td style="width: 17px; height: 24px;">
                    </td>
                    <td style="width: 120px; height: 24px;">
                        &nbsp;</td>
                    <td style="width: 157px; height: 24px;">
                        &nbsp;</td>
                </tr>
                <tr>
				    
					<td height=20%>
                        Nama :*</td>
                    <td style="width: 295px"><asp:TextBox id=txtEmpName width=100% maxlength=64 runat=server CssClass="font9Tahoma"/></td>
                    
                    <td style="width: 17px">&nbsp;</td>
					<td style="width: 120px; height: 21px;">
                        &nbsp;</td>
                    <td style="width: 157px; height: 21px;">
                        &nbsp;</td>
                </tr>
                <tr>
				    <td style="height: 21px">
                        Tgl. Masuk Kerja: *</td>
                    <td style="width: 295px"><asp:TextBox id=txtDOJ width=50% OnTextChanged ="DDListRedirect"  AutoPostBack="True" runat=server CssClass="font9Tahoma"/>
                    <a href="javascript:PopCal('txtDOJ');"><asp:Image id="btnSelDOJ" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
                        &nbsp;
					</td>
                    
                    <td style="width: 17px">&nbsp;</td>
					<td style="width: 120px; height: 21px;">
                        &nbsp;</td>
                    <td style="width: 157px; height: 21px;">
                        &nbsp;</td>
                </tr>
				<tr>
				    <td style="height: 21px">
                        Tgl. Menjadi Kary Tetap :
                    </td>
                    <td style="width: 295px"><asp:TextBox id=txtDOP width=50% runat=server CssClass="font9Tahoma"/>
                    <a href="javascript:PopCal('txtDOP');"><asp:Image id="Image2" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
                        &nbsp;
					</td>
				    
					<td style="width: 17px; height: 50px">&nbsp;</td>
					<td style="width: 120px; height: 50px"></td>		
                    <td rowspan="7" style="width: 157px">
                        <asp:Image class="zoom" ID="imgphoto" runat="server" Height="156px" Width="123px" /></td>
				</tr>
                <tr>
                    <td style="height: 19px">
                        Tempat/Tgl. Lahir :*</td>
					<td style="height: 19px; width: 295px;">	<asp:TextBox id=txtPOB width="48%" maxlength=64  runat=server Height="23px" CssClass="font9Tahoma"/>
                        /
                        <asp:TextBox id=txtDOB width="38%" runat=server CssClass="font9Tahoma"/>
						<a href="javascript:PopCal('txtDOB');"><asp:Image id="btnSelDOB" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
                    </td>
					<td style="width: 17px; height: 19px;">&nbsp;</td>
					<td style="width: 120px; height: 14px;"></td>
				</tr>
				<tr>
					<td style="height: 14px">
                        Jenis Kelamin :*</td>
					<td style="height: 14px; width: 295px;"><asp:DropDownList id=ddlSex width="50%" runat=server/></td>
					<td style="width: 17px; height: 14px;">&nbsp;</td>
					<td style="width: 120px; height: 14px;"></td>
				</tr>
                <tr>
                    <td height="20%">
                        Status Perkawinan :*</td>
                    <td style="width: 295px"> <asp:DropDownList id=ddlMarital width="50%" runat=server/></td>
                    <td style="width: 17px"></td>
                    <td></td>
                </tr>
				<tr>
					<td style="height: 14px">
                        Tanggungan Pajak : *</td>
					<td style="width: 295px; height: 14px;"><asp:DropDownList id=ddltangungan width="50%" runat=server /></td>
					<td style="width: 17px; height: 14px;">&nbsp;</td>
					<td style="width: 120px; height: 14px;"></td>

				</tr>
				<tr>
					<td style="height: 14px">
                        Tanggungan Kesehatan: *</td>
					<td style="width: 295px; height: 14px;"><asp:DropDownList id=ddltangunganhlt width="50%" runat=server /></td>
					<td style="width: 17px; height: 14px;">&nbsp;</td>
					<td style="width: 120px; height: 14px;"></td>
				</tr>
				<tr>
					<td style="height: 17px">
                        Tanda Pengenal / No.</td>
					<td style="width: 295px; height: 17px"><asp:DropDownList id=ddlID width="30%" runat=server />
                        /
                        <asp:TextBox id=txtIDNo width="60%" maxlength=18 runat=server CssClass="font9Tahoma"/></td>
					<td style="width: 17px; height: 17px">&nbsp;</td>
					<td style="width: 120px; height: 17px"></td>
				</tr>
				<tr>
					<td style="height: 21px">
                        Agama</td>
                    <td style="width: 295px; height: 21px;">
                        <asp:DropDownList id=ddlReligion width="50%" runat=server /></td>
					<td style="width: 17px; height: 21px;">&nbsp;</td>
					<td style="width: 120px; height: 21px;"></td>
				</tr>
				<tr>
					<td style="height: 27px">
                        Gol.Darah</td>
					<td style="width: 295px; height: 27px;">
                        <asp:DropDownList ID="ddlbloodtype" width="30%" runat="server" >
                            <asp:ListItem Value="" Text="-"></asp:ListItem>
                            <asp:ListItem Value="A" Text="A"></asp:ListItem>
                            <asp:ListItem Value ="B" Text="B"></asp:ListItem>
                            <asp:ListItem Value="AB" Text="AB"></asp:ListItem>
                            <asp:ListItem Value="O" Text="O"></asp:ListItem>
                        </asp:DropDownList></td>
					<td style="width: 17px; height: 27px;">&nbsp;</td>
					<td style="width: 120px; height: 27px;">
                        </td>
					<td style="width: 157px; height: 27px;">
                        <asp:FileUpload ID="flUpload" runat="server" /> 
					    <asp:ImageButton id=btnUpload imageurl="../../images/butt_upload.gif" AlternateText="Upload foto" onclick=UploadBtn_Click runat=server />
						
					</td>
				</tr>
				
				<tr>
					<td height=20%>
                        No.Astek/Periode :</td>
                    <td style="height: 19px; width: 295px;">	<asp:TextBox id=txtastek width="48%" maxlength=25   runat=server Height="23px" CssClass="font9Tahoma"/>
                        /
                        <asp:TextBox id=txtastek_prd width="38%" maxlength=8 runat=server CssClass="font9Tahoma"/>
					</td>
					<td style="width: 17px; height: 50px">&nbsp;</td>
					<td style="width: 120px; height: 50px"></td>
					<td style="height: 50px; width: 157px;"></td>
				</tr>
								
				<tr>
					<td height=20%>
                        No.NPWP/Tgl NPWP :</td>
                    <td style="height: 19px; width: 295px;">	<asp:TextBox id=txtnpwp width="48%" maxlength=64  runat=server Height="23px" CssClass="font9Tahoma"/>
                        /
                        <asp:TextBox id=txtDONpwp width="38%" runat=server CssClass="font9Tahoma"/>
						<a href="javascript:PopCal('txtDONpwp');"><asp:Image id="btnSelDONpwp" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					</td>
					<td style="width: 17px; height: 50px">&nbsp;</td>
					<td style="width: 120px; height: 50px"></td>
					<td style="height: 50px; width: 157px;"></td>
				</tr>
				
				<tr>
					<td style="height: 14px">
                        Kantor Pelayanan Pajak : </td>
					<td style="width: 295px; height: 14px;"><asp:DropDownList id=txtkpp width="100%" maxlength=100 runat=server Height="23px" /></td>
					<td style="width: 17px; height: 14px;">&nbsp;</td>
					<td style="width: 120px; height: 14px;"></td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
			</table>
			
<%--Tab Payroll--%>
			
			<table id="TabHeadPayrol_Estate" cellSpacing="0" cellPadding="0" width="99%" runat="server"  class="font9Tahoma">
				<tr >
					<td class="lb-hti"></td>
				</tr>
			 </table>
			 
			 <%--<table id="TabPayrol_Estate" style="VISIBILITY: visible; POSITION: relative" cellSpacing="1" cellPadding="0" width="99%" border="0" runat="server">--%>
			  <table id="TabPayrol_Estate"  cellSpacing="1" cellPadding="0" width="99%" border="0" runat="server"  class="font9Tahoma">
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td style="height: 22px; width: 131px;">
                        Divisi &nbsp;: *</td>
					<td style="width: 227px; height: 22px;"><GG:AutoCompleteDropDownList ID="ddldivisi" OnSelectedIndexChanged="DDListRedirect2" AutoPostBack="True" runat="server" Width="153px" />
                    </td>
                     
					<td style="width: 17px; height: 22px;">&nbsp;</td>
					<td style="width: 120px; height: 22px;">
                        Jabatan : *</td>
					<td style="width: 157px; height: 22px;"><GG:AutoCompleteDropDownList ID="ddljabatan" runat="server" Width="97%" /></td>
				</tr>
				
				<tr>
					<td style="height: 20px; width: 131px;"></td>
					<td style="width: 227px;">
                        <asp:CheckBox ID="chkmandor" runat="server" Text="Mandor " /></td>
					<td style="width: 17px; height: 20px;">&nbsp;</td>
					<td style="width: 120px;">
                        Status Karyawan :</td>
					<td style="width: 157px;"><asp:DropDownList ID="ddlkrystat" runat="server" Width="97%"></asp:DropDownList></td>
				</tr>
				<tr>
					<td style="height: 19px; width: 131px;">
                        Kode Gaji : </td>
					<td style="width: 227px;"><asp:TextBox id=txtsalarycode width="93%" maxlength=64  runat=server Height="23px" ReadOnly="True" CssClass="font9Tahoma"/></td>
					<td style="width: 17px; height: 19px;"></td>
					<td style="width: 120px;">
                        Min Hk Pinjaman</td>
					<td style="width: 157px;"><asp:TextBox id=txtmin_pjman width="35%" maxlength=18 CssClass="font9Tahoma" runat=server   ReadOnly="True" onkeypress="javascript:return isNumberKey(event)" /></td>

				</tr>
				<tr>
					<td style="height: 20px; width: 131px;"></td>
					<td style="width: 227px;"><asp:CheckBox ID="chkgol" runat="server" Text="Sesuai Golongan" Enabled="False" /></td>
					<td style="width: 17px; height: 20px;">&nbsp;</td>
					<td style="width: 120px;">
                        Gaji Kecil / Pinjaman</td>
					<td style="width: 157px;"><asp:TextBox id=txtgajikecil width="97%" maxlength=18 runat=server  onkeypress="javascript:return isNumberKey(event)" /></td>
				</tr>
				<tr>
					<td style="height: 24px; width: 131px;">
                        Kode Golongan : *</td>
					<td style="width: 227px;"><GG:AutoCompleteDropDownList ID="ddlgol" runat="server" Width="153px" OnSelectedIndexChanged="ddlgol_OnSelectedIndexChanged" AutoPostBack=true /></td>
					<td style="width: 17px; height: 24px;">&nbsp;</td>
					<td style="width: 120px;">
                                    <asp:CheckBox ID="chkcatu" runat="server" Text="Natura Beras : "  />&nbsp;</td>
					<td style="width: 157px;">
                             <asp:TextBox id=txtberas width="97%" maxlength=18 runat=server CssClass="font9Tahoma" ReadOnly="True" /></td>
				</tr>
                     <tr>
                         <td style="height: 24px; width: 131px;">
                             Upah / Gaji : *
                         </td>
                         <td style="width: 227px;">
                             <asp:TextBox id=txtgajibesar width="93%" maxlength=18 runat=server oldvalue="" CssClass="font9Tahoma"/></td>
                         <td style="width: 17px; height: 24px">
                         </td>
                         <td style="width: 120px;">
                             Lembur / Hari :</td>
					     <td style="width: 157px;">
                             <asp:TextBox id=txtlembur width="97%" maxlength=18 runat=server  ReadOnly="True" CssClass="font9Tahoma"/></td>

                         
                     </tr>
                     <tr>
                         <td style="height: 24px; width: 131px;">
                             Premi Tetap : 
                         </td>
                         <td style="width: 227px;">
                             <asp:TextBox id=txtpremitetap width="93%" maxlength=18 runat=server onkeypress="javascript:return isNumberKey(event)" CssClass="font9Tahoma"/></td>
                         <td style="width: 17px; height: 24px">
                         </td>
                         <td style="width: 120px;">
						 <asp:CheckBox ID="chkmakan" runat="server" Text=" Makan : " Enabled="True" /></td>
                         <td style="width: 157px;">
                             <asp:TextBox id=txtmakan width="97%" runat=server   onkeypress="javascript:return isNumberKey(event)" MaxLength="18" ReadOnly="True" CssClass="font9Tahoma"/></td>
                     </tr>
                     <tr>
                         <td style=" height: 24px; width: 131px;">
                             Tunjangan Tetap : </td>
                         <td style="width: 227px; height: 24px;">
                             <asp:TextBox id=txttunjangan width="93%" maxlength=18 runat=server onkeypress="javascript:return isNumberKey(event)" CssClass="font9Tahoma"/></td>
                         <td style="width: 17px; height: 24px">
                         </td>
                         <td style="width: 120px; height: 24px;">
						 <asp:CheckBox ID="chktrans" runat="server" Text=" Transport :" Enabled="True" /></td>
                         <td style="width: 157px; height: 24px;">
                             <asp:TextBox id=txttrans width="97%" runat=server  onkeypress="javascript:return isNumberKey(event)" MaxLength="18" ReadOnly="True" CssClass="font9Tahoma"/></td>
                     </tr>
                     <tr>
                         <td style="height: 23px; width: 131px;">
                             Upah / Hari : * &nbsp;</td>
                         <td style="width: 227px;">
                             <asp:TextBox id=txtupah width="45%" maxlength=18 runat=server  ReadOnly="True" onkeypress="javascript:return isNumberKey(event)" CssClass="font9Tahoma"/>
                             Unit :
                             <asp:DropDownList ID="ddlunit" runat="server" Width="30%" >
							 <asp:ListItem Value="HK" Text="HK"></asp:ListItem>
							 </asp:DropDownList></td>
                         <td style="width: 17px; height: 23px">
                         </td>
                         <td style="width: 120px;">
                             Potongan Mangkir :</td>
                         <td style="width: 157px;">
							<asp:TextBox id=txtmangkir width="97%" maxlength=18 runat=server ReadOnly="True" CssClass="font9Tahoma"/></td>
                     </tr>
                     <tr>
                         <td style="height: 23px; width: 131px;">
                         </td>
                         <td style="width: 227px;">
                         </td>
                         <td style="width: 17px; height: 23px">
                         </td>
                         <td style="width: 120px;">
						 <asp:CheckBox ID="chkspsi" runat="server" Text=" Potongan SPSI :" Enabled="True" OnCheckedChanged="chkspsi_OnCheckedChanged" AutoPostBack=true/></td>
                         <td style="width: 157px;">
                             <asp:TextBox id=txtspsi width="97%" maxlength=18 runat=server onkeypress="javascript:return isNumberKey(event)" CssClass="font9Tahoma"/></td>
                     </tr>
                     <tr>
                         <td style="height: 22px; width: 131px;">
                             Jenis Karyawan :*</td>
                         <td style="width: 227px;"><asp:DropDownList ID="ddlpekerja" runat="server" Width="153px"></asp:DropDownList></td>
                         <td style="width: 17px; height: 22px">
                         </td>
                         <td style="width: 120px;">
                         </td>
                         <td style="width: 157px;">
						     <asp:CheckBox ID="chkastek" runat="server" Text=" BPJS-Tenaga Kerja-JKK " Enabled="True" Width="157px"/>
							 <asp:CheckBox ID="chkastekJKM" runat="server" Text=" BPJS-Tenaga Kerja-JKM " Enabled="True" Width="157px"/>
							 <asp:CheckBox ID="chkastekJHT" runat="server" Text=" BPJS-Tenaga Kerja-JHT " Enabled="True" Width="157px"/>
							 <asp:CheckBox ID="chkbpjs" runat="server"  Text=" BPJS-Kesehatan " Enabled="True" Width="157px"/>
							 <asp:CheckBox ID="chkjp" runat="server"    Text=" BPJS-Pensiun " Enabled="True" Width="157px"/>
							 <asp:CheckBox ID="chkbonus" runat="server" Text=" Bonus "  />
					     </td>
                     </tr>
                      <tr>
                         <td style="height: 22px; width: 131px;"> 
						     <asp:CheckBox ID="chknastek" runat="server" Text=" AstekNBeras " Enabled="True" Visible="False" Width="110px" />
                             <asp:TextBox id=txtpotongan width="97%" maxlength=18 runat=server visible=False CssClass="font9Tahoma"/>
                             </td>
                         <td style="width: 227px;"></td>
                         <td style="width: 17px; height: 22px">
                         </td>
                         <td style="width: 120px;">
                             </td>
                         <td style="width: 157px;">
						 </td>
                     </tr>
                      <tr>
                         <td style="height: 22px; width: 131px;">
                             </td>
                         <td style="width: 227px;"></td>
                         <td style="width: 17px; height: 22px">
                         </td>
                         <td colspan="2">
                             &nbsp;&nbsp;
                         </td>
                     </tr>
					<tr>
					<td height=25 colspan="5">
					    <asp:ImageButton id=btnSave imageurl="../../images/butt_save.gif" AlternateText="Save" onclick=btnSave_Click runat=server />
						<asp:ImageButton id=btnDelete imageurl="../../images/butt_delete.gif" CausesValidation=False AlternateText="Delete" onclick=btnDelete_Click runat=server />
						<asp:ImageButton id=btnUnDelete imageurl="../../images/butt_undelete.gif" CausesValidation=False AlternateText="UnDelete" onclick=btnUnDelete_Click runat=server />
						<asp:ImageButton id=btnConfirm imageurl="../../images/butt_confirm.gif" CausesValidation=False AlternateText="Confirm" onclick=btnUnDelete_Click runat=server />
						<asp:ImageButton id=btnBack imageurl="../../images/butt_back.gif" CausesValidation=False AlternateText="Back" onclick=btnBack_Click runat=server />
						<asp:ImageButton id=btnRefresh imageurl="../../images/butt_refresh.gif" CausesValidation=False AlternateText="Refresh" onclick=btnRefresh_Click runat=server />
			
					    <br />
			
					</td>
				</tr>
            </table>
                        
         	<table id="Table4" cellSpacing="0" cellPadding="0" width="100%" runat="server"  class="font9Tahoma">
							<tr>
								<td colSpan="2"><IMG height="0" src="../../images/spacer.gif" width="5" border="0"></td>
							</tr>
			</table>

<%--Tab Jam Kerja --%>

   		    <table id="TabHeadJamKerja_Estate" cellSpacing="0" cellPadding="0" width="99%" runat="server"  class="font9Tahoma">
				<tr height="20">
					<td width="20"></td>
					<td width="14"><IMG src="../../images/arow.gif" border="0" align="left"></td>
					<td class="lb-hti"><A class="mb-t" href="javascript:togglebox(TabAlamat_Estate);javascript:togglebox1(TabFamily_Estate);javascript:togglebox1(TabRPekerjaan_Estate);javascript:togglebox1(TabRPayroll_Estate);javascript:togglebox1(TabRPendidikan_Estate);javascript:togglebox1(TabRKursus_Estate);javascript:togglebox1(TabRPromosi_Estate);javascript:togglebox1(TabRKesehatan_Estate);javascript:togglebox1(TabTg_Estate);">Jam Kerja</A></td>
				</tr>
			 </table>
			 
			 <%--style="VISIBILITY: hidden; POSITION: absolute" --%>
			 	<table id="TabJaKerja_Estate" style="VISIBILITY: visible; POSITION: relative" cellSpacing="1" cellPadding="0" width="99%" border="0" runat="server"  class="font9Tahoma">
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
				<tr>	
				<td width="20"></td>
				<td width="14"></td>
				<td colspan="5">
                    <asp:DataGrid ID="datajam" 
                         runat="server" 
                         AutoGenerateColumns="False" 
                         GridLines = none
                         CellPadding="2"
                         OnEditCommand="JAMDEDR_Edit"
                         OnDeleteCommand="JAMDEDR_Delete"
                         OnUpdateCommand="JAMDEDR_Update"
                         OnCancelCommand="JAMDEDR_Cancel"  CssClass="font9Tahoma"
                         PageSize="6" Width="100%">
                                              <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>		
                        <AlternatingItemStyle CssClass="mr-r" />
                        <ItemStyle  />
                        <HeaderStyle />
                        <Columns>
                            <asp:TemplateColumn HeaderText="ID" ItemStyle-Width=3%>
                                <ItemTemplate>
                                    <%#Container.DataItem("ID")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="jamid" CssClass="font9Tahoma" runat="server"  Enabled=false Text='<%# trim(Container.DataItem("ID")) %>'  Width="95%"></asp:TextBox>
                                 </EditItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Periode Awal" ItemStyle-Width=7%>
                                <ItemTemplate>
                                    <%#Container.DataItem("PeriodeAwl")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="jampawal" CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("PeriodeAwl")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Periode Akhir" ItemStyle-Width=7%>
                                <ItemTemplate>
                                    <%#Container.DataItem("PeriodeAhr")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="jampakhir" CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("PeriodeAhr")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
							
							 <asp:TemplateColumn HeaderText="Hari" ItemStyle-Width=7%>
                                <ItemTemplate>
                                    <%#Container.DataItem("HariHari")%>
                                </ItemTemplate>
                                <EditItemTemplate>
								 <asp:DropDownList ID="hariddl" width="100%" runat="server" >
                                  <asp:ListItem Value=1 Text="Minggu"></asp:ListItem>
                                  <asp:ListItem Value=2 Text="Senin"></asp:ListItem>
								   <asp:ListItem Value=3 Text="Selasa"></asp:ListItem>
								    <asp:ListItem Value=4 Text="Rabu"></asp:ListItem>
									 <asp:ListItem Value=5 Text="Kamis"></asp:ListItem>
									  <asp:ListItem Value=6 Text="Jumat"></asp:ListItem>
									   <asp:ListItem Value=7 Text="Sabtu"></asp:ListItem>
                                   </asp:DropDownList>
								   
                                    
								  <asp:Label ID="harilbl" runat="server" Text='<%# trim(Container.DataItem("Hari")) %>' visible=False Width="100%"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="JamKerja#" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# Container.DataItem("JamKerja") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:DropDownList id="jamddl1" width=100% runat=server />
								  <asp:Label ID="jamlblkerja" runat="server" Text='<%# trim(Container.DataItem("CodeKerja")) %>' visible=False Width="100%"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="JamIstirahat#" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# Container.DataItem("JamIstirahat") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:DropDownList id="jamddl2" width=100% runat=server />
								  <asp:Label ID="jamlblrest" runat="server" Text='<%# trim(Container.DataItem("CodeRest")) %>' visible=False Width="100%"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Tgl Update" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox id="jamStatus" CssClass="font9Tahoma" Visible=False Text='<%# Container.DataItem("Status")%>' runat="server"/>
                                    <asp:TextBox ID="jamUpdateDate" CssClass="font9Tahoma" runat="server" ReadOnly="TRUE" size="8" Text='<%# objGlobal.GetLongDate(Now()) %>'
                                        Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="jamCreateDate" CssClass="font9Tahoma" runat="server" Text='<%# Container.DataItem("CreateDate") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="diupdated " ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# Container.DataItem("UserName") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn ItemStyle-Width=12%>
                                <ItemTemplate >
                                    <asp:LinkButton ID="Edit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="Update" runat="server" CommandName="Update" Text="Save"></asp:LinkButton>
                                    <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                    <asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"  Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
				</tr>
                     <tr>
                         <td style="height: 25px" width="20"></td>
                         <td style="height: 25px" width="14"></td>
                         <td style="height: 25px"><asp:ImageButton ID="btnAddJam" OnClick="btnAddJam_onClick" runat="server" AlternateText="New Jam Kerja" ImageUrl="../../images/butt_add.gif" /></td>
                         <td rowspan="1" style="width: 295px; height: 25px"></td>
                         <td style="width: 17px; height: 25px"></td>
                         <td style="width: 120px; height: 25px"></td>
                         <td style="width: 157px; height: 25px"></td>
                     </tr>
		        
		        <tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
                </table>
      
             <table id="Table5BA" cellSpacing="0" cellPadding="0" width="100%" runat="server"  class="font9Tahoma">
							<tr>
								<td colSpan="2"><IMG height="0" src="../../images/spacer.gif" width="5" border="0"></td>
							</tr>
			</table>			
			
<%--Tab NoREK Bank--%>

   		    <table id="TabHeadNoRekt_Estate" cellSpacing="0" cellPadding="0" width="99%" runat="server"  class="font9Tahoma">
				<tr height="20">
					<td width="20"></td>
					<td width="14"><IMG src="../../images/arow.gif" border="0" align="left"></td>
					<td class="lb-hti"><A class="mb-t" href="javascript:togglebox(TabAlamat_Estate);javascript:togglebox1(TabFamily_Estate);javascript:togglebox1(TabRPekerjaan_Estate);javascript:togglebox1(TabRPayroll_Estate);javascript:togglebox1(TabRPendidikan_Estate);javascript:togglebox1(TabRKursus_Estate);javascript:togglebox1(TabRPromosi_Estate);javascript:togglebox1(TabRKesehatan_Estate);javascript:togglebox1(TabTg_Estate);">Rekening Bank</A></td>
				</tr>
			 </table>
			 
			 <%--style="VISIBILITY: hidden; POSITION: absolute" --%>
			 	<table id="TabNoRek_Estate" style="VISIBILITY: visible; POSITION: relative" cellSpacing="1" cellPadding="0" width="99%" border="0" runat="server" class="font9Tahoma">
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
				<tr>	
				<td width="20"></td>
				<td width="14"></td>
				<td colspan="5">
                    <asp:DataGrid ID="dataNoRek" 
                         runat="server" 
                         AutoGenerateColumns="False" 
                         GridLines = none
                         CellPadding="2"
                         OnEditCommand="NREKDEDR_Edit"
                         OnDeleteCommand="NREKDEDR_Delete"
                         OnUpdateCommand="NREKDEDR_Update"
                         OnCancelCommand="NREKDEDR_Cancel"  CssClass="font9Tahoma"
                         PageSize="6" Width="100%">
                        <AlternatingItemStyle CssClass="mr-r" />
                        <ItemStyle CssClass="mr-l" />
                        <HeaderStyle CssClass="mr-h" />
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
                            <asp:TemplateColumn HeaderText="ID" ItemStyle-Width=3%>
                                <ItemTemplate>
                                    <%#Container.DataItem("NRekID")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="nrekid" CssClass="font9Tahoma" runat="server"  Enabled=false Text='<%# trim(Container.DataItem("NRekID")) %>'  Width="95%"></asp:TextBox>
                                 </EditItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Periode Awal" ItemStyle-Width=7%>
                                <ItemTemplate>
                                    <%#Container.DataItem("PeriodeAwl")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="nrekpawal" CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("PeriodeAwl")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Periode Akhir" ItemStyle-Width=7%>
                                <ItemTemplate>
                                    <%#Container.DataItem("PeriodeAhr")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="nrekpakhir" CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("PeriodeAhr")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Bank" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# Container.DataItem("Bank") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:DropDownList id="nrekddlbank" width=100% runat=server />
								  <asp:Label ID="nreklblbank" runat="server" Text='<%# trim(Container.DataItem("Bank")) %>' visible=False Width="100%"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="No.Rek" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%#Container.DataItem("NRek")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="nrekno" CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("NRek")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Nama" ItemStyle-Width=15%>
                                <ItemTemplate>
                                    <%#Container.DataItem("NRekNama")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="nreknama" CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("NRekNama")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Tgl Update" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox id="nrekStatus" Visible=False Text='<%# Container.DataItem("Status")%>' runat="server" CssClass="font9Tahoma"/>
                                    <asp:TextBox ID="nrekUpdateDate" CssClass="font9Tahoma" runat="server" ReadOnly="TRUE" size="8" Text='<%# objGlobal.GetLongDate(Now()) %>'
                                        Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="nrekCreateDate" CssClass="font9Tahoma" runat="server" Text='<%# Container.DataItem("CreateDate") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="diupdated " ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# Container.DataItem("UserName") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn ItemStyle-Width=12%>
                                <ItemTemplate >
                                    <asp:LinkButton ID="Edit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="Update" runat="server" CommandName="Update" Text="Save"></asp:LinkButton>
                                    <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                    <asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"  Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
				</tr>
                     <tr>
                         <td style="height: 25px" width="20"></td>
                         <td style="height: 25px" width="14"></td>
                         <td style="height: 25px"><asp:ImageButton ID="btnAddNRek" OnClick="btnAddNRek_onClick" runat="server" AlternateText="New No.Rek" ImageUrl="../../images/butt_add.gif" /></td>
                         <td rowspan="1" style="width: 295px; height: 25px"></td>
                         <td style="width: 17px; height: 25px"></td>
                         <td style="width: 120px; height: 25px"></td>
                         <td style="width: 157px; height: 25px"></td>
                     </tr>
		        
		        <tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
                </table>
      
             <table id="Table5B" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="../../images/spacer.gif" width="5" border="0"></td>
							</tr>
			</table>

<%--Tab Alamat--%>

   		    <table id="TabHeadAlamat_Estate" cellSpacing="0" cellPadding="0" width="99%" runat="server" class="font9Tahoma">
				<tr height="20">
					<td width="20"></td>
					<td width="14"><IMG src="../../images/arow.gif" border="0" align="left"></td>
					<td class="lb-hti"><A class="mb-t" href="javascript:togglebox(TabAlamat_Estate);javascript:togglebox1(TabFamily_Estate);javascript:togglebox1(TabRPekerjaan_Estate);javascript:togglebox1(TabRPayroll_Estate);javascript:togglebox1(TabRPendidikan_Estate);javascript:togglebox1(TabRKursus_Estate);javascript:togglebox1(TabRPromosi_Estate);javascript:togglebox1(TabRKesehatan_Estate);javascript:togglebox1(TabTg_Estate);">Alamat</A></td>
				</tr>
			 </table>
			 
			 <%--style="VISIBILITY: hidden; POSITION: absolute" --%>
			 	<table id="TabAlamat_Estate" style="VISIBILITY: visible; POSITION: relative" cellSpacing="1" cellPadding="0" width="99%" border="0" runat="server">
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
				<tr>	
				<td width="20"></td>
				<td width="14"></td>
				<td colspan="5">
                    <asp:DataGrid ID="dataaddress" 
                         runat="server" 
                         AutoGenerateColumns="False" 
                         GridLines = none
                         CellPadding="2"
                         OnEditCommand="ADRDEDR_Edit"
                         OnDeleteCommand="ADRDEDR_Delete"
                         OnUpdateCommand="ADRDEDR_Update"
                         OnCancelCommand="ADRDEDR_Cancel" CssClass="font9Tahoma"
                         PageSize="6" Width="100%">
                        <AlternatingItemStyle CssClass="mr-r" />
                        <ItemStyle CssClass="mr-l" />
                        <HeaderStyle CssClass="mr-h" />
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
                            <asp:TemplateColumn HeaderText="ID" ItemStyle-Width=3%>
                                <ItemTemplate>
                                    <%#Container.DataItem("AddID")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="adrid"  CssClass="font9Tahoma" runat="server" MaxLength="8" Enabled=false Text='<%# trim(Container.DataItem("AddID")) %>'
                                        Width="95%"></asp:TextBox>
                                 </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Alamat" ItemStyle-Width=25%>
                                <ItemTemplate>
                                    <%#Container.DataItem("Address")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="adralamat" CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("Address")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Kota" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%#Container.DataItem("Kota")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="adrkota" CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("Kota")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
							<asp:TemplateColumn HeaderText="Telp" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%#Container.DataItem("phone")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="adrtelp" CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("phone")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Tgl Update" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox id="Status" Visible=False Text='<%# Container.DataItem("Status")%>' runat="server" CssClass="font9Tahoma"/>
                                    <asp:TextBox ID="UpdateDate"  CssClass="font9Tahoma" runat="server" ReadOnly="TRUE" size="8" Text='<%# objGlobal.GetLongDate(Now()) %>'
                                        Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="CreateDate" CssClass="font9Tahoma" runat="server" Text='<%# Container.DataItem("CreateDate") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="diupdated " ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# Container.DataItem("UserName") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn ItemStyle-Width=12%>
                                <ItemTemplate >
                                    <asp:LinkButton ID="Edit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="Update" runat="server" CommandName="Update" Text="Save"></asp:LinkButton>
                                    <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                    <asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"  Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
				</tr>
                     <tr>
                         <td style="height: 25px" width="20"></td>
                         <td style="height: 25px" width="14"></td>
                         <td style="height: 25px"><asp:ImageButton ID="ImageButton5" OnClick="btnAddAdr_onClick" runat="server" AlternateText="New Family" ImageUrl="../../images/butt_add.gif" /></td>
                         <td rowspan="1" style="width: 295px; height: 25px"></td>
                         <td style="width: 17px; height: 25px"></td>
                         <td style="width: 120px; height: 25px"></td>
                         <td style="width: 157px; height: 25px"></td>
                     </tr>
		        
		        <tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
                </table>
      
             <table id="Table5" cellSpacing="0" cellPadding="0" width="100%" runat="server" class="font9Tahoma">
							<tr>
								<td colSpan="2"><IMG height="0" src="../../images/spacer.gif" width="5" border="0"></td>
							</tr>
			</table>

<%--Tab Family--%>

   		    <table id="TabHeadFamily_Estate" cellSpacing="0" cellPadding="0" width="99%" runat="server" class="font9Tahoma">
				<tr height="20">
					<td width="20"></td>
					<td width="14"><IMG src="../../images/arow.gif" border="0" align="left"></td>
					<td class="lb-hti"><A class="mb-t" href="javascript:togglebox1(TabAlamat_Estate);javascript:togglebox(TabFamily_Estate);javascript:togglebox1(TabRPekerjaan_Estate);javascript:togglebox1(TabRPayroll_Estate);javascript:togglebox1(TabRPendidikan_Estate);javascript:togglebox1(TabRKursus_Estate);javascript:togglebox1(TabRPromosi_Estate);javascript:togglebox1(TabRKesehatan_Estate);togglebox1(TabTg_Estate);">Keluarga</A></td>
				</tr>
			 </table>
			 <%--style="VISIBILITY: visible; POSITION: relative" --%>
			 
			 	<table id="TabFamily_Estate" style="VISIBILITY: visible; POSITION: relative" cellSpacing="1" cellPadding="0" width="99%" border="0" runat="server">
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
				<tr>	
				<td width="20"></td>
				<td width="14"></td>
				<td colspan="5">
                    <asp:DataGrid ID="datafamily" 
                         runat="server" 
                         AutoGenerateColumns="False" 
                         GridLines = none
                         CellPadding="2"
                         OnEditCommand="FMDEDR_Edit"
                         OnDeleteCommand="FMDEDR_Delete"
                         OnUpdateCommand="FMDEDR_Update"
                         OnCancelCommand="FMDEDR_Cancel"  CssClass="font9Tahoma"
                         PageSize="6" Width="100%">
                        <AlternatingItemStyle CssClass="mr-r" />
                        <ItemStyle CssClass="mr-l" />
                        <HeaderStyle CssClass="mr-h" />


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
                            <asp:TemplateColumn HeaderText="ID" ItemStyle-Width=3%>
                                <ItemTemplate>
                                    <%#Container.DataItem("FamMemberID")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="fmid" CssClass="font9Tahoma" runat="server" MaxLength="8" Enabled=false Text='<%# trim(Container.DataItem("FamMemberID")) %>'
                                        Width="95%"></asp:TextBox>
                                 </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Nama" ItemStyle-Width=17%>
                                <ItemTemplate>
                                    <%#Container.DataItem("FamName")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="fmname" CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("FamName")) %>' Width="100%"></asp:TextBox>
                                    <asp:Label id=lblErrfmname visible=false text ="Please input Name" forecolor=red runat=server/>	
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Tmp.Lahir" ItemStyle-Width=7%>
                                <ItemTemplate>
                                    <%#Container.DataItem("lahir")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="fmlahir" CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("lahir")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Tgl.Lahir" ItemStyle-Width=9%>
                                <ItemTemplate>
                                    <%# objGlobal.GetLongDate(Container.DataItem("DOB")) %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="fmtgllahir" CssClass="font9Tahoma" runat="server" Text='<%# Date_Validation(Container.DataItem("DOB"), True) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Gender" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%#GetGender(Container.DataItem("Gender"))%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:DropDownList id="fmddlgender" width=100% runat=server />
                                  <asp:Label id=lblErrfmddlgender visible=false text ="Please select Gender" forecolor=red runat=server/>	
                                  <asp:label id=lblddlgender visible=false text='<%# Container.DataItem("Gender") %>' runat=server/>						
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Hubungan" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# GetRelation(Container.DataItem("Relationship")) %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:DropDownList id="fmddlrelation" width=100% runat=server />
                                  <asp:Label id=lblErrddlrelation visible=false text ="Please select Relation" forecolor=red runat=server/>	
                                  <asp:label id=lblddlrelation visible=false text='<%# Container.DataItem("Relationship") %>' runat=server/>		
                                </EditItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Pendidikan" ItemStyle-Width=12%>
                                <ItemTemplate>
                                    <%# Container.DataItem("Pendidikan") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="fmpendidikan" CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("Pendidikan")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
							<asp:TemplateColumn HeaderText="Pekerjaan" ItemStyle-Width=12%>
                                <ItemTemplate>
                                    <%# Container.DataItem("Pekerjaan") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="fmpekerjaan" CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("Pekerjaan")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Tgl Update" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox id="Status"  Visible=False Text='<%# Container.DataItem("Status")%>' runat="server" CssClass="font9Tahoma"/>
                                    <asp:TextBox ID="UpdateDate" CssClass="font9Tahoma" runat="server" ReadOnly="TRUE" size="8" Text='<%# objGlobal.GetLongDate(Now()) %>'
                                        Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="CreateDate" CssClass="font9Tahoma" runat="server" Text='<%# Container.DataItem("CreateDate") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="diupdated " ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# Container.DataItem("UserName") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="UserName" CssClass="font9Tahoma" runat="server" ReadOnly="TRUE" size="8" Text='<%# Session("SS_USERID") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-Width=12%>
                                <ItemTemplate >
                                    <asp:LinkButton ID="Edit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="Update" runat="server" CommandName="Update" Text="Save"></asp:LinkButton>
                                    <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                    <asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"  Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
				</tr>
                     <tr>
                         <td style="height: 25px" width="20"></td>
                         <td style="height: 25px" width="14"></td>
                         <td style="height: 25px"><asp:ImageButton ID="btnAddfm" OnClick="btnAddfm_onClick" runat="server" AlternateText="New Family" ImageUrl="../../images/butt_add.gif" /></td>
                         <td rowspan="1" style="width: 295px; height: 25px"></td>
                         <td style="width: 17px; height: 25px"></td>
                         <td style="width: 120px; height: 25px"></td>
                         <td style="width: 157px; height: 25px"></td>
                     </tr>
		        
		        <tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
				
                </table>
      
             <table id="Table3" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="../../images/spacer.gif" width="5" border="0"></td>
							</tr>
			</table>

<%--Tab TANGGUNGAN--%>

   		    <table id="TabHeadTg_Estate" cellSpacing="0" cellPadding="0" width="99%" runat="server" class="font9Tahoma">
				<tr height="20">
					<td width="20"></td>
					<td width="14"><IMG src="../../images/arow.gif" border="0" align="left"></td>
					<td class="lb-hti"><A class="mb-t" href="javascript:togglebox1(TabAlamat_Estate);javascript:togglebox1(TabFamily_Estate);javascript:togglebox1(TabRPekerjaan_Estate);javascript:togglebox1(TabRPayroll_Estate);javascript:togglebox1(TabRPendidikan_Estate);javascript:togglebox1(TabRKursus_Estate);javascript:togglebox1(TabRPromosi_Estate);javascript:togglebox1(TabRKesehatan_Estate);togglebox(TabTg_Estate);">Riwayat Tanggungan</A></td>
				</tr>
			 </table>
			 <%--style="VISIBILITY: visible; POSITION: relative" --%>
			 
			 	<table id="TabTg_Estate" style="VISIBILITY: visible; POSITION: relative" cellSpacing="1" cellPadding="0" width="99%" border="0" runat="server">
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
				<tr>	
				<td width="20"></td>
				<td width="14"></td>
				<td colspan="5">
                    <asp:DataGrid ID="datatg" 
                         runat="server" 
                         AutoGenerateColumns="False" 
                         GridLines = none
                         CellPadding="2"
                         OnEditCommand="TGDEDR_Edit"
                         OnDeleteCommand="TGDEDR_Delete"
                         OnUpdateCommand="TGDEDR_Update"
                         OnCancelCommand="TGDEDR_Cancel"  CssClass="font9Tahoma"
                         PageSize="6" Width="100%">
                        <AlternatingItemStyle CssClass="mr-r" />
                        <ItemStyle CssClass="mr-l" />
                        <HeaderStyle CssClass="mr-h" />
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
                            <asp:TemplateColumn HeaderText="ID" ItemStyle-Width=3%>
                                <ItemTemplate>
                                    <%#Container.DataItem("ETgID")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tgid" CssClass="font9Tahoma" runat="server" MaxLength="20" Enabled=false Text='<%# trim(Container.DataItem("ETgID")) %>'
                                        Width="95%"></asp:TextBox>
                                 </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Periode Awal" ItemStyle-Width=7%>
                                <ItemTemplate>
                                    <%#Container.DataItem("PeriodeAwl")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tgpawal" CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("PeriodeAwl")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Periode Akhir" ItemStyle-Width=7%>
                                <ItemTemplate>
                                    <%#Container.DataItem("PeriodeAhr")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tgpakhir" CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("PeriodeAhr")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Tanggungan Pajak" ItemStyle-Width=9%>
                                <ItemTemplate>
                                    <%# Container.DataItem("CodeTg")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:DropDownList id="ddltg" width=100% runat=server />
                                  <asp:label id=lblddltg visible=false text='<%# Container.DataItem("CodeTg") %>' runat=server/>						
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Tanggungan Kesehatan" ItemStyle-Width=9%>
                                <ItemTemplate>
                                    <%#Container.DataItem("CodeTgHlt")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:DropDownList id="ddltghlt" width=100% runat=server />
                                  <asp:label id=lblddltghlt visible=false text='<%# Container.DataItem("CodeTgHlt") %>' runat=server/>						
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
							 <asp:TemplateColumn HeaderText="Tanggungan HRD/Beras" ItemStyle-Width=9%>
                                <ItemTemplate>
                                    <%#Container.DataItem("CodeTgHrd")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:DropDownList id="ddltghrd" width=100% runat=server />
                                  <asp:label id=lblddltghrd visible=false text='<%# Container.DataItem("CodeTgHrd") %>' runat=server/>						
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                           
                            <asp:TemplateColumn HeaderText="Tgl Update" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox id="Status" Visible=False Text='<%# Container.DataItem("Status")%>' runat="server" CssClass="font9Tahoma"/>
                                    <asp:TextBox ID="UpdateDate" CssClass="font9Tahoma" runat="server" ReadOnly="TRUE" size="8" Text='<%# objGlobal.GetLongDate(Now()) %>'
                                        Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="CreateDate" CssClass="font9Tahoma" runat="server" Text='<%# Container.DataItem("CreateDate") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="diupdated " ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# Container.DataItem("UserName") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="UserName" CssClass="font9Tahoma" runat="server" ReadOnly="TRUE" size="8" Text='<%# Session("SS_USERID") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-Width=12%>
                                <ItemTemplate >
                                    <asp:LinkButton ID="Edit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="Update" runat="server" CommandName="Update" Text="Save"></asp:LinkButton>
                                    <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                    <asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"  Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
				</tr>
                     <tr>
                         <td style="height: 25px" width="20"></td>
                         <td style="height: 25px" width="14"></td>
                         <td style="height: 25px"><asp:ImageButton ID="btnAddtg" OnClick="btnAddtg_onClick" runat="server" AlternateText="New Tanggungan" ImageUrl="../../images/butt_add.gif" /></td>
                         <td rowspan="1" style="width: 295px; height: 25px"></td>
                         <td style="width: 17px; height: 25px"></td>
                         <td style="width: 120px; height: 25px"></td>
                         <td style="width: 157px; height: 25px"></td>
                     </tr>
		        
		        <tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
				
                </table>
      
             <table id="Table6" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="../../images/spacer.gif" width="5" border="0"></td>
							</tr>
			</table>
			
			
<%--Tab Riwayat Pekerjaan--%>
	
  		    <table id="TabHeadRPekerjaan_Estate" cellSpacing="0" cellPadding="0" width="99%" runat="server" class="font9Tahoma">
				<tr height="20">
					<td width="20"></td>
					<td width="14"><IMG src="../../images/arow.gif" border="0" align="left"></td>
					<td class="lb-hti"><A class="mb-t" href="javascript:togglebox1(TabAlamat_Estate);javascript:togglebox1(TabFamily_Estate);javascript:togglebox(TabRPekerjaan_Estate);javascript:togglebox1(TabRPayroll_Estate);javascript:togglebox1(TabRPendidikan_Estate);javascript:togglebox1(TabRKursus_Estate);javascript:togglebox1(TabRPromosi_Estate);javascript:togglebox1(TabRKesehatan_Estate);togglebox1(TabTg_Estate);">Riwayat Pekerjaan</A></td>
				</tr>
			 </table>
			 
			 <%--"VISIBILITY: hidden; POSITION: absolute"--%>
			 	<table id="TabRPekerjaan_Estate" style="VISIBILITY: visible; POSITION: relative"  cellSpacing="0" cellPadding="0" width="99%" border="0" runat="server">
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
				<tr>	
				    <td width="20"></td>
				    <td width="14"></td>
				    <td colspan="5">
				    <asp:DataGrid ID="datapekerjaan" 
                         runat="server" 
                         AutoGenerateColumns="False" 
                         GridLines = none
                         CellPadding="2"
                         OnEditCommand="WKDEDR_Edit"
                         OnDeleteCommand="WKDEDR_Delete"
                         OnUpdateCommand="WKDEDR_Update"
                         OnCancelCommand="WKDEDR_Cancel" CssClass="font9Tahoma"
                         PageSize="6" Width="100%">
                        <AlternatingItemStyle CssClass="mr-r" />
                        <ItemStyle CssClass="mr-l" />
                        <HeaderStyle CssClass="mr-h" />
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
                            <asp:TemplateColumn HeaderText="ID" ItemStyle-Width=3%>
                                <ItemTemplate>
                                    <%#Container.DataItem("WorkHistID")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="wkid" CssClass="font9Tahoma" runat="server" MaxLength="8" Enabled=false Text='<%# trim(Container.DataItem("WorkHistID")) %>'
                                        Width="95%"></asp:TextBox>
                                 </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Nama Perusahaan" ItemStyle-Width=25%>
                                <ItemTemplate>
                                    <%#Container.DataItem("CompName")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="wkcompy" CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("CompName")) %>' Width="100%"></asp:TextBox>
                                    <asp:Label id=lblErrwkname visible=false text ="Please input Company" forecolor=red runat=server/>	
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Periode Awal" ItemStyle-Width=12%>
                                <ItemTemplate>
                                    <%#Container.DataItem("PeriodeAwl")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:TextBox ID="wkawal" maxlength=6  CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("PeriodeAwl")) %>' onkeypress="javascript:return isNumberKey(event)" Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Periode Akhir" ItemStyle-Width=12%>
                                <ItemTemplate>
                                    <%# Container.DataItem("PeriodeAhr") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:TextBox ID="wkakhir" maxlength=6 CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("PeriodeAhr")) %>' onkeypress="javascript:return isNumberKey(event)" Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                                                    
                            <asp:TemplateColumn HeaderText="Jabatan" ItemStyle-Width=12%>
                                <ItemTemplate>
                                    <%# Container.DataItem("Jabatan") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:TextBox ID="wkjabatn" CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("Jabatan")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Divisi" ItemStyle-Width=12%>
                                <ItemTemplate>
                                    <%# Container.DataItem("CodeDiv") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:DropDownList ID="wkdivisi" runat="server"  Width="100%"/>
								  <asp:label ID="lbldivisi" runat="server" Text='<%# trim(Container.DataItem("CodeDiv")) %>' visible=false Width="100%" />
					            </EditItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="CodeJabatan" ItemStyle-Width=12%>
                                <ItemTemplate>
                                    <%# Container.DataItem("CodeJabatan") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:DropDownList ID="wkjbt" runat="server"  Width="100%"/>
								  <asp:label ID="lbljbt" runat="server" Text='<%# trim(Container.DataItem("CodeJabatan")) %>' visible=false Width="100%"/>
								  
                                </EditItemTemplate>
                            </asp:TemplateColumn>
							
                            
                            <asp:TemplateColumn HeaderText="Tgl Update" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox id="wkStatus" Visible=False Text='<%# Container.DataItem("Status")%>' runat="server" CssClass="font9Tahoma"/>
                                    <asp:TextBox ID="wkUpdateDate" runat="server" ReadOnly="TRUE" size="8" Text='<%# objGlobal.GetLongDate(Now()) %>'
                                        Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="kCreateDate" CssClass="font9Tahoma" runat="server" Text='<%# Container.DataItem("CreateDate") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="diupdate" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# Container.DataItem("UserName") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="wkUserName" CssClass="font9Tahoma" runat="server" ReadOnly="TRUE" size="8" Text='<%# Session("SS_USERID") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
						
                            <asp:TemplateColumn ItemStyle-Width=12%>
                                <ItemTemplate >
                                    <asp:LinkButton ID="Edit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="Update" runat="server" CommandName="Update" Text="Save"></asp:LinkButton>
                                    <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                    <asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"  Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateColumn>

							
                        </Columns>
                    </asp:DataGrid>
				    </td>
				</tr>
                    
                 <tr>
                    <td style="height: 25px" width="20"> </td>
                         <td style="height: 25px" width="14"></td>
                         <td style="height: 25px"><asp:ImageButton ID="btnAddwk" OnClick="btnAddwk_OnClick" runat="server" AlternateText="New Family" ImageUrl="../../images/butt_add.gif" /></td>
                         <td rowspan="1" style="width: 295px; height: 25px"></td>
                         <td style="width: 17px; height: 25px"></td>
                         <td style="width: 120px; height: 25px"></td>
                         <td style="width: 157px; height: 25px"></td>
                     </tr>
		
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
                </table>
      
             <table id="Table14" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td colSpan="2"><IMG height="0" src="../../images/spacer.gif" width="5" border="0"></td>
				</tr>
			</table>
			
<%--Tab Riwayat Promosi/Demosi--%>
	
  		    <table id="TabHeadRPromosi_Estate" cellSpacing="0" cellPadding="0" width="99%" runat="server" class="font9Tahoma">
				<tr height="20">
					<td width="20"></td>
					<td width="14"><IMG src="../../images/arow.gif" border="0" align="left"></td>
					<td class="lb-hti"><A class="mb-t" href="javascript:togglebox1(TabAlamat_Estate);javascript:togglebox1(TabFamily_Estate);javascript:togglebox1(TabRPekerjaan_Estate);javascript:togglebox1(TabRPayroll_Estate);javascript:togglebox1(TabRPendidikan_Estate);javascript:togglebox1(TabRKursus_Estate);javascript:togglebox(TabRPromosi_Estate);javascript:togglebox1(TabRKesehatan_Estate);togglebox1(TabTg_Estate);">Riwayat Promosi/Demosi</A></td>
				</tr>
			 </table>
			 
			 <%--"VISIBILITY: hidden; POSITION: absolute"--%>
			 	<table id="TabRPromosi_Estate" style="VISIBILITY: visible; POSITION: relative"  cellSpacing="0" cellPadding="0" width="99%" border="0" runat="server">
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
				<tr>	
				    <td width="20"></td>
				    <td width="14"></td>
				    <td colspan="5">
				    <asp:DataGrid ID="datapdmsi" 
                         runat="server" 
                         AutoGenerateColumns="False" 
                         GridLines = none
                         CellPadding="2" CssClass="font9Tahoma"
						  PageSize="6" Width="100%">
                        <AlternatingItemStyle CssClass="mr-r" />
                        <ItemStyle CssClass="mr-l" />
                        <HeaderStyle CssClass="mr-h" />
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
                            <asp:TemplateColumn HeaderText="ID" ItemStyle-Width=3%>
                                <ItemTemplate>
									<asp:LinkButton id=lnkpdID CommandArgument='<%# Container.DataItem("ProDeID") %>'  Text='<%# Container.DataItem("ProDeID") %>'  runat=server />
									<asp:Label ID="lblPdID" Visible=false runat="server" Text='<%#Container.DataItem("ProDeID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
							<asp:TemplateColumn HeaderText="Type" ItemStyle-Width=8%>
                                <ItemTemplate>
                                    <%#Container.DataItem("TypePro")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
							
											                                                    
                            <asp:TemplateColumn HeaderText="Tipe Kry Lama" ItemStyle-Width=9%>
                                <ItemTemplate>
                                    <%#Container.DataItem("EmpTyAwl")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
							<asp:TemplateColumn HeaderText="Jabatan Lama" ItemStyle-Width=15%>
                                <ItemTemplate>
                                    <%#Container.DataItem("JabatanA")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Tipe Kry Baru" ItemStyle-Width=9%>
                                <ItemTemplate>
                                    <%#Container.DataItem("EmptyBru")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:DropDownList ID="pdmsitempbru" runat="server" Width="100%" />
								  <asp:label id=lblpdmsitempbru visible=false text='<%# Container.DataItem("EmptyBru") %>' runat=server/>	
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
							<asp:TemplateColumn HeaderText="Jabatan Baru" ItemStyle-Width=15%>
                                <ItemTemplate>
                                    <%#Container.DataItem("Jabatan")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:TextBox ID="pdmsitjobbru" maxlength=100 CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("JabatanBru")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Tgl.Efektif" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# objGlobal.GetLongDate(Container.DataItem("EfektifDate")) %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:TextBox ID="pdmsiedate" maxlength=10 CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("EfektifDate")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                                                       
							 <asp:TemplateColumn HeaderText="Doc.No" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# Container.DataItem("Docno") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:TextBox ID="pdmsidno" maxlength=100  CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("Docno")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                                                    
                           
                            <asp:TemplateColumn HeaderText="Tgl Update" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox id="pdmsiStatus" CssClass="font9Tahoma" Visible=False Text='<%# Container.DataItem("Status")%>' runat="server"/>
                                    <asp:TextBox ID="pdmsiUpdateDate" CssClass="font9Tahoma" runat="server" ReadOnly="TRUE" size="8" Text='<%# objGlobal.GetLongDate(Now()) %>'
                                        Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="pdmsiCreateDate" CssClass="font9Tahoma" runat="server" Text='<%# Container.DataItem("CreateDate") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="diupdate" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# Container.DataItem("UName") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="pdmsiUserName" CssClass="font9Tahoma" runat="server" ReadOnly="TRUE" size="8" Text='<%# Session("SS_USERID") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                         
							</Columns>
                    </asp:DataGrid>
				    </td>
				</tr>
                    
           	
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
                </table>
      
             <table id="Table9" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td colSpan="2"><IMG height="0" src="../../images/spacer.gif" width="5" border="0"></td>
				</tr>
			</table>	

<%--Tab Riwayat Gaji--%>
	
  		    <table id="TabHeadRPayroll_Estate" cellSpacing="0" cellPadding="0" width="99%" runat="server" class="font9Tahoma">
				<tr height="20">
					<td width="20"></td>
					<td width="14"><IMG src="../../images/arow.gif" border="0" align="left"></td>
					<td class="lb-hti"><A class="mb-t" href="javascript:togglebox1(TabAlamat_Estate);javascript:togglebox1(TabFamily_Estate);javascript:togglebox1(TabRPekerjaan_Estate);javascript:togglebox(TabRPayroll_Estate);javascript:togglebox1(TabRPendidikan_Estate);javascript:togglebox1(TabRKursus_Estate);javascript:togglebox1(TabRPromosi_Estate);javascript:togglebox1(TabRKesehatan_Estate);togglebox1(TabTg_Estate);">Riwayat Gaji</A></td>
				</tr>
			 </table>
			 
			 <%--"VISIBILITY: hidden; POSITION: absolute"--%>
			 	<table id="TabRPayroll_Estate" style="VISIBILITY: visible; POSITION: relative"  cellSpacing="0" cellPadding="0" width="99%" border="0" runat="server">
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
				<tr>	
				    <td width="20"></td>
				    <td width="14"></td>
				    <td colspan="5">
				    <asp:DataGrid ID="datagaji" 
                         runat="server" 
                         AutoGenerateColumns="False" 
                         GridLines = none
                         CellPadding="2"
						 OnItemCommand=datagaji_OnCommand  CssClass="font9Tahoma"
					     Width="100%">
                        <AlternatingItemStyle CssClass="mr-r" />
                        <ItemStyle CssClass="mr-l" />
                        <HeaderStyle CssClass="mr-h" />
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
                                <asp:TemplateColumn HeaderText="ID" ItemStyle-Width=12%>
                                <ItemTemplate>
									<asp:LinkButton id=lnkPayHistID CommandArgument='<%# Container.DataItem("PayHistID") %>'  Text='<%# Container.DataItem("PayHistID") %>'  runat=server />
									<asp:Label ID="lblPayHistID" Visible=false runat="server" Text='<%#Container.DataItem("PayHistID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                                                      
                            <asp:TemplateColumn HeaderText="Periode Awal" ItemStyle-Width=7%>
                                <ItemTemplate>
                                    <%#Container.DataItem("PeriodeAwl")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Periode Akhir" ItemStyle-Width=7%>
                                <ItemTemplate>
                                    <%# Container.DataItem("PeriodeAhr") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                                                    
                            <asp:TemplateColumn HeaderText="Gaji Pokok" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# Container.DataItem("BasicSalary") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Premi Tetap" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# Container.DataItem("PremiSalary") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
							
									
							<asp:TemplateColumn HeaderText="Upah/Hari" ItemStyle-Width=7%>
                                <ItemTemplate>
                                    <%# Container.DataItem("Upah") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
							<asp:TemplateColumn HeaderText="Pinjaman" ItemStyle-Width=8%>
                                <ItemTemplate>
                                    <%# Container.DataItem("SmallSalary") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Beras" ItemStyle-Width=7%>
                                <ItemTemplate>
                                    <%# Container.DataItem("BerasRate") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Lembur" ItemStyle-Width=7%>
                                <ItemTemplate>
                                    <%# Container.DataItem("OvrTmRate") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Astek" ItemStyle-Width=5%>
                                <ItemTemplate>
                                    <%# Container.DataItem("isAstek") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Beras" ItemStyle-Width=5%>
                                <ItemTemplate>
                                    <%# Container.DataItem("isBeras") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="BPJS" ItemStyle-Width=5%>
                                <ItemTemplate>
                                    <%# Container.DataItem("isBPJS") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Pensiun" ItemStyle-Width=5%>
                                <ItemTemplate>
                                    <%# Container.DataItem("isJP") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
						
							
                            <asp:TemplateColumn HeaderText="Tgl Update" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="diupdate" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# Container.DataItem("UserName") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                        </Columns>
                    </asp:DataGrid>
				    </td>
				</tr>
                    
          	
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
                </table>
      
             <table id="Table7" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td colSpan="2"><IMG height="0" src="../../images/spacer.gif" width="5" border="0"></td>
				</tr>
			</table>
			
<%--Tab Riwayat kantor pelayanan Pajak--%>
	
  		    <table id="TabHeadRKpp_Estate" cellSpacing="0" cellPadding="0" width="99%" runat="server" class="font9Tahoma">
				<tr height="20">
					<td width="20"></td>
					<td width="14"><IMG src="../../images/arow.gif" border="0" align="left"></td>
					<td class="lb-hti"><A class="mb-t" href="javascript:togglebox1(TabAlamat_Estate);javascript:togglebox1(TabFamily_Estate);javascript:togglebox1(TabRPekerjaan_Estate);javascript:togglebox1(TabRPayroll_Estate);javascript:togglebox(TabRPendidikan_Estate);javascript:togglebox1(TabRKursus_Estate);javascript:togglebox1(TabRPromosi_Estate);javascript:togglebox1(TabRKesehatan_Estate);togglebox1(TabTg_Estate);">Riwayat Kantor Pelayanan Pajak</A></td>
				</tr>
			 </table>
			 
			 <%--"VISIBILITY: hidden; POSITION: absolute"--%>
			 	<table id="TabRKppr_Estate" style="VISIBILITY: visible; POSITION: relative"  cellSpacing="0" cellPadding="0" width="99%" border="0" runat="server" class="font9Tahoma">
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
				<tr>	
				    <td width="20"></td>
				    <td width="14"></td>
				    <td colspan="5">
				    <asp:DataGrid ID="dataKpp" 
                         runat="server" 
                         AutoGenerateColumns="False" 
                         GridLines = none
                         CellPadding="2"
						 OnEditCommand="KPPEDR_Edit"
                         OnDeleteCommand="KPPDEDR_Delete"
                         OnUpdateCommand="KPPDEDR_Update"
                         OnCancelCommand="KPPDEDR_Cancel"  CssClass="font9Tahoma"
                         PageSize="6" Width="100%">
                        <AlternatingItemStyle CssClass="mr-r" />
                        <ItemStyle CssClass="mr-l" />
                        <HeaderStyle CssClass="mr-h" />
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
                                 <asp:TemplateColumn HeaderText="ID" ItemStyle-Width=3%>
                                <ItemTemplate>
                                    <%#Container.DataItem("KppHistID")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="kppid" CssClass="font9Tahoma" runat="server" MaxLength="8" Enabled=false Text='<%# trim(Container.DataItem("KppHistID")) %>'
                                        Width="95%"></asp:TextBox>
                                 </EditItemTemplate>
                            </asp:TemplateColumn>
                            
							<asp:TemplateColumn HeaderText="Perusahaan" ItemStyle-Width=30%>
                                <ItemTemplate>
                                    <%#Container.DataItem("CompName")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:TextBox ID="kppcomp" maxlength=30 CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("CompName")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="KPP" ItemStyle-Width=30%>
                                <ItemTemplate>
                                    <%#Container.DataItem("KPPDescr")%>
                                </ItemTemplate>
                                <EditItemTemplate>
								  <asp:DropDownList ID="ddlkpp" width="100%" runat="server" >
                                  </asp:DropDownList>
								  <asp:label id=lblkpp visible=false text='<%# Container.DataItem("CodeKpp") %>' runat=server/>		
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                                                      
                            <asp:TemplateColumn HeaderText="Periode Awal" ItemStyle-Width=9%>
                                <ItemTemplate>
                                    <%#Container.DataItem("PeriodeAwl")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="kppawl" maxlength=6 CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("PeriodeAwl")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>

                            
                            <asp:TemplateColumn HeaderText="Periode Akhir" ItemStyle-Width=9%>
                                <ItemTemplate>
                                    <%# Container.DataItem("PeriodeAhr") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:TextBox ID="kppahr" maxlength=6 CssClass="font9Tahoma"  runat="server" Text='<%# trim(Container.DataItem("PeriodeAhr")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
							 
                            <asp:TemplateColumn HeaderText="Tgl Update" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox id="kppStatus" Visible=False Text='<%# Container.DataItem("Status")%>' CssClass="font9Tahoma" runat="server"/>
                                    <asp:TextBox ID="kppUpdateDate" CssClass="font9Tahoma" runat="server" ReadOnly="TRUE" size="8" Text='<%# objGlobal.GetLongDate(Now()) %>'
                                        Visible="False"  ></asp:TextBox>
                                    <asp:TextBox ID="kppCreateDate" CssClass="font9Tahoma" runat="server" Text='<%# Container.DataItem("CreateDate") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="diupdate" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# Container.DataItem("UserName") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="kppUserName" CssClass="font9Tahoma" runat="server" ReadOnly="TRUE" size="8" Text='<%# Session("SS_USERID") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-Width=12%>
                                <ItemTemplate >
                                    <asp:LinkButton ID="Edit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="Update" runat="server" CommandName="Update" Text="Save"></asp:LinkButton>
                                    <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                    <asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"  Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
							</Columns>
                    </asp:DataGrid>
				    </td>
				</tr>
                    
                 <tr>
                    <td style="height: 25px" width="20"> </td>
                         <td style="height: 25px" width="14"></td>
                         <td style="height: 25px"><asp:ImageButton ID="btnAddkpp"  OnClick="btnAddkpp_OnClick"  runat="server" AlternateText="New Letter" ImageUrl="../../images/butt_add.gif" /></td>
                         <td rowspan="1" style="width: 295px; height: 25px"></td>
                         <td style="width: 17px; height: 25px"></td>
                         <td style="width: 120px; height: 25px"></td>
                         <td style="width: 157px; height: 25px"></td>
                     </tr>
		
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
                </table>
      
             <table id="Table16" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td colSpan="2"><IMG height="0" src="../../images/spacer.gif" width="5" border="0"></td>
				</tr>
			</table>
			
<%--Tab Riwayat Surat Penghargaan & Peringatan--%>
	
  		    <table id="TabHeadRLetter_Estate" cellSpacing="0" cellPadding="0" width="99%" runat="server" class="font9Tahoma">
				<tr height="20">
					<td width="20"></td>
					<td width="14"><IMG src="../../images/arow.gif" border="0" align="left"></td>
					<td class="lb-hti"><A class="mb-t" href="javascript:togglebox1(TabAlamat_Estate);javascript:togglebox1(TabFamily_Estate);javascript:togglebox1(TabRPekerjaan_Estate);javascript:togglebox1(TabRPayroll_Estate);javascript:togglebox(TabRPendidikan_Estate);javascript:togglebox1(TabRKursus_Estate);javascript:togglebox1(TabRPromosi_Estate);javascript:togglebox1(TabRKesehatan_Estate);togglebox1(TabTg_Estate);">Riwayat Surat Penghargaan & Peringatan</A></td>
				</tr>
			 </table>
			 
			 <%--"VISIBILITY: hidden; POSITION: absolute"--%>
			 	<table id="TabRLetter_Estate" style="VISIBILITY: visible; POSITION: relative"  cellSpacing="0" cellPadding="0" width="99%" border="0" runat="server">
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
				<tr>	
				    <td width="20"></td>
				    <td width="14"></td>
				    <td colspan="5">
				    <asp:DataGrid ID="dataletter" 
                         runat="server" 
                         AutoGenerateColumns="False" 
                         GridLines = none
                         CellPadding="2"
						 OnEditCommand="LTREDR_Edit"
                         OnDeleteCommand="LTRDEDR_Delete"
                         OnUpdateCommand="LTRDEDR_Update"
                         OnCancelCommand="LTRDEDR_Cancel" CssClass="font9Tahoma"
                         PageSize="6" Width="100%">
                        <AlternatingItemStyle CssClass="mr-r" />
                        <ItemStyle CssClass="mr-l" />
                        <HeaderStyle CssClass="mr-h" />

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
                                 <asp:TemplateColumn HeaderText="ID" ItemStyle-Width=3%>
                                <ItemTemplate>
                                    <%#Container.DataItem("LetterCode")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="ltrid" CssClass="font9Tahoma" runat="server" MaxLength="8" Enabled=false Text='<%# trim(Container.DataItem("LetterCode")) %>'
                                        Width="95%"></asp:TextBox>
                                 </EditItemTemplate>
                            </asp:TemplateColumn>
                            
							<asp:TemplateColumn HeaderText="No.Surat" ItemStyle-Width=25%>
                                <ItemTemplate>
                                    <%#Container.DataItem("LetterNo")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:TextBox ID="ltrno" maxlength=30 CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("LetterNo")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Tipe" ItemStyle-Width=15%>
                                <ItemTemplate>
                                    <%#Container.DataItem("LetterType")%>
                                </ItemTemplate>
                                <EditItemTemplate>
								  <asp:DropDownList ID="ltrtingkat" width="100%" runat="server" >
                                  <asp:ListItem Value="SA" Text="Surat Penghargaan"></asp:ListItem>
                                  <asp:ListItem Value="SP" Text="Surat Peringatan"></asp:ListItem>
                                   </asp:DropDownList>
								  <asp:label id=lblltrtingkat visible=false text='<%# Container.DataItem("LetterType") %>' runat=server/>		
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                                                      
                            <asp:TemplateColumn HeaderText="Tgl.Surat" ItemStyle-Width=9%>
                                <ItemTemplate>
                                    <%# objGlobal.GetLongDate(Container.DataItem("LetterDate")) %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="ltrdate" CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("LetterDate")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>

                            
                            <asp:TemplateColumn HeaderText="Keterangan" ItemStyle-Width=30%>
                                <ItemTemplate>
                                    <%# Container.DataItem("Notes") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:TextBox ID="ltrnotes" maxlength=100  CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("Notes")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
							 
                            <asp:TemplateColumn HeaderText="Tgl Update" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox id="ltrStatus" Visible=False Text='<%# Container.DataItem("Status")%>' CssClass="font9Tahoma" runat="server"/>
                                    <asp:TextBox ID="ltrUpdateDate" CssClass="font9Tahoma" runat="server" ReadOnly="TRUE" size="8" Text='<%# objGlobal.GetLongDate(Now()) %>'
                                        Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="ltrCreateDate" CssClass="font9Tahoma" runat="server" Text='<%# Container.DataItem("CreateDate") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="diupdate" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# Container.DataItem("UserName") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="ltrUserName" CssClass="font9Tahoma" runat="server" ReadOnly="TRUE" size="8" Text='<%# Session("SS_USERID") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-Width=12%>
                                <ItemTemplate >
                                    <asp:LinkButton ID="Edit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="Update" runat="server" CommandName="Update" Text="Save"></asp:LinkButton>
                                    <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                    <asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"  Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
							</Columns>
                    </asp:DataGrid>
				    </td>
				</tr>
                    
                 <tr>
                    <td style="height: 25px" width="20"> </td>
                         <td style="height: 25px" width="14"></td>
                         <td style="height: 25px"><asp:ImageButton ID="btnAddltr"  OnClick="btnAddltr_OnClick"  runat="server" AlternateText="New Letter" ImageUrl="../../images/butt_add.gif" /></td>
                         <td rowspan="1" style="width: 295px; height: 25px"></td>
                         <td style="width: 17px; height: 25px"></td>
                         <td style="width: 120px; height: 25px"></td>
                         <td style="width: 157px; height: 25px"></td>
                     </tr>
		
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
                </table>
      
             <table id="Table15" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td colSpan="2"><IMG height="0" src="../../images/spacer.gif" width="5" border="0"></td>
				</tr>
			</table>
			
<%--Tab Riwayat Pendidikan--%>
	
  		    <table id="TabHeadRPendidikan_Estate" cellSpacing="0" cellPadding="0" width="99%" runat="server" class="font9Tahoma">
				<tr height="20">
					<td width="20"></td>
					<td width="14"><IMG src="../../images/arow.gif" border="0" align="left"></td>
					<td class="lb-hti"><A class="mb-t" href="javascript:togglebox1(TabAlamat_Estate);javascript:togglebox1(TabFamily_Estate);javascript:togglebox1(TabRPekerjaan_Estate);javascript:togglebox1(TabRPayroll_Estate);javascript:togglebox(TabRPendidikan_Estate);javascript:togglebox1(TabRKursus_Estate);javascript:togglebox1(TabRPromosi_Estate);javascript:togglebox1(TabRKesehatan_Estate);togglebox1(TabTg_Estate);">Riwayat Pendidikan</A></td>
				</tr>
			 </table>
			 
			 <%--"VISIBILITY: hidden; POSITION: absolute"--%>
			 	<table id="TabRPendidikan_Estate" style="VISIBILITY: visible; POSITION: relative"  cellSpacing="0" cellPadding="0" width="99%" border="0" runat="server">
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
				<tr>	
				    <td width="20"></td>
				    <td width="14"></td>
				    <td colspan="5">
				    <asp:DataGrid ID="datastudy" 
                         runat="server" 
                         AutoGenerateColumns="False" 
                         GridLines = none
                         CellPadding="2"
						 OnEditCommand="SDYEDR_Edit"
                         OnDeleteCommand="SDYDEDR_Delete"
                         OnUpdateCommand="SDYDEDR_Update"
                         OnCancelCommand="SDYDEDR_Cancel"
                         PageSize="6" Width="100%" CssClass="font9Tahoma">
                        <AlternatingItemStyle CssClass="mr-r" />
                        <ItemStyle CssClass="mr-l" />
                        <HeaderStyle CssClass="mr-h" />
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
                                 <asp:TemplateColumn HeaderText="ID" ItemStyle-Width=3%>
                                <ItemTemplate>
                                    <%#Container.DataItem("StudyHistID")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="sdyid" CssClass="font9Tahoma" runat="server" MaxLength="8" Enabled=false Text='<%# trim(Container.DataItem("StudyHistID")) %>'
                                        Width="95%"></asp:TextBox>
                                 </EditItemTemplate>
                            </asp:TemplateColumn>
                            
							<asp:TemplateColumn HeaderText="Tingkat" ItemStyle-Width=15%>
                                <ItemTemplate>
                                    <%#Container.DataItem("Tingkat")%>
                                </ItemTemplate>
                                <EditItemTemplate>
								  <asp:DropDownList ID="sdytingkat" width="100%" runat="server" >
                                  <asp:ListItem Value="" Text="-"></asp:ListItem>
                                  <asp:ListItem Value="SD" Text="SD/Ibtidaiyah/Paket A"></asp:ListItem>
                                  <asp:ListItem Value="SMP" Text="SMP/Tsanawiyah/Paket B"></asp:ListItem>
                                  <asp:ListItem Value="SMA" Text="SMA/SMK/STM/Aliyah/Paket C"></asp:ListItem>
								  <asp:ListItem Value="D1" Text="Diploma 1"></asp:ListItem>
								  <asp:ListItem Value="D2" Text="Diploma 2"></asp:ListItem>
								  <asp:ListItem Value="D3" Text="Diploma 3"></asp:ListItem>
								  <asp:ListItem Value="D4" Text="Diploma 4"></asp:ListItem>
								  <asp:ListItem Value="S1" Text="Strata 1"></asp:ListItem>
								  <asp:ListItem Value="S2" Text="Strata 2"></asp:ListItem>
								  <asp:ListItem Value="S3" Text="Strata 3"></asp:ListItem>
								  </asp:DropDownList>
								  <asp:label id=lblsdytingkat visible=false text='<%# Container.DataItem("Tingkat") %>' runat=server/>		
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                                                      
                            <asp:TemplateColumn HeaderText="Nama" ItemStyle-Width=25%>
                                <ItemTemplate>
                                    <%#Container.DataItem("NamaStudy")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:TextBox ID="sdynama" maxlength=100 runat="server" Text='<%# trim(Container.DataItem("NamaStudy")) %>' Width="100%" CssClass="font9Tahoma"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Tahun Masuk" ItemStyle-Width=8%>
                                <ItemTemplate>
                                    <%# Container.DataItem("TahunAwl") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:TextBox ID="sdytahunawl" maxlength=4 CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("TahunAwl")) %>' onkeypress="javascript:return isNumberKey(event)" Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
							 <asp:TemplateColumn HeaderText="Tahun Lulus" ItemStyle-Width=8%>
                                <ItemTemplate>
                                    <%# Container.DataItem("TahunAhr") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:TextBox ID="sdytahunahr" maxlength=4  runat="server" Text='<%# trim(Container.DataItem("TahunAhr")) %>' onkeypress="javascript:return isNumberKey(event)" Width="100%" CssClass="font9Tahoma"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                                                    
                           
                            <asp:TemplateColumn HeaderText="Tgl Update" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox id="sdyStatus" Visible=False Text='<%# Container.DataItem("Status")%>' runat="server" CssClass="font9Tahoma"/>
                                    <asp:TextBox ID="sdyUpdateDate" CssClass="font9Tahoma" runat="server" ReadOnly="TRUE" size="8" Text='<%# objGlobal.GetLongDate(Now()) %>'
                                        Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="sdyCreateDate" CssClass="font9Tahoma" runat="server" Text='<%# Container.DataItem("CreateDate") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="diupdate" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# Container.DataItem("UserName") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="sdyUserName" CssClass="font9Tahoma" runat="server" ReadOnly="TRUE" size="8" Text='<%# Session("SS_USERID") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-Width=12%>
                                <ItemTemplate >
                                    <asp:LinkButton ID="Edit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="Update" runat="server" CommandName="Update" Text="Save"></asp:LinkButton>
                                    <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                    <asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"  Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
							</Columns>
                    </asp:DataGrid>
				    </td>
				</tr>
                    
                 <tr>
                    <td style="height: 25px" width="20"> </td>
                         <td style="height: 25px" width="14"></td>
                         <td style="height: 25px"><asp:ImageButton ID="btnAddsdy"  OnClick="btnAddsdy_OnClick"  runat="server" AlternateText="New Family" ImageUrl="../../images/butt_add.gif" /></td>
                         <td rowspan="1" style="width: 295px; height: 25px"></td>
                         <td style="width: 17px; height: 25px"></td>
                         <td style="width: 120px; height: 25px"></td>
                         <td style="width: 157px; height: 25px"></td>
                     </tr>
		
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
                </table>
      
             <table id="Table10" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td colSpan="2"><IMG height="0" src="../../images/spacer.gif" width="5" border="0"></td>
				</tr>
			</table>	
			
<%--Tab Riwayat Workshop--%>
	
  		    <table id="TabHeadRKursus_Estate" cellSpacing="0" cellPadding="0" width="99%" runat="server" class="font9Tahoma">
				<tr height="20">
					<td width="20"></td>
					<td width="14"><IMG src="../../images/arow.gif" border="0" align="left"></td>
					<td class="lb-hti"><A class="mb-t" href="javascript:togglebox1(TabAlamat_Estate);javascript:togglebox1(TabFamily_Estate);javascript:togglebox1(TabRPekerjaan_Estate);javascript:togglebox1(TabRPayroll_Estate);javascript:togglebox1(TabRPendidikan_Estate);javascript:togglebox(TabRKursus_Estate);javascript:togglebox1(TabRPromosi_Estate);javascript:togglebox1(TabRKesehatan_Estate);togglebox1(TabTg_Estate);">Riwayat Kursus/Workshop</A></td>
				</tr>
			 </table>
			 
			 <%--"VISIBILITY: hidden; POSITION: absolute"--%>
			 	<table id="TabRKursus_Estate" style="VISIBILITY: visible; POSITION: relative"  cellSpacing="0" cellPadding="0" width="99%" border="0" runat="server">
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
				<tr>	
				    <td width="20"></td>
				    <td width="14"></td>
				    <td colspan="5">
				     <asp:DataGrid ID="datawshop" 
                         runat="server" 
                         AutoGenerateColumns="False" 
                         GridLines = none
                         CellPadding="2"
						 OnEditCommand="WSHPDEDR_Edit"
                         OnDeleteCommand="WSHPDEDR_Delete"
                         OnUpdateCommand="WSHPDEDR_Update"
                         OnCancelCommand="WSHPDEDR_Cancel"  CssClass="font9Tahoma"
                         PageSize="6" Width="100%">
                        <AlternatingItemStyle CssClass="mr-r" />
                        <ItemStyle CssClass="mr-l" />
                        <HeaderStyle CssClass="mr-h" />
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
                                 <asp:TemplateColumn HeaderText="ID" ItemStyle-Width=3%>
                                <ItemTemplate>
                                    <%#Container.DataItem("WShopHistID")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="wshpid" CssClass="font9Tahoma" runat="server" MaxLength="8" Enabled=false Text='<%# trim(Container.DataItem("WShopHistID")) %>'
                                        Width="95%"></asp:TextBox>
                                 </EditItemTemplate>
                            </asp:TemplateColumn>
                            
						                                                    
                            <asp:TemplateColumn HeaderText="Workshop/Kursus" ItemStyle-Width=25%>
                                <ItemTemplate>
                                    <%#Container.DataItem("NamaWshop")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:TextBox ID="wshpnama" maxlength=100 CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("NamaWshop")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
							<asp:TemplateColumn HeaderText="Lokasi" ItemStyle-Width=25%>
                                <ItemTemplate>
                                    <%#Container.DataItem("Daerah")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:TextBox ID="wshpdaerah" maxlength=100 CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("Daerah")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
							
							<asp:TemplateColumn HeaderText="Penyelenggara" ItemStyle-Width=25%>
                                <ItemTemplate>
                                    <%#Container.DataItem("Peyelenggara")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                  <asp:TextBox ID="wshppenyelenggara" maxlength=100 CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("Peyelenggara")) %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                                                       
							 <asp:TemplateColumn HeaderText="Tahun" ItemStyle-Width=8%>
                                <ItemTemplate>
                                    <%# Container.DataItem("Tahun") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                <asp:TextBox ID="wshptahun" maxlength=4 CssClass="font9Tahoma" runat="server" Text='<%# trim(Container.DataItem("Tahun")) %>' onkeypress="javascript:return isNumberKey(event)" Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                                                    
                           
                            <asp:TemplateColumn HeaderText="Tgl Update" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox id="wshpStatus" Visible=False Text='<%# Container.DataItem("Status")%>' CssClass="font9Tahoma" runat="server"/>
                                    <asp:TextBox ID="wshpUpdateDate" runat="server" ReadOnly="TRUE" size="8" Text='<%# objGlobal.GetLongDate(Now()) %>'
                                        Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="wshpCreateDate" CssClass="font9Tahoma" runat="server" Text='<%# Container.DataItem("CreateDate") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="diupdate" ItemStyle-Width=10%>
                                <ItemTemplate>
                                    <%# Container.DataItem("UserName") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="wshpUserName" CssClass="font9Tahoma" runat="server" ReadOnly="TRUE" size="8" Text='<%# Session("SS_USERID") %>'
                                        Visible="False"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-Width=12%>
                                <ItemTemplate >
                                    <asp:LinkButton ID="Edit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="Update" runat="server" CommandName="Update" Text="Save"></asp:LinkButton>
                                    <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                    <asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"  Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
							</Columns>
                    </asp:DataGrid>
				    </td>
				</tr>
                    
                 <tr>
                    <td style="height: 25px" width="20"> </td>
                         <td style="height: 25px" width="14"></td>
                         <td style="height: 25px"><asp:ImageButton ID="btnAddwshp"  OnClick="btnAddwshp_OnClick" runat="server" AlternateText="New Family" ImageUrl="../../images/butt_add.gif" /></td>
                         <td rowspan="1" style="width: 295px; height: 25px"></td>
                         <td style="width: 17px; height: 25px"></td>
                         <td style="width: 120px; height: 25px"></td>
                         <td style="width: 157px; height: 25px"></td>
                     </tr>
		
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
                </table>
      
             <table id="Table8" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td colSpan="2"><IMG height="0" src="../../images/spacer.gif" width="5" border="0"></td>
				</tr>
			</table>		

	
			
<%--Tab Riwayat Kesehatan--%>
	
  		    <table id="TabHeadRKesehatan_Estate" cellSpacing="0" cellPadding="0" width="99%" runat="server" class="font9Tahoma">
				<tr height="20">
					<td width="20"></td>
					<td width="14"><IMG src="../../images/arow.gif" border="0" align="left"></td>
					<td class="lb-hti"><A class="mb-t" href="javascript:togglebox1(TabAlamat_Estate);javascript:togglebox1(TabFamily_Estate);javascript:togglebox1(TabRPekerjaan_Estate);javascript:togglebox1(TabRPayroll_Estate);javascript:togglebox1(TabRPendidikan_Estate);javascript:togglebox1(TabRKursus_Estate);javascript:togglebox1(TabRPromosi_Estate);javascript:togglebox(TabRKesehatan_Estate);togglebox1(TabTg_Estate);">Riwayat Kesehatan</A></td>
				</tr>
			 </table>
			 
			 <%--"VISIBILITY: hidden; POSITION: absolute"--%>
			 	<table id="TabRKesehatan_Estate" style="VISIBILITY: visible; POSITION: relative"  cellSpacing="0" cellPadding="0" width="99%" border="0" runat="server">
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
				<tr>	
				    <td width="20"></td>
				    <td width="14"></td>
				    <td colspan="5">
				    <asp:TextBox id="txt_medrec" Width=100% TextMode=MultiLine Height=100 runat=server CssClass="font9Tahoma">
				    </asp:TextBox>
                    </td>
				</tr>
				
                    
                 <tr>
                    <td style="height: 25px" width="20"> </td>
                         <td style="height: 25px" width="14"></td>
                         <td style="height: 25px"><asp:ImageButton ID="btnAddMedrec" OnClick="btnAddMedrec_OnClick" runat="server" AlternateText="Save medical record" ImageUrl="../../images/butt_add.gif" /></td>
                         <td rowspan="1" style="width: 295px; height: 25px"></td>
                         <td style="width: 17px; height: 25px"></td>
                         <td style="width: 120px; height: 25px"></td>
                         <td style="width: 157px; height: 25px"></td>
                     </tr>
		
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
				
                </table>
      
             <table id="Table11" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td colSpan="2"><IMG height="0" src="../../images/spacer.gif" width="5" border="0"></td>
				</tr>
			</table>		
			
			<input type=hidden id=isNew runat=server />
			<input type=hidden id=hidEmpCode value='' runat=server />
			<input type=hidden id=hidEmpName value='' runat=server />
			<input type=hidden id=hidStatus value=0 runat=server/>
			<input type=hidden id=idPayHist  runat=server />
			<input type=hidden id=idWrkHist  runat=server />
		</div>
        </td>
        </tr>
        </table>
		</form>    
	</body>
</html>

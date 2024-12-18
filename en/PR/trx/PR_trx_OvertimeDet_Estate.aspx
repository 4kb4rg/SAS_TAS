<%@ Page Language="vb" Src="../../../include/PR_trx_OvertimeDet_Estate.aspx.vb" Inherits="PR_trx_OvertimeDet_Estate" %>

<%@ Register TagPrefix="UserControl" TagName="MenuPRTrx" Src="../../menu/menu_prtrx.ascx" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<html>
<head>
    <title>Contract Payment Details</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
//		 function isNumberKey(evt)
//            {
//             var charCode = (evt.which) ? evt.which : event.keyCode
//                if (charCode > 31 && (charCode < 46 || charCode > 57))
//                return false;

//             return true;
//            }


        function setovrhour()
        {
        var doc = document.frmMain;
        
        var s = doc.txtStartTm.value;
        var e = doc.txtEndTm.value;
//        
        if ((s.length==5) && (e.length==5))
        {
        var s1 = s.substring(2,3);
        var e1 = e.substring(2,3);
        
           if ((s1==":")&&(e1==":"))
            {
                var s2 = parseFloat(s.substring(0,2))
                var ms2 = parseFloat(s.substring(3,5))
                var e2 = parseFloat(e.substring(0,2))
                var me2 = parseFloat(e.substring(3,5))
            
                if (e2 < s2)
                {
                               
                    var a = (24-s2)+e2;
                    var b = ((60+me2)-ms2)/60;
                    doc.TxtQty.value = (a+b).toFixed(2);
                }
                else
                {
                     var a = (e2 - s2)-1;
                     var b = ((60+me2)-ms2)/60;
                     doc.TxtQty.value = (a+b).toFixed(2);
                }
                //setOvertimePsn()                
            }
            else
            {
                exit;
            }
        }
        else
        {
        exit;
        }      
        }
            
        function setOvertimePsn()
        {
         var doc = document.frmMain;
         var _dt = doc.txtWPDate.value;
         var dt = new Date(doc.txtWPDate.value).getDate();
         var mn = new Date(doc.txtWPDate.value).getMonth();
         var yr = new Date(doc.txtWPDate.value).getFullYear();
         
         if (dt < 10)
            dt='0'+dt;
         else
            dt=dt; 
            
         if (mn < 9)
           mn='0'+(mn+1);
         else
           mn=(mn+1);
           
         var dt__ = dt+'/'+mn+'/'+yr
         
       mydate = new Date(dt__)
       myday = mydate.getDay();
   
       
       if (myday==0)
         {
            var s = parseFloat(doc.TxtQty.value)-7
            doc.txt150.value = parseFloat(0)
            if (s > 0)
            {
            doc.txt200.value = parseFloat(doc.TxtQty.value)
            doc.txt300.value = parseFloat(s)
            doc.txt400.value = parseFloat(0)
            }
            else
            {
            doc.txt200.value = parseFloat(doc.TxtQty.value)
            doc.txt300.value = parseFloat(0)
            doc.txt400.value = parseFloat(0)
            } 
         }
       else
         {
            var s =  parseFloat(doc.TxtQty.value)-1      
            if (s >= 0)
            {
            doc.txt150.value = parseFloat(1)
            doc.txt200.value = parseFloat(s)
            doc.txt300.value = parseFloat(0)
            doc.txt400.value = parseFloat(0)
            }
            else
            {
            doc.txt150.value = parseFloat(doc.TxtQty.value)
            doc.txt200.value = parseFloat(0)
            doc.txt300.value = parseFloat(0)
            doc.txt400.value = parseFloat(0)
            }
            
         }
         
         if (doc.txt150.value=='NaN') doc.txt150.value = 0
         if (doc.txt200.value=='NaN') doc.txt200.value = 0
         if (doc.txt300.value=='NaN') doc.txt300.value = 0
         if (doc.txt400.value=='NaN') doc.txt400.value = 0
         
         doc.thrs.value  = doc.TxtQty.value
         doc.tpsn1.value = doc.txt150.value
         doc.tpsn2.value = doc.txt200.value
         doc.tpsn3.value = doc.txt300.value
         doc.tpsn4.value = doc.txt400.value
        
        }
          
        function hitungbalik1()
        {
         var doc = document.frmMain;
         
         if (doc.txt150.value > 1)
            {
            alert('Max value 1 !')
            setOvertimePsn()
            exit;
            }
        
        }
        
         function hitungbalik2()
        {
         var doc = document.frmMain;
         doc.TxtQty.value = parseFloat(doc.txt150.value*1)+ parseFloat(doc.txt200.value)
         
        
        }
        
        
              
    </script>

    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 8px;
        }
    </style>
      <Preference:PrefHdl ID="PrefHdl" runat="server" />
</head>
<body>
  
    <form id="frmMain" class="main-modul-bg-app-list-pu" runat="server">
<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 400px" valign="top">
			    <div class="kontenlist">     
        <table border="0" cellspacing="0" cellpadding="2" width="99%" id="TABLE1" class="font9Tahoma">
            <tr>
                <td colspan="5">
                    <UserControl:MenuPRTrx ID="MenuPRTrx" runat="server" />
                </td>
            </tr>
            <tr>
                <td   colspan="5">
                    <br />
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td class="font9Tahoma">
                  <strong> DETAIL LEMBUR KARYAWAN</strong> </td>
                            <td class="font9Header" style="text-align: right">
                    Periode :
                    <asp:Label ID="lblPeriod" runat="server" />&nbsp;|
                    Tgl Update :
                    <asp:Label ID="lblLastUpdate" runat="server" />&nbsp;|
                    Status :
                    <asp:Label ID="lblStatus" runat="server" />&nbsp;|
                    Tgl Buat :
                    <asp:Label ID="lblDateCreated" runat="server" />&nbsp;|
                    Tgl Buat :
                <asp:Label ID="lblUpdatedBy" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="5" class="style2">
                    <hr style="width :100%" />
                    </td>
            </tr>
            <tr>
                <td width="15%" height="25">
                    No.Lembur :
                </td>
                <td style="width: 425px">
                    <asp:Label ID="LblidM" runat="server" />
                    -
                    <asp:Label ID="LblidD" runat="server" /></td>
                <td>
                    &nbsp;</td>
                <td width="15%">
                    &nbsp;</td>
                <td style="width: 267px">
                    &nbsp;</td>
            </tr>
			<tr>
                <td height="25">
                    Tanggal : *</td>
                <td style="width: 425px; height: 28px">
                    <asp:TextBox ID="txtWPDate" runat="server" CssClass="fontObject" MaxLength="10" Width="50%"></asp:TextBox>
                    <a href="javascript:PopCal('txtWPDate');">
                        <asp:Image ID="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif" /></a></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td style="width: 267px;">
                    &nbsp;</td>
            </tr>
			
            <tr>
                <td style="height: 27px">
                    Divisi :
                </td>
                <td style="width: 425px; height: 27px">
                    <asp:DropDownList ID="ddlEmpDiv"  CssClass="fontObject" runat="server" Width="88%" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpDiv_OnSelectedIndexChanged" />
                    <asp:Label ID="lbldivisi" runat="server" /></td>
                <td style="height: 27px">
                    &nbsp;</td>
                <td style="height: 27px">
                    &nbsp;</td>
                <td style="height: 27px; width: 267px;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td height="25">
                    Nama:*
                </td>
                <td style="width: 425px">
                    <asp:DropDownList ID="ddlEmpCode"  CssClass="fontObject" runat="server" Width="88%" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlEmpCode_OnSelectedIndexChanged" /><asp:Label ID="lblempcode"
                            runat="server" /></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td style="width: 267px;">
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td style="height:28px">
                    Sub Kategori :</td>
                <td style="width: 425px; height: 28px;">
                    <asp:DropDownList ID="ddlsubcat"  CssClass="fontObject" runat="server" Width="88%" OnSelectedIndexChanged="ddlsubcat_OnSelectedIndexChanged" AutoPostBack=true /></td>
                <td style="height: 28px">
                    &nbsp;</td>
                <td style="height: 28px">
                    &nbsp;</td>
                <td style="width: 267px; height: 28px;">&nbsp;</td>
            </tr>
            
            <tr>
                <td style="height:28px">
                    Aktiviti :</td>
                <td style="width: 425px; height: 28px;">
                    <asp:DropDownList ID="ddljob"  CssClass="fontObject" runat="server" Width="88%" OnSelectedIndexChanged="ddljob_OnSelectedIndexChanged" AutoPostBack=true /></td>
                <td style="height: 28px">
                    &nbsp;</td>
                <td style="height: 28px"></td>
                <td style="width: 267px; height: 28px;"></td>
            </tr>
            
            <tr>
                <td style="height:28px">
                    Tahun Tanam :</td>
                <td style="width: 425px; height: 28px;">
                    <asp:DropDownList ID="ddlttnm"  CssClass="fontObject" runat="server" Width="88%" /></td>
                <td style="height: 28px">
                    &nbsp;</td>
                <td style="height: 28px"></td>
                <td style="width: 267px; height: 28px;"></td>
            </tr>
            
            
            <tr>
                <td style="height: 28px">
                    Jam Lembur (hh:mm):</td>
                <td style="width: 425px; height: 28px;">
                    <asp:TextBox ID="txtStartTm" CssClass="fontObject" runat="server" MaxLength="5" Width="12%" onkeyup="setovrhour()"></asp:TextBox>to
                    <asp:TextBox ID="txtEndTm"  CssClass="fontObject" runat="server" MaxLength="5" Width="12%" onkeyup="setovrhour()"></asp:TextBox>
				    &nbsp;<asp:DropDownList ID="ddlstp" runat="server" />
				</td>
                <td style="height: 28px">
                </td>
				    
                <td style="height: 28px">
				
                </td>
                <td style="width: 267px; height: 28px;">
                </td>
            </tr>
            
            <tr>
                <td style="height: 28px">
                    Jumlah Jam&nbsp;:</td>
                <td style="height: 28px">
                    <asp:TextBox ID="TxtQty" runat="server" MaxLength="10" Width="12%" style="text-align:right;"  onkeypress="return isNumberKey(event)"
                        onkeyup="setOvertimePsn()" CssClass="mr-h" ReadOnly="True" ></asp:TextBox>&nbsp;Jam&nbsp;&nbsp;<asp:Label
                            ID="lblkoreksi" runat="server">Koreksi</asp:Label>
                    <%--<div style="display:none" id="txtkorek">--%>
                    <asp:TextBox ID="txtkoreksi" runat="server" MaxLength="10" style="text-align:right;"  onkeypress="return isNumberKey(event)"
                        onkeyup="setOvertimePsn()" Width="12%"></asp:TextBox>&nbsp;
                    <asp:ImageButton ID="btnCalc" AlternateText="kalkulasi OT %" OnClick="btnCalcClick" ImageUrl="../../images/butt_process.gif"
                        runat="server" /><%--</div>--%></td>
                <td style="height: 28px">
                </td>
                <!-- Based on HoiYee, this Qty is not calculated, so is ok if the datatype in table is nchar -->
                <!-- but , the question is I will ask, why in this form the data type is DOUBLE data type ? -->
                <td style="height: 28px">
                </td>
                <td style="width: 267px; height: 28px;">
                </td>
            </tr>
            <tr>
                <td style="height: 68px">
                    % Lembur :</td>
                <!-- Modified BY ALIM maxlength=22 -->
                <td style="width: 425px; height: 68px;">
                    <table>
                        <tr>
                            <td style="width: 40px"><asp:Label ID="lbl_psn1" runat="server" /></td>
                            <td style="width: 40px"><asp:Label ID="lbl_psn2" runat="server" /></td>
                            <td style="width: 40px"><asp:Label ID="lbl_psn3" runat="server" /></td>
                            <td style="width: 40px"><asp:Label ID="lbl_psn4" runat="server" /></td>
                        </tr>
                        <tr>
                            <td style="width: 40px">
                                <asp:TextBox ID="txt150" runat="server" MaxLength="10" Width="100%" style="text-align:right;" onkeypress="return isNumberKey(event)"
                                    CssClass="mr-h" ReadOnly="False" /></td>
                            <td style="width: 40px">
                                <asp:TextBox ID="txt200" runat="server" MaxLength="10" Width="100%" style="text-align:right;" onkeypress="return isNumberKey(event)"
                                    CssClass="mr-h" ReadOnly="False" /></td>
                            <td style="width: 40px">
                                <asp:TextBox ID="txt300" runat="server" MaxLength="10" Width="100%" style="text-align:right;" onkeypress="return isNumberKey(event)"
                                    CssClass="mr-h" ReadOnly="False" /></td>
                            <td style="width: 40px">
                                <asp:TextBox ID="txt400" runat="server" MaxLength="10" Width="100%" style="text-align:right;" onkeypress="return isNumberKey(event)"
                                    CssClass="mr-h" ReadOnly="False" /></td>
                        </tr>
                    </table>
                </td>
                <td style="height: 68px">
                </td>
                <td style="height: 68px">
                </td>
                <!-- Modified BY ALIM maxlength=22 -->
                <td style="width: 267px; height: 68px;">
                    &nbsp;&nbsp;<!-- Modified BY ALIM -->
                </td>
            </tr>
            <tr>
                <td style="height: 39px">
                    Deskripsi :</td>
                <td style="width: 425px; height: 39px">
                    <asp:TextBox ID="TxtDeskripsi"  CssClass="fontObject" runat="server" Width="88%"></asp:TextBox></td>
                <td style="height: 39px">
                </td>
                <td style="height: 39px">
                </td>
                <td style="width: 267px; height: 39px">
                </td>
            </tr>
            <tr>
                <td colspan="5">
				     <asp:Label ID="lblErrMessage" Visible="false" ForeColor="red" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
				    <asp:ImageButton ID="NewBtn" AlternateText="  New  " OnClick="NewBtn_Click" ImageUrl="../../images/butt_new.gif"
                        runat="server" />
                    <asp:ImageButton ID="SaveBtn" AlternateText="  Save  " OnClick="SaveBtn_Click" ImageUrl="../../images/butt_save.gif"
                        runat="server" />
                    <asp:ImageButton ID="DeleteBtn" AlternateText=" Close " OnClick="DeleteBtn_Click"
                        ImageUrl="../../images/butt_delete.gif" runat="server" />
                    <asp:ImageButton ID="BackBtn" OnClick="BackBtn_Click" AlternateText="  Back  " CausesValidation="False"
                        ImageUrl="../../images/butt_back.gif" runat="server" />
					<asp:ImageButton ID="VerBtn" AlternateText="  Verified  " OnClick="BtnVerBK_onClick"
                        CausesValidation="False" ImageUrl="../../images/butt_verified.gif" runat="server" />
					<asp:ImageButton ID="ConfBtn" AlternateText="  Confirm  " OnClick="BtnConfBK_onClick"
                        CausesValidation="False" ImageUrl="../../images/butt_confirm.gif" runat="server" />	
					<asp:Button ID="ReActBt" Text="  ReActive  " OnClick="BtnReActBK_onClick" class="button-small"   runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="6" style="height: 42px">
                    <table border="0" cellspacing="1" cellpadding="1" width="100%">
                        <tr>
                            <td align="center" style="border-right: green 1px dotted; border-top: green 1px dotted;
                                border-left: green 1px dotted; border-bottom: green 1px dotted; background-color: transparent;
                                width: 100%;">
                                Lembur</td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:DataGrid ID="dgOvtDet" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    CellPadding="2" GridLines="None" PagerStyle-Visible="False" PageSize="15" Width="100%" CssClass="font9Tahoma"
                                    OnItemCommand="GetItem" ShowFooter="True" OnItemDataBound=KeepRunningSum>
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
										<asp:TemplateColumn HeaderText="No.">
															<ItemTemplate>
																<%# Container.ItemIndex + 1 %>
															</ItemTemplate>
															<ItemStyle HorizontalAlign="Right" Width="3%" />
										</asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="ID.Lebur" SortExpression="OtLnID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Container.DataItem("OtLnID") %>' Visible="false"></asp:Label>
                                                <asp:LinkButton ID="lbID" runat="server" CommandName="Item" Text='<%# Container.DataItem("OtLnID") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Tanggal" SortExpression="OtDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldate" runat="server" Text='<%# format(Container.DataItem("OtDate"),"dd/MM/yyyy") %>'
                                                    Visible="false" />
                                                <%#objGlobal.GetLongDate(Container.DataItem("OtDate"))%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Aktiviti">
                                            <ItemTemplate>
                                                <asp:Label ID="lblJob" runat="server" Text='<%# trim(Container.DataItem("Job")) %>' />
                                                <asp:HiddenField ID="hidjob" runat="server" Value='<%# trim(Container.DataItem("codeJob")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Deskripsi" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesc" runat="server" Text='<%# trim(Container.DataItem("Description")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
										
										<asp:TemplateColumn HeaderText="T.Tanam">
                                            <ItemTemplate>
                                                <asp:Label ID="lblblk" runat="server" Text='<%# trim(Container.DataItem("CodeBlk")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
										
                                        <asp:TemplateColumn HeaderText="Jam Lembur">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstartTm" runat="server" Text='<%# format(Container.DataItem("StartTm"),"HH:mm") %>'
                                                    Visible="false" />
                                                <asp:Label ID="lblendTm" runat="server" Text='<%# format(Container.DataItem("EndTm"),"HH:mm") %>'
                                                    Visible="false" />
                                                <asp:Label ID="lblTime" Text='<%# Container.DataItem("TimeOT") %>' runat="server" />
                                            </ItemTemplate>
											
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Jml Jam">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" Text='<%# Container.DataItem("Hours") %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle BorderWidth="1px" BorderStyle="Outset" BorderColor="#CCCCCC" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
											<FooterTemplate >
												<asp:Label ID=lbTotal runat=server />
											</FooterTemplate>
											<FooterStyle BorderWidth=1px BorderStyle=Outset BorderColor="Black" HorizontalAlign="Right" BackColor="#CCCCCC" ForeColor="Black" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Jam I">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpsn1" Text='<%# Container.DataItem("psn1") & "% x " & Container.DataItem("Ovr1") %>' runat="server" />
                                                <asp:HiddenField ID="hidpsn1" runat="server" Value='<%# Container.DataItem("psn1") %>' />
                                                <asp:HiddenField ID="hidovr1" runat="server" Value='<%# Container.DataItem("Ovr1") %>' />
                                            </ItemTemplate>
                                            <ItemStyle BorderWidth="1px" BorderStyle="Outset" BorderColor="#CCCCCC" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
											<FooterTemplate >
												<asp:Label ID=lbTotal1 runat=server />
											</FooterTemplate>
											<FooterStyle BorderWidth=1px BorderStyle=Outset BorderColor="Black" HorizontalAlign="Right" BackColor="#CCCCCC" ForeColor="Black" />

                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Jam II">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpsn2" Text='<%# Container.DataItem("psn2") & "% x " & Container.DataItem("Ovr2") %>' runat="server" />
                                                <asp:HiddenField ID="hidpsn2" runat="server" Value='<%# Container.DataItem("psn2") %>' />
                                                <asp:HiddenField ID="hidovr2" runat="server" Value='<%# Container.DataItem("Ovr2") %>' />
                                            </ItemTemplate>
                                            <ItemStyle BorderWidth="1px" BorderStyle="Outset" BorderColor="#CCCCCC" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
											<FooterTemplate >
												<asp:Label ID=lbTotal2 runat=server />
											</FooterTemplate>
											<FooterStyle BorderWidth=1px BorderStyle=Outset BorderColor="Black" HorizontalAlign="Right" BackColor="#CCCCCC" ForeColor="Black" />

                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Jam III">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPsn3" Text='<%# Container.DataItem("psn3") & "% x " & Container.DataItem("Ovr3") %>' runat="server" />
                                                <asp:HiddenField ID="hidpsn3" runat="server" Value='<%# Container.DataItem("psn3") %>' />
                                                <asp:HiddenField ID="hidovr3" runat="server" Value='<%# Container.DataItem("Ovr3") %>' />
                                            </ItemTemplate>
                                            <ItemStyle BorderWidth="1px" BorderStyle="Outset" BorderColor="#CCCCCC" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
											<FooterTemplate >
												<asp:Label ID=lbTotal3 runat=server />
											</FooterTemplate>
											<FooterStyle BorderWidth=1px BorderStyle=Outset BorderColor="Black" HorizontalAlign="Right" BackColor="#CCCCCC" ForeColor="Black" />

                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Jam IV">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpsn4" Text='<%# Container.DataItem("psn4") & "% x " & Container.DataItem("Ovr4") %>' runat="server" />
                                                <asp:HiddenField ID="hidpsn4" runat="server" Value='<%# Container.DataItem("psn4") %>' />
                                                <asp:HiddenField ID="hidovr4" runat="server" Value='<%# Container.DataItem("Ovr4") %>' />
                                            </ItemTemplate>
                                            <ItemStyle BorderWidth="1px" BorderStyle="Outset" BorderColor="#CCCCCC" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
											<FooterTemplate >
												<asp:Label ID=lbTotal4 runat=server />
											</FooterTemplate>
											<FooterStyle BorderWidth=1px BorderStyle=Outset BorderColor="Black" HorizontalAlign="Right" BackColor="#CCCCCC" ForeColor="Black" />

                                        </asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Rp">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmt" Text='<%# Container.DataItem("Amount") %>' runat="server" />
                                                <asp:HiddenField ID="hidAmt" runat="server" Value='<%# Container.DataItem("Amount") %>' />
                                            </ItemTemplate>
                                            <ItemStyle BorderWidth="1px" BorderStyle="Outset" BorderColor="#CCCCCC" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
											<FooterTemplate >
												<asp:Label ID=lbTotalAmt runat=server />
											</FooterTemplate>
											<FooterStyle BorderWidth=1px BorderStyle=Outset BorderColor="Black" HorizontalAlign="Right" BackColor="#CCCCCC" ForeColor="Black" />

                                        </asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstat" runat="server" Text='<%# trim(Container.DataItem("StatApv")) %>' />
												<asp:Label ID="lblstat2" runat="server" Text='<%# trim(Container.DataItem("Status")) %>' visible=False/>
												<asp:Label ID="lblOC" runat="server" Text='<%# trim(Container.DataItem("OTCode")) %>' visible=False/>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle Visible="False" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
            </tr>
            <tr>
                <td colspan="5">
                    <hr style="width :100%" />
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                    Tarif Lembur/Jam :<asp:Label ID="LblTarifLembur" style="font:bold;" runat="server" ForeColor="Black"></asp:Label><br />
                 </tr>
        </table>
        <input type="hidden" id="isNew" value="" runat="server" />
		<input type="hidden" id="isEdit" value="" runat="server" />
		<input type="hidden" id="otlnid" value="" runat="server" />
        <input type="hidden" id="thrs" value="" runat="server" />
        <input type="hidden" id="tpsn1" value="" runat="server" />
        <input type="hidden" id="tpsn2" value="" runat="server" />
        <input type="hidden" id="tpsn3" value="" runat="server" />
        <input type="hidden" id="tpsn4" value="" runat="server" />
        <input type="hidden" id="ovrrate"  runat="server" />
		<input type="hidden" id="codeempty"  runat="server" />
		<input type="hidden" id="hidstatus"  runat="server" />
	</div>
    </td>
    </tr>
    </table>
    </form>
</body>
</html>

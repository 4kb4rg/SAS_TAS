<%@ Page Language="vb" Src="../../../include/PR_trx_BKMDet_Estate.aspx.vb" Inherits="PR_BKMDet_Estate" %>

<%@ Register TagPrefix="UserControl" TagName="MenuPRTrx" Src="../../menu/menu_prtrx.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<html>
<head>
    <title>BUKU KERJA DETAIL</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <Preference:PrefHdl ID="PrefHdl" runat="server" />
    <form id="frmMain" class="main-modul-bg-app-list-pu" runat="server">
          <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

        <asp:Label ID="lblErrMessage" Visible="false" Text="" ForeColor="red" runat="server" />
        <table border="0" cellspacing="1" cellpadding="1" width="99%" class="font9Tahoma">
            <tr>
                <td colspan="6">
                    <UserControl:MenuPRTrx ID="MenuPRTrx" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="mt-h" colspan="3">
                  <strong>  BUKU KERJA DETAIL</strong> </td>
                <td colspan="3" align="right">
                    <asp:Label ID="lblTracker" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="6">
                   <hr style="width :100%" />   
                </td>
            </tr>
            <tr>
                <td height="25" style="width: 206px">
                    ID
                </td>
                <td style="width: 347px">
                    <asp:Label ID="LblIDM" runat="server" Width="253px"></asp:Label></td>
                <td style="width: 29px">
                    &nbsp;</td>
                <td style="width: 192px">
                    &nbsp;</td>
                <td style="width: 236px">
                </td>
            </tr>
            <tr>
                <td height="25" style="width: 206px">
                    Tanggal (hari/bulan/tahun): *</td>
                <td style="width: 347px">
                    <asp:TextBox ID="txtWPDate" runat="server" MaxLength="10" Width="50%" />
                    <a href="javascript:PopCal('txtWPDate');"><asp:Image ID="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
                    <asp:Label ID="lblwpdate" Visible="false" runat="server" Width="100%" /></td>
                <td style="width: 29px">
                    &nbsp;</td>
                <td style="width: 192px">
                    Periode :
                </td>
                <td style="width: 236px">
                    <asp:Label ID="lblPeriod" runat="server" /></td>
            </tr>
            <tr>
                <td height="25" style="width: 206px">
                    Kategori : *</td>
                <td style="width: 347px">
                    <GG:AutoCompleteDropDownList ID="ddlbkcategory" runat="server" Width="88%" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlbkcategory_OnSelectedIndexChanged" />
                    <asp:Label ID="lblbkcategory" Visible="false" runat="server" Width="100%" /></td>
                <td style="width: 29px">
                    &nbsp;</td>
                <td style="width: 192px">
                    Status :
                </td>
                <td style="width: 236px">
                    <asp:Label ID="lblStatus" runat="server" /></td>
            </tr>
            <tr>
                <td height="25" style="width: 206px">
                    Sub Kategori : *</td>
                <td style="width: 347px">
                    <GG:AutoCompleteDropDownList ID="ddlbksubcategory" runat="server" Width="88%" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlbksubcategory_OnSelectedIndexChanged" />
                    <asp:Label ID="lblbksubcategory" Visible="false" runat="server" Width="100%" /></td>
                <td style="width: 29px">
                </td>
                <td style="width: 192px">
                    Date Created :</td>
                <td style="width: 236px">
                    <asp:Label ID="lblDateCreated" runat="server" /></td>
            </tr>
            <tr>
                <td height="25" style="width: 206px">
                    Divisi Code:*</td>
                <td style="width: 347px">
                    <asp:DropDownList ID="ddldivisicode" AutoPostBack="true" runat="server" Width="88%"
                        OnSelectedIndexChanged="ddldivisicode_OnSelectedIndexChanged" />
                    <asp:Label ID="lbldivisicode" Visible="false" runat="server" Width="100%" />
                </td>
                <td style="width: 29px">
                    &nbsp;</td>
                <td style="width: 192px">
                    Last Updated :</td>
                <td style="width: 236px">
                    <asp:Label ID="lblLastUpdate" runat="server" /></td>
            </tr>
            <tr>
                <td height="25" style="width: 206px">
                    <asp:Label ID="lbltxtmandor" runat="server" Width="100%">Mandor Code :*</asp:Label></td>
                <td style="width: 347px">
                    <asp:DropDownList ID="ddlMandorCode" AutoPostBack="true" runat="server" Width="88%"
                        OnSelectedIndexChanged="ddlMandorCode_OnSelectedIndexChanged" />
                    <asp:DropDownList ID="ddlkcscode" runat="server" Width="88%" Visible="False" />
                    <asp:Label ID="lblMandorCode" Visible="false" runat="server" Width="100%" />
                    <asp:Label ID="lblkcscode" Visible="false" runat="server" Width="100%" />
                </td>
                <td style="width: 29px">
                    &nbsp;</td>
                <td style="width: 192px">
                    Updated by :
                </td>
                <td style="width: 236px;">
                    &nbsp;<asp:Label ID="lblupdatedby" runat="server" /></td>
            </tr>
            <tr>
                <td height="25" style="width: 206px">
                    <asp:Label ID="Label1" runat="server" Width="100%">Keterangan :</asp:Label></td>
                <td colspan="5">
                    <asp:TextBox ID="TextBox1" runat="server" MaxLength="128" Width="99%"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="6" style="height: 36px">
                    <hr style="width :100%" />   
                </td>
            </tr>
            <tr>
                <td style="height: 24px;" colspan="5" class="font9Tahoma">
                    <igtab:UltraWebTab Visible="false" ID="TABBK" ThreeDEffect="False" TabOrientation="TopLeft"
                        SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" runat="server">
                        <DefaultTabStyle ForeColor="transparent" Height="22px">
                        </DefaultTabStyle>
                        <HoverTabStyle CssClass="ContentTabHover">
                        </HoverTabStyle>
                        <RoundedImage LeftSideWidth="6" RightSideWidth="6" SelectedImage="../../images/thumbs/ig_tab_winXP1.gif"
                            NormalImage="../../images/thumbs/ig_tab_winXP3.gif" HoverImage="../../images/thumbs/ig_tab_winXP2.gif"
                            FillStyle="LeftMergedWithCenter"></RoundedImage>
                        <SelectedTabStyle CssClass="ContentTabSelected">
                        </SelectedTabStyle>
                        <Tabs>
                            <%--Perawatan--%>
                            <igtab:Tab Key="RWT" Text="Karyawan & Pekerjaan" Tooltip="Karyawan">
                                <ContentPane>
                                    <table border="0" class="mb-c" cellspacing="1" cellpadding="1" width="99%">
                                        <tr>
                                            <td colspan="5">
                                                <asp:DataGrid ID="dgRW" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                    CellPadding="2" OnItemDataBound="dgRW_BindGrid" OnCancelCommand="dgRW_Cancel"
                                                    OnDeleteCommand="dgRW_Delete" Width="100%">
                                                    <AlternatingItemStyle CssClass="mr-r" />
                                                    <ItemStyle CssClass="mr-l" />
                                                    <HeaderStyle CssClass="mr-h" />
                                                    <Columns>
                                                        <%--<asp:TemplateColumn HeaderText="No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="no" Width="10px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateColumn>--%>
                                                        <asp:TemplateColumn HeaderText="Karyawan">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgRW_lbl_ed" Width="150px" Text='<%# Container.DataItem("EmpDet") %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <GG:AutoCompleteDropDownList ID="dgRW_ddl_ededit" OnSelectedIndexChanged="dgRW_ddl_ededit_OnSelectedIndexChanged"
                                                                    AutoPostBack="true" Width="80%" runat="server" />
                                                                <input type="button" value=" ... "  id="gbRW_ButtonFind" onclick="javascript:PopEmp('frmMain', '', 'txt_hid_emp', 'False');" runat="server" />
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Pekerjaan">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgRW_hid_jb" Value='<%# Container.DataItem("job") %>' runat="Server" />
                                                                <GG:AutoCompleteDropDownList ID="dgRW_ddl_jb" Width="250px" OnSelectedIndexChanged="dgRW_ddl_jb_OnSelectedIndexChanged"
                                                                    AutoPostBack="true" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Blok">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgRW_hid_cb" Value='<%# Container.DataItem("codeblok") %>' runat="Server" />
                                                                <GG:AutoCompleteDropDownList ID="dgRW_ddl_cb" OnSelectedIndexChanged="dgRW_ddl_cb_OnSelectedIndexChanged"
                                                                    AutoPostBack="true" Width="120px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="HK">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgRW_txt_hk" Text='<%# Container.DataItem("hkjob") %>' Width="30px"
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Hasil Kerja">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgRW_txt_hs" Text='<%# Container.DataItem("hsljob") %>' Width="50px"
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="9%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Rot">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgRW_txt_rot" Text='<%# Container.DataItem("rotasi") %>' Width="20px"
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Norma">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgRW_lbl_nr" Width="50px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="UOM">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgRW_lbl_um" Text='<%# Container.DataItem("uom") %>' Width="20px"
                                                                    runat="server" />
                                                                <asp:HiddenField ID="dgRW_hid_ec" Value='<%# Container.DataItem("codeemp") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgRW_hid_id" Value='<%# Container.DataItem("id") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgRW_hid_idx" Value='<%# Container.DataItem("idx") %>' runat="Server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" CommandName="Delete" Text="Delete" runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <%--<asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>--%>
                                                                <asp:LinkButton ID="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation="False"
                                                                    runat="server" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" style="width: 185px">
                                                <asp:ImageButton ID="btn_RWT_add" OnClick="RWT_add_onClick" runat="server" AlternateText="+ Karyawan"
                                                    ImageUrl="../../images/butt_add.gif" /></td>
                                            <td style="width: 378px">
                                                &nbsp;</td>
                                            <td style="width: 29px">
                                                &nbsp;</td>
                                            <td style="width: 192px">
                                                &nbsp;</td>
                                            <td style="width: 200px">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>
                            <%--Panen Potong Buah--%>
                            <igtab:Tab Key="PBS" Text="Potong Buah" Tooltip="Potong buah">
                                <ContentPane>
                                    <table border="0" class="mb-c" cellspacing="1" cellpadding="1" width="99%">
                                        <tr>
                                            <td colspan="5">
                                                <asp:DataGrid ID="dgPBS" runat="server" AutoGenerateColumns="False" OnItemDataBound="dgPBS_BindGrid"
                                                    OnCancelCommand="dgPBS_Cancel" OnDeleteCommand="dgPBS_Delete" GridLines="Both" CellPadding="2" PageSize="6"
                                                    Width="100%">
                                                    <AlternatingItemStyle CssClass="mr-r" />
                                                    <ItemStyle CssClass="mr-l" />
                                                    <HeaderStyle CssClass="mr-h" />
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="Karyawan">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgPBS_lbl_ed" Width="150px" Text='<%# Container.DataItem("empdet") %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <GG:AutoCompleteDropDownList ID="dgPBS_ddl_ededit" Width="80%" OnSelectedIndexChanged="dgPBS_ddl_ededit_OnSelectedIndexChanged"
                                                                    AutoPostBack="true" runat="server" />
                                                                <input type="button" value=" ... " id="Find" onclick="javascript:PopEmp('frmMain', '', 'dgPBS_ddl_ededit', 'True');"
                                                                    runat="server" />
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="HK">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPBS_txt_hk" Width="30px" Text='<%# Container.DataItem("hk")%>'
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Blok">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgPBS_hid_cb" Value='<%# Container.DataItem("codeblok") %>'
                                                                    runat="Server" />
                                                                <asp:DropDownList ID="dgPBS_ddl_cb" Width="90px" OnSelectedIndexChanged="dgPBS_Blok_OnSelectedIndexChanged"
                                                                    runat="server" AutoPostBack="True" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="JJG">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPBS_txt_jg" Text='<%# Container.DataItem("jjg") %>' onkeypress="javascript:return isNumberKey(event)"
                                                                    OnTextChanged="dgPBS_jjg_OnTextChanged" Width="30px" runat="server" AutoPostBack="True" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Hektar">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPBS_txt_ha" Text='<%# Container.DataItem("ha") %>' onkeypress="javascript:return isNumberKey(event)"
                                                                    Width="30px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Rotasi">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPBS_txt_rot" Text='<%# Container.DataItem("rotasi") %>' onkeypress="javascript:return isNumberKey(event)"
                                                                    Width="20px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="BJR">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPBS_txt_bjr" CssClass="mr-h" Text='<%# Container.DataItem("bjr") %>'
                                                                    ReadOnly="true" Width="35px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Kg">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPBS_txt_kg" CssClass="mr-h" Text='<%# Container.DataItem("kg") %>'
                                                                    ReadOnly="true" Width="50px" runat="server" />
                                                                <asp:HiddenField ID="dgPBS_hid_ec" Value='<%# Container.DataItem("codeemp") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgPBS_hid_jb" Value='<%# Container.DataItem("codejob") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgPBS_hid_id" Value='<%# Container.DataItem("id") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgPBS_hid_idx" Value='<%# Container.DataItem("idx") %>' runat="Server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Buah Mentah" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="dgPBS_chk_Dmentah" Checked='<%# Cbool(Container.DataItem("dnd_mentah")) %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Buah Tinggal" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="dgPBS_chk_Dtinggal" Checked='<%# Cbool(Container.DataItem("dnd_tinggal")) %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="B.Tdk TPH" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="dgPBS_chk_Dtph" Checked='<%# Cbool(Container.DataItem("dnd_tph")) %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Tngkai. Panjang" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="dgPBS_chk_Dpjg" Checked='<%# Cbool(Container.DataItem("dnd_pjng")) %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Plpah Sengkleh" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="dgPBS_chk_DSengkleh" Checked='<%# Cbool(Container.DataItem("dnd_sengkleh")) %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Non Basis" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgPBS_txt_Dbasis" Text='<%# Container.DataItem("dnd_basis") %>'
                                                                    Width="30px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                             <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" CommandName="Delete" Text="Delete" runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation="False"
                                                                    runat="server" />
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" style="width: 185px">
                                                <asp:ImageButton ID="Btn_PBS_add" OnClick="PBS_add_onClick" runat="server" AlternateText="+ Pemanen"
                                                    ImageUrl="../../images/butt_add.gif" /></td>
                                            <td style="width: 378px">
                                                &nbsp;</td>
                                            <td style="width: 29px">
                                                &nbsp;</td>
                                            <td style="width: 192px">
                                                &nbsp;</td>
                                            <td style="width: 200px">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>
                            <%--Panen Kutip Brondolan--%>
                            <igtab:Tab Key="KBS" Text="Kutip Brondolan" Tooltip="Kutip Brondolan">
                                <ContentPane>
                                    <table border="0" class="mb-c" cellspacing="1" cellpadding="1" width="99%">
                                        <tr>
                                            <td colspan="5">
                                                <asp:DataGrid ID="dgKBS" runat="server" AutoGenerateColumns="False" OnItemDataBound="dgKBS_BindGrid"
                                                    OnCancelCommand="dgKBS_Cancel" OnDeleteCommand="dgKBS_Delete" GridLines="Both" CellPadding="2" PageSize="6"
                                                    Width="100%">
                                                    <AlternatingItemStyle CssClass="mr-r" />
                                                    <ItemStyle CssClass="mr-l" />
                                                    <HeaderStyle CssClass="mr-h" />
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="Karyawan">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgKBS_lbl_ed" Width="150px" Text='<%# Container.DataItem("empdet") %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <GG:AutoCompleteDropDownList ID="dgKBS_ddl_ededit" Width="80%" OnSelectedIndexChanged="dgKBS_ddl_ededit_OnSelectedIndexChanged"
                                                                    AutoPostBack="true" runat="server" />
                                                                <input type="button" value=" ... " id="Find" onclick="javascript:PopEmp('frmMain', '', 'dgKBS_ddl_ededit', 'True');"
                                                                    runat="server" />
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="HK">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgKBS_txt_hk" Width="30px" Text='<%# Container.DataItem("hk")%> '
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Blok">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgKBS_hid_cb" Value='<%# Container.DataItem("codeblok") %>'
                                                                    runat="Server" />
                                                                <asp:DropDownList ID="dgKBS_ddl_cb" Width="90px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Kg">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgKBS_txt_kg" Text='<%# Container.DataItem("kg") %>' Width="50px" runat="server" onkeypress="javascript:return isNumberKey(event)" />
                                                                <asp:HiddenField ID="dgKBS_hid_ec" Value='<%# Container.DataItem("codeemp") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgKBS_hid_jb" Value='<%# Container.DataItem("codejob") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgKBS_hid_id" Value='<%# Container.DataItem("id") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgKBS_hid_idx" Value='<%# Container.DataItem("idx") %>' runat="Server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Hektar">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgKBS_txt_ha" Text='<%# Container.DataItem("ha") %>' onkeypress="javascript:return isNumberKey(event)"
                                                                    Width="50px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Rotasi">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgKBS_txt_rot" Text='<%# Container.DataItem("rotasi") %>' onkeypress="javascript:return isNumberKey(event)"
                                                                    Width="20px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Kutip Kotor" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgKBS_txt_Dkk" Text='<%# Container.DataItem("dnd_ktpktr") %>' Width="50px"
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Brondolan Kotor" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="dgKBS_chk_Dbk" Checked='<%# Cbool(Container.DataItem("dnd_brdktr")) %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="B.Tdk TPH" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="dgKBS_chk_Dtph" Checked='<%# Cbool(Container.DataItem("dnd_tph")) %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                             <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" CommandName="Delete" Text="Delete" runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation="False"
                                                                    runat="server" />
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" style="width: 185px">
                                                <asp:ImageButton ID="Btn_KBS_add" OnClick="KBS_add_onClick" runat="server" AlternateText="+ Karyawan"
                                                    ImageUrl="../../images/butt_add.gif" /></td>
                                            <td style="width: 378px">
                                                &nbsp;</td>
                                            <td style="width: 29px">
                                                &nbsp;</td>
                                            <td style="width: 192px">
                                                &nbsp;</td>
                                            <td style="width: 200px">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>
                            <%--Panen Muat TBS--%>
                            <igtab:Tab Key="BMS" Text="Bongkar Muat TBS" Tooltip="Bongkar Muat TBS">
                                <ContentPane>
                                    <table border="0" class="mb-c" cellspacing="1" cellpadding="1" width="99%">
                                        <tr>
                                            <td colspan="5">
                                                <asp:DataGrid ID="dgBMS" runat="server" AutoGenerateColumns="False" OnItemDataBound="dgBMS_BindGrid"
                                                    OnCancelCommand="dgBMS_Cancel" OnDeleteCommand="dgBMS_Delete" GridLines="Both" CellPadding="2" PageSize="6"
                                                    Width="100%">
                                                    <AlternatingItemStyle CssClass="mr-r" />
                                                    <ItemStyle CssClass="mr-l" />
                                                    <HeaderStyle CssClass="mr-h" />
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="Karyawan">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgBMS_lbl_ed" Width="150px" Text='<%# Container.DataItem("empdet") %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <GG:AutoCompleteDropDownList ID="dgBMS_ddl_ededit" Width="80%" OnSelectedIndexChanged="dgBMS_ddl_ededit_OnSelectedIndexChanged"
                                                                    AutoPostBack="true" runat="server" />
                                                                <input type="button" value=" ... " id="Find" onclick="javascript:PopEmp('frmMain', '', 'dgBMS_ddl_ededit', 'True');"
                                                                    runat="server" />
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="HK">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgBMS_txt_hk" Width="30px" Text='<%# Container.DataItem("hk")%> '
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Blok">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgBMS_hid_cb" Value='<%# Container.DataItem("codeblok") %>'
                                                                    runat="Server" />
                                                                <asp:DropDownList ID="dgBMS_ddl_cb" Width="90px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Pekerjaan">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgBMS_hid_tyjob" Value='<%# Container.DataItem("TyJob") %>'
                                                                    runat="Server" />
                                                                <asp:DropDownList ID="dgBMS_ddl_tyjob" Width="90px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Kg">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgBMS_txt_kg" Text='<%# Container.DataItem("kg") %>' Width="50px" runat="server" onkeypress="javascript:return isNumberKey(event)" />
                                                                <asp:HiddenField ID="dgBMS_hid_ec" Value='<%# Container.DataItem("codeemp") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgBMS_hid_jb" Value='<%# Container.DataItem("codejob") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgBMS_hid_id" Value='<%# Container.DataItem("id") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgBMS_hid_idx" Value='<%# Container.DataItem("idx") %>' runat="Server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Rotasi">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgBMS_txt_rot" Text='<%# Container.DataItem("rotasi") %>' onkeypress="javascript:return isNumberKey(event)"
                                                                    Width="20px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Denda" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="red">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgBMS_txt_dnd" Width="50px" Text='<%# Container.DataItem("denda") %>'
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                           <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" CommandName="Delete" Text="Delete" runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation="False"
                                                                    runat="server" />
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" style="width: 185px">
                                                <asp:ImageButton ID="Btn_BMS_add" OnClick="BMS_add_onClick" runat="server" AlternateText="+ Karyawan"
                                                    ImageUrl="../../images/butt_add.gif" /></td>
                                            <td style="width: 378px">
                                                &nbsp;</td>
                                            <td style="width: 29px">
                                                &nbsp;</td>
                                            <td style="width: 192px">
                                                &nbsp;</td>
                                            <td style="width: 200px">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>
                            <%-- Civil--%>
                            <igtab:Tab Key="CVL" Text="Karyawan & Pekerjaan" Tooltip="Karyawan">
                                <ContentPane>
                                    <table border="0" class="mb-c" cellspacing="1" cellpadding="1" width="99%">
                                        <tr>
                                            <td colspan="5">
                                                <asp:DataGrid ID="dgAD" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                    CellPadding="2" Width="100%">
                                                    <AlternatingItemStyle CssClass="mr-r" />
                                                    <ItemStyle CssClass="mr-l" />
                                                    <HeaderStyle CssClass="mr-h" />
                                                    <Columns>
                                                        <%--<asp:TemplateColumn HeaderText="No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="no" Width="10px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateColumn>--%>
                                                        <asp:TemplateColumn HeaderText="Karyawan">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgAD_lbl_ed" Width="150px" Text='<%# Container.DataItem("EmpDet") %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <GG:AutoCompleteDropDownList ID="dgAD_ddl_ededit" OnSelectedIndexChanged="dgRW_ddl_ededit_OnSelectedIndexChanged"
                                                                    AutoPostBack="true" Width="80%" runat="server" />
                                                                <input type="button" value=" ... " id="Find" onclick="javascript:PopEmp('frmMain', '', 'dgAD_ddl_ededit', 'True');"
                                                                    runat="server" />
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Pekerjaan">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgAD_hid_jb" Value='<%# Container.DataItem("job") %>' runat="Server" />
                                                                <GG:AutoCompleteDropDownList ID="dgAD_ddl_jb" Width="270px" OnSelectedIndexChanged="dgRW_ddl_jb_OnSelectedIndexChanged"
                                                                    AutoPostBack="true" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Blok">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgAD_hid_cb" Value='<%# Container.DataItem("codeblok") %>' runat="Server" />
                                                                <GG:AutoCompleteDropDownList ID="dgAD_ddl_cb" OnSelectedIndexChanged="dgRW_ddl_cb_OnSelectedIndexChanged"
                                                                    AutoPostBack="true" Width="90px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="HK">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgAD_txt_hk" Text='<%# Container.DataItem("hkjob") %>' Width="30px"
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Hasil Kerja">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgAD_txt_hs" Text='<%# Container.DataItem("hsljob") %>' Width="50px"
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="9%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Norma">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgAD_lbl_nr" Width="50px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="UOM">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgAD_lbl_um" Text='<%# Container.DataItem("uom") %>' Width="20px"
                                                                    runat="server" />
                                                                <asp:HiddenField ID="dgAD_hid_ec" Value='<%# Container.DataItem("codeemp") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgAD_hid_id" Value='<%# Container.DataItem("id") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgAD_hid_idx" Value='<%# Container.DataItem("idx") %>' runat="Server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                             <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" CommandName="Delete" Text="Delete" runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <%--<asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>--%>
                                                                <asp:LinkButton ID="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation="False"
                                                                    runat="server" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" style="width: 185px">
                                                <asp:ImageButton ID="Btn_CVL_add" runat="server" AlternateText="+ Karyawan" ImageUrl="../../images/butt_add.gif" /></td>
                                            <td style="width: 378px">
                                                &nbsp;</td>
                                            <td style="width: 29px">
                                                &nbsp;</td>
                                            <td style="width: 192px">
                                                &nbsp;</td>
                                            <td style="width: 200px">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>
                            <%--Traksi--%>
                            <igtab:Tab Key="TRK" Text="Karyawan & Pekerjaan" Tooltip="Karyawan">
                                <ContentPane>
                                    <table border="0" class="mb-c" cellspacing="1" cellpadding="1" width="99%">
                                        <tr>
                                            <td colspan="5">
                                                <asp:DataGrid ID="dgTRX" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                    CellPadding="2" Width="100%">
                                                    <AlternatingItemStyle CssClass="mr-r" />
                                                    <ItemStyle CssClass="mr-l" />
                                                    <HeaderStyle CssClass="mr-h" />
                                                    <Columns>
                                                        <%--<asp:TemplateColumn HeaderText="No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="no" Width="10px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateColumn>--%>
                                                        <asp:TemplateColumn HeaderText="Karyawan">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgTRX_lbl_ed" Width="150px" Text='<%# Container.DataItem("EmpDet") %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <GG:AutoCompleteDropDownList ID="dgTRX_ddl_ededit" Width="80%" runat="server" />
                                                                <input type="button" value=" ... " id="Find" onclick="javascript:PopEmp('frmMain', '', 'dgTRX_ddl_ededit', 'True');"
                                                                    runat="server" />
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Pekerjaan">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgTRX_hid_jb" Value='<%# Container.DataItem("job") %>' runat="Server" />
                                                                <GG:AutoCompleteDropDownList ID="dgTRX_ddl_jb" Width="270px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="HK">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgTRX_txt_hk" Text='<%# Container.DataItem("hkjob") %>' Width="30px"
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Trip">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgTRX_txt_tr" Text='<%# Container.DataItem("hsljob") %>' Width="50px"
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="9%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Qty">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgTRX_txt_kg" Text='<%# Container.DataItem("hsljob") %>' Width="50px"
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="9%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Over Basis">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgTRX_txt_bs" Text='<%# Container.DataItem("hsljob") %>' Width="50px"
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="9%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Premi">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgTRX_lbl_pr" Width="50px" runat="server" />
                                                                <asp:HiddenField ID="dgRW_hid_id" Value='<%# Container.DataItem("id") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgRW_hid_idx" Value='<%# Container.DataItem("idx") %>' runat="Server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                             <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" CommandName="Delete" Text="Delete" runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <%--<asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>--%>
                                                                <asp:LinkButton ID="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation="False"
                                                                    runat="server" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" style="width: 185px">
                                                <asp:ImageButton ID="Btn_TRK_add" runat="server" AlternateText="+ Karyawan" ImageUrl="../../images/butt_add.gif" /></td>
                                            <td style="width: 378px">
                                                &nbsp;</td>
                                            <td style="width: 29px">
                                                &nbsp;</td>
                                            <td style="width: 192px">
                                                &nbsp;</td>
                                            <td style="width: 200px">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>
                            <%--UMUM--%>
                            <igtab:Tab Key="UMM" Text="Karyawan & Pekerjaan" Tooltip="Karyawan">
                                <ContentPane>
                                    <table border="0" class="mb-c" cellspacing="1" cellpadding="1" width="99%">
                                        <tr>
                                            <td colspan="5">
                                                <asp:DataGrid ID="dgUMM" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                    CellPadding="2" Width="100%">
                                                    <AlternatingItemStyle CssClass="mr-r" />
                                                    <ItemStyle CssClass="mr-l" />
                                                    <HeaderStyle CssClass="mr-h" />
                                                    <Columns>
                                                        <%--<asp:TemplateColumn HeaderText="No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="no" Width="10px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateColumn>--%>
                                                        <asp:TemplateColumn HeaderText="Karyawan">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgUMM_lbl_ed" Width="150px" Text='<%# Container.DataItem("EmpDet") %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <GG:AutoCompleteDropDownList ID="dgUMM_ddl_ededit" OnSelectedIndexChanged="dgRW_ddl_ededit_OnSelectedIndexChanged"
                                                                    AutoPostBack="true" Width="80%" runat="server" />
                                                                <input type="button" value=" ... " id="Find" onclick="javascript:PopEmp('frmMain', '', 'dgUMM_ddl_ededit', 'True');"
                                                                    runat="server" />
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Pekerjaan">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgUMM_hid_jb" Value='<%# Container.DataItem("job") %>' runat="Server" />
                                                                <GG:AutoCompleteDropDownList ID="dgUMM_ddl_jb" Width="270px" OnSelectedIndexChanged="dgRW_ddl_jb_OnSelectedIndexChanged"
                                                                    AutoPostBack="true" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="HK">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgUMM_txt_hk" Text='<%# Container.DataItem("hkjob") %>' Width="30px"
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Premi">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgUMM_lbl_pr" Width="50px" runat="server" />
                                                                <asp:HiddenField ID="dgUMM_hid_id" Value='<%# Container.DataItem("id") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgUMM_hid_idx" Value='<%# Container.DataItem("idx") %>' runat="Server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" CommandName="Delete" Text="Delete" runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <%--<asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>--%>
                                                                <asp:LinkButton ID="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation="False"
                                                                    runat="server" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" style="width: 185px">
                                                <asp:ImageButton ID="Btn_UMM_add" runat="server" AlternateText="+ Karyawan" ImageUrl="../../images/butt_add.gif" /></td>
                                            <td style="width: 378px">
                                                &nbsp;</td>
                                            <td style="width: 29px">
                                                &nbsp;</td>
                                            <td style="width: 192px">
                                                &nbsp;</td>
                                            <td style="width: 200px">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>
                            <igtab:TabSeparator>
                            </igtab:TabSeparator>
                            <%--Bahan--%>
                            <igtab:Tab Key="BHN" Text="Bahan" Tooltip="Pemakaian bahan">
                                <ContentPane>
                                    <table border="0" class="mb-c" cellspacing="1" cellpadding="1" width="99%">
                                        <tr>
                                            <td colspan="5">
                                                <asp:DataGrid ID="dgBHN" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                    CellPadding="2" Width="100%" OnItemDataBound="dgBHN_BindGrid" OnCancelCommand="dgBHN_Cancel" OnDeleteCommand="dgBHN_Delete">
                                                    <AlternatingItemStyle CssClass="mr-r" />
                                                    <ItemStyle CssClass="mr-l" />
                                                    <HeaderStyle CssClass="mr-h" />
                                                    <Columns>
                                                        <%--<asp:TemplateColumn HeaderText="No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="no" Width="10px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateColumn>--%>
                                                        <asp:TemplateColumn HeaderText="Pekerjaan">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgBHN_hid_jb" Value='<%# Container.DataItem("job") %>' runat="Server" />
                                                                <GG:AutoCompleteDropDownList ID="dgBHN_ddl_jb" Width="270px" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Bahan">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="dgBHN_hid_itm" Value='<%# Container.DataItem("codeitem") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgBHN_hid_uom" Value='<%# Container.DataItem("uom") %>' runat="Server" />
                                                                <GG:AutoCompleteDropDownList ID="dgBHN_ddl_itm" Width="270px" OnSelectedIndexChanged="dgBHN_ddl_itm_OnSelectedIndexChanged" AutoPostBack=true runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Qty">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dgBHN_txt_Qty" Text='<%# Container.DataItem("Qty") %>' Width="50px"
                                                                    onkeypress="javascript:return isNumberKey(event)" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="UOM">
                                                            <ItemTemplate>
                                                                <asp:Label ID="dgBHN_lbl_um" Text='<%# Container.DataItem("uom") %>' Width="20px"
                                                                    runat="server" />
                                                                <asp:HiddenField ID="dgBHN_hid_id" Value='<%# Container.DataItem("id") %>' runat="Server" />
                                                                <asp:HiddenField ID="dgBHN_hid_idx" Value='<%# Container.DataItem("idx") %>' runat="Server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" CommandName="Delete" Text="Delete" runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation="False"
                                                                    runat="server" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" style="width: 185px">
                                                <asp:ImageButton ID="Btn_BHN_add" OnClick="BHN_add_onClick" runat="server" AlternateText="+ Pemakaian Bahan"
                                                    ImageUrl="../../images/butt_add.gif" /></td>
                                            <td style="width: 378px">
                                                &nbsp;</td>
                                            <td style="width: 29px">
                                                &nbsp;</td>
                                            <td style="width: 192px">
                                                &nbsp;</td>
                                            <td style="width: 200px">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>
                        </Tabs>
                    </igtab:UltraWebTab>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <hr style="width :100%" />   
                </td>
            </tr>
            <tr>
                <td colspan="5" style="height: 26px">
                    <asp:ImageButton ID="NewBtn" OnClick="BtnNewBK_OnClick" ImageUrl="../../images/butt_new.gif"
                        AlternateText="New" runat="server" />
                    <asp:ImageButton ID="SaveBtn" OnClick="BtnSaveBK_OnClick" ImageUrl="../../images/butt_save.gif"
                        AlternateText="Save" runat="server" />
                    <asp:ImageButton ID="BackBtn" AlternateText="  Back  " OnClick="BtnBackBK_onClick"
                        CausesValidation="False" ImageUrl="../../images/butt_back.gif" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
        &nbsp; &nbsp;<input type="hidden" id="isNew" value="" runat="server" />
        <input type="hidden" id="hid_cat" value="" runat="server" />
        <input type="hidden" id="hid_subcat" value="" runat="server" />
        <input type="hidden" id="hid_div" value="" runat="server" />
        <input type="hidden" id="txt_hid_emp" value="" runat="server" />
        </div>
        </td>
        </tr>
        </table>
    </form>
</body>
</html>

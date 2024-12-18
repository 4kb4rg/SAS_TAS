<%@ Page Language="vb" src="../include/menu_hrstp.aspx.vb" Inherits="menu_hrstp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GG-Menu</title>
    
    <link href="include/css/MenuStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="/en/include/script/jscript.js" type="text/jscript"></script>

</head>
    <body bgcolor="black" style="padding-right: 0px; padding-left: 0px; margin-left: 0px; margin-right: 0px" >
    <form id="form1" runat="server">
         
           <div id="Nav" style="position:absolute; width:20%; top:0px; left:0px; height:1000px">
            	
            		<table>
			    <tr height="20">
			    <td width="20"></td>
			   </tr>
			</table> 

  


						<table id="tlbStpHRHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpHR);javascript:togglebox1(tlbStpPR);javascript:togglebox1(tlbStpHR_Estate);javascript:togglebox1(tlbStpPR_Estate);">Human Resources</A></td>
							</tr>
						</table>
						<table id="tlbStpHR" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR01" runat="server" NavigateUrl="/en/HR/setup/HR_setup_POH.aspx" target="middleFrame" text="Point Of Hired" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR02" runat="server" NavigateUrl="/en/HR/setup/HR_setup_DeptCode.aspx" target="middleFrame" text="Departement Code" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR03" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Deptlist.aspx" target="middleFrame" text="Departement" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR04" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Nationality.aspx" target="middleFrame" text="Nationality" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR05" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Function.aspx" target="middleFrame" text="Function" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR06" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Position.aspx" target="middleFrame" text="Position" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR07" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Level.aspx" target="middleFrame" text="Level" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR08" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Religion.aspx" target="middleFrame" text="Religion" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR09" runat="server" NavigateUrl="/en/HR/setup/HR_setup_ICType.aspx" target="middleFrame" text="IC Type" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR10" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Race.aspx" target="middleFrame" text="Race" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR11" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Skill.aspx" target="middleFrame" text="Skill" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR12" runat="server" NavigateUrl="/en/HR/setup/HR_setup_ShiftList.aspx" target="middleFrame" text="Shift" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR13" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Qualification.aspx" target="middleFrame" text="Qualification" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR14" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Subject.aspx" target="middleFrame" text="Subject" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR15" runat="server" NavigateUrl="/en/HR/setup/HR_setup_Evaluation.aspx" target="middleFrame" text="Evaluation" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR16" runat="server" NavigateUrl="/en/HR/setup/HR_setup_CPlist.aspx" target="middleFrame" text="Riwayat Pekerjaan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR17" runat="server" NavigateUrl="/en/HR/setup/HR_setup_SalScheme.aspx" target="middleFrame" text="Employee Category" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR18" runat="server" NavigateUrl="/en/HR/setup/HR_setup_SalGradeList.aspx" target="middleFrame" text="Salary Grade" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR19" runat="server" NavigateUrl="/en/HR/setup/HR_setup_BankFormat.aspx" target="middleFrame" text="Bank Format" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR20" runat="server" NavigateUrl="/en/HR/setup/HR_setup_BankList.aspx" target="middleFrame" text="Bank" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR21" runat="server" NavigateUrl="/en/HR/setup/HR_setup_TaxBranchlist.aspx" target="middleFrame" text="Tax Branch" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR22" runat="server" NavigateUrl="/en/HR/setup/HR_setup_TaxList.aspx" target="middleFrame" text="Tax" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR23" runat="server" NavigateUrl="/en/HR/setup/HR_setup_JamsostekList.aspx" target="middleFrame" text="Jamsostek" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR24" runat="server" NavigateUrl="/en/HR/setup/HR_setup_GPH.aspx" target="middleFrame" text="Public Holiday" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
			
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR25" runat="server" NavigateUrl="/en/HR/setup/HR_setup_HSList.aspx" target="middleFrame" text="Holiday Schedule" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR26" runat="server" NavigateUrl="/en/HR/setup/HR_setup_GangList.aspx" target="middleFrame" text="Gang" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpHR27" runat="server" NavigateUrl="/en/HR/setup/HR_setup_ContractorSuperList.aspx" target="middleFrame" text="Contractor Supervision" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
						</table>

					<table id="tlbSpc1" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						


						<table id="tlbStpPRHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox1(tlbStpHR);javascript:togglebox(tlbStpPR);javascript:togglebox1(tlbStpHR_Estate);javascript:togglebox1(tlbStpPR_Estate);">Payroll</A></td>
							</tr>
						</table>
						<table id="tlbStpPR" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR01" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_ADGroup.aspx" target="middleFrame" text="Allowance And Deduction Group"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR02" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_ADList.aspx" target="middleFrame" text="Allowance And Deduction Code"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR03" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_Load.aspx" target="middleFrame" text="Load" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR04" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_Route.aspx" target="middleFrame" text="Route" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR05" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_AttdList.aspx" target="middleFrame" text="Attendance Code" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR06" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_AirBusList.aspx" target="middleFrame" text="Air Fare/Bus Ticket" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR07" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_DendaList.aspx" target="middleFrame" text="Denda" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR08" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_HarvIncList.aspx" target="middleFrame" text="Harvesting Incentive Scheme"
										cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR09" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_Contractor.aspx" target="middleFrame" text="Contrator" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR10" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_RiceRationList.aspx" target="middleFrame" text="Catu Beras" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR11" runat="server"  NavigateUrl="/en/PR/Setup/PR_Setup_IncentiveList.aspx" target="middleFrame" text="Premi Kerajinan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR12" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_MedicalList.aspx" target="middleFrame" text="Kesehatan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR13" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_MaternityList.aspx" target="middleFrame" text="Kehamilan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR14" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_Payroll.aspx" target="middleFrame" text="Payroll Process" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR15" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_DanaPensiunList.aspx" target="middleFrame" text="Pensiunan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR16" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_RelocationList.aspx" target="middleFrame" text="Relocation" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR17" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_EmpEvalList.aspx" target="middleFrame" text="Employee Evaluation" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR18" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_StdEvalList.aspx" target="middleFrame" text="Standard Evaluation" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="lnkStpPR19" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_SalIncList.aspx" target="middleFrame" text="Salary Increase" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
						</table>
						
						<table id="Table3" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						
						
						<table id="tlbStpHRHead_Estate" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpHR_Estate);javascript:togglebox1(tlbStpPR_Estate);">Human Resources-Estate</A></td>
							</tr>
						</table>
						<table id="tlbStpHR_Estate" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							
							
							<%--Religon--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink13" runat="server" NavigateUrl="/en/HR/setup/HR_Setup_Religion.aspx" target="middleFrame" text="Agama" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
											
						    <%--ICType--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink9" runat="server" NavigateUrl="/en/HR/setup/HR_setup_ICType.aspx" target="middleFrame" text="Tipe ID-Card" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--EmpType--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink5" runat="server" NavigateUrl="/en/HR/setup/HR_setup_EmpType_Estate.aspx" target="middleFrame" text="Tipe Karyawan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
									
							<%--Group Pekerjaan--%>						
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink6" runat="server" NavigateUrl="/en/HR/setup/HR_setup_JobGroup_Estate.aspx" target="middleFrame" text="Group Jabatan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
									
							<%--Jabatan--%>						
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink19" runat="server" NavigateUrl="/en/HR/setup/HR_Setup_Jabatan_Estate.aspx" target="middleFrame" text="Jabatan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>	
							
							<%--KPP--%>						
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink34" runat="server" NavigateUrl="/en/GL/setup/TX_Setup_Kpp.aspx" target="middleFrame" text="Kantor Pelayanan Pajak" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>	
					
						</table>

						<table id="tlbSpc3" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						
						<table id="tlbStpPRHead_Estate" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox1(tlbStpHR_Estate);javascript:togglebox(tlbStpPR_Estate);">Payroll-Estate</A></td>
							</tr>
						</table>
						<table id="tlbStpPR_Estate" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
				
				
							<%--Divisi Code--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink1" runat="server" NavigateUrl="/en/PR/setup/PR_setup_DivCode_Estate.aspx" target="middleFrame" text="Divisi" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--Divisi Code--%>
							<%--
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink40" runat="server" NavigateUrl="/en/PR/setup/PR_setup_DivAsisten_Estate.aspx" target="middleFrame" text="Divisi Asisten-Mandor 1" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							--%>
												    
						    <%--Year Plan--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink2" runat="server" NavigateUrl="/en/PR/setup/PR_setup_YearPlan_Estate.aspx" target="middleFrame" text="Tahun Tanam" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
						
						    <%--Blok--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink12" runat="server" NavigateUrl="/en/PR/setup/PR_setup_BlokList_Estate.aspx" target="middleFrame" text="Blok" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--BJR--%>
							<%--
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink3" runat="server" NavigateUrl="/en/PR/setup/PR_setup_BlokBJRList_Estate.aspx" target="middleFrame" text="Bjr" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							--%>
							
				
				            <%--Daftar Gol SKU-Bulanan--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink8" runat="server" NavigateUrl="/en/PR/setup/PR_setup_GolList_Estate.aspx" target="middleFrame" text="Gol Bulanan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
									
							<%--Status Gol Karyawan,UMP--%>		
						    <tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink10" runat="server" NavigateUrl="/en/PR/setup/PR_setup_SalList_Estate.aspx" target="middleFrame" text="Upah Karyawan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							
							<%--Bonus--%>		
						    <%--<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink20" runat="server" NavigateUrl="/en/PR/setup/PR_setup_SalaryList.aspx" target="middleFrame" text="Alokasi Pekerjaan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>--%>
							
							
							<%--Tanggungn code--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink11" runat="server" NavigateUrl="/en/PR/setup/PR_Setup_TgCode_Estate.aspx" target="middleFrame" text="Kode Tanggungan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
					
					        <%--Beras rate--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink14" runat="server" NavigateUrl="/en/PR/setup/PR_setup_BerasRt_Estate.aspx" target="middleFrame" text="Harga Beras" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
				
					        <%--premi Beras--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink32" runat="server" NavigateUrl="/en/PR/Setup/PR_setup_PrmBrsList_Estate.aspx" target="middleFrame" text="Premi Beras" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
				
				            <%--Astek--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink28" runat="server" NavigateUrl="/en/PR/Setup/PR_setup_AstekList_Estate.aspx" target="middleFrame" text="Astek" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--cut off periode--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink29" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_CutOff_Estate.aspx" target="middleFrame" text="Cut-Off Gaji" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--attendace code--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink30" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_AttCode_Estate.aspx" target="middleFrame" text="Kode Absensi" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--attendace setting--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink31" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_AttdList_Estate.aspx" target="middleFrame" text="Setting Kode Absensi" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--jamkerja--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink7a" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_JamKerja_Estate.aspx" target="middleFrame" text="Jam Kerja" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							
							<%--holiday--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink7" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_Holiday_Estate.aspx" target="middleFrame" text="Hari Libur" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							
							<%--Hari Pengganti--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink7_Chg" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_Hari_Pengganti_Estate.aspx" target="middleFrame" text="Setting Hari Pengganti" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--overtime setting --%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink33" runat="server" NavigateUrl="/en/PR/Setup/PR_setup_OTList_Estate.aspx" target="middleFrame" text="Tarif Lembur" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--premi golongan setting --%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink4" runat="server" NavigateUrl="/en/PR/Setup/PR_setup_PrmiPanenList_Estate.aspx" target="middleFrame" text="Tabel Basis & Premi Panen" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--denda setting --%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink15" runat="server" NavigateUrl="/en/PR/Setup/PR_setup_DendaList_Estate.aspx" target="middleFrame" text="Denda Panen" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--premi brondolan 
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink16" runat="server" NavigateUrl="/en/PR/Setup/PR_setup_PrmBrondolList_Estate.aspx" target="middleFrame" text="Premi Brondolan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							--%>
							
							<%--premi kerajinan setting --%>
							<%--
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink17" runat="server" NavigateUrl="/en/PR/Setup/PR_setup_PrmiKrjnnList_Estate.aspx" target="middleFrame" text="Premi Kerajinan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							--%>
							
							<%--premi supir setting --%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink20" runat="server" NavigateUrl="/en/PR/Setup/PR_setup_PrmiDriver_Estate.aspx" target="middleFrame" text="Premi Supir" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--premi mandor --%>
							<%--
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink18" runat="server" NavigateUrl="/en/PR/Setup/PR_setup_PrmiMndorList_Estate.aspx" target="middleFrame" text="Premi Mandor" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							--%>
							
							<%--premi deres --%>
							<%--
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink23" runat="server" NavigateUrl="/en/PR/Setup/PR_setup_PrmKaretList_Estate.aspx" target="middleFrame" text="Premi Deres" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							--%>
							
							<%--premi lain --%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink202" runat="server" NavigateUrl="/en/PR/Setup/PR_setup_PrmiLain_Estate.aspx" target="middleFrame" text="Premi Lain" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--jenis kendaraan --%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpFI09" runat="server" target="middleFrame" NavigateUrl="/en/PR/Setup/PR_Setup_Vehicle_Estate.aspx" text="Jenis Kenderaan"></asp:hyperlink></td>
							</tr>
							
						   												
							<%--Alokasi Pekerjaan--%>		
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink26" runat="server" NavigateUrl="/en/PR/setup/PR_Setup_Aktivitikategori_Estate.aspx" target="middleFrame" text="Kategori Aktiviti" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink27" runat="server" NavigateUrl="/en/PR/setup/PR_Setup_AktivitiSubkategori_Estate.aspx" target="middleFrame" text="Sub Kategori Aktiviti" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink22" runat="server" NavigateUrl="/en/PR/setup/PR_setup_AktivitiList_Estate.aspx" target="middleFrame" text="Aktiviti" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink24" runat="server" NavigateUrl="/en/PR/setup/PR_setup_Borongan_Estate.aspx" target="middleFrame" text="Borongan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
						    <tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink25" runat="server" NavigateUrl="/en/PR/setup/PR_setup_ComponentGajiList_Estate.aspx" target="middleFrame" text="Komponen Gaji" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							  <tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink25a" runat="server" NavigateUrl="/en/PR/setup/PR_setup_Tunjangan_Estate.aspx" target="middleFrame" text="Tunjangan Gaji" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink21" runat="server" NavigateUrl="/en/PR/setup/PR_setup_PPH21List_Estate.aspx" target="middleFrame" text="PPH21" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
						
						
						</table>

						<table id="Table2" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						
						
						<table id="tlbStpHRHead_MILL" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tlbStpHR_MILL);javascript:togglebox1(tlbStpPR_MILL);">Human Resources-HO/MILL</A></td>
							</tr>
						</table>
						<table id="tlbStpHR_MILL" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
							
							
							<%--Religon--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML13" runat="server" NavigateUrl="/en/HR/setup/HR_Setup_Religion.aspx" target="middleFrame" text="Agama" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
											
						    <%--ICType--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML9" runat="server" NavigateUrl="/en/HR/setup/HR_setup_ICType.aspx" target="middleFrame" text="Tipe ID-Card" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--EmpType--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML5" runat="server" NavigateUrl="/en/HR/setup/HR_setup_EmpType_Estate.aspx" target="middleFrame" text="Tipe Karyawan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
									
							<%--Group Pekerjaan--%>						
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML6" runat="server" NavigateUrl="/en/HR/setup/HR_setup_JobGroup_Estate.aspx" target="middleFrame" text="Group Jabatan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
									
							<%--Jabatan--%>						
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML19" runat="server" NavigateUrl="/en/HR/setup/HR_Setup_Jabatan_Estate.aspx" target="middleFrame" text="Jabatan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>	
							
							<%--KPP--%>						
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML34" runat="server" NavigateUrl="/en/GL/setup/TX_Setup_Kpp.aspx" target="middleFrame" text="Kantor Pelayanan Pajak" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>	
					
						</table>

						<table id="tlbSpc4" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr>
								<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
							</tr>
						</table>
						
						<table id="tlbStpPRHead_MILL" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<tr height="20">
								<td width="20"></td>
								<td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
								<td class="lb-hti"><A class="lb-tti" href="javascript:togglebox1(tlbStpHR_MILL);javascript:togglebox(tlbStpPR_MILL);">Payroll-HO/MILL</A></td>
							</tr>
						</table>
						<table id="tlbStpPR_MILL" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
							width="100%" border="0" runat="server">
				
				            <%--Daftar Gol SKU-Bulanan--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML8" runat="server" NavigateUrl="/en/PR/setup/PR_setup_GolList_Estate.aspx" target="middleFrame" text="Gol Bulanan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
									
							<%--Status Gol Karyawan,UMP--%>		
						    <tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML10" runat="server" NavigateUrl="/en/PR/setup/PR_setup_SalList_Estate.aspx" target="middleFrame" text="Upah Karyawan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							
							<%--Tanggungn code--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML11" runat="server" NavigateUrl="/en/PR/setup/PR_Setup_TgCode_Estate.aspx" target="middleFrame" text="Kode Tanggungan" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
					
					        <%--Beras rate--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML14" runat="server" NavigateUrl="/en/PR/setup/PR_setup_BerasRt_Estate.aspx" target="middleFrame" text="Harga Beras" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
				
					        <%--premi Beras--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML32" runat="server" NavigateUrl="/en/PR/Setup/PR_setup_PrmBrsList_Estate.aspx" target="middleFrame" text="Premi Beras" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
				
				            <%--Astek--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML28" runat="server" NavigateUrl="/en/PR/Setup/PR_setup_AstekList_Estate.aspx" target="middleFrame" text="Astek" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--cut off periode--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML29" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_CutOff_Estate.aspx" target="middleFrame" text="Cut-Off Gaji" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--attendace code--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML30" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_AttCode_Estate.aspx" target="middleFrame" text="Kode Absensi" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--attendace setting--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML31" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_AttdList_Estate.aspx" target="middleFrame" text="Setting Kode Absensi" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--jamkerja--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML7a" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_JamKerja_Estate.aspx" target="middleFrame" text="Jam Kerja" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--holiday--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML7" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_Holiday_Estate.aspx" target="middleFrame" text="Hari Libur" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--Hari Pengganti--%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML7_Chg" runat="server" NavigateUrl="/en/PR/Setup/PR_Setup_Hari_Pengganti_Estate.aspx" target="middleFrame" text="Setting Hari Pengganti" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--overtime setting --%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML33" runat="server" NavigateUrl="/en/PR/Setup/PR_setup_OTList_Estate.aspx" target="middleFrame" text="Tarif Lembur" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--premi lain --%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="Hyperlink202b" runat="server" NavigateUrl="/en/PR/Setup/PR_setup_PrmiLain_Estate.aspx" target="middleFrame" text="Premi Lain" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<%--premi supir setting --%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML20" runat="server" NavigateUrl="/en/PR/Setup/PR_setup_PrmiDriver_Estate.aspx" target="middleFrame" text="Premi Supir" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							
							<%--jenis kendaraan --%>
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink class="lb-mt" id="lnkStpML09" runat="server" target="middleFrame" NavigateUrl="/en/PR/Setup/PR_Setup_Vehicle_Estate.aspx" text="Jenis Kenderaan"></asp:hyperlink></td>
							</tr>
							
						   												
							<%--Alokasi Pekerjaan--%>		
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML26" runat="server" NavigateUrl="/en/PR/setup/PR_Setup_Aktivitikategori_Estate.aspx" target="middleFrame" text="Kategori Aktiviti" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML27" runat="server" NavigateUrl="/en/PR/setup/PR_Setup_AktivitiSubkategori_Estate.aspx" target="middleFrame" text="Sub Kategori Aktiviti" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML22" runat="server" NavigateUrl="/en/PR/setup/PR_setup_AktivitiList_Estate.aspx" target="middleFrame" text="Aktiviti" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							
						    <tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML25" runat="server" NavigateUrl="/en/PR/setup/PR_setup_ComponentGajiList_Estate.aspx" target="middleFrame" text="Komponen Gaji" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							  <tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML25a" runat="server" NavigateUrl="/en/PR/setup/PR_setup_Tunjangan_Estate.aspx" target="middleFrame" text="Tunjangan Gaji" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
							
							<tr height="20">
								<td width="20"></td>
								<td width="14">&nbsp;<IMG src="images/dot.gif" border="0"></td>
								<td class="lb-mti"><asp:hyperlink id="HyperlinkML21" runat="server" NavigateUrl="/en/PR/setup/PR_setup_PPH21List_Estate.aspx" target="middleFrame" text="PPH21" cssclass="lb-mt"></asp:hyperlink></td>
							</tr>
						
						
						</table>



            </div>
         
             <div style="position:absolute; top:0px; width:85%; left:179px; height:1000px" >
          
              	<iframe id="Iframe1" name="middleFrame"  style="width:100%; height:100%; background-color:Black"
				 scrolling="auto" src="black.aspx"></iframe>
             
               </div>
            
         
           <div class="BackgroundTopCorner"></div>
        </form>
           

</body>
</html>


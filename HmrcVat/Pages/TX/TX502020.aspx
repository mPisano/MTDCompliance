<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" 
    CodeFile="TX502020.aspx.cs" Inherits="Page_TX502020" Title="Untitled Page"  %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.HMRC.VATMaint" PrimaryView="Period_Header" PageLoadBehavior="PopulateSavedValues">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="SignInHMRC" PostData="Self"/>
            <px:PXDSCallbackCommand Name="SendVATReturn"  Visible="true" CommitChanges="true" PostData="Self" />
            <px:PXDSCallbackCommand Name="CheckVATReturn" Visible="false" CommitChanges="true" PostData="Self" />
            <px:PXDSCallbackCommand Name="ViewTaxDocument" Visible="false" PostData="Self"  DependOnGrid="gridDetails" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" Width="100%" DataMember="Period_Header" Caption="Selection" TabIndex="4500">
        <Template>
            <px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" GroupCaption="TAX PERIOD"/>
            <px:PXSegmentMask ID="edOrganizationID" runat="server" DataField="OrganizationID" AllowEdit="true" CommitChanges="true" />
            <px:PXSegmentMask ID="edBranchID" runat="server" DataField="BranchID" AllowEdit="true" CommitChanges="true" AutoRefresh="True"/>
            <px:PXSegmentMask ID="edVendorID" runat="server" DataField="VendorID" AllowEdit="true" CommitChanges="true" />
            <px:PXSelector Size="S" ID="edTaxPeriodID" runat="server" DataField="TaxPeriodID" CommitChanges="true" />
            <px:PXLayoutRule runat="server" Merge="True" />
            <px:PXSelector ID="RevisionId" runat="server" DataField="RevisionId" Size="s" AutoRefresh="true" CommitChanges="true" />
            <px:PXLayoutRule Merge="False" runat="server" />
            <px:PXDateTimeEdit ID="edStartDate" runat="server" DataField="StartDate" />
            <px:PXDateTimeEdit ID="edEndDate" runat="server" DataField="EndDate" />
			
            <px:PXLayoutRule ID="PXLayoutRule2" runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" GroupCaption="HMRC TAX PERIOD" />

            <px:PXSelector ID="edPeriodKey" runat="server" DataField="PeriodKey" Enabled="True" Size="s" CommitChanges="true"/>
            <px:PXDateTimeEdit ID="edStart" runat="server" DataField="Start" Enabled="False" />
            <px:PXDateTimeEdit ID="edEnd" runat="server" DataField="End" Enabled="False" />
            <px:PXDateTimeEdit ID="edDue" runat="server" DataField="Due" Enabled="False" />
			<px:PXDropDown ID="edStatus" runat="server" DataField="Status" Enabled="False" />
            <px:PXDateTimeEdit ID="edReceived" runat="server" DataField="Received" Enabled="False" />                
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Width="100%" Height="150px" NoteIndicator="True" FilesIndicator="True" DataMember="Period_Details">
        <Items>
            <px:PXTabItem Text="VAT Return">
                <Template>
                    <px:PXGrid ID="gridDetails" runat="server" Height="150px" Width="100%" ActionsPosition="Top" SkinID="Inquire">
                        <ActionBar>
							<Actions>
								<EditRecord ToolBarVisible="False" />
								<Delete ToolBarVisible="False"/>
								<AddNew ToolBarVisible="False"/>
							</Actions>
                        </ActionBar>
                        <Levels>
                            <px:PXGridLevel DataMember="Period_Details">
                                <Columns>
                                    <px:PXGridColumn DataField="LineNbr" TextAlign="Left" Width="64px" AllowShowHide="True" />
                                    <px:PXGridColumn DataField="SortOrder" TextAlign="Left" Width="64px" AllowShowHide="True"/>
                                    <px:PXGridColumn DataField="ReportLineNbr" TextAlign="Left" Width="64px" />                                    
                                    <px:PXGridColumn DataField="Descr" Width="360px" />
                                    <px:PXGridColumn DataField="TaxHistoryReleased__ReportFiledAmt" TextAlign="Right" Width="81px" LinkCommand="ViewTaxDocument" />
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Submitted VAT Return">
                <Template>
                    <px:PXGrid ID="gridVATRows" runat="server" Height="150px" Width="100%" ActionsPosition="Top" SkinID="Inquire">
                        <ActionBar>
							<Actions>
								<EditRecord ToolBarVisible="False" />
								<Delete ToolBarVisible="False"/>
								<AddNew ToolBarVisible="False"/>
							</Actions>
                            <CustomItems>
                                <px:PXToolBarButton Text="Retrieve VAT return" Tooltip="Retrieve VAT return">
                                    <AutoCallBack Command="CheckVATReturn" Target="ds">
                                        <Behavior CommitChanges="True" />
                                    </AutoCallBack>
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                        <Levels>
                            <px:PXGridLevel DataMember="VATRows">
                                <Columns>
                                    <px:PXGridColumn DataField="TaxBoxNbr" TextAlign="Left" Width="64px" />
                                    <px:PXGridColumn DataField="Descr" Width="360px" />
                                    <px:PXGridColumn DataField="Amt" TextAlign="Right" Width="81px" />
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Container="Window" Enabled="True" />
    </px:PXTab>
</asp:Content>

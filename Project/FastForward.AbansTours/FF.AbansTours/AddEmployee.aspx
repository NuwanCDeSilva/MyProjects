<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEmployee.aspx.cs" Inherits="FF.AbansTours.AddEmployee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upReceiptEntry" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
        <ContentTemplate>
            <div style="height: 22px; width: 100%; text-align: right; background-color: Silver;">
                <asp:Button ID="btnSave" Text="Save" runat="server" Width="80px" Enabled="true"
                    ValidationGroup="Save" OnClick="btnSave_Click" />
                <asp:Button ID="btnUpdate" Text="Update" runat="server" Width="80px" Enabled="true"
                    ValidationGroup="Save" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnClear" Text="Clear" runat="server" Width="80px" OnClick="btnClear_Click" />
                <asp:ConfirmButtonExtender ID="CbeSave" runat="server" TargetControlID="btnSave"
                    ConfirmText="Do you want to save?" ConfirmOnFormSubmit="false">
                </asp:ConfirmButtonExtender>
                <asp:ConfirmButtonExtender ID="CbeUpdate" runat="server" TargetControlID="btnUpdate"
                    ConfirmText="Do you want to Update?" ConfirmOnFormSubmit="false">
                </asp:ConfirmButtonExtender>
                <asp:ConfirmButtonExtender ID="CbeClear" runat="server" TargetControlID="btnClear"
                    ConfirmText="Do you want to clear?" ConfirmOnFormSubmit="false">
                </asp:ConfirmButtonExtender>
            </div>
            <div class="col-md-12">
                &nbsp;
            </div>
            <div>
                <div class="row rowmargin0 col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading pannelheading">
                            Add Employee
                        </div>
                        <div class="panel-body paddingleft0 paddingright30">
                            <div class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                    Employee Code
                                </div>
                                <div class="col-md-2 padding5">
                                    <div class="col-md-8 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtEPFNo" runat="server" CssClass="textbox" OnTextChanged="txtEPFNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="EPF No is required."
                                            Display="None" CssClass="Validators" ControlToValidate="txtEPFNo" ValidationGroup="EPFNo"></asp:RequiredFieldValidator>
                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                                        </asp:ValidatorCalloutExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="EPF No is required."
                                            Display="None" CssClass="Validators" ControlToValidate="txtEPFNo" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="RequiredFieldValidator9">
                                        </asp:ValidatorCalloutExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="EPF No is required."
                                            Display="None" CssClass="Validators" ControlToValidate="txtEPFNo" ValidationGroup="ProfitCenter"></asp:RequiredFieldValidator>
                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" TargetControlID="RequiredFieldValidator12">
                                        </asp:ValidatorCalloutExtender>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="imgbtnEmployeeCode" runat="server" ImageUrl="~/Images/icon_search.png"
                                            ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtnEmployeeCode_Click" />
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="imgbtnEmployeeCodeSearch" runat="server" ImageUrl="~/Images/Details.png"
                                            ValidationGroup="ReceiptNo" ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtnEmployeeCodeSearch_Click" />
                                    </div>
                                </div>
                                <div class="col-md-2 padding3">
                                </div>
                                <div class="col-md-2 padding5 ">
                                </div>
                                <div class="col-md-2 padding3">
                                </div>
                                <div class="col-md-2 padding5">
                                </div>
                            </div>
                            <div class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                    Title
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:DropDownList ID="ddlTitle" runat="server" CssClass="textbox ddlhight1">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 padding3">
                                    First Name
                                </div>
                                <div class="col-md-2 padding5 ">
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="textbox"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="First Name is required."
                                        Display="None" CssClass="Validators" ControlToValidate="txtFirstName" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                                <div class="col-md-2 padding3">
                                    Last Name
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="textbox"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Last Name is required."
                                        Display="None" CssClass="Validators" ControlToValidate="txtLastName" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator3" PopupPosition="Left">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                    NIC
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtNIC" runat="server" CssClass="textbox" ToolTip=" eg : 755482369V" AutoPostBack="true" OnTextChanged="txtNIC_TextChanged" MaxLength="10"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtNIC" FilterType="Numbers, Custom" ValidChars="V,v,X,x"></asp:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNIC"
                                        ErrorMessage="NIC Number is required." Display="None" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="RequiredFieldValidator5">
                                    </asp:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Enter valid NIC Number."
                                        ControlToValidate="txtNIC" Display="None" ValidationExpression="[0-9]{9}[v|x|V|X]{1}"
                                        ValidationGroup="Save"></asp:RegularExpressionValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="RegularExpressionValidator3">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                                <div class="col-md-2 padding3">
                                    Date Of Birth
                                </div>
                                <div class="col-md-2 padding0">
                                    <div class="col-md-10 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="textbox" Format="dd/MMM/yyyy"
                                            onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateOfBirth"
                                            PopupButtonID="imgbtnDateOfBirth" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="imgbtnDateOfBirth" runat="server" ImageUrl="~/Images/calendar.png"
                                            ImageAlign="Middle" CssClass="imageicon" />
                                    </div>
                                </div>
                                <div class="col-md-2 padding3">
                                    Date Of Join
                                </div>
                                <div class="col-md-2 padding0">
                                    <div class="col-md-10 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtDateOfJoin" runat="server" CssClass="textbox" Format="dd/MMM/yyyy"
                                            onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDateOfJoin"
                                            PopupButtonID="imgbtnDateOfJoin" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Date Of Join is required."
                                            Display="None" CssClass="Validators" ControlToValidate="txtDateOfJoin" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator4" PopupPosition="Left">
                                        </asp:ValidatorCalloutExtender>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="imgbtnDateOfJoin" runat="server" ImageUrl="~/Images/calendar.png"
                                            ImageAlign="Middle" CssClass="imageicon" />
                                    </div>
                                </div>
                            </div>
                            <div class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                    Address1
                                </div>
                                <div class="col-md-10 padding5">
                                    <asp:TextBox ID="txtAddress1" runat="server" CssClass="textbox"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Address1 is required."
                                        Display="None" CssClass="Validators" ControlToValidate="txtAddress1" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RequiredFieldValidator6" PopupPosition="Left">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                    Address2
                                </div>
                                <div class="col-md-6 padding5">
                                    <asp:TextBox ID="txtAddress2" runat="server" CssClass="textbox"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Address2 is required." 
                                        Display="None" CssClass="Validators" ControlToValidate="txtAddress2" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="RequiredFieldValidator7">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                    Address3
                                </div>
                                <div class="col-md-6 padding5">
                                    <asp:TextBox ID="txtAddress3" runat="server" CssClass="textbox"></asp:TextBox>                                    
                                </div>
                            </div>
                            <div class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                    Phone
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="textbox" ToolTip="eg :+94700000000/0110000000" MaxLength="12"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtPhone" FilterType="Numbers, Custom" ValidChars="+"></asp:FilteredTextBoxExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter valid Phone Number."
                                        ControlToValidate="txtPhone" Display="None" ValidationExpression="[+][0-9]{11}|[0][0-9]{9}"
                                        ValidationGroup="Save"></asp:RegularExpressionValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="RegularExpressionValidator1">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                                <div class="col-md-2 padding3">
                                    Mobile
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="textbox" ToolTip="eg :+94700000000/0710000000" MaxLength="12"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtMobile" FilterType="Numbers, Custom" ValidChars="+"></asp:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Mobile Number is required."
                                        Display="None" CssClass="Validators" ControlToValidate="txtMobile" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="RequiredFieldValidator8">
                                    </asp:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter valid Mobile Number."
                                        ControlToValidate="txtMobile" Display="None" ValidationExpression="[+][0-9]{11}|[0][0-9]{9}"
                                        ValidationGroup="Save"></asp:RegularExpressionValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" TargetControlID="RegularExpressionValidator2">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                                <div class="col-md-2 padding3">
                                </div>
                                <div class="col-md-2 padding5">
                                </div>
                            </div>
                            <div class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                    Employee Source
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:DropDownList ID="ddlEmployeeSource" runat="server" CssClass="textbox ddlhight1">
                                        <asp:ListItem Text="Company" Value="Company"></asp:ListItem>
                                        <asp:ListItem Text="External" Value="External"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 padding3">
                                    Employee Type
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="textbox ddlhight1" OnTextChanged="ddlEmployeeType_TextChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 padding3">
                                    Tourist Board Lic-no 
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtTouristBoardLicNo" runat="server" CssClass="textbox"></asp:TextBox>
                                </div>
                            </div>
                            <div id="LicenseDiv" runat="server" visible="false" class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                    License No
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtLicenseNo" runat="server" CssClass="textbox"></asp:TextBox>
                                </div>
                                <div class="col-md-2 padding3">
                                    License Category
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:DropDownList ID="ddlLicenseCategory" runat="server" CssClass="textbox ddlhight1">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 padding3">
                                    Licence Exp Date
                                </div>
                                <div class="col-md-2 padding0">
                                    <div class="col-md-10 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtLicenceExpDate" runat="server" CssClass="textbox" Format="dd/MMM/yyyy"
                                            onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtLicenceExpDate"
                                            PopupButtonID="imgbtnLicenceExpDate" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="imgbtnLicenceExpDate" runat="server" ImageUrl="~/Images/calendar.png"
                                            ImageAlign="Middle" CssClass="imageicon" />
                                    </div>
                                </div>
                            </div>
                            <div class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                    Contact Person
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtContactPerson" runat="server" CssClass="textbox"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Contact Person is required."
                                        Display="None" CssClass="Validators" ControlToValidate="txtContactPerson" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="RequiredFieldValidator10">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                                <div class="col-md-2 padding3">
                                    Contact Person Mobile
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtContactPersonMobile" runat="server" CssClass="textbox"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtContactPersonMobile" FilterType="Numbers, Custom" ValidChars="+"></asp:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Contact Person Mobile is required."
                                        Display="None" CssClass="Validators" ControlToValidate="txtContactPersonMobile" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="RequiredFieldValidator11">
                                    </asp:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Enter valid Mobile Number."
                                        ControlToValidate="txtContactPersonMobile" Display="None" ValidationExpression="[+][0-9]{11}|[0][0-9]{9}"
                                        ValidationGroup="Save"></asp:RegularExpressionValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" TargetControlID="RegularExpressionValidator4">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                                <div class="col-md-2 padding3">
                                    Status
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="textbox ddlhight1">
                                        <asp:ListItem Text="ACTIVE" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="DEACTIVE" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                    ProfitCenter
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:DropDownList ID="ddlProfitCenter" runat="server" CssClass="textbox ddlhight1">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 padding3">
                                    <asp:Button ID="btnAAddProfitCenter" runat="server" Text="Add" OnClick="btnAAddProfitCenter_Click" ValidationGroup="ProfitCenter" />
                                </div>
                                <div class="col-md-2 padding5">
                                </div>
                                <div class="col-md-2 padding3">
                                </div>
                                <div class="col-md-2 padding5">
                                </div>
                            </div>
                            <div class="row no-margin-left">
                                <div class="col-md-6 padding3 pan Scroll">
                                    <asp:GridView ID="grdProfitCenter" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablemod">
                                        <Columns>
                                            <asp:BoundField DataField='MPE_PC' HeaderText='ProfitCenter' />
                                            <asp:TemplateField ItemStyle-Width="5px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkActive" runat="server" AutoPostBack="true" Checked='<%# Convert.ToBoolean(Eval("MPE_ACT")) %>'
                                                        OnCheckedChanged="ChkActive_CheckedChanged" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-2 padding5">
                                </div>
                                <div class="col-md-2 padding3">
                                </div>
                                <div class="col-md-2 padding5">
                                </div>
                            </div>
                            <%--</div>--%>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ModelsSpecificationsViewer.aspx.cs" Inherits="FastForward.SCMWeb.View.Enquiries.Inventory.ModelsSpecificationsViewer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function ConfirmClearForm() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }

        };
        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };

        function ShowModalPopup() {
            var url = $get("<%=txtUrl.ClientID %>").value;
            // url = url.split('v=')[1];

            if (isValidURL(url) == true) {
                $get("videoItem").src = url;
            } else {
                document.getElementById('BodyContent_txtUrl').value = '';
                showStickyNoticeToast("Please enter the URL Link !");

            }



            return false;
        }

        function isValidURL(url) {
            var RegExp = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;

            if (RegExp.test(url)) {
                return true;
            } else {
                return false;
            }
        }

        function ConfirmDeleteItem() {
            var selectedValue = confirm("Are you sure you want to delete?");
            if (selectedValue) {
                document.getElementById('<% =txtDeleteconformmessageValue.ClientID %>').value = "1";
            }
            else {
                document.getElementById('<% =txtDeleteconformmessageValue.ClientID %>').value = "0";
            }
        };
    </script>


    <script type="text/javascript">

        function showStickySuccessToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'success',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }
            });
        }
        function showStickyNoticeToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'notice',
                closeText: '',
                close: function () { console.log("toast is closed ..."); }
            });
        }
        function showStickyWarningToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'warning',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }

            });

        }
        function showStickyErrorToast(value) {

            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'error',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }
            });
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">


    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>

    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
    <asp:HiddenField ID="hdfTabIndex" runat="server" />
    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-8  buttonrow">
                        </div>
                        <div class="col-sm-4  buttonRow">

                            <div class="col-sm-6">
                            </div>
                            <div class="col-sm-3 paddingRight0">
<%--                                <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="lbtnSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                </asp:LinkButton>--%>
                            </div>

                            <div class="col-sm-3">
                                <asp:LinkButton ID="lblUClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm()" OnClick="lblUClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-default">
                        <div class="panel-heading paddingtopbottom0" style="font-weight: bolder">
                           Product Information
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-3 paddingLeft0">
                                <div class="panel panel-default">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-4 labelText1 paddingLeft5">
                                                            Model
                                                        </div>
                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                            <asp:TextBox ID="txtModel" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" TabIndex="110" OnTextChanged="txtModel_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                            <asp:LinkButton ID="lbtnSearchModel" runat="server" TabIndex="111" OnClick="lbtnSearchModel_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-4 labelText1 paddingLeft5">
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                        </div>
                                                        <div class="col-sm-6 paddingRight5 labelText1 paddingLeft0">
                                                            <asp:Label ID="lbldes" runat="server"></asp:Label>

                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height10">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-4 labelText1 paddingLeft5">
                                                            Main Category
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                            :
                                                        </div>
                                                        <div class="col-sm-6 paddingRight5 labelText1 paddingLeft0">
                                                            <asp:Label ID="lblMainCat" runat="server"></asp:Label>

                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-2 labelText1 paddingLeft5">
                                                            Sub  
                                                        </div>
                                                        <div class="col-sm-2 labelText1 paddingLeft5">
                                                            Level 1 
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                            :
                                                        </div>
                                                        <div class="col-sm-6 paddingRight5 labelText1 paddingLeft0">
                                                            <asp:Label ID="lblCat1" runat="server"></asp:Label>

                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-2 labelText1 paddingLeft5">
                                                        </div>
                                                        <div class="col-sm-2 labelText1 paddingLeft5">
                                                            Level 2 
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                            :
                                                        </div>
                                                        <div class="col-sm-6 paddingRight5 labelText1 paddingLeft0">
                                                            <asp:Label ID="lblCat2" runat="server"></asp:Label>

                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-2 labelText1 paddingLeft5">
                                                        </div>
                                                        <div class="col-sm-2 labelText1 paddingLeft5">
                                                            Level 3 
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                            :
                                                        </div>
                                                        <div class="col-sm-6 paddingRight5 labelText1 paddingLeft0">
                                                            <asp:Label ID="lblCat3" runat="server"></asp:Label>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-2 labelText1 paddingLeft5">
                                                        </div>
                                                        <div class="col-sm-2 labelText1 paddingLeft5">
                                                            Level 4 
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                            :
                                                        </div>
                                                        <div class="col-sm-6 paddingRight5 labelText1 paddingLeft0">
                                                            <asp:Label ID="lblCat4" runat="server"></asp:Label>

                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height10">
                                                    </div>
                                                </div>

                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                            </div>

                            <div class="col-sm-9 paddingLeft0">
                                <div class="panel panel-default">

                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-12 ">
                                                <div class="bs-example">
                                                    <ul class="nav nav-tabs" id="myTab">
                                                        <li class="active"><a href="#Specification" data-toggle="tab">Specification</a></li>
                                                        <li><a href="#Images" data-toggle="tab">Images</a></li>
                                                        <li><a href="#Videos" data-toggle="tab">Videos</a></li>
                                                        <li><a href="#Pdf" data-toggle="tab">Product Brochures</a></li>
                                                    </ul>

                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 ">
                                                    <div class="tab-content">
                                                        <div class="tab-pane active" id="Specification">
                                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <%-- <asp:DataList ID="DataList1" runat="server" OnItemDataBound="MyDataList_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <h4>
                                                                                <asp:Label ID="ProductNameLabel" runat="server"
                                                                                    Text='<%# Eval("mct_cls_cat") %>' /></h4>
                                                                            <table border="0">
                                                                                <tr>
                                                                                    <td class="ProductPropertyLabel">Category:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="CategoryNameLabel" runat="server"
                                                                                            Text='<%# Eval("mct_cls_cat") %>' /></td>
                                                                                    <td class="ProductPropertyLabel">Supplier:</td>
                                                                                                                                                                      <td>
                                                                                        <asp:Label ID="Label1" runat="server"
                                                                                            Text='<%# Eval("mct_cls_tp") %>' /></td>
                                                                                </tr>
                                                                              
                                                                            </table>
                                                                        </ItemTemplate>
                                                                    </asp:DataList>--%>

                                                                    <asp:DataList ID="DataList1" runat="server" CssClass="table table-condensed table-condensed" OnItemDataBound="DataList1_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <table border="0">
                                                                                <tr>
                                                                                    <td class="width100">
                                                                                        <h4>
                                                                                            <asp:Label ID="lblCategory" runat="server" Visible="false" Text='<%# Eval("Id") %>'></asp:Label></h4>
                                                                                        <h4>
                                                                                            <asp:Label ID="lblCategoryDes" runat="server" Text='<%# Eval("Name") %>'></asp:Label></h4>
                                                                                    </td>
                                                                                    <td></td>
                                                                                    <td>

                                                                                        <asp:DataList ID="DataList2" CssClass="table table-condensed table-condensed" runat="server" Width="200px">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblCategorytype" runat="server" Visible="false" Text='<%# Eval("mct_cls_tp") %>'></asp:Label>
                                                                                                <asp:Label ID="lblCategorytypeDes" runat="server" Text='<%# Eval("MCT_CLS_TP_DES") %>'></asp:Label>
                                                                                            </ItemTemplate>

                                                                                        </asp:DataList>
                                                                                    </td>
                                                                                    <td>

                                                                                        <asp:DataList ID="DataList3" CssClass="table table-responsive table-responsive width525" runat="server">

                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="txtCategorytypeDes" Text='<%# Eval("MCT_DEF") %>' runat="server" TextMode="MultiLine"></asp:Label>

                                                                                            </ItemTemplate>

                                                                                        </asp:DataList>
                                                                                    </td>

                                                                                </tr>

                                                                            </table>
                                                                        </ItemTemplate>
                                                                    </asp:DataList>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="tab-pane " id="Images">
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                   <%-- <div class="row">
                                                                        <asp:FileUpload ID="FileUpload" runat="server" />
                                                                    </div>--%>
                                                                   <%-- <div class="row height10">
                                                                    </div>--%>
                                                                   <%-- <div class="row">
                                                                        <asp:Button class="btn btn-default btn-sm" ID="btnUpload" runat="server" Text="Upload" OnClick="Upload" />
                                                                        <asp:Button class="btn btn-default btn-sm" ID="Button2" Visible="false" runat="server" Text="Save " />
                                                                    </div>--%>
                                                                    <div class="row">
                                                                        <asp:Image ID="imgsave" runat="server" />
                                                                    </div>

                                                                </div>
                                                                <div class="col-sm-8">
                                                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                        <ContentTemplate>

                                                                            <div class="panel-body  panelscollbar height200">

                                                                                <asp:ListView runat="server" ID="ImageListView" ItemPlaceholderID="itemPlaceHolder"
                                                                                    GroupPlaceholderID="groupPlaceHolder" OnItemCommand="ImageListView_ItemCommand">
                                                                                    <LayoutTemplate>
                                                                                        <h1>
                                                                                            <asp:Label Text="" runat="server" ID="titleLabel" OnLoad="titleLabel_Load" />

                                                                                        </h1>
                                                                                        <div runat="server" id="groupPlaceHolder">
                                                                                        </div>
                                                                                    </LayoutTemplate>
                                                                                    <GroupTemplate>
                                                                                        <span>
                                                                                            <div id="itemPlaceHolder" runat="server"></div>
                                                                                        </span>
                                                                                    </GroupTemplate>
                                                                                    <ItemTemplate>

                                                                                        <asp:ImageButton ID="itemImageButton" runat="server"
                                                                                            CommandArgument="<%# Container.DataItem %>"
                                                                                            ImageUrl="<%# Container.DataItem %>" Width="150" Height="150"
                                                                                            OnCommand="itemImageButton_Command" />
<%--                                                                                        <asp:LinkButton ID="deleteLinkButton" runat="server" CommandName="Remove"
                                                                                            CommandArgument="<%# Container.DataItem %>" Text="Delete" Visible="true" OnClientClick="ConfirmDeleteItem()"
                                                                                            OnLoad="deleteLinkButton_Load" />--%>

                                                                                    </ItemTemplate>
                                                                                    <EmptyItemTemplate>
                                                                                        <td />
                                                                                    </EmptyItemTemplate>
                                                                                    <EmptyDataTemplate>
                                                                                        <h3>No images available</h3>
                                                                                    </EmptyDataTemplate>
                                                                                    <InsertItemTemplate>
                                                                                    </InsertItemTemplate>
                                                                                </asp:ListView>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>


                                                        </div>
                                                        <div class="tab-pane " id="Videos">
                                                            <div class="row">
                                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="col-sm-6">
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-10 padding0">
                                                                                        <asp:Label ID="txtUrl" placeholder="URL Link" runat="server" Width="300" Text="" />
                                                                                    </div>
                                                                                   <%-- <div class="col-sm-2 padding0">
                                                                                        <asp:Label ID="lbtnaddproductionline" runat="server" TabIndex="103" CausesValidation="false" OnClick="btnAdd_Click">
                                                                     <span class="glyphicon glyphicon-plus" style="font-size:20px;"  aria-hidden="true"></span>
                                                                                        </asp:Label>
                                                                                    </div>--%>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row height10">
                                                                            </div>
                                                                            <div class="row">
                                                                                <asp:Button class="btn btn-default btn-sm" OnClientClick="return ShowModalPopup()" Visible="false" ID="btnShow" runat="server" Text="Upload" />
                                                                                <asp:Button class="btn btn-default btn-sm" ID="btnAdd" Visible="false" runat="server" Text="Add " OnClick="btnAdd_Click" />
                                                                            </div>

                                                                            <asp:Panel runat="server" Visible="true">
                                                                                <div class="row">
                                                                                    <div class="panel panel-default">
                                                                                        <div class="panel-heading paddingtopbottom0">
                                                                                            Video List
                                                                                        </div>
                                                                                        <div class="panel-body panelscollbar height200">
                                                                                            <asp:GridView ID="grvideo" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText=" ">
                                                                                                        <ItemTemplate>
<%--                                                                                                            <asp:LinkButton ID="lbtnser_Remove" runat="server" CausesValidation="false" OnClientClick="ConfirmDeleteItem()" Width="20px" OnClick="lbtnser_Remove_Click">
                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                                                                            </asp:LinkButton>--%>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Path ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lbtnpath" OnClick="lbtnPLAY_Click" runat="server" Text='<%# Bind("MMP_PATH") %>' CausesValidation="false">
                                                                                                            </asp:LinkButton>

                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                            </asp:Panel>


                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>

                                                                            <iframe id="videoItem" runat="server" width="420" height="315" frameborder="0" allowfullscreen></iframe>

                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                       
                                                        <div class="tab-pane " id="Pdf">
                                                             <asp:Panel runat="server" ID="pnlpdf">
                                                            <div class="row">

                                                                <div class="row">
                                                                    <div class="col-sm-5">
                                                                       <%-- <div class="col-sm-8">
                                                                            <asp:FileUpload ID="FileUpload1" runat="server" />

                                                                        </div>--%>


                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 height10">
                                                                    </div>
                                                                </div>
                                                                <div class="row paddingLeft0 paddingRight0">
                                                                    <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                                        <div class="col-sm-2">
                                                                           <%-- <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <asp:Button class="btn btn-default btn-sm" ID="btnuploadPdf" runat="server" Text="Upload" OnClick="btnuploadPdf_click" />
                                                                                </div>
                                                                            </div>--%>
                                                                            
                                                                                <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <asp:LinkButton ID="lnkView" runat="server" Text="View Brochures" OnClick="View" AutoPostBack="true"></asp:LinkButton>

                                                                                </div>
                                                                            </div>
                                                                            
                                                                        </div>
                                                                        <div class="col-sm-7 paddingLeft0 paddingRight0">
                                                                            <div class="twstsasa">
                                                                                <asp:Literal ID="ltEmbed" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <hr />


                                                            </div>
                                                                  </asp:Panel>
                                                        </div>
                                                            
                                                    </div>
                                                   
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-default height400 width700">
            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div4" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" id="search" runat="server">
                                <div class="col-sm-2 labelText1">
                                    Search by key
                                </div>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-3 paddingRight5">
                                            <asp:DropDownList ID="ddlSearchbykey" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="col-sm-2 labelText1">
                                    Search by word
                                </div>
                                <div class="col-sm-4 paddingRight5">
                                    <asp:TextBox ID="txtSearchbyword" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                </div>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtSearchbyword" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdResult" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
                                            <Columns>
                                                <asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />

                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <script type="text/javascript">
        Sys.Application.add_load(fun);
        function fun() {
            $('#myTab a').click(function (e) {
                e.preventDefault();
                $(this).tab('show');
                //  alert($(this).attr('href'));
                document.getElementById('<%=hdfTabIndex.ClientID %>').value = $(this).attr('href');
            });

            $(document).ready(function () {
                var tab = document.getElementById('<%= hdfTabIndex.ClientID%>').value;
                // alert(tab);
                $('#myTab a[href="' + tab + '"]').tab('show');
            });
        }
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="FastForward.SCMWeb.View.ADMIN.WebForm1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
    <script src="../../Js/jquery-1.7.2.min.js"></script>
    <link href="//netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.css" rel="stylesheet" />

    <style type="text/css">

body {
    position: relative;
    overflow-x: hidden;
}
body,
html { height: 100%;}
.nav .open > a, 
.nav .open > a:hover, 
.nav .open > a:focus {background-color: transparent;}

/*-------------------------------*/
/*           Wrappers            */
/*-------------------------------*/

#wrapper {
    padding-left: 0;
    -webkit-transition: all 0.5s ease;
    -moz-transition: all 0.5s ease;
    -o-transition: all 0.5s ease;
    transition: all 0.5s ease;
}

#wrapper.toggled {
    padding-left: 220px;
}

#sidebar-wrapper {
    z-index: 1000;
    left: 220px;
    width: 0;
    height: 100%;
    margin-left: -220px;
    overflow-y: auto;
    overflow-x: hidden;
    background: #1a1a1a;
    -webkit-transition: all 0.5s ease;
    -moz-transition: all 0.5s ease;
    -o-transition: all 0.5s ease;
    transition: all 0.5s ease;
}

#sidebar-wrapper::-webkit-scrollbar {
  display: none;
}

#wrapper.toggled #sidebar-wrapper {
    width: 220px;
}

#page-content-wrapper {
    width: 100%;
    padding-top: 70px;
}

#wrapper.toggled #page-content-wrapper {
    position: absolute;
    margin-right: -220px;
}

/*-------------------------------*/
/*     Sidebar nav styles        */
/*-------------------------------*/

.sidebar-nav {
    position: absolute;
    top: 0;
    width: 220px;
    margin: 0;
    padding: 0;
    list-style: none;
}

.sidebar-nav li {
    position: relative; 
    line-height: 20px;
    display: inline-block;
    width: 100%;
}

.sidebar-nav li:before {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    z-index: -1;
    height: 100%;
    width: 3px;
    background-color: #1c1c1c;
    -webkit-transition: width .2s ease-in;
      -moz-transition:  width .2s ease-in;
       -ms-transition:  width .2s ease-in;
            transition: width .2s ease-in;

}
.sidebar-nav li:first-child a {
    color: #fff;
    background-color: #1a1a1a;
}
.sidebar-nav li:nth-child(2):before {
    background-color: #ec1b5a;   
}
.sidebar-nav li:nth-child(3):before {
    background-color: #79aefe;   
}
.sidebar-nav li:nth-child(4):before {
    background-color: #314190;   
}
.sidebar-nav li:nth-child(5):before {
    background-color: #279636;   
}
.sidebar-nav li:nth-child(6):before {
    background-color: #7d5d81;   
}
.sidebar-nav li:nth-child(7):before {
    background-color: #ead24c;   
}
.sidebar-nav li:nth-child(8):before {
    background-color: #2d2366;   
}
.sidebar-nav li:nth-child(9):before {
    background-color: #35acdf;   
}
.sidebar-nav li:hover:before,
.sidebar-nav li.open:hover:before {
    width: 100%;
    -webkit-transition: width .2s ease-in;
      -moz-transition:  width .2s ease-in;
       -ms-transition:  width .2s ease-in;
            transition: width .2s ease-in;

}

.sidebar-nav li a {
    display: block;
    color: #ddd;
    text-decoration: none;
    padding: 10px 15px 10px 30px;    
}

.sidebar-nav li a:hover,
.sidebar-nav li a:active,
.sidebar-nav li a:focus,
.sidebar-nav li.open a:hover,
.sidebar-nav li.open a:active,
.sidebar-nav li.open a:focus{
    color: #fff;
    text-decoration: none;
    background-color: transparent;
}

.sidebar-nav > .sidebar-brand {
    height: 65px;
    font-size: 20px;
    line-height: 44px;
}
.sidebar-nav .dropdown-menu {
    position: relative;
    width: 100%;
    padding: 0;
    margin: 0;
    border-radius: 0;
    border: none;
    background-color: #222;
    box-shadow: none;
}

/*-------------------------------*/
/*       Hamburger-Cross         */
/*-------------------------------*/

.hamburger {
  position: fixed;
  top: 20px;  
  z-index: 999;
  display: block;
  width: 32px;
  height: 32px;
  margin-left: 15px;
  background: transparent;
  border: none;
}
.hamburger:hover,
.hamburger:focus,
.hamburger:active {
  outline: none;
}
.hamburger.is-closed:before {
  content: '';
  display: block;
  width: 100px;
  font-size: 14px;
  color: #fff;
  line-height: 32px;
  text-align: center;
  opacity: 0;
  -webkit-transform: translate3d(0,0,0);
  -webkit-transition: all .35s ease-in-out;
}
.hamburger.is-closed:hover:before {
  opacity: 1;
  display: block;
  -webkit-transform: translate3d(-100px,0,0);
  -webkit-transition: all .35s ease-in-out;
}

.hamburger.is-closed .hamb-top,
.hamburger.is-closed .hamb-middle,
.hamburger.is-closed .hamb-bottom,
.hamburger.is-open .hamb-top,
.hamburger.is-open .hamb-middle,
.hamburger.is-open .hamb-bottom {
  position: absolute;
  left: 0;
  height: 4px;
  width: 100%;
}
.hamburger.is-closed .hamb-top,
.hamburger.is-closed .hamb-middle,
.hamburger.is-closed .hamb-bottom {
  background-color: #1a1a1a;
}
.hamburger.is-closed .hamb-top { 
  top: 5px; 
  -webkit-transition: all .35s ease-in-out;
}
.hamburger.is-closed .hamb-middle {
  top: 50%;
  margin-top: -2px;
}
.hamburger.is-closed .hamb-bottom {
  bottom: 5px;  
  -webkit-transition: all .35s ease-in-out;
}

.hamburger.is-closed:hover .hamb-top {
  top: 0;
  -webkit-transition: all .35s ease-in-out;
}
.hamburger.is-closed:hover .hamb-bottom {
  bottom: 0;
  -webkit-transition: all .35s ease-in-out;
}
.hamburger.is-open .hamb-top,
.hamburger.is-open .hamb-middle,
.hamburger.is-open .hamb-bottom {
  background-color: #1a1a1a;
}
.hamburger.is-open .hamb-top,
.hamburger.is-open .hamb-bottom {
  top: 50%;
  margin-top: -2px;  
}
.hamburger.is-open .hamb-top { 
  -webkit-transform: rotate(45deg);
  -webkit-transition: -webkit-transform .2s cubic-bezier(.73,1,.28,.08);
}
.hamburger.is-open .hamb-middle { display: none; }
.hamburger.is-open .hamb-bottom {
  -webkit-transform: rotate(-45deg);
  -webkit-transition: -webkit-transform .2s cubic-bezier(.73,1,.28,.08);
}
.hamburger.is-open:before {
  content: '';
  display: block;
  width: 100px;
  font-size: 14px;
  color: #fff;
  line-height: 32px;
  text-align: center;
  opacity: 0;
  -webkit-transform: translate3d(0,0,0);
  -webkit-transition: all .35s ease-in-out;
}
.hamburger.is-open:hover:before {
  opacity: 1;
  display: block;
  -webkit-transform: translate3d(-100px,0,0);
  -webkit-transition: all .35s ease-in-out;
}

/*-------------------------------*/
/*            Overlay            */
/*-------------------------------*/

.overlay {
    position: fixed;
    display: none;
    width: 100%;
    height: 100%;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: rgba(250,250,250,.8);
    z-index: 1;
}

    </style>


    <script type="text/javascript">

        $(document).ready(function () {
            var trigger = $('.hamburger'),
                overlay = $('.overlay'),
               isClosed = false;

            trigger.click(function () {
                hamburger_cross();
            });

            function hamburger_cross() {

                if (isClosed == true) {
                    overlay.hide();
                    trigger.removeClass('is-open');
                    trigger.addClass('is-closed');
                    isClosed = false;
                } else {
                    overlay.show();
                    trigger.removeClass('is-closed');
                    trigger.addClass('is-open');
                    isClosed = true;
                }
            }

            $('[data-toggle="offcanvas"]').click(function () {
                $('#wrapper').toggleClass('toggled');
            });
        });

    </script>
</asp:Content>






<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    
    

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>

            <asp:HiddenField ID="txtconfirmclear" runat="server" />


            



            <div class="panel panel-default marginLeftRight5"  >

                <div class="row" >

                         <div id="wrapper">
        <div class="overlay"></div>

         <%--<nav class="navbar navbar-inverse navbar-fixed-bottom" id="sidebar-wrapper" role="navigation">--%>
        <nav class=" navbar-fixed-bottom navbar-fixed-bottom navbar-fixed-top" id="sidebar-wrapper" role="navigation">

            <ul class="nav sidebar-nav">
                <li class="sidebar-brand">
                    <a href="#">
                       Brand
                    </a>
                </li>
                <li>
                    <a href="#">Home</a>
                </li>
                <li>
                    <a href="#">About</a>
                </li>
                <li>
                    <a href="#">Events</a>
                </li>
                <li>
                    <a href="#">Team</a>
                </li>
                <li class="dropdown">
                  <a href="#" class="dropdown-toggle" data-toggle="dropdown">Works <span class="caret"></span></a>
                  <ul class="dropdown-menu" role="menu">
                    <li class="dropdown-header">Dropdown heading</li>
                    <li><a href="#">Action</a></li>
                    <li><a href="#">Another action</a></li>
                    <li><a href="#">Something else here</a></li>
                    <li><a href="#">Separated link</a></li>
                    <li><a href="#">One more separated link</a></li>
                  </ul>
                </li>
                <li>
                    <a href="#">Services</a>
                </li>
                <li>
                    <a href="#">Contact</a>
                </li>
                <li>
                    <a href="https://twitter.com/maridlcrmn">Follow me</a>
                </li>
            </ul>

        </nav>
        
        <div id="page-content-wrapper">
            <button type="button" class="hamburger is-closed" data-toggle="offcanvas">
                <span class="hamb-top"></span>
    			<span class="hamb-middle"></span>
				<span class="hamb-bottom"></span>
            </button>
        </div>

    </div>

                    </div>


                    <div class="row">

                <div visible="true" class="alert alert-success" role="alert" runat="server" id="DivAsk">
                    <div class="col-sm-11  buttonrow ">
                        <strong>Well done!</strong>
                        <asp:Label ID="lblAsk" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-1  buttonrow">
                        <div class="col-sm-3  buttonrow">
                            <asp:LinkButton ID="lbtnColse" runat="server" CausesValidation="false"  CssClass="floatright">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                            </asp:LinkButton>
                        </div>
                    </div>

                </div>

                <div visible="false" class="alert alert-danger" role="alert" runat="server" id="Div1">
                    <div class="col-sm-11  buttonrow ">
                        <strong>Alert!</strong>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-1  buttonrow">
                        <div class="col-sm-3  buttonrow">
                            <asp:LinkButton ID="lbtnclosealert" runat="server" CausesValidation="false"  CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>

                <div visible="false" class="alert alert-info" role="alert" runat="server" id="DivInfo">
                    <div class="col-sm-11  buttonrow ">
                        <strong>Record Found !</strong>
                        <asp:Label ID="lblinfo" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-1  buttonrow">
                        <div class="col-sm-3  buttonrow">
                            <asp:LinkButton ID="lbtncloseinfo" runat="server" CausesValidation="false"  CssClass="floatright">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>

            </div>

                    <div class="row">

                    <div class="panel-body">

                      <div class="col-sm-12">

                        <div class="panel panel-default" >

                            <div class="panel-heading pannelheading" >
                                Order Entry
                            </div>

                            <div class="panel-body">

                            <div class="col-sm-3">
                                             <div class="row">

                                                 <div class="col-sm-4 labelText1">
                                                     Manual Ref #
                                                 </div>
                                                 <div class="col-sm-8" style="margin-left:-25px">
                                                     <asp:TextBox ID="txtmanualref" runat="server" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                                 </div>

                                             </div>
                                         </div>

                            <div class="col-sm-3">
                                             <div class="row">

                                                 <div class="col-sm-3 labelText1">
                                                     Supplier
                                                 </div>
                                                 <div class="col-sm-8 paddingRight5">
                                                     <asp:TextBox ID="txtsupplier" runat="server" CssClass="form-control"></asp:TextBox>
                                                     <asp:Label runat="server" ID="lblemail" Text="No Email"></asp:Label>
                                                 </div>
                                                 <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" TabIndex="2" >
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                             </div>
                                         </div>

                            <div class="col-sm-3">
                                             <div class="row">

                                                 <div class="col-sm-3 labelText1">
                                                     Order #
                                                 </div>

                                                 <div class="col-sm-8 paddingRight5">
                                                     <asp:TextBox ID="txtordno" runat="server" CssClass="form-control"></asp:TextBox>
                                                 </div>
                                                 <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearchRoleNew" runat="server" TabIndex="3" >
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                             </div>
                                         </div>

                            <div class="col-sm-3">
                                             <div class="row">

                                                 <div class="col-sm-3 labelText1">
                                                     Date
                                                 </div>

                                                 <div>
                                                     <div class="col-sm-8">
                                                         <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                             onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                         <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtdate"
                                                             PopupButtonID="lbtnimgselectdate" Format="dd/MMM/yyyy">
                                                         </asp:CalendarExtender>
                                                     </div>

                                                     <div id="caldv" class="col-sm-1 paddingLeft0" style="margin-left:-10px;margin-top:-4px">
                                                         <asp:LinkButton ID="lbtnimgselectdate" TabIndex="4" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true" style="font-size:20px"></span>
                                                         </asp:LinkButton>
                                                     </div>
                                                 </div>

                                             </div>
                                         </div>

                            <div class="row">
                                             <div class="col-sm-12 height5">
                                             </div>
                                         </div>

                            <div class="col-sm-12">
                                             <div class="row">

                                                 <div class="col-sm-1 labelText1">
                                                     Remarks
                                                 </div>
                                                 <div class="col-sm-11 padding0" style="margin-left:-12px">
                                                      <asp:TextBox ID="txtdescription" runat="server" TabIndex="5" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                 </div>

                                             </div>
                                         </div>

                            <div class="row">
                                             <div class="col-sm-12 height5">
                                             </div>
                                         </div>

                            <div class="col-sm-3">
                                             <div class="row">

                                                 <div class="col-sm-3 labelText1">
                                                      Trade Term
                                                 </div>
                                                 <div class="col-sm-8 paddingRight5">
                                                       <asp:TextBox ID="txttradeterm" runat="server" CssClass="form-control" ></asp:TextBox>
                                                      <asp:Label runat="server" ID="lblcif" Text="Cost, Insurance and Freight - CIF"></asp:Label>
                                                 </div>
                                                 <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="LinkButton2" runat="server" TabIndex="6"  >
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                             </div>
                                         </div>

                            <div class="col-sm-3">
                                             <div class="row">

                                                 <div class="col-sm-3 labelText1">
                                                       Mode of Shipment
                                                 </div>
                                                 <div class="col-sm-8 paddingRight5">
                                                      <asp:TextBox ID="txtmodeofshipment" runat="server" CssClass="form-control"></asp:TextBox>
                                                      <asp:Label runat="server" ID="lblship" Text="by Sea"></asp:Label>
                                                 </div>
                                                 <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="LinkButton3" runat="server" TabIndex="7"  >
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                             </div>
                                         </div>

                            <div class="col-sm-3">
                                             <div class="row">

                                                 <div class="col-sm-3 labelText1">
                                                      Port of Origin
                                                 </div>
                                                 <div class="col-sm-8 paddingRight5">
                                                       <asp:TextBox ID="txtportoforigin" runat="server" CssClass="form-control"></asp:TextBox>
                                                      <asp:Label runat="server" ID="lblportoforigin" Text="Any port of india"></asp:Label>
                                                 </div>
                                                 <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="LinkButton4" runat="server" TabIndex="8"  >
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                             </div>
                                         </div>

                            <div class="col-sm-3">
                                             <div class="row">

                                                 <div class="col-sm-3 labelText1">
                                                     ETA
                                                 </div>
                                                 <div>
                                                     <div class="col-sm-8">
                                                         <asp:TextBox ID="txteta" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                             onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                         <asp:Label runat="server" ID="lblleadtime" Text="Lead time in 30 Days"></asp:Label>
                                                         <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txteta"
                                                             PopupButtonID="lbtneta" Format="dd/MMM/yyyy">
                                                         </asp:CalendarExtender>
                                                     </div>

                                                     <div id="caldv3" class="col-sm-1 paddingLeft0" style="margin-left:-10px;margin-top:-4px">
                                                         <asp:LinkButton ID="lbtneta" TabIndex="9" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true" style="font-size:20px"></span>
                                                         </asp:LinkButton>
                                                     </div>
                                                 </div>


                                             </div>
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
                                             <div class="col-sm-12 height5">
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
                                             <div class="col-sm-12 height5">
                                             </div>
                                         </div>

                       </div>

                    </div>

                    </div>

            </div>

                    <div class="row">
                    <div class="panel-body">
                        <div class="col-sm-12">
                            <div class="panel panel-default">

                                <div class="panel-heading panelHeadingInfoBar">

                                    <div class="col-sm-3">

                                                         <div class="row">

                                                 <div class="col-sm-4 labelText1">
                                                      Description
                                                 </div>
                                                 <div class="col-sm-8">
                                                     <asp:Label ID="lbldescription" TabIndex="23" runat="server" CssClass="col-sm-3 labelText1" Text="100" Font-Bold="true"></asp:Label>
                                                 </div>

                                             </div>

                                                          </div>
                                                     
                                    <div class="col-sm-3">

                                                         <div class="row">

                                                 <div class="col-sm-4 labelText1">
                                                      Brand
                                                 </div>
                                                 <div class="col-sm-8">
                                                     <asp:Label ID="lblbrand" TabIndex="23" runat="server" CssClass="col-sm-3 labelText1" Text="100" Font-Bold="true"></asp:Label>
                                                 </div>

                                             </div>

                                                          </div>

                                    <div class="col-sm-3">

                                                         <div class="row">

                                                 <div class="col-sm-4 labelText1">
                                                      UOM
                                                 </div>
                                                 <div class="col-sm-8">
                                                     <asp:Label ID="lbluom" TabIndex="23" runat="server" Font-Bold="true" CssClass="col-sm-3 labelText1" Text="100"></asp:Label>
                                                 </div>

                                             </div>

                                                          </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                    <div class="row">

                       <div class="panel-body">
                           <div class="col-sm-12">
                               <div class="panel panel-default">

                                   <div class="panel-heading pannelheading" >
                               Order Items
                            </div>

                            <div class="panel-body panelscollOrderEntry">

                                 <div class="col-sm-2" style="margin-left:15px;">

                                                     <div class="row">
                                                     <div class="col-sm-4 labelText1" style="margin-left:-14px">
                                                         Order Validity
                                                     </div>

                                                     <div class="col-sm-4" style="margin-left:-18px">
                                                         <asp:DropDownList ID="ddlYear" runat="server" Width="73px" TabIndex="10" AutoPostBack="true" CssClass="form-control">
                                                         </asp:DropDownList>
                                                         </div>

                                                     <div class="col-sm-4" style="margin-left:30px">
                                                           <asp:DropDownList ID="ddlMonth" runat="server" Width="127px" TabIndex="11" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>

                                                         </div>

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
                                             <div class="col-sm-12 height5">
                                             </div>
                                         </div>

                                                     <div class="col-sm-2">

                                                        <div class="row">

                                                 <div class="col-sm-3 labelText1">
                                                      Model
                                                 </div>
                                                 <div class="col-sm-9">
                                                     <asp:TextBox ID="txtmodel" runat="server" TabIndex="12" CssClass="form-control"></asp:TextBox>
                                                 </div>

                                             </div>

                                                     </div>

                                                     <div class="col-sm-2">

                                                         <div class="row">

                                                 <div class="col-sm-3 labelText1">
                                                       Item
                                                 </div>
                                                 <div class="col-sm-8 paddingRight5">
                                                      <asp:TextBox ID="txtitem" runat="server" CssClass="form-control"></asp:TextBox>
                                                 </div>
                                                 <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="LinkButton5" runat="server" TabIndex="13"  >
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                             </div>

                                                     </div>

                                                     <div class="col-sm-2">

                                                          <div class="row">

                                                 <div class="col-sm-3 labelText1">
                                                     Item Type
                                                 </div>
                                                 <div class="col-sm-9">
                                                   <asp:DropDownList ID="ddlitemtype" runat="server" TabIndex="14" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                 </div>

                                             </div>

                                                     </div>

                                                     <div class="col-sm-2">

                                                         <div class="row">

                                                 <div class="col-sm-3 labelText1">
                                                       Colour
                                                 </div>
                                                 <div class="col-sm-9">
                                                      <asp:TextBox ID="txtcolour" runat="server" TabIndex="15" CssClass="form-control"></asp:TextBox>
                                                 </div>

                                             </div>

                                                     </div>

                                                     <div class="col-sm-2">

                                                         <div class="row">

                                                 <div class="col-sm-3 labelText1">
                                                       Qty
                                                 </div>
                                                 <div class="col-sm-9">
                                                    <asp:TextBox ID="txtqty" runat="server" TabIndex="16" CssClass="form-control"></asp:TextBox>
                                                 </div>

                                             </div>
                                                     </div>

                                                     <div class="col-sm-2">

                                                         <div class="row">

                                                 <div class="col-sm-3 labelText1">
                                                       Unit Rate
                                                 </div>
                                                 <div class="col-sm-9">
                                                   <asp:TextBox ID="txtunitrate" runat="server" TabIndex="17" CssClass="form-control"></asp:TextBox>
                                                 </div>

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
                                             <div class="col-sm-12 height5">
                                             </div>
                                         </div>

                                                     <div class="col-sm-2">
                                    <div class="row">

                                        <div class="col-sm-3 labelText1">
                                        </div>
                                        <div class="col-sm-9">
                                        </div>

                                    </div>
                                </div>

                                                     <div class="col-sm-2">
                                    <div class="row">

                                        <div class="col-sm-3 labelText1">
                                        </div>
                                        <div class="col-sm-9">
                                        </div>

                                    </div>
                                </div>

                                                     <div class="col-sm-2">
                                    <div class="row">

                                        <div class="col-sm-3 labelText1">
                                        </div>
                                        <div class="col-sm-9">
                                        </div>

                                    </div>
                                </div>

                                                     <div class="col-sm-2">
                                    <div class="row">

                                        <div class="col-sm-3 labelText1">
                                        </div>
                                        <div class="col-sm-9">
                                        </div>

                                    </div>
                                </div>

                                                     <div class="col-sm-2">
                                    <div class="row">

                                        <div class="col-sm-3 labelText1">
                                        </div>
                                        <div class="col-sm-9">
                                        </div>

                                    </div>
                                </div>

                                                     <div class="col-sm-2">
                                             <div class="row">

                                                 <div class="col-sm-3 labelText1">
                                                      
                                                 </div>
                                                 <div class="col-sm-9">
                                                     <asp:LinkButton ID="lbtnadditems" CausesValidation="false" TabIndex="18" CssClass="floatRight" runat="server" OnClientClick="Confirm()">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true" style="font-size:20px"></span>Add Entry
                                                     </asp:LinkButton>
                                                 </div>

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

                                                     <asp:GridView ID="grdorderdetails" TabIndex="19" runat="server" CssClass="table table-hover table-striped">

                                                        <EmptyDataTemplate>
                                                            <table class="table table-hover table-striped"  border="1" style="border-collapse: collapse;" rules="all">
                                                                <tbody>
                                                                    <tr>
                                                                        <th scope="col">
                                                                            Item
                                                                        </th>
                                                                        <th scope="col">
                                                                            Description
                                                                        </th>
                                                                        <th scope="col">
                                                                            Model
                                                                        </th>
                                                                        <th scope="col">
                                                                            Colour
                                                                        </th>
                                                                        <th scope="col">
                                                                            UOM
                                                                        </th>
                                                                        <th scope="col">
                                                                            Type
                                                                        </th>
                                                                        <th scope="col">
                                                                            Ord.Qty
                                                                        </th>
                                                                        <th scope="col">
                                                                            Unit Rate
                                                                        </th>
                                                                        <th scope="col">
                                                                            Value
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td >No records found.
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                    </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
                                                        
                                                        <Columns>
                                                            <asp:BoundField DataField="CusID" HeaderText="CusID" SortExpression="CusID" />
                                                            <asp:BoundField DataField="CusName" HeaderText="CusName" SortExpression="CusName"></asp:BoundField>
                                                            <asp:BoundField DataField="CusAddress" HeaderText="CusAddress" SortExpression="CusAddress"/>
                                                            <asp:BoundField DataField="CusCountry" HeaderText="CusCountry" SortExpression="CusCountry" />
                                                        </Columns>
                                                    </asp:GridView>
                             </div>

                               </div>
                           </div>
                       </div>

                   </div>

                    <div class="row">
                    <div class="panel-body">
                        <div class="col-sm-12">
                            <div class="panel panel-default">

                                <div class="panel-heading panelHeadingInfoBar">

                                         <div class="col-sm-3">
                                             <div class="row">

                                                 <div class="col-sm-3 labelText1">
                                                      
                                                 </div>
                                                 <div class="col-sm-9">
                                                      
                                                 </div>

                                             </div>
                                         </div>

                                         <div class="col-sm-3">
                                             <div class="row">

                                                 <div class="col-sm-3 labelText1">
                                                     
                                                 </div>
                                                 <div class="col-sm-9">
                                                    
                                                 </div>

                                             </div>
                                         </div>
     
                                         <div class="col-sm-3">
                                             <div class="row">

                                                 <div class="col-sm-4 labelText1">
                                                      Total Order Qty
                                                 </div>
                                                 <div class="col-sm-8">
                                                     <asp:Label ID="lbltotordqty" runat="server" CssClass="col-sm-3 labelText1" Text="100"></asp:Label>
                                                 </div>

                                             </div>
                                         </div> 

                                         <div class="col-sm-3">
                                             <div class="row">

                                                 <div class="col-sm-6 labelText1">
                                                     Total Order Value In
                                                 </div>
                                                 <div class="col-sm-6">
                                                     <asp:Label ID="lblcurrency" runat="server"  CssClass="col-sm-3 labelText1" Text="USD"></asp:Label>
                                                    <asp:Label ID="lbltotordval" runat="server" CssClass="col-sm-3 labelText1" Text="100"></asp:Label>
                                                 </div>

                                             </div>
                                         </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                </div>    

        </ContentTemplate>

    </asp:UpdatePanel>

 
</asp:Content>

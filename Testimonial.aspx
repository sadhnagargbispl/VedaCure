<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="Testimonial.aspx.cs" Inherits="Testimonial" EnableEventValidation="false"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <style>
            .red {
                color: red;
                font-size: 1.5em;
                padding-left: 4px;
                font-weight: bold;
            }
        </style>
        <section class="section">
            <ul class="breadcrumb breadcrumb-style ">
                <%--      <li class="breadcrumb-item">
            <h4 class="page-title m-b-0">Dashboard</h4>
        </li>
        <li class="breadcrumb-item">
            <a href="#"><i data-feather="home"></i></a>
        </li>
        <li class="breadcrumb-item active">Dashboard</li>--%>
            </ul>
            <div class="row">
                <div class="col-12 col-sm-12 col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="mb-0">Testimonial</h5>
                        </div>
                        <div class="card-body">
                            <div class="row g-1">
                                <div class="clr">
                                    <asp:Label ID="errMsg" runat="server" CssClass="error"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Heading<span class="red">*</span></label>
                                        <asp:HiddenField ID="hdnSessn" runat="server" />
                                        <%--  <asp:TextBox ID="oldpass" class="validate[required] form-control" TextMode="Password"
                                            runat="server"></asp:TextBox>
                                        --%>
                                        <asp:TextBox ID="txtHeading" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Description<span class="red">*</span>
                                        </label>

                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine "></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Image<span class="red">*</span></label>
                                        <asp:FileUpload ID="ImageUpload" runat="server" CssClass="form-control" />

                                    </div>

                                    <div class="form-group">
                                        <asp:Button ID="BtnSave" runat="server" Text="Submit" class="btn btn-primary" OnClick="BtnSave_Click" />
                                    </div>
                                </div>

                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </section>


    </div>
</asp:Content>


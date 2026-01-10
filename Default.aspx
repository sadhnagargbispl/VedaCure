<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, shrink-to-fit=no" name="viewport">
    <title><%=Session["Title"].ToString ()%></title>
    <link rel="stylesheet" href="assets/css/app.min.css">
    <link rel="stylesheet" href="assets/bundles/bootstrap-social/bootstrap-social.css">
    <link rel="stylesheet" href="assets/css/style.css">
    <link rel="stylesheet" href="assets/css/components.css">
</head>
<body class="background-image-body">
    <div class="loader"></div>
    <form id="form1" runat="server" class="needs-validation">

        <div id="app">
            <section class="section">
                <div class="container mt-5">
                    <div class="row">
                        <div class="col-12 col-sm-8 offset-sm-2 col-md-6 offset-md-3 col-lg-6 offset-lg-3 col-xl-4 offset-xl-4">
                            <div class="card card-auth">
                                <div class="login-brand login-brand-color">
                                    <img alt="image" src="images/LOGO/logo.png" width="200px" />
                                </div>
                                <div class="card-header card-header-auth">
                                    <h4>Login Account</h4>
                                </div>
                                <div class="card-body">
                                    <div>
                                        <div class="form-group">
                                            <label for="email">Username</label>
                                            <input class="form-control" type="text" runat="server" id="Txtuid" name="uid" placeholder="Username" tabindex="1" autofocus>
                                            <div class="invalid-feedback">
                                                Please fill in your Username
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="d-block">
                                                <label for="password" class="control-label">Password</label>
                                                <div class="float-right">
                                                    <a href="#" class="text-small">Forgot Password?
                                                    </a>
                                                </div>
                                            </div>
                                            <input class="form-control" type="password" runat="server" id="Txtpwd" name="pwd" tabindex="2">
                                            <div class="invalid-feedback">
                                                please fill in your password
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="custom-control custom-checkbox">
                                                <input type="checkbox" name="remember" class="custom-control-input" tabindex="3" id="remember-me">
                                                <label class="custom-control-label" for="remember-me">Remember Me</label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <button type="submit" class="btn btn-lg btn-primary w-100" id="BtnSubmit" runat="server" onserverclick="BtnSubmit_ServerClick">
                                                Login
                                            </button>
                                        </div>
                                    </div>
                                    <%--<div class="text-center mt-4 mb-3">
                                        <div class="text-job text-muted">Login With Social</div>
                                    </div>
                                    <div class="row sm-gutters justify-content-center">
                                        <div class="col-6">
                                            <a class="btn btn-block btn-social btn-facebook w-100 text-center">
                                                <span class="fab fa-facebook"></span>Facebook
                                            </a>
                                        </div>
                                        <div class="col-6">
                                            <a class="btn btn-block btn-social btn-twitter w-100 text-center">
                                                <span class="fab fa-twitter"></span>Twitter
                                            </a>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                            <div class="mt-5 text-muted text-center">
                                Don't have an account? <a href="NewJoining.aspx">Create One</a>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </form>

</body>
<script src="assets/js/app.min.js"></script>
<!-- JS Libraies -->
<!-- Page Specific JS File -->
<!-- Template JS File -->
<script src="assets/js/scripts.js"></script>
<script src="disable.js"></script>
</html>

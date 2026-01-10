<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />
    <style>
        .upload-box {
            width: 100%;
            height: 180px;
            border: 2px dashed #bbb;
            border-radius: 10px;
            text-align: center;
            padding-top: 35px;
            cursor: pointer;
            background-color: #fafafa;
        }

            .upload-box:hover {
                background-color: #f0f0f0;
            }
        /* Only Desktop (>= 992px) */
        @media (min-width: 992px) {
            .ref-card,
            .news-card,
            .ref-card .card-body,
            .news-card .card-body {
                height: 200px !important; /* fix height */
                overflow: hidden !important; /* extra content hide */
            }

            /* rows ko bhi 50px me adjust karna */
            .ref-row {
                height: 50px !important;
                align-items: center;
            }
        }
        /* ------------------------------------------
   PREMIUM DASHBOARD THEME � BY CHATGPT
------------------------------------------- */

        /* Google Font */
        @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap');

        :root {
            --primary: #4f46e5; /* Indigo */
            --primary-dark: #3730a3;
            --success: #10b981; /* Emerald */
            --warning: #f59e0b; /* Amber */
            --danger: #ef4444; /* Red */
            --info: #0ea5e9; /* Sky Blue */
            --dark: #1e293b; /* Slate */
            --text-gray: #475569;
        }

        /* ---------- GENERAL PAGE ---------- */


        body {
            background: linear-gradient(135deg, #eef2ff 0%, #e0e7ff 100%);
            min-height: 100vh;
            font-family: 'Poppins', sans-serif;
            color: var(--dark);
        }

        h1, h2, h3, h4, h5, h6 {
            font-weight: 600;
        }

        p, td, th, span, small {
            font-weight: 400;
            font-size: 14px;
        }

        /* ---------- CARD DESIGN ---------- */

        .card {
            border: none;
            border-radius: 18px;
            box-shadow: 0 8px 30px rgba(0,0,0,0.07);
            overflow: hidden;
            transition: all 0.3s ease;
            background: #ffffff;
        }

            .card:hover {
                /*transform: translateY(-6px);*/
                box-shadow: 0 15px 40px rgba(0,0,0,0.12);
            }

        /* ---------- CARD HEADER ---------- */

        .card-header {
            background: linear-gradient(135deg, var(--primary), var(--primary-dark));
            color: white;
            font-size: 16px;
            font-weight: 600;
            padding: 14px 20px;
            border-bottom: none !important;
        }

        /* ---------- PROFILE IMAGE ---------- */

        .profile-img {
            width: 125px;
            height: 125px;
            object-fit: cover;
            border: 4px solid white;
            box-shadow: 0 10px 25px rgba(0,0,0,0.25);
            border-radius: 50%;
        }

        /* ---------- STAT CARDS ---------- */

        .stat-card {
            background: white;
            border-radius: 18px;
            padding: 1.7rem;
            text-align: center;
            box-shadow: 0 8px 20px rgba(0,0,0,0.08);
            transition: all 0.3s ease;
        }

            .stat-card:hover {
                transform: translateY(-5px);
                box-shadow: 0 12px 30px rgba(0,0,0,0.15);
            }

        .stat-icon {
            width: 75px;
            height: 75px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 0 auto 14px;
            font-size: 28px;
            color: white;
        }

        /* ICON GRADIENTS */
        .bg-grad1 {
            background: linear-gradient(135deg, #6366f1 0%, #8b5cf6 100%);
        }

        .bg-grad2 {
            background: linear-gradient(135deg, #fb7185 0%, #f43f5e 100%);
        }

        .bg-grad3 {
            background: linear-gradient(135deg, #3b82f6 0%, #06b6d4 100%);
        }

        .bg-grad4 {
            background: linear-gradient(135deg, #22c55e 0%, #10b981 100%);
        }

        /* ---------- COPY BUTTON ---------- */

        .copy-btn {
            background: var(--primary);
            border: none;
            color: white;
            padding: 10px 22px;
            border-radius: 10px;
            font-size: 14px;
            font-weight: 500;
            transition: 0.3s ease;
        }

            .copy-btn:hover {
                background: var(--primary-dark);
                transform: translateY(-2px);
            }

        /* ---------- NEWS SCROLLER ---------- */

        .news-marquee {
            height: 300px;
            overflow-y: scroll;
            padding-right: 5px;
        }

        /* ---------- TABLES ---------- */

        .table th {
            background: #f1f5f9;
            font-weight: 600;
            font-size: 14px;
        }

        .table td {
            font-size: 14px;
        }

        .table:not(.table-sm) thead th {
            border-bottom: none;
            background-color: #ececec;
            color: #18173c;
            padding-top: 15px;
            padding-bottom: 15px;
        }
        /* Zebra rows */
        .table tbody tr:nth-child(odd) {
            background-color: #f9fafb;
        }

        .table tbody tr:hover {
            background-color: #eef2ff;
            transition: 0.2s;
        }
        /* ---------------------------
   GLOBAL CARD TITLE PREMIUM THEME
---------------------------- */

        /* Font Family */
        body, h1, h2, h3, h4, h5, h6, td, th, p, span, strong {
            font-family: "Poppins", sans-serif !important;
        }

        /* All card titles (stat cards + normal cards) */
        .card-header h5,
        .stat-card h5,
        .card h5,
        .card-title {
            font-size: 15px !important;
            font-weight: 600 !important;
            /*color: #e1e4e9 !important; */
            /* Slate Gray � Professional */
            letter-spacing: 0.3px;
            margin-bottom: 4px;
        }


        .theme-white .text-primary {
            color: #4f4949 !important;
        }
        /* Amounts / Numbers on cards */
        .stat-card h3 {
            font-size: 21px !important;
            font-weight: 700 !important;
            color: #111827 !important; /* Darker premium */
        }

        /* card-header background (premium blue-purple) */
        /*.card-header {
    background: linear-gradient(135deg, #2e815b, #2e815b) !important;
    color: #ffffff !important;
    font-weight: 600;
    font-size: 16px;
    border: none;
    padding: 14px 20px;
}*/
        .card-header {
            background: linear-gradient(135deg, #ffffff, #ffffff) !important;
            color: #181717 !important;
            font-weight: 600;
            font-size: 16px;
            border: none;
            padding: 14px 20px;
        }
        /* stat-card design */
        .stat-card {
            background: #ffffff;
            border-radius: 16px;
            padding: 1.2rem 1rem;
            text-align: center;
            border: 1px solid #f1f5f9;
            box-shadow: 0 4px 18px rgba(0,0,0,0.06);
            transition: 0.25s;
        }

            .stat-card:hover {
                transform: translateY(-4px);
                box-shadow: 0 12px 32px rgba(0,0,0,0.12);
            }

        /* Icon smaller and premium */
        .stat-icon {
            width: 55px;
            height: 55px;
            border-radius: 14px;
            font-size: 22px;
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 0 auto 12px;
            color: white;
        }
        /* Member card - professional modern style */
        .member-card {
            border: none;
            border-radius: 14px;
            background: #ffffff;
            box-shadow: 0 10px 30px rgba(18, 24, 37, 0.06);
            padding: 18px;
            overflow: hidden;
        }

            /* container inside card that aligns image + text */
            .member-card .card-body {
                display: flex;
                gap: 22px;
                align-items: center;
                flex-wrap: nowrap;
                padding: 12px;
            }

        /* profile circle */
        .profile-wrap {
            width: 110px;
            height: 110px;
            min-width: 110px;
            border-radius: 50%;
            overflow: hidden;
            display: flex;
            align-items: center;
            justify-content: center;
            background: linear-gradient(180deg,#f3f6fb,#ffffff);
            box-shadow: 0 12px 30px rgba(37,50,88,0.08);
            border: 3px solid rgba(74,92,255,0.06);
        }

        /* actual image */
        .profile-img {
            width: 100%;
            height: 100%;
            object-fit: cover;
            display: block;
        }

        /* Text area */
        .member-text {
            flex: 1 1 auto;
            text-align: left;
            padding-right: 8px;
        }

        /* Member name row */
        .member-title {
            font-family: 'Inter', system-ui, -apple-system, 'Segoe UI', Roboto, 'Helvetica Neue', Arial;
            font-size: 1.02rem;
            font-weight: 600;
            color: #111827;
            margin: 0 0 6px 0;
            display: block;
        }

            /* label inside title */
            .member-title .label {
                font-weight: 600;
                color: #374151;
                margin-right: 6px;
                font-size: 0.95rem;
            }

        /* highlighted value */
        .value {
            color: #2e815b; /* premium blue */
            font-weight: 700;
            letter-spacing: 0.25px;
            display: inline-block;
        }

        /* sub details row */
        .member-sub {
            font-size: 0.93rem;
            color: #4b5563;
            margin: 0;
            display: flex;
            align-items: center;
            gap: 10px;
            flex-wrap: wrap;
        }

        /* small dot separator */
        .dot {
            color: #bfc9d9;
            margin: 0 6px;
            font-weight: 700;
        }

        /* rank small badge */
        .rank-badge {
            background: linear-gradient(90deg,#fff,#f3f6ff);
            border: 1px solid rgba(65,89,255,0.08);
            color: #0f172a;
            padding: 6px 10px;
            border-radius: 16px;
            font-weight: 600;
            font-size: 0.9rem;
            display: inline-block;
        }

        /* responsive for small screens */
        @media (max-width: 576px) {
            .member-card .card-body {
                flex-direction: row;
                gap: 12px;
            }

            .member-text {
                padding-right: 0;
            }

            .profile-wrap {
                width: 88px;
                height: 88px;
                min-width: 88px;
            }
        }
        /* Quick Links (small icon buttons next to profile) */
        .quick-links {
            display: flex;
            gap: 12px;
            align-items: center;
            margin-left: 8px;
            flex-wrap: wrap;
        }

        /* Each quick link item */
        .ql-item {
            width: 64px;
            text-align: center;
            font-size: 12px;
            color: var(--text-gray, #475569);
            text-decoration: none;
            display: inline-flex;
            flex-direction: column;
            align-items: center;
            gap: 6px;
            transform: translateY(0);
            transition: transform .18s ease, box-shadow .18s ease;
        }

        /* icon circle */
        .ql-icon {
            width: 46px;
            height: 46px;
            border-radius: 12px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            background: linear-gradient(180deg,#ffffff, #f3f6ff);
            border: 1px solid rgba(74,92,255,0.06);
            box-shadow: 0 6px 16px rgba(37,50,88,0.06);
            font-size: 20px;
            color: var(--primary, #4f46e5);
        }

        /* label below icon */
        .ql-label {
            font-size: 12px;
            max-width: 70px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            color: #263238;
        }

        /* hover effect */
        .ql-item:hover .ql-icon {
            transform: translateY(-4px);
            box-shadow: 0 10px 28px rgba(37,50,88,0.12);
        }

        .ql-item:hover .ql-label {
            color: var(--primary, #4f46e5);
        }

        /* compact variant for small cards */
        .quick-links.compact .ql-item {
            width: 54px;
        }

        .quick-links.compact .ql-icon {
            width: 40px;
            height: 40px;
            border-radius: 10px;
            font-size: 18px;
        }

        /* responsive (stack under text on very small screens) */
        @media (max-width:420px) {
            .card-body {
                gap: 12px;
            }

            .quick-links {
                margin-left: 0;
                margin-top: 10px;
            }
        }
        /* Referral card responsive styling */
        .ref-card {
            border-radius: 12px;
            overflow: hidden;
            background: #fff;
            box-shadow: 0 8px 24px rgba(32,40,80,0.06);
            border: 1px solid #eef2ff;
        }

            .ref-card .card-header {
                background: linear-gradient(135deg,#6d28d9,#4f46e5);
                color: #fff;
                padding: 12px 16px;
                font-weight: 600;
                border-bottom: 1px solid rgba(255,255,255,0.06);
            }

            .ref-card .card-body {
                padding: 14px;
            }

        /* Grid row: label | link | button */
        .ref-row {
            display: grid;
            grid-template-columns: 180px 1fr 120px;
            gap: 12px;
            align-items: center;
            width: 100%;
        }

            /* label red (same as your screenshot) */
            .ref-row .ref-label {
                color: #d32f2f;
                font-weight: 700;
                padding: 8px 6px;
            }

            /* link cell */
            .ref-row .ref-link {
                padding: 8px 6px;
                font-weight: 500;
                color: #1b4ed6;
                word-break: break-word; /* wrap very long URL */
                hyphens: auto;
                max-width: 100%;
                overflow-wrap: anywhere;
            }

            /* copy button cell center */
            .ref-row .ref-action {
                text-align: center;
            }

        /* small screen: stack */
        @media (max-width: 768px) {
            .ref-row {
                grid-template-columns: 1fr; /* single column stack */
                gap: 8px;
            }

                .ref-row .ref-action {
                    text-align: left;
                }
        }

        /* make table fallback responsive (if you keep table) */
        .table-responsive-custom {
            width: 100%;
            overflow-x: auto;
            -webkit-overflow-scrolling: touch;
        }

        /* subtle link style */
        .ref-link a {
            color: #1b4ed6;
            text-decoration: none;
        }

            .ref-link a:hover {
                text-decoration: underline;
            }

        /* small helpers */
        .copy-btn {
            padding: 8px 14px;
            border-radius: 8px;
            border: none;
            background: #2e815b;
            color: #fff;
            cursor: pointer;
            font-weight: 600;
        }

            .copy-btn:active {
                transform: translateY(1px);
            }

        .theme-white .btn-primary {
            background-color: #2e815b;
            border-color: transparent !important;
            color: #fff;
        }

            .theme-white .btn-primary:hover {
                background-color: #2e815b !important;
                color: #fff;
            }

            .theme-white .btn-primary:focus:active {
                background-color: #2e815b !important;
            }

        /* Mobile ke liye kuch change nahi — already sahi hai */
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <section class="section">

            <%--<div class="row mb-4">
                <div class="col-lg-12">
                    <div class="card member-card">
                        <div class="card-body">

                            
                            <div class="profile-wrap">
                                <asp:Image ID="Image2" runat="server" CssClass="profile-img"
                                    Style="width: 130px; height: 130px; border-radius: 50%; cursor: pointer;"
                                    onclick="openPhotoModal()" />
                            </div>

                            
                            <div class="modal fade" id="profilePhotoModal" tabindex="-1">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content">

                                        <div class="modal-header bg-light">
                                            <h4 class="modal-title">Upload Profile Photo</h4>
                                            <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                                        </div>

                                        <div class="modal-body">

                                            <div class="row">

                                               
                                                <div class="col-md-4 text-center">
                                                    <img id="photoPreview" runat="server"
                                                        src="https://www.iconpacks.net/icons/2/free-user-icon-3296-thumb.png"
                                                        style="width: 150px; height: 150px; border-radius: 10px; border: 1px solid #ccc;" />
                                                </div>

                                               
                                                <div class="col-md-8">
                                                    <label class="form-label fw-bold">Upload new photo</label>

                                                    <div class="upload-box" onclick="openFilePicker()">
                                                        <i class="fa fa-image" style="font-size: 40px; color: #999;"></i>
                                                        <p><b>Upload</b> or drop your file here</p>
                                                    </div>

                                                    <asp:FileUpload ID="FileUpload1" runat="server" accept="image/*"
                                                        Style="display: none;" onchange="previewPhoto(this)" />

                                                    <div class="mt-3" style="font-size: 13px; color: #555;">
                                                        Accepted file types: .jpg, .jpeg, .png  
                    <br />
                                                        Best result: 200x200 pixel
                                                    </div>
                                                </div>

                                            </div>

                                        </div>

                                        <div class="modal-footer">
                                            <button class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                                            <asp:Button ID="btnSave" runat="server" Text="Save"
                                                CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="member-text">

                                <h5 class="member-title mb-1">
                                    <span class="label">Name :</span>
                                    <span class="value">
                                        <asp:Label ID="LblMemName" runat="server"></asp:Label>
                                    </span>
                                </h5>

                                <p class="member-sub mb-0">
                                    <span><strong>Login ID :</strong>
                                        <span class="value">
                                            <asp:Label ID="LblMemId" runat="server"></asp:Label></span>
                                    </span>
                                </p>
                                
                                <p class="member-sub mb-0">
                                    <span><strong>Joining Date : 
                                    </strong>
                                        <span class="value">
                                            <asp:Label ID="LblJoiningDate" runat="server"></asp:Label>
                                        </span>
                                    </span>
                                </p>
                                <p class="member-sub mb-0">
                                    <span><strong>Mobile No : 
                                    </strong>
                                        <span class="value">
                                            <asp:Label ID="LblMobileno" runat="server"></asp:Label>
                                        </span>
                                    </span>
                                </p>
                            </div>

                        </div>
                    </div>
                </div>

            </div>--%>
            <div class="row mb-4">
                <div class="col-xl-3 col-lg-6">
                    <div class="card l-bg-style1">
                        <div class="card-statistic-3">
                            <div class="card-icon card-icon-large"><i class="fa fa-award"></i></div>
                            <div class="card-content">
                                <h4 class="card-title">Total Incentive</h4>
                                <span>
                                    <asp:Label ID="LblTotalIncentive" runat="server"></asp:Label></span>
                                <div class="progress1 mt-1 mb-1" data-height="8">
                                </div>
                                <p class="mb-0 text-sm">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-3 col-lg-6">
                    <div class="card l-bg-style2">
                        <div class="card-statistic-3">
                            <div class="card-icon card-icon-large"><i class="fa fa-briefcase"></i></div>
                            <div class="card-content">
                                <h4 class="card-title">E Wallet Balance</h4>
                                <span>
                                    <asp:Label ID="LblEWalletBalnace" runat="server"></asp:Label></span>
                                <div class="progress1 mt-1 mb-1" data-height="8">
                                </div>
                                <p class="mb-0 text-sm">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-3 col-lg-6">
                    <div class="card l-bg-style3">
                        <div class="card-statistic-3">
                            <div class="card-icon card-icon-large"><i class="fa fa-globe"></i></div>
                            <div class="card-content">
                                <h4 class="card-title">R Wallet Balance</h4>
                                <span>
                                    <asp:Label ID="LblRWalletBalnace" runat="server"></asp:Label></span>
                                <div class="progress1 mt-1 mb-1" data-height="8">
                                </div>
                                <p class="mb-0 text-sm">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-3 col-lg-6">
                    <div class="card l-bg-style4">
                        <div class="card-statistic-3">
                            <div class="card-icon card-icon-large"><i class="fa fa-money-bill-alt"></i></div>
                            <div class="card-content">
                                <h4 class="card-title">G Wallet Balance</h4>
                                <span>
                                    <asp:Label ID="LblGWalletBalnace" runat="server"></asp:Label></span>
                                <div class="progress1 mt-1 mb-1" data-height="8">
                                </div>
                                <p class="mb-0 text-sm">
                                </p>
                            </div>
                        </div>
                    </div>
                </div>







            </div>



            <div class="row">



                <div class="col-12 col-sm-12 col-md-12 col-lg-6 col-xl-6">
                    <div class="card">
                        <%-- <div class="card-header">
                            <h4>Activation Business</h4>
                        </div>
                        <span id="Span8"><a href="MyPurchase.aspx">[View]</a></span>--%>
                        <div class="card-header d-flex justify-content-between align-items-center">
                            <h4 class="mb-0">Activation Business</h4>
                            <a href="DownlinePurchase.aspx" style="font-size:15px;">[View]</a>
                        </div>
                        <div class="card-body">
                            <div class="table-responsive" id="scroll">
                                <table class="table table-hover table-xl mb-0">
                                    <thead>
                                        <tr>
                                            <th>Name</th>
                                            <th>Left</th>
                                            <th>Right</th>
                                            <th>Total</th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RptActivation" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="text-truncate"><%#Eval("Name")%></td>
                                                    <td class="text-truncate">
                                                        <%#Eval("Left")%></td>
                                                    <td class="text-truncate">
                                                        <%#Eval("Right")%></td>
                                                    <td class="text-truncate">
                                                        <%#Eval("Total")%></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>




                <div class="col-12 col-sm-12 col-md-12 col-lg-6 col-xl-6">
                    <div class="card">
                        <div class="card-header d-flex justify-content-between align-items-center">
                            <h4 class="mb-0">Repurchase Business</h4>
                            <a href="DownlinePurchase.aspx" style="font-size:15px;">[View]</a>
                        </div>
                        <%-- <div class="card-header">
                            <h4>Repurchase Business</h4>
                        </div>
                         <span id="Span8"><a href="MyPurchase.aspx">[View]</a></span>--%>
                        <div class="card-body">
                            <div class="table-responsive" id="scroll">
                                <table class="table table-hover table-xl mb-0">
                                    <thead>
                                        <tr>
                                            <th>Name</th>
                                            <th>Left</th>
                                            <th>Right</th>
                                            <th>Total</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RptRepuchase" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="text-truncate"><%#Eval("Name")%></td>
                                                    <td class="text-truncate">
                                                        <%#Eval("Left")%></td>
                                                    <td class="text-truncate">
                                                        <%#Eval("Right")%></td>
                                                    <td class="text-truncate">
                                                        <%#Eval("Total")%></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <div class="row">

                <div class="col-12 col-sm-12 col-md-12 col-lg-6 col-xl-6">
                    <div class="card">
                        <div class="card-header d-flex justify-content-between align-items-center">
                            <h4 class="mb-0">Self Business</h4>
                            <a href="MyPurchase.aspx" style="font-size:15px;">[View]</a>
                        </div>

                        <%--<div class="card-header">
                            <h4>Self Business</h4>
                        </div>
                         <span id="Span8"><a href="MyPurchase.aspx">[View]</a></span>--%>
                        <div class="card-body">
                            <div class="table-responsive" id="scroll">
                                <table class="table table-hover table-xl mb-0">
                                    <thead>
                                        <tr>
                                            <th>Name</th>
                                            <th>Activation</th>
                                            <th>Repurchase</th>
                                            <th>Total</th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RptSelfBV" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="text-truncate"><%#Eval("Name")%></td>
                                                    <td class="text-truncate">
                                                        <%#Eval("MonthlySelfBV")%></td>
                                                    <td class="text-truncate">
                                                        <%#Eval("MonthlySelfRepurcBV")%></td>
                                                    <td class="text-truncate">
                                                        <%#Eval("Total")%></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-12 col-sm-12 col-md-12 col-lg-6 col-xl-6">
                    <div class="card">
                        <div class="card-header text-center">
                            <h4 class="mb-0">Help and Support</h4>
                        </div>

                        <div class="card-body">
                            <div class="border rounded p-4" style="background: #ffffff;">

                                <!-- Row 1: Small subheading (optional but nice look) -->
                                <div class="text-center mb-3">
                                    <span class="text-muted" style="font-size: 14px;">We are always here to help you</span>
                                </div>
                                <!-- Row 2: Contact items -->
                                <div class="d-flex justify-content-center flex-wrap gap-5 mb-3">

                                    <div class="d-flex align-items-center">
                                        <i class="fa-solid fa-phone text-primary fs-5 me-2"></i>
                                        <span class="fw-semibold">
                                            <asp:Label ID="LblMobileNob" runat="server"></asp:Label></span>
                                    </div>

                                    <div class="d-flex align-items-center">
                                        <i class="fa-solid fa-envelope text-danger fs-5 me-2"></i>
                                        <span class="fw-semibold">
                                            <asp:Label ID="Lblemail" runat="server"></asp:Label></span>
                                    </div>

                                    <div class="d-flex align-items-center">
                                        <i class="fa-solid fa-globe text-success fs-5 me-2"></i>
                                        <span class="fw-semibold">
                                            <asp:Label ID="LblWebsite" runat="server"></asp:Label></span>
                                    </div>

                                </div>

                                <!-- Row 3 : bottom clean line -->
                                <div style="border-top: 1px solid #e2e2e2;"></div>

                            </div>
                        </div>
                    </div>
                </div>






            </div>

        </section>

    </div>

    <%-- <script>
        function openPhotoModal() {
            var modal = new bootstrap.Modal(document.getElementById('profilePhotoModal'));
            modal.show();
        }

        // open file chooser
        function openFilePicker() {
            document.getElementById('<%= FileUpload1.ClientID %>').click();
        }

        // instant preview after selecting image
        function previewPhoto(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    // show preview inside modal
                    document.getElementById("ContentPlaceHolder1_photoPreview").src = e.target.result;
                };

                reader.readAsDataURL(input.files[0]);
            }
        }


    </script>--%>
</asp:Content>


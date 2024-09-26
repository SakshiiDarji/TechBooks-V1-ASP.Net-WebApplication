            <%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="TechBooks.WebUI.Login.SignIn" %>

<asp:Content ContentPlaceHolderID="BodyContent" runat="server">

    <div class="row">
        <div class="col-md-6">
            <fieldset class="border p-2">
                <legend class="float-none w-auto">Sign In</legend>
                <asp:Label Text="E-mail:" AssociatedControlID="txtEmail" runat="server"></asp:Label>
                <asp:RequiredFieldValidator ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="E-mail is required" runat="server"   CssClass="text-danger" />
                <asp:TextBox ID="txtEmail" placeholder="Type your e-mail here" CssClass="form-control" runat="server" TextMode="Email" ></asp:TextBox>
                
                <asp:Label Text="Password:" AssociatedControlID="txtPassword" runat="server"></asp:Label>
                <asp:RequiredFieldValidator ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="Password is required" runat="server"   CssClass="text-danger" />
                <asp:TextBox ID="txtPassword" placeholder="Type your password here" CssClass="form-control" runat="server" TextMode="Password" ></asp:TextBox>
                
                <asp:Button ID="btnSignIn" Text="Sign in" CssClass="btn btn-primary vspace" runat="server"  OnClick="btnSignIn_Click" />
            </fieldset>
            <br />
            <p>New User?</p>
            <a href="Register.aspx">Click here to register</a>
        </div>
    </div>

</asp:Content>


        
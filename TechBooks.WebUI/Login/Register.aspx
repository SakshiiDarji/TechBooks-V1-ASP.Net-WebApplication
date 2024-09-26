<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TechBooks.WebUI.Login.Register" %>

<asp:Content ContentPlaceHolderID="BodyContent" runat="server">

    <div class="row">
        <div class="col-md-6">
            <fieldset class="border p-2">
                <legend class="float-none w-auto">Register</legend>
                <asp:Label Text="E-mail:" AssociatedControlID="txtNewEmail" runat="server"></asp:Label>
                <asp:RequiredFieldValidator ControlToValidate="txtNewEmail" Display="Dynamic" ErrorMessage="E-mail is required" runat="server" ValidationGroup="newuser"  CssClass="text-danger" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="E-mail is invalid" ControlToValidate="txtNewEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="newuser"  CssClass="text-danger"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtNewEmail" placeholder="Type your e-mail here" CssClass="form-control" runat="server" TextMode="Email" ValidationGroup="newuser"></asp:TextBox>
                

                <asp:Label Text="Password:" AssociatedControlID="txtNewPassword1" runat="server"></asp:Label>
                <asp:RequiredFieldValidator ControlToValidate="txtNewPassword1" Display="Dynamic" ErrorMessage="Password is required" runat="server" ValidationGroup="newuser"  CssClass="text-danger" />
                <asp:TextBox ID="txtNewPassword1" placeholder="Type your password here" CssClass="form-control" Display="Dynamic" runat="server" TextMode="Password" ValidationGroup="newuser"></asp:TextBox>

                <asp:Label Text="Repeat your password:" AssociatedControlID="txtNewPassword2" runat="server"></asp:Label>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="The passwords do not match" ControlToValidate="txtNewPassword1" ControlToCompare="txtNewPassword2" ValidationGroup="newuser"  CssClass="text-danger"></asp:CompareValidator>
                <asp:TextBox ID="txtNewPassword2" placeholder="Repeat your password here" CssClass="form-control" Display="Dynamic" runat="server" TextMode="Password" ValidationGroup="newuser"></asp:TextBox>
                

                <asp:Button ID="btnRegister" Text="Register" CssClass="btn btn-primary vspace" runat="server" ValidationGroup="newuser" OnClick="btnRegister_Click" />
            </fieldset>
            <br />
            <p>Already have an account?</p>
            <a href="SignIn.aspx">Click here to sign in</a>
        </div>
    </div>

</asp:Content>

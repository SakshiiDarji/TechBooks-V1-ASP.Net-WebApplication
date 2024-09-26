            <%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="TechBooks.WebUI.Login.Success" %>

<asp:Content ContentPlaceHolderID="BodyContent" runat="server">
    <div class="alert alert-success alert-dismissable d-flex justify-content-between">
        Your user has been successfully been created
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close" />
    </div>

    <a href="SignIn.aspx" class="btn btn-primary">Click here to sign in</a>
</asp:Content>

        
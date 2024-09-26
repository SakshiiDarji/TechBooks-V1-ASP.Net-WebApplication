<%@ Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddOrUpdate.aspx.cs" Inherits="TechBooks.WebUI.ManageAuthors.AddOrUpdate" %>

<asp:Content ContentPlaceHolderID="BodyContent" runat="server">

    <div class="row">
        <div class="col-md-6">
            
            <asp:Label AssociatedControlID="txtName" CssClass="control-label" runat="server">Name</asp:Label>
            <asp:RequiredFieldValidator ErrorMessage="Name is required" ControlToValidate="txtName" Display="Dynamic" CssClass="text-danger" runat="server"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>

            <asp:Label AssociatedControlID="txtEmail" CssClass="control-label" runat="server">Email</asp:Label>
            <asp:RequiredFieldValidator ErrorMessage="Email is required" ControlToValidate="txtEmail" Display="Dynamic" CssClass="text-danger" runat="server"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ErrorMessage="E-mail is invalid" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" CssClass="text-danger" runat="server"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>

            <a href="/ManageAuthors/" class="btn btn-outline-secondary vspace">Back</a>

            <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn btn-primary vspace" OnClick="btnSubmit_Click" />
        </div>
    </div>


</asp:Content>

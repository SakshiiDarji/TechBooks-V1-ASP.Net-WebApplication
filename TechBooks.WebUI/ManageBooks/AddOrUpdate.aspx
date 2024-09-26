            <%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddOrUpdate.aspx.cs" Inherits="TechBooks.WebUI.ManageBooks.AddOrUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">

    <div class="row">
        <div class="col-md-6">
            
            <asp:Label AssociatedControlID="txtTitle" CssClass="control-label" runat="server">Title</asp:Label>
            <asp:RequiredFieldValidator ErrorMessage="Title is required" ControlToValidate="txtTitle" Display="Dynamic" CssClass="text-danger" runat="server"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtTitle" CssClass="form-control" runat="server"></asp:TextBox>

            <asp:Label AssociatedControlID="ddlCategory" CssClass="control-label" runat="server">Category</asp:Label>
            <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server"></asp:DropDownList>

            <a href="/ManageBooks/" class="btn btn-outline-secondary vspace">Back</a>

            <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn btn-primary vspace" OnClick="btnSubmit_Click" />
        </div>
    </div>

</asp:Content>

        
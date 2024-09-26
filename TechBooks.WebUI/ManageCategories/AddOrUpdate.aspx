<%@ Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddOrUpdate.aspx.cs" Inherits="TechBooks.WebUI.ManageCategories.AddOrUpdate" %>

<asp:Content ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-md-6">
            
            <asp:Label AssociatedControlID="txtDescription" CssClass="control-label" runat="server">Description</asp:Label>
            <asp:RequiredFieldValidator ErrorMessage="Description is required" ControlToValidate="txtDescription" Display="Dynamic" CssClass="text-danger" runat="server"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server"></asp:TextBox>

            <a href="/ManageCategories/" class="btn btn-outline-secondary vspace">Back</a>

            <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn btn-primary vspace" OnClick="btnSubmit_Click" />
        </div>
    </div>
</asp:Content>

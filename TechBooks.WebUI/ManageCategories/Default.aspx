<%@ Page Title="Manage Categories" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TechBooks.WebUI.ManageCategories.Default" %>

<asp:Content ContentPlaceHolderID="BodyContent" runat="server">
    <p>
        <a class="btn btn-primary" href="AddOrUpdate.aspx">Click here to Add</a>
    </p>

    <asp:DataGrid ID="dgCategories" CssClass="table" AutoGenerateColumns="False" OnItemCommand="dgCategories_ItemCommand" runat="server">
        <HeaderStyle Font-Bold="true" />
        <Columns>
            <asp:BoundColumn DataField="CategoryId" HeaderText="CategoryId"></asp:BoundColumn>
            <asp:BoundColumn DataField="Description" HeaderText="Description"></asp:BoundColumn>
            <asp:TemplateColumn>
                <ItemTemplate>
                    <asp:HyperLink CssClass="btn btn-outline-primary" NavigateUrl=<%# "AddOrUpdate.aspx?CategoryId=" + Eval("CategoryId") %> runat="server">Update</asp:HyperLink>

                    <asp:LinkButton ID="btnRemove" runat="server" 
                        CommandName="RemoveCategory" 
                        CommandArgument='<%# Eval("CategoryId") %>' 
                        CssClass="btn btn-danger"
                        OnClientClick="return removeConfirmation();">Remove</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
</asp:Content>

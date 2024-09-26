<%@ Page Title="Manage Authors" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TechBooks.WebUI.ManageAuthors.Default" %>

<asp:Content ContentPlaceHolderID="BodyContent" runat="server">
    <p>
        <a class="btn btn-primary" href="AddOrUpdate.aspx">Click here to Add</a>
    </p>

    <asp:DataGrid ID="dgAuthors" CssClass="table" AutoGenerateColumns="False" OnItemCommand="dgAuthors_ItemCommand" runat="server">
        <HeaderStyle Font-Bold="true" />
        <Columns>
            <asp:BoundColumn DataField="AuthorId" HeaderText="AuthorId"></asp:BoundColumn>
            <asp:BoundColumn DataField="Name" HeaderText="Name"></asp:BoundColumn>
            <asp:BoundColumn DataField="Email" HeaderText="Email"></asp:BoundColumn>
            <asp:TemplateColumn>
                <ItemTemplate>
                    <asp:HyperLink CssClass="btn btn-outline-primary" NavigateUrl=<%# "AddOrUpdate.aspx?AuthorId=" + Eval("AuthorId") %> runat="server">Update</asp:HyperLink>

                    <asp:HyperLink CssClass="btn btn-outline-primary" NavigateUrl=<%# "../ManageAuthorBooks/Default.aspx?AuthorId=" + Eval("AuthorId") %> runat="server">Manage Author Books</asp:HyperLink>

                    <asp:LinkButton ID="btnRemove" runat="server" 
                        CommandName="RemoveAuthor" 
                        CommandArgument='<%# Eval("AuthorId") %>' 
                        CssClass="btn btn-danger"
                        OnClientClick="return removeConfirmation();">Remove</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
</asp:Content>

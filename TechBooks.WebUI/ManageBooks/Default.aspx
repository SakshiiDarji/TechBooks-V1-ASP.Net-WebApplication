<%@ Page Title="Manage Books" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TechBooks.WebUI.ManageBooks.Default" %>

<asp:Content ContentPlaceHolderID="BodyContent" runat="server">
    <p>
        <a class="btn btn-primary" href="AddOrUpdate.aspx">Click here to Add</a>
    </p>

    <asp:DataGrid ID="dgBooks" CssClass="table" AutoGenerateColumns="False" OnItemCommand="dgBooks_ItemCommand" runat="server">
        <HeaderStyle Font-Bold="true" />
        <Columns>
            <asp:BoundColumn DataField="BookId" HeaderText="BookId"></asp:BoundColumn>
            <asp:BoundColumn DataField="Title" HeaderText="Title"></asp:BoundColumn>

            <asp:TemplateColumn HeaderText="Category">
                <ItemTemplate>
                    <%# GetCategoryDescription(Eval("CategoryId")) %>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn>
                <ItemTemplate>
                    <asp:HyperLink CssClass="btn btn-outline-primary" NavigateUrl=<%# "AddOrUpdate.aspx?BookId=" + Eval("BookId") %> runat="server">Update</asp:HyperLink>

                    <asp:HyperLink CssClass="btn btn-outline-primary" NavigateUrl=<%# "../ManageBookAuthors/Default.aspx?BookId=" + Eval("BookId") %> runat="server">Manage Book Authors</asp:HyperLink>

                    <asp:LinkButton ID="btnRemove" runat="server" 
                        CommandName="RemoveBook" 
                        CommandArgument='<%# Eval("BookId") %>' 
                        CssClass="btn btn-danger"
                        OnClientClick="return removeConfirmation();">Remove</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
</asp:Content>

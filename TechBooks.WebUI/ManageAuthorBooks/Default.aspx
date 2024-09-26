<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TechBooks.WebUI.ManageAuthorBooks.Default" %>

<asp:Content ContentPlaceHolderID="BodyContent" runat="server">
    
    <asp:Panel ID="pnlBooksToAdd" runat="server">
        <asp:DropDownList ID="ddlBook" onchange="selectChange(this)" runat="server"></asp:DropDownList>
        <asp:Button ID="btnAddBook" Text="Click here to Add" CssClass="btn btn-primary btnAddBook" OnClick="btnAddBook_Click" disabled="disabled" runat="server" />
    </asp:Panel>

    <br />

    <asp:Panel ID="pnlNothingToAdd" Visible="false" runat="server">
        <p>This author is associated with all books from our catalog.</p>
    </asp:Panel>

    <asp:Panel ID="pnlNoAssociation" Visible="false" runat="server">
        <p>This author is not associated with any Book.</p>
    </asp:Panel>

    <asp:Panel ID="pnlAssociations" Visible="false" runat="server">
        <p><strong>Author of:</strong></p>
        <asp:DataGrid ID="dgAuthorBooks" CssClass="table" AutoGenerateColumns="False" OnItemCommand="dgAuthorBooks_ItemCommand" runat="server">
        <HeaderStyle Font-Bold="true" />
        <Columns>
            <asp:BoundColumn DataField="BookId" HeaderText="BookId"></asp:BoundColumn>
            <asp:BoundColumn DataField="Title" HeaderText="Title"></asp:BoundColumn>
            <asp:TemplateColumn>
                <ItemTemplate>
                    <asp:LinkButton ID="btnRemove" runat="server" 
                        CommandName="RemoveBook" 
                        CommandArgument='<%# Eval("BookId") %>' 
                        CssClass="btn btn-danger"
                        OnClientClick="return removeConfirmation();">Remove</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
    </asp:Panel>

    <script>
        function selectChange(select) {
            if (select.selectedIndex == 0)
                document.getElementsByClassName("btnAddBook")[0].disabled = "disabled";
            else
                document.getElementsByClassName("btnAddBook")[0].disabled = "";
        }
    </script>
</asp:Content>

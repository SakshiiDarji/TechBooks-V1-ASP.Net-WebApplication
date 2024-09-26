<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TechBooks.WebUI.ManageBookAuthors.Default" %>

<asp:Content ContentPlaceHolderID="BodyContent" runat="server">
     <asp:Panel ID="pnlAuthorsToAdd" runat="server">
        <asp:DropDownList ID="ddlAuthor" onchange="selectChange(this)" runat="server"></asp:DropDownList>
        <asp:Button ID="btnAddAuthor" Text="Click here to Add" CssClass="btn btn-primary btnAddAuthor" OnClick="btnAddAuthor_Click" disabled="disabled" runat="server" />
    </asp:Panel>

    <br />

    <asp:Panel ID="pnlNothingToAdd" Visible="false" runat="server">
        <p>This book is associated with all authors from our catalog.</p>
    </asp:Panel>

    <asp:Panel ID="pnlNoAssociation" Visible="false" runat="server">
        <p>This book is not associated with any Author.</p>
    </asp:Panel>

    <asp:Panel ID="pnlAssociations" Visible="false" runat="server">
        <p><strong>Authors:</strong></p>
        <asp:DataGrid ID="dgBookAuthors" CssClass="table" AutoGenerateColumns="False" OnItemCommand="dgBookAuthors_ItemCommand" runat="server">
        <HeaderStyle Font-Bold="true" />
        <Columns>
            <asp:BoundColumn DataField="AuthorId" HeaderText="AuthorId"></asp:BoundColumn>
            <asp:BoundColumn DataField="Name" HeaderText="Name"></asp:BoundColumn>
            <asp:TemplateColumn>
                <ItemTemplate>
                    <asp:LinkButton ID="btnRemove" runat="server" 
                        CommandName="RemoveAuthor" 
                        CommandArgument='<%# Eval("AuthorId") %>' 
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
                document.getElementsByClassName("btnAddAuthor")[0].disabled = "disabled";
            else
                document.getElementsByClassName("btnAddAuthor")[0].disabled = "";
        }
    </script>
</asp:Content>

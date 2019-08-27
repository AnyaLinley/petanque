<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddMember.aspx.cs" Inherits="Petanque.AddMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />

    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" />

    <asp:FormView runat="server" ID="addUserForm" ItemType="Petanque.Models.User"
        InsertMethod="addUserForm_InsertItem" DefaultMode="Insert"
        RenderOuterTable="false" OnItemInserted="addUserForm_ItemInserted">
        <InsertItemTemplate>
            <fieldset>
                <ol>
                    <asp:DynamicEntity runat="server" Mode="Insert" />
                </ol>
                <asp:Button runat="server" Text="Insert" CommandName="Insert" />
                <asp:Button runat="server" Text="Cancel" CausesValidation="false" OnClick="cancelButton_Click" />
            </fieldset>
        </InsertItemTemplate>
    </asp:FormView>


</asp:Content>

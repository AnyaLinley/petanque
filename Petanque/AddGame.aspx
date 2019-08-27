<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddGame.aspx.cs" Inherits="Petanque.AddGame" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />

            <fieldset>
                <asp:Label ID="lWinner1" runat="server" Text="Winner 1: "></asp:Label>
                <asp:DropDownList runat="server" ID="ddWinner1" Width="150px"></asp:DropDownList><br /><br />
                
                <asp:Label ID="lWinner2" runat="server" Text="Winner 2: "></asp:Label>
                <asp:DropDownList runat="server" ID="ddWinner2" Width="150px"></asp:DropDownList><br /><br />
                
                <asp:Label ID="lWinner3" runat="server" Text="Winner 3: "></asp:Label>
                <asp:DropDownList runat="server" ID="ddWinner3" Width="150px"></asp:DropDownList><br /><br />
                
                <asp:Label ID="lLoser1" runat="server" Text="Loser 1: "></asp:Label>
                <asp:DropDownList runat="server" ID="ddLoser1" Width="150px"></asp:DropDownList><br /><br />
                
                <asp:Label ID="lLoser2" runat="server" Text="Loser 2: "></asp:Label>
                <asp:DropDownList runat="server" ID="ddLoser2" Width="150px"></asp:DropDownList><br /><br />
                
                <asp:Label ID="lLoser3" runat="server" Text="Loser 3: "></asp:Label>
                <asp:DropDownList runat="server" ID="ddLoser3" Width="150px"></asp:DropDownList><br /><br />
                
                <asp:Label ID="lLosingScore" runat="server" Text="Score: 13 vs "></asp:Label>
                <asp:DropDownList runat="server" ID="ddLosingScore"></asp:DropDownList><br /><br />

                <asp:Label ID="lDate" runat="server" Text="Date: "></asp:Label>
                <asp:TextBox ID="tbDate" runat="server" Width="80px"></asp:TextBox>
                <asp:LinkButton ID="lbDate" runat="server" OnClick="lbDate_Click">Select</asp:LinkButton>
                <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar><br /><br />

                <asp:Label ID="lLeague" runat="server" Text="League: "></asp:Label>
                <asp:DropDownList runat="server" ID="ddLeague"></asp:DropDownList><br /><br />
                
                <asp:Button runat="server" Text="Insert" OnClick="insertButton_Click" />
                <asp:Button runat="server" Text="Cancel" CausesValidation="false" OnClick="cancelButton_Click" /><br /><br />

                <asp:Label runat="server" Text="" ID="lValidation"></asp:Label>

            </fieldset>

</asp:Content>

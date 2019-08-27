<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Petanque._Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
         
    <br />

    <p>Scores for a league are calculated as follows:</p>
    <ul>
        <li>Only the best eight games count for scoring purposes.</li>
        <li>Players score 3 points for a win and 1 point for a loss.</li>
    </ul>

    <hr />

    <asp:Label runat="server" Text="Select League: "></asp:Label>
    <asp:DropDownList ID="ddLeague" runat="server" AutoPostBack="True" onselectedindexchanged="ddLeague_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:Label runat="server" Text="Select Year: "></asp:Label>
    <asp:DropDownList ID="ddYear" runat="server" AutoPostBack="True" onselectedindexchanged="ddYear_SelectedIndexChanged">
    </asp:DropDownList>

    <br /><br />

    <asp:Chart ID="Chart1" runat="server" Width="400px" Palette="Bright" BorderlineColor="Black" BorderlineDashStyle="Solid">
        <Titles>
            <asp:Title Text="League Scores"></asp:Title>
        </Titles>
        <Series>
            <asp:Series Name="Series1" ChartArea="ChartArea1" ChartType="Bar">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
                <AxisX Title="Member" Interval="1"></AxisX>
                <AxisY Title="Score"></AxisY>
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>

    <br />

    <asp:Label runat="server" ID="lError" Text=""></asp:Label>

    <hr />

    <asp:Label runat="server" Text="Select Club Member: "></asp:Label>
    <asp:DropDownList ID="ddUser" runat="server" AutoPostBack="True" onselectedindexchanged="ddUser_SelectedIndexChanged">
    </asp:DropDownList>

    <br />
    <br />

    <asp:Label runat="server" ID="userInfoLabel" Text=""></asp:Label>

    <br />
    <br />

    <asp:GridView runat="server" ID="gamesGrid" ItemType="Petanque.Models.Game"  DataKeyNames="GameID" 
        SelectMethod="gamesGrid_GetData" AutoGenerateColumns="False" OnRowDataBound="gamesGrid_RowDataBound">
        <Columns>
            
            <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Winner 1" SortExpression="Winner1Id">
                <ItemTemplate>
                    <asp:Label ID="Winner1Label" runat="server" DataField="Winner1Id" Text="<%# GetUserNameById(Item.Winner1Id) %>" Mode="ReadOnly" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Winner 2" SortExpression="Winner2Id">
                <ItemTemplate>
                    <asp:Label ID="Winner2Label" runat="server" DataField="Winner2Id" Text="<%# GetUserNameById(Item.Winner2Id) %>" Mode="ReadOnly" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Winner 3" SortExpression="Winner3Id">
                <ItemTemplate>
                    <asp:Label ID="Winner3Label" runat="server" DataField="Winner3Id" Text="<%# GetUserNameById(Item.Winner3Id) %>" Mode="ReadOnly" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Loser 1" SortExpression="Loser1Id">
                <ItemTemplate>
                    <asp:Label ID="Loser1Label" runat="server" DataField="Loser1Id" Text="<%# GetUserNameById(Item.Loser1Id) %>" Mode="ReadOnly" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Loser 2" SortExpression="Loser2Id">
                <ItemTemplate>
                    <asp:Label ID="Loser2Label" runat="server" DataField="Loser2Id" Text="<%# GetUserNameById(Item.Loser2Id) %>" Mode="ReadOnly" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Loser 3" SortExpression="Loser3Id">
                <ItemTemplate>
                    <asp:Label ID="Loser3Label" runat="server" DataField="Loser3Id" Text="<%# GetUserNameById(Item.Loser3Id) %>" Mode="ReadOnly" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Losing Score" SortExpression="LosingScore">
                <ItemTemplate>
                    <asp:Label ID="LosingScoreLabel" runat="server" DataField="LosingScore" Text="<%# Item.LosingScore %>" Mode="ReadOnly" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:DynamicField DataField="Date" />
            <asp:DynamicField DataField="League" />

        </Columns>
    </asp:GridView>

</asp:Content>

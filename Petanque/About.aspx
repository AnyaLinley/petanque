<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Petanque.About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />

    <h2>Introduction</h2>

    <p>This is my first project using ASP.NET. It uses the entity framework with a code first approach. 
        I wanted to use a real world example and decided on a site that keeps track of club members, games and league scores in the game of petanque.</p>

    <p>As a member of my local petanque club, I know a little about the game and am kept up to date with the season scores via emails from the
        club secretary. The scores are drawn up in Excel and look something like this.</p>

    <asp:Image runat="server" ImageUrl="~/img/petanque_spring.png" alt="Petanque Scores" width="600">
    </asp:image>

    <p>The spreadsheet is quite difficult to read, let alone understand, Also, it isn't available to browse online. Having a database of members
        and games played with a home page that figures out the overall scores and displays them as a graph addresses these issues.</p>

    <h2>Learning and Development Process</h2>

    <p>I began by watching through <a href="https://www.pluralsight.com/courses/aspdotnet-webforms4-intro" target="_blank">this video tutorial series</a>
        provided by Pluralsight. It was a great introduction to ASP.NET but somewhat out of date when it came to the practicalities of making a site
        and so this example started out by following 
        <a href="https://docs.microsoft.com/en-us/aspnet/web-forms/overview/presenting-and-managing-data/model-binding/" target="_blank">
        an official Microsoft tutorial</a> which uses a code first model to demonstrate a simple database linking students to courses.
         The tutorial in question creates a third 'enrollment' table to model a many-to-many link between students and courses. However, 
        I realised that I wanted something a little different for my example. Each user (i.e. club member) could be involved in many games 
        but each game could only have up to three winners and three losers and it made sense to enter them together as a unit. Therefore,
        I needed each possible player to be a separate foreign key within a game. 
        After a bit of reserach, I discovered the inverse property and this produced the database I wanted.</p>

    <p>The Microsoft tutorial uses dynamic data templates for the grid view which I found to be both a blessing and a curse. 
        I can see the benefit of automated templates with built in error checking but I found that the foreign key fields in particular, did not 
        display how I wanted them to. A dropdown list if IDs is incredibly unintuitive and leads to errors if you select an ID that
        doesn't exist. It took me a long time to figure out how to display a dropdown of names instead and to change the ID associated with
        that name to the foreign key in the game when edited. My work around is probably not the most elegant but it does 
        the trick. I didn't realise that this stage of development that a dropdown list item has both a value and a text property and I'me sure this 
        would have helped. Instead, my solution was to store the edited names of winners and losers as class variables during the row update 
        event and then use these variables to update the database in the item update for the grid view.</p>

    <p>Another challenge I faced was how to deal with user deletion. I didn't want to prevent deletion, nor did I want the affected games to 
        show an empty slot when a player no longer exists. Games are allowed to have blank player slots in them and I wanted to differentiate 
        between a player that has been deleted from the database and no player at all. My solution was to make all player slots non-null and to introduce
        the special user names, [Noone] and [Deleted]. I then turned off WillCascadeOnDelete for all foreign keys and wrote a bit of extra code
        to handle deletion so that the relevant games show [Deleted] instead. To make this obvious in the grid view of games, any cell with a [Deleted] 
        player displays in red. The drawback of this approach is that [Noone] and [Deleted] must always be present in the database.</p>

    <p>While I didn't implement much by way of validation, errors in name slecection and scoring are prevented by restricting input to valid choices 
        in a drop down list. Additionally, you can select a date using the date picker when adding a new game to the database.</p>

    <p>To display the scores for a seasonal league, I decided to use a bar chart since this in keeping with the spreadsheet data
        above. For more detailed information about a particular club member, you can select someone from the dropdown list below the chart.
        You will then see their total score highlighted in the chart and a list of all games that member took part in for the selected league.</p>

    <h2>Possible Improvements and Additions</h2>

    <p>This is a backend only demo and would definitely look a lot better with some frontend work, using Bootstap 4 and some additional custom 
        styling on top.</p>

    <p>As far as code goes, this was my first foray into ASP.NET and I feel I know so much more at the end of this project than I did at the
        start. I've kept things as they are to preserve my learning experience but if I were to start again I would probably do a few things 
        differently. In particular, I would find a better way of editing the foreign keys that doesn't require storing class variables 
        between event methods.</p>
        
     <p>It would nice to add some extra validation to make sure the same name isn't used more than once
        within a game and that there is always at least one winner and one loser.</p>

    <p>In petanque scoring, the 'score difference' is used to differentiate between players with the same leage score. This is the cumulaive score 
        difference of all (counted) games played. I've kept things simple here but a more complete solution would have included the score difference 
        as well.</p> 

    <p>Ideally, a site like this would have two levels of membership login - one for regular club members to view and track their 
        stats and one for admin who should be the only people who can edit the lists of users and games. At the very least, admin login
        would be necessary to prevent anyone visiting the site from editing the database. Again, to keep things simple, I chose not to tackle 
        login systems or security for this example.</p>
   
</asp:Content>

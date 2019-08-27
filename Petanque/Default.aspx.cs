using Petanque.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace Petanque
{
    public partial class _Default : PetanquePage
    {
        // keep track of selected user details
        int numGamesWonByUser = 0; // number of games the selected user won in the selected league
        int numGamesLostByUser = 0; // number of games the selected user lost in the selected league
        int userScore = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddLeague.DataSource = Enum.GetNames(typeof(LeagueType));
                ddLeague.DataBind();
                ddYear.DataSource = GetYears();
                ddYear.DataBind();
                ddUser.DataSource = GetUserNamesWithoutDeleted();
                ddUser.DataBind();
                ddUser.SelectedValue = "[Noone]";
                GetChartData();
            }
            else
            {
                GetChartData();
                gamesGrid_GetData();
                gamesGrid.DataBind();
                UpdateUserInfo();
            }
        }

        protected void ddLeague_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void ddYear_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void ddUser_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void GetChartData()
        {
            Series series = Chart1.Series["Series1"];
            List<User> users = GetUsersByLeague((LeagueType)Enum.Parse(typeof(LeagueType), ddLeague.SelectedItem.Text), Int32.Parse(ddYear.SelectedItem.Text));
            if (users == null)
            {
                return;
            }
            for (int i=0; i<users.Count; i++)
            {
                series.Points.AddXY(users[i].Name, users[i].Score);
                if (users[i].Name == ddUser.SelectedValue)
                {
                    series.Points[i].Color = Color.BlueViolet; // #e6d2f9 for lighter shade
                }
                else
                {
                    series.Points[i].Color = Color.Green;
                }
            }
            series.Sort(PointSortOrder.Ascending, "Y");
        }

        private void UpdateUserInfo()
        {
            if (ddUser.SelectedValue == "[Noone]")
            {
                userInfoLabel.Text = "";
            }
            else if ((numGamesWonByUser + numGamesLostByUser) == 0)
            {
                userInfoLabel.Text = ddUser.SelectedValue + " didn't play any games during the " + ddLeague.SelectedItem.Text + " " 
                    + ddYear.SelectedValue + " league.";
            }
            else if ((numGamesWonByUser + numGamesLostByUser) <= 8)
            {
                userInfoLabel.Text = ddUser.SelectedValue + " played a total of " + (numGamesWonByUser + numGamesLostByUser).ToString() 
                    + " games in the " + ddLeague.SelectedItem.Text + " " + ddYear.SelectedValue + " league, winning " + numGamesWonByUser.ToString()
                    + " and losing " + numGamesLostByUser.ToString() + ". This gives a league score of " + userScore.ToString() + ".";
            }
            else if ((numGamesWonByUser + numGamesLostByUser) > 8)
            {
                userInfoLabel.Text = ddUser.SelectedValue + " played a total of " + (numGamesWonByUser + numGamesLostByUser).ToString()
                    + " games in the " + ddLeague.SelectedItem.Text + " " + ddYear.SelectedValue + " league, winning " + numGamesWonByUser.ToString()
                    + " and losing " + numGamesLostByUser.ToString() + ". Only the best 8 of these games count towards a league score of " 
                    + userScore.ToString() + ".";
            }
        }

        private List<int> GetUserScores()
        {
            using (PetanqueContext db = new PetanqueContext())
            {
                return db.Users.Select(u => u.TestScore).ToList();
            }
        }

        private List<User> GetUsersByLeague(LeagueType league, int year)
        {
            using (PetanqueContext db = new PetanqueContext())
            {
                // Get list of all games played during the selected league
                List<Game> games = db.Games.Where(g => g.League == league).Where(g => g.Date.Year == year).ToList();
                if (games.Count == 0)
                {
                    // no games played so hide chart, display error message and return
                    Chart1.Visible = false;
                    lError.Text = "No games were played during the selected time period.";
                    return null;
                }

                Chart1.Visible = true;
                lError.Text = "";
                
                // Get list of all valid petanque members (users)
                List<User> users = db.Users.Where(u => u.Name != "[Deleted]" && u.Name != "[Noone]").ToList();
                
                // Loop through each user and work out their league score. Store this in their Score class property
                foreach (User user in users)
                {
                    user.NumGamesWon = 0;
                    user.NumGamesLost = 0;
                    
                    // loop through all games in league to total number of wins and losses for the user
                    foreach (Game game in games)
                    {
                        if (game.Winner1 == user || game.Winner2 == user || game.Winner3 == user)
                        {
                            user.NumGamesWon++;
                        }
                        else if (game.Loser1 == user || game.Loser2 == user || game.Loser3 == user)
                        {
                            user.NumGamesLost++;
                        }
                    }

                    // if this is the selected user then store number of games won and lost (before they get reduced down to 8)
                    if (user.Name == ddUser.SelectedValue)
                    {
                        numGamesWonByUser = user.NumGamesWon;
                        numGamesLostByUser = user.NumGamesLost;
                    }

                    // best 8 games of the league are used so adjust total games down to 8, removing losses first
                    if ((user.NumGamesWon + user.NumGamesLost) > 8)
                    {
                        if (user.NumGamesWon >= 8)
                        {
                            user.NumGamesWon = 8;
                            user.NumGamesLost = 0;
                        }
                        else
                        {
                            user.NumGamesLost = 8 - user.NumGamesWon;
                        }
                    }

                    // Calculate score of all games, awarding 3 points for a win and 1 point for a loss
                    user.Score = 3 * user.NumGamesWon + user.NumGamesLost;

                    if (user.Name == ddUser.SelectedValue)
                    {
                        userScore = user.Score;
                    }
                }

                // finally, remove any non-scoring users from the list since they didn't play any games for this league
                users = users.Where(u => u.Score != 0).ToList();

                // return the user list with calucated scores for use in the chart
                return users;
            }
        }

        private List<int> GetYears()
        {
            using (PetanqueContext db = new PetanqueContext())
            {
                List<int> years = new List<int>();
                List<Game> games = db.Games.ToList();
                foreach (Game game in games)
                {
                    if (!years.Contains(game.Date.Year))
                    {
                        years.Add(game.Date.Year);
                    }
                }
                return years;   
            }
        }


        public IQueryable<Game> gamesGrid_GetData()
        {
            // don't display grid if no name is selected
            if (ddUser.SelectedValue == "[Noone]")
                return null;

            // get the id of the user name in the dropdown
            int userID = GetUserIdByName(ddUser.SelectedValue);

            // get league and year
            LeagueType league = (LeagueType)Enum.Parse(typeof(LeagueType), ddLeague.SelectedItem.Text);
            int year = Int32.Parse(ddYear.SelectedItem.Text);

            PetanqueContext db = new PetanqueContext();
            var query = db.Games.Where(g => (g.Winner1Id == userID
                                        || g.Winner2Id == userID
                                        || g.Winner3Id == userID
                                        || g.Loser1Id == userID
                                        || g.Loser2Id == userID
                                        || g.Loser3Id == userID)
                                        && g.League == league
                                        && g.Date.Year == year);

            return query;
        }

        protected void gamesGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // If any name columns contain the name of the selected user then highlight this
                for (int i = 0; i < 6; i++)
                {
                    Label lb = e.Row.Cells[i].Controls[1] as Label;
                    if (lb == null)
                    {
                        return;
                    }
                    if (lb.Text == ddUser.SelectedValue)
                    {
                        e.Row.Cells[i].BackColor = Color.FromArgb(1, 230, 210, 249);
                    }
                }
            }
        }

    }

}
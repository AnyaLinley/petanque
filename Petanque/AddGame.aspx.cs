using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Petanque.Models;

namespace Petanque
{
    public partial class AddGame : PetanquePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddWinner1.DataSource = GetUserNamesWithoutDeletedOrNoone();
                ddWinner1.DataBind();

                ddWinner2.DataSource = GetUserNamesWithoutDeleted();
                ddWinner2.DataBind();

                ddWinner3.DataSource = GetUserNamesWithoutDeleted();
                ddWinner3.DataBind();

                ddLoser1.DataSource = GetUserNamesWithoutDeletedOrNoone();
                ddLoser1.DataBind();

                ddLoser2.DataSource = GetUserNamesWithoutDeleted();
                ddLoser2.DataBind();

                ddLoser3.DataSource = GetUserNamesWithoutDeleted();
                ddLoser3.DataBind();

                ddLosingScore.DataSource = GetPossibleLosingScores();
                ddLosingScore.DataBind();

                ddLeague.DataSource = Enum.GetNames(typeof(LeagueType));
                ddLeague.DataBind();

                Calendar1.Visible = false;
            }
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Games");
        }

        protected void lbDate_Click(object sender, EventArgs e)
        {
            Calendar1.Visible = !Calendar1.Visible;
        }

        protected void insertButton_Click(object sender, EventArgs e)
        {
            // return and show error if date isn't valid
            DateTime dt;
            if (!DateTime.TryParseExact(tbDate.Text, "dd-MMM-yy", null, DateTimeStyles.None, out dt) == true)
            {
                // not a valid DateTime in short date string format
                lValidation.Text = "Please enter a valid date using the calendar";
                return;
            }

            using (PetanqueContext db = new PetanqueContext())
            {
                var item = new Game();

                item.Winner1Id = (from u in db.Users where u.Name == ddWinner1.SelectedItem.Text select u.UserID).SingleOrDefault();
                item.Winner1 = (from u in db.Users where u.UserID == item.Winner1Id select u).SingleOrDefault();
                item.Winner2Id = (from u in db.Users where u.Name == ddWinner2.SelectedItem.Text select u.UserID).SingleOrDefault();
                item.Winner2 = (from u in db.Users where u.UserID == item.Winner2Id select u).SingleOrDefault();
                item.Winner3Id = (from u in db.Users where u.Name == ddWinner3.SelectedItem.Text select u.UserID).SingleOrDefault();
                item.Winner3 = (from u in db.Users where u.UserID == item.Winner3Id select u).SingleOrDefault();
                item.Loser1Id = (from u in db.Users where u.Name == ddLoser1.SelectedItem.Text select u.UserID).SingleOrDefault();
                item.Loser1 = (from u in db.Users where u.UserID == item.Loser1Id select u).SingleOrDefault();
                item.Loser2Id = (from u in db.Users where u.Name == ddLoser2.SelectedItem.Text select u.UserID).SingleOrDefault();
                item.Loser2 = (from u in db.Users where u.UserID == item.Loser2Id select u).SingleOrDefault();
                item.Loser3Id = (from u in db.Users where u.Name == ddLoser3.SelectedItem.Text select u.UserID).SingleOrDefault();
                item.Loser3 = (from u in db.Users where u.UserID == item.Loser3Id select u).SingleOrDefault();

                item.LosingScore = Int32.Parse(ddLosingScore.SelectedItem.Text);
                item.Date = Convert.ToDateTime(tbDate.Text);
                item.League = (LeagueType)Enum.Parse(typeof(LeagueType), ddLeague.SelectedItem.Text);

                db.Games.Add(item);
                db.SaveChanges();
                
            }

            Response.Redirect("~/Games");
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            tbDate.Text = Calendar1.SelectedDate.ToShortDateString();
            Calendar1.Visible = false;
        }
    }
}
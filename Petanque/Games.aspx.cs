using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Petanque.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.ModelBinding;
using System.Drawing;

namespace Petanque
{
    public partial class Games : PetanquePage
    {
        // store list of names taken from dropdown list when updating method is called - used to update FKs
        string newWinner1Name, newWinner2Name, newWinner3Name, newLoser1Name, newLoser2Name, newLoser3Name = ""; 

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Game> gamesGrid_GetData()
        {
            PetanqueContext db = new PetanqueContext();
            var query = db.Games;

            return query;
        }

        public void gamesGrid_UpdateItem(int gameID)
        {
            using (PetanqueContext db = new PetanqueContext())
            {
                Game item = null;
                item = db.Games.Find(gameID);
                if (item == null)
                {
                    ModelState.AddModelError("", String.Format("Item with id {0} was not found", gameID));
                    return;
                }

                // change foreign keys to correct ids based on name selected during update
                item.Winner1Id = (from u in db.Users where u.Name == newWinner1Name select u.UserID).SingleOrDefault();
                item.Winner1 = (from u in db.Users where u.UserID == item.Winner1Id select u).SingleOrDefault();
                item.Winner2Id = (from u in db.Users where u.Name == newWinner2Name select u.UserID).SingleOrDefault();
                item.Winner2 = (from u in db.Users where u.UserID == item.Winner2Id select u).SingleOrDefault();
                item.Winner3Id = (from u in db.Users where u.Name == newWinner3Name select u.UserID).SingleOrDefault();
                item.Winner3 = (from u in db.Users where u.UserID == item.Winner3Id select u).SingleOrDefault();
                item.Loser1Id = (from u in db.Users where u.Name == newLoser1Name select u.UserID).SingleOrDefault();
                item.Loser1 = (from u in db.Users where u.UserID == item.Loser1Id select u).SingleOrDefault();
                item.Loser2Id = (from u in db.Users where u.Name == newLoser2Name select u.UserID).SingleOrDefault();
                item.Loser2 = (from u in db.Users where u.UserID == item.Loser2Id select u).SingleOrDefault();
                item.Loser3Id = (from u in db.Users where u.Name == newLoser3Name select u.UserID).SingleOrDefault();
                item.Loser3 = (from u in db.Users where u.UserID == item.Loser3Id select u).SingleOrDefault();

                TryUpdateModel(item);
                if (ModelState.IsValid)
                {
                    db.SaveChanges();
                }
            }
        }


        public void gamesGrid_DeleteItem(int gameID)
        {
            using (PetanqueContext db = new PetanqueContext())
            {
                var item = new Game { GameID = gameID };
                db.Entry(item).State = EntityState.Deleted;
                try { db.SaveChanges(); }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("",
                        String.Format("Item with id {0} no longer exists in the database.", gameID));
                }
            }
        }

        protected void gamesGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // If any name columns contain [Deleted] user then highlight this in red 
                if (e.Row.RowState == DataControlRowState.Edit)
                {
                    return;
                }
                for (int i = 1; i < 7; i++)
                {
                    Label lb = e.Row.Cells[i].Controls[1] as Label;
                    if (lb == null)
                    {
                        return;
                    }
                    if (lb.Text == "[Deleted]")
                    {
                        e.Row.Cells[i].BackColor = Color.Red;
                    }
                }
            }
        }        

        protected void gamesGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            newWinner1Name = e.NewValues["Winner1.Name"].ToString();
            newWinner2Name = e.NewValues["Winner2.Name"].ToString();
            newWinner3Name = e.NewValues["Winner3.Name"].ToString();
            newLoser1Name = e.NewValues["Loser1.Name"].ToString();
            newLoser2Name = e.NewValues["Loser2.Name"].ToString();
            newLoser3Name = e.NewValues["Loser3.Name"].ToString();
        }
    }
}
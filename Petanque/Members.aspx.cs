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

namespace Petanque
{
    public partial class Members : PetanquePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<User> usersGrid_GetData([Control] AgeBracket? ageBracket)
        {
            PetanqueContext db = new PetanqueContext();
            IQueryable<User> query = db.Users.Where(u => (u.Name != "[Deleted]") && (u.Name != "[Noone]"));
            
            if (ageBracket != null)
            {
                query = query.Where(u => u.AgeRange == ageBracket);
            }

            return query;
        }

        public void usersGrid_UpdateItem(int userId)
        {
            using (PetanqueContext db = new PetanqueContext())
            {
                User item = null;
                item = db.Users.Find(userId);
                if (item == null)
                {
                    ModelState.AddModelError("", String.Format("Item with id {0} was not found", userId));
                    return;
                }

                TryUpdateModel(item);
                if (ModelState.IsValid)
                {
                    db.SaveChanges();
                }
            }
        }

        public void usersGrid_DeleteItem(int userId)
        {
            using (PetanqueContext db = new PetanqueContext())
            {
                // Change any games that reference this user to reference a dummy user instead
                List<Game> refGames = GetGamesByUserReference(userId);
                int dummyUserId = GetUserIdByName("[Deleted]");
                foreach (Game game in refGames)
                {
                    if (game.Winner1Id == userId) { game.Winner1Id = dummyUserId; db.Entry(game).State = EntityState.Modified; }
                    if (game.Winner2Id == userId) { game.Winner2Id = dummyUserId; db.Entry(game).State = EntityState.Modified; }
                    if (game.Winner3Id == userId) { game.Winner3Id = dummyUserId; db.Entry(game).State = EntityState.Modified; }
                    if (game.Loser1Id == userId) { game.Loser1Id = dummyUserId; db.Entry(game).State = EntityState.Modified; }
                    if (game.Loser2Id == userId) { game.Loser2Id = dummyUserId; db.Entry(game).State = EntityState.Modified; }
                    if (game.Loser3Id == userId) { game.Loser3Id = dummyUserId; db.Entry(game).State = EntityState.Modified; }
                }
                
                db.SaveChanges(); 
                
                 var item = new User { UserID = userId };
                 db.Entry(item).State = EntityState.Deleted;
                 try { db.SaveChanges(); }
                 catch (DbUpdateConcurrencyException)
                 {
                       ModelState.AddModelError("",
                       String.Format("Item with id {0} no longer exists in the database.", userId));
                 }
            }
        }
 
    }
}
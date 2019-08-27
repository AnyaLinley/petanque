using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Petanque.Models;

namespace Petanque
{
    public class PetanquePage: System.Web.UI.Page
    {
        public List<string> GetUserNames()
        {
            using (PetanqueContext db = new PetanqueContext())
            {
                return db.Users.Select(u => u.Name).ToList();
            }
        }

        public List<string> GetUserNamesWithoutDeleted()
        {
            using (PetanqueContext db = new PetanqueContext())
            {
                return db.Users.Where(u => u.Name != "[Deleted]").Select(u => u.Name).ToList();
            }
        }

        public List<string> GetUserNamesWithoutNoone()
        {
            using (PetanqueContext db = new PetanqueContext())
            {
                return db.Users.Where(u => u.Name != "[Noone]").Select(u => u.Name).ToList();
            }
        }

        public List<string> GetUserNamesWithoutDeletedOrNoone()
        {
            using (PetanqueContext db = new PetanqueContext())
            {
                return db.Users.Where(u => u.Name != "[Deleted]" && u.Name != "[Noone]").Select(u => u.Name).ToList();
            }
        }

        public List<int> GetPossibleLosingScores()
        {
            List<int> scores = new List<int>();
            for (int i = 0; i < 13; i++)
            {
                scores.Add(i);
            }
            return scores;
        }             

        public List<int> GetUserIds()
        {
            using (PetanqueContext db = new PetanqueContext())
            {
                return db.Users.Select(u => u.UserID).ToList();
            }
        }

        public string GetUserNameById(int id)
        {
            using (PetanqueContext db = new PetanqueContext())
            {
                string name = (from u in db.Users
                               where u.UserID == id
                               select u.Name).SingleOrDefault();
                if (name == "[Noone]")
                { name = ""; }
                return name;
            }
        }

        public int GetUserIdByName(string name)
        {
            using (PetanqueContext db = new PetanqueContext())
            {
                return (from u in db.Users
                        where u.Name == name
                        select u.UserID).SingleOrDefault();
            }
        }

        public List<Game> GetGamesByUserReference(int id)
        {
            using (PetanqueContext db = new PetanqueContext())
            {
                return (from g in db.Games
                        where g.Winner1Id == id || g.Winner2Id == id || g.Winner3Id == id || g.Loser1Id == id || g.Loser2Id == id || g.Loser3Id == id
                        select g).ToList();
            }
        }
        
    }
}
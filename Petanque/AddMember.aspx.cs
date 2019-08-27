using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Petanque.Models;

namespace Petanque
{
    public partial class AddMember : PetanquePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void addUserForm_InsertItem()
        {
            var item = new User();

            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                using (PetanqueContext db = new PetanqueContext())
                {
                    db.Users.Add(item);
                    db.SaveChanges();
                }
            }
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Members");
        }

        protected void addUserForm_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            Response.Redirect("~/Members");
        }
    }
}
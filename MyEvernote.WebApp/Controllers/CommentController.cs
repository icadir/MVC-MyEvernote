using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MyEvernote.WebApp.Controllers
{
    public class CommentController : Controller
    {
        NoteManager noteManager = new NoteManager();

        private CommentManager commentManager = new CommentManager();

        public ActionResult ShowNoteComments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Note note = noteManager.Find(x => x.Id == id);
            Note note = noteManager.ListQueryable().Include("Comments").FirstOrDefault(x => x.Id == id);

            if (note == null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialComments", note.Comments);
        }
    }
}
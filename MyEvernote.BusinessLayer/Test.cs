using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
using System;

namespace MyEvernote.BusinessLayer
{
    public class Test
    {
        private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();
        private Repository<Category> repo_category = new Repository<Category>();
        private Repository<Comment> repo_comment = new Repository<Comment>();
        private Repository<Note> repo_note = new Repository<Note>();

        public Test()
        {

            Repository<Category> repo = new Repository<Category>();
            var category = repo.List();
        }

        public void Inserttest()
        {
            Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();

        }

        public void CommentTest()
        {
            var user = repo_user.Find(x => x.Id == 1);
            var note = repo_note.Find(x => x.Id == 3);
            var comment = new Comment()
            {
                Text = "bu bir test'dir",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUsername = "ismailcadir",
                Note = note,
                Owner = user,
            };
        }
    }
}

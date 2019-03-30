using MyEvernote.BusinessLayer.Abstract;
using MyEvernote.Entities;

namespace MyEvernote.BusinessLayer
{
    public class CategoryManager : ManagerBase<Category>
    {
        //Category silme için gerekli  1.yoldu
        // --> diger yöntem ise databas gidip deleterole unu  cascade yaparak çözeriz. 
        // --> diger yöntem code first tarafında fluent Apı ile yapılabilir.
        //public override int Delete(Category category)
        //{
        //    NoteManager noteManager = new NoteManager();
        //    LikedManager likedManager = new LikedManager();
        //    CommentManager commentManager = new CommentManager();
        //    //kategori ile ilişkili notların silinmesi gerekiyor.
        //    foreach (Note note in category.Notes)
        //    {
        //        //note ile ilişkili like'ları sileleim
        //        foreach (Liked like in note.Likes)
        //        {
        //            likedManager.Delete(like);
        //        }

        //        //note ile ilişkişli commentleirn silinmesi
        //        foreach (Comment comment in note.Comments)
        //        {
        //            commentManager.Delete(comment);
        //        }

        //        noteManager.Delete(note);
        //    }

        //    return base.Delete(category);
        //}
    }
}

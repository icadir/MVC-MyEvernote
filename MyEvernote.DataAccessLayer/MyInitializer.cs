using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Entities;

namespace MyEvernote.DataAccessLayer
{
    //katılım alınına createdatabaseIfnoexists database yoksa olustur demek ve olustururken seed methodunu ezerek calısma anında fake daha ekliyoruz.
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            EvernoteUser admin = new EvernoteUser()
            {
                Name = "ismail",
                Surname = "Çadır",
                Email = "ismailcadir@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "ismailcadir",
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "ismailcadir"

            };

            EvernoteUser standartUser = new EvernoteUser()
            {
                Name = "İsmail2",
                Surname = "Çadır2",
                Email = "ismailcadir@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "ismail2cadir2",
                Password = "654321",
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(65),
                ModifiedUsername = "ismailcadir"
            };
            context.EvernoteUsers.Add(admin);
            context.EvernoteUsers.Add(standartUser);
            for (int i = 0; i < 8; i++)
            {
                EvernoteUser user = new EvernoteUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    Username = $"user{i}",
                    Password = "123",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUsername = $"user{i}"
                };
                context.EvernoteUsers.Add(user);
            }

            context.SaveChanges();

            List<EvernoteUser> userlist = context.EvernoteUsers.ToList();

            //adding fake kategori.
            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUsername = "ismailcadir",
                };

                context.Categories.Add(cat);

                for (int k = 0; k < FakeData.NumberData.GetNumber(5, 9); k++)
                {
                    var owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];
                    var note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),

                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        IsDraft = false,
                        Owner = owner,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUsername = owner.Username,
                    };

                    cat.Notes.Add(note);

                    for (int j = 0; j < FakeData.NumberData.GetNumber(3, 5); j++)
                    {
                        var comment_owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];
                        var comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            Owner = comment_owner,
                            ModifiedUsername = comment_owner.Username,
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now)
                        };
                        note.Comments.Add(comment);
                    }


                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = userlist[m]
                        };
                        note.Likes.Add(liked);
                    }
                }
            }

            context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
   public class Liked
    {
        public int Id { get; set; }

        public Note Note { get; set; }
        public EvernoteUser LikedUser { get; set; }


    }
}

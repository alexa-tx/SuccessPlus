using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuccessPlus.Model
{
    partial class Student
    {
        private Core _db = new Core();
        public string GroupName => _db.context.Group.Where(x => x.IdGroup == GroupId).FirstOrDefault().NameGroup;
        public List<int> MarkStudents => _db.context.MarkStudent.Where(x => x.IdStudent == IdStudent).Select(x => x.Marks.IdMark).ToList();

        public double AVGMark => MarkStudents.AsEnumerable().Average(mark => (double)mark);

        public List<int> Visiting => _db.context.VisitingStudent.Where(x => x.IdStudent == IdStudent).Select (x => x.Visiting.IdVisiting).ToList();
        

        public int TotalVisiting => Visiting.Sum();

        public string FIO => $"{LastName} {FisrtName[0]}.";

        //public List <int?> SportEvent => _db.context.EventStudent.Where(x => x.IdStudent == IdStudent).Select(x => x.IdMark).ToList();

        //public int? a => _db.context.EventStudent.Where(x => x.IdStudent == IdStudent).FirstOrDefault().IdMark == null ? 0 : 1;

        //public double AVGSportEvent => SportEvent.AsEnumerable().Average(mark => (double)mark);

        public List<int?> SportEvent => _db.context.EventStudent.Where(x => x.IdStudent == IdStudent && x.IdEvent == 2).Select(x => x.IdMark).ToList();

        public double AVGSportEvent
        {
            get
            {
                var marks = SportEvent.Where(mark => mark.HasValue).Select(mark => mark.Value);
                return marks.Any() ? marks.Average() : 0;
            }
        }

        public List<int?> SocialEvent => _db.context.EventStudent.Where(x => x.IdStudent == IdStudent && x.IdEvent == 3).Select(x => x.IdMark).ToList();

        public double? AVGSocialEvent
        {
            get
            {
                var marks = SocialEvent.Where(mark => mark.HasValue).Select(mark => mark.Value);
                return marks.Any() ? (double?)marks.Average() : 0;
            }
        }

        public List<int?> NttEvent => _db.context.EventStudent.Where(x => x.IdStudent == IdStudent && x.IdEvent == 1).Select(x => x.IdMark).ToList();

        public double? AVGNttEvent
        {
            get
            {
                var marks = NttEvent.Where(mark => mark.HasValue).Select(mark => mark.Value);
                return marks.Any() ? (double?)marks.Average() : 0;
            }
        }

        public int CountThrees => _db.context.MarkStudent.Where(x => x.IdStudent == IdStudent && x.Marks.MarkName == 3).Count();

        public int CountFours => _db.context.MarkStudent.Where(x => x.IdStudent == IdStudent && x.Marks.MarkName == 4).Count();

        public int CountFives => _db.context.MarkStudent.Where(x => x.IdStudent == IdStudent && x.Marks.MarkName == 5).Count();

    }
}

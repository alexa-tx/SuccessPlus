using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public List<int> Visiting => _db.context.VisitingStudent.Where(x => x.IdStudent == IdStudent).Select(x => x.Visiting.IdVisiting).ToList();

        public int TotalVisiting => Visiting.Sum();

        public string FIO => $"{LastName} {FisrtName[0]}.";

        public List<int?> SelfDevEvent
        {
            get
            {
                using (var dbContext = new SuccessPlusEntities1())
                {
                    var ratings = dbContext.EventStudent
                        .Where(es => es.IdStudent == this.IdStudent &&
                                     es.Event.Type == 5)
                        .Select(es => es.IdMark)
                        .ToList();

                    return ratings;
                }
            }

        }

        public double AVGSelfDev
        {
            get
            {
                var marks = SelfDevEvent.Where(mark => mark.HasValue).Select(mark => mark.Value);
                return marks.Any() ? marks.Average() : 0;
            }
        }
        public List<int?> AmateurEvent
        {
            get
            {
                using (var dbContext = new SuccessPlusEntities1())
                {
                    var ratings = dbContext.EventStudent
                        .Where(es => es.IdStudent == this.IdStudent &&
                                     es.Event.Type == 4)
                        .Select(es => es.IdMark)
                        .ToList();

                    return ratings;
                }
            }

        }

        public double AVGAmateurEvent
        {
            get
            {
                var marks = AmateurEvent.Where(mark => mark.HasValue).Select(mark => mark.Value);
                return marks.Any() ? marks.Average() : 0;
            }
        }

        public List<int?> SportEvent
        {
            get
            {
                using (var dbContext = new SuccessPlusEntities1())
                {
                    var ratings = dbContext.EventStudent
                        .Where(es => es.IdStudent == this.IdStudent &&
                                     es.Event.Type == 2)
                        .Select(es => es.IdMark)
                        .ToList();

                    return ratings;
                }
            }

        }
        
        public double AVGSportEvent
        {
            get
            {
                var marks = SportEvent.Where(mark => mark.HasValue).Select(mark => mark.Value);
                return marks.Any() ? marks.Average() : 0;
            }
        }
        public List<int?> SocialEvent
        { 
            get
            {
                using (var dbContext = new SuccessPlusEntities1())
                {
                    var ratings = dbContext.EventStudent
                        .Where(es => es.IdStudent == this.IdStudent &&
                                     es.Event.Type == 3)
                        .Select(es => es.IdMark)
                        .ToList();

                    return ratings;
                }
            }
            
        }
       
        public double? AVGSocialEvent
        {
            get
            {
                var marks = SocialEvent.Where(mark => mark.HasValue).Select(mark => mark.Value);
                return marks.Any() ? (double?)marks.Average() : 0;
            }
        }

        public List<int?> NttEvent
        {
            get
            {
                using (var dbContext = new SuccessPlusEntities1())
                {
                    var ratings = dbContext.EventStudent
                        .Where(es => es.IdStudent == this.IdStudent &&
                                     es.Event.Type == 3)
                        .Select(es => es.IdMark)
                        .ToList();

                    return ratings;
                }
            }

        }

       
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

        public List< string> NameSportEvent => _db.context.Event.Where(x => x.Type == 2).Select(x =>x.Name).ToList();

        public List< string> NameSocialEvent => _db.context.Event.Where(x => x.Type == 1).Select(x =>x.Name).ToList();

        public List< string> NameNttEvent => _db.context.Event.Where(x => x.Type == 3).Select(x =>x.Name).ToList();
    }
}
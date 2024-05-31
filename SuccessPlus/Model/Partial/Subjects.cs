using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuccessPlus.Model
{
      partial class Subjects
    {
        private Core _db = new Core();

        //public int IdUser => _db.context.MarkStudent.Where(x => x.IdSubject == IdSubjects).Select(x => x.IdStudent);

        public string MarkSubject => String.Join(", ",  _db.context.MarkStudent.Where(x => x.IdSubject == IdSubjects).Select(s => s.IdMark).ToList().ToArray());
    }
}

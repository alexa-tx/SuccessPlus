using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuccessPlus.Model
{
    public partial class Subjects
    {
        private Core _db = new Core();

        public int MarkSubject => _db.context.MarkStudent.Where(x => x.IdMarkStudent == IdSubjects).FirstOrDefault().IdMark;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuccessPlus.Model
{
    partial class MarkStudent
    {
        private Core _db = new Core();
        public string NameSubject => _db.context.Subjects.Where(x => x.IdSubjects == IdSubject).FirstOrDefault().Name == null ? "" : _db.context.Subjects.Where(x => x.IdSubjects == IdSubject).FirstOrDefault().Name;
    }
}

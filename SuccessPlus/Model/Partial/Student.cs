using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SuccessPlus.Model
{
    partial class Student
    {
        private Core _db = new Core();
        public string GroupName => _db.context.Group.Where(x => x.IdGroup == GroupId).FirstOrDefault().NameGroup;
        public List<int> MarkStudents => _db.context.MarkStudent.Where(x => x.IdStudent == IdStudent).Select(x => x.Marks.IdMark).ToList();

        public List<DateTime> Date => _db.context.MarkStudent.Where(x => x.IdStudent == IdStudent).Select(x => x.Date).ToList();

        public double AVGMark => MarkStudents.AsEnumerable().Average(mark => (double)mark);

        public List<int> Visiting => _db.context.VisitingStudent.Where(x => x.IdStudent == IdStudent).Select(x => x.Visiting.IdVisiting).ToList();

        public int TotalVisiting => Visiting.Sum();

        public string FIO => $"{LastName} {FisrtName[0]}.";

        public List<int?> SelfDevEvent
        {
            get
            {
                using (var dbContext = new SuccessPlusEntities2())
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
                using (var dbContext = new SuccessPlusEntities2())
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
                using (var dbContext = new SuccessPlusEntities2())
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
                using (var dbContext = new SuccessPlusEntities2())
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
                using (var dbContext = new SuccessPlusEntities2())
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
        public int CountZeros => _db.context.MarkStudent.Where(x => x.IdStudent == IdStudent && x.Marks.MarkName == 0).Count();

        public int CountTwos => _db.context.MarkStudent.Where(x => x.IdStudent == IdStudent && x.Marks.MarkName == 2).Count();
        public int CountThrees => _db.context.MarkStudent.Where(x => x.IdStudent == IdStudent && x.Marks.MarkName == 3).Count();

        public int CountFours => _db.context.MarkStudent.Where(x => x.IdStudent == IdStudent && x.Marks.MarkName == 4).Count();

        public int CountFives => _db.context.MarkStudent.Where(x => x.IdStudent == IdStudent && x.Marks.MarkName == 5).Count();

        public List<string> NameSportEvent => _db.context.Event.Where(x => x.Type == 2).Select(x => x.Name).ToList();

        public List<string> NameSocialEvent => _db.context.Event.Where(x => x.Type == 1).Select(x => x.Name).ToList();

        public List<string> NameNttEvent => _db.context.Event.Where(x => x.Type == 3).Select(x => x.Name).ToList();



        // Новое свойство для получения оценки поведения
        public int BehaviorScore
        {
            get
            {
                using (var dbContext = new SuccessPlusEntities2())
                {
                    var behaviorFine = dbContext.Fine
                        .Where(x => x.IdStudent == this.IdStudent && x.TypeFine == 1)
                        .Select(x => x.IdMark)
                        .FirstOrDefault();
                    return behaviorFine;
                }
            }
        }

        // Новое свойство для получения оценки за задолженности
        public int DebtScore
        {
            get
            {
                using (var dbContext = new SuccessPlusEntities2())
                {
                    var debtFine = dbContext.Fine
                        .Where(f => f.IdStudent == this.IdStudent && f.TypeFine == 2)
                        .Select(f => f.IdMark)
                        .FirstOrDefault();
                    return debtFine;
                }
            }
        }

        // Новое свойство для получения бонуса руководства
        public int LeadershipBonus
        {
            get
            {
                using (var dbContext = new SuccessPlusEntities2())
                {
                    var leadershipFine = dbContext.Fine
                        .Where(f => f.IdStudent == this.IdStudent && f.TypeFine == 3)
                        .Select(f => f.IdMark)
                        .FirstOrDefault();
                    return leadershipFine;
                }
            }
        }

        public double AverageFineScore
        {
            get
            {
                using (var dbContext = new SuccessPlusEntities2())
                {
                    // Получение всех штрафов для студента
                    var fines = dbContext.Fine
                        .Where(f => f.IdStudent == this.IdStudent)
                        .Select(f => f.IdMark)
                        .ToList();

                    // Вычисление среднего значения, если есть штрафы
                    return fines.Any() ? fines.Average() : 0;
                }
            }
        }

        // Новое свойство для процента успеваемости
        public double AcademicPerformancePercentage
        {
            get
            {
                var K2 = CountTwos;
                var K3 = CountThrees;
                var K4 = CountFours;
                var K5 = CountFives;
                var K0 = CountZeros;
                return 40 * (2 * K2 + 3 * K3 + 4 * K4 + 5 * K5) / (5 * (K2 + K3 + K4 + K5));
            }
        }

        // Новое свойство для процента прогулов
        public double AbsenteeismPercentage
        {
            get
            {
                var Kprog = TotalVisiting; // количество пропущенных часов студентом за месяц
                const int Vch = 160; // количество часов занятий в месяц (примерное значение, может быть изменено)
                return 20 * (Vch - Kprog) / Vch;
            }
        }

        // Новое свойство для рейтинговой оценки
        public double RatingScore
        {
            get
            {
                var Pusp = AcademicPerformancePercentage;
                var Pprog = AbsenteeismPercentage;
                var Oreb = AVGAmateurEvent; // оценка участия в общественной работе
                var S = AVGSportEvent; // оценка участия в спортивных мероприятиях
                var Sa = AVGSelfDev; // оценка участия в самодеятельности
                var NTT = AVGNttEvent ?? 0; // оценка участия в научно-техническом труде
                var Pov = (double)BehaviorScore; // штрафные баллы за поведение
                var Oz = (double)DebtScore; // штрафные баллы за задолженности за предыдущие семестры
                var Bruk = (double)LeadershipBonus; // бонус руководства

                return Pusp + Pprog + Oreb + S + Sa + NTT - Pov - Oz + Bruk;
            }
        }
    }
}

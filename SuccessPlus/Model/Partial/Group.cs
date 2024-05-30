using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SuccessPlus.Model
{
    partial class Group
    {
        private Core _db = new Core();

        public int TotalAbsences
        {
            get
            {
                using (var dbContext = new SuccessPlusEntities2())
                {
                    var totalAbsences = (from student in dbContext.Student
                                         join visiting in dbContext.VisitingStudent on student.IdStudent equals visiting.IdStudent
                                         where student.GroupId == this.IdGroup
                                         select visiting).Count();

                    return totalAbsences;
                }
            }
        }

        public int TotalNTT
        {
            get
            {
                using (var dbContext = new SuccessPlusEntities2())
                {
                    var totalNTT = (from student in dbContext.Student
                                    join eventStudent in dbContext.EventStudent on student.IdStudent equals eventStudent.IdStudent
                                    join eventType in dbContext.EventType on eventStudent.Event.Type equals eventType.IdTypeEvent
                                    where student.GroupId == this.IdGroup && eventType.Name == "Научно-технический труд"
                                    select eventStudent).Count();

                    return totalNTT;
                }
            }
        }

        public int TotalSports
        {
            get
            {
                using (var dbContext = new SuccessPlusEntities2())
                {
                    var totalSports = (from student in dbContext.Student
                                       join eventStudent in dbContext.EventStudent on student.IdStudent equals eventStudent.IdStudent
                                       join eventType in dbContext.EventType on eventStudent.Event.Type equals eventType.IdTypeEvent
                                       where student.GroupId == this.IdGroup && eventType.Name == "Спортивные мероприятия"
                                       select eventStudent).Count();

                    return totalSports;
                }
            }
        }

        public int TotalPublicActivity
        {
            get
            {
                using (var dbContext = new SuccessPlusEntities2())
                {
                    var totalPublicActivity = (from student in dbContext.Student
                                               join eventStudent in dbContext.EventStudent on student.IdStudent equals eventStudent.IdStudent
                                               join eventType in dbContext.EventType on eventStudent.Event.Type equals eventType.IdTypeEvent
                                               where student.GroupId == this.IdGroup && eventType.Name == "Общественные мероприятия"
                                               select eventStudent).Count();

                    return totalPublicActivity;
                }
            }
        }

        public int TotalArtActivity
        {
            get
            {
                using (var dbContext = new SuccessPlusEntities2())
                {
                    var totalArtActivity = (from student in dbContext.Student
                                            join eventStudent in dbContext.EventStudent on student.IdStudent equals eventStudent.IdStudent
                                            join eventType in dbContext.EventType on eventStudent.Event.Type equals eventType.IdTypeEvent
                                            where student.GroupId == this.IdGroup && eventType.Name == "Самодеятельность"
                                            select eventStudent).Count();

                    return totalArtActivity;
                }
            }
        }

        public int TotalSelfEducation
        {
            get
            {
                using (var dbContext = new SuccessPlusEntities2())
                {
                    var totalSelfEducation = (from student in dbContext.Student
                                              join eventStudent in dbContext.EventStudent on student.IdStudent equals eventStudent.IdStudent
                                              join eventType in dbContext.EventType on eventStudent.Event.Type equals eventType.IdTypeEvent
                                              where student.GroupId == this.IdGroup && eventType.Name == "Самообразование"
                                              select eventStudent).Count();

                    return totalSelfEducation;
                }
            }
        }

        public double AverageGrade
        {
            get
            {
                using (var dbContext = new SuccessPlusEntities2())
                {
                    var averageGrade = (from student in dbContext.Student
                                        join markStudent in dbContext.MarkStudent on student.IdStudent equals markStudent.IdStudent
                                        where student.GroupId == this.IdGroup
                                        select markStudent.Marks.MarkName).Average();

                    return averageGrade;
                }
            }
        }
        public double AverageGroupRating
        {
            get
            {
                using (var dbContext = new SuccessPlusEntities2())
                {
                    // Получаем список всех студентов в группе
                    var students = dbContext.Student.Where(s => s.GroupId == this.IdGroup).ToList();

                    // Если нет студентов в группе, возвращаем 0
                    if (students.Count == 0)
                    {
                        return 0;
                    }

                    // Суммируем рейтинговые оценки всех студентов
                    double totalRating = students.Sum(s => s.RatingScore);

                    // Рассчитываем среднюю арифметическую рейтинговую оценку группы
                    double averageGroupRating = totalRating / students.Count;

                    return averageGroupRating;
                }
            }
        }


    }
}
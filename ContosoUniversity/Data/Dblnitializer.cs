using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
           
            if (context.Courses.Any())
            {
                return;  
            }

          
            var instructors = new Instructor[]
            {
                new Instructor { FirstMidName = "Kim",    LastName = "Abercrombie", HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstMidName = "Fadi",   LastName = "Fakhouri",    HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { FirstMidName = "Roger",  LastName = "Kapoor",      HireDate = DateTime.Parse("2003-07-01") },
                new Instructor { FirstMidName = "Laura",  LastName = "Zheng",       HireDate = DateTime.Parse("2003-01-01") }
            };

            foreach (Instructor i in instructors)
            {
                context.Instructors.Add(i);
                context.SaveChanges();
            }

            var students = new Student[]
            {
                new Student { FirstMidName = "Carson",   LastName = "Alexander", EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Student { FirstMidName = "Meredith", LastName = "Alonso",    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstMidName = "Arturo",   LastName = "Anand",     EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstMidName = "Gytis",    LastName = "Barzdukas", EnrollmentDate = DateTime.Parse("2012-09-01") },
            };

            foreach (Student s in students)
            {
                context.Students.Add(s);
            }

           
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🛑 ERROR DURING INITIAL SAVE (Persons): {ex.InnerException?.Message ?? ex.Message}");
                return; 
            }
            if(context.Departments.Any())
            {
                return;
            }
           
            var departments = new Department[]
            {
                new Department
                {
                    Name = "English", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID

                },
                new Department
                {
                    Name = "Mathematics", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                  InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID
                },
                new Department
                {
                    Name = "Engineering", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single(i => i.LastName == "Kapoor").ID
                },
            };

            foreach (Department d in departments)
            {
                context.Departments.Add(d);
                context.SaveChanges();
            }
            var courses = new Course[]
            {
              
                new Course { CourseID = 1050, Title = "Chemistry", Credits = 3,
                    DepartmentID = departments.Single(d => d.Name == "Engineering").DepartmentID },
                new Course
                {
                   CourseID = 4022,
                   Title = "Microeconomics",
                   Credits = 3,
                   DepartmentID = departments.Single(d => d.Name == "Mathematics").DepartmentID
                },
                new Course
                {
                    CourseID = 4041,
                    Title = "Macroeconomics",
                    Credits = 3,
                    DepartmentID = departments.Single(d => d.Name == "Mathematics").DepartmentID
                },
                new Course
                {
                    CourseID = 1045,
                    Title = "Calculus",
                    Credits = 4,
                    DepartmentID = departments.Single(d => d.Name == "Mathematics").DepartmentID
                },
            };

            foreach (Course c in courses)
            {
                context.Courses.Add(c);
                context.SaveChanges();
            }

            
            var enrollments = new Enrollment[]
            {
                new Enrollment { StudentID = students.Single(s => s.LastName == "Alexander").ID, CourseID = courses.Single(c => c.Title == "Chemistry").CourseID, Grade = Grade.A },
                new Enrollment { StudentID = students.Single(s => s.LastName == "Alexander").ID, CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID, Grade = Grade.C },
                new Enrollment { StudentID = students.Single(s => s.LastName == "Alonso").ID, CourseID = courses.Single(c => c.Title == "Calculus").CourseID, Grade = Grade.B },
                new Enrollment { StudentID = students.Single(s => s.LastName == "Anand").ID, CourseID = courses.Single(c => c.Title == "Chemistry").CourseID, Grade = Grade.B },
            };

            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
                context.SaveChanges();
            }


             
        }
    }
}
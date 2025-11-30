
using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            // التأكد من أن قاعدة البيانات قد أُنشئت (في حال عدم استخدام Migrations)
            // بما أننا سنستخدم التنقلات، يمكن حذف هذا السطر إذا أردت، لكن تركه لا يضر.
            context.Database.EnsureCreated();

            // التحقق مما إذا كانت هناك أي سجلات طلاب موجودة. إذا كانت موجودة، فلا حاجة للتهيئة.
            if (context.Students.Any())
            {
                return;   // قاعدة البيانات مُهيئة بالفعل
            }

            // 1. إنشاء قائمة الطلاب
            var students = new Student[]
            {
                new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2019-09-01")},
                new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2017-09-01")},
                new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2018-09-01")},
                new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2017-09-01")},
                new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2017-09-01")},
                new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2016-09-01")},
                new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2019-09-01")},
                new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2011-09-01")}
            };
            context.Students.AddRange(students);
            context.SaveChanges(); // حفظ الطلاب للحصول على قيم ID

            // 2. إنشاء قائمة الدورات
            // ملاحظة: قمنا بتعيين [DatabaseGenerated(DatabaseGeneratedOption.None)] لذا يجب تحديد ID
            var courses = new Course[]
            {
                new Course{CourseID=1050,Title="Chemistry",Credits=3},
                new Course{CourseID=4022,Title="Microeconomics",Credits=3},
                new Course{CourseID=4041,Title="Macroeconomics",Credits=3},
                new Course{CourseID=1045,Title="Calculus",Credits=4},
                new Course{CourseID=3141,Title="Trigonometry",Credits=4},
                new Course{CourseID=2021,Title="Composition",Credits=3},
                new Course{CourseID=2042,Title="Literature",Credits=4}
            };
            context.Courses.AddRange(courses);
            context.SaveChanges(); // حفظ الدورات

            // 3. إنشاء قائمة التسجيلات
            var enrollments = new Enrollment[]
            {
                new Enrollment{StudentID=students.Single(s => s.LastName == "Alexander").ID,CourseID=courses.Single(c => c.Title == "Chemistry" ).CourseID,Grade=Grade.A},
                new Enrollment{StudentID=students.Single(s => s.LastName == "Alexander").ID,CourseID=courses.Single(c => c.Title == "Microeconomics" ).CourseID,Grade=Grade.C},
                new Enrollment{StudentID=students.Single(s => s.LastName == "Alexander").ID,CourseID=courses.Single(c => c.Title == "Macroeconomics" ).CourseID,Grade=Grade.B},
                new Enrollment{StudentID=students.Single(s => s.LastName == "Alonso").ID,CourseID=courses.Single(c => c.Title == "Calculus" ).CourseID,Grade=Grade.B},
                new Enrollment{StudentID=students.Single(s => s.LastName == "Alonso").ID,CourseID=courses.Single(c => c.Title == "Trigonometry" ).CourseID,Grade=Grade.C},
                new Enrollment{StudentID=students.Single(s => s.LastName == "Alonso").ID,CourseID=courses.Single(c => c.Title == "Composition" ).CourseID,Grade=Grade.B},
                new Enrollment{StudentID=students.Single(s => s.LastName == "Anand").ID,CourseID=courses.Single(c => c.Title == "Chemistry" ).CourseID},


new Enrollment { StudentID = students.Single(s => s.LastName == "Anand").ID, CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID, Grade =Grade.B },
                new Enrollment { StudentID = students.Single(s => s.LastName == "Barzdukas").ID, CourseID = courses.Single(c => c.Title == "Chemistry").CourseID, Grade =Grade. B },
                new Enrollment { StudentID = students.Single(s => s.LastName == "Li").ID, CourseID = courses.Single(c => c.Title == "Composition").CourseID, Grade =Grade.B },
                new Enrollment { StudentID = students.Single(s => s.LastName == "Justice").ID, CourseID = courses.Single(c => c.Title == "Literature").CourseID, Grade =Grade.B }
            };

            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
    context.SaveChanges();
        }
    }
}
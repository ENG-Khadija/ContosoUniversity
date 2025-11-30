using System;
using System.Collections.Generic;

namespace ContosoUniversity.Models
{
    public class Student
    {
        // المفتاح الأساسي (Primary Key - PK). EF يكتشفه لأنه يحمل اسم ID.
        public int ID { get; set; }

        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        // خاصية الملاحة (Navigation Property)
        // تمثل العلاقة One-to-Many: الطالب يمكن أن يكون لديه عدة تسجيلات.
        // ملاحظة .NET 8 (NRT): بما أننا نستخدم .NET 8 (الذي يفرض NRTs افتراضيًا)، من الأفضل تهيئة الخصائص القابلة للقيم الفارغة.
        // في هذا الكود، يتم تهيئة ICollection.
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
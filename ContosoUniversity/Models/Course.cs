using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Course
    {
        // Attribute يمنع قاعدة البيانات من توليد قيمة تلقائية للمفتاح الأساسي.
        // سيتم تحديد رقم الدورة يدوياً في كود التهيئة.
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }

        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }

        // خاصية الملاحة: الدورة يمكن أن يكون لديها عدة تسجيلات.
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
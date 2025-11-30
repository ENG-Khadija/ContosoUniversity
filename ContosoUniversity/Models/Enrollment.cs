namespace ContosoUniversity.Models
{
    // تعداد (Enum) لدرجة التسجيل
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        // المفتاح الأساسي (PK) باستخدام النمط ClassnameID.
        public int EnrollmentID { get; set; }

        // مفاتيح أجنبية (Foreign Keys - FKs)
        public int CourseID { get; set; }
        public int StudentID { get; set; }

        // الدرجة قابلة للقيم الفارغة (nullable) لتشير إلى أن الدرجة لم تُسند بعد.
        public Grade? Grade { get; set; }

        // خصائص الملاحة (Navigation Properties)
        // العلاقة One-to-One: التسجيل مرتبط بدورة واحدة وطالب واحد.
        public Course Course { get; set; } = null!;
        public Student Student { get; set; } = null!;
    }
}

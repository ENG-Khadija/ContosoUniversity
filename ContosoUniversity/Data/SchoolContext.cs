using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data
{
    // يجب أن يرث من فئة DbContext
    public class SchoolContext : DbContext
    {
        // الدالة البانية التي تقبل خيارات السياق (يتم تمريرها من Program.cs)
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        // خصائص DbSet تمثل مجموعات الكيانات (الجداول) في قاعدة البيانات
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }

        // هذه الدالة تسمح لنا بتكوين نموذج البيانات قبل إنشاء قاعدة البيانات.
        // هنا، نستخدمها لفرض أسماء الجداول لتكون مفردة (Course, Enrollment, Student) بدلاً من الجمع الافتراضي.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}
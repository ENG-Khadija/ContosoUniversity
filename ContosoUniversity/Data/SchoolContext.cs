using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data
{
    // يرث من DbContext لتمكين الاتصال بقاعدة البيانات
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        // تحديد مجموعات البيانات (DbSets) للنماذج
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        // استخدام هذه الدالة لتحديد اسم الجدول في قاعدة البيانات
        // هذا يمنع EF Core من إنشاء أسماء جداول بصيغة الجمع (مثل Students, Courses)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}
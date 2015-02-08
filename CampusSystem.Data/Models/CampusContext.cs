using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using CampusSystem.Data.Models.Mapping;

namespace CampusSystem.Data.Models
{
    public partial class CampusContext : DbContext
    {
        static CampusContext()
        {
            Database.SetInitializer<CampusContext>(null);
        }

        public CampusContext()
            : base("Name=Campus")
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Copy> Copies { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lend> Lends { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<SendTo> SendToes { get; set; }
        public DbSet<Shuttle> Shuttles { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookMap());
            modelBuilder.Configurations.Add(new ClassMap());
            modelBuilder.Configurations.Add(new CopyMap());
            modelBuilder.Configurations.Add(new CourseMap());
            modelBuilder.Configurations.Add(new GroupMap());
            modelBuilder.Configurations.Add(new LendMap());
            modelBuilder.Configurations.Add(new MessageMap());
            modelBuilder.Configurations.Add(new ScheduleMap());
            modelBuilder.Configurations.Add(new SendToMap());
            modelBuilder.Configurations.Add(new ShuttleMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}

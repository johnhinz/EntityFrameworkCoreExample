using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ConsoleApp27
{
    class Program
    {
        static void Main(string[] args)
        {

            JobContext context = new JobContext();

            var result =
                context.ApplicantEducations
                .Where(appRecord => appRecord.Major.Contains("Web"))
                .Where(appRecord => appRecord.CompletionPercent > 60);

            foreach (ApplicantEducationPoco item in result)
            {
                Console.WriteLine(item.Major);
            }

        }
    }

    public class JobContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory
           = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<ApplicantEducationPoco> 
            ApplicantEducations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder
                .UseLoggerFactory(MyLoggerFactory)
                .UseSqlServer(@"Data Source=CSHARPHUMBER\HUMBERBRIDGING;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True;");

            base.OnConfiguring(optionsBuilder);
        }
    }

    [Table("Applicant_Educations")]
    public class ApplicantEducationPoco 
    {
        [Key]
        public Guid Id { get; set; }

        public Guid Applicant { get; set; }

        [StringLength(100)]
        public string Major { get; set; }

        [Column("Certificate_Diploma")]
        [StringLength(100)]
        public string CertificateDiploma { get; set; }

        [Column("Start_Date")]
        public DateTime? StartDate { get; set; }

        [Column("Completion_Date")]
        public DateTime? CompletionDate { get; set; }

        [Column("Completion_Percent")]
        public byte? CompletionPercent { get; set; }

        [Column(name: "Time_Stamp", Order = 3, TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
    }

}

namespace SIMS_Dapper.Models
{
    
    
        public class Subject
        {
            public int SubjectId { get; set; }

            public string SubjectName { get; set; }

            public string SubjectCode { get; set; }

            public int CourseId { get; set; }

            public int BranchId { get; set; }

            public int Semester { get; set; }

            public string SubjectType { get; set; }

            public string CourseName { get; set; }

            public string BranchName { get; set; }
        }
    
}

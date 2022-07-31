using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ExamPortal.Models.ViewModels;

namespace ExamPortal.Models
{

    [MetadataType(typeof(CourseMetadata))]
    public partial class Course { }

    [MetadataType(typeof(ClassMetadata))]
    public partial class Class { }

    [MetadataType(typeof(TeacherMetadata))]
    public partial class Teacher { }

    [MetadataType(typeof(StudentMetadata))]
    public partial class Student { }

    [MetadataType(typeof(Assigned_TestMetadata))]
    public partial class Assigned_Test { }

    [MetadataType(typeof(LogerMetadata))]
    public partial class Loger { }

    [MetadataType(typeof(QuestionMetadata))]
    public partial class Question { }

    [MetadataType(typeof(Registered_CadidatesMetadata))]
    public partial class Registered_Cadidates { }

    [MetadataType(typeof(Student_Current_QualificationMetadata))]
    public partial class Student_Current_Qualification { }

    [MetadataType(typeof(Student_10th_QualificationMetadata))]
    public partial class Student_10th_Qualification { }

    [MetadataType(typeof(Student_12th_QualificationMetadata))]
    public partial class Student_12th_Qualification { }

    [MetadataType(typeof(SubjectMetadata))]
    public partial class Subject { }

    [MetadataType(typeof(Subjects_In_10thMetadata))]
    public partial class Subjects_In_10th { }

    [MetadataType(typeof(Subjects_In_12thMetadata))]
    public partial class Subjects_In_12th { }

    [MetadataType(typeof(TeachMetadata))]
    public partial class Teach { }

    [MetadataType(typeof(TestMetadata))]
    public partial class Test { }

    [MetadataType(typeof(Test_QuestionMetadata))]
    public partial class Test_Question { }

    [MetadataType(typeof(UserLoginMetadata))]
    public partial class UserLogin{ }

    [MetadataType(typeof(UserRoleMetadata))]
    public partial class UserRole { }

    [MetadataType(typeof(UnapprovedResultMetadata))]
    public partial class UnapprovedResult { }

    [MetadataType(typeof(AssignTestPartialMetadata))]
    public partial class AssignTestPartial { }

    [MetadataType(typeof(AssignTestStudentPartial2Metadata))]
    public partial class AssignTestStudentPartial2 { }

    [MetadataType(typeof(TeacherAdminDeskMetadata))]
    public partial class TeacherAdminDesk { }

    [MetadataType(typeof(UnassignedTestMetadata))]
    public partial class UnassignedTest { }

}

namespace ExamPortal.Models.ViewModels
{
    [MetadataType(typeof(TeacherVMMetadata))]
    public partial class TeacherVM { }

    [MetadataType(typeof(CreateStudentVMMetadata))]
    public partial class CreateStudentVM { }
}
using ExamPortal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Models
{
    public class StudentMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public int scholar_no { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, ErrorMessage = "Student name is too long.")]
        [DisplayName("Students Name")]
        [RegularExpression("([a-zA-Z .]+)", ErrorMessage = "Invalid Students-name")]
        public string unique_name { get; set; }

        [Required(ErrorMessage = "Required")]
        [Column(TypeName = "numeric")]
        [DisplayName("Parents Phone")]
        [RegularExpression(@"^[0-9]{10}([0-9]{2})?$", ErrorMessage = "Invalid Parents-phone")]
        public decimal parents_phone { get; set; }

        [Required(ErrorMessage = "Required")]
        [Column(TypeName = "numeric")]
        [DisplayName("Student Mobile")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Invalid Student-mobile")]
        public decimal mobile { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Photo")]
        [StringLength(50, ErrorMessage = "Photo filename is too long.")]
        [RegularExpression("([0-9a-zA-Z./ ]+)", ErrorMessage = "Invalid Photo")]
        public string photo { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Enrollment no.")]
        [RegularExpression("V16R[0-9]{9}", ErrorMessage = "Invalid Enrollment no.")]
        public string enrollment_no { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(200, ErrorMessage = "Residential address is too long.")]
        [DisplayName("Residential Address")]
        [RegularExpression(@"([0-9A-Za-z ,.\-()]+)", ErrorMessage = "Invalid Residential-address")]
        public string residential_address { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(200, ErrorMessage = "Permanent Address is too long.")]
        [DisplayName("Permanent Address")]
        [RegularExpression(@"([0-9A-Za-z ,.\-()]+)", ErrorMessage = "Invalid Permanent-address")]
        public string permanent_address { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, ErrorMessage = "Fathers-name is too long.")]
        [DisplayName("Father's Name")]
        [RegularExpression("([a-zA-Z .]+)", ErrorMessage = "Invalid Fathers-name")]
        public string fathers_name { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, ErrorMessage = "Mothers-name is too long.")]
        [DisplayName("Mother's Name")]
        [RegularExpression("([a-zA-Z .]+)", ErrorMessage = "Invalid Mothers-name")]
        public string mothers_name { get; set; }

        [Required(ErrorMessage = "Required")]
        [Column(TypeName = "numeric")]
        [DisplayName("Aadhaar no.")]
        [RegularExpression("[0-9]{12}", ErrorMessage = "Invalid Adhar")]
        public decimal adhar_no { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Blood Group")]
        [StringLength(5, ErrorMessage = "Invalid Blood Group")]
        [RegularExpression("^(A|B|AB|O)[+-]$", ErrorMessage = "Invalid Blood Group")]
        public string blood_group { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dob { get; set; }

        [ForeignKey("Class")]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Class")]
        public int class_id { get; set; }

        [ForeignKey("UserLogin")]
        [Required(ErrorMessage = "Required")]
        public int user_id { get; set; }

    }
    public class Assigned_TestMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public int id { get; set; }

        [Required(ErrorMessage = "Required")]
        [ForeignKey("Test")]
        [DisplayName("Test")]
        public int test_id { get; set; }

        [Required(ErrorMessage = "Required")]
        [ForeignKey("Student")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Student")]
        public int scholar_no { get; set; }
    }
    public class ClassMetadata
    {
        [Key]
        [DisplayName("Class ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int class_id { get; set; }

        [Required(ErrorMessage = "Required")]
        [ForeignKey("Course")]
        [DisplayName("Course/Stream")]
        public string course_name { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Class Coordinator")]
        [ForeignKey("Teacher")]
        public int class_coordinator { get; set; }

        [Required]
        [DisplayName("Year")]
        [RegularExpression(@"^(\d{1})$", ErrorMessage = "Wrong year")]
        public byte year { get; set; }

        [Required]
        [DisplayName("Semester")]
        [RegularExpression(@"^(\d{1})$", ErrorMessage = "Wrong Semester")]
        public byte semester { get; set; }
    }
    public class CourseMetadata
    {
        [Key]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Course/Stream")]
        [StringLength(100, ErrorMessage = "Course/Stream is too long.")]
        [RegularExpression(@"([0-9A-Za-z .\-()]+)", ErrorMessage = "Invalid Course/Stream")]
        public string course_name { get; set; }
    }
    public class LogerMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Log ID")]
        public int log_id { get; set; }

        [DisplayName("Description")]
        [StringLength(1000, ErrorMessage = "Description is too long.")]
        public string description { get; set; }

        [DisplayName("Category")]
        [StringLength(50, ErrorMessage = "Category is too long.")]
        public string category { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Logged at Date/Time")]
        public System.DateTime log_datetime { get; set; }
    }
    public class QuestionMetadata
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Q ID")]
        public int q_id { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Subject")]
        public int subject_code { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Question")]
        [StringLength(500, ErrorMessage = "Question text is too long.")]
        public string question_text { get; set; }

        [DisplayName("1)")]
        [StringLength(200, ErrorMessage = "Option1 is too long.")]
        public string option1 { get; set; }

        [DisplayName("2)")]
        [StringLength(200, ErrorMessage = "Option2 is too long.")]
        public string option2 { get; set; }

        [DisplayName("3)")]
        [StringLength(200, ErrorMessage = "Option3 is too long.")]
        public string option3 { get; set; }

        [DisplayName("4)")]
        [StringLength(200, ErrorMessage = "Option4 is too long.")]
        public string option4 { get; set; }

        [DisplayName("is option1 correct?")]
        public Nullable<bool> is_option1_correct { get; set; }

        [DisplayName("is option2 correct?")]
        public Nullable<bool> is_option2_correct { get; set; }

        [DisplayName("is option3 correct?")]
        public Nullable<bool> is_option3_correct { get; set; }

        [DisplayName("is option4 correct?")]
        public Nullable<bool> is_option4_correct { get; set; }

        [ForeignKey("Teacher")]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Created By")]
        public int creator { get; set; }
        
        [DisplayName("Difficulty Level")]
        [Required(ErrorMessage = "Required")]
        [Column(TypeName = "numeric")]
        [RegularExpression("[0-9]{1}", ErrorMessage = "Invalid difficulty. The value must be between 0 and 9.")]
        [Range(0, 9, ErrorMessage = "Invalid difficulty. The value must be between 0 and 9.")]
        public int difficulty_level { get; set; }

        [StringLength(100, ErrorMessage = "Topic name is too long.")]
        [DisplayName("Topic")]
        public string topic { get; set; }

    }
    public class Registered_CadidatesMetadata
    {
        [Key]
        [ForeignKey("Test")]
        [Column(Order = 0)]
        [Required(ErrorMessage = "Required")]
        public int test_id { get; set; }

        [Key]
        [ForeignKey("Student")]
        [Column(Order = 1)]
        [Required(ErrorMessage = "Required")]
        public int scholar_no { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Can this student view result?")]
        public bool approved { get; set; }

        [DisplayName("Marks")]
        [RegularExpression(@"^\d{0,2}(\.\d{1,2})?$", ErrorMessage = "Invalid Marks")]
        public decimal? marks { get; set; }

        public virtual Student Student { get; set; }
        public virtual Test Test { get; set; }
    }
    public class Student_Current_QualificationMetadata
    {
        [Key]
        [ForeignKey("Student")]
        [Column(Order = 0)]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Student")]
        public int scholar_no { get; set; }

        [Key]
        [ForeignKey("Subject")]
        [Column(Order = 1)]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Subject")]
        public int subject_code { get; set; }

        [Column(TypeName = "numeric")]
        [DisplayName("Marks")]
        [RegularExpression(@"^\d{0,2}(\.\d{1,2})?$", ErrorMessage = "Invalid Marks")]
        public decimal marks { get; set; }

        [Column(TypeName = "smalldatetime")]
        [DisplayName("Last Appeared")]
        public DateTime last_appeared { get; set; }

        public virtual IEnumerable<Student> Student { get; set; }

        public virtual IEnumerable<Subject> Subject { get; set; }
    }
    public class Student_10th_QualificationMetadata
    {
        [Key]
        [ForeignKey("Student")]
        [DisplayName("Student")]
        [Column(Order = 0)]
        [Required(ErrorMessage = "Required")]
        public int scholar_no { get; set; }

        [ForeignKey("Subjects_In_10th")]
        [Required(ErrorMessage = "Required")]
        [Column(Order = 1)]
        [DisplayName("Subject in 10th")]
        public int subject_code { get; set; }

        [Column(TypeName = "numeric")]
        [DisplayName("Marks In Percent")]
        [RegularExpression(@"^0(\.\d{1,4})?$", ErrorMessage = "Invalid Marks in 10th subject")]
        public decimal percent_marks { get; set; }

    }

    public class Student_12th_QualificationMetadata
    {
        [Key]
        [ForeignKey("Student")]
        [Column(Order = 0)]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Student")]
        public int scholar_no { get; set; }

        [Key]
        [ForeignKey("Subjects_In_12th")]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Subject in 12th")]
        [Column(Order = 1)]
        public int subject_code { get; set; }

        [Column(TypeName = "numeric")]
        [DisplayName("Marks")]
        [RegularExpression(@"^0(\.\d{1,4})?$", ErrorMessage = "Invalid Marks in 12th subject")]
        public decimal percent_marks { get; set; }

    }
    public class SubjectMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Subject code")]
        public int subject_code { get; set; }
        
        [Required(ErrorMessage = "Required")]
        [DisplayName("Subject")]
        [StringLength(100, ErrorMessage = "Subject name is too long.")]
        public string subject_name { get; set; }
    }

    public class Subjects_In_10thMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Subject code")]
        public int subject_code { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Subject")]
        [StringLength(100, ErrorMessage = "Subject name is too long.")]
        public string subject_name { get; set; }
    }
    public class Subjects_In_12thMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Subject code")]
        public int subject_code { get; set; }
        
        [Required(ErrorMessage = "Required")]
        [DisplayName("Subject")]
        [StringLength(100, ErrorMessage = "Subject name is too long.")]
        public string subject_name { get; set; }
    }
    public class TeachMetadata
    {   
        [Key]
        [ForeignKey("Teacher")]
        [Column(Order = 0)]
        [Required(ErrorMessage = "Required")]
        public int faculty_id { get; set; }

        [Key]
        [ForeignKey("Class")]
        [Column(Order = 1)]
        [Required(ErrorMessage = "Required")]
        public int class_id { get; set; }

        [Key]
        [ForeignKey("Subject")]
        [Column(Order = 2)]
        [Required(ErrorMessage = "Required")]
        public int subject_code { get; set; }

        public virtual Class Class { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
    public class TeacherMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Teacher ID")]
        public int faculty_id { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Teachers Name")]
        [StringLength(50, ErrorMessage = "Teacher-name is too long.")]
        [RegularExpression("([a-zA-Z .]+)", ErrorMessage = "Invalid Name")]
        public string faculty_name { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date),DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public System.DateTime dob { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Blood-group")]
        [StringLength(5, ErrorMessage = "Invalid Blood Group")]
        [RegularExpression(@"^(A|B|AB|O)[+-]$", ErrorMessage = "Invalid Blood-group")]
        public string blood_group { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(200, ErrorMessage = "Residential address is too long.")]
        [DisplayName("Residential Address")]
        [RegularExpression(@"([0-9A-Za-z ,.\-()]+)", ErrorMessage = "Invalid Residential-address")]
        public string residential_address { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Mobile")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:##########}")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]
        [DataType(DataType.PhoneNumber)]
        public decimal mobile { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Graduate from")]
        [StringLength(200, ErrorMessage = "Graduate is too long.")]
        [RegularExpression(@"([0-9A-Za-z ,.\-()]+)", ErrorMessage = "Invalid Graduation")]
        public string graduate { get; set; }

        [DisplayName("Post graduate from")]
        [StringLength(200, ErrorMessage = "Post graduate is too long.")]
        [RegularExpression(@"([0-9A-Za-z ,.\-()]+)", ErrorMessage = "Invalid Post-graduation")]
        public string post_graduate { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Year of graduation")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:####}")]
        [DataType(DataType.PhoneNumber)]
        [Range(2019-100, 3000, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [RegularExpression(@"^(\d{4})$", ErrorMessage = "Wrong year")]
        public decimal year_graduated { get; set; }

        [RegularExpression(@"^(\d{4})?$", ErrorMessage = "Wrong year")]
        [DisplayName("Year of post-graduation")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:####}")]
        [DataType(DataType.PhoneNumber)]
        [Range(2019 - 100, 3000, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public Nullable<decimal> year_post_graduated { get; set; }

        [DisplayName("Served in organisations")]
        [StringLength(200, ErrorMessage = "Served-in is too long.")]
        [RegularExpression(@"([0-9A-Za-z ,.\-()]+)", ErrorMessage = "Invalid Served")]
        public string served { get; set; }
    }

    public class TeacherAdminDeskMetadata:TeacherMetadata
    {
        [DisplayName("Is Coordinator")]
        public bool is_coordinator { get; set; }

        [DisplayName("Username")]
        public string username { get; set; }
    }
    public class TestMetadata
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Test ID")]
        public int test_id { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Test title")]
        [StringLength(100, ErrorMessage = "Test title is too long.")]
        [RegularExpression(@"([0-9A-Za-z .\-()]+)", ErrorMessage = "Invalid title")]
        public string test_name { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Description")]
        [StringLength(200, ErrorMessage = "Description is too long.")]
        [RegularExpression(@"([0-9A-Za-z ,.\-()]+)", ErrorMessage = "Invalid description")]
        public string description { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Start date/time")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime start_datetime { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("End time")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime end_datetime { get; set; }

        [ForeignKey("Teacher")]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Creator")]
        public int creator { get; set; }

        [ForeignKey("Subject")]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Subject")]
        public int subject_code { get; set; }
    }

    public class Test_QuestionMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public int id { get; set; }

        [ForeignKey("Test")]
        [Required(ErrorMessage = "Required")]
        public int test_id { get; set; }

        [ForeignKey("Question")]
        [Required(ErrorMessage = "Required")]
        public int question_id { get; set; }

        public virtual Question Question { get; set; }
        public virtual Test Test { get; set; }
    }

    public class UnassignedTestMetadata
    {
        [DisplayName("Test ID")]
        public int test_id { get; set; }

        [DisplayName("Test name")]
        public string test_name { get; set; }

        [DisplayName("Description")]
        public string description { get; set; }

        [DisplayName("Subject")]
        public string subject_name { get; set; }

        [DisplayName("Date")]
        public Nullable<System.DateTime> date { get; set; }

        [DisplayName("Start Time")]
        public Nullable<System.TimeSpan> start_time { get; set; }

        [DisplayName("End Time")]
        public Nullable<System.TimeSpan> end_time { get; set; }

        [DisplayName("Test Creator")]
        public string creator { get; set; }
    }

    public class UnapprovedResultMetadata
    {

        [DisplayName("Student name")]
        public string unique_name { get; set; }

        [DisplayName("Semester")]
        public byte semester { get; set; }

        [DisplayName("Year")]
        public byte year { get; set; }

        [DisplayName("Course")]
        public string course_name { get; set; }

        [DisplayName("Test Creator")]
        public int creator { get; set; }
        
        [DisplayName("Subject")]
        public string subject_name { get; set; }

        [DisplayName("Photo")]
        public string photo { get; set; }

        [DisplayName("Test title")]
        public string test_name { get; set; }

        [DisplayName("Marks approved")]
        public bool approved { get; set; }

        [DisplayName("Marks")]
        public Nullable<decimal> marks { get; set; }

        [DisplayName("Scholar no.")]
        public int scholar_no { get; set; }

        [DisplayName("Class")]
        public string Pretty_Class_name { get; }
    }

    public class AssignTestPartialMetadata
    {
        [DisplayName("Scholar no.")]
        public int scholar_no { get; set; }

        [DisplayName("Student name")]
        public string student_name { get; }

        [DisplayName("ATKT Subjects")]
        public List<string> student_ATKT_subjects { get; }

        [DisplayName("Class")]
        public string pretty_class { get; }
    }

    public class AssignTestStudentPartial2Metadata
    {

        [DisplayName("Scholar no.")]
        public int scholar_no { get; set; }

        [DisplayName("Photo")]
        public string photo { get; set; }
        
        [DisplayName("Student name")]
        public string unique_name { get; set; }

        [DisplayName("Mobile")]
        public string student_mobile { get; set; }
        
        [DisplayName("Parents phone")]
        public string parents_phone { get; set; }

        [DisplayName("Residential address")]
        public string residential_address { get; set; }

        [DisplayName("Coordinator")]
        public string class_coordinator { get; set; }

        [DisplayName("Student Current Qualifications")]
        public IEnumerable<Student_Current_Qualification> student_current_qualifications { get; set; }
    }

    public class UserLoginMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("User ID")]
        public int user_id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, ErrorMessage = "Invalid Username")]
        [DisplayName("Username")]
        [RegularExpression("([0-9A-Za-z ,.-]+)", ErrorMessage = "Invalid Username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, ErrorMessage = "Password is too long")]
        [DisplayName("Password")]
        [RegularExpression("([0-9A-Za-z ,.-]+)", ErrorMessage = "Invalid Password")]
        public string password { get; set; }
    }

    public class UserRoleMetadata
    {
        [Key,Column(Order=1)]
        [ForeignKey("UserLogin")]
        [Required(ErrorMessage = "Required")]
        [DisplayName("User")]
        public int user_id { get; set; }

        [Key, Column(Order = 2)]
        [Required(ErrorMessage = "Required")]
        [StringLength(50, ErrorMessage = "Role is too long")]
        [DisplayName("Role")]
        [RegularExpression("([0-9A-Za-z ,.-]+)", ErrorMessage = "Invalid User Role")]
        public string role { get; set; }
    }
}

namespace ExamPortal.Models.ViewModels
{
    public class TeacherVMMetadata:TeacherMetadata
    {

        [Required(ErrorMessage = "Required")]
        [DisplayName("Username")]
        [Remote("IsTeacherUsernameExists", "Teachers", ErrorMessage = "Username already in use")]
        public string username { get; set; }


        [Required(ErrorMessage = "Required")]
        [DisplayName("Password")]
        public string password { get; set; }

    }
    public class CreateClassVMMetadata : ClassMetadata
    {
     
    }
    public class ModifyTestVMMetadata : TestMetadata
    {

    }
    public class QuestionVMMetadata : QuestionMetadata
    {
        /*public int new_q_id { get; }*/
        public bool use_option1 { get; set; }
        public bool use_option2 { get; set; }
        public bool use_option3 { get; set; }
        public bool use_option4 { get; set; }
        public new string option1 { get; set; }
        public new string option2 { get; set; }
        public new string option3 { get; set; }
        public new string option4 { get; set; }
        public new bool is_option1_correct { get; set; }
        public new bool is_option2_correct { get; set; }
        public new bool is_option3_correct { get; set; }
        public new bool is_option4_correct { get; set; }
    }
    public class TestResultMetadata : QuestionVMMetadata
    {

    }
    public class ViewQuestionVMMetadata : QuestionMetadata
    {

    }
    public class CreateStudentVMMetadata : StudentMetadata
    {

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public new System.DateTime dob { get; set; }

        [DisplayName("Class")]
        public string Pretty_Class_name { get; set; }
    }
}
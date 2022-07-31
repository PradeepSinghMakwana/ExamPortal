using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPortal.Tests.Models
{
    public class TestModelHelper
    {
        public static IList<ValidationResult> Validate(object model) {
            var results = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);

            Validator.TryValidateObject(model, validationContext, results, true);

            if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);
            return results;
        }

        public static ExamPortal.Models.Question MockQuestion()
        {
            ExamPortal.Models.Question q = new ExamPortal.Models.Question()
            {
                q_id = 1,
                subject_code = MockSubject().subject_code,
                question_text = "This is question 1.",
                option1 = "this is option1 of question1",
                option2 = "this is option2 of question1",
                option3 = "this is option3 of question1",
                option4 = "this is option4 of question1",
                is_option1_correct=false,
                is_option3_correct=true,
                is_option2_correct=true,
                is_option4_correct=false
            };

            return q;
        }

        public static ExamPortal.Models.Test_Question MockTestQuestion()
        {
            ExamPortal.Models.Test_Question tq = new ExamPortal.Models.Test_Question()
            {
                id = 1,
                test_id = MockTest().test_id,
                question_id = MockQuestion().q_id
            };

            return tq;
        }
        /*
        public static ExamPortal.Models.Teach MockTeaches()
        {
            ExamPortal.Models.Teach t = new ExamPortal.Models.Teach()
            {
                id=1,
                faculty_id = MockTeacher().faculty_id,
                class_id = MockClass().class_id,
                subject_code = MockSubject().subject_code
            };

            return t;
        }
        */
        public static ExamPortal.Models.Test MockTest()
        {
            ExamPortal.Models.Test t = new ExamPortal.Models.Test()
            {
                test_id=1,
                test_name="Test1",
                description="This is a test.",
                start_datetime=DateTime.Now,
                end_datetime = DateTime.Now,
                creator=MockTeacher().faculty_id,
                subject_code=MockSubject().subject_code
            };

            return t;
        }

        public static ExamPortal.Models.ViewModels.TeacherVM MockTeacher()
        {
            ExamPortal.Models.ViewModels.TeacherVM t = new ExamPortal.Models.ViewModels.TeacherVM()
            {
                faculty_id=1,
                faculty_name="tefirst telast",
                dob=DateTime.Now,
                blood_group="B+",
                residential_address = "this is, residential address 1.",
                mobile=1234567890,
                username="teacherUsername",
                password="teacherPassword",
                graduate="St.Maritz College",
                post_graduate="",
                year_graduated=DateTime.Now.Year,
                year_post_graduated=1986,
                served="ABC, INTS9940(AQT-M4)"
            };

            return t;
        }
        /*
        public static ExamPortal.Models.Student MockStudent()
        {
            ExamPortal.Models.Student s = new ExamPortal.Models.Student()
            {
                scholar_no = 1,
                unique_name = "stfirst stlast",
                parents_phone = 9123456780,
                mobile = 1234567890,
                photo = "1.jpg",
                enrollment_no = "V16R123456789",
                residential_address = "this is, residential address 1.",
                permanent_address = "this is, permanent address 1.",
                fathers_name = "fathers. name",
                mothers_name="mothers. name",
                adhar_no = 123456789012,
                blood_group = "B+",
                dob = DateTime.Now,
                class_id = MockClass().class_id,
                username = "username123",
                password = "password345"
            };

            return s;
        }
        */

        public static ExamPortal.Models.Course MockCourse()
        {
            ExamPortal.Models.Course c = new ExamPortal.Models.Course()
            {
                course_name = "B.Com(CA)"
            };

            return c;
        }

        public static ExamPortal.Models.Subject MockSubject()
        {
            ExamPortal.Models.Subject s = new ExamPortal.Models.Subject()
            {
                subject_code = 1,
                subject_name="C & Unix"
            };

            return s;
        }

        public static ExamPortal.Models.Class MockClass()
        {
            ExamPortal.Models.Class c = new ExamPortal.Models.Class()
            {
                class_id = 1,
                class_coordinator = MockTeacher().faculty_id,
                year = 1,
                semester = 1,
                course_name = MockCourse().course_name
            };

            return c;
        }
        /*
        public static ExamPortal.Models.Admin MockAdmin()
        {
            ExamPortal.Models.Admin a = new ExamPortal.Models.Admin()
            {
                admin_id = 1,
                username = "administrator",
                password="administrator"
            };

            return a;
        }
        */
        /*
        public static ExamPortal.Models.Assigned_Test MockAssignedTest()
        {
            ExamPortal.Models.Assigned_Test s = new ExamPortal.Models.Assigned_Test()
            {
                scholar_no = MockStudent().scholar_no,
                test_id = MockTest().test_id
            };

            return s;
        }
        */
        public static ExamPortal.Models.Loger MockLoger()
        {
            ExamPortal.Models.Loger s = new ExamPortal.Models.Loger()
            {
                log_id = 1,
                description = "This is some log. (description).",
                category = "Class",
                log_datetime = DateTime.Now
            };

            return s;
        }
        /*
        public static ExamPortal.Models.Registered_Candidates MockRegisteredCandidates()
        {
            ExamPortal.Models.Registered_Candidates s = new ExamPortal.Models.Registered_Candidates()
            {
                test_id = MockTest().test_id,
                scholar_no = MockStudent().scholar_no,
                approved = false
            };

            return s;
        }
        */
        /*
        public static ExamPortal.Models.Student_Current_Qualification MockStudentCurrentQualification()
        {
            ExamPortal.Models.Student_Current_Qualification s = new ExamPortal.Models.Student_Current_Qualification()
            {
                id=1,
                scholar_no=MockStudent().scholar_no,
                subject_code=MockSubject().subject_code
            };

            return s;
        }
        
        public static ExamPortal.Models.Student_Previous_Qualification MockStudentPreviousQualification()
        {
            ExamPortal.Models.Student_Previous_Qualification s = new ExamPortal.Models.Student_Previous_Qualification()
            {
                scholar_no=MockStudent().scholar_no,
                marks_english_12=Convert.ToDecimal(23.50),
                marks_mathematics_12=Convert.ToDecimal(34.42),
                marks_physics_12=Convert.ToDecimal(34.24),
                marks_chemistry_12=Convert.ToDecimal(78.84),
                marks_biology_12=Convert.ToDecimal(57.97),
                marks_computer_science_12=Convert.ToDecimal(89.79),
                marks_physical_education_12=Convert.ToDecimal(98.7),
                marks_business_studies_12=Convert.ToDecimal(86.78),
                marks_accountancy_12=Convert.ToDecimal(6),
                marks_history_12=Convert.ToDecimal(0),
                marks_political_science_12=Convert.ToDecimal(8.94),
                marks_economics_12=Convert.ToDecimal(09.87),
                marks_psychology_12=Convert.ToDecimal(29.75),
                marks_hindi_12=Convert.ToDecimal(84.3),
                marks_sociology_12=Convert.ToDecimal(97.58),
                marks_english_10=Convert.ToDecimal(12.34),
                marks_mathematics_10=Convert.ToDecimal(0.75),
                marks_science_10=Convert.ToDecimal(.97),
                marks_hindi_10=Convert.ToDecimal(34)
            };

            return s;
        }
        */
    }
}

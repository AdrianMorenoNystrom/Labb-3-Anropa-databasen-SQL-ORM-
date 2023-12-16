using LABB3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABB3
{
    public class DataBaseManager
    {
        private SchoolContext dbContext;

        public DataBaseManager()
        {
            dbContext = new SchoolContext();
            dbContext.Database.EnsureCreated();
        }
        public void createPersonal()
        {
            Console.WriteLine("Skriv in information om den anställde:");

            Console.Write("Förnamn: ");
            string firstName = Console.ReadLine();

            Console.Write("Efternamn: ");
            string lastName = Console.ReadLine();

            Console.Write("Befattning: ");
            string role = Console.ReadLine();

            string personalNumber;

            while (true)
            {
                Console.Write("Personnummer (ÅÅMMDD-XXXX): ");
                personalNumber = Console.ReadLine();

                if (ValidatePersonalNumber(personalNumber))
                {
                    var newPersonal = new PersonalTabell
                    {
                        FörNamn = firstName,
                        EfterNamn = lastName,
                        Befattning = role,
                        Personnummer = personalNumber
                    };

                    // Add the new student to the database
                    dbContext.PersonalTabells.Add(newPersonal);
                    dbContext.SaveChanges();

                    Console.WriteLine("Anställd tillagd i databasen");

                    // Exit the loop since a valid personal number was provided
                    break;
                }
            }
        }

            public void createStudent()
            {
                Console.WriteLine("Skriv in information om eleven:");

                Console.Write("ElevID: ");
                int studentId = int.Parse(Console.ReadLine());

                Console.Write("Förnamn: ");
                string firstName = Console.ReadLine();

                Console.Write("Efternamn: ");
                string lastName = Console.ReadLine();

                string personalNumber;

                while (true)
                {
                    Console.Write("Personnummer (ÅÅMMDD-XXXX): ");
                    personalNumber = Console.ReadLine();

                    if (ValidatePersonalNumber(personalNumber))
                    {
                        var newStudent = new StudentTabell
                        {
                            StudentIdPk = studentId,
                            FörNamn = firstName,
                            EfterNamn = lastName,
                            Personnummer = personalNumber
                        };

                        // Add the new student to the database
                        dbContext.StudentTabells.Add(newStudent);
                        dbContext.SaveChanges();

                        Console.WriteLine("Elev tillagd i databasen");

                        // Exit the loop since a valid personal number was provided
                        break;
                    }

                }

            }

            public bool ValidatePersonalNumber(string personalNumber)
            {
                // Check if the length is correct
                if (personalNumber.Length != 11)
                {
                    Console.WriteLine("Ogiltig längd på personumret");
                    return false;
                }

                // Check if the format is correct
                if (!personalNumber.Substring(6, 1).Equals("-"))
                {
                    Console.WriteLine("Ogiltigt format på personummret.");
                    return false;
                }
                // The personal number is valid
                return true;
            }

            public void GetGradesAndCourses()
            {
                var courseStatistics = dbContext.KursTabells
                .Select(course => new
                {
                    CourseName = course.KursNamn,
                    Grades = dbContext.BetygTabells
                .Where(grade => grade.KursIdFk == course.KursIdPk)
                .Select(grade => GradeToNumeric(grade.Betyg))
                .AsEnumerable()
                })
                .ToList();

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Lista över kurser med statistik:");
                Console.ResetColor();
                foreach (var statistics in courseStatistics)
                {
                    var gradesList = statistics.Grades.ToList();

                    var averageGrade = gradesList.DefaultIfEmpty(0).Average();
                    var maxGrade = gradesList.DefaultIfEmpty(0).Max();
                    var minGrade = gradesList.DefaultIfEmpty(0).Min();

                    Console.WriteLine($"Kurs: {statistics.CourseName}, Snittbetyg: {averageGrade}, Högsta betyg: {maxGrade}, Lägsta betyg: {minGrade}");
                }
            }

            private static int GradeToNumeric(string gradeChar)
            {
                return gradeChar.Trim() switch
                {
                    "A" => (int)6,
                    "B" => (int)5,
                    "C" => (int)4,
                    "D" => (int)3,
                    "E" => (int)2,
                    "F" => (int)1,
                };
            }

            public void GetGrades()
            {

                DateTime currentDate = DateTime.Now;

                DateTime lastMonth = currentDate.AddMonths(-1);

                var grades = dbContext.BetygTabells.ToList()
                .Join(
                dbContext.StudentTabells,
                grade => grade.StudentIdFk,
                student => student.StudentIdPk,
                (grade, student) => new { grade, student }

            )
                .Join(
                dbContext.KursTabells,
                combined => combined.grade.KursIdFk,
                course => course.KursIdPk,
                (combined, courses) => new { combined.grade, combined.student, courses }
            )
            .ToList();


                foreach (var grade in grades)
                {
                    if (grade.grade.BetygDatum >= lastMonth && grade.grade.BetygDatum <= currentDate)
                    {
                        string formattedDate = grade.grade.BetygDatum?.ToString("yyyy-MM-dd");
                        Console.WriteLine($"\nNamn:{grade.student.FörNamn}{grade.student.EfterNamn} \nKurs:{grade.courses.KursNamn} \nBetyg:{grade.grade.Betyg} \nBetygsättningsdatum:{formattedDate}");
                    }
                }

                string currentdate = currentDate.ToString("yyyy-MM-dd");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Dagens datum: {currentdate}");
                Console.ResetColor();
            }
            //Hämtar alla elever och en switch med olika utfall beroende på vad användaren väljer.
            public void GetStudents(string sortBy, bool ascending)
            {
                //var students = dbContext.StudentTabells.OrderByDescending(x=>x.FörNamn).ToList();

                var studentsQuery = dbContext.StudentTabells.AsQueryable();

                switch (sortBy)
                {
                    case "firstname":
                        studentsQuery = ascending ? studentsQuery.OrderBy(s => s.FörNamn) : studentsQuery.OrderByDescending(s => s.FörNamn);
                        break;
                    case "lastname":
                        studentsQuery = ascending ? studentsQuery.OrderBy(s => s.EfterNamn) : studentsQuery.OrderByDescending(s => s.EfterNamn);
                        break;
                    default:
                        Console.WriteLine("Ogiltig siffra för elevlista.");
                        return;
                }

                var students = studentsQuery.ToList();

                foreach (var student in students)
                {

                    Console.WriteLine($"{student.FörNamn}{student.EfterNamn}");
                }
            }
            //Hämtar alla ur personaltabellen oavsett befattning.
            public void GetEmployees()
            {
                var employees = dbContext.PersonalTabells.ToList();


                Console.WriteLine("Lista över alla anställda:");
                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FörNamn} {employee.EfterNamn} - Befattning: {employee.Befattning}");
                }
            }

            //Hämtar anställda ur personaltabellen beroende på vilken befattning dom har.
            public void GetEmployeesByPosition(int positionChoice)
            {

                if (positionChoice == 8)
                {
                    GetEmployees();
                    return;
                }

                string position;

                switch (positionChoice)
                {
                    case 1:
                        position = "Lärare";
                        break;
                    case 2:
                        position = "Rektor";
                        break;
                    case 3:
                        position = "Administratör";
                        break;
                    case 4:
                        position = "Vaktmästare";
                        break;
                    case 5:
                        position = "Kioskansvarig";
                        break;
                    case 6:
                        position = "Städare";
                        break;
                    case 7:
                        position = "Ägare";
                        break;
                    default:
                        Console.WriteLine("Ogiltig siffra för befattning.");
                        return;
                }

                var employees = dbContext.PersonalTabells
                    .Where(employees => employees.Befattning == position)
                    .ToList();

                Console.WriteLine($"\nLista över alla {position.ToLower()}:");
                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FörNamn}{employee.EfterNamn}");
                }
            }
        }
    }




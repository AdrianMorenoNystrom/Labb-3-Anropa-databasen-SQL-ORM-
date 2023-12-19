using LABB3.Models;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABB3
{
    public class Menu
    {
        private SchoolContext dbContext;
        public void ShowMenu()
        {
            DataBaseManager database = new DataBaseManager();
            ChoiceManager choices = new ChoiceManager();

            choices.ShowChoices();

            int choice = choices.GetUserChoice();
            while (choice != 9)
            {
                switch (choice)
                {
                    case 1:
                        database.GetEmployeesByPosition();
                        break;
                    case 2:
                        database.GetStudents();
                        break;
                    case 3:
                        database.GetStudentsInClass();
                        break;
                    case 4:
                        database.GetGrades();
                        break;
                    case 5:
                        database.GetGradesAndCourses();
                        break;
                    case 6:
                        database.createStudent();
                        break;
                    case 7:
                        database.createPersonal();
                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Tryck valfri tangent för att gå tillbaka...");
                Console.ReadKey();
                Console.Clear(); // Rensa konsolfönstret för att visa menyn igen
                ShowMenu();

                choice = choices.GetUserChoice();
            }
        }
    }
}

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
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Välkommen till navigationsmenyn för skoldatabasen!");
            Console.ResetColor();
            Console.WriteLine("1. Hämta en lista på anställda.");
            Console.WriteLine("2. Hämta en lista på alla elever.");
            Console.WriteLine("3. Hämta en lista på alla betyg satt den senaste månaden.");
            Console.WriteLine("4. Hämta en lista på alla kurser i databasen, snittbetyget samt det högsta och lägsta betyget för varje kurs.");
            Console.WriteLine("5. Lägg till en elev i databasen");
            Console.WriteLine("6. Lägg till en anställd i databasen");
            Console.WriteLine("7. Avsluta");

            int choice = GetUserChoice();
            
            

            DataBaseManager database = new DataBaseManager();
 

            while (choice != 7)
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine(
                            "1.Lärare\n" +
                            "2.Rektor\n" +
                            "3.Administratörer\n" +
                            "4.Vaktmästare\n" +
                            "5.Kioskansvarig\n" +
                            "6.Städare\n" +
                            "7.Ägare\n" +
                            "8.Alla anställda");

                        Console.Write("Välj befattning(1-8): ");
                        int positionChoice;
                        if (int.TryParse(Console.ReadLine(), out positionChoice))
                        {
                            database.GetEmployeesByPosition(positionChoice);
                        }
                        else
                        {
                            Console.WriteLine("Ogiltig inmatning. Ange en siffra.");
                        }
                        break;

                    case 2:
                        Console.WriteLine("1. Förnamn stigande");
                        Console.WriteLine("2. Förnamn fallande");
                        Console.WriteLine("3. Efternamn stigande");
                        Console.WriteLine("4. Efternamn fallande");

                        Console.Write("Välj sortering för elever: ");
                        int sortChoice;
                        if (int.TryParse(Console.ReadLine(), out sortChoice))
                        {
                            string sortBy = "lastname";
                            bool ascending = true;

                            switch (sortChoice)
                            {
                                case 1:
                                    sortBy = "firstname";
                                    ascending = true;
                                    break;
                                case 2:
                                    sortBy = "firstname";
                                    ascending = false;
                                    break;
                                case 3:
                                    sortBy = "lastname";
                                    ascending = true;
                                    break;
                                case 4:
                                    sortBy = "lastname";
                                    ascending = false;
                                    break;
                                default:
                                    Console.WriteLine("Ogiltig inmatning för elevtabell");
                                    break;
                            }

                            database.GetStudents(sortBy, ascending);
                        }
                        else
                        {
                            Console.WriteLine("Ogiltig inmatning. Ange en siffra.");
                        }
                        
                        break;

                    case 3:
                        database.GetGrades();
                        break;
                    case 4:
                        database.GetGradesAndCourses();
                        Console.WriteLine("\nSiffrorna representerar betygen\n" +
                            "A=6\n" +
                            "B=5\n" +
                            "C=4\n" +
                            "D=3\n" +
                            "E=2\n" +
                            "F=1");
                        break;
                    case 5:
                        database.createStudent();
                        break;
                    case 6:
                        database.createPersonal();
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

                choice = GetUserChoice();
            }

            Console.WriteLine("Programmet avslutas................");
        }

        private int GetUserChoice()
        {
            Console.Write("Gör ditt val (1-7): ");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Ogiltigt val. Försök igen.");
                Console.Write("Gör ditt val (1-7): ");
                
            }
            Console.Clear();
            return choice;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABB3
{
    public class ChoiceManager
    {
        public void ShowChoices()
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
            Console.Write("Gör ditt val (1-7): ");
        }
        public int PositionChoice()
        { 
            int PositionChoice;
            while (!int.TryParse(Console.ReadLine(), out PositionChoice))
            {
                Console.WriteLine("Ogiltigt val. Försök igen.");
            }
            Console.Clear();
            return PositionChoice;
        }

        public int GetUserChoice()
        {
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

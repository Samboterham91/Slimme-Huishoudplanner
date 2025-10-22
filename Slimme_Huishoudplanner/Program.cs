namespace Slimme_Huishoudplanner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            int choice;

            do
            {
                Console.WriteLine("\n--- Hoofdmenu ---");
                Console.WriteLine("\n--- Welkom! ---");

                Console.WriteLine("Maak een keuze: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Ongeldige invoer. Probeer het opnieuw.");
                    continue;
                }
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Programma wordt afgesloten.");
                        break;
                    case 1:
                        Console.WriteLine("Je hebt gekozen voor optie 1.");
                        // Voeg hier de code toe voor optie 1
                        break;
                    default:
                        Console.WriteLine("Ongeldige keuze. Probeer het opnieuw.");
                        break;
                }
            } while (choice != 0);
        }
    }
}

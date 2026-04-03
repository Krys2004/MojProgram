using System;
using System.IO;

class Program {
    static void Main() {
        Console.Title = "Generator Kosztorysu";
        Console.WriteLine("=== GENERATOR KOSZTORYSU ===");

        // Pobieranie danych od użytkownika
        Console.Write("Podaj nazwe projektu: ");
        string projekt = Console.ReadLine();

        Console.Write("Podaj nazwe uslugi: ");
        string usluga = Console.ReadLine();

        Console.Write("Podaj kwote (PLN): ");
        string kwota = Console.ReadLine();

        // Lokalizacja Pulpitu
        string pulpit = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string nazwaPliku = "Kosztorys_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".txt";
        string sciezka = Path.Combine(pulpit, nazwaPliku);

        // Treść kosztorysu
        string tresc = "==============================\n" +
                       "       KOSZTORYS PROJEKTU     \n" +
                       "==============================\n" +
                       "Data wystawienia: " + DateTime.Now.ToString() + "\n" +
                       "Nazwa projektu:   " + projekt + "\n" +
                       "------------------------------\n" +
                       "Opis uslugi:      " + usluga + "\n" +
                       "Wartosc:          " + kwota + " PLN\n" +
                       "==============================\n" +
                       "Wygenerowano przez: MojProgram.exe";

        try {
            File.WriteAllText(sciezka, tresc);
            Console.WriteLine("\n[SUKCES] Plik zostal zapisany na pulpicie!");
            Console.WriteLine("Nazwa pliku: " + nazwaPliku);
            Console.WriteLine("\nAby zrobic PDF: Otworz ten plik i wybierz Plik -> Drukuj -> Microsoft Print to PDF.");
        } catch (Exception e) {
            Console.WriteLine("\n[BLAD] Nie udalo sie zapisac pliku: " + e.Message);
        }

        Console.WriteLine("\nNacisnij ENTER, aby zakonczyc...");
        Console.ReadLine();
    }
}

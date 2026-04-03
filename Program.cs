using System;
using System.IO;

class Program {
    static void Main() {
        Console.WriteLine("--- PROGRAM SKOMPILOWANY ONLINE ---");
        
        // Ścieżka do Twojego pulpitu
        string pulpit = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string sciezka = Path.Combine(pulpit, "wynik_z_githuba.txt");

        try {
            File.WriteAllText(sciezka, "Gratulacje! Ten plik stworzyl program .exe zbudowany na GitHubie.");
            Console.WriteLine("Sukces! Plik 'wynik_z_githuba.txt' pojawil sie na Twoim pulpicie.");
        } catch (Exception) {
            Console.WriteLine("Program zadzialal, ale nie mogl zapisac pliku na pulpicie.");
        }

        Console.WriteLine("\nNacisnij Enter, aby zamknąć...");
        Console.ReadLine();
    }
}

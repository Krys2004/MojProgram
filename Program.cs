using System;

class Program {
    static void Main() {
        // Zmiana kolorów dla efektu
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();

        Console.WriteLine("============================================");
        Console.WriteLine("   MOJA PIERWSZA APLIKACJA WINDOWS .EXE     ");
        Console.WriteLine("============================================");
        Console.ResetColor();

        Console.WriteLine($"\nData i godzina: {DateTime.Now}");
        Console.WriteLine($"Nazwa komputera: {Environment.MachineName}");
        Console.WriteLine($"Wersja systemu: {Environment.OSVersion}");
        Console.WriteLine($"Uzytkownik: {Environment.UserName}");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n[SUKCES] Program wykonal zadanie pomyślnie.");
        Console.ResetColor();

        Console.WriteLine("\nNacisnij dowolny klawisz, aby wyjsc...");
        Console.ReadKey(); // To sprawi, ze okno sie nie zamknie samo!
    }
}

using System;

namespace GuessNumberConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Игра «Угадай число» ===");
            Console.WriteLine("Компьютер загадал число от 1 до 100");
            
            Random random = new Random();
            int secretNumber = random.Next(1, 101);
            int attempts = 0;
            bool isGuessed = false;

            while (!isGuessed)
            {
                Console.Write("Введите ваше предположение: ");
                string input = Console.ReadLine();
                
                if (!int.TryParse(input, out int guess))
                {
                    Console.WriteLine("Ошибка: введите целое число!");
                    continue;
                }

                attempts++;

                if (guess < 1 || guess > 100)
                {
                    Console.WriteLine("Число должно быть от 1 до 100!");
                    continue;
                }

                if (guess < secretNumber)
                {
                    Console.WriteLine("Загаданное число БОЛЬШЕ");
                }
                else if (guess > secretNumber)
                {
                    Console.WriteLine("Загаданное число МЕНЬШЕ");
                }
                else
                {
                    isGuessed = true;
                    Console.WriteLine($"Поздравляю! Вы угадали число {secretNumber} за {attempts} попыток!");
                }
            }
            
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}

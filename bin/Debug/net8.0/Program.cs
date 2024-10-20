using System.Text;
using System.Security.Cryptography;
using PasswordManager;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Для выхода из программы используйте комманду /exit");
        CheckCreatedPassword();
    }
    public static void CheckCreatedPassword()
    {
        string filePath = "Password.json";
        if (!File.Exists(filePath))
        {
            using (FileStream file = new FileStream(filePath, FileMode.Create))
            {
                string password = CreateUserPassword(16);
                string hashPassword = ComputeHash(password, SHA256.Create());
                Console.WriteLine($"Ваш мастер пароль: {password}");
                JsonSerializer.Serialize(file, hashPassword);
            }
            CryptoKeysCreator.CreateKeys();
            ConsoleWorker.TakeInstruction();
        }
        else
        {
            using (FileStream reader = new FileStream(filePath, FileMode.Open))
            {
                string line = JsonSerializer.Deserialize<string>(reader);
                Console.WriteLine("Введите пароль:");
                string userPasswordInput = Console.ReadLine();
                if (userPasswordInput.ToLower() == "/exit")
                {
                    Environment.Exit(0);
                }
                string userPassword = ComputeHash(userPasswordInput, SHA256.Create());
                if (userPassword == line)
                {
                    ConsoleWorker.TakeInstruction();
                }
                else
                {
                    Console.WriteLine("Пароль неправильный"); //выводиться в консоль для пользователя, что пароль не верный
                    reader.Close(); //принудительное закрытие потока, чтобы не было ошибки
                    CheckCreatedPassword(); //циклиться метод, пока пароль не будет подтвержден. Переход на строку 
                }
            }
        }
    }
    public static string CreateUserPassword(int count)
    {
        string result = "";
        string chars = "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM!№;%:?*()_+=-";
        Random rand = new Random();
        for (int i = 0; i < count; i++)
        {
            result += chars[rand.Next(chars.Length)];
        }
        return result;
    }

    public static string ComputeHash(string input, HashAlgorithm algorithm)
    {
        return BitConverter.ToString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(input)));
    }
}


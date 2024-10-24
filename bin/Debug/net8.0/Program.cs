using System.Text;                      //подключение пространства имен, которое будет преобразовывать знаки в байты
using System.Security.Cryptography;     //подключение пространства имен, которое отвечает за службу шифрования
using PasswordManager;                  //подключение пространства имен проекта, чтобы обращаться ко всем классам проекта
using System.Text.Json;                 //подключение пространства имен, которое используется для десирилизации и сериализации JSON

class Program                           //определение класса
{
    static void Main(string[] args)     //метод, который является точкой входа в программу
    {
        Console.WriteLine("Для выхода из программы используйте комманду /exit"); //вывод в консоль подсказки по выходу
        CheckCreatedPassword();                                                  //вызов метода
    }
    public static void CheckCreatedPassword()                                       //Создание метода, который ничего не возвращает
    {
        string filePath = "Password.json";                                          //Объявление переменной, которая хранит имя файла
        if (!File.Exists(filePath))                                                 //Проверка, создан ли файл
        {                                                           
            using (FileStream file = new FileStream(filePath, FileMode.Create))     //Создания файла с именем из filePath (если файл не создан по условию из if)
            {
                string password = CreateUserPassword(16);                           //Объявление переменной, которая хранит из себя значение, возвращаемое методом. В методе генерируется пароль
                string hashPassword = ComputeHash(password, SHA256.Create());       //Объявление переменной, которая хранит из себя значение, вовзращаемое методом. В методе передаваемое значение представляется в виде байтов
                Console.WriteLine($"Ваш мастер пароль: {password}");                //Вывод в консоль сгенерированного пароля
                JsonSerializer.Serialize(file, hashPassword);                       //Запись закодированного пароля в файл в формате json
            }
            CryptoKeysCreator.CreateKeys();                                         //Вызов метода из класса CryptoKeysCreator для создания
            ConsoleWorker.TakeInstruction();                                        //Вызов метода из класса ConsoleWorker
        }
        else
        {
            using (FileStream reader = new FileStream(filePath, FileMode.Open))     //Открытие файла, если такой файл уже создан
            {
                string line = JsonSerializer.Deserialize<string>(reader);           //Объявление переменной, которая хранит десериализированную строку из созданного файла
                Console.WriteLine("Введите пароль:");                               //вывод в консоль указания что нужно делать
                string userPasswordInput = Console.ReadLine();                      //объявление переменной, которая хранит значение из вводимого в консоль
                if (userPasswordInput.ToLower() == "/exit")                         //приведение вводимого к нижнему регистру и проверка, равно ли это "/exit"
                {
                    Environment.Exit(0);                                            //Если условие true, то приложение закрывается
                }
                string userPassword = ComputeHash(userPasswordInput, SHA256.Create());  //Объявление переменной, которая хранит из себя значение, возвращаемое методом. В методе передаваемое значение представляется в виде байтов
                if (userPassword == line)                                               //проверка, равны ли байты вводимого пароля байтам пароля из файла
                {
                    ConsoleWorker.TakeInstruction();                                    //если условие true, то запустить метод из класса ConsoleWorker
                }
                else                                                                    //если условие false
                {
                    Console.WriteLine("Пароль неправильный");                           //выводиться в консоль для пользователя, что пароль не верный
                    reader.Close();                                                     //принудительное закрытие потока, чтобы не было ошибки
                    CheckCreatedPassword();                                             //циклиться метод, пока пароль не будет подтвержден. Перезапуск метода
                }
            }
        }
    }
    public static string CreateUserPassword(int count)                                  //Метод для генерации пароля, принимает int, который будет отвечать за размер пароля
    {
        string result = "";                                                             //объявление пустого стринга
        string chars = "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM!№;%:?*()_+=-";   //объявление переменной, в которой будут храниться все возможные символы для генерации пароля
        Random rand = new Random();                                                     //объявление класса, который представляет генератор псевдослучайных чисел
        for (int i = 0; i < count; i++)                                                 //цикл for, который записывает в result символы из chars, пока не будет count символов в пароле
        {
            result += chars[rand.Next(chars.Length)];                                   //добавление случайного символа из chars в result
        }
        return result;                                                                  //после окончание циклов возвращается пароль
    }

    public static string ComputeHash(string input, HashAlgorithm algorithm)             //Метод для преобразования получаемого пароля в байтовое представление и запись этого в строку
    {
        return BitConverter.ToString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(input))); //Возвращение строки, которая содержит пароль в байтовом представлении
    }
}


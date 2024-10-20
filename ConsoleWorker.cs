using Newtonsoft.Json;          //библиотека для работы с json

namespace PasswordManager       //объявление области действия 
{
    internal class ConsoleWorker    //определение класса, компоненты которого доступны из любого места кода в этой же сборке, однако класс недоступен для других программ и сборок
    {
        public static void TakeInstruction()       //
        {
            Console.WriteLine("Доступные команды:\n - get {name} - получение пароля по имени\n - set {name} {password} - создание пароля для имени. Важно! Не использовать пробел в имени и пароле\n - /exit - Выход из программы\nВведите Вашу команду:"); //подсказка по командам
            string[] userConsoleText = Console.ReadLine().Split(); //объявление массива, который хранит слова из вводимого пользователем
            string falseInstruction = "Incorrect format";          //объявление перемменной с текстом, который выводится при некорректном вводе команды
            switch (userConsoleText[0].ToLower())                  //свитч по нулевому элементу массива, приведенному к нижнему регистру
            {
                case "get":                                        //если введен "get"
                    if (userConsoleText.Length == 2)               //если введено 2 слова
                    {
                        PasswordService.GetPassword(userConsoleText[1]);           //вызывается метод получения пароля, в который передается первый элемент массива, т.е имя
                    }
                    else                                           //если не 2 слова
                    {
                        Console.WriteLine(falseInstruction);       //В консоль выводится, что формат некорректный
                    }
                    break;
                case "set":                                         //если введен "set"
                    if (userConsoleText.Length == 3)                //если введено 3 слова
                    {
                        NamePasswordDto userNamePasswordDto = new NamePasswordDto(userConsoleText[1], userConsoleText[2]);   //создается класс, который принимает второе введенное слово и третье, как имя + пароль
                        PasswordService.SetPassword(userNamePasswordDto);              //Вызывается метод создания пароля для имени (передаем объект класса)
                    }
                    else                                            //если не 3 слова
                    {
                        Console.WriteLine(falseInstruction);        //В консоль выводится, что формат некорректный
                    }
                    break;
                case "/exit":                                       //если введен "/exit"
                    if (userConsoleText.Length == 1)                //если введено 1 слово
                    {
                        Environment.Exit(0);                        //закрытие приложения
                    }
                    else                                            //если не 1 слова
                    {
                        Console.WriteLine(falseInstruction);        //В консоль выводится, что формат некорректный
                    }
                    break;
                default:                                            //для всех остальных случаев
                    Console.WriteLine(falseInstruction);            //В консоль выводится, что формат некорректный
                    break;
            }
            TakeInstruction();                                    //Вызывается метод (снова можно команду вводить)
        }
     
    }
}

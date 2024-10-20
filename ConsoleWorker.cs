using Newtonsoft.Json;          //библиотека для работы с json

namespace PasswordManager       //объявление области действия 
{
    internal class ConsoleWorker    //определение класса, компоненты которого доступны из любого места кода в этой же сборке, однако класс недоступен для других программ и сборок
    {
        static string pathFile = "Passwords.json"; //объявление переменной, которая хранит название файла
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
                        GetPassword(userConsoleText[1]);           //вызывается метод получения пароля, в который передается первый элемент массива, т.е имя
                    }
                    else                                           //если не 2 слова
                    {
                        Console.WriteLine(falseInstruction);       //В консоль выводится, что формат некорректный
                    }
                    break;
                case "set":                                         //если введен "set"
                    if (userConsoleText.Length == 3)                //если введено 3 слова
                    {
                        NamePassword userNamePassword = new NamePassword(userConsoleText[1], userConsoleText[2]);   //создается класс, который принимает второе введенное слово и третье, как имя + пароль
                        SetPassword(userNamePassword);              //Вызывается метод создания пароля для имени (передаем объект класса)
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
        public static void GetPassword(string name)                //метод для получение пароля, принимающий имя            
        {
            List<NamePassword> namesPasswordsList = ReadAllFromFile(); //объявление листа с классами, в котором хранится информация из файла, возвращаемая из метода
            Cryptographer cryptographer = new Cryptographer();         //создание экземпляра класса криптографера
            NamePassword namePasswordSearch = namesPasswordsList.FirstOrDefault(x => x.Name == name); //объявление объекта, в котором будет хранится объект из листа, попадающего под условие
            string decryptPassword = cryptographer.Decrypt(namePasswordSearch.Password);              //объявление переменной, в котором будет хранится рашсифрованный пароль
            Console.WriteLine($"Пароль в Вашем буфере обмена");        //вывод в консоль
            TextCopy.ClipboardService.SetText(decryptPassword);        //в буфер обмена закидывается расшифрованный пароль
            TakeInstruction();                                         //переход в метод, для продолжения работы с командами
        }
        public static void SetPassword(NamePassword namePassword)   //создание имени + пароля, получает объект класса
        {
            Cryptographer cryptographer = new Cryptographer();      //создание экземпляра класса криптографера
            NamePassword cryptoNamePassword = new NamePassword(namePassword.Name, cryptographer.Encrypt(namePassword.Password)); //создание объекта, который будет хранить имя + зашифрованный пароль
            List<NamePassword> namesPasswordsList = ReadAllFromFile();  //объявление листа с классами, в котором хранится информация из файла, возвращаемая из метода
            namesPasswordsList.Add(cryptoNamePassword);                 //добавление в лист нового объекта
            string serializedNamesPasswords = JsonConvert.SerializeObject(namesPasswordsList, Formatting.Indented); //Сериализация. Formatting.Indented - для переноса строка в файле.
            File.WriteAllText(pathFile, serializedNamesPasswords);      //Запись в файл  сериализационного листа
        }
        static List<NamePassword> ReadAllFromFile()                    //метод, который возвращает лист из объектов класса
        {
            if (!File.Exists(pathFile))                                //проверка, если файла нет
            {
                var file = File.Create(pathFile);                      //создать файл
                file.Close();                                          //закрыть файл
            }
            string json = File.ReadAllText(pathFile);                  //объявление переменной, в которой хранится весь текст из файла
            List<NamePassword> namesPasswordsList = JsonConvert.DeserializeObject<List<NamePassword>>(json); //десериализация
            return namesPasswordsList ?? new List<NamePassword>(); //возвращаем лист, если нечего возвращать, то возвращаем новый пустой лист
        }
    }
}
public class NamePassword                               //объявление класса                    
{
    public NamePassword(string name, string password)   //создание конструктора класса
    {
        Name = name;
        Password = password;
    }
    public string Name { get; set; }
    public string Password { get; set; }
}

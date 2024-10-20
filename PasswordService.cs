using Newtonsoft.Json;

namespace PasswordManager
{
    internal class PasswordService
    {
        static string pathFile = "Passwords.json"; //объявление переменной, которая хранит название файла
        public static void GetPassword(string name)                //метод для получение пароля, принимающий имя            
        {
            List<NamePasswordDto> namesPasswordsList = ReadAllFromFile(); //объявление листа с классами, в котором хранится информация из файла, возвращаемая из метода
            Cryptographer cryptographer = new Cryptographer();         //создание экземпляра класса криптографера
            NamePasswordDto namePasswordSearch = namesPasswordsList.FirstOrDefault(x => x.Name == name); //объявление объекта, в котором будет хранится объект из листа, попадающего под условие
            string decryptPassword = cryptographer.Decrypt(namePasswordSearch.Password);              //объявление переменной, в котором будет хранится рашсифрованный пароль
            Console.WriteLine($"Пароль в Вашем буфере обмена");        //вывод в консоль
            TextCopy.ClipboardService.SetText(decryptPassword);        //в буфер обмена закидывается расшифрованный пароль
        }
        public static void SetPassword(NamePasswordDto namePassword)   //создание имени + пароля, получает объект класса
        {
            Cryptographer cryptographer = new Cryptographer();      //создание экземпляра класса криптографера
            NamePasswordDto cryptoNamePasswordDto = new NamePasswordDto(namePassword.Name, cryptographer.Encrypt(namePassword.Password)); //создание объекта, который будет хранить имя + зашифрованный пароль
            List<NamePasswordDto> namesPasswordsList = ReadAllFromFile();  //объявление листа с классами, в котором хранится информация из файла, возвращаемая из метода
            namesPasswordsList.Add(cryptoNamePasswordDto);                 //добавление в лист нового объекта
            string serializedNamesPasswords = JsonConvert.SerializeObject(namesPasswordsList, Formatting.Indented); //Сериализация. Formatting.Indented - для переноса строка в файле.
            File.WriteAllText(pathFile, serializedNamesPasswords);      //Запись в файл  сериализационного листа
        }
        static List<NamePasswordDto> ReadAllFromFile()                    //метод, который возвращает лист из объектов класса
        {
            if (!File.Exists(pathFile))                                //проверка, если файла нет
            {
                var file = File.Create(pathFile);                      //создать файл
                file.Close();                                          //закрыть файл
            }
            string json = File.ReadAllText(pathFile);                  //объявление переменной, в которой хранится весь текст из файла
            List<NamePasswordDto> namesPasswordsList = JsonConvert.DeserializeObject<List<NamePasswordDto>>(json); //десериализация
            return namesPasswordsList ?? new List<NamePasswordDto>(); //возвращаем лист, если нечего возвращать, то возвращаем новый пустой лист
        }
    }
}

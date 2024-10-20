using System.Security.Cryptography; //подключение пространства имен, которое отвечает за службу шифрования
using System.Text;                  //подключение пространства имен, которое будет преобразовывать знаки в байты

namespace PasswordManager           //объявление области действия 
{
    internal class CryptoKeysCreator    //определение класса, компоненты которого доступны из любого места кода в этой же сборке, однако класс недоступен для других программ и сборок
    {
        const string pubKeyFileName = "id_RSA.pub";     //объявление константы, которая хранит название для файла с публичным ключом
        const string privateKeyFileName = "id_RSA";     //объявление константы, которая хранит название для файла с приватным ключом
        const string keysPath = ".ssh/";                //объявление константы, которая хранит название папки с приватным и публичным ключом
        public static void CreateKeys()                 //метод по созданию файлов и ключей в них
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();  //создание нового экземпляра класса RSACryptoServiceProvider для генерации приватного и публичного ключей
            var publicKey = rsa.ToXmlString(false);                         //объявление переменной, которая хранит публичный ключ. false - публичный                            
            var privateKey = rsa.ToXmlString(true);                         //объявление переменной, которая хранит приватный ключ. true - приватный
            if (!Directory.Exists(keysPath))                                //проверка, есть путь .ssh/
            {
                Directory.CreateDirectory(keysPath);                        //создание папки, чтобы путь .ssh/ был актуальным
            }
            File.WriteAllText($"{keysPath}{pubKeyFileName}", publicKey, Encoding.Default);          //Создание файла с публичным ключем
            File.WriteAllText($"{keysPath}{privateKeyFileName}", privateKey, Encoding.Default);     //Создание файла с приватным ключем
        }
    }
}

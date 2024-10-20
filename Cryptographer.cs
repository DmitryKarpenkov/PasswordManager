using System.Security.Cryptography;     //подключение пространства имен, которое отвечает за службу шифрования
using System.Text;                      //подключение пространства имен, которое будет преобразовывать знаки в байты

namespace PasswordManager               //объявление области действия      
{
    internal class Cryptographer        //определение класса, компоненты которого доступны из любого места кода в этой же сборке, однако класс недоступен для других программ и сборок
    {
        const string pubKeyFileName = "id_RSA.pub";                         //объявление константы, которая хранит название для файла с публичным ключом
        const string privateKeyFileName = "id_RSA";                         //объявление константы, которая хранит название для файла с приватным ключом
        const string keysPath = ".ssh/";                                    //объявление константы, которая хранит название папки с приватным и публичным ключом
        public string Decrypt(string base64Text)                                    //метод с расшифровкой пароля
        {
            var privateKey = File.ReadAllText($"{keysPath}{privateKeyFileName}");   //объявление переменной, в которую помещается приватный ключ из файла
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();          //создание нового экземпляра класса RSA
            rsa.FromXmlString(privateKey);                                          //инициализация RSA, используя приватный ключ
            var dataToDecrypt = Convert.FromBase64String(base64Text);               //объявление переменной, которая хранит закодированный в байтах передаваемый пароль 
            var decryptedData = rsa.Decrypt(dataToDecrypt, false);                  //объявление переменной c расщифровкой переданного пароля
            return Encoding.UTF8.GetString(decryptedData);                          //Возвращение декодируемого массива байтов в строку
        }
        public string Encrypt(string text)                                          //метод с шифрованием пароля
        {
            var publicKey = File.ReadAllText($"{keysPath}{pubKeyFileName}");        //объявление переменной, в которую помещается публичный ключ из файла
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();          //создание нового экземпляра RSA
            rsa.FromXmlString(publicKey);                                           //инициализация RSA, используя приватный ключ  
            var dataToEncrypt = Encoding.UTF8.GetBytes(text);                       //объявление переменной, которая хранит декодируемый массив байтов в строке
            var encrypted = rsa.Encrypt(dataToEncrypt, false);                      //объявление переменной c шифрованием переданного пароля
            return Convert.ToBase64String(encrypted);                               //Возвращает закодированный в байтах передаваемый пароль 
        }
    }
}

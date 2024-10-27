namespace PasswordManager       //объявление области действия 
{
    internal class ConsoleWorker    //определение класса, компоненты которого доступны из любого места кода в этой же сборке, однако класс недоступен для других программ и сборок
    {
        public static void TakeInstruction() 
        {
            Console.WriteLine("Доступные команды:\n" +
                " - get {name} - получение пароля по имени\n" +
                " - set {name} {password} - создание пароля для имени. Важно! Не использовать пробел в имени и пароле\n" +
                " - reset - для сброса всех паролей и создания нового\n" +
                " - /exit - Выход из программы\n" +
                "Введите Вашу команду:");
            string[] userConsoleText = Console.ReadLine().Split(); //объявление массива, который хранит слова из вводимого пользователем
            CommandFactory commandFactory = new CommandFactory();
            ICommand command = commandFactory.GetCommand(userConsoleText[0]);
            command.Execute(userConsoleText);
            TakeInstruction();                                    //Вызывается метод (снова можно команду вводить)
        }
    }
}

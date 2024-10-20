public class NamePasswordDto                              //объявление класса                    
{
    public NamePasswordDto(string name, string password)   //создание конструктора класса
    {
        Name = name;
        Password = password;
    }
    public string Name { get; set; }
    public string Password { get; set; }
}

namespace PasswordManager
{
    internal interface ICommand
    {
        public void Execute(string[] args);
    }
}

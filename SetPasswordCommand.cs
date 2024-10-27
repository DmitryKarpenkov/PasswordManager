namespace PasswordManager
{
    internal class SetPasswordCommand : ICommand
    {
        public void Execute(string[] args)
        {
            if (args.Length == 3)
            {
                NamePasswordDto userNamePasswordDto = new NamePasswordDto(args[1], args[2]);
                PasswordService.SetPassword(userNamePasswordDto);
            }
            else
            {
                Console.WriteLine("Incorrect Format");
            }
        }
    }
}

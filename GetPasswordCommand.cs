namespace PasswordManager
{
    internal class GetPasswordCommand : ICommand
    {
        public void Execute(string[] args)
        {
            if (args.Length == 2)
            {
                PasswordService.GetPassword(args[1]);
            }
            else
            {
                Console.WriteLine("Incorrect Format");
            }
        }
    }
}

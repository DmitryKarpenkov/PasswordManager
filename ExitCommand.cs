namespace PasswordManager
{
    internal class ExitCommand : ICommand
    {
        public void Execute(string[] args)
        {
            if (args.Length == 1)
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Incorrect Format");
            }
        }
    }
}

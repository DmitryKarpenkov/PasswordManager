namespace PasswordManager
{
    internal class GetPasswordCommand : ICommand
    {
        public async void Execute(string[] args)
        {
            if (args.Length == 2)
            {
                Task getPasswordTask = PasswordService.GetPasswordAsync(args[1]);
                await getPasswordTask;
            }
            else
            {
                Console.WriteLine("Incorrect Format");
            }
        }
    }
}

namespace PasswordManager
{
    internal class ResetPasswordsFileCommand : ICommand
    {
        public async void Execute(string[] args)
        {
            string[] filesPaths = {"Password.json", "Passwords.json", ".ssh/id_RSA", ".ssh/id_RSA.pub" };
            if (args.Length == 1)
            {
                for (int i = 0; i<filesPaths.Length; i++)
                {
                    File.Delete(filesPaths[i]);
                }
                await Program.CheckCreatedPasswordAsync();
            }
            else
            {
                Console.WriteLine("Incorrect Format");
            }
        }
    }
}

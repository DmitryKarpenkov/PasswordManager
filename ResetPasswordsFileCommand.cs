namespace PasswordManager
{
    internal class ResetPasswordsFileCommand : ICommand
    {
        public void Execute(string[] args)
        {
            string[] filesPaths = {"Password.json", "Passwords.json", ".ssh/id_RSA", ".ssh/id_RSA.pub" };
            if (args.Length == 1)
            {
                for (int i = 0; i<filesPaths.Length; i++)
                {
                    File.Delete(filesPaths[i]);
                }
                Program.CheckCreatedPassword();
            }
            else
            {
                Console.WriteLine("Incorrect Format");
            }
        }
    }
}

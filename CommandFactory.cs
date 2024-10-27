namespace PasswordManager
{
    internal class CommandFactory
    {
        public ICommand GetCommand(string commandName)
        {
            switch (commandName.ToLower())
            {
                case "get":
                    return new GetPasswordCommand();

                case "set":
                    return new SetPasswordCommand();

                case "exit":
                    return new ExitCommand();

                case "reset":
                    return new ResetPasswordsFileCommand();

                default:
                    throw new NotImplementedException("Incorrect format");
            }

        }
    }
}



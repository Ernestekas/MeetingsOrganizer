namespace MeetingsOrganizer.App
{
    public class OrganizerConsole
    {
        private LoginForm loginForm = new LoginForm();

        public void Run()
        {
            bool runApp = true;
            Console.WriteLine("Welcome.");

            while (runApp)
            {
                Console.Clear();
                Console.WriteLine("Type 'login' to login or 'exit' to close an application:");
                string action = Console.ReadLine();
                
                switch (action.ToLower())
                {
                    case "exit":
                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();
                        runApp = false;
                        break;
                    case "login":
                        loginForm.Run();
                        break;
                    default:
                        Console.WriteLine("Invalid command.");
                        Console.Clear();
                        break;
                }
            }
        }
    }
}

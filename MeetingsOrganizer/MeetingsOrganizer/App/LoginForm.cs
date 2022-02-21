using MeetingsOrganizer.Models;
using MeetingsOrganizer.Services;

namespace MeetingsOrganizer.App
{
    public class LoginForm
    {
        private PersonService personService = new PersonService();
        private MainMenuForm mainMenuForm = new MainMenuForm();

        public void Run()
        {
            bool runLogin = true;
            while (runLogin)
            {
                Console.Clear();
                Console.WriteLine("Please login.");
                Console.WriteLine("Name:");
                string name = Console.ReadLine();
                Console.WriteLine("Surname:");
                string surname = Console.ReadLine();

                switch (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
                {
                    case true:
                        Console.WriteLine("Name and surname are required.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case false:
                        Person person = personService.CheckPerson(name, surname);

                        mainMenuForm.Run(person);

                        runLogin = false;
                        break;
                }
            }
        }
    }
}

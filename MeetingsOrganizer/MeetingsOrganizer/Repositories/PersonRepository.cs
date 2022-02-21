using MeetingsOrganizer.Models;
using Newtonsoft.Json;

namespace MeetingsOrganizer.Repositories
{
    public class PersonRepository
    {
        private readonly string peopleData = "./personData.json";

        public PersonRepository()
        {
            if (!File.Exists(peopleData))
            {
                File.WriteAllText(peopleData, "[]");
            }
        }

        public void AddNew(Person person)
        {
            List<Person> people = GetAll();

            if(people.Count == 0)
            {
                person.Id = 1;
            }
            else
            {
                person.Id = people.Select(p => p.Id).Max() + 1;
            }
            
            people.Add(person);

            Update(people);
        }

        public void Remove(Person person)
        {
            List<Person> people = GetAll();
            people = people.Where(p => p.Id != person.Id).ToList();

            Update(people);
        }

        public List<Person> GetAll()
        {
            return JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(peopleData));
        }

        public void Update(List<Person> updatedPeople)
        {
            string json = JsonConvert.SerializeObject(updatedPeople);
            File.WriteAllText(peopleData, json);
        }
    }
}

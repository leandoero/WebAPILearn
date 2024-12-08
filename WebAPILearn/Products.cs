namespace Products;

public record Person    //https://metanit.com/sharp/tutorial/3.51.php
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
public class Persons
{
    List<Person> people = new List<Person>()          //https://metanit.com/sharp/tutorial/4.5.php
    {
    new Person{Id = 0, Name = "first", Description = "desk" },
    new Person{ Id = 1, Name = "second", Description = "desk"},
    new Person{ Id = 2, Name = "third", Description = "desk"}
    };

    public List<Person> GetPersons()
    {
        return people;
    }

    public List<Person> GetPerson(int ID)
    {
        //about SingleOrDefault https://learn.microsoft.com/ru-ru/dotnet/api/system.linq.enumerable.singleordefault?view=net-8.0

        var persons = people.Where(p => p.Id == ID).ToList();

        if (persons.Any())
        {
            return persons;
        }
        else
        {
            throw new Exception("Error..");
        }
    }
    public void CreatePerson(Person person)
    {
        people.Add(person);
    }
    public void UpdatePerson(int ID, Person person)
    {
        int index = -1;
        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Id == ID)
            {
                index = i;
                people[index] = person;
                break;
            }
        }
        if (index < 0)
        {
            throw new Exception("wrond id");
        }
    }
    public void DeletePerson(int ID)
    {
        people = people.FindAll(p => p.Id != ID).ToList();      //FindAll создает новый список, включающий только те элементы, для которых предикат вернул true.
        //Метод FindAll возвращает результат в виде коллекции типа List<T>, но так как результат может быть отфильтрован в коллекцию типа IEnumerable<T>, мы вызываем ToList(),
    }
}

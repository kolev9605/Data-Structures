using System;
using System.Collections.Generic;
using System.Linq;

public class PersonCollectionSlow : IPersonCollection
{
    private List<Person> persons = new List<Person>(); 

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (this.FindPerson(email) != null)
        {
            return false;
        }

        var person = new Person()
        {
            Age = age,
            Email = email,
            Name = name,
            Town = town,
        };

        this.persons.Add(person);
        return true;
    }

    public int Count => this.persons.Count;

    public Person FindPerson(string email)
    {
        return this.persons.FirstOrDefault(p => p.Email == email);
    }

    public bool DeletePerson(string email)
    {
        var person = this.FindPerson(email);
        return this.persons.Remove(person);
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        return this.persons
            .Where(x => x.Email.EndsWith("@" + emailDomain))
            .OrderBy(x => x.Email);

    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        return this.persons
            .Where(x => x.Name == name && x.Town == town)
            .OrderBy(x => x.Email);
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        return this.persons
            .Where(x => x.Age >= startAge && x.Age <= endAge)
            .OrderBy(x => x.Age)
            .ThenBy(x => x.Email);
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        return this.persons
            .Where(x => x.Town == town)
            .Where(x => x.Age >= startAge && x.Age <= endAge)
            .OrderBy(x => x.Age)
            .ThenBy(x => x.Email);
    }
}

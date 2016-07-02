using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    private Dictionary<string, Person> personsByEmail;
    private Dictionary<string, SortedSet<Person>> personsByDomain;
    private Dictionary<string, SortedSet<Person>> personsByTownAndName;
    private OrderedDictionary<int, SortedSet<Person>> personsByAge;
    private Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> personsByTownAndAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();

    public PersonCollection()
    {
        this.personsByEmail = new Dictionary<string, Person>();
        this.personsByDomain = new Dictionary<string, SortedSet<Person>>();
        this.personsByTownAndName = new Dictionary<string, SortedSet<Person>>();
        this.personsByAge = new OrderedDictionary<int, SortedSet<Person>>();
        this.personsByTownAndAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();
    }


    public bool AddPerson(string email, string name, int age, string town)
    {
        if (this.personsByEmail.ContainsKey(email))
        {
            return false;
        }

        var person = new Person()
        {
            Email = email,
            Name = name,
            Age = age,
            Town = town
        };

        //add by email
        this.personsByEmail.Add(email, person);

        //add by domain
        var domain = this.ExtractDomain(email);
        if (!this.personsByDomain.ContainsKey(domain))
        {
            this.personsByDomain[domain] = new SortedSet<Person>();
        }

        this.personsByDomain[domain].Add(person);

        //add by town+name
        var nameTown = this.CombineNameAndTown(name, town);
        if (!this.personsByTownAndName.ContainsKey(nameTown))
        {
            this.personsByTownAndName[nameTown] = new SortedSet<Person>();
        }

        this.personsByTownAndName[nameTown].Add(person);

        //add by age
        if (!this.personsByAge.ContainsKey(age))
        {
            this.personsByAge[age] = new SortedSet<Person>();
        }

        this.personsByAge[age].Add(person);

        //add by town and age
        if (!this.personsByTownAndAge.ContainsKey(town))
        {
            this.personsByTownAndAge[town] = new OrderedDictionary<int, SortedSet<Person>>();
        }

        if (!this.personsByTownAndAge[town].ContainsKey(age))
        {
            this.personsByTownAndAge[town][age] = new SortedSet<Person>();
        }

        this.personsByTownAndAge[town][age].Add(person);

        return true;
    }

    public int Count => this.personsByEmail.Count;

    public Person FindPerson(string email)
    {
        if (!this.personsByEmail.ContainsKey(email))
        {
            return null;
        }

        return this.personsByEmail[email];
    }

    public bool DeletePerson(string email)
    {
        var person = this.FindPerson(email);
        if (!this.personsByEmail.ContainsKey(email))
        {
            return false;
        }

        this.personsByEmail.Remove(email);

        var domain = this.ExtractDomain(email);
        this.personsByDomain[domain].Remove(person);

        var nameTown = this.CombineNameAndTown(person.Name, person.Town);
        this.personsByTownAndName[nameTown].Remove(person);

        this.personsByAge[person.Age].Remove(person);

        this.personsByTownAndAge[person.Town][person.Age].Remove(person);

        return true;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        if (this.personsByDomain.ContainsKey(emailDomain))
        {
            if (this.personsByDomain[emailDomain].Any())
            {
                return this.personsByDomain[emailDomain];
            }
        }

        return Enumerable.Empty<Person>();
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        var nameTown = this.CombineNameAndTown(name, town);

        if (this.personsByTownAndName.ContainsKey(nameTown))
        {
            if (this.personsByTownAndName[nameTown].Any())
            {
                return this.personsByTownAndName[nameTown];
            }
        }

        return Enumerable.Empty<Person>();
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        return this.personsByAge.Range(startAge, true, endAge, true).SelectMany(x => x.Value);
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
    {
        if (!this.personsByTownAndAge.ContainsKey(town))
        {
            yield break;
        }

        var personsInRange = this.personsByTownAndAge[town]
            .Range(startAge, true, endAge, true);

        foreach (var entry in personsInRange.SelectMany(person => person.Value))
        {
            yield return entry;
        }
    }

    private string ExtractDomain(string email)
    {
        var domain = email.Split('@')[1];
        return domain;
    }

    private string CombineNameAndTown(string name, string town)
    {
        return name + "|!|" + town;
    }
}

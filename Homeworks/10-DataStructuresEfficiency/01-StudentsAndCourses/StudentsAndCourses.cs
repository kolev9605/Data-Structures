namespace _01_StudentsAndCourses
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class StudentsAndCourses
    {
        static void Main(string[] args)
        {
            SortedDictionary<string, SortedSet<Student>> studentsAndCourses = new SortedDictionary<string, SortedSet<Student>>();

            while (true)
            {
                string command = Console.ReadLine();
                if (string.IsNullOrEmpty(command))
                {
                    break;
                }

                var tokens = command.Split('|');
                var firstName = tokens[0].Trim();
                var lastName = tokens[1].Trim();
                var courseName = tokens[2].Trim();

                if (!studentsAndCourses.ContainsKey(courseName))
                {
                    studentsAndCourses[courseName] = new SortedSet<Student>();
                }

                Student student = new Student(firstName,lastName);
                studentsAndCourses[courseName].Add(student);
            }

            foreach (var pair in studentsAndCourses)
            {
                Console.WriteLine("{0}: {1}",
                    pair.Key,
                    string.Join(", ", pair.Value));
            }
        }
    }

    class Student : IComparable<Student>
    {
        public Student(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CompareTo(Student other)
        {
            if (this.LastName.CompareTo(other.LastName) == 0)
            {
                return this.FirstName.CompareTo(other.FirstName);
            }

            return this.LastName.CompareTo(other.LastName);
        }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }
    }
}

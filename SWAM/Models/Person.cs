using System.Collections.Generic;

namespace SWAM.Models
{
    /// <summary>
    /// The basic model of the class in the database representing the person.
    /// The class that all actors of the appliaction should inherit from.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// The id number of the person in the database.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// First name of the person.
        /// </summary>
        public string Name { get; set; }
    }
}

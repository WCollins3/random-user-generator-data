using System.Collections.Generic;
using System.Linq;

namespace RandomUserGeneratorData.Core.Models
{
    /// <summary>
    ///     Class built to hold data about randomly-generated users.
    /// </summary>
    public class RandomUserGeneratorDataHolder
    {
        /// <summary>
        ///     Dictionary linking a gender to the amount of <see cref="User"/>s that identify as that gender.
        /// </summary>
        public Dictionary<string, int> UserCountsByGender { get; set; }

        /// <summary>
        ///     Dictionary linking a country to the amount of people <see cref="User"/>s who live there.s
        /// </summary>
        public Dictionary<string, int> UserCountsByCountry { get; set; }

        /// <summary>
        ///     Dictionary linking a letter to the number of <see cref="User"/>s who's first name starts with that letter.
        /// </summary>
        public Dictionary<string, int> UserCountsByFirstNameFirstLetter { get; set; }

        /// <summary>
        ///     Dictionary linking a letter to the number of <see cref="User"/>s who's last name starts with that letter.
        /// </summary>
        public Dictionary<string, int> UserCountsByLastNameFirstLetter { get; set; }

        /// <summary>
        ///     Takes a collection of <see cref="User"/>s to create an instance of the <see cref="RandomUserGeneratorDataHolder"/> class.
        /// </summary>
        /// <param name="users">Collection of <see cref="User"/>s.</param>
        public RandomUserGeneratorDataHolder(IEnumerable<User> users)
        {
            UserCountsByGender = calculateUserCountsByTrait(users.Select(u => u.Gender));
            UserCountsByCountry = calculateUserCountsByTrait(users.Select(u => u.Country));
            UserCountsByFirstNameFirstLetter = calculateUserCountsByTrait(users.Select(u => u.FirstName.First().ToString()));
            UserCountsByLastNameFirstLetter = calculateUserCountsByTrait(users.Select(u => u.LastName.First().ToString()));
        }

        /// <summary>
        ///     Calculate the amount <see cref="User"/>s who have certain traits.
        /// </summary>
        /// <param name="values">Values of the traits of the <see cref="User"/>s. For example, user genders and names.</param>
        /// <returns>Dictionary where the key is the value of a trait and the value is the number of users with that trait.</returns>
        private Dictionary<string, int> calculateUserCountsByTrait(IEnumerable<string> values)
        {
            return values.GroupBy(u => u)
                .ToDictionary(g => g.Key, g => g.Count());
        }

    }
}

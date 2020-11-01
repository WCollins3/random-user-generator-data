using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomUserGeneratorData.Core.Models
{
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

        public RandomUserGeneratorDataHolder(IEnumerable<User> users)
        {
            UserCountsByGender = calculateUserCountsByTrait(users.Select(u => u.Gender));
            UserCountsByCountry = calculateUserCountsByTrait(users.Select(u => u.Country));
            UserCountsByFirstNameFirstLetter = calculateUserCountsByTrait(users.Select(u => u.FirstName.First().ToString()));
            UserCountsByLastNameFirstLetter = calculateUserCountsByTrait(users.Select(u => u.LastName.First().ToString()));
        }

        private Dictionary<string, int> calculateUserCountsByTrait(IEnumerable<string> values)
        {
            return values.GroupBy(u => u)
                .ToDictionary(g => g.Key, g => g.Count());
        }

    }
}

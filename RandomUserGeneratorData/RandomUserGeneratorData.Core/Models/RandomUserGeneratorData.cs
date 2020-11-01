using System;
using System.Collections.Generic;
using System.Text;

namespace RandomUserGeneratorData.Core.Models
{
    public class RandomUserGeneratorData
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
        public Dictionary<char, int> UsersByFirstNameFirstLetter { get; set; }

        /// <summary>
        ///     Dictionary linking a letter to the number of <see cref="User"/>s who's last name starts with that letter.
        /// </summary>
        public Dictionary<char, int> UsersByLirstNameFirstLetter { get; set; }
    }
}

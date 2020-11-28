namespace RandomUserGeneratorData.Core.Models
{
    /// <summary>
    ///     A randomly-generated user.
    /// </summary>
    public class User
    {
        /// <summary>
        ///     Gender of the <see cref="User"/>.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        ///     First name of the <see cref="User"/>.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Last name of the <see cref="User"/>.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     The country that the <see cref="User"/> lives in.
        /// </summary>
        public string Country { get; set; }
    }
}

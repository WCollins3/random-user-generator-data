using Newtonsoft.Json.Linq;
using RandomUserGeneratorData.Core.DataRetrieval;
using RandomUserGeneratorData.Core.Exceptions;
using RandomUserGeneratorData.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RandomUserGeneratorData.DataRetrieval
{
    /// <summary>
    ///     A class to retrieve random users from the random-user API.
    /// </summary>
    public class RandomUserRetriever : IRandomUserRetriever
    {
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        ///     Creates an instance of the <see cref="RandomUserRetriever"/> class.
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public RandomUserRetriever(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <inheritdoc/>
        public async Task<IList<User>> GetUsersAsync(int numUsers)
        {
            if (numUsers < 1)
            {
                throw new InvalidOperationException($"numUsers must be at least 1. Value of numUsers: {numUsers}.");
            }

            var client = _httpClientFactory.CreateClient("RandomUser");
            string text;

            try
            {
                text = await client.GetStringAsync($"/api/?results={numUsers}");
            }
            catch(Exception ex)
            {
                var message = $"Unexpected error from Random User API. Error message: {ex.Message}";
                throw new UnexpectedApiException(message);
            }

            var responseDetails = JObject.Parse(text);
            var results = responseDetails["results"];
            var users = new List<User>();
            foreach (var result in results)
            {
                var gender = result["gender"].ToString();
                var firstName = result["name"]["first"].ToString();
                var lastName = result["name"]["last"].ToString();
                var country = result["location"]["country"].ToString();

                var user = new User
                {
                    Gender = gender,
                    FirstName = firstName,
                    LastName = lastName,
                    Country = country,
                };

                users.Add(user);
            }

            return users;
        }
    }
}

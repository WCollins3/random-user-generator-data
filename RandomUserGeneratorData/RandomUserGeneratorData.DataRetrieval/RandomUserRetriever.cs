using Newtonsoft.Json.Linq;
using RandomUserGeneratorData.Core.DataRetrieval;
using RandomUserGeneratorData.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RandomUserGeneratorData.DataRetrieval
{
    public class RandomUserRetriever : IRandomUserRetriever
    {
        private readonly IHttpClientFactory _httpClientFactory;

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
            string text = await client.GetStringAsync($"/api/?results={numUsers}");

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

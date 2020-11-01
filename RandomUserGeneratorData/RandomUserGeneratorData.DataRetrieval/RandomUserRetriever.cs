using Newtonsoft.Json.Linq;
using RandomUserGeneratorData.Core.DataRetrieval;
using RandomUserGeneratorData.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RandomUserGeneratorData.DataRetrieval
{
    public class RandomUserRetriever : IRandomUserRetriever
    {
        /// <inheritdoc/>
        public async Task<IEnumerable<User>> GetUsersAsync(int numUsers)
        {
            var request = WebRequest.Create($"https://randomuser.me/api/?results={numUsers}");
            string text;

            using (var response = await request.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var textReader = new StreamReader(stream))
                    {
                        text = textReader.ReadToEnd();
                    }
                }
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

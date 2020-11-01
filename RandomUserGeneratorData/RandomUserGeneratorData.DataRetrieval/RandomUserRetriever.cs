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

            Console.WriteLine(text);

            // Return dummy list of users
            IEnumerable<User> users = new List<User>();
            return users;
        }
    }
}

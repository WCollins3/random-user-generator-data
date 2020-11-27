using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Contrib.HttpClient;
using RandomUserGeneratorData.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RandomUserGeneratorData.DataRetrieval.Tests
{
    [TestClass]
    public class RandomUserRetrieverTests
    {
        private static string baseAddress = "https://NotRealAddress.com";
        private static string requestUrl = $"{baseAddress}/api/?results=";
        private static string clientName = "RandomUser";
        private static string jonSnowUserString = "{\"gender\":\"male\",\"name\":{\"first\":\"Jon\",\"last\":\"Snow\"},\"location\":{\"country\":\"House Stark\"}}";
        private static string aryaStartUserString = "{\"gender\":\"female\",\"name\":{\"first\":\"Arya\",\"last\":\"Stark\"},\"location\":{\"country\":\"House Stark\"}}";

        [TestMethod]
        public async Task GetUsersAsync_SingleUser_Valid()
        {
            int numUsers = 1;

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var httpClientFactory = handler.CreateClientFactory();
            var httpClient = handler.CreateClient();

            string apiReturnString = $"{{\"results\":[{jonSnowUserString}]}}";

            handler.SetupRequest(HttpMethod.Get, $"{requestUrl}{numUsers}")
                .ReturnsResponse(apiReturnString);

            Mock.Get(httpClientFactory).Setup(x => x.CreateClient(clientName))
                .Returns(() =>
                {
                    var client = handler.CreateClient();
                    client.BaseAddress = new Uri(baseAddress);
                    return client;

                });

            var randomUserRetriever = new RandomUserRetriever(httpClientFactory);

            var result = await randomUserRetriever.GetUsersAsync(numUsers);

            Assert.AreEqual(1, result.Count);

            var user = result[0];
            Assert.AreEqual("male", user.Gender);
            Assert.AreEqual("Jon", user.FirstName);
            Assert.AreEqual("Snow", user.LastName);
            Assert.AreEqual("House Stark", user.Country);

            handler.VerifyAll();
        }

        [TestMethod]
        public async Task GetUsersAsync_TwoUsers_Valid()
        {
            int numUsers = 2;

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var httpClientFactory = handler.CreateClientFactory();
            var httpClient = handler.CreateClient();

            string apiReturnString = $"{{\"results\":[{jonSnowUserString},{aryaStartUserString}]}}";

            handler.SetupRequest(HttpMethod.Get, $"{requestUrl}{numUsers}")
                .ReturnsResponse(apiReturnString);

            Mock.Get(httpClientFactory).Setup(x => x.CreateClient(clientName))
                .Returns(() =>
                {
                    var client = handler.CreateClient();
                    client.BaseAddress = new Uri(baseAddress);
                    return client;

                });

            var randomUserRetriever = new RandomUserRetriever(httpClientFactory);

            var result = await randomUserRetriever.GetUsersAsync(numUsers);

            Assert.AreEqual(2, result.Count);

            var user1 = result[0];
            Assert.AreEqual("male", user1.Gender);
            Assert.AreEqual("Jon", user1.FirstName);
            Assert.AreEqual("Snow", user1.LastName);
            Assert.AreEqual("House Stark", user1.Country);

            var user2 = result[1];
            Assert.AreEqual("female", user2.Gender);
            Assert.AreEqual("Arya", user2.FirstName);
            Assert.AreEqual("Stark", user2.LastName);
            Assert.AreEqual("House Stark", user2.Country);

            handler.VerifyAll();
        }

        [TestMethod]
        public async Task GetUsersAsync_Zero_Invalid()
        {
            int numUsers = 0;

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var httpClientFactory = handler.CreateClientFactory();

            var randomUserRetriever = new RandomUserRetriever(httpClientFactory);

            var ex = await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => randomUserRetriever.GetUsersAsync(numUsers));
            Assert.AreEqual($"numUsers must be at least 1. Value of numUsers: {numUsers}.", ex.Message);

            handler.VerifyAll();
        }

        [TestMethod]
        public async Task GetUsersAsync_Negative_Invalid()
        {
            int numUsers = -1;

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var httpClientFactory = handler.CreateClientFactory();

            var randomUserRetriever = new RandomUserRetriever(httpClientFactory);

            var ex = await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => randomUserRetriever.GetUsersAsync(numUsers));
            Assert.AreEqual($"numUsers must be at least 1. Value of numUsers: {numUsers}.", ex.Message);

            handler.VerifyAll();
        }

        [TestMethod]
        public async Task GetUsersAsync_UnexpectedApiException()
        {
            int numUsers = 1;

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var httpClientFactory = handler.CreateClientFactory();
            var httpClient = handler.CreateClient();

            // Actual error message from random user API
            string errorMessage = "Unexpected error message.";

            handler.SetupRequest(HttpMethod.Get, $"{requestUrl}{numUsers}")
                .Throws(new Exception(errorMessage));

            Mock.Get(httpClientFactory).Setup(x => x.CreateClient(clientName))
                .Returns(() =>
                {
                    var client = handler.CreateClient();
                    client.BaseAddress = new Uri(baseAddress);
                    return client;

                });

            var randomUserRetriever = new RandomUserRetriever(httpClientFactory);

            var ex = await Assert.ThrowsExceptionAsync<UnexpectedApiException>(() => randomUserRetriever.GetUsersAsync(numUsers));
            Assert.AreEqual($"Unexpected error from Random User API. Error message: {errorMessage}", ex.Message);

            handler.VerifyAll();
        }
    }
}

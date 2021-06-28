using AutoFixture.Xunit2;
using Chatter.BusinessLogic.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Chatter.Test.API
{
    public class MessageControllerTests : BaseIntegrationTest
    {
       
        public MessageControllerTests(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task GetMessages_ReturnsSuccess()
        {
            // arrange
            var client = _factory.CreateClient();
            // act
            var response = await client.GetAsync("Message/GetMessages").ConfigureAwait(false);
            // assert
            response.EnsureSuccessStatusCode();

        }
        [Theory, AutoData]
        [Trait("Category", "Integration")]
        public async Task SendMessage_AppliesDatabaseInsert(Message message)
        {
            // Arrange
            // override messageId as we don't want to insert that to the database.
            message.MessageId = default;
            var client = _factory.CreateClient();
            var messageBytes = GetByteContentForObject(message);
            // Act
            // block the call to ensure we don't get the message before the save is completed.
            var response = client.PostAsync("Message/SendMessage", messageBytes).Result;
            // Assert
            response.EnsureSuccessStatusCode();
            var id = await response.Content.ReadAsStringAsync();
            // retrieve message sent by id returned from post call
            var messagesResponse = await client.GetAsync($"Message/GetMessage?id={id}").ConfigureAwait(false);
            var returnedMessage = JsonConvert.DeserializeObject<Message>(
                await messagesResponse.Content.ReadAsStringAsync().ConfigureAwait(false));
            returnedMessage.Text.Should().Be(message.Text);

        }

     
    
    }
}

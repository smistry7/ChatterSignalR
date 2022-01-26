using AutoFixture.Xunit2;
using Chatter.APIClient;
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
            var chatterClient = new ApiClientFactory(client).BuildChatterApi();

            // act
            var response = await chatterClient.GetMessagesResponse();
            // assert
            await response.EnsureSuccessStatusCodeAsync();

        }
        [Theory, AutoData]
        [Trait("Category", "Integration")]
        public async Task SendMessage_AppliesDatabaseInsert(APIClient.Message message)
        {
            // Arrange
            // override messageId as we don't want to insert that to the database.
            message.MessageId = default;
            var client = _factory.CreateClient();
            var chatterClient = new ApiClientFactory(client).BuildChatterApi();

            // Act
            var response = await chatterClient.SendMessageResponse(message);

            // Assert
            await response.EnsureSuccessStatusCodeAsync();
            var id = response.Content;
            // retrieve message sent by id returned from post call
            var returnedMessage = await chatterClient.GetMessage(id);
            returnedMessage.Text.Should().Be(message.Text);

        }
    }
}

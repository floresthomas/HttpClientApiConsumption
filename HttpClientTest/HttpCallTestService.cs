using Moq.Protected;
using Moq;
using System.Net;
using HttpClientConsumeApi.Interface;
using HttpClientConsumeApi.Service;
using HttpClientConsumeApi.Models;
using System.Text.Json;
using HttpClientConsumeApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientTest
{
    public class HttpCallTestService
    {
        [Fact]
        public async Task GetDataFromApi_Success()
        {
            // Arrange
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var expectedData = new DataModel();
            var json = JsonSerializer.Serialize(expectedData);
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(json) };
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<System.Threading.CancellationToken>())
                .ReturnsAsync(responseMessage);

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var httpCallService = new HttpCallService(httpClientFactoryMock.Object);

            // Act
            var result = await httpCallService.GetDataFromApi<DataModel>();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetDataFromApi_Failure()
        {
            // Arrange
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<System.Threading.CancellationToken>())
                .ReturnsAsync(responseMessage);

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var httpCallService = new HttpCallService(httpClientFactoryMock.Object);

            // Act
            var result = await httpCallService.GetDataFromApi<DataModel>();

            // Assert
            Assert.Null(result);
        }
    }

    public class HttpCallControllerTests
    {
        [Fact]
        public async Task Get_Returns_OkResult()
        {
            // Arrange
            var httpCallServiceMock = new Mock<IHttpCallService>();
            httpCallServiceMock.Setup(x => x.GetDataFromApi<DataModel>()).ReturnsAsync(new DataModel());
            var controller = new HttpCallController(httpCallServiceMock.Object);

            // Act
            var result = await controller.Get() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_Returns_NotFoundResult()
        {
            // Arrange
            var httpCallServiceMock = new Mock<IHttpCallService>();
            httpCallServiceMock.Setup(x => x.GetDataFromApi<DataModel>()).ReturnsAsync((DataModel)null);
            var controller = new HttpCallController(httpCallServiceMock.Object);

            // Act
            var result = await controller.Get() as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
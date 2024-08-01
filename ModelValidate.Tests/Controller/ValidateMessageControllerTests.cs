using Microsoft.AspNetCore.Mvc;
using ModelValidate.API.Controllers;
using ModelValidate.BusinessLogic.Interface;
using ModelValidate.BusinessLogic.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelValidate.Tests.Controller
{
    public class ValidateMessageControllerTests
    {
        private readonly ValidateMessageController _validateMessageController;
        private readonly Mock<IValidateMessageService> _validateMessageServiceMock;

        public ValidateMessageControllerTests()
        {
            _validateMessageServiceMock = new Mock<IValidateMessageService>();
            _validateMessageController = new ValidateMessageController(_validateMessageServiceMock.Object);
        }

        /// <summary>
        /// This test case created for validated message
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DetectPhoneNumbersTest_Validate()
        {
            //Arrange
            var inputMessage = "Hello Alex, we can connect over professional network.";
            var resultMessage = inputMessage; // result should be same as input message

            Tuple<string, bool> expected = new Tuple<string, bool>(resultMessage, true);
            _validateMessageServiceMock.Setup(message => message.ValidateInputMessage(inputMessage, false))
                .ReturnsAsync(expected);

            //Act
            var result = await _validateMessageController.Post(inputMessage) as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expected.Item1, resultMessage); // Validated result message as input message
            Assert.Equal(expected.Item2, true); // true as Validated
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// This test case created for not validate message
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DetectPhoneNumbersTest_NotValidate()
        {
            //Arrange
            var inputMessage = "Hello Alex, please contact me on my phone +91 9876543210";
            var resultMessage = "Hello Alex, please contact me on my phone <b>+<b>91</b> <b>9876543210</b></b>"; // highlighted result message

            Tuple<string,bool> expected = new Tuple<string, bool>(resultMessage, false);
            _validateMessageServiceMock.Setup(message => message.ValidateInputMessage(inputMessage, false))
                .ReturnsAsync(expected);

            //Act
            var result = await _validateMessageController.Post(inputMessage) as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expected.Item1, resultMessage); // highlighted phone number string
            Assert.Equal(expected.Item2, false); // false as Not Validated
            Assert.IsType<OkObjectResult>(result);
        }
    }
}

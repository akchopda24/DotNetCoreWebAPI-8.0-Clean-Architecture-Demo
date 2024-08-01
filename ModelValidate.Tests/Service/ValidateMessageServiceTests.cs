using AutoMapper;
using ModelValidate.BusinessLogic.Interface;
using ModelValidate.BusinessLogic.Service;
using ModelValidate.DataAccess.Entities;
using ModelValidate.DataAccess.GenericRepository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelValidate.Tests.Service
{
    public class ValidateMessageServiceTests
    {
        /// <summary>
        /// Mock variable 
        /// </summary>
        private readonly ValidateMessageService? _validateMessageService;
        private readonly Mock<IValidateMessageService> _validateMessageServiceMock;
        private readonly Mock<IRepositoryBase<ValidatedMessage>> _validatedMessageRepositoryMock;
        private readonly Mock<IMapper> _mapper;

        /// <summary>
        /// Registered mock variable in constructor
        /// </summary>
        public ValidateMessageServiceTests()
        {
            _validateMessageServiceMock = new Mock<IValidateMessageService>();
            _validatedMessageRepositoryMock = new Mock<IRepositoryBase<ValidatedMessage>>();
            _mapper = new Mock<IMapper>();
            _validateMessageService = new ValidateMessageService(
                _validatedMessageRepositoryMock.Object,
                _mapper.Object
                );
        }


        /// <summary>
        /// Not Validate message test
        /// </summary>
        /// <returns>Return false with highlighted phone number message</returns>
        [Fact]
        public async Task ValidateInputMessage_Return_HighlightsPhoneNumbersWithFalseResult_ForNotValidInput()
        {
            // Arrange
            var inputMessage = "Hey Alix, contact me at +1-123-456-7890 or (123) 456-7890";
            var resultMessage = "Hey Alix, contact me at <b>+<b>1</b>-<b>1</b>23<b>-<b>456</b></b><b>-7890</b></b> or<b> (<b>1</b>23) <b>456</b><b>-7890</b></b>";
            bool onlyValidate = false;

            // Act
            var result = await _validateMessageService?.ValidateInputMessage(inputMessage, onlyValidate);

            // Assert
            Assert.Equal(result.Item1, resultMessage); // Highlighted phone number in result message
            Assert.Equal(result.Item2, false); // false as Not validated
        }

        /// <summary>
        /// Validate message test
        /// </summary>
        /// <returns>Return true with result message as same input message</returns>
        [Fact]
        public async Task ValidateInputMessage_Return_ValidatedMessageWithTrueResult_ForValidInput()
        {
            // Arrange
            var input = "This is my input message without phone numbers and it is validated as same input message. we can direct compare result with input message.";
            var resultMessage = input; // It should be same message as input message due to validated message
            bool onlyValidate = false;

            // Act
            var result = await _validateMessageService?.ValidateInputMessage(input, onlyValidate);

            // Assert
            Assert.Equal(result.Item1, resultMessage); // same as input message due to validated message
            Assert.Equal(result.Item2, true); // true as Validated message
        }
    }
}

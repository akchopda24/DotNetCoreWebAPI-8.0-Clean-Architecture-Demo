using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ModelValidate.BusinessLogic.Interface;
using ModelValidate.DataAccess.CustomModels;
using ModelValidate.Utilities.Constants;
using ModelValidate.Utilities.GenericResponse;

namespace ModelValidate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateMessageController : ControllerBase
    {
        private readonly IValidateMessageService _validateMessageService;

        /// <summary>
        /// Use dependency injection object service
        /// </summary>
        /// <param name="validateMessageService"></param>
        public ValidateMessageController(IValidateMessageService validateMessageService)
        {
            _validateMessageService = validateMessageService;
        }

        /// <summary>
        /// Detect phone numbers from input message param
        /// </summary>
        /// <param name="inputMessage">string message param</param>
        /// <returns>
        /// Success : Success or failed
        /// Data : Returns validated message or highlighted phone numbers
        /// AlertMessage : Return Success or Error message 
        /// </returns>
        [HttpPost("DetectPhoneNumbers")]
        public async Task<IActionResult> Post([FromBody] string inputMessage)
        {
            var response = new ApiSingleResponse<string>();

            var phoneNumbers = await _validateMessageService.ValidateInputMessage(inputMessage);

            response.Data = phoneNumbers.Item1;
            response.Success = phoneNumbers.Item2;
            if (response.Success)
            {
                response.AlertMessage = VMConstants.ValidatedMsg;
            }
            else
            {
                response.AlertMessage = VMConstants.NotValidatedMsg;
            }

            return Ok(response);
        }

        /// <summary>
        /// This API returns Get all validated message
        /// </summary>
        /// <returns>
        /// Success : Success or failed as True/False
        /// Data : Returns all validated message json
        /// AlertMessage : Return Success or Error message 
        /// </returns>
        [HttpGet("messages")]
        public async Task<IActionResult> Get()
        {
            var response = new ApiListResponse<ValidatedMessageDto>();
            response.Data = await _validateMessageService.GetValidatedMessages();
            if (response.Data.Count() > 0)
            {
                response.AlertMessage = VMConstants.DataFound;
            }
            else
            {
                response.AlertMessage = VMConstants.DataNotFound;
            }

            return Ok(response);
        }

        /// <summary>
        /// This API used for save validated message in InMemory database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// Success : Success or failed as True/False
        /// Data : Returns success saved validated message json or if not validated then return same model json
        /// AlertMessage : Return Success or Error message 
        /// </returns>
        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody] ValidatedMessageDto model)
        {
            var response = new ApiSingleResponse<ValidatedMessageDto>();

            var messageModel = await _validateMessageService.AddValidatedMessage(model);
            if (messageModel.Id == 0)
            {
                response.AlertMessage = VMConstants.NotValidatedMsg;
                response.Success = false;
            }
            else
            {
                response.AlertMessage = VMConstants.ValidatedMsg;
                response.Success = true;
            }
            response.Data = messageModel;

            return Ok(response);
        }
    }
}

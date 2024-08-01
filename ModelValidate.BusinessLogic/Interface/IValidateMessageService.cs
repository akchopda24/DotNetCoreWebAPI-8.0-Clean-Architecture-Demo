using ModelValidate.DataAccess.CustomModels;
using ModelValidate.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelValidate.BusinessLogic.Interface
{
    public interface IValidateMessageService
    {
        /// <summary>
        /// Validating message service call
        /// </summary>
        /// <param name="message">in this param, we will pass string message</param>
        /// <returns></returns>
        Task<Tuple<string, bool>> ValidateInputMessage(string message, bool onlyValidateMessage = false);

        /// <summary>
        /// Get all validated messages
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ValidatedMessageDto>> GetValidatedMessages();

        /// <summary>
        /// Add validated message by this service
        /// </summary>
        /// <param name="model">in model class we will pass Id & string Message</param>
        /// <returns></returns>
        Task<ValidatedMessageDto> AddValidatedMessage(ValidatedMessageDto model);
    }
}

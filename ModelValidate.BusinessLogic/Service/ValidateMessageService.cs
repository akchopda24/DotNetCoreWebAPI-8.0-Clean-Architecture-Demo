using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using ModelValidate.BusinessLogic.Interface;
using ModelValidate.DataAccess.CustomModels;
using ModelValidate.DataAccess.Entities;
using ModelValidate.DataAccess.GenericRepository;
using ModelValidate.Utilities.Constants;

namespace ModelValidate.BusinessLogic.Service
{
    public class ValidateMessageService : IValidateMessageService
    {
        private readonly IRepositoryBase<ValidatedMessage> _validatedMessageRepository;
        private readonly IMapper _mapper;

        public ValidateMessageService(IRepositoryBase<ValidatedMessage> validatedMessageRepository,
            IMapper mapper)
        {
            _validatedMessageRepository = validatedMessageRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Detect phone numbers from input string and check valid or not
        /// </summary>
        /// <param name="inputMessage">String as input message</param>
        /// <param name="onlyValidate">Optional Bool param if only check validate or not</param>
        /// <returns>Return true/false value with valid/not valid input message (highlighted phone number if not not valid)</returns>
        public async Task<Tuple<string, bool>> ValidateInputMessage(string inputMessage, bool onlyValidate = false)
        {
            var phoneNumbers = new List<string>();
            bool validatedInputMessage = false;
            string modifiedInputMessage = inputMessage;
            foreach (var pattern in VMConstants.PhonePatterns)
            {
                var matches = Regex.Matches(inputMessage, pattern, RegexOptions.IgnoreCase);

                foreach (Match match in matches)
                {
                    if (!phoneNumbers.Contains(match.Value))
                        phoneNumbers.Add(match.Value);
                }
            }

            validatedInputMessage = phoneNumbers.Count() > 0 ? VMConstants.NotValidated : VMConstants.Validated;

            if (!onlyValidate)
            {
                modifiedInputMessage = HighlightPhoneNumbers(inputMessage, phoneNumbers);
            }
            else
            {
                return Tuple.Create(inputMessage, validatedInputMessage);
            }

            return Tuple.Create(modifiedInputMessage, validatedInputMessage);
        }

        /// <summary>
        /// Highlight detected phone numbers in input message
        /// </summary>
        /// <param name="inputMessage">input message string</param>
        /// <param name="inputNumbers">detected phone numbers or numbers</param>
        /// <returns>Return highlighted input phone number message</returns>
        public string HighlightPhoneNumbers(string inputMessage, List<string> inputNumbers)
        {
            foreach (string word in inputNumbers)
            {
                inputMessage = inputMessage.Replace(word, $@"<b>{word}</b>");
            }
            return inputMessage;
        }

        /// <summary>
        /// Get all validated messages from in-memory db
        /// </summary>
        /// <returns>Return all validated messages json in data</returns>
        public async Task<IEnumerable<ValidatedMessageDto>> GetValidatedMessages()
        {
            var message = await _validatedMessageRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ValidatedMessageDto>>(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">model class with param Id & Message</param>
        /// <param name="Id">Id param as integer pass as zero for insert</param>
        /// <param name="message">message param as string value</param>
        /// <returns>return inserted model with unique Id</returns>
        public async Task<ValidatedMessageDto> AddValidatedMessage(ValidatedMessageDto model)
        {
            var validateMessage = await ValidateInputMessage(model.Message, true);
            if (model.Id == 0 && validateMessage.Item2)
            {
                var message = _mapper.Map<ValidatedMessage>(model);
                await _validatedMessageRepository.AddAsync(message);
                return _mapper.Map<ValidatedMessageDto>(message);
            }
            return model;
        }
    }
}

using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Extensions;
using TripleT.Application.Common.Models.Emailing;
using TripleT.Application.Interfaces.Infrastructure;

namespace TripleT.Infrastructure.Emailing
{
    public class EmailingService : IEmailingService
    {
        private readonly ILogger<EmailingService> _logger;

        public EmailingService(ILogger<EmailingService> logger)
        {
            _logger = logger;
        }

        public async Task<string> SendEmailAsync<TTemplateData>(EmailRequest<TTemplateData> request, CancellationToken cancellationToken)
        {
            var client = new AmazonSimpleNotificationServiceClient("", "", Amazon.RegionEndpoint.AFSouth1);

            _logger.LogInformation($"Sending message to emailing SNS topic: [{request.FormatAsJsonForLogging()}]");

            string json = JsonConvert.SerializeObject(request);

            //publish to an SNS topic
            PublishRequest publishRequest = new PublishRequest("arn:aws:sns:af-south-1:986076875842:TripleTEmailing", json);

            try
            {
                PublishResponse publishResponse = await client.PublishAsync(publishRequest, cancellationToken);

                _logger.LogInformation($"SNS publish response: [{publishResponse.FormatAsJsonForLogging()}]");

                return publishResponse.MessageId;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while sending email: [{e}]");
                throw;
            }
        }
    }
}

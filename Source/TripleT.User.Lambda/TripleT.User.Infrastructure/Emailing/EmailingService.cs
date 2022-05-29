using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.Common.Extensions;
using TripleT.User.Application.Common.Interfaces.Infrastructure;
using TripleT.User.Application.Common.Models.Emailing;
using TripleT.User.Application.Common.Utilities;

namespace TripleT.User.Infrastructure.Emailing
{
    public class EmailingService : IEmailingService
    {
        private readonly ILogger<EmailingService> _logger;
        private readonly IConfiguration _configuration;

        public EmailingService(ILogger<EmailingService> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> SendEmailAsync<TTemplateData>(EmailRequest<TTemplateData> request, CancellationToken cancellationToken)
        {
            var client = new AmazonSimpleNotificationServiceClient();

            _logger.LogInformation("Sending message to emailing SNS topic: [{Request}]", request.FormatAsJsonForLogging());

            var json = SerializerHelper.SerializeObject(request);

            var topicArn = _configuration["SNS_TOPIC_ARN"];
            
            //publish to an SNS topic
            var publishRequest = new PublishRequest(topicArn, json);

            try
            {
                var publishResponse = await client.PublishAsync(publishRequest, cancellationToken);

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
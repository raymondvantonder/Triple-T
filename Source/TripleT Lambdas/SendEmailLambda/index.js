var aws = require("aws-sdk");
var ses = new aws.SES({ region: "af-south-1" });

exports.handler = async (event, context) => {
  try {
    console.log(context);
    const snsEvent = event.Records[0];

    const message = JSON.parse(snsEvent.Sns.Message);

    const templateData = {
      name: message.data.name,
      surname: message.data.surname,
      verificationUrl: message.data.verificationUrl,
    };

    const params = {
      Source: "triplet.consulting.notify@gmail.com",
      Template: "TripleT-EmailVerificationTemplate",
      Destination: {
        ToAddresses: [],
      },
      TemplateData: JSON.stringify(templateData),
    };

    let result = null;

    for (let index = 0; index < message.toEmailAdresses.length; index++) {
      const email = message.toEmailAdresses[index];

      params.Destination.ToAddresses = [email];

      result = await ses.sendTemplatedEmail(params).promise();

      console.log(result);
    }

    return result;
  } catch (error) {
    console.log(error);
    throw error;
  }
};

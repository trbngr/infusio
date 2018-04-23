//----------------------
// <auto-generated>
// </auto-generated>
//----------------------
using LanguageExt;

namespace Infusion.Model
{
    public class EmailSentCreate : Record<EmailSentCreate>
    {
            public readonly long ContactId;
            public readonly string Headers;
            public readonly string HtmlContent;
            public readonly long Id;
            public readonly string OpenedDate;
            public readonly string PlainContent;
            public readonly string ReceivedDate;
            public readonly string SentDate;
            public readonly string SentFromAddress;
            public readonly string SentFromReplyAddress;
            public readonly string SentToAddress;
            public readonly string SentToBccAddresses;
            public readonly string SentToCcAddresses;
            public readonly string Subject;
      
      public EmailSentCreate(long contactId = default, string headers = default, string htmlContent = default, long id = default, string openedDate = default, string plainContent = default, string receivedDate = default, string sentDate = default, string sentFromAddress = default, string sentFromReplyAddress = default, string sentToAddress = default, string sentToBccAddresses = default, string sentToCcAddresses = default, string subject = default)
      {
                ContactId = contactId;
                Headers = headers;
                HtmlContent = htmlContent;
                Id = id;
                OpenedDate = openedDate;
                PlainContent = plainContent;
                ReceivedDate = receivedDate;
                SentDate = sentDate;
                SentFromAddress = sentFromAddress;
                SentFromReplyAddress = sentFromReplyAddress;
                SentToAddress = sentToAddress;
                SentToBccAddresses = sentToBccAddresses;
                SentToCcAddresses = sentToCcAddresses;
                Subject = subject;
        
      }

      public EmailSentCreate Copy(long? contactId = default, string headers = default, string htmlContent = default, long? id = default, string openedDate = default, string plainContent = default, string receivedDate = default, string sentDate = default, string sentFromAddress = default, string sentFromReplyAddress = default, string sentToAddress = default, string sentToBccAddresses = default, string sentToCcAddresses = default, string subject = default) => new EmailSentCreate(
                  contactId:  contactId ?? ContactId, 
                        headers:  headers ?? Headers, 
                        htmlContent:  htmlContent ?? HtmlContent, 
                        id:  id ?? Id, 
                        openedDate:  openedDate ?? OpenedDate, 
                        plainContent:  plainContent ?? PlainContent, 
                        receivedDate:  receivedDate ?? ReceivedDate, 
                        sentDate:  sentDate ?? SentDate, 
                        sentFromAddress:  sentFromAddress ?? SentFromAddress, 
                        sentFromReplyAddress:  sentFromReplyAddress ?? SentFromReplyAddress, 
                        sentToAddress:  sentToAddress ?? SentToAddress, 
                        sentToBccAddresses:  sentToBccAddresses ?? SentToBccAddresses, 
                        sentToCcAddresses:  sentToCcAddresses ?? SentToCcAddresses, 
                        subject:  subject ?? Subject
            
      );
    }
}
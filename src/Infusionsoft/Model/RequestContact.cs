//----------------------
// <auto-generated>
// </auto-generated>
//----------------------
using LanguageExt;

namespace Infusion.Model
{
    public class RequestContact : Record<RequestContact>
    {
            public readonly Lst<Address> Addresses;
            public readonly string Anniversary;
            public readonly string Birthday;
            public readonly RequestCompanyReference Company;
            public readonly string ContactType;
            public readonly Lst<CustomFieldValue> CustomFields;
            public readonly Lst<EmailAddress> EmailAddresses;
            public readonly string FamilyName;
            public readonly Lst<FaxNumber> FaxNumbers;
            public readonly string GivenName;
            public readonly string JobTitle;
            public readonly long LeadSourceId;
            public readonly string MiddleName;
            public readonly string Notes;
            public readonly string OptInReason;
            public readonly long OwnerId;
            public readonly Lst<PhoneNumber> PhoneNumbers;
            public readonly string PreferredLocale;
            public readonly string PreferredName;
            public readonly string Prefix;
            public readonly Lst<SocialAccount> SocialAccounts;
            public readonly SourceType SourceType;
            public readonly string SpouseName;
            public readonly string Suffix;
            public readonly string TimeZone;
            public readonly string Website;
      
      public RequestContact(Lst<Address> addresses = default, string anniversary = default, string birthday = default, RequestCompanyReference company = default, string contactType = default, Lst<CustomFieldValue> customFields = default, Lst<EmailAddress> emailAddresses = default, string familyName = default, Lst<FaxNumber> faxNumbers = default, string givenName = default, string jobTitle = default, long leadSourceId = default, string middleName = default, string notes = default, string optInReason = default, long ownerId = default, Lst<PhoneNumber> phoneNumbers = default, string preferredLocale = default, string preferredName = default, string prefix = default, Lst<SocialAccount> socialAccounts = default, SourceType sourceType = default, string spouseName = default, string suffix = default, string timeZone = default, string website = default)
      {
                Addresses = addresses;
                Anniversary = anniversary;
                Birthday = birthday;
                Company = company;
                ContactType = contactType;
                CustomFields = customFields;
                EmailAddresses = emailAddresses;
                FamilyName = familyName;
                FaxNumbers = faxNumbers;
                GivenName = givenName;
                JobTitle = jobTitle;
                LeadSourceId = leadSourceId;
                MiddleName = middleName;
                Notes = notes;
                OptInReason = optInReason;
                OwnerId = ownerId;
                PhoneNumbers = phoneNumbers;
                PreferredLocale = preferredLocale;
                PreferredName = preferredName;
                Prefix = prefix;
                SocialAccounts = socialAccounts;
                SourceType = sourceType;
                SpouseName = spouseName;
                Suffix = suffix;
                TimeZone = timeZone;
                Website = website;
        
      }

      public RequestContact Copy(Lst<Address> addresses = default, string anniversary = default, string birthday = default, RequestCompanyReference company = default, string contactType = default, Lst<CustomFieldValue> customFields = default, Lst<EmailAddress> emailAddresses = default, string familyName = default, Lst<FaxNumber> faxNumbers = default, string givenName = default, string jobTitle = default, long? leadSourceId = default, string middleName = default, string notes = default, string optInReason = default, long? ownerId = default, Lst<PhoneNumber> phoneNumbers = default, string preferredLocale = default, string preferredName = default, string prefix = default, Lst<SocialAccount> socialAccounts = default, SourceType sourceType = default, string spouseName = default, string suffix = default, string timeZone = default, string website = default) => new RequestContact(
                  addresses:  addresses == default ? Addresses : addresses, 
                        anniversary:  anniversary ?? Anniversary, 
                        birthday:  birthday ?? Birthday, 
                        company:  company == default ? Company : company, 
                        contactType:  contactType ?? ContactType, 
                        customFields:  customFields == default ? CustomFields : customFields, 
                        emailAddresses:  emailAddresses == default ? EmailAddresses : emailAddresses, 
                        familyName:  familyName ?? FamilyName, 
                        faxNumbers:  faxNumbers == default ? FaxNumbers : faxNumbers, 
                        givenName:  givenName ?? GivenName, 
                        jobTitle:  jobTitle ?? JobTitle, 
                        leadSourceId:  leadSourceId ?? LeadSourceId, 
                        middleName:  middleName ?? MiddleName, 
                        notes:  notes ?? Notes, 
                        optInReason:  optInReason ?? OptInReason, 
                        ownerId:  ownerId ?? OwnerId, 
                        phoneNumbers:  phoneNumbers == default ? PhoneNumbers : phoneNumbers, 
                        preferredLocale:  preferredLocale ?? PreferredLocale, 
                        preferredName:  preferredName ?? PreferredName, 
                        prefix:  prefix ?? Prefix, 
                        socialAccounts:  socialAccounts == default ? SocialAccounts : socialAccounts, 
                        sourceType:  sourceType == default ? SourceType : sourceType, 
                        spouseName:  spouseName ?? SpouseName, 
                        suffix:  suffix ?? Suffix, 
                        timeZone:  timeZone ?? TimeZone, 
                        website:  website ?? Website
            
      );
    }
}
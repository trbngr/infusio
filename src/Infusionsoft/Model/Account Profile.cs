//----------------------
// <auto-generated>
// </auto-generated>
//----------------------
using LanguageExt;

namespace Infusion.Model
{
    public class AccountProfile : Record<AccountProfile>
    {
            public readonly Address Address;
            public readonly Lst<string> BusinessGoals;
            public readonly string BusinessType;
            public readonly string CurrencyCode;
            public readonly string Email;
            public readonly string LanguageTag;
            public readonly string LogoUrl;
            public readonly string Name;
            public readonly string Phone;
            public readonly string PhoneExt;
            public readonly string TimeZone;
            public readonly string Website;
      
      public AccountProfile(Address address = default, Lst<string> businessGoals = default, string businessType = default, string currencyCode = default, string email = default, string languageTag = default, string logoUrl = default, string name = default, string phone = default, string phoneExt = default, string timeZone = default, string website = default)
      {
                Address = address;
                BusinessGoals = businessGoals;
                BusinessType = businessType;
                CurrencyCode = currencyCode;
                Email = email;
                LanguageTag = languageTag;
                LogoUrl = logoUrl;
                Name = name;
                Phone = phone;
                PhoneExt = phoneExt;
                TimeZone = timeZone;
                Website = website;
        
      }

      public AccountProfile Copy(Address address = default, Lst<string> businessGoals = default, string businessType = default, string currencyCode = default, string email = default, string languageTag = default, string logoUrl = default, string name = default, string phone = default, string phoneExt = default, string timeZone = default, string website = default) => new AccountProfile(
                  address:  address == default ? Address : address, 
                        businessGoals:  businessGoals == default ? BusinessGoals : businessGoals, 
                        businessType:  businessType ?? BusinessType, 
                        currencyCode:  currencyCode ?? CurrencyCode, 
                        email:  email ?? Email, 
                        languageTag:  languageTag ?? LanguageTag, 
                        logoUrl:  logoUrl ?? LogoUrl, 
                        name:  name ?? Name, 
                        phone:  phone ?? Phone, 
                        phoneExt:  phoneExt ?? PhoneExt, 
                        timeZone:  timeZone ?? TimeZone, 
                        website:  website ?? Website
            
      );
    }
}
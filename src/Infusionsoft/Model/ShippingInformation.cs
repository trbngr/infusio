//----------------------
// <auto-generated>
// </auto-generated>
//----------------------
using LanguageExt;

namespace Infusion.Model
{
    public class ShippingInformation : Record<ShippingInformation>
    {
            public readonly string City;
            public readonly string Company;
            public readonly string Country;
            public readonly string FirstName;
            public readonly long Id;
            public readonly string LastName;
            public readonly string MiddleName;
            public readonly string Phone;
            public readonly string State;
            public readonly string Street1;
            public readonly string Street2;
            public readonly string Zip;
      
      public ShippingInformation(string city = default, string company = default, string country = default, string firstName = default, long id = default, string lastName = default, string middleName = default, string phone = default, string state = default, string street1 = default, string street2 = default, string zip = default)
      {
                City = city;
                Company = company;
                Country = country;
                FirstName = firstName;
                Id = id;
                LastName = lastName;
                MiddleName = middleName;
                Phone = phone;
                State = state;
                Street1 = street1;
                Street2 = street2;
                Zip = zip;
        
      }

      public ShippingInformation Copy(string city = default, string company = default, string country = default, string firstName = default, long? id = default, string lastName = default, string middleName = default, string phone = default, string state = default, string street1 = default, string street2 = default, string zip = default) => new ShippingInformation(
                  city:  city ?? City, 
                        company:  company ?? Company, 
                        country:  country ?? Country, 
                        firstName:  firstName ?? FirstName, 
                        id:  id ?? Id, 
                        lastName:  lastName ?? LastName, 
                        middleName:  middleName ?? MiddleName, 
                        phone:  phone ?? Phone, 
                        state:  state ?? State, 
                        street1:  street1 ?? Street1, 
                        street2:  street2 ?? Street2, 
                        zip:  zip ?? Zip
            
      );
    }
}
//----------------------
// <auto-generated>
// </auto-generated>
//----------------------
using LanguageExt;

namespace Infusion.Model
{
    public class RequestCompanyReference : Record<RequestCompanyReference>
    {
            public readonly long Id;
      
      public RequestCompanyReference(long id = default)
      {
                Id = id;
        
      }

      public RequestCompanyReference Copy(long? id = default) => new RequestCompanyReference(
                  id:  id ?? Id
            
      );
    }
}
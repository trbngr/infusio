//----------------------
// <auto-generated>
// </auto-generated>
//----------------------
using LanguageExt;

namespace Infusion.Model
{
    public class CampaignNodeDTO : Record<CampaignNodeDTO>
    {
            public readonly string Id;
            public readonly string Name;
            public readonly object Properties;
            public readonly bool Ready;
            public readonly Type Type;
      
      public CampaignNodeDTO(string id = default, string name = default, object properties = default, bool ready = default, Type type = default)
      {
                Id = id;
                Name = name;
                Properties = properties;
                Ready = ready;
                Type = type;
        
      }

      public CampaignNodeDTO Copy(string id = default, string name = default, object properties = default, bool ready = default, Type type = default) => new CampaignNodeDTO(
                  id:  id ?? Id, 
                        name:  name ?? Name, 
                        properties:  properties ?? Properties, 
                        ready:  ready == default ? Ready : ready, 
                        type:  type == default ? Type : type
            
      );
    }
}
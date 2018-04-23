//----------------------
// <auto-generated>
// </auto-generated>
//----------------------
using LanguageExt;

namespace Infusion.Model
{
    public class Item : Record<Item>
    {
            public readonly long Id;
            public readonly string Name;
            public readonly long NextItemId;
            public readonly long PreviousItemId;
            public readonly Type Type;
      
      public Item(long id = default, string name = default, long nextItemId = default, long previousItemId = default, Type type = default)
      {
                Id = id;
                Name = name;
                NextItemId = nextItemId;
                PreviousItemId = previousItemId;
                Type = type;
        
      }

      public Item Copy(long? id = default, string name = default, long? nextItemId = default, long? previousItemId = default, Type type = default) => new Item(
                  id:  id ?? Id, 
                        name:  name ?? Name, 
                        nextItemId:  nextItemId ?? NextItemId, 
                        previousItemId:  previousItemId ?? PreviousItemId, 
                        type:  type == default ? Type : type
            
      );
    }
}
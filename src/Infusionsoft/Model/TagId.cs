//----------------------
// <auto-generated>
// </auto-generated>
//----------------------
using LanguageExt;

namespace Infusion.Model
{
    public class TagId : Record<TagId>
    {
            public readonly Lst<long> TagIds;
      
      public TagId(Lst<long> tagIds = default)
      {
                TagIds = tagIds;
        
      }

      public TagId Copy(Lst<long> tagIds = default) => new TagId(
                  tagIds:  tagIds == default ? TagIds : tagIds
            
      );
    }
}
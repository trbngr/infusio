//----------------------
// <auto-generated>
// </auto-generated>
//----------------------
using LanguageExt;

namespace Infusion.Model
{
    public class TaggedContactList : Record<TaggedContactList>
    {
            public readonly Lst<TaggedContact> Contacts;
            public readonly int Count;
            public readonly string Next;
            public readonly string Previous;
      
      public TaggedContactList(Lst<TaggedContact> contacts = default, int count = default, string next = default, string previous = default)
      {
                Contacts = contacts;
                Count = count;
                Next = next;
                Previous = previous;
        
      }

      public TaggedContactList Copy(Lst<TaggedContact> contacts = default, int? count = default, string next = default, string previous = default) => new TaggedContactList(
                  contacts:  contacts == default ? Contacts : contacts, 
                        count:  count ?? Count, 
                        next:  next ?? Next, 
                        previous:  previous ?? Previous
            
      );
    }
}
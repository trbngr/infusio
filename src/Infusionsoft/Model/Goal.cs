//----------------------
// <auto-generated>
// </auto-generated>
//----------------------
using LanguageExt;

namespace Infusion.Model
{
    public class Goal : Record<Goal>
    {
            public readonly HistoricalCounts HistoricalContactCounts;
            public readonly long Id;
            public readonly string Name;
            public readonly Lst<long> NextSequenceIds;
            public readonly Lst<long> PreviousSequenceIds;
            public readonly Type Type;
      
      public Goal(HistoricalCounts historicalContactCounts = default, long id = default, string name = default, Lst<long> nextSequenceIds = default, Lst<long> previousSequenceIds = default, Type type = default)
      {
                HistoricalContactCounts = historicalContactCounts;
                Id = id;
                Name = name;
                NextSequenceIds = nextSequenceIds;
                PreviousSequenceIds = previousSequenceIds;
                Type = type;
        
      }

      public Goal Copy(HistoricalCounts historicalContactCounts = default, long? id = default, string name = default, Lst<long> nextSequenceIds = default, Lst<long> previousSequenceIds = default, Type type = default) => new Goal(
                  historicalContactCounts:  historicalContactCounts == default ? HistoricalContactCounts : historicalContactCounts, 
                        id:  id ?? Id, 
                        name:  name ?? Name, 
                        nextSequenceIds:  nextSequenceIds == default ? NextSequenceIds : nextSequenceIds, 
                        previousSequenceIds:  previousSequenceIds == default ? PreviousSequenceIds : previousSequenceIds, 
                        type:  type == default ? Type : type
            
      );
    }
}
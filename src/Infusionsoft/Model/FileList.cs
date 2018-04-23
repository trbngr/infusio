//----------------------
// <auto-generated>
// </auto-generated>
//----------------------
using LanguageExt;

namespace Infusion.Model
{
    public class FileList : Record<FileList>
    {
            public readonly int Count;
            public readonly Lst<FileDescriptor> Files;
            public readonly string Next;
            public readonly string Previous;
      
      public FileList(int count = default, Lst<FileDescriptor> files = default, string next = default, string previous = default)
      {
                Count = count;
                Files = files;
                Next = next;
                Previous = previous;
        
      }

      public FileList Copy(int? count = default, Lst<FileDescriptor> files = default, string next = default, string previous = default) => new FileList(
                  count:  count ?? Count, 
                        files:  files == default ? Files : files, 
                        next:  next ?? Next, 
                        previous:  previous ?? Previous
            
      );
    }
}
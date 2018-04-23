//----------------------
// <auto-generated>
// </auto-generated>
//----------------------
using LanguageExt;

namespace Infusion.Model
{
    public class FileInformation : Record<FileInformation>
    {
            public readonly string FileData;
            public readonly FileDescriptor FileDescriptor;
      
      public FileInformation(string fileData = default, FileDescriptor fileDescriptor = default)
      {
                FileData = fileData;
                FileDescriptor = fileDescriptor;
        
      }

      public FileInformation Copy(string fileData = default, FileDescriptor fileDescriptor = default) => new FileInformation(
                  fileData:  fileData ?? FileData, 
                        fileDescriptor:  fileDescriptor == default ? FileDescriptor : fileDescriptor
            
      );
    }
}
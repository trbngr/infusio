using System.IO;
using System.Reflection;
using NJsonSchema.CodeGeneration;
using DefaultTemplateFactory = NSwag.CodeGeneration.DefaultTemplateFactory;

namespace DslCompiler.CodeGen
{
    public class TemplateFactory : DefaultTemplateFactory
    {
        public TemplateFactory(CodeGeneratorSettingsBase settings, Assembly[] assemblies) : base(settings, assemblies)
        {
        }

        protected override string GetEmbeddedLiquidTemplate(string language, string template)
        {
            var stream = GetType().Assembly.GetManifestResourceStream($"DslCompiler.CodeGen.{template}.liquid");            
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }
    }
}
using System.Collections.Generic;
using NSwag.CodeGeneration.CSharp.Models;

namespace DslCompiler.CodeGen
{
    public class FreeMonadTemplateModel : CSharpTemplateModelBase
    {
        private readonly FreeMonadGeneratorSettings _settings;

        public FreeMonadTemplateModel(
            IEnumerable<CSharpOperationModel> operations,
            string controllerName,
            FreeMonadGeneratorSettings settings
        ) : base(controllerName, settings)
        {
            _settings = settings;
            Operations = operations;
        }

        /// <summary>Gets the namespace.</summary>
        public string Namespace => _settings.CSharpGeneratorSettings.Namespace ?? string.Empty;
        public IEnumerable<CSharpOperationModel> Operations { get; }
        public string FreeType => _settings.FreeType;
        public string DslModuleName => _settings.DslModuleName;
    }
}
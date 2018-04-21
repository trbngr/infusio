using System;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.OperationNameGenerators;

namespace DslCompiler.CodeGen
{
    /// <summary>
    /// Settings for the <see cref="FreeMonadGenerator"/>.
    /// </summary>
    public class FreeMonadGeneratorSettings : SwaggerToCSharpGeneratorSettings
    {
        public FreeMonadGeneratorSettings()
        {
            ClassName = "InfusionsoftFree";
            OperationNameGenerator = new NormalizedOperationNameGenerator();
            CodeGeneratorSettings.TemplateFactory = new TemplateFactory(
                CodeGeneratorSettings,
                new[] {GetType().Assembly}
            );
        }

        /// <summary>
        /// Gets or sets the name of the Free Monad class.
        /// </summary>
        /// <remarks>Defaults to Free. Generates Free&lt;A&gt;</remarks>
        public string FreeType { get; set; }

        /// <summary>
        /// Gets of sets the name of the DSL module.
        /// </summary>
        /// <remarks>Defaults to Dsl</remarks>
        public string DslModuleName { get; set; }

        private class NormalizedOperationNameGenerator : IOperationNameGenerator
        {
            public bool SupportsMultipleClients { get; } = false;

            public virtual string GetClientName(SwaggerDocument document, string path,
                SwaggerOperationMethod httpMethod, SwaggerOperation operation) =>
                string.Empty;

            public virtual string GetOperationName(SwaggerDocument document, string path,
                SwaggerOperationMethod httpMethod, SwaggerOperation operation)
            {
                var indexOf = operation.OperationId.IndexOf("Using", StringComparison.Ordinal);
                return indexOf > 0 ? operation.OperationId.Substring(0, indexOf) : operation.OperationId;
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp.Models;

namespace DslCompiler.CodeGen
{
    public class FreeMonadGenerator : SwaggerToCSharpGeneratorBase
    {
        private readonly SwaggerDocument _document;
        private readonly FreeMonadGeneratorSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="FreeMonadGenerator" /> class.
        /// </summary>
        /// <param name="document">The Swagger document.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="resolver">The resolver.</param>
        public FreeMonadGenerator(
            SwaggerDocument document,
            FreeMonadGeneratorSettings settings,
            CSharpTypeResolver resolver
        ) : base(document, settings, resolver)
        {
            _document = document;
            _settings = settings;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FreeMonadGenerator" /> class.
        /// </summary>
        /// <param name="document">The Swagger document.</param>
        /// <param name="settings">The settings.</param>
        public FreeMonadGenerator(SwaggerDocument document,
            FreeMonadGeneratorSettings settings) :
            this(document, settings, CreateResolverWithExceptionSchema(settings.CSharpGeneratorSettings, document))
        {
        }

        /// <inheritdoc />
        public override string GenerateFile() =>
            GenerateClientClass(null, null, null, ClientGeneratorOutputType.Implementation);

        /// <inheritdoc />
        public override ClientGeneratorBaseSettings BaseSettings => _settings;

        /// <inheritdoc />
        protected override string GenerateClientClass(
            string controllerName,
            string controllerClassName,
            IList<CSharpOperationModel> operations,
            ClientGeneratorOutputType outputType
        )
        {
            _document.GenerateOperationIds();

           var ops = _document.Paths
                .SelectMany(pair => pair.Value.Select(p => new { Path = pair.Key.TrimStart('/'), HttpMethod = p.Key, Operation = p.Value }))
                .Select(tuple =>
                {
                    var opModel = CreateOperationModel(tuple.Operation, BaseSettings);
                    opModel.ControllerName = BaseSettings.OperationNameGenerator.GetClientName(_document, tuple.Path, tuple.HttpMethod, tuple.Operation);
                    opModel.Path = tuple.Path;
                    opModel.HttpMethod = tuple.HttpMethod;
                    opModel.OperationName = BaseSettings.OperationNameGenerator.GetOperationName(_document, tuple.Path, tuple.HttpMethod, tuple.Operation);
                    return opModel;
                })
                .ToList();
            
            var model = new FreeMonadTemplateModel(ops, controllerName, _settings);
            var factory = _settings.CSharpGeneratorSettings.TemplateFactory;
            var template = factory.CreateTemplate("CSharp", "Free", model);
            return template.Render();
        }

        /// <inheritdoc />
        protected override CSharpOperationModel CreateOperationModel(
            SwaggerOperation operation,
            ClientGeneratorBaseSettings settings
        ) => new CSharpOperationModel(
            operation,
            (SwaggerToCSharpGeneratorSettings) settings,
            this,
            (CSharpTypeResolver) Resolver
        );
    }
}
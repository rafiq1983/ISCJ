using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Core.Web
{
    public class AddRequiredHeaderParameter //: IOperationFilter
    {
        /*
        public void Apply(Swashbuckle.AspNetCore.Swagger.Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<Parameter>();

            operation.Parameters.Add(new Parameter
            {
                name = "MyHeaderField",
                @in = "header",
                type = "string",
                description = "My header field",
                required = true
            });
        }

        void IOperationFilter.Apply(Swashbuckle.AspNetCore.Swagger.Operation operation, OperationFilterContext context)
        {
           operation.Para
        }*/
    }


    public class SwaggerSecurityRequirementsDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument document, DocumentFilterContext context)
        {
            document.Security = new List<IDictionary<string, IEnumerable<string>>>()
            {
                new Dictionary<string, IEnumerable<string>>()
                {
                    { "Bearer", new string[]{ } },
                    { "Basic", new string[]{ } },
                }
            };
        }
    }
}

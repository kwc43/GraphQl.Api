using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQl.Infrastructure.GraphQl;
using GraphQl.Infrastructure.GraphQl.Queries;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Mvc;

namespace GraphQl.Api.Controllers
{
    [Microsoft.AspNetCore.Components.Route("[controller]")]
    public class GraphQlController : Controller
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;

        public GraphQlController(IDocumentExecuter documentExecuter, ISchema schema)
        {
            _documentExecuter = documentExecuter;
            _schema = schema;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQlQuery query,
            [FromServices] IEnumerable<IValidationRule> validationRules)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var inputs = query.Variables.ToInputs();
            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query.Query,
                Inputs = inputs,
                UserContext = new GraphQlUserContext
                {
                    User = User
                },
                ValidationRules = validationRules,
#if (DEBUG)
                ExposeExceptions = true,
                EnableMetrics = true,
#endif
            };

            var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
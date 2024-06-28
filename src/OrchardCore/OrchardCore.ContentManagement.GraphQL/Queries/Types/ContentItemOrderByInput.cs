using System;
using Microsoft.Extensions.Options;
using OrchardCore.Apis.GraphQL.Queries;
using OrchardCore.Apis.GraphQL.Queries.Types;
using OrchardCore.ContentManagement.GraphQL.Options;

namespace OrchardCore.ContentManagement.GraphQL.Queries.Types
{
    public class ContentItemOrderByInput : OrderByInputObjectGraphType
    {
        private readonly IOptions<GraphQLContentOptions> _optionsAccessor;

        public ContentItemOrderByInput(string contentItemName, IOptions<GraphQLContentOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;

            Name = $"{contentItemName}OrderByInput";

            Description = $"the {contentItemName} content item order by input";

            this.AddScalarFilterFields<OrderByDirectionGraphType>("contentItemId", "content item id");
            this.AddScalarFilterFields<OrderByDirectionGraphType>("contentItemVersionId", "the content item version id");
            this.AddScalarFilterFields<OrderByDirectionGraphType>("contentType", "the content type of the content item");
            this.AddScalarFilterFields<OrderByDirectionGraphType>("displayText", "the display text of the content item");
            this.AddScalarFilterFields<OrderByDirectionGraphType>("published", "is the published version");
            this.AddScalarFilterFields<OrderByDirectionGraphType>("latest", "is the latest version");
            this.AddScalarFilterFields<OrderByDirectionGraphType>("createdUtc", "the date and time of creation");
            this.AddScalarFilterFields<OrderByDirectionGraphType>("modifiedUtc", "the date and time of modification");
            this.AddScalarFilterFields<OrderByDirectionGraphType>("publishedUtc", "the date and time of publication");
            this.AddScalarFilterFields<OrderByDirectionGraphType>("owner", "the owner of the content item");
            this.AddScalarFilterFields<OrderByDirectionGraphType>("author", "the author of the content item");
        }

        public override void AddScalarFilterFields(Type graphType, string fieldName, string description)
        {
            if (!_optionsAccessor.Value.ShouldSkip(typeof(ContentItemType), fieldName))
            {
                base.AddScalarFilterFields(graphType, fieldName, description);
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DialogFoundation.Backend.LG;
using LanguageGeneration.V2;
using Microsoft.Bot.Builder.Ai.LanguageGeneration.Engine;
using Microsoft.Bot.Builder.Ai.LanguageGeneration.Helpers;
using Microsoft.Bot.Schema;

namespace Microsoft.Bot.Builder.Ai.LanguageGeneration.Resolver
{
    public class LGResolver
    {
        private readonly LGEndpoint _lgEndpoint;
        private readonly LGOptions _lgOptions;
        private readonly IResolverPipelineFactory _resolverPipelineFactory;
        private readonly IResolverPipeline _resolverPipeline;

        public LGResolver(LGEndpoint lgEndpoint, LGOptions lgOptions)
        {
            _lgEndpoint = lgEndpoint ?? throw new ArgumentNullException(nameof(lgEndpoint));
            _lgOptions = lgOptions ?? throw new ArgumentNullException(nameof(lgOptions));
            _resolverPipelineFactory = new ResolverPipelineFactory();
            _resolverPipeline = _resolverPipelineFactory.CreateResolverPipeline(_lgEndpoint.EndpointURI);
        }

        public async Task ResolveAsync(Activity activity, IDictionary<string, object> entities)
        {
            if (activity == null)
            {
                throw new ArgumentNullException(nameof(activity));
            }

            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            await _resolverPipeline.ExecuteAsync(activity, entities).ConfigureAwait(false);
        }
    }
}

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace yjx.core.service.modold
{
    public class ResourceService : IResourceService
    {
        private readonly IOptions<Settings> settings;

        public ResourceService(IOptions<Settings> settings)
        {
            this.settings = settings;
        }
    }
}

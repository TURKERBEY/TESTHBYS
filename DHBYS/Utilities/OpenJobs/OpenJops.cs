using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Utilities.OpenJobs
{

    
    public class OpenJops
    {
        IMemoryCache _memoryCache;

        public OpenJops(IMemoryCache memoryCache)
        {

            _memoryCache = memoryCache;
        }
        public bool islem()
        {
            return true;
        }

    }
}

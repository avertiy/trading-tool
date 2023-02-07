using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVS.PoloniexApi.Infrastructure;
using AVS.Trading.Core;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Services;

namespace AVS.PoloniexApi.Services
{
    public class PoloniexSymbolService : SymbolService
    {
        public PoloniexSymbolService()
        {
        }

        protected override string SymbolToPairInternal(string str)
        {
            return str;
        }
    }
}

using System;
using System.Collections.Generic;
using AVS.CoreLib.WinForms.MVC;
using AVS.Trading.Tool.Controls.TradingTools.ChildControls;
using AVS.Trading.Framework.Services.TradingTools;
using AVS.Trading.Framework.Services.WalletTools;
using AVS.Trading.Core;
using AVS.Trading.Core.Interfaces;
using AVS.Trading.Core.Interfaces.WalletTools;
using AVS.Trading.Core.Services;

namespace AVS.Trading.Tool.Controls.TradingTools.Controllers
{
    public class MyTradeBalancesController : ControllerBase<IMyTradeBalancesView>
    {
        private readonly IWalletDataPreprocessor _dataPreprocessor;
        private readonly IWalletToolsService _walletService;
        private readonly IMarginToolsService _marginService;

        public MyTradeBalancesController(IWalletDataPreprocessor dataPreprocessor, IWalletToolsService walletService, IMarginToolsService marginService)
        {
            _dataPreprocessor = dataPreprocessor;
            _walletService = walletService;
            _marginService = marginService;
        }

        public BalanceInfo LoadData(string market)
        {
            var pair = CurrencyPair.Parse(market);
            if(pair == null || pair.IsAll)
                return null;

            var response = _walletService.GetCompleteBalances();
            var tradableMarginBalances = _marginService.GetTradableBalances();
            var info = _dataPreprocessor.GetBalanceInfo(pair, response.Data, tradableMarginBalances);
            return info;
        }
    }

    
}
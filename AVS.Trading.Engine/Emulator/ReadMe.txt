	//Context [::AlgorithmContext] 
    //contains market data, trading data and state [long / short positions, balances etc.])

    //Engine [::TradingEngine]
    //
    //-------- start Engine.Excecute(string exchange, CurrencyPair pair) ----------
    //restores TradeSession via ITradeSessionService
    //initiates Algorithm and ContextSettings
    //builds Context via IContextBuilder 
    //restores algorithm State based on TradeSession via IStateService (or IContextBuilder due to Context includes State)
    //IStateService also checks whether there were any new trades, updates Balances and PositionMap
    //in case we want emulate trading the IStateService as well as IContextBuilder must provide data instead of real calls to exchange API
    //____________________________
    //at this point we have a certain Algorithm and Context are ready
    //Algorithm.Execute(context)=> IDecision
    //IDecisionHandler handles IDecision [calls exchange API or emulates API calls]
    //IDecisionHandler.Execute(context, decision)=> IResult [PlaceOrderResult/MoveOrderResult/EmptyResult]
    //IDecision/IResult could be logged keeping a record of all Algorithm actions/decisions
    //-------- end Engine.Excecute(string exchange, CurrencyPair pair) ----------

    //Emulator instantiates the Engine injecting services that emulates exhcnage API calls
    //Emulator.Run() 
    //calls MarketApi to get chart data [ICandlestick[] candles]
    //separates candles on history set and test set  [80/20]
    //foreach (var candle in testSet)
    //  contextBuilder.AppendToChartData(candle);
    //  decisionHandler 
    //  engine.Execute("fake","MAID_BTC");
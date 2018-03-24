namespace StockProject.BalanceSheet
{
    public sealed class ConsolidatedBalanceSheet
    {
        public ConsolidatedBalanceSheet()
        {
            Assets = new Assets();
            LiabilitiesAndEquity = new LiabilitiesAndEquity();
            StockholdersEquity = new StockholdersEquity();
        }

        public ConsolidatedBalanceSheet(Assets assets, LiabilitiesAndEquity liabilities, StockholdersEquity stockholdersEquity)
        {
            Assets = assets;
            LiabilitiesAndEquity = liabilities;
            StockholdersEquity = stockholdersEquity;
        }

        public Assets Assets { get; set; }
        public LiabilitiesAndEquity LiabilitiesAndEquity { get; set; }
        public StockholdersEquity StockholdersEquity { get; set; }

        public bool SheetBalances => (Assets.TotalAssests - (LiabilitiesAndEquity.TotalLiabilities + 
            StockholdersEquity.TotalStockholdersEquity)) == 0;
    }

    public sealed class CurrentAssets
    {
        public double CashAndEquivalents { get; set; }
        public double MarketableSecurities { get; set; }
        public double TotalCashAndMarketable => CashAndEquivalents + MarketableSecurities;
        public double AccountsReceivable { get; set; }
        public double ReceivableUnderReversePurchase { get; set; }
        public double IncomeTaxesReceivable { get; set; }
        public double Inventory { get; set; }
        public double OtherCurrentAssets { get; set; }
        public double TotaleCurrentAssets => TotalCashAndMarketable + AccountsReceivable + ReceivableUnderReversePurchase + 
            IncomeTaxesReceivable + Inventory + OtherCurrentAssets;
    }

    public sealed class Assets
    {
        public Assets()
        {
            CurrentAssets = new CurrentAssets();
        }

        public Assets(CurrentAssets currentAssets)
        {
            CurrentAssets = currentAssets;
        }

        public CurrentAssets CurrentAssets { get; set; }
        public double NonmarketableInvestments { get; set; }
        public double DeferredIncomeTaxes { get; set; }
        public double NetProperty { get; set; }
        public double NetIntangibleAssets { get; set; }
        public double Goodwill { get; set; }
        public double OtherNoncurrentAssets { get; set; }
        public double TotalAssests => CurrentAssets.TotaleCurrentAssets + NonmarketableInvestments +
            DeferredIncomeTaxes + NetProperty + NetIntangibleAssets + Goodwill + 
            OtherNoncurrentAssets;
    }

    public sealed class LiabilitiesAndEquity
    {
        public LiabilitiesAndEquity()
        {
            CurrentLiabilities = new CurrentLiabilities();
        }

        public LiabilitiesAndEquity(CurrentLiabilities currentLiabilities)
        {
            CurrentLiabilities = currentLiabilities;
        }

        public CurrentLiabilities CurrentLiabilities { get; set; }
        public double LongTermDebt { get; set; }
        public double DeferredRevenueNonCurrent { get; set; }
        public double IncomeTaxesPayableNonCurrent { get; set; }
        public double DeferredIncomeTaxes { get; set; }
        public double OtherLongTermLiabilities { get; set; }
        public double TotalLiabilities => CurrentLiabilities.TotalCurrentLiabilities + LongTermDebt + 
            DeferredRevenueNonCurrent + IncomeTaxesPayableNonCurrent + DeferredIncomeTaxes + 
            OtherLongTermLiabilities;
    }

    public sealed class CurrentLiabilities
    {
        public double AccountsPayable { get; set; }
        public double ShortTermDebt { get; set; }
        public double AccruedCompensationAndBenefits { get; set; }
        public double AccruedExpensesAndOtherCurrentLiabilities { get; set; }
        public double AccruedRevenueShare { get; set; }
        public double SecuritiesLendingPayable { get; set; }
        public double DeferredRevenue { get; set; }
        public double NetIncomeTaxesPayable { get; set; }
        public double TotalCurrentLiabilities => AccountsPayable + ShortTermDebt + AccruedCompensationAndBenefits +
            AccruedExpensesAndOtherCurrentLiabilities + AccruedRevenueShare + SecuritiesLendingPayable + DeferredRevenue + 
            NetIncomeTaxesPayable;
    }

    public sealed class StockholdersEquity
    {
        public double ConvertiblePreferredStock { get; set; }
        public double CommonStockClassAandB { get; set; }
        public double AccumulatedOtherCompehensiveLoss { get; set; }
        public double RetainedEarnings { get; set; }
        public double TotalStockholdersEquity => ConvertiblePreferredStock + CommonStockClassAandB + 
            AccumulatedOtherCompehensiveLoss + RetainedEarnings;

    }
}

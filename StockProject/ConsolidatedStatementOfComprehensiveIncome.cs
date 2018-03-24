namespace StockProject.Income
{
    public sealed class ConsolidatedStatementOfComprehensiveIncome
    {
        public ConsolidatedStatementOfComprehensiveIncome()
        {
            AvailableForSaleInvestments = new AvailableForSaleInvestments();
            CashFlowHedges = new CashFlowHedges();
        }

        public ConsolidatedStatementOfComprehensiveIncome(AvailableForSaleInvestments availableForSaleInvestments, CashFlowHedges cashFlowHedges)
        {
            AvailableForSaleInvestments = availableForSaleInvestments;
            CashFlowHedges = cashFlowHedges;
        }

        public double NetIncome { get; set; }
        public double ChangeInForeignCurrencyTranslationAdjustment { get; set; }
        public AvailableForSaleInvestments AvailableForSaleInvestments { get; set; }
        public CashFlowHedges CashFlowHedges { get; set; }
        public double OtherCompehensiveIncome { get; set; }
        public double TotalComprehensiveIncome => NetIncome + ChangeInForeignCurrencyTranslationAdjustment +
            AvailableForSaleInvestments.NetChange + CashFlowHedges.NetChange + OtherCompehensiveIncome;
    }

    public sealed class AvailableForSaleInvestments
    {
        public double ChangeInNetUnrealizedGains { get; set; }
        public double ReclassificationAdjustmentForNetGains { get; set; }
        public double NetChange => ChangeInNetUnrealizedGains + ReclassificationAdjustmentForNetGains;
    }

    public sealed class CashFlowHedges
    {
        public double ChangeInNetUnrealizedGains { get; set; }
        public double ReclassificationAdjustmentForNetGains { get; set; }
        public double NetChange => ChangeInNetUnrealizedGains + ReclassificationAdjustmentForNetGains;
    }

}

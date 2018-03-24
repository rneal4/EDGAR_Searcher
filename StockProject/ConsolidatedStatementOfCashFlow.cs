namespace StockProject.CashFlow
{
    public sealed class ConsolidatedStatementOfCashFlow
    {
        public ConsolidatedStatementOfCashFlow()
        {
            OperatingActivities = new OperatingActivities();
            InvestingActivities = new InvestingActivities();
            FinancingActivities = new FinancingActivities();
        }

        public ConsolidatedStatementOfCashFlow(OperatingActivities operatingActivities, InvestingActivities investingActivities, FinancingActivities financingActivities)
        {
            OperatingActivities = operatingActivities;
            InvestingActivities = investingActivities;
            FinancingActivities = financingActivities;
        }

        public OperatingActivities OperatingActivities { get; set; }
        public InvestingActivities InvestingActivities { get; set; }
        public FinancingActivities FinancingActivities { get; set; }
        public double EffectOfExchangeRateChangesOnCash { get; set; }
        public double NetChangeInCashAndEquivalents => OperatingActivities.NetCashProviedByOperatingActivities +
            InvestingActivities.NetCashUsedInInvestingActivities + FinancingActivities.NetCashUsedInFinancingActivities +
            EffectOfExchangeRateChangesOnCash;
    }

    public sealed class OperatingActivities
    {
        public double NetIncome { get; set; }
        public double DepreciationOfProperty { get; set; }
        public double AmortizationOfIntangibleAssets { get; set; }
        public double StockBasedCompensationExpense { get; set; }
        public double ExcessTaxBenefitsFromStockBasedAward { get; set; }
        public double DeferredIncomeTaxes { get; set; }
        public double GainOnEquityInterest { get; set; }
        public double NetLossOnInvestments { get; set; }
        public double OtherAdjustments { get; set; }
        public double AccountsReceivable { get; set; }
        public double NetIncomeTaxes { get; set; }
        public double OtherAssets { get; set; }
        public double AccountsPayable { get; set; }
        public double AccruedExpensesAndLiabilities { get; set; }
        public double AccruedRevenueShare { get; set; }
        public double DeferredRevenue { get; set; }
        public double NetCashProviedByOperatingActivities => NetIncome + DepreciationOfProperty + AmortizationOfIntangibleAssets +
            StockBasedCompensationExpense + ExcessTaxBenefitsFromStockBasedAward + DeferredIncomeTaxes + GainOnEquityInterest + 
            NetLossOnInvestments + OtherAdjustments + AccountsReceivable + NetIncomeTaxes + OtherAssets + AccountsPayable +
            AccruedExpensesAndLiabilities + AccruedRevenueShare + DeferredRevenue;
    }

    public sealed class InvestingActivities
    {
        public double PurchasesOfProperty { get; set; }
        public double ProceedsFromDisposalsOfProperty { get; set; }
        public double PurchasesOfMarketableSecurities { get; set; }
        public double MaturitiesAndSalesOfInvestments { get; set; }
        public double CashCollateral { get; set; }
        public double InvestmentsInReverseRepurchaseAgreements { get; set; }
        public double AcquisitionsNet { get; set; }
        public double ProceedsFromCollectionOfNotes { get; set; }
        public double NetCashUsedInInvestingActivities => PurchasesOfProperty + ProceedsFromDisposalsOfProperty +
            PurchasesOfMarketableSecurities + MaturitiesAndSalesOfInvestments + CashCollateral + InvestmentsInReverseRepurchaseAgreements +
            AcquisitionsNet + ProceedsFromCollectionOfNotes;
    }

    public sealed class FinancingActivities
    {
        public double NetPaymentsToStockBasedAwards { get; set; }
        public double ExcessTaxBenefitsFromStockBasedAwards { get; set; }
        public double AdjustmentPaymentToClassCStockHolders { get; set; }
        public double RepurchasesOfCapitalStock { get; set; }
        public double NetProceedsFromIssuranceOfDebt { get; set; }
        public double RepaymentsOfDebt { get; set; }
        public double ProceedsFromSaleOfSubsidiaryShares { get; set; }
        public double NetCashUsedInFinancingActivities => NetPaymentsToStockBasedAwards + ExcessTaxBenefitsFromStockBasedAwards + RepurchasesOfCapitalStock +
           AdjustmentPaymentToClassCStockHolders + NetProceedsFromIssuranceOfDebt + RepaymentsOfDebt + ProceedsFromSaleOfSubsidiaryShares;
    }
}

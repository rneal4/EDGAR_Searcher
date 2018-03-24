namespace StockProject.Income
{
    public sealed class ConsolidatedStaementOfIncome
    {
        public double Revenues { get; set; }
        public CostsAndExpenses CostsAndExpenses { get; set; }
        public double IncomeFromOperations => Revenues - CostsAndExpenses.TotalCostsAndExpenses;
        public double NetOtherIncome { get; set; }
        public double TotalPreTaxIncome => IncomeFromOperations + NetOtherIncome;
        public double ProvisionForIncomeTaxes { get; set; }
        public double NetIncome => TotalPreTaxIncome - ProvisionForIncomeTaxes;
        public double BasicNetIncomePerShareStocksClassABC { get; set; }
        public double DilutedNetIncomePerShareStocksClassABC { get; set; }
    }

    public sealed class CostsAndExpenses
    {
        public double CostOfRevenues { get; set; }
        public double ResearchAndDevelopment { get; set; }
        public double SalesAndMarketing { get; set; }
        public double GeneralAndAdministrative { get; set; }
        public double EuropeanCommissionFine { get; set; }
        public double TotalCostsAndExpenses => CostOfRevenues + ResearchAndDevelopment + SalesAndMarketing +
            GeneralAndAdministrative + EuropeanCommissionFine;
    }
}

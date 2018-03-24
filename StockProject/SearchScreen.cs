using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using StockProject.BalanceSheet;
using StockProject.CashFlow;
using StockProject.Income;

namespace StockProject
{
    //TODO: Decouple browser automation logic from UI logic

    public partial class SearchScreen : Form
    {
        public SearchScreen()
        {
            InitializeComponent();
        }

        private void btnStartSearch_Click(object sender, EventArgs e)
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = false;

            var options = new ChromeOptions();
            options.AddArguments("start-maximized");
            options.AddArguments("--disable-extensions");
            options.AddArgument("no-sandbox");
            options.AddArgument("ignore-certificate-errors");
            options.AddArgument("--allow-running-insecure-content");
            options.AddAdditionalCapability("useAutomationExtension", false);

            var driver = new ChromeDriver(driverService, options);

            Thread.Sleep(1000);

            driver.Navigate().GoToUrl("https://www.sec.gov/edgar/searchedgar/companysearch.html");

            Thread.Sleep(2000);

            try
            {
                //EDGAR Search Page

                driver.FindElementById("cik").SendKeys(txtTicketSymbol.Text);

                Thread.Sleep(2000);

                driver.FindElementById("cik_find").Click();

                Thread.Sleep(2000);

                var tableDiv = driver.FindElement(By.Id("seriesDiv"));

                var tableRows = tableDiv.FindElements(By.TagName("tr"));

                foreach (var tr in tableRows)
                {
                    if (tr.Text.Contains("10-Q"))
                    {
                        tr.FindElement(By.Id("documentsbutton")).Click();
                        break;
                    }
                }

                Thread.Sleep(2000);

                tableDiv = driver.FindElement(By.ClassName("tableFile"));

                tableRows = tableDiv.FindElements(By.TagName("tr"));

                foreach (var tr in tableRows)
                {
                    if (tr.Text.Contains("FORM 10-Q"))
                    {
                        tr.FindElement(By.XPath("//*/td[3]/a")).Click();
                        break;
                    }
                }
                
                Thread.Sleep(2000);

                var Tables = driver.FindElements(By.TagName("table"));

                foreach (var table in Tables)
                {
                    if (table.Text.Contains("Liabilities and Stockholders’ Equity"))
                        balanceSheet = GetBalanceSheet(table);
                    else if (table.Text.Contains("Net cash provided by operating activities"))
                        cashFlow = GetCashFlow(table);
                    else if (table.Text.Contains("Cash flow hedges"))
                        comprehensiveIncome = GetComprehensiveIncome(table);
                    else if (table.Text.Contains("sdfsdfdfgdfg"))
                        income = GetIncome(table);
                }

            }
            catch (Exception ex)
            { }
        }

        private ConsolidatedBalanceSheet GetBalanceSheet(IWebElement table)
        {
            var currentAssets = new CurrentAssets();
            var assets = new Assets();
            var liabilities = new LiabilitiesAndEquity();
            var currentLiabilities = new CurrentLiabilities();
            var stockholdersEquity = new StockholdersEquity();

            var tableRows = table.FindElements(By.TagName("tr"));

            foreach (var tr in tableRows)
            {
                var tableCellCount = tr.FindElements(By.TagName("td")).Count();
                var tcs= tr.FindElements(By.TagName("td"));

                if (tr.Text.Contains("Cash and cash equivalents"))
                    currentAssets.CashAndEquivalents = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Marketable securities"))
                    currentAssets.MarketableSecurities = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Accounts receivable"))
                    currentAssets.AccountsReceivable = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Receivable under reverse repurchase agreements"))
                    currentAssets.ReceivableUnderReversePurchase = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Deferred income taxes, net"))
                     assets.DeferredIncomeTaxes = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Income taxes receivable, net"))
                     currentAssets.IncomeTaxesReceivable = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Prepaid revenue share, expenses and other assets") || tr.Text.Contains("Other current assets"))
                     currentAssets.OtherCurrentAssets = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Prepaid revenue share, expenses and other assets, non-current") || tr.Text.Contains("Other non-current assets"))
                     assets.OtherNoncurrentAssets = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Non-marketable investments"))
                     assets.NonmarketableInvestments = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Property and equipment, net"))
                     assets.NetProperty= GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Intangible assets, net"))
                     assets.NetIntangibleAssets = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Goodwill"))
                     assets.Goodwill = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Accounts payable"))
                     currentLiabilities.AccountsPayable = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Short-term debt"))
                     currentLiabilities.ShortTermDebt = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Accrued compensation and benefits"))
                     currentLiabilities.AccruedCompensationAndBenefits = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Accrued expenses and other current liabilities"))
                     currentLiabilities.AccruedExpensesAndOtherCurrentLiabilities = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Accrued revenue share"))
                     currentLiabilities.AccruedRevenueShare = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Securities lending payable"))
                     currentLiabilities.SecuritiesLendingPayable = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Deferred revenue"))
                     currentLiabilities.DeferredRevenue = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Income taxes payable, net"))
                     currentLiabilities.NetIncomeTaxesPayable = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Long-term debt"))
                     liabilities.LongTermDebt = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Deferred revenue, non-current"))
                     liabilities.DeferredRevenueNonCurrent = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Income taxes payable, non-current"))
                     liabilities.IncomeTaxesPayableNonCurrent = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Deferred income taxes, net, non-current"))
                     liabilities.DeferredIncomeTaxes = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Other long-term liabilities"))
                     liabilities.OtherLongTermLiabilities = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Convertible preferred stock"))
                     stockholdersEquity.ConvertiblePreferredStock = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Class A and Class B common stock, and Class C capital stock and additional paid-in capital"))
                     stockholdersEquity.CommonStockClassAandB = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Accumulated other comprehensive income"))
                     stockholdersEquity.AccumulatedOtherCompehensiveLoss = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Retained earnings"))
                     stockholdersEquity.RetainedEarnings = GetDoubleFromString(tcs[tableCellCount - 2].Text);
            }

            assets.CurrentAssets = currentAssets;
            liabilities.CurrentLiabilities = currentLiabilities;

            return new ConsolidatedBalanceSheet(assets, liabilities, stockholdersEquity);
        }

        private ConsolidatedStatementOfCashFlow GetCashFlow(IWebElement table)
        {
            var cashFlow = new ConsolidatedStatementOfCashFlow();
            var operating = new OperatingActivities();
            var investing = new InvestingActivities();
            var financing = new FinancingActivities();

            var tableRows = table.FindElements(By.TagName("tr"));

            foreach (var tr in tableRows)
            {
                var tableCellCount = tr.FindElements(By.TagName("td")).Count();
                var tcs = tr.FindElements(By.TagName("td"));

                if (tr.Text.Contains("Net income"))
                   operating.NetIncome = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Depreciation expense and impairment of property and equipment"))
                   operating.DepreciationOfProperty  = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Amortization and impairment of intangible assets"))
                    operating.AmortizationOfIntangibleAssets = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Stock-based compensation expense"))
                    operating.StockBasedCompensationExpense = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Excess tax benefits from stock-based award activities"))
                    operating.ExcessTaxBenefitsFromStockBasedAward = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Deferred income taxes"))
                    operating.DeferredIncomeTaxes = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Gain on equity interest"))
                    operating.GainOnEquityInterest = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("loss on marketable and non-marketable investments, net"))
                    operating.NetLossOnInvestments = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Other"))
                    operating.OtherAdjustments = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Accounts receivable"))
                    operating.AccountsReceivable = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Income taxes, net"))
                    operating.NetIncomeTaxes = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Prepaid revenue share, expenses and other assets"))
                     operating.OtherAssets = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Accounts payable"))
                     operating.AccountsPayable = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Accrued expenses and other liabilities"))
                     operating.AccruedExpensesAndLiabilities = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Accrued revenue share"))
                    operating.AccruedRevenueShare = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Deferred revenue"))
                    operating.DeferredRevenue = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Purchases of property and equipment"))
                    investing.PurchasesOfProperty = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Purchases of marketable securities"))
                    investing.PurchasesOfMarketableSecurities = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Maturities and sales of marketable securities"))
                    investing.MaturitiesAndSalesOfInvestments = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Purchases of non-marketable investments"))
                    investing.PurchasesOfMarketableSecurities = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Cash collateral related to securities lending"))
                    investing.CashCollateral = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("nvestments in reverse repurchase agreements"))
                    investing.InvestmentsInReverseRepurchaseAgreements = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Acquisitions, net of cash acquired, and purchases of intangibles and other assets"))
                     investing.AcquisitionsNet = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Net payments related to stock-based award activities"))
                    financing.NetPaymentsToStockBasedAwards = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Excess tax benefits from stock-based award activities"))
                    financing.ExcessTaxBenefitsFromStockBasedAwards = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Adjustment Payment to Class C capital stockholders"))
                    financing.AdjustmentPaymentToClassCStockHolders = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Proceeds from issuance of debt, net of costs"))
                    financing.NetProceedsFromIssuranceOfDebt = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Repayments of debt"))
                    financing.RepaymentsOfDebt = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                else if (tr.Text.Contains("Effect of exchange rate changes on cash and cash equivalents"))
                    cashFlow.EffectOfExchangeRateChangesOnCash = GetDoubleFromString(tcs[tableCellCount - 2].Text);
            }

            cashFlow.OperatingActivities = operating;
            cashFlow.InvestingActivities = investing;
            cashFlow.FinancingActivities = financing;

            return cashFlow;
        }

        private ConsolidatedStatementOfComprehensiveIncome GetComprehensiveIncome(IWebElement table)
        {
            var comprehensiveIncome = new ConsolidatedStatementOfComprehensiveIncome();
            var forSaleInvestments = new AvailableForSaleInvestments();
            var cashFlowHedges = new CashFlowHedges();

            var tableRows = table.FindElements(By.TagName("tr"));

            bool cashFlowHedgeUnrealizedGain = false;
            bool cashFlowHedgeLess = false;

            foreach (var tr in tableRows)
            {
                var tableCellCount = tr.FindElements(By.TagName("td")).Count();
                var tcs = tr.FindElements(By.TagName("td"));

                if (tr.Text.Contains("Net income"))
                {
                    comprehensiveIncome.NetIncome = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                }
                else if (tr.Text.Contains("Change in foreign currency translation adjustment"))
                {
                    comprehensiveIncome.ChangeInForeignCurrencyTranslationAdjustment = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                }
                else if (tr.Text.Contains("Change in net unrealized gains (losses)"))
                {
                    if (!cashFlowHedgeUnrealizedGain)
                    {
                        forSaleInvestments.ChangeInNetUnrealizedGains = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                        cashFlowHedgeUnrealizedGain = true;
                    }
                    else
                    {
                        cashFlowHedges.ChangeInNetUnrealizedGains = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                    }
                }
                else if (tr.Text.Contains("Less: reclassification adjustment for net (gains) losses included in net income"))
                {
                    if (!cashFlowHedgeLess)
                    {
                        forSaleInvestments.ReclassificationAdjustmentForNetGains = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                        cashFlowHedgeLess = true;
                    }
                    else
                    {
                        cashFlowHedges.ReclassificationAdjustmentForNetGains = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                    }
                }
                else if (tr.Text.Contains("Other comprehensive income (loss)"))
                {
                    comprehensiveIncome.OtherCompehensiveIncome = GetDoubleFromString(tcs[tableCellCount - 2].Text);
                }
            }

            comprehensiveIncome.AvailableForSaleInvestments = forSaleInvestments;
            comprehensiveIncome.CashFlowHedges = cashFlowHedges;

            return comprehensiveIncome;
        }

        private ConsolidatedStaementOfIncome GetIncome(IWebElement table)
        {
            throw new NotImplementedException();
        }

        private double GetDoubleFromString(string Submitted)
        {
            if (string.IsNullOrWhiteSpace(Submitted))
                return 0;

            string ret = "";
            bool isNegative = false;
            foreach (char c in Submitted)
            {
                if (char.IsDigit(c) || c == '.')
                    ret += c;
                else if (c == '(')
                    isNegative = true;

            }

            if (string.IsNullOrWhiteSpace(ret))
                return 0;

            return isNegative ? Convert.ToDouble(ret) * -1  : Convert.ToDouble(ret);

        }

        private ConsolidatedBalanceSheet balanceSheet;
        private ConsolidatedStatementOfCashFlow cashFlow;
        private ConsolidatedStatementOfComprehensiveIncome comprehensiveIncome;
        private ConsolidatedStaementOfIncome income;
    }


}

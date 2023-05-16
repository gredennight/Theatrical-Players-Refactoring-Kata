using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        private enum PerfomanceTypes
        {
            tragedy,
            comedy,
            history,
            pastoral
        }
        private PerfomanceTypes GetType(string type)
        {
            switch (type)
            {
                case "tragedy":
                    return PerfomanceTypes.tragedy;
                case "comedy":
                    return PerfomanceTypes.comedy;
                case "history":
                    return PerfomanceTypes.history;
                case "pastoral":
                    return PerfomanceTypes.pastoral;
                default:
                    throw new Exception("unknown type: " + type);
            }
        }
        private int TragedyCase(Performance perf, Play play)
        {
            int thisAmount = 40000;
            if (perf.Audience > 30)
            {
                thisAmount += 1000 * (perf.Audience - 30);
            }
            return thisAmount;
        }
        private int ComedyCase(Performance perf, Play play)
        {
            int thisAmount = 30000;
            if (perf.Audience > 20)
            {
                thisAmount += 10000 + 500 * (perf.Audience - 20);
            }
            thisAmount += 300 * perf.Audience;
            return thisAmount;
        }
        private int HistoryCase(Performance perf, Play play)
        {
            int thisAmount = 30000;
            if (perf.Audience > 20)
            {
                thisAmount += 10000 + 500 * (perf.Audience - 20);
            }
            thisAmount += 300 * perf.Audience;
            return thisAmount;
        }
        private int PastoralCase(Performance perf, Play play)
        {
            int thisAmount = 30000;
            if (perf.Audience > 20)
            {
                thisAmount += 10000 + 500 * (perf.Audience - 20);
            }
            thisAmount += 300 * perf.Audience;
            return thisAmount;
        }
        private int CalculateAmount(Performance perf, Play play)
        {
            switch (GetType(play.Type))
            {
                case PerfomanceTypes.tragedy:
                    return TragedyCase(perf, play);
                case PerfomanceTypes.comedy:
                    return ComedyCase(perf, play);
                case PerfomanceTypes.history:
                    return HistoryCase(perf, play);
                case PerfomanceTypes.pastoral:
                    return PastoralCase(perf, play);
            }
            return 0;
        }
        private int CalculateVolumeCredits(Performance perf, Play play)
        {
            // add volume credits
            int volumeCredits = Math.Max(perf.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
            return volumeCredits;

        }
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");
            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayID];
                var thisAmount = CalculateAmount(perf, play);
                volumeCredits += CalculateVolumeCredits(perf, play);
                // print line for this order
                result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
                totalAmount += thisAmount;
            }
            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            result += String.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }
    }
}

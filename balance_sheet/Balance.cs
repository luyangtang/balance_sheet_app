using System;


namespace balance_sheet
{
    public class Balance
    {

        //computed prop

        public string Date { get; set; } = "";
        public string Desc { get; set; } = "";
        public double Amount { get; set; } = 0;


        //constructor
        public Balance()
        {

        }


        public Balance(string date, string desc, double amount)
        {
            this.Date = date;
            this.Desc = desc;
            this.Amount = amount;

        }
    }
}

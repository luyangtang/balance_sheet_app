using System;
using SQLite;


namespace balance_sheet
{
    [Table("Balance")]
    public class Balance
    {

        //computed prop
        [Column("Id"), SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
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

        public Balance(int Id, string date, string desc, double amount)
        {
            this.Id = Id;
            this.Date = date;
            this.Desc = desc;
            this.Amount = amount;

        }
    }
}

using System;

using AppKit;
using Foundation;

namespace balance_sheet
{
    public partial class ViewController : NSViewController
    {



        //AddRecord button clicked
        private int numberOfTimesClicked = 0;
        partial void AddRecord(NSButton sender)
        {

            //throw new NotImplementedException();
            MyConsole.StringValue = string.Format("The button has been clicked {0} time{1}.", ++numberOfTimesClicked, (numberOfTimesClicked < 2) ? "" : "s");
        }


        //populate table
        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            //create data source
            var DataSource = new BalanceTableDataSource();
            DataSource.Balances.Add(new Balance("8-15", "car", -199));
            DataSource.Balances.Add(new Balance("8-16", "salary", 200));
            DataSource.Balances.Add(new Balance("8-17", "makeup", -9));

            //populate table
            BalanceTable.DataSource = DataSource;
            BalanceTable.Delegate = new BalanceTableDelegate(this, DataSource);
        }

        public void ReloadTable()
        {
            BalanceTable.ReloadData();
        }




        //constructor
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
            MyConsole.StringValue = "Button has not been clicked yet";
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }
    }
}

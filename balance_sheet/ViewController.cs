using System;
using System.IO;
using SQLite;
using AppKit;
using Foundation;
using CoreGraphics;

namespace balance_sheet
{
    public partial class ViewController : NSViewController
    {
        //db
        public static string DbName = "SQLitedb.db3";
        public static string DbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DbName);

        public void CreateDB(string dbPath)
        {
            try
            {
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<Balance>();
                db.CreateTable<InitialValue>();
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public string GetDbPath()
        {
            return DbPath;
        }


        //AddRecord button clicked
        private int numberOfTimesClicked = 0;
        partial void AddRecord(NSButton sender)
        {

            //Push data 
            Balance newBalance = new Balance(DateForm.StringValue, DescForm.StringValue, AmountForm.DoubleValue);
            var conn = new SQLite.SQLiteConnection(DbPath);
            conn.Insert(newBalance);
            PopulateTable();

            clearAll();
        }


        //Update initial
        partial void UpdateInitAmt(NSButton updateInitBtn)
        {
            Console.WriteLine(0000);
            ////Push data
            var conn = new SQLite.SQLiteConnection(DbPath);
            var initial = conn.Table<InitialValue>().OrderByDescending(x => x.id).First();
            var initialValue = initial.Value;
            var initialValueId = initial.id;

            // Wireup events
            updateInitBtn.Activated += (sender, e) => {
                // Get button and product
                var btn = sender as NSButton;

                // Configure alert
                var alert = new NSAlert()
                {
                    AlertStyle = NSAlertStyle.Informational,
                    InformativeText = $"可以修改你的初始Money",
                    MessageText = $"修改初始",
                };
                alert.AddButton("取消");
                alert.AddButton("确定");

                // initial value override
                var input = new NSTextField(new CGRect(0, 0, 300, 20));
                input.StringValue = String.Format("{0}",initialValue);
                alert.AccessoryView = input;
                alert.Layout();

                alert.BeginSheetForResponse(View.Window, (result) =>
                {
                    Console.WriteLine(result);
                    // Should we delete the requested row?
                    if (result == 1001)
                    {
                        // update init value
                        
                        conn.Update(new InitialValue(float.Parse(input.StringValue)));
                        Console.WriteLine(conn.Update(new InitialValue(float.Parse(input.StringValue), initialValueId)));
                        Console.WriteLine(conn.Table<InitialValue>().OrderByDescending(x => x.id).First().Value);
                        ReloadTable();
                    }
                });

            };

            
        }



        //clear the content and populate table
        private void clearAll()
        {
            // clear the input
            DateForm.DateValue = (NSDate)DateTime.Today;
            DescForm.StringValue = "";
            AmountForm.DoubleValue = 0;
        }

        private void PopulateTable()
        {
            //Connect to db
            CreateDB(DbPath);
            var conn = new SQLite.SQLiteConnection(DbPath);
            var cmd = new SQLite.SQLiteCommand(conn);
            var results = conn.Table<Balance>().Where(x => true);
            var DataSource = new BalanceTableDataSource();

            double totalBalance = 0;

            foreach (var r in results)
            {
                DataSource.Balances.Add(new Balance(r.Id, r.Date, r.Desc, r.Amount));
                totalBalance += r.Amount;
            }
                

            //populate table
            BalanceTable.DataSource = DataSource;
            BalanceTable.Delegate = new BalanceTableDelegate(this, DataSource);

            // populate initial
            double initialValue = 1000;
            try
            {
                initialValue = conn.Table<InitialValue>().OrderByDescending(x => x.id).First().Value;

            } catch (Exception e)
            {
                conn.Insert(new InitialValue(initialValue));
            }
            InitialLabel.StringValue = string.Format("(Init. amt: {0})", initialValue);


            //Populate total
            TotalLabel.StringValue = string.Format("结余: {0}", initialValue + totalBalance);

            
        }






        //populate table
        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            clearAll();
            PopulateTable();


        }
        public void ReloadTable()
        {
            BalanceTable.ReloadData();
            PopulateTable();
        }



        //constructor
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
            //MyConsole.StringValue = "Button has not been clicked yet";
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

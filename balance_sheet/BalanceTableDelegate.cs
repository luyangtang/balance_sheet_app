
using System;
using AppKit;
using CoreGraphics;
using Foundation;
using System.Collections;
using System.Collections.Generic;
using SQLite;

namespace balance_sheet
{
    public class BalanceTableDelegate : NSTableViewDelegate
    {
        private const string CellIdentifier = "BalanceCell";

        private BalanceTableDataSource DataSource;
        private ViewController Controller;



        //constructor
        public BalanceTableDelegate(ViewController controller, BalanceTableDataSource dataSource)
        {
            this.Controller = controller;
            this.DataSource = dataSource;
        }


        //override method
        public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            NSTextField view = (NSTextField)tableView.MakeView(CellIdentifier, this);
            if(view == null)
            {
                view = new NSTextField();
                view.Identifier = CellIdentifier;
                view.BackgroundColor = NSColor.Clear;
                view.Bordered = false;
                view.Selectable = false;
                view.Editable = false;
            }

            switch(tableColumn.Title)
            {
                case "Date":
                    view.StringValue = DataSource.Balances[(int)row].Date;
                    break;

                case "Description":
                    view.StringValue = DataSource.Balances[(int)row].Desc;
                    //view.Editable = true;
                    break;

                case "Amount":
                    view.DoubleValue = DataSource.Balances[(int)row].Amount;
                    //view.Editable = true;
                    break;

                case "Actions":

                    ////////////
                    // Delete //
                    ////////////


                    //create button
                    var deleteButton = new NSButton(new CGRect(0, 0, 35, 16));
                    deleteButton.SetButtonType(NSButtonType.MomentaryPushIn);
                    deleteButton.Title = "删除";
                    deleteButton.Tag = row;

                    // Wireup events
                    deleteButton.Activated += (sender, e) =>
                    {
                        DeleteOnClick(sender, e);
                    };

                    // Add to view
                    view.AddSubview(deleteButton);


                    //////////
                    // Edit //
                    //////////

                    ////create button
                    //var editButton = new NSButton(new CGRect(36, 0, 35, 16));
                    //editButton.SetButtonType(NSButtonType.MomentaryPushIn);
                    //editButton.Title = "修改";
                    //editButton.Tag = row;

                    //// Wireup events
                    //editButton.Activated += (sender, e) =>
                    //{
                        
                    //    Console.WriteLine("Desc: {0}",String.Format(tableView.GetCell(0, row).StringValue));
                    //    Console.WriteLine("Amt: {0}", String.Format(tableView.GetCell(1, row).StringValue));
                    //    Console.WriteLine(tableView.ColumnForView((NSView) sender));
                    //    EditOnClick(sender, e);
                    //};

                    //// Add to view
                    //view.AddSubview(editButton);

                    break;


            }


            return view;
        }





        // onclick event

        private void EditOnClick(object sender, EventArgs e)
        {
            // Get button and product
            var btn = sender as NSButton;
            var currentBalance = DataSource.Balances[(int)btn.Tag];
            //var newBalance = Balance(currentBalance.Id, currentBalance.)

            // Configure alert
            var alert = new NSAlert()
            {
                AlertStyle = NSAlertStyle.Informational,
                InformativeText = $"确认要修改吗？",
                MessageText = $"修改?",
            };
            alert.AddButton("取消");
            alert.AddButton("修改");
            alert.BeginSheetForResponse(Controller.View.Window, (result) =>
            {

                // Should we delete the requested row?
                if (result == 1001)
                {
                    // Remove the given row from the dataset

                    var conn = new SQLite.SQLiteConnection(Controller.GetDbPath());
                    conn.Update(DataSource.Balances[(int)btn.Tag]);

                    Controller.ReloadTable();
                }
            });
        }


        private void DeleteOnClick(object sender, EventArgs e)
        {
            // Get button and product
            var btn = sender as NSButton;
            var balance = DataSource.Balances[(int)btn.Tag];

            // Configure alert
            var alert = new NSAlert()
            {
                AlertStyle = NSAlertStyle.Informational,
                InformativeText = $"你真的要删掉我吗？>.<",
                MessageText = $"Delete?",
            };
            alert.AddButton("取消");
            alert.AddButton("删除");
            alert.BeginSheetForResponse(Controller.View.Window, (result) =>
            {

                // Should we delete the requested row?
                if (result == 1001)
                {
                    // Remove the given row from the dataset

                    var conn = new SQLite.SQLiteConnection(Controller.GetDbPath());
                    conn.Delete(DataSource.Balances[(int)btn.Tag]);

                    Controller.ReloadTable();
                }
            });
        }




    }



    
}

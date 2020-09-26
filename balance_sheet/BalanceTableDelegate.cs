
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
                    break;

                case "Amount":
                    view.DoubleValue = DataSource.Balances[(int)row].Amount;
                    break;

                case "Actions":
                    //create button
                    var deleteButton = new NSButton(new CGRect(0, 0, 81, 16));
                    deleteButton.SetButtonType(NSButtonType.MomentaryPushIn);
                    deleteButton.Title = "Delete";
                    deleteButton.Tag = row;

                    // Wireup events
                    deleteButton.Activated += (sender, e) => {
                        // Get button and product
                        var btn = sender as NSButton;
                        var balance = DataSource.Balances[(int)btn.Tag];

                        // Configure alert
                        var alert = new NSAlert()
                        {
                            AlertStyle = NSAlertStyle.Informational,
                            InformativeText = $"Are you sure you want to delete the record? This operation cannot be undone.",
                            MessageText = $"Delete?",
                        };
                        alert.AddButton("Cancel");
                        alert.AddButton("Delete");
                        alert.BeginSheetForResponse(Controller.View.Window, (result) =>
                        {

                            // Should we delete the requested row?
                            if (result == 1001)
                            {
                                // Remove the given row from the dataset
                                //DataSource.Balances.RemoveAt((int)btn.Tag);

                                
                                var conn = new SQLite.SQLiteConnection(Controller.GetDbPath());
                                //conn.Insert(newBalance);
                                conn.Delete(DataSource.Balances[(int)btn.Tag]);



                                Controller.ReloadTable();
                            }
                        });

                    };

                    // Add to view
                    view.AddSubview(deleteButton);
                    break;


            }


            return view;
        }
    }
}

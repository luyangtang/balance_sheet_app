using System;
using AppKit;
using CoreGraphics;
using Foundation;
using System.Collections;
using System.Collections.Generic;


namespace balance_sheet
{
    public class BalanceTableDataSource : NSTableViewDataSource
    {

        //public variables
        public List<Balance> Balances = new List<Balance>();

        //constructor
        public BalanceTableDataSource()
        {
        }

        //nrow of table = number of balances
        public override nint GetRowCount(NSTableView tableView)
        {
            return Balances.Count;
        }

    }
}


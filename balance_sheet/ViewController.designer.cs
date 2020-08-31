// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace balance_sheet
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTableColumn ActionsColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn AmountColumn { get; set; }

		[Outlet]
		AppKit.NSTextField AmountForm { get; set; }

		[Outlet]
		AppKit.NSTableView BalanceTable { get; set; }

		[Outlet]
		AppKit.NSTableColumn DateColumn { get; set; }

		[Outlet]
		AppKit.NSDatePicker DateForm { get; set; }

		[Outlet]
		AppKit.NSTableColumn DescColumn { get; set; }

		[Outlet]
		AppKit.NSTextField DescForm { get; set; }

		[Outlet]
		AppKit.NSTextField InitialLabel { get; set; }

		[Outlet]
		AppKit.NSTextField TotalLabel { get; set; }

		[Action ("addRecord:")]
		partial void addRecord (AppKit.NSButton sender);

		[Action ("AddRecord:")]
		partial void AddRecord (AppKit.NSButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ActionsColumn != null) {
				ActionsColumn.Dispose ();
				ActionsColumn = null;
			}

			if (AmountColumn != null) {
				AmountColumn.Dispose ();
				AmountColumn = null;
			}

			if (AmountForm != null) {
				AmountForm.Dispose ();
				AmountForm = null;
			}

			if (BalanceTable != null) {
				BalanceTable.Dispose ();
				BalanceTable = null;
			}

			if (DateColumn != null) {
				DateColumn.Dispose ();
				DateColumn = null;
			}

			if (DateForm != null) {
				DateForm.Dispose ();
				DateForm = null;
			}

			if (DescColumn != null) {
				DescColumn.Dispose ();
				DescColumn = null;
			}

			if (DescForm != null) {
				DescForm.Dispose ();
				DescForm = null;
			}

			if (TotalLabel != null) {
				TotalLabel.Dispose ();
				TotalLabel = null;
			}

			if (InitialLabel != null) {
				InitialLabel.Dispose ();
				InitialLabel = null;
			}
		}
	}
}

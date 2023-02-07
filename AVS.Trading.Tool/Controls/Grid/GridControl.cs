using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using AVS.CoreLib.WinForms.MVC;
using AVS.Poloniex.Utils;

namespace AVS.Poloniex.Controls.Grid
{
    public partial class GridControl : UserControl, IGridControl 
    {
        private static readonly object loadDataCompletedKey = new object();
        #region Prop-s

        [Browsable(false)]
        public IGridViewController Controller { get; set; }

        [Browsable(false)]
        public GridHightlighter Hightlighter { get; } = new GridHightlighter();
        
        /// <summary>
        /// requires StatusLabel to be initialized
        /// </summary>
        public string GridSummaryText
        {
            get => lblStatus.Text;
            set => lblStatus.Text = value;
        }

        [Browsable(false)]
        public DataGridView DataGrid => grid;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(ExtendedDataGridViewColumnCollectionEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public DataGridViewColumnCollection Columns => this.grid.Columns;

        [Browsable(false)]
        public object DataSource
        {
            get => this.bindingSource.DataSource;
            set => this.bindingSource.DataSource = value;
        }
        
        /// <summary>Occurs when the background operation has completed, has been canceled, or has raised an exception.</summary>
        [Category("Async")]
        [Description("GridControl_LoadDataCompleted")]
        public event RunWorkerCompletedEventHandler LoadDataCompleted
        {
            add => this.Events.AddHandler(GridControl.loadDataCompletedKey, (Delegate)value);
            remove => this.Events.RemoveHandler(GridControl.loadDataCompletedKey, (Delegate)value);
        }

        protected virtual void OnLoadDataCompleted(RunWorkerCompletedEventArgs e)
        {
            var handler = (RunWorkerCompletedEventHandler)this.Events[GridControl.loadDataCompletedKey];
            handler?.Invoke((object)this, e);
        }

        #endregion

        public GridControl()
        {
            InitializeComponent();
            GridSummaryText = string.Empty;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToOrderColumns = true;
            grid.AllowUserToResizeColumns = true;
        }

        public void RunLoadDataAsync(object argument)
        {
            GridSummaryText = "Loading data..";
            backgroundWorker.RunWorkerAsync(argument);
        }

        public void ReportProgress(string message, int percentage)
        {
            backgroundWorker.ReportProgress(percentage, message);
        }

        #region background worker
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var message = e.UserState as string;
            GridSummaryText = $"{message} Progress: {e.ProgressPercentage}%";
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //we can only load data here, data binding is impossible in background process
            var data = Controller.LoadData(e.Argument);
            e.Result = Controller.ToSortableBindingList(data);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                GridSummaryText = "Operation has been canceled";
            }
            else if (e.Error != null)
            {
                GridSummaryText = "Error occured: " + e.Error.Message;
            }
            else
            {
                GridSummaryText = "Binding data 80%";
                Controller.BindData(e.Result);
                GridSummaryText = "Highlighting data 95%";
                Hightlighter.Execute(grid);
                GridSummaryText = $"Row count {grid.RowCount}";
            }
            OnLoadDataCompleted(e);
        } 
        #endregion

        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            Controller.GridSelectionChanged();//grid
        }

        private void grid_Sorted(object sender, EventArgs e)
        {
            Hightlighter.Execute(grid);
        }
    }
}

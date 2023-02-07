using System;
using System.ComponentModel;
using System.Windows.Forms;
using AVS.CoreLib.WinForms;
using AVS.CoreLib.WinForms.Controls;
using AVS.CoreLib.WinForms.Grid;
using AVS.CoreLib._System.ComponentModel;
using AVS.CoreLib._System.Debug;
using AVS.Trading.Tool.Controls.Common;
using AVS.Trading.Tool.Controls.Extensions;
using AVS.Trading.Tool.Controls.TradingTools.Controllers;
using AVS.Trading.Tool.Forms.TradingTools;
using AVS.Trading.Data.Domain.TradingTools;

namespace AVS.Trading.Tool.Controls.TradingTools
{
    public interface IMyOpenOrdersView: IGridView
    {
        IFormView ParentView { get; }
    }

    public partial class MyOpenOrdersControl : UserControlEx, IMyOpenOrdersView
    {
        #region prop-s
        private ITradeHistoryFiltersView _view;
        public IGridControl GridControl => gridControl1;
        public ISummaryView SummaryView => null;
        public ISelectedCellsSummaryView SelectedCellsSummaryView => null;
        protected MyOpenOrdersController Controller;
        private ViewModeEnum _viewMode = ViewModeEnum.Normal;
        public ViewModeEnum ViewMode
        {
            get => _viewMode;
            set
            {
                _viewMode = value;
                SwitchMode(value);
            }
        }
        
        private void SwitchMode(ViewModeEnum value)
        {
            if (value == ViewModeEnum.Detailed)
            {
                NotesColumn.Visible = true;
                DateUtcColumn.Visible = true;
            }
            else
            {
                NotesColumn.Visible = false;
                DateUtcColumn.Visible = false;
            }
        }

        #endregion

        public void LoadDataAsync(ITradeHistoryFiltersView view)
        {
            if(view == null)
                throw new ArgumentNullException(nameof(view));
            contextMenuStrip1.Enabled = true;
            var filters = view.GetFilters();
            _view = view;

            if (string.IsNullOrEmpty(filters.Market) || filters.Market == "all")
            {
                OnLoadDataCompleted(null);
                return;
            }

            LoadOrdersAsync();
        }

        protected override void Initialize()
        {
            base.Initialize();
            InitializeComponent();
            Controller = GetController<MyOpenOrdersController>();
            gridControl1.Controller = Controller;
            InitializeGridHightlighter();
        }

        private void InitializeGridHightlighter()
        {
            gridControl1.Hightlighter.WithOrderTypeColorScheme(TypeColumn);
            gridControl1.Hightlighter.WithAccountTypeColorScheme(AccountColumn);
        }

        #region context menu
        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(@"Please confirm you would like to cancel the selected orders?",
                @"Confirm order(s) canceling",
                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Controller.CancelOrders(gridControl1);
                LoadOrdersAsync();
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var watch = DebugUtil.Stopwatch(LoadOrdersAsync);
        }

        private void newOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new MyOrdersForm();
            frm.Initialize(_view.GetFilters().Market);
            frm.Show();
        }

        private async void LoadOrdersAsync()
        {
            var response = await Controller.LoadDataAsync(_view.GetFilters());

            if(response.Success)
                gridControl1.BindData(new SortableBindingList<OpenOrder>(response.Data));
            else
                gridControl1.SetError(response.Error);

            OnLoadDataCompleted(null);
        }

        #endregion

        #region LoadDataCompleted event
        private static readonly object LoadDataCompletedKey = new object();

        [Category("Async")]
        [Description("Load open orders data completed")]
        public event RunWorkerCompletedEventHandler LoadDataCompleted
        {
            add => this.Events.AddHandler(LoadDataCompletedKey, (Delegate)value);
            remove => this.Events.RemoveHandler(LoadDataCompletedKey, (Delegate)value);
        }
        private void gridControl1_LoadDataCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnLoadDataCompleted(e);
        }
        private void OnLoadDataCompleted(RunWorkerCompletedEventArgs e)
        {
            var handler = (RunWorkerCompletedEventHandler)this.Events[LoadDataCompletedKey];
            handler?.Invoke((object)this, e);
        }
        #endregion

        
    }
}

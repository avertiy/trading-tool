using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AVS.Poloniex.Controls.Grid;
using AVS.Poloniex.Framework.Utils;

namespace AVS.Poloniex.Controls.Controllers
{
    public interface IGridViewController : IViewController
    {
        object LoadData(object argument);
        object ToSortableBindingList(object dataSource);
        void BindData(object dataSource);
        void GridSelectionChanged();
    }

    public abstract class GridViewController<TView, TEntity>: ControllerBase<TView>, IGridViewController
        where TView : class, IGridView
    {
        protected DataGridView DataGrid => View.GridControl.DataGrid;
        protected GridSelectionHelper GridSelectionHelper;

        public abstract object LoadData(object argument);

        public virtual object ToSortableBindingList(object dataSource)
        {
            if(dataSource is IList<TEntity> list)
                return new SortableBindingList<TEntity>(list);
            throw new ArgumentException($"DataSource is expected of type IList<{typeof(TEntity).Name}>");
        }

        public virtual void BindData(object source)
        {
            View.GridControl.DataSource = source;
            //View.GridControl.GridSummaryText = 
            if (View.SummaryView != null)
               View.SummaryView.DataSource = source;
        }

        public virtual void GridSelectionChanged()
        {
            View.GridControl.GridSummaryText = GridSelectionHelper.GetSelectedCellsSum(DataGrid);
        }

        public override void SetView(object view)
        {
            base.SetView(view);
            //we can't initialize GridSelectionHelper in c-tor due to View is set after the Controller c-tor is called
            GridSelectionHelper = new GridSelectionHelper(DataGrid.Columns);
        }

        public void DisplaySelectedCellsSummary()
        {
            View.SelectedCellsSummaryView?.Initialize(DataGrid.SelectedCells);
        }
    }
}
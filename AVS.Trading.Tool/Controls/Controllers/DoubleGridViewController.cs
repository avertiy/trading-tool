using System.Windows.Forms;
using AVS.Poloniex.Controls.Grid;

namespace AVS.Poloniex.Controls.Controllers
{
    public interface IDoubleGridViewController : IViewController
    {
        object Grid1LoadData(object argument);
        object Grid2LoadData(object argument);
        void Grid1BindData(object argument);
        void Grid2BindData(object argument);
        void Grid1SelectionChanged();
        void Grid2SelectionChanged();
    }

    //public class GridViewController<TView>: GridViewController<TView>

    public abstract class DoubleGridViewController<TView> : ControllerBase<TView>, IDoubleGridViewController
        where TView : class, IDoubleGridView
    {
        protected DataGridView DataGrid1 => View.Grid1.DataGrid;
        protected DataGridView DataGrid2 => View.Grid2.DataGrid;
        protected GridSelectionHelper GridSelectionHelper1;
        protected GridSelectionHelper GridSelectionHelper2;

        public abstract object Grid1LoadData(object argument);
        public abstract object Grid2LoadData(object argument);

        public virtual void Grid1BindData(object source)
        {
            View.Grid1

            View.Grid1.DataSource = source;
        }

        public virtual void Grid2BindData(object source)
        {
            View.Grid2.DataSource = source;
        }

        public virtual void Grid1SelectionChanged()
        {
            View.Grid1.GridSummaryText = GridSelectionHelper1.GetSelectedCellsSum(DataGrid1);
        }
        public virtual void Grid2SelectionChanged()
        {
            View.Grid2.GridSummaryText = GridSelectionHelper2.GetSelectedCellsSum(DataGrid2);
        }

        public override void SetView(object view)
        {
            base.SetView(view);
            //we can't initialize GridSelectionHelper in c-tor due to View is set after the Controller c-tor is called
            GridSelectionHelper1 = new GridSelectionHelper(DataGrid1.Columns);
            GridSelectionHelper2 = new GridSelectionHelper(DataGrid2.Columns);
        }
    }
}
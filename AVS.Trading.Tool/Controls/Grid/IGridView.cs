using System.Windows.Forms;

namespace AVS.Poloniex.Controls.Grid
{
    public interface IGridView
    {
        IGridControl GridControl { get; }
        ISummaryView SummaryView { get; }
        ISelectedCellsSummaryView SelectedCellsSummaryView { get; }
    }

    public interface IGridControl
    {
        object DataSource { get; set; }
        DataGridView DataGrid { get; }
        string GridSummaryText { get; set; }
        void ReportProgress(string message, int percentage);
    }

    public interface ISummaryView
    {
        object DataSource { get; set; }
    }

    public interface ISelectedCellsSummaryView
    {
        void Initialize(DataGridViewSelectedCellCollection selectedCells);
    }

    public interface IDoubleGridView
    {
        IGridControl Grid1 { get; }
        IGridControl Grid2 { get; }
    }
}
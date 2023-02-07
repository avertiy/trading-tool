using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using AVS.CoreLib.WinForms;

namespace AVS.Trading.Tool.Forms
{
    [ToolboxItemFilter("System.Windows.Forms.Control.TopLevel")]
    [Designer("System.Windows.Forms.Design.FormDocumentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
    [DesignerCategory("Form")]
    public class MyBaseForm : Form, IFormView
    {
        public string FormTitle
        {
            get => this.Text;
            set => this.Text = value;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steam_Desktop_Authenticator
{
    public class DoubleBufferedDataGridView : DataGridView
    {
        protected override bool DoubleBuffered { get => true; }
    }
}
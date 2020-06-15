using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework2
// The class was composed using answer by Ian:
//https://stackoverflow.com/questions/6677533/how-to-avoid-flickering-in-tablelayoutpanel-in-c-net
//
{
    public class DoubleBufferedTableLayoutPanel : TableLayoutPanel
    {
        public DoubleBufferedTableLayoutPanel(){
            DoubleBuffered = true;
        }

    }
}

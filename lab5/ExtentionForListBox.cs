using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtentionMethodsForListBox
{
    public static class ExtentionForListBox
    {
        public static void SelectBottomIndex(this ListBox listBox)
        {
            listBox.SelectedIndex = listBox.Items.Count - 1;
        }
    }
}

/*
CountyChecker report generator
© Steven J. Stover, 2025

This program is free software: you can redistribute it and/or modify it under the terms of the
GNU Affero General Public License as published by the Free Software Foundation, either version 3 
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even
the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General
Public License for more details.

You should have received a copy of the GNU Affero General Public License along with this program.
If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountyChecker
{
    internal class ListBoxEx : ListBox
    {
        // Custom event that fires when the number of selected items changes
        public event EventHandler<SelectionCountChangedEventArgs> ?SelectionCountChanged;

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            // Always call the base method to ensure standard ListBox behavior
            base.OnSelectedIndexChanged(e);

            // Raise our custom event
            OnSelectionCountChanged(new SelectionCountChangedEventArgs(this.SelectedItems.Count));
        }

        protected virtual void OnSelectionCountChanged(SelectionCountChangedEventArgs e)
        {
            SelectionCountChanged?.Invoke(this, e);
        }
    }

    // Event arguments class to pass the count data
    public class SelectionCountChangedEventArgs(int count) : EventArgs
    {
        public int SelectedCount { get; } = count;
    }
}

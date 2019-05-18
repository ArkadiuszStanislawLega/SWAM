using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Events.NavigationButton
{
    public class ButtonSelectedArgs : EventArgs
    {
        /// <summary>
        /// Flags indicating if the button is pressed.
        /// </summary>
        private bool _isSelected;

        public ButtonSelectedArgs(bool isSelected)
        {
            this._isSelected = isSelected;
        }

        public bool IsSelected { get => _isSelected; }
    }
}

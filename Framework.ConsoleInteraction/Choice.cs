using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.ConsoleInteraction
{
    /// <summary>
    /// Represents a single user choice item and encapsulates the text for the item and the action to invoke when the choice is selected.
    /// </summary>
    public class Choice
    {
        private Action<string> _action;

        private string _displayText;

        /// <summary>
        /// Invokes the action assigned to the choice.
        /// </summary>
        public void Invoke()
        {
            _action(_displayText);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Choice"/> class.
        /// </summary>
        /// <param name="displayText">The text to display to the user for this choice.</param>
        /// <param name="action">The delegate to invoke when the choice is selected by the user.</param>
        public Choice(string displayText, Action<string> action)
        {
            _action = action;
            _displayText = displayText;
        }

        /// <summary>
        /// Gets the text displayed to the user for this choice.
        /// </summary>
        /// <value>The test to display.</value>
        public string DisplayText
        {
            get { return _displayText; }
        }

    }
}

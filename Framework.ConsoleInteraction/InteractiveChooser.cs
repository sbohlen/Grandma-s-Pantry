using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.ConsoleInteraction
{
    public class InteractiveChooser
    {
        private ICollection<Choice> _choices = new List<Choice>();

        private string _promptMessage;


        /// <summary>
        /// Initializes a new instance of the <see cref="InteractiveChooser"/> class.
        /// </summary>
        /// <param name="promptMessage">The prompt message to display.</param>
        public InteractiveChooser(string promptMessage)
        {
            _promptMessage = promptMessage;

        }


        /// <summary>
        /// Adds a choice to the list of available choices.
        /// </summary>
        /// <param name="choice">The choice.</param>
        public void AddChoice(Choice choice)
        {
            _choices.Add(choice);
        }


        /// <summary>
        /// Assign a collection of choices to display.
        /// </summary>
        /// <param name="choices">The collection of choices.</param>
        public void SetChoices(ICollection<Choice> choices)
        {
            _choices = choices;
        }


        /// <summary>
        /// Prompt the user to select a single choice from the collection of available choices.
        /// </summary>
        public void AskForChoice()
        {
            DisplayChoices();

            while (true)
            {
                string response = Console.ReadLine();

                if (response == "0")
                    return;

                int numericResponse;

                if (int.TryParse(response, out numericResponse))
                {
                    if (_choices.Count >= numericResponse)
                    {
                        InvokeSelectedAction(numericResponse);
                        return;
                    }
                }

                Console.WriteLine("Invalid selection, try again.");
            }

        }

        private void DisplayChoices()
        {
            _choices.Add(new Choice("Quit", Quit));

            Console.WriteLine("\n\n{0}:", _promptMessage);

            Console.WriteLine("0. Main Menu");

            int choiceCount = 1;

            foreach (Choice choice in _choices)
            {
                Console.WriteLine("{0}. {1}", choiceCount, choice.DisplayText);
                choiceCount++;
            }

            Console.WriteLine("-----------------\n");
        }

        private void Quit(string response)
        {
            Environment.Exit(0);
        }

        private void InvokeSelectedAction(int response)
        {
            Choice choice = _choices.ElementAt(response - 1);
            choice.Invoke();
        }

    }
}

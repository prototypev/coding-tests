namespace SystemDependencies
{
    using System;
    using System.Linq;

    /// <summary>
    /// Console application program for testing system dependencies.
    /// </summary>
    public static class Program
    {
        #region Static Fields

        /// <summary>
        /// The dependency resolver.
        /// </summary>
        private static readonly DependencyResolver DependencyResolver = new DependencyResolver();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Main program entry point.
        /// </summary>
        /// <param name="args">
        /// The arguments.
        /// </param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter inputs:");

            while (true)
            {
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    // Trim whitespaces
                    string[] inputs = input.Trim()
                                           .Split(' ');
                    for (int i = 0; i < inputs.Length; i++)
                    {
                        inputs[i] = inputs[i].Trim();
                    }

                    if (Command.END.Equals(inputs[0], StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }

                    bool isValid = ParseInputs(inputs);
                    if (!isValid)
                    {
                        Console.WriteLine("Unrecognized command: {0}", input);
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses the inputs.
        /// </summary>
        /// <param name="inputs">
        /// The inputs.
        /// </param>
        /// <returns>
        /// <c>true</c> if inputs are valid; otherwise <c>false</c>.
        /// </returns>
        private static bool ParseInputs(string[] inputs)
        {
            // TODO: Due to time constraints, not doing much input validation, as that's not the key part of this test
            bool isValid = true;

            string command = inputs[0];

            if (Command.DEPEND.Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                string componentId = inputs[1];
                string[] dependencyIds = inputs.Skip(2)
                                               .ToArray();

                DependencyResolver.AddComponentDefinition(componentId, dependencyIds);
            }
            else if (Command.INSTALL.Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                string componentId = inputs[1];

                DependencyResolver.InstallComponent(componentId, true);
            }
            else if (Command.REMOVE.Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                string componentId = inputs[1];

                DependencyResolver.RemoveComponent(componentId);
            }
            else if (Command.LIST.Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                foreach (Component component in DependencyResolver.InstalledComponents)
                {
                    Console.WriteLine("\t{0}", component.Id);
                }
            }
            else
            {
                isValid = false;
            }

            return isValid;
        }

        #endregion
    }
}
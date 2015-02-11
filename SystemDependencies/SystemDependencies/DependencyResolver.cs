namespace SystemDependencies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The dependency resolver.
    /// TODO: No circular dependency checking in place yet.
    /// </summary>
    public class DependencyResolver
    {
        #region Fields

        /// <summary>
        /// The components that are available to be installed.
        /// </summary>
        private readonly ISet<Component> _components = new HashSet<Component>();

        /// <summary>
        /// Backing field for InstalledComponents.
        /// </summary>
        private readonly ISet<Component> _installedComponents = new HashSet<Component>();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the installed components.
        /// </summary>
        /// <value>
        /// The installed components.
        /// </value>
        public IEnumerable<Component> InstalledComponents
        {
            get
            {
                return this._installedComponents;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Adds the component definition.
        /// </summary>
        /// <param name="componentId">
        /// The component identifier.
        /// </param>
        /// <param name="dependencyIds">
        /// The dependency ids.
        /// </param>
        /// <returns>
        /// The component.
        /// </returns>
        public Component AddComponentDefinition(string componentId, params string[] dependencyIds)
        {
            Component component = this._components.SingleOrDefault(c => c.Id.Equals(componentId, StringComparison.Ordinal));
            if (component == null)
            {
                // Component not yet defined, so create a new component definition
                component = new Component(componentId);
                this._components.Add(component);
            }

            foreach (string dependencyId in dependencyIds)
            {
                component.AddDependency(dependencyId);

                // Check if the dependency has already been defined, if not define them
                Component dependency = this._components.SingleOrDefault(c => c.Id.Equals(dependencyId, StringComparison.Ordinal));
                if (dependency == null)
                {
                    dependency = new Component(dependencyId);
                    this._components.Add(dependency);

                    // We don't know the dependencies of this depedency component for now, so just leave it
                }
            }

            return component;
        }

        /// <summary>
        /// Installs the component.
        /// </summary>
        /// <param name="componentId">
        /// The component identifier.
        /// </param>
        /// <param name="isUserInstalled">
        /// if set to <c>true</c>, indicates that the component is user installed.
        /// </param>
        public void InstallComponent(string componentId, bool isUserInstalled)
        {
            Component component = this._components.SingleOrDefault(c => c.Id.Equals(componentId, StringComparison.Ordinal));
            if (component == null)
            {
                // Automatically add component definition if not defined yet
                component = this.AddComponentDefinition(componentId);
            }

            if (this._installedComponents.Contains(component))
            {
                if (isUserInstalled)
                {
                    // Provide feedback only if the component is installed by the user.
                    // No feedback needed for automatic dependency installation.
                    Console.WriteLine("\t{0} is already installed.", componentId);
                }
            }
            else
            {
                // First install dependencies
                foreach (string dependencyId in component.DependsOn)
                {
                    this.InstallComponent(dependencyId, false);
                }

                // Then install the actual component
                component.IsUserInstalled = isUserInstalled;

                Console.WriteLine("\tInstalling {0}", componentId);
                this._installedComponents.Add(component);
            }
        }

        /// <summary>
        /// Removes the component.
        /// </summary>
        /// <param name="componentId">
        /// The component identifier.
        /// </param>
        public void RemoveComponent(string componentId)
        {
            Component componentToRemove = this._installedComponents.SingleOrDefault(c => c.Id.Equals(componentId, StringComparison.Ordinal));
            if (componentToRemove == null)
            {
                Console.WriteLine("\t{0} is not installed.", componentId);
            }
            else
            {
                if (!this.CanRemoveComponent(componentId))
                {
                    Console.WriteLine("\t{0} is still needed.", componentId);
                }
                else
                {
                    Console.WriteLine("\tRemoving {0}", componentId);
                    this._installedComponents.Remove(componentToRemove);

                    // Attempt to remove non-user installed dependencies
                    foreach (string dependencyId in componentToRemove.DependsOn)
                    {
                        if (this.CanRemoveComponent(dependencyId))
                        {
                            this.RemoveComponent(dependencyId);
                        }
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether the specified component can be removed.
        /// </summary>
        /// <param name="componentId">
        /// The component identifier.
        /// </param>
        /// <returns>
        /// <c>true</c> if the component can be removed; otherwise <c>false</c>.
        /// </returns>
        private bool CanRemoveComponent(string componentId)
        {
            // Check if any of the installed components depends on the component that is intended to be removed
            foreach (Component component in this._installedComponents)
            {
                // Ignore component to be removed (for obvious reasons)
                if (!component.Id.Equals(componentId, StringComparison.Ordinal))
                {
                    if (component.HasDependency(componentId))
                    {
                        // An installed components depends on the component that is intended to be removed, so we reject the removal
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion
    }
}
namespace SystemDependencies
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Definition for a component that potentially has dependencies.
    /// </summary>
    public class Component
    {
        #region Fields

        /// <summary>
        /// Backing field for DependsOn.
        /// </summary>
        private readonly ISet<string> _dependsOn = new HashSet<string>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Component"/> class.
        /// </summary>
        /// <param name="id">
        /// The identifier.
        /// </param>
        public Component(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the Ids of the components this component depends on.
        /// </summary>
        /// <value>
        /// The Ids of the dependencies.
        /// </value>
        public IEnumerable<string> DependsOn
        {
            get
            {
                return this._dependsOn;
            }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is user installed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is user installed; otherwise, <c>false</c>.
        /// </value>
        public bool IsUserInstalled { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Adds the dependency.
        /// </summary>
        /// <param name="dependencyId">
        /// The dependency identifier.
        /// </param>
        /// <returns>
        /// <c>true</c> if the dependency was successfully added; otherwise <c>false</c>.
        /// </returns>
        public bool AddDependency(string dependencyId)
        {
            return this._dependsOn.Add(dependencyId);
        }

        /// <summary>
        /// Determines whether the specified object is equal to this instance.
        /// </summary>
        /// <param name="obj">
        /// The object to compare with this instance.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            Component component = obj as Component;
            return component != null && this.Id.Equals(component.Id, StringComparison.Ordinal);
        }

        /// <summary>
        /// Determines whether this component has the specified dependency.
        /// </summary>
        /// <param name="dependencyId">
        /// The dependency identifier.
        /// </param>
        /// <returns>
        /// <c>true</c> if dependency exists; otherwise <c>false</c>.
        /// </returns>
        public bool HasDependency(string dependencyId)
        {
            return this._dependsOn.Contains(dependencyId);
        }

        #endregion
    }
}
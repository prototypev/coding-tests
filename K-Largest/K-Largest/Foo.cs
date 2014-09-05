namespace K_Largest
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Base class definition for a collection of comparable objects for which special operations can be performed.
    /// </summary>
    /// <typeparam name="T">
    /// The object type.
    /// </typeparam>
    public abstract class Foo<T>
        where T : IComparable<T>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Foo{T}"/> class.
        /// </summary>
        /// <param name="k">
        /// The value indicating the number of largest values, ordered from largest to smallest.
        /// </param>
        protected Foo(int k)
        {
            this.K = k;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value indicating the number of largest values, ordered from largest to smallest.
        /// </summary>
        /// <value>
        /// The value indicating the number of largest values, ordered from largest to smallest.
        /// </value>
        protected int K { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the top k elements, order from largest to smallest.
        /// </summary>
        /// <returns>The top k elements, order from largest to smallest.</returns>
        public abstract List<T> GetTopK();

        /// <summary>
        /// Offers the specified item.
        /// </summary>
        /// <param name="item">
        /// The item to be offered.
        /// </param>
        public abstract void Offer(T item);

        #endregion
    }
}
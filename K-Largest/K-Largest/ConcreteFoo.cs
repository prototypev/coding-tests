namespace K_Largest
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A concrete implementation of Foo.
    /// </summary>
    /// <typeparam name="T">
    /// The object type.
    /// </typeparam>
    public class ConcreteFoo<T> : Foo<T>
        where T : IComparable<T>
    {
        #region Fields

        /// <summary>
        /// The list containing the top K items, ordered from largest to smallest.
        /// </summary>
        private readonly List<T> _list;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteFoo{T}"/> class. 
        /// </summary>
        /// <param name="k">
        /// The value indicating the number of largest values, ordered from largest to smallest.
        /// </param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// k;k needs to be non-negative.
        /// </exception>
        public ConcreteFoo(int k)
            : base(k)
        {
            if (k < 0)
            {
                throw new ArgumentOutOfRangeException("k", "k needs to be non-negative.");
            }

            this._list = new List<T>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the top k elements, order from largest to smallest.
        /// </summary>
        /// <returns>The top k elements, order from largest to smallest.</returns>
        public override List<T> GetTopK()
        {
            return this._list;
        }

        /// <summary>
        /// Offers the specified item.
        /// </summary>
        /// <param name="item">
        /// The item to be offered.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// item;null values cannot be offered.
        /// </exception>
        public override void Offer(T item)
        {
            if (ReferenceEquals(item, null))
            {
                throw new ArgumentNullException("item", "null values cannot be offered.");
            }

            int insertIndex = 0;
            for (; insertIndex < this._list.Count; insertIndex++)
            {
                if (item.CompareTo(this._list[insertIndex]) > 0)
                {
                    // Found an item in the list that is less than the given item
                    break;
                }
            }

            if (insertIndex < this.K)
            {
                // Only bother inserting if there is a valid index
                this._list.Insert(insertIndex, item);

                if (this._list.Count > this.K)
                {
                    this._list.RemoveAt(this._list.Count - 1);
                }
            }
        }

        #endregion
    }
}
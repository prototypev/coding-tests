// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionHelper.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace WingtipToys.Core.Infrastructure.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Linq.Expressions;

    /// <summary>
    ///     A utility class to help with reflection based operations.
    /// </summary>
    public static class ReflectionHelper
    {
        #region Public Methods and Operators

        /// <summary>
        /// Determines whether the type to check is a subclass of the specified raw generic type.
        /// From: http://stackoverflow.com/questions/457676/check-if-a-class-is-derived-from-a-generic-class
        /// </summary>
        /// <param name="toCheck">The type to check.</param>
        /// <param name="generic">The generic type.</param>
        /// <returns>
        ///   <c>true</c> if the type is a subclass; otherwise, <c>flase</c>.
        /// </returns>
        public static bool IsSubclassOfRawGeneric(this Type toCheck, Type generic)
        {
            Contract.Requires(toCheck != null);
            Contract.Requires(generic != null);

            while (toCheck != null && toCheck != typeof(object))
            {
                Type cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }

                toCheck = toCheck.BaseType;
            }

            return false;
        }

        /// <summary>
        /// An extension method on an actual object to get its member names.
        /// </summary>
        /// <typeparam name="TObj">The object type.</typeparam>
        /// <typeparam name="TMember">The member type.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="separator">The separator character. If none is specified, default to '.'</param>
        /// <returns>
        /// The member name.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Expression must be of member access</exception>
        public static string GetMemberName<TObj, TMember>(this TObj obj, Expression<Func<TObj, TMember>> expression, char separator = '.')
        {
            return MembersOf<TObj>.GetName(expression, separator);
        }

        #endregion

        /// <summary>
        /// If we don't have an actual object we can still refer to its type with this generic static class
        /// </summary>
        /// <typeparam name="TObj">
        /// The object type.
        /// </typeparam>
        public static class MembersOf<TObj>
        {
            #region Public Methods and Operators

            /// <summary>
            /// Gets the name of a member.
            /// </summary>
            /// <typeparam name="TMember">The member type.</typeparam>
            /// <param name="expression">The expression.</param>
            /// <param name="separator">The separator character. If none is specified, default to '.'</param>
            /// <returns>
            /// The member name.
            /// </returns>
            /// <exception cref="System.InvalidOperationException">Expression must be of member access</exception>
            public static string GetName<TMember>(Expression<Func<TObj, TMember>> expression, char separator = '.')
            {
                Contract.Requires(expression != null);

                var stack = new Stack<string>();

                MemberExpression memberExpression;
                switch (expression.Body.NodeType)
                {
                    case ExpressionType.Convert:
                    case ExpressionType.ConvertChecked:
                        var unaryExpression = expression.Body as UnaryExpression;
                        memberExpression = unaryExpression == null ? null : unaryExpression.Operand as MemberExpression;
                        break;
                    default:
                        memberExpression = expression.Body as MemberExpression;
                        break;
                }

                while (memberExpression != null)
                {
                    stack.Push(memberExpression.Member.Name);
                    memberExpression = memberExpression.Expression as MemberExpression;
                }

                return string.Join(separator.ToString(CultureInfo.InvariantCulture), stack.ToArray());
            }

            #endregion
        }
    }
}
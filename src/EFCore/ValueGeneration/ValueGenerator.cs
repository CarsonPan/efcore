// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Microsoft.EntityFrameworkCore.ValueGeneration
{
    /// <summary>
    ///     Generates values for properties when an entity is added to a context.
    /// </summary>
    public abstract class ValueGenerator
    {
        /// <summary>
        ///     Gets a value to be assigned to a property.
        /// </summary>
        /// <para>The change tracking entry of the entity for which the value is being generated.</para>
        /// <returns> The value to be assigned to a property. </returns>
        public virtual object Next([NotNull] EntityEntry entry)
            => NextValue(entry);

        /// <summary>
        ///     Template method to be overridden by implementations to perform value generation.
        /// </summary>
        /// <para>The change tracking entry of the entity for which the value is being generated.</para>
        /// <returns> The generated value. </returns>
        protected abstract object NextValue([NotNull] EntityEntry entry);

        /// <summary>
        ///     Gets a value to be assigned to a property.
        /// </summary>
        /// <para>The change tracking entry of the entity for which the value is being generated.</para>
        /// <returns> The value to be assigned to a property. </returns>
        public virtual ValueTask<object> NextAsync(
            [NotNull] EntityEntry entry,
            CancellationToken cancellationToken = default)
            => NextValueAsync(entry, cancellationToken);

        /// <summary>
        ///     Template method to be overridden by implementations to perform value generation.
        /// </summary>
        /// <para>The change tracking entry of the entity for which the value is being generated.</para>
        /// <returns> The generated value. </returns>
        protected virtual ValueTask<object> NextValueAsync(
            [NotNull] EntityEntry entry,
            CancellationToken cancellationToken = default)
            => new ValueTask<object>(NextValue(entry));

        /// <summary>
        ///     <para>
        ///         Gets a value indicating whether the values generated are temporary (i.e they should be replaced
        ///         by database generated values when the entity is saved) or are permanent (i.e. the generated values
        ///         should be saved to the database).
        ///     </para>
        ///     <para>
        ///         An example of temporary value generation is generating negative numbers for an integer primary key
        ///         that are then replaced by positive numbers generated by the database when the entity is saved. An
        ///         example of permanent value generation are client-generated values for a <see cref="Guid" /> primary
        ///         key which are saved to the database.
        ///     </para>
        /// </summary>
        public abstract bool GeneratesTemporaryValues { get; }
    }
}

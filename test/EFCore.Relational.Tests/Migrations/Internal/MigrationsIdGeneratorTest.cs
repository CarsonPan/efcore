// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Migrations.Internal
{
    public class MigrationsIdGeneratorTest
    {
        [ConditionalFact]
        public void CreateId_works()
        {
            var id = new MigrationsIdGenerator().GenerateId("Twilight");

            Assert.Matches("[0-9]{14}_Twilight", id);
        }

        [ConditionalFact]
        public void CreateId_always_increments_timestamp()
        {
            var generator = new MigrationsIdGenerator();

            var id1 = generator.GenerateId("Rainbow");
            var id2 = generator.GenerateId("Rainbow");

            Assert.NotEqual(id1, id2);
        }

        [ConditionalFact]
        [UseCulture("fa")]
        public void CreateId_uses_invariant_calendar()
        {
            var invariantYear = CultureInfo.InvariantCulture.Calendar.GetYear(DateTime.Today).ToString();

            var id = new MigrationsIdGenerator().GenerateId("Zecora");

            Assert.StartsWith(invariantYear, id);
        }

        [ConditionalFact]
        public void GetName_works()
        {
            var name = new MigrationsIdGenerator().GetName("20150302100620_Apple");

            Assert.Equal("Apple", name);
        }

        [ConditionalFact]
        public void IsValidId_returns_true_when_valid()
        {
            var valid = new MigrationsIdGenerator().IsValidId("20150302100930_Rarity");

            Assert.True(valid);
        }

        [ConditionalFact]
        public void IsValidId_returns_false_when_invalid()
        {
            var valid = new MigrationsIdGenerator().IsValidId("Rarity");

            Assert.False(valid);
        }
    }
}

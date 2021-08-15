using System;
using ClientBarometer.Domain.Constants;
using ClientBarometer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ClientBarometer.DataAccess.Maps
{
    public static class BarometerEntriesMap
    {
        public static ModelBuilder Build<T>(this ModelBuilder modelBuilder) where T: BarometerEntry
        {
            var entityBuilder = modelBuilder.Entity<T>();

            // Main
            entityBuilder.ToTable("barometer_values");

            // Indexes
            entityBuilder.HasIndex(_ => _.Id);

            // Values
            entityBuilder.Property(_ => _.Id)
                .ValueGeneratedOnAdd()
                .HasValueGenerator<GuidValueGenerator>();

            entityBuilder.Property(_ => _.RowVersion)
                .IsRowVersion();
            
            return modelBuilder;
        }
    }
}

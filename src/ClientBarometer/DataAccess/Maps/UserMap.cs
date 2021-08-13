using System;
using ClientBarometer.Domain.Constants;
using ClientBarometer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ClientBarometer.DataAccess.Maps
{
    public static class UserMap
    {
        public static ModelBuilder Build<T>(this ModelBuilder modelBuilder) where T: User
        {
            var entityBuilder = modelBuilder.Entity<T>();

            // Main
            entityBuilder.ToTable("users");

            // Indexes
            entityBuilder.HasIndex(_ => _.Id);
            entityBuilder.HasIndex(_ => _.SourceId);

            // Values
            entityBuilder.Property(_ => _.Id)
                .ValueGeneratedOnAdd()
                .HasValueGenerator<GuidValueGenerator>();

            entityBuilder.Property(_ => _.RowVersion)
                .IsRowVersion();
            
            entityBuilder.HasData(new User
            {
                Id = ChatConsts.DEFAULT_USER_ID,
                SourceId = ChatConsts.DEFAULT_USER_SOURCE_ID.ToString(),
                Source = "Telegram",
                Name = "Admin",
                Birthday = DateTime.Parse("01.01.1990")
            });

            return modelBuilder;
        }
    }
}

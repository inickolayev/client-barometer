using ClientBarometer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ClientBarometer.DataAccess.Maps
{
    public static class MessageMap
    {
        public static ModelBuilder Build<T>(this ModelBuilder modelBuilder) where T: Message
        {
            var entityBuilder = modelBuilder.Entity<T>();

            // Main
            entityBuilder.ToTable("messages");

            // Indexes
            entityBuilder.HasIndex(_ => _.Id);
            entityBuilder.HasIndex(_ => _.ChatId);
            entityBuilder.HasIndex(_ => _.UserId);

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

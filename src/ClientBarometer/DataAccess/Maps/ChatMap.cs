using ClientBarometer.Domain.Constants;
using ClientBarometer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ClientBarometer.DataAccess.Maps
{
    public static class ChatMap
    {
        public static ModelBuilder Build<T>(this ModelBuilder modelBuilder) where T: Chat
        {
            var entityBuilder = modelBuilder.Entity<T>();

            // Main
            entityBuilder.ToTable("chats");

            // Indexes
            entityBuilder.HasIndex(_ => _.Id);
            entityBuilder.HasIndex(_ => _.SourceId);

            // Values
            entityBuilder.Property(_ => _.Id)
                .ValueGeneratedOnAdd()
                .HasValueGenerator<GuidValueGenerator>();

            entityBuilder.Property(_ => _.RowVersion)
                .IsRowVersion();

            entityBuilder.HasData(new Chat
            {
                Id = ChatConsts.DEFAULT_CHAT_ID,
                SourceId = ChatConsts.DEFAULT_CHAT_SOURCE_ID.ToString(),
                Source = "Telegram"
            });

            return modelBuilder;
        }
    }
}

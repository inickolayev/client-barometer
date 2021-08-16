using System;

namespace ClientBarometer.Domain.Constants
{
    public class ChatConsts
    {
        public const int MESSAGES_TAKE_DEFAULT = 20;
        public static readonly Guid DEFAULT_USER_ID = Guid.Parse("70313da4-aa68-41d3-bf80-265e2743846f");
        public static readonly Guid DEFAULT_CHAT_ID = Guid.Parse("8c43a70b-163c-4457-8cfa-42242bced8fa");
        public const string DEFAULT_CHAT_SOURCE_ID = "352328891";
        public const string DEFAULT_USER_SOURCE_ID = "Admin";

        public const string TELEGRAM_SOURCE = "Telegram";
    }
}
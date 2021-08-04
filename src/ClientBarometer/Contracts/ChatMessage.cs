using System;

namespace ClientBarometer.Contracts
{
    public class ChatMessage
    {
        public string Text { get; set; }
        public string Username { get; set; }
        public string RoomId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Id { get; set; }
    }
}
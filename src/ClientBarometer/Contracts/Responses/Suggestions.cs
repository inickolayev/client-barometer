using System;
using System.ComponentModel.DataAnnotations;

namespace ClientBarometer.Contracts.Responses
{
    public class Suggestions
    {
        public string[] Messages { get; set; }
    }
}
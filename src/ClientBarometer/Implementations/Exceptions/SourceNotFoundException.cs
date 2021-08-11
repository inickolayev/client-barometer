using System;
using AutoMapper;
using ClientBarometer.Common.Abstractions.Mappers;

namespace ClientBarometer.Implementations.Exceptions
{
    public class SourceNotFoundException : Exception
    {
        public string Source { get; }

        public SourceNotFoundException(string source, string message = "")
            : base(message)
        {
            Source = source;
        }
    }
}

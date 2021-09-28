using System;

namespace Fedora.API
{
    public class PluginInfo : Attribute
    {
        public string Name = "Unknown";
        public string Author = "Unknown";
        public string Description = "Unknown";
        public string Version = "Unknown";
    }
}
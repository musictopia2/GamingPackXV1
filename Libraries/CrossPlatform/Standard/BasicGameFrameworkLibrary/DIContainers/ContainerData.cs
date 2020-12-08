using System;
namespace BasicGameFrameworkLibrary.DIContainers
{
    internal class ContainerData
    {
        public Type? TypeIn { get; set; }
        public Type? TypeOut { get; set; }
        public object? ThisObject { get; set; }
        public bool IsSingle { get; set; }
        public string Tag { get; set; } = "";
        public Func<object>? GetNewObject { get; set; } //i think this is still needed.   
    }
}
using System.Collections.Generic;
using Pipeline;

namespace LevelGenerator
{
    public interface IPipelineObjectsProvider
    {
        Hamster Hamster { get; }
        HamsterHouse HamsterHouse { get; set; }
        Bowl Bowl { get; set; }
        List<Pipe> Pipes { get; }
    }
}
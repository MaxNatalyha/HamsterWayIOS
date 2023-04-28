using System.Collections.Generic;
using Pipeline;

namespace LevelGenerator
{
    public class PipelineObjectsProvider : IPipelineObjectsProvider
    {
        public Hamster Hamster => HamsterHouse.Hamster;
        public HamsterHouse HamsterHouse { get; set; }
        public Bowl Bowl { get; set; }
        public List<Pipe> Pipes { get; }

        public PipelineObjectsProvider()
        {
            Pipes = new List<Pipe>();
        }
    }
}
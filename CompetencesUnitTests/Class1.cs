using Edument.CQRS;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CompetencesUnitTests
{
    public class NameSet
    {
        public NameSet(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
    public class Example : Aggregate, IApplyEvent<NameSet>
    {
        public string Name { get; set; }

        public void SetName(string name)
        {
            ApplyOneEvent<NameSet>(new NameSet(name));
        }

        public void Apply(NameSet e)
        {
            Name = e.Name;
        }
    }
    public class Agg_Test
    {
        [Fact]
        public void Load_From_Event()
        {
            var exampl = new Example();
            var events = new List<NameSet>
            {
                new NameSet("Mat")
            };
            exampl.ApplyEvents(events);
            Assert.Equal("Mat", exampl.Name);
            Assert.Equal(1, exampl.EventsLoaded);
        }
        [Fact]
        public void Load_From_Constructor()
        {
            var exampl = new Example();
            exampl.SetName("Mat");

            Assert.Equal("Mat", exampl.Name);
            Assert.Equal(1, exampl.EventsLoaded);

        }
    }
}

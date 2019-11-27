using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTests.NEventStoreTests
{
    class SampleEvent
    {
        public SampleEvent(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }

        
    }
}

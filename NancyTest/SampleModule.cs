using System;
using System.Collections.Generic;
using System.Text;
using Nancy;

namespace NancyTest
{
    public class SampleModule : NancyModule
    {
        public SampleModule()
        {
            Get("/test",  parameters => { return "Hello World!"; });

        }
    }
}


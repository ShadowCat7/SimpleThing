using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleThing
{
    public static class Rando
    { public static Random random = new Random(System.DateTime.Now.Millisecond); }
}

using System;
using System.Collections.Generic;

namespace MonkeyService
{
    public interface IMonkeyService
    {
        List<Monkey> GetService(string weburi);
    }
}

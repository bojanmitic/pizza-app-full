using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_server.Models
{
    public enum OrderState
    {
        WaitingForPreparation = 1,
        Making = 2,
        Made = 3
    }
}

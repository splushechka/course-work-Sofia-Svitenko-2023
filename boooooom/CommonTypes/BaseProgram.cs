using boooooom.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boooooom.CommonTypes
{
    public abstract class BaseProgram
    {
        public static GameStatus Status { get; set; } = GameStatus.Paused;
    }
}

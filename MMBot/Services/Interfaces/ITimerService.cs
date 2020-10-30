﻿using System;
using System.Threading.Tasks;
using MMBot.Data.Entities;

namespace MMBot.Services.Interfaces
{
    public interface ITimerService
    {
        Task Start(MMTimer t, bool bReinit = false, TimeSpan? ToFirstRing = null);
        Task Stop(MMTimer t);
        bool Check(MMTimer t);
    }
}

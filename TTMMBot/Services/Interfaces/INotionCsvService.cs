﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TTMMBot.Data;

namespace TTMMBot.Services.Interfaces
{
    public interface INotionCsvService
    {
        Task<Exception> ImportCsv(byte[] csv);
        Task<byte[]> ExportCsv();
    }
}
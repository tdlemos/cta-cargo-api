﻿using CtaCargo.CctImportacao.Domain.Entities;

namespace CtaCargo.CctImportacao.Application.Support
{
    public interface IValidadorMaster
    {
        void TratarErrosMaster(Master master);
    }
}
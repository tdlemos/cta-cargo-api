﻿using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Application.Support.Contracts
{
    public interface IMotorIata
    {
        List<ArquivoXML> HousesXML { get; set; }
        List<ArquivoXML> MasterHouseXML { get; set; }
        List<ArquivoXML> MastersXML { get; set; }
        List<ArquivoXML> VoosXML { get; set; }

        string GenFlightManifest(Voo voo, int? trechoId = null, DateTime? actualDateTime = null, bool isScheduled = false);
        string GenFullManifest(Voo vooCompleto);
        string GenHouseManifest(House house, IataXmlPurposeCode purposeCode);
        string GenMasterManifest(Master master, IataXmlPurposeCode purposeCode);
        string GenMasterHouseManifest(SubmeterRFBMasterHouseItemRequest masterInfo, List<House> houses, IataXmlPurposeCode purposeCode, DateTime issueDate);
    }
}
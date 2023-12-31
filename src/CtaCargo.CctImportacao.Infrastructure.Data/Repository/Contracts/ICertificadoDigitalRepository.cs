﻿using CtaCargo.CctImportacao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts
{
    public interface ICertificadoDigitalRepository
    {
        void CreateCertificadoDigital(CertificadoDigital certificado);
        void DeleteCertificadoDigital(CertificadoDigital certificado);
        Task<IEnumerable<CertificadoDigital>> GetAllCertificadosDigital(int empresaId);
        Task<CertificadoDigital> GetCertificadoDigitalById(int id);
        Task<CertificadoDigital> GetCertificadoDigitalBySerialNumber(int empresaId, string serialNumber);
        Task<bool> SaveChanges();
        void UpdateCertificadoDigital(CertificadoDigital certificado);
    }
}
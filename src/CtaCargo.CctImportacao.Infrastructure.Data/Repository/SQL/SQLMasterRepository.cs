﻿using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL
{
    public class SQLMasterRepository : IMasterRepository
    {
        private readonly ApplicationDbContext _context;

        public SQLMasterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateMaster(int companyId, Master master)
        {
            if (master == null)
            {
                throw new ArgumentNullException(nameof(master));
            }

            _context.Masters.Add(master);
        }

        public void DeleteMaster(int companyId, Master master)
        {
            if (master == null)
            {
                throw new ArgumentNullException(nameof(master));
            }
            master.DataExclusao = DateTime.UtcNow;
            _context.Masters.Update(master);
        }

        public async Task<IEnumerable<Master>> GetAllMasters(Expression<Func<Master, bool>> predicate)
        {
            return await _context.Masters
                .Include("ULDs")
                .Include("ErrosMaster")
                .Include("UsuarioCriacaoInfo")
                .Include("VooInfo")
                .Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Master>> GetAllMastersByDataCriacao(int companyId, DateTime dataEmissao)
        {
            DateTime dataInicial = new DateTime(
                dataEmissao.Year,
                dataEmissao.Month,
                dataEmissao.Day,
                0, 0, 0, 0);
            DateTime dataFinal = new DateTime(
                dataEmissao.Year,
                dataEmissao.Month,
                dataEmissao.Day,
                23, 59, 59, 997);
            return await _context.Masters
                .Include("ULDs")
                .Include("ErrosMaster")
                .Include("UsuarioCriacaoInfo")
                .Include("VooInfo")
                .Where( x => 
            x.EmpresaId == companyId &&
            x.DataExclusao == null &&
            x.CreatedDateTimeUtc >= dataInicial &&
            x.CreatedDateTimeUtc <= dataFinal).ToListAsync();
        }
        
        public async Task<Master> GetMasterById(int companyId, int masterId)
        {
            return await _context.Masters
                .Include("ULDs")
                .Include("ErrosMaster")
                .Include("UsuarioCriacaoInfo")
                .Include("VooInfo")
                .FirstOrDefaultAsync(x => x.EmpresaId == companyId && x.Id == masterId && x.DataExclusao == null);
        }
        public async Task<IEnumerable<Master>> GetMasterByIds(int companyId, int[] masterIds)
        {
            return await _context.Masters
                .Include("ULDs")
                .Include("ErrosMaster")
                .Include("UsuarioCriacaoInfo")
                .Include("VooInfo")
                .Where(x => x.EmpresaId == companyId && masterIds.Contains(x.Id) && x.DataExclusao == null)
                .ToListAsync();
        }

        public async Task<List<Master>> GetMastersForUploadById(int companyId, int[] masterArrayId)
        {
            return await _context.Masters
                .Include("CiaAereaInfo")
                .Include("AeroportoOrigemInfo")
                .Include("AeroportoDestinoInfo")
                .Include("ErrosMaster")
                .Include("ULDs")
                .Include("VooInfo")
                .Include("VooInfo.CompanhiaAereaInfo")
                .Include("VooInfo.PortoIataOrigemInfo")
                .Include("VooInfo.PortoIataDestinoInfo")
                .Where(x => x.EmpresaId == companyId && masterArrayId.Contains(x.Id) && x.DataExclusao == null)
                .ToListAsync();
        }

        public async Task<Master> GetMasterForUploadById(int companyId, int masterId)
        {
            return await _context.Masters
                .Include("CiaAereaInfo")
                .Include("AeroportoOrigemInfo")
                .Include("AeroportoDestinoInfo")
                .Include("ErrosMaster")
                .Include("ULDs")
                .Include("VooInfo")
                .Include("VooInfo.CompanhiaAereaInfo")
                .Include("VooInfo.PortoIataOrigemInfo")
                .Include("VooInfo.PortoIataDestinoInfo")
                .Where(x => x.EmpresaId == companyId && x.Id == masterId && x.DataExclusao == null)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Master>> GetMastersForUploadByVooId(int companyId, int vooId)
        {
            return await _context.Masters
                .Include("CiaAereaInfo")
                .Include("AeroportoOrigemInfo")
                .Include("AeroportoDestinoInfo")
                .Include("ErrosMaster")
                .Include("ULDs")
                .Include("VooInfo")
                .Include("VooInfo.CompanhiaAereaInfo")
                .Include("VooInfo.PortoIataOrigemInfo")
                .Include("VooInfo.PortoIataDestinoInfo")
                .Where(x => x.EmpresaId == companyId && x.VooId == vooId && x.DataExclusao == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<MasterListaQuery>> GetMastersListaByVooId(int companyId, int vooId)
        {
            return await (from m in _context.Masters
                          where m.EmpresaId == companyId && m.VooId == vooId &&
                          m.DataExclusao == null
                          select new MasterListaQuery()
                          {
                              MasterId = m.Id,
                              Numero = m.Numero
                          }).ToListAsync();
        }

        public async Task<IEnumerable<MasterVooQuery>> GetMastersVoo(int companyId, int vooId)
        {
            return await (from m in _context.Masters
                        where m.EmpresaId == companyId && m.VooId == vooId && m.DataExclusao == null
                        select new MasterVooQuery()
                        {
                            Numero = m.Numero,
                            Descricao = m.DescricaoMercadoria,
                            Peso = m.PesoTotalBruto,
                            PesoUnidade = m.PesoTotalBrutoUN,
                            TotalPecas = m.TotalPecas,
                            PortoOrigemId = m.AeroportoOrigemId,
                            PortoDestinoId = m.AeroportoDestinoId
                        }).ToListAsync();
        }

        public async Task<Master> GetMasterIdByNumber(int companyId, int? vooId, string masterNumber)
        {
            var result = await _context.Masters
                .Where(x => x.VooId == vooId && x.Numero == masterNumber && x.DataExclusao == null)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<Master> GetMasterByNumber(int companyId, string masterNumber)
        {
            var result = await _context.Masters
                .Where(x => x.Numero == masterNumber && x.DataExclusao == null)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<SituacaoRFBQuery> GetMasterRFBStatus(int masterId)
        {
            return await _context.Masters
                .Where(x => x.Id == masterId && x.DataExclusao == null)
                .Select(x => new SituacaoRFBQuery
                {
                    Id = x.Id,
                    SituacaoRFB = x.SituacaoRFBId,
                    ProtocoloRFB = x.ProtocoloRFB
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetMasterIdByNumberValidate(int ciaId, string numero, DateTime dataLimite)
        {
            var result = await _context.Masters
                .Where(x => x.CiaAereaId == ciaId && x.Numero == numero && x.CreatedDateTimeUtc >= dataLimite && x.DataExclusao == null)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void UpdateMaster(Master master)
        {
            _context.Update(master);
        }
    }
}

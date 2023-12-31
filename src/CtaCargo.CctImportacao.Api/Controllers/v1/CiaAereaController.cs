﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CtaCargo.CctImportacao.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class CiaAereaController : Controller
    {
        private readonly ICiaAereaService _ciaAereaService;
        public CiaAereaController(ICiaAereaService ciaAereaService)
        {
            _ciaAereaService = ciaAereaService;
        }

        [HttpGet]
        [Authorize]
        [Route("ObterCiaAereaPorId")]
        public async Task<ApiResponse<CiaAereaResponseDto>> ObterCiaAereaPorId(int ciaId)
        {
            return await _ciaAereaService.CiaAereaPorId(ciaId);
        }

        [HttpGet]
        [Authorize]
        [Route("ListarCiasAereas")]
        public async Task<ApiResponse<IEnumerable<CiaAereaResponseDto>>> ListarCiasAereas(int empresaId)
        {
            return await _ciaAereaService.ListarCiaAereas(empresaId);
        }

        [HttpPost]
        [Authorize(Roles = "AdminCiaAerea")]
        [Route("InserirCiaAerea")]
        public async Task<ApiResponse<CiaAereaResponseDto>> InserirCiaAerea([FromBody]CiaAereaInsertRequest input)
        {  
            return await _ciaAereaService.InserirCiaAerea(input);
        }

        [HttpPost]
        [Authorize(Roles = "AdminCiaAerea")]
        [Route("AtualizarCiaAerea")]
        public async Task<ApiResponse<CiaAereaResponseDto>> AtualizarCiaAerea([FromBody]CiaAereaUpdateRequest input)
        {
            return await _ciaAereaService.AtualizarCiaAerea(input);
        }

        [HttpDelete]
        [Authorize(Roles = "AdminCiaAerea")]
        [Route("ExcluirCiaAerea")]
        public async Task<ApiResponse<CiaAereaResponseDto>> ExcluirCiaAerea(int ciaId)
        {
            return await _ciaAereaService.ExcluirCiaAerea(ciaId);
        }

        [HttpGet]
        [Authorize]
        [Route("ListarCiasAereasSimples")]
        public async Task<ApiResponse<IEnumerable<CiaAreaListaSimplesResponse>>> ListarCiasAereasSimples(int empresaId)
        {
            return await _ciaAereaService.ListarCiaAereasSimples(empresaId);
        }
    }
}

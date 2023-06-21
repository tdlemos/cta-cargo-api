﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Request
{
    public class UsuarioUpdateRequest
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int CompanhiaId { get; set; }
        public bool AlteraCompanhia { get; set; }
        public bool AcessoUsuarios { get; set; }
        public bool AcessoClientes { get; set; }
        public bool AcessoCompanhias { get; set; }
        public bool Bloqueado { get; set; }
        public int UsuarioModificadorId { get; set; }
        public int? CertificadoDigitalId { get; set; }
    }
}

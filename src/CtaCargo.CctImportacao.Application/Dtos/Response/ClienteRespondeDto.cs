﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Response
{
    public class ClienteRespondeDto
    {
        public string Cnpj { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Postal { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
    }
}

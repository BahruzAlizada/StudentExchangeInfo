﻿using StudentExchangeInfo.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace StudentExchangeInfo.Domain.Entities
{
    public class ExchangeProgramDocument : BaseEntity
    {
        public int ExchangeProgramId { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Document { get; set; }
        public ExchangeProgram ExchangeProgram { get; set; }

    }
}
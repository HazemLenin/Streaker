﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Streaker.DAL.Dtos.Auth
{
    public class AuthDto
    {
        [JsonIgnore]
        public bool IsSucceed => Errors == null;
        [JsonIgnore]
        public Dictionary<string, List<string>>? Errors { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
    }
}

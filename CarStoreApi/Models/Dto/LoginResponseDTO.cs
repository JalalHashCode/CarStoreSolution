﻿namespace CarStoreApi.Models.Dto
{
    public class LoginResponseDTO
    {
        public LocalUser User { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}

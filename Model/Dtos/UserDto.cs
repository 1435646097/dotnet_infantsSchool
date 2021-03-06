﻿using System;

namespace Model.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public string Address { get; set; }
        public string Pwd { get; set; }
        public bool Gender { get; set; }
        public string AddressDetail { get; set; }
        public bool IsDelete { get; set; }
    }
}
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Model.Dtos
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Pid { get; set; }
        public string Icon { get; set; }
        public string Path { get; set; }
        public IEnumerable<MenuDto> Children { get; set; }
    }
}

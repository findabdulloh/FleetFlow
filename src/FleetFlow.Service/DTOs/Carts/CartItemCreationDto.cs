﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetFlow.Service.DTOs.Carts
{
    public class CartItemCreationDto
    {
        public long ProductId { get; set; }
        public int Amount { get; set; }
    }
}
﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SWAM.Models.Warehouse
{
    [Table("WarehouseAddress")]
    public class WarehouseAddress : Address
    {
        public Warehouse Warehouse { get; set; }
    }
}

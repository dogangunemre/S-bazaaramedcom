﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace bazaaramed.Models
{
    public class KategoriAddresses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Path { get; set; }
        public bool State { get; set; }
        public int Source { get; set; } = 3;
        public string ShopPHPID { get; set; }
        public string ShopPHPName { get; set; }
        public bool ShopPHPState { get; set; }
    }
}

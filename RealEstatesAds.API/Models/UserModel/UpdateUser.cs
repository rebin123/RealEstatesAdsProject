﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstatesAds.API.Models.UserModel
{
    public class UpdateUser
    {
        public int UserId { get; set; }

        public string Value { get; set; }
    }
}

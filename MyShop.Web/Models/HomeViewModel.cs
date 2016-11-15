﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<SlideViewModel> Slides { set; get; }
        public IEnumerable<ProductViewModel> LatestProducts { set; get; }
        public IEnumerable<ProductViewModel> HotestProducts { set; get; }
    }
}
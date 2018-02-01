using System;
using System.Collections.Generic;

namespace shop.Models
{
    public class Checkout
    {
        public List<AmzCartItem> items;
        public string Id;
        public string countrycode;
    }
}

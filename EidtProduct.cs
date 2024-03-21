using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1020451.DomainModels
{
    
    public  class EidtProduct
    { 
        public Product product {  get; set; }
         
        public List<ProductPhoto> Photos { get; set; }
        public List<ProductAttribute> Attributes { get; set; }

    }

}

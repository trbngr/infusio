//----------------------
// <auto-generated>
// </auto-generated>
//----------------------
using LanguageExt;

namespace Infusion.Model
{
    public class ProductStatus : Record<ProductStatus>
    {
            public readonly Product Product;
            public readonly Status Status;
      
      public ProductStatus(Product product = default, Status status = default)
      {
                Product = product;
                Status = status;
        
      }

      public ProductStatus Copy(Product product = default, Status status = default) => new ProductStatus(
                  product:  product == default ? Product : product, 
                        status:  status == default ? Status : status
            
      );
    }
}

using System.Collections.Generic;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.Business.Abstract
{
		public interface ISellerService
		{
				List<Seller> GetAll();
				Seller GetById(int sellerId);
				List<Seller> GetBySeller(int sellerId);
				
				Seller Add(Seller seller);
				void Update(Seller seller);
				void Delete(Seller seller);

		}
}
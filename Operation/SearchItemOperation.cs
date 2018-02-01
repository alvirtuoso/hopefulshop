using System;
namespace shop.Operation
{
    public class SearchItemOperation: OperationBase
    {
        public SearchItemOperation()
        {
			base.StoreInDictionary.Add("Operation", "ItemLookup");
        }

		public void PresetOperation(string asin)
		{
			this.AddService("AWSECommerceService");
            this.AddAssociateTag(Helpers.Constants.AMAZON_DEFAULT_ID);
			this.AddOrReplace("ResponseGroup", "Images,ItemAttributes,Reviews,Offers,SalesRank,Similarities");

            this.AddOrReplace("ItemId", asin);
            this.AddOrReplace("IdType", "ASIN");

		}
    }
}

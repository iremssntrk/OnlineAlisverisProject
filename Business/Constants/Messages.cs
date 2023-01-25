
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string ProductListed = "Ürün listeleme başarılı";
        internal static string ProductCountOfCategoryError="Ürün kategory sayısı fazla";
        internal static string ProductNameAlreadyExist="Aynı isimde zaten başka ürün bulunmaktadır";
        internal static string CategoryLimitExceded="Kategory limiti aşıldığı için yeni ürün eklenemiyor";
    }
}

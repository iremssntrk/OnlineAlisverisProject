using Core.Entities.Concreate;
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
        internal static string AuthorizationDenied="Yetkiniz yok";
        internal static string UserRegistered="Kullanıcı kaydedildi";
        internal static string UserNotFound="Kullanıcı bulunamadı";
        internal static string PasswordError="Şifre hatalı";
        internal static string SuccessfulLogin="Giriş başarılı";
        internal static string UserAlreadyExists="Kullanıcı zaten mevcut";
        internal static string AccessTokenCreated="Giriş token yaratıldı";
    }
}


using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Google.Cloud.Firestore;

namespace Service.Shared;

[FirestoreData]
public class UserModel
{
    [FirestoreDocumentId]
    public string Id { get; set; }
    
    [FirestoreProperty]
    public string Email { get; set; }

    [FirestoreProperty]
    public string UserName { get; set; }

    [FirestoreProperty]
    public string DateOfBirth { get; set; }
}

[FirestoreData]
public class OrderModel
{
    [FirestoreDocumentId]
    public string OrderId { get; set; }

    [FirestoreProperty]
    public int OrderAmount { get; set; }

    [FirestoreProperty]
    public string OrderStatus { get; set; }

    [FirestoreProperty]
    public PaymentModel PaymentDetails { get; set; }
}

[FirestoreData]
public class PaymentModel
{
    [FirestoreDocumentId]
    public string PaymentId { get; set; }

    [FirestoreProperty]
    public string PaymentStatus { get; set; }

}

[FirestoreData]
public class ProductModel
{
    [FirestoreDocumentId]
    public string ProductId { get; set; }

    [FirestoreProperty]
    public string ProductName { get; set; }

    [FirestoreProperty]
    public int RemaninigCount { get; set; }

    [FirestoreProperty]
    public CategoryModel CategoryDetails { get; set; }


}

[FirestoreData]
public class CategoryModel
{
    [FirestoreDocumentId]
    public string CategoryId { get; set; }

    [FirestoreProperty]
    public string CategoryName { get; set; }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebsitebanhangEx.Models
{
    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
        [NotMapped]
        public HttpPostedFileBase UploadImage { get; set; }
    }

    public partial class Category
    {
        [NotMapped]
        public List<Category> ListCate { get; set; }

    }
    public partial class AdminUser
    {
        [Required(ErrorMessage = "ID not empty...")]
        [Display(Name = "Mã user")]

        public int ID { get; set; }
        [Required(ErrorMessage = "Name not empty...")]
        [Display(Name = "Tên User")]

        public string NameUser { get; set; }
        [Display(Name = "Vị trí")]
        public string RoleUser { get; set; }
        [Display(Name = "Nhập mật khẩu")]
        [Required(ErrorMessage = "Pass not empty...")]
        [DataType(DataType.Password)]
        public string PasswordUser { get; set; }
        [NotMapped]
        [Compare("PasswordUser")]
        [DisplayName("Nhập lại mật khẩu")]
        public string ComfirmPass { get; set; }
        [NotMapped]
        public string ErrorLogin { get; set; }
    }
    public partial class Customer
    {
        [Required(ErrorMessage = "ID not empty...")]
        [Display(Name = "Mã người dùng")]
        public int IDCus { get; set; }


        [Required(ErrorMessage = "Name not empty...")]
        [Display(Name = "Tên khách hàng")]
        public string NameCus { get; set; }
        [Required(ErrorMessage = "Name not empty...")]


        [Display(Name = "Họ")]
        public string FirstName { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Năm sinh")]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2100", ErrorMessage = "Ngày sinh không hợp lệ")]
        public Nullable<System.DateTime> DateBirth { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Địa chỉ email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Pass not empty...")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z]).{8,}$", ErrorMessage = "Mật khẩu phải có ít nhất 1 chữ thường, 1 chữ hoa,và 8 ký tư")]
        public string PasswordCus { get; set; }
        [NotMapped]
        [Compare("PasswordCus")]
        [DisplayName("Nhập lại mật khẩu")]
        public string ComfirmPass { get; set; }
        [NotMapped]
        public string ErrorLogin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderPro> OrderProes { get; set; }
    }
    public partial class Handsize
    {

        [NotMapped]
        public List<Handsize> ListSize { get; set; }
    }
    public partial class Nentang
    {
        [NotMapped]
        public List<Nentang> ListNentang { get; set; }

    }


}
using DTO;

using BL.Interfaces;
using DAL.Data;
using DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace PPMS.ViewModels
{
    public class CreateUserViewModel
    {
        public CreateUserDTO User { get; set; } = new CreateUserDTO();
        public IEnumerable<SelectListItem> Employees { get; set; } = new List<SelectListItem>();
    }
}


//الفوائد

//لا حاجة لاستخدام ViewBag → يقلل NullReferenceException.

//كل البيانات التي تحتاجها الـ View موجودة في ViewModel واحد.

//يمكنك إعادة استخدام DropdownService لاحقًا لتجهيز القوائم لجميع الصفحات.

//الخدمة (UserService) تبقى نظيفة وتركز على CRUD فقط، دون أي معرفة بالـ UI.
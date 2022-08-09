using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechShed.WebUI.AppCode.Infrastructure;

namespace TechShed.WebUI.Models.Entities
{
    public class ContactPost : BaseEntity
    {
        [Required(ErrorMessage = "Boş Buraxmaq Olmaz")]
        public string Message { get; set; }
        [Required(ErrorMessage = "Boş Buraxmaq Olmaz")]
        public string FullName { get; set; }
        [EmailAddress(ErrorMessage = "E-poct Adresiniz Uygun Deyil")]
        [Required(ErrorMessage = "Boş Buraxmaq Olmaz")]
        public string Email { get; set; }
        public DateTime? AnswerDate { get; set; }
        public string AnswerMessage { get; set; }
        public int? AnswerById { get; set; }


    }
}

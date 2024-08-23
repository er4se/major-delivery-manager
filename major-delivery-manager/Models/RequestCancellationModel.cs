using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace major_delivery_manager.Models
{
    internal class RequestCancellationModel : BindableBase
    {
        [Key]
        public string Id { get; set; }

        public string RequestModelId { get; set; }

        [NotMapped]
        private string? comment;
        public RequestModel? Request { get; set; }

        public string? Comment
        {
            get => comment;
            set
            {
                SetProperty(ref comment, value);
            }
        }

        public RequestCancellationModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

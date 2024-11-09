using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Base.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public string Note { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public Guid CreatedUser { get; set; }

        [MaxLength(255)]
        public string CreatedName { get; set; } = string.Empty;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public Guid UpdatedUser { get; set; }

        [MaxLength(255)]
        public string UpdatedName { get; set; } = string.Empty;
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedUser { get; set; }

        [MaxLength(255)]
        public string? DeletedName { get; set; }

        public BaseEntity()
        {
        }

        public void SetCreator(Guid currentUserId, string currentUserName)
        {
            this.CreatedUser = currentUserId;
            this.CreatedName = currentUserName;
            this.CreatedDate = DateTime.UtcNow;

            this.UpdatedUser = currentUserId;
            this.UpdatedName = currentUserName;
            this.UpdatedDate = DateTime.UtcNow;
        }

        public void SetModifier(Guid currentUserId, string currentUserName)
        {
            this.UpdatedUser = currentUserId;
            this.UpdatedName = currentUserName;
            this.UpdatedDate = DateTime.UtcNow;
        }

        public void SetRemover(Guid currentUserId, string currentUserName)
        {
            this.DeletedUser = currentUserId;
            this.DeletedName = currentUserName;
            this.DeletedDate = DateTime.UtcNow;
        }

        protected static void CheckNotNullOrWhiteSpace(string value, string valueName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException($"{valueName} is null or empty");
            }
            if (value.Trim().Length == 0)
            {
                throw new ArgumentException($"{valueName} is empty");
            }
        }
    }
}

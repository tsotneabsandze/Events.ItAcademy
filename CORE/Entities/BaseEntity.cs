using System;

namespace CORE.Entities
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
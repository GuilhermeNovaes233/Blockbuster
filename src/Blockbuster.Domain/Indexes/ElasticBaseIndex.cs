using System;

namespace Blockbuster.Domain.Indexes
{
    public abstract class ElasticBaseIndex
    {
        protected ElasticBaseIndex()
        {
            Id = Guid.NewGuid();
            UpdateTime = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
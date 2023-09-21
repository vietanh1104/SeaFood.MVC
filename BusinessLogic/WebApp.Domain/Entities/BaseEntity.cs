namespace WebApp.Domain.Entities
{
#nullable disable
    public interface BaseEntity<T>
    {
        public T Id { get; set; }
    }
}

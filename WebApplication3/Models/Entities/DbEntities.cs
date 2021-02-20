namespace WebApplication3.Models.Entities {
    public abstract class DbEntities {
        public virtual int Id { get; protected set; }
        public virtual bool IsDelete { get; protected set; }

        public virtual bool Delete(bool delete)
            => IsDelete = delete;


    }
}

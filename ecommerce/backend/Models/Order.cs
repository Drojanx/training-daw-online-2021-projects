
namespace backend.Models
{
    public class Order
    {
        public int Id { get; set; }

        /*
         * Pensaba usar directamente CartProduct, pero se me crearía una columna OrderId 
         * en la tabla Cart y no queremos eso. Mejor usar una clase extra OrderProduct
        */
        public ICollection<OrderProduct> Products { get; set; }
    }
}
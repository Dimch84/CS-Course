namespace ComponentsMediator
{
    public class ProductChangeInitiator
    {
        public ProductChangeInitiator(int selectedProductId)
        {
            SelectedProductId = selectedProductId;            
        }

        public int SelectedProductId { get; set; }

    }
}

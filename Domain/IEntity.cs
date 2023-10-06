namespace Domain
{
    public interface IEntity
    {
        // An interface is used to create a sort of 'contract' in your application.
        // Every class inheriting from IEntity HAS to have the following methods.
        // This is useful because this ensures consistancy throughout the different classes.
        // For example it prevents having an Item.Remove() and a Container.Delete() method.

        // Update -> this doesnt get used because we do everything in the API now... :/

        public void Create();
        public void Delete();
        public void Update();
    }
}

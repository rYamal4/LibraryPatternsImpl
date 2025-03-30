namespace LibraryPatternsImpl.Users;

public interface ISubscriber
{
    void Update(string message);
    void ShowNotifications();
}

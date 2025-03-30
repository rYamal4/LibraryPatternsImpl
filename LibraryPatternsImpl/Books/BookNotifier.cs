using LibraryPatternsImpl.Users;

namespace LibraryPatternsImpl.Books;

public class BookNotifier
{
    private readonly Dictionary<int, List<ISubscriber>> _subscribers = [];

    public void Subscribe(int bookId, ISubscriber user)
    {
        if (!_subscribers.ContainsKey(bookId))
            _subscribers[bookId] = [];

        if (!_subscribers[bookId].Contains(user))
            _subscribers[bookId].Add(user);
    }

    public void Notify(int bookId, string bookTitle)
    {
        if (_subscribers.TryGetValue(bookId, out var users))
        {
            foreach (var user in users)
            {
                user.Update($"The book \"{bookTitle}\" is now available.");
            }

            _subscribers.Remove(bookId);
        }
    }
}
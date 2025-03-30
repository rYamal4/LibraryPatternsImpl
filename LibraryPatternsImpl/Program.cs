using LibraryPatternsImpl.Books;
using LibraryPatternsImpl.Catalog;
using LibraryPatternsImpl.CLI;
using LibraryPatternsImpl.Users;
using LibraryPatternsImpl.Utils;

var catalog = new Catalog();
var notifier = new BookNotifier();
BookStorage.Populate(catalog);

var userManager = new UserManager();

while (true)
{
    var user = userManager.LoginOrRegister();
    if (user == null) return;

    if (user is ISubscriber subscriber)
        subscriber.ShowNotifications();
    var facade = new LibraryFacade(catalog, user, notifier);
    facade.ShowMenu();
}
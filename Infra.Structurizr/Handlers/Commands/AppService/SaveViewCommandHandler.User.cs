using AnemicModel;

using Core;

using Structurizr;

using System.Text;

namespace Infrastructure.Structurizr.Handlers.Commands.AppService
{
    public static partial class SaveViewCommandHandler
    {
        private static string GetAllTags(User user)
        {
            StringBuilder tags = new(user.GetType().Name);
            char comma = ',';

            tags.Append(comma + user.Name);

            return tags.ToString();
        }
        private static void AddUsers(Model model, Diagram diagram, StaticView view)
        {
            if (diagram.UserGroups.Exists())
            {
                foreach (IEnumerable<User> users in diagram.UserGroups)
                {
                    int userCount = users.Count();

                    Person[] persons = new Person[userCount];

                    for (int i = 0; i < userCount; i++)
                    {
                        User user = users.ElementAt(i);
                        Person person = model.GetPersonWithName(user.Name);
                        view.Add(person);
                        persons[i] = person;
                    }
                    HandleAnimation(view, persons); 
                }
            }
        }
        private static void HandleUsers(
            IEnumerable<User> users, Model model, SoftwareSystem system)
        {
            if (users == null)
            {
                return;
            }

            foreach (User user in users)
            {
                Person person = model.AddPerson(user.Name, user.Description);

                switch (user.Location)
                {
                    case Place.External:
                        person.Location = Location.External;
                        break;
                    case Place.Internal:
                        person.Location = Location.Internal;
                        break;
                }

                person.Tags = GetAllTags(user)
                    ;
                HandleRelations(system, user, person);
            }
        }
    }
}

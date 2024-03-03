using System.IO;
using System.Reflection.PortableExecutable;

namespace PhoneBook;

public class PhoneBook
{
    public PhoneBook(List<Contact> contacts)
    {
        Contacts = contacts;
    }

    public PhoneBook()
    {
        Contacts = new List<Contact>();
    }
    public List<Contact> Contacts { get; set; }
    public IConsoleReader ConsoleReader = new ConsoleReader();

    public void AddContact()
    {
        Contact contact = new Contact();
        ConsoleReader.Write("Введите имя: ");
        contact.FirstName = ConsoleReader.ReadLine();

        ConsoleReader.Write("Введите фамилию: ");
        contact.LastName = ConsoleReader.ReadLine();

        ConsoleReader.Write("Введите номер телефона: ");
        contact.PhoneNumber = ConsoleReader.ReadLine();
        Contacts.Add(contact);

        ConsoleReader.WriteLine("Контакт добавлен.");


    }

    public void ViewContacts()
    {
        if (Contacts.Count() > 0)
        {
            int i = 0;
            ConsoleReader.WriteLine("Список контактов:");
            foreach (Contact contact in Contacts)
            {
                i++;
                ConsoleReader.WriteLine(i + ") " + "Имя: " + contact.FirstName + " Фамилия: " + contact.LastName + " Номер телефона: " + contact.PhoneNumber);
            }
        }
        else
        {
            ConsoleReader.WriteLine("Список контактов пуст.");
        }

    }

    public void UpdateContact()
    {
        ConsoleReader.Write("Введите номер телефона/имя/фамилию контакта, который хотите обновить: ");
        string consRead = ConsoleReader.ReadLine();

        Contact contact = Contacts.FirstOrDefault
            (x => x.PhoneNumber == consRead ||
             x.FirstName == consRead ||
             x.LastName == consRead);
        if (contact == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            ConsoleReader.WriteLine("\tКонтакт не найден.");

            Console.ResetColor();
        }
        else
        {
            Console.Write("Введите новое имя:\t");

            contact.FirstName = ConsoleReader.ReadLine();

            Console.Write("Введите новую фамилию:\t");

            contact.LastName = ConsoleReader.ReadLine();

            Console.Write("Введите новый телефон:\t");

            contact.PhoneNumber = ConsoleReader.ReadLine();

            Contacts[Contacts.IndexOf(contact)] = contact;

            Console.ForegroundColor = ConsoleColor.Green;

            ConsoleReader.WriteLine("\tКонтакт обновлен.");

            Console.ResetColor();
        }


    }

    public void DeleteContact()
    {
        ConsoleReader.Write("\"Введите номер телефона/имя/фамилию контакта, который хотите обновить:  ");
        string consRead = ConsoleReader.ReadLine();

        Contact contact = Contacts.FirstOrDefault
           (x => x.PhoneNumber == consRead ||
            x.FirstName == consRead ||
            x.LastName == consRead);

        if (contact == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            ConsoleReader.WriteLine("\tКонтакт не найден.");

            Console.ResetColor();
        }
        else
        {
            Contacts.RemoveAt(Contacts.IndexOf(contact));

            Console.ForegroundColor = ConsoleColor.Green;

            ConsoleReader.WriteLine("\tКонтакт удален.");

            Console.ResetColor();
        }


    }

    public void SearchContact()
    {
        ConsoleReader.Write("Введите номер телефона/имя/фамилию контакта, который хотите обновить:\t");

        string search = ConsoleReader.ReadLine();

        Contact contact = Contacts.FirstOrDefault
           (x => x.PhoneNumber == search ||
            x.FirstName == search ||
            x.LastName == search);

        if (contact == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            ConsoleReader.WriteLine("\tКонтакт не найден.");

            Console.ResetColor();
        }
        else
        {
            ConsoleReader.WriteLine("Имя: " + contact.FirstName +
                                " Фамилия: " + contact.LastName +
                         " Номер телефона: " + contact.PhoneNumber);
        }

    }

    public void SaveBook()
    {
        using (StreamWriter writer = new StreamWriter("contacts.txt"))
        {
            foreach (Contact contact in Contacts)
            {
                writer.WriteLine(contact.FirstName + "," + contact.LastName + "," + contact.PhoneNumber);
            }
        }

        ConsoleReader.WriteLine("Книга сохранена.");
    }

    public void LoadBook()
    {

        using (StreamReader reader = new StreamReader("contacts.txt"))
        {
            while (!reader.EndOfStream)
            {
                Contact contact = new Contact();
                string[] fieldsContact = reader.ReadLine().Split(',');
                contact.FirstName = fieldsContact[0];
                contact.LastName = fieldsContact[1];
                contact.PhoneNumber = fieldsContact[2];
                Contacts.Add(contact);
            }
        }
       
    }
}

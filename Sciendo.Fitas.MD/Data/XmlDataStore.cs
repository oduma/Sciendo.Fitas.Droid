using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Sciendo.Fitas.Model;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Sciendo.Fitas.Data
{
    public class XmlDataStore
    {
#if MONOTOUCH
        static string AppPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
#elif DROID
		static string AppPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);			
#elif SILVERLIGHT
		// with IsolatedStorage there is no ROOT path, only application files can be accessed
        static string AppPath = "";
#else
        static string AppPath = Assembly.GetAssembly(typeof(XmlDataStore)).CodeBase.Substring(0, Assembly.GetAssembly(typeof(XmlDataStore)).CodeBase.LastIndexOf("/")).Replace("file:///", "");
#endif

        #region File System Access
        static List<WeekSummary> GetWeekSummaryList()
        {
            string dataFilePath = Path.Combine(AppPath, Path.Combine("Xml", "weektemplates.xml"));

            var loadedData = XDocument.Load(dataFilePath);
            using (var reader = loadedData.Root.CreateReader())
                return (List<WeekSummary>)new XmlSerializer(typeof(List<WeekSummary>)).Deserialize(reader);
        }


        private static void SaveDays(List<Day> daySummaries)
        {
            string dataFilePath = Path.Combine(AppPath, Path.Combine("Xml", "daysshistory.xml"));
            using (StreamWriter writer = new StreamWriter(dataFilePath))
            {
                var serializer = new XmlSerializer(typeof(List<Day>));
                serializer.Serialize(writer, daySummaries);
            }
        }

        private static List<Day> GetDaysList()
        {
            string dataFilePath = Path.Combine(AppPath, Path.Combine("Xml", "daysshistory.xml"));
            if (!File.Exists(dataFilePath))
                return new List<Day>();
            var loadedData = XDocument.Load(dataFilePath);
            using (var reader = loadedData.Root.CreateReader())
                return (List<Day>)new XmlSerializer(typeof(List<Day>)).Deserialize(reader);
        }


        private static void SaveWeeks(List<WeekSummary> weekSummaries)
        {
            string dataFilePath = Path.Combine(AppPath, Path.Combine("Xml", "weekshistory.xml"));
            using (StreamWriter writer = new StreamWriter(dataFilePath))
            {
                var serializer = new XmlSerializer(typeof(List<WeekSummary>));
                serializer.Serialize(writer, weekSummaries);
            }
        }


        private static List<WeekSummary> GetWeeksHistory()
        {
            string dataFilePath = Path.Combine(AppPath, Path.Combine("Xml", "weekshistory.xml"));

            if (!File.Exists(dataFilePath))
                return new List<WeekSummary>();
            var loadedData = XDocument.Load(dataFilePath);
            using (var reader = loadedData.Root.CreateReader())
                return (List<WeekSummary>)new XmlSerializer(typeof(List<WeekSummary>)).Deserialize(reader);
        }

        #endregion
        public static List<WeekSummary> GetWeekHistorySummaries()
        {
            var weeksFromHistory = GetWeeksHistory();
            if (weeksFromHistory == null)
                weeksFromHistory = new List<WeekSummary>();
            var weeksFromTemplate= GetWeekSummaryList().Where(w=>weeksFromHistory.FirstOrDefault(h=>h.WeekId==w.WeekId)==null);
            return weeksFromHistory.Union(weeksFromTemplate).ToList();
        }
        public static List<WeekSummary> GetWeekSummaries(int weekId)
        {
            return GetWeekSummaryList().Where(w => w.WeekId == weekId).ToList();
        }

        //public static List<Customer> GetCustomers(string filter, int page, int items)
        //{
        //    return (from item in GetCustomerList()
        //            where item.Name.Contains(string.IsNullOrWhiteSpace(filter) ? filter : item.Name) ||
        //                  item.PrimaryAddress.City.Contains(!string.IsNullOrWhiteSpace(filter) ? filter : item.PrimaryAddress.City) ||
        //                  item.PrimaryAddress.State.Contains(!string.IsNullOrWhiteSpace(filter) ? filter : item.PrimaryAddress.State) ||
        //                  item.PrimaryAddress.Zip.Contains(!string.IsNullOrWhiteSpace(filter) ? filter : item.PrimaryAddress.Zip)
        //            select new Customer()
        //            {
        //                ID = item.ID,
        //                Name = item.Name,
        //                Website = item.Website
        //            }).Skip((page - 1) * items).Take(items).ToList();
        //}

        //public static List<Customer> GetCustomers(string[] show)
        //{
        //    return (from item in GetCustomerList()
        //            select new Customer()
        //            {
        //                ID = show.Contains("id") ? item.ID : null,
        //                Name = show.Contains("name") ? item.Name : null,
        //                PrimaryAddress = show.Contains("primaryaddress") ? item.PrimaryAddress : null,
        //                PrimaryPhone = show.Contains("primaryphone") ? item.PrimaryPhone : null,
        //                Website = show.Contains("website") ? item.Website : null,
        //                Addresses = show.Contains("addresses") ? item.Addresses : null,
        //                Contacts = show.Contains("contacts") ? item.Contacts : null,
        //                Orders = show.Contains("orders") ? item.Orders : null,
        //            }).ToList();
        //}

        //public static List<Customer> GetCustomers(string filter, string[] show, int page, int items)
        //{
        //    List<Customer> retval = (from item in GetCustomerList()
        //                             where item.Name.Contains(!string.IsNullOrWhiteSpace(filter) ? filter : item.Name) ||
        //                                   item.PrimaryAddress.City.Contains(!string.IsNullOrWhiteSpace(filter) ? filter : item.PrimaryAddress.City) ||
        //                                   item.PrimaryAddress.State.Contains(!string.IsNullOrWhiteSpace(filter) ? filter : item.PrimaryAddress.State) ||
        //                                   item.PrimaryAddress.Zip.Contains(!string.IsNullOrWhiteSpace(filter) ? filter : item.PrimaryAddress.Zip)
        //                             select new Customer()
        //                             {
        //                                 ID = show.Contains("id") ? item.ID : null,
        //                                 Name = show.Contains("name") ? item.Name : null,
        //                                 PrimaryAddress = show.Contains("primaryaddress") ? item.PrimaryAddress : null,
        //                                 PrimaryPhone = show.Contains("primaryphone") ? item.PrimaryPhone : null,
        //                                 Website = show.Contains("website") ? item.Website : null,
        //                                 Addresses = show.Contains("addresses") ? item.Addresses : null,
        //                                 Contacts = show.Contains("contacts") ? item.Contacts : null,
        //                                 Orders = show.Contains("orders") ? item.Orders : null,
        //                             }).Skip(page - 1 * items).ToList();
        //    return items == 0 ? retval : retval.Take(items).ToList();
        //}

        //public static List<Product> GetProducts()
        //{
        //    return GetProductList();
        //}

        //public static Product GetProduct(string productId)
        //{
        //    return GetProductList().Where(obj => obj.ID == productId).FirstOrDefault();
        //}

        //public static List<Order> GetCustomerOrders(string customer)
        //{
        //    return (from item in GetOrderList()
        //            where item.Customer.ID == customer
        //            select new Order()
        //            {
        //                ID = item.ID,
        //                PurchaseOrder = item.PurchaseOrder,
        //                Customer = item.Customer
        //            }).ToList();
        //}

        //public static List<Order> GetCustomerOrder(string customer, string orderId)
        //{
        //    return (from item in GetOrderList()
        //            where item.Customer.ID == customer && item.ID == orderId
        //            select new Order()
        //            {
        //                ID = item.ID,
        //                PurchaseOrder = item.PurchaseOrder,
        //                Customer = item.Customer,
        //                Items = item.Items
        //            }).ToList();
        //}

        //public static Customer GetCustomer(string customer)
        //{
        //    Customer retval = GetCustomerList().Where(obj => obj.ID == customer).FirstOrDefault();
        //    if (retval.Addresses.Count > 0 && retval.PrimaryAddress == null)
        //    {
        //        retval.PrimaryAddress = new Address()
        //        {
        //            ID = retval.Addresses[0].ID,
        //            Description = retval.Addresses[0].Description,
        //            Street1 = retval.Addresses[0].Street1,
        //            Street2 = retval.Addresses[0].Street2,
        //            City = retval.Addresses[0].City,
        //            State = retval.Addresses[0].State,
        //            Zip = retval.Addresses[0].Zip
        //        };
        //    }
        //    return retval;
        //}

        //public static List<Contact> GetContacts(string customer)
        //{
        //    return GetCustomerList().Where(obj => obj.ID == customer).FirstOrDefault().Contacts;
        //}

        //public static Contact GetContact(string customer, string contact)
        //{
        //    Contact retval = GetContacts(customer).Where(obj => obj.ID == contact).FirstOrDefault();
        //    return retval;
        //}

        //// Create Methods
        //public static Customer CreateCustomer(Customer instance)
        //{
        //    List<Customer> companies = GetCustomerList();

        //    // Set ID's
        //    string ID = (companies.Max(a => Convert.ToInt32(a.ID)) + 1).ToString();
        //    instance.ID = ID;
        //    instance.PrimaryAddress.ID = string.Format("{0}-a1", ID);

        //    companies.Add(instance);
        //    SaveCustomers(companies);
        //    return instance;
        //}

        //public static Contact CreateContact(string customer, Contact instance)
        //{
        //    List<Contact> contacts = GetContacts(customer);

        //    // Set ID
        //    string ID = (contacts.Count + 1).ToString();
        //    instance.ID = string.Format("{0}-c{1}", customer, ID);

        //    contacts.Add(instance);
        //    SaveContacts(customer, contacts);
        //    return instance;
        //}

        //public static Order CreateOrder(Order instance)
        //{
        //    List<Order> orders = GetOrderList();

        //    // Set ID
        //    string ID = (int.Parse(orders.Max(a => a.ID)) + 1).ToString();
        //    instance.ID = ID;

        //    orders.Add(instance);
        //    SaveOrders(orders);
        //    return instance;
        //}

        //// Update Methods
        //public static Customer UpdateCustomer(Customer instance)
        //{
        //    List<Customer> companies = GetCustomerList();
        //    if (companies.Where(obj => obj.ID == instance.ID).Count() != 0)
        //        companies.Remove(companies.First(obj => obj.ID == instance.ID));
        //    companies.Add(instance);
        //    SaveCustomers(companies);
        //    return instance;
        //}

        //public static Contact UpdateContact(string customer, Contact instance)
        //{
        //    List<Contact> contacts = GetContacts(customer);
        //    contacts.Remove(contacts.First(obj => obj.ID == instance.ID));
        //    contacts.Add(instance);
        //    SaveContacts(customer, contacts);
        //    return instance;
        //}

        //public static Order UpdateOrder(Order instance)
        //{
        //    List<Order> orders = GetOrderList();
        //    orders.Remove(orders.First(obj => obj.ID == instance.ID));
        //    orders.Add(instance);
        //    SaveOrders(orders);
        //    return instance;
        //}

        //// Delete Methods
        //public static void DeleteCustomer(string customer)
        //{
        //    List<Customer> companies = GetCustomerList();
        //    companies.Remove(companies.First(obj => obj.ID == customer));
        //    SaveCustomers(companies);
        //}

        ////public static void DeleteContact(string customer, string contact)
        ////{
        ////    List<Contact> contacts = GetContacts(customer);
        ////    contacts.Remove(contacts.First(obj => obj.ID == contact));
        ////    SaveContacts(customer, contacts);
        ////}

        //public static void DeleteOrder(string order)
        //{
        //    List<Order> orders = GetOrderList();
        //    orders.Remove(orders.First(obj => obj.ID == order));
        //    SaveOrders(orders);
        //}

        ////
        //// File system Access 
        ////

        //static List<Product> GetProductList()
        //{
        //    string dataFilePath = Path.Combine(AppPath, Path.Combine("Xml", "Products.xml"));

        //    var loadedData = XDocument.Load(dataFilePath);
        //    using (var reader = loadedData.Root.CreateReader())
        //    {
        //        return (List<Product>)new XmlSerializer(typeof(List<Product>)).Deserialize(reader);
        //    }
        //}

        //static List<Order> GetOrderList()
        //{
        //    string dataFilePath = Path.Combine(AppPath, Path.Combine("Xml", "Orders.xml"));

        //    var loadedData = XDocument.Load(dataFilePath);
        //    using (var reader = loadedData.Root.CreateReader())
        //    {
        //        return (List<Order>)new XmlSerializer(typeof(List<Order>)).Deserialize(reader);
        //    }
        //}


        //static void SaveContacts(string customer, List<Contact> contacts)
        //{
        //    Customer instance = GetCustomer(customer);
        //    instance.Contacts = contacts;
        //    UpdateCustomer(instance);
        //}

        //static void SaveOrders(List<Order> orders)
        //{
        //    string dataFilePath = Path.Combine(AppPath, Path.Combine("Xml", "Orders.xml"));

        //    using (StreamWriter writer = new StreamWriter(dataFilePath))
        //    {
        //        var serializer = new XmlSerializer(typeof(List<Order>));
        //        serializer.Serialize(writer, orders);
        //    }
        //}


        internal static void SaveFinishedDay(Day currentDay)
        {
            List<Day> daySummaries = GetDaysList();
            var foundDay = daySummaries.FirstOrDefault(d => ((d.DayId == currentDay.DayId)
                && (d.WeekId == currentDay.WeekId)));
            if (foundDay != null)
                daySummaries.Remove(foundDay);
            daySummaries.Add(currentDay);
            SaveDays(daySummaries);
        }

        internal static Day GetLastFinishedDay()
        {
            var day = GetDaysList().OrderBy(d => d.WeekId.ToString() + d.DayId.ToString()).LastOrDefault();
            return day;
            
        }

        internal static void SaveWeekHistory(WeekSummary finishedWeek)
        {
            List<WeekSummary> weekSummaries = GetWeeksHistory();
            var foundWeek = weekSummaries.FirstOrDefault(w => w.WeekId == finishedWeek.WeekId);
            if (foundWeek != null)
                weekSummaries.Remove(foundWeek);
            weekSummaries.Add(finishedWeek);
            SaveWeeks(weekSummaries);
        }

        internal static List<Day> GetWeekDays(WeekSummary weekSummary)
        {
            var daysDone = GetDaysList().Where(d => d.WeekId == weekSummary.WeekId).ToList();
            var initialCount = daysDone.Count;
            for (int i = weekSummary.NoOfDays; i > initialCount; i--)
                daysDone.Add(new Day { DayId = i, Status = Status.NotStarted, WeekId = weekSummary.WeekId, WeekSummary = weekSummary });
            return daysDone.OrderBy(d => d.DayId).ToList();
        }
    }
}

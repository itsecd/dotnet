using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DynamicData;

using WPF4.Models;

namespace WPF4.Services
{
    public sealed class PersonListService
    {
        private static readonly TimeSpan RequestImitationDelay = TimeSpan.FromSeconds(3);
        private readonly SourceCache<Person, int> _persons = new(o => o.Id);

        public IObservable<IChangeSet<Person, int>> Connect()
        {
            return _persons.Connect();
        }

        public async Task Update()
        {
            await Task.Delay(RequestImitationDelay).ConfigureAwait(false);

            var persons = new List<Person> {
                new Person { Id=1, FirstName="Megan", LastName="Roberts", IsFemale = true },
                new Person { Id=2, FirstName="Michael", LastName="Campbell" },
                new Person { Id=3, FirstName="Cindy", LastName="Benson", IsFemale = true  },
                new Person { Id=4, FirstName="Christina", LastName="Hampton", IsFemale = true },
                new Person { Id=5, FirstName="Timothy", LastName="Carroll" },
                new Person { Id=6, FirstName="Joshua", LastName="Nichols" },
                new Person { Id=7, FirstName="Bradley", LastName="Gonzalez" }
            };

            _persons.Clear();
            _persons.AddOrUpdate(persons);
        }

        public async Task Add(Person person)
        {
            await Task.Delay(RequestImitationDelay).ConfigureAwait(false);

            var items = _persons.Items;
            person.Id = items.Any() ? items.Select(o => o.Id).Max() + 1 : 1;

            // В случае асинхронного формата общения с сервером
            // этой логике лучше находиться в месте,
            // где обрабатываются события с сервера
            _persons.AddOrUpdate(person);
        }

        public async Task Remove(Person person)
        {
            await Task.Delay(RequestImitationDelay).ConfigureAwait(false);

            // В случае асинхронного формата общения с сервером
            // этой логике лучше находиться в месте,
            // где обрабатываются события с сервера
            _persons.Remove(person);
        }
    }
}

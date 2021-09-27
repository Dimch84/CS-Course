using System;
using System.Collections.Generic;
using System.Text;

namespace EventsAndDelegates
{
    public class PeopleList : IEnumerable<Person>
    {
        List<Person> peopleList = new List<Person>();

        #region IEnumerable<Person> Members

        IEnumerator<Person> IEnumerable<Person>.GetEnumerator()
        {
            return peopleList.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
           return ((IEnumerable<string>)this).GetEnumerator();
        }

        #endregion

        public void AddPerson(Person item)
        {
            peopleList.Add(item);
        }

        public bool RemovePerson(Person item)
        {
            return peopleList.Remove(item);
        }

        public void InsertPerson(int index, Person item)
        {
            peopleList.Insert(index, item);
        }

        public void SortByAge()
        {
            peopleList.Sort(new SortPeopleByAge());
        }

        public void SortByLastName()
        {
            peopleList.Sort(new SortPeopleByLastName());
        }

        private class SortPeopleByAge : IComparer<Person>
        {

            #region IComparer<Person> Members

            int IComparer<Person>.Compare(Person x, Person y)
            {
                if (x.Age > y.Age) return 1;
                if (x.Age < y.Age) return -1;
                return 0;
            }

            #endregion
        }

        private class SortPeopleByLastName : IComparer<Person>
        {

            #region IComparer<Person> Members

            int IComparer<Person>.Compare(Person x, Person y)
            {
                return String.Compare(x.LastName, y.LastName);
            }

            #endregion
        }
    }
}
